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
    public class DeviantArtPlugin : Plugin {
        public event MessageDelegate onMessage;

        public String name() { return "deviantART Plugin"; }

        public String help() { return "!dA"; }

        public String description() { return "Gives a random link to a deviation on deviantART."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public DeviantArtPlugin() {
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!dA", RegexOptions.IgnoreCase);
            if (output.Success) {
                logMessage("Requesting random page.", false);
                WebRequest webReq = WebRequest.Create("http://www.deviantart.com/random/deviation");
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();
                logMessage("Gotcha!", false);

                message.Chat.SendMessage(String.Format(
                    @"Random deviation: {0}",
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