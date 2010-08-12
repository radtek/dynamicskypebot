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
    public class WafflePlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Random random;

        public override String name() { return "WaffleImages Plugin"; }

        public override String help() { return "!waffle"; }

        public override String description() { return "Links to a random image uploaded to WaffleImages."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public WafflePlugin() {
            random = new Random();
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!waffle", RegexOptions.IgnoreCase);
            if (output.Success) {
                WebRequest webReq = WebRequest.Create("http://waffleimages.com/random");
                webReq.Timeout = 10000;
                log.Info("Contacting server...");
                WebResponse response = webReq.GetResponse();
                log.Debug("Response received; parsing...");
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Match imgFinder = Regex.Match(responseText, @"http://img.waffleimages.com/[0-9a-f]+/r");

                if (!imgFinder.Success) {
                    log.Warn("Couldn't find any image in the result.");
                    log.Warn("If this event s");
                    message.Chat.SendMessage(@"Error communicating with server.");
                }
                else {
                    log.Debug("Result sent to chat.");
                    message.Chat.SendMessage(String.Format(@"Random WaffleImage: {0}", imgFinder.Value));
                }
                
            }
        }
    }
}   