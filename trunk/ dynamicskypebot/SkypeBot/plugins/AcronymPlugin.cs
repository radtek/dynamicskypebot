using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Threading;
using System.Globalization;
using SKYPE4COMLib;
using SkypeBot.Properties;
using log4net;

namespace SkypeBot.plugins {
    public class AcronymPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Random random;
        private Dictionary<char, List<String>> wordList;

        public override String name() { return "Acronym Maker Plugin"; }

        public override String help() { return "!acro <text>"; }

        public override String description() { return "Expands acronyms."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public AcronymPlugin() {
            log.Debug("Loading words into dictionary...");
            random = new Random();
            wordList = new Dictionary<char, List<String>>(27);
            String[] wordlist = Resources.wordlist.Split('\n');
            foreach (String word in wordlist) {
                char firstLetter = word.ToCharArray()[0];
                if (!wordList.ContainsKey(firstLetter))
                    wordList.Add(firstLetter, new List<String>(100));

                wordList[firstLetter].Add(word.Trim());
            }
            log.Debug("Initialization complete.");
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!acro ([\w-.]+)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String acronym = output.Groups[1].Value.ToLower();
                log.Info(String.Format("Got request for acronym \"{0}\".", acronym));
                char[] acroChars = acronym.ToCharArray();
                String[] outputStr = new String[acroChars.Length];
                int i = 0;

                foreach (char letter in acroChars) {
                    try {
                        List<String> letterList = wordList[letter];
                        outputStr[i] = letterList[random.Next(letterList.Count)];
                    }
                    catch (Exception) {
                        outputStr[i] = Char.ToString(letter);
                    }
                    i++;
                }

                String expansion = String.Join(" ", outputStr);

                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                expansion = textInfo.ToTitleCase(expansion);

                log.Info(String.Format("Ended up with the expansion \"{0}\".", expansion));

                message.Chat.SendMessage(String.Format(@"As far as I know, the acronym ""{0}"" means ""{1}"".", acronym, expansion));
            }
        }
    }
}
