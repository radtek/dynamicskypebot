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
            Match output = Regex.Match(message.Body, @"p(e+)nis", RegexOptions.IgnoreCase);
            if (output.Success) {
                message.Chat.SendMessage("8==" + output.Groups[1].Value.ToLower().Replace("e", "=") + "D");
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   