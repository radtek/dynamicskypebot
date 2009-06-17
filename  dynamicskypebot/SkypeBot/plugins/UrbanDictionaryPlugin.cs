using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Windows.Forms;
using SKYPE4COMLib;
using SkypeBot.UrbanDictionary;

namespace SkypeBot.plugins {
    public class UrbanDictionaryPlugin : Plugin {
        public event MessageDelegate onMessage;
        
        // Signups for keys are closed
        // Found it at https://cyanox.nl/trac/noxbot/changeset/21
        private const string KEY = "a237993550175803efbf9530ff4de2bc";

        private UrbanSearchPortTypeClient dict;

        public String name() { return "UrbanDictionary plugin"; }

        public String help() { return "!urban <word>"; }

        public String description() { return "Does UrbanDictionary lookups."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public UrbanDictionaryPlugin() {
            dict = new UrbanSearchPortTypeClient();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!urban (.*)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String queryString = output.Groups[1].Value;
                if (!dict.verify_key(KEY)) {
                    logMessage("Invalid key used for dictionary lookup.", true);
                    message.Chat.SendMessage("The automated Urban Dictionary lookup is down until further notice.");
                }
                else {
                    logMessage("Key verified.", false);
                    Definition[] defs = dict.lookup(KEY, queryString);
                    if (defs.Length <= 0)
                        message.Chat.SendMessage(String.Format(@"UrbanDictionary lookup of ""{0}"": No results found.", queryString));
                    else {
                        message.Chat.SendMessage(String.Format(
                            "UrbanDictionary lookup of \"{0}\":\n\n{1}: {2}\n\n{3}",
                            queryString,
                            defs[0].word,
                            defs[0].definition,
                            defs[0].example
                        ));
                    }
                }
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}
