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

namespace SkypeBot.plugins {
    public class PenisPlugin : Plugin {
        public event MessageDelegate onMessage;

        public String name() { return "Penis Plugin"; }

        public String help() { return null; }

        public String description() { return "Responds to penis queries."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public PenisPlugin() {
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            MatchCollection outputColl = Regex.Matches(message.Body, @"p(e+)nis|c(o+)ck|d(o+)ng|clitoris|d(i+)ck|s(h+)long", RegexOptions.IgnoreCase);
            if (outputColl.Count > 0) {
                String outputString = "";

                foreach (Match output in outputColl) {
                    String midPart = "";
                    foreach (System.Text.RegularExpressions.Group g in output.Groups) {
                        if (g != output.Groups[0])
                            midPart += g.Value;
                    }

                    midPart = Regex.Replace(midPart, ".", "=");

                    outputString += (outputString == "" ? "" : " ") +
                                    "8==" +
                                    midPart +
                                    "D";
                }
                message.Chat.SendMessage(outputString);
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   