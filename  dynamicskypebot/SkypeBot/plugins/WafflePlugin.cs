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
    public class WafflePlugin : Plugin {
        public event MessageDelegate onMessage;
        private Random random;

        public String name() { return "WaffleImages Plugin"; }

        public String help() { return "!waffle"; }

        public String description() { return "Links to a random image uploaded to WaffleImages."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public WafflePlugin() {
            random = new Random();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!waffle", RegexOptions.IgnoreCase);
            if (output.Success) {
                WebRequest webReq = WebRequest.Create("http://waffleimages.com/random");
                webReq.Timeout = 10000;
                logMessage("Contacting server...", false);
                WebResponse response = webReq.GetResponse();
                logMessage("Response received; parsing...", false);
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Match imgFinder = Regex.Match(responseText, @"http://img.waffleimages.com/[0-9a-f]+/r");

                if (!imgFinder.Success) {
                    logMessage("Couldn't find any image in the result.", true);
                    message.Chat.SendMessage(@"Error communicating with server.");
                }
                else {
                    logMessage("Result sent to chat.", false);
                    message.Chat.SendMessage(String.Format(@"Random WaffleImage: {0}", imgFinder.Value));
                }
                
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   