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
    public class RandomLinkPlugin : Plugin {
        public event MessageDelegate onMessage;

        public String name() { return "Random Link Plugin"; }

        public String help() { return "!link"; }

        public String description() { return "Provides a random link."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public RandomLinkPlugin() {
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!link", RegexOptions.IgnoreCase);
            if (output.Success) {
                logMessage("Fetching link...", false);
                WebRequest webReq = WebRequest.Create("http://del.icio.us/recent?random");
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();

                message.Chat.SendMessage(String.Format(
                    @"Random link: {0}",
                    response.ResponseUri
                ));
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   