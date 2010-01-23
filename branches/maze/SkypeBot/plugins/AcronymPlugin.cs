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

namespace SkypeBot.plugins {
    public class AcronymPlugin : Plugin {
        private Random random;
        private Dictionary<char, List<String>> wordList;

        public event MessageDelegate onMessage;

        public String name() { return "Acronym Maker Plugin"; }

        public String help() { return "!acro <text>"; }

        public String description() { return "Expands acronyms."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public AcronymPlugin() {
            random = new Random();
            wordList = new Dictionary<char, List<String>>(27);
            String[] wordlist = Resources.wordlist.Split('\n');
            foreach (String word in wordlist) {
                char firstLetter = word.ToCharArray()[0];
                if (!wordList.ContainsKey(firstLetter))
                    wordList.Add(firstLetter, new List<String>(100));

                wordList[firstLetter].Add(word.Trim());
            }
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!acro ([\w-.]+)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String acronym = output.Groups[1].Value.ToLower();
                logMessage(String.Format("Got request for acronym \"{0}\".", acronym), false);
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

                logMessage(String.Format("Ended up with the expansion \"{0}\".", expansion), false);

                message.Chat.SendMessage(String.Format(@"As far as I know, the acronym ""{0}"" means ""{1}"".", acronym, expansion));
                logMessage(String.Format("Result sent to chat.", expansion), false);
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}
