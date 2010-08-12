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
using log4net;

namespace SkypeBot.plugins {
    public class CypherPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override String name() { return "Cypher Plugin"; }

        public override String help() { return "!cypher rot<n> <text>, where <n> is a number from 0 to 25."; }

        public override String description() { return "Implements a few simple cyphers."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public CypherPlugin() {
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!cypher ([^ ]+) (.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                String cypher = output.Groups[1].Value;
                String plainText = output.Groups[2].Value;
                Match rotMatch = Regex.Match(cypher, @"^rot(\d+)$", RegexOptions.IgnoreCase);
                if (rotMatch.Success) {
                    int rot = int.Parse(rotMatch.Groups[1].Value)%26;
                    String code = caesar(plainText, rot);
                    message.Chat.SendMessage(String.Format("ROT{0} of \"{1}\" is \"{2}\".", rot, plainText, code));
                }
            }
        }

        private String caesar(String text, int rot) {
            char[] plain = text.ToCharArray();
            StringBuilder outStr = new StringBuilder();
                foreach (char c in plain) {
                    if (c >= 'a' && c <= 'z') {
                        int nc = (int)c + rot;
                        outStr.Append((char)(nc > (int)'z' ? nc - 26 : nc));
                    }
                    else if (c >= 'A' && c <= 'Z') {
                        int nc = (int)c + rot;
                        outStr.Append((char)(nc > (int)'Z' ? nc - 26 : nc));
                    }
                    else
                        outStr.Append(c);
                }

            return outStr.ToString();
        }
    }
}
