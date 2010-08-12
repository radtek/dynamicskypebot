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
    public class DeviantArtPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override String name() { return "deviantART Plugin"; }

        public override String help() { return "!dA"; }

        public override String description() { return "Gives a random link to a deviation on deviantART."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public DeviantArtPlugin() {
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!dA", RegexOptions.IgnoreCase);
            if (output.Success) {
                log.Info("Requesting random page...");
                WebRequest webReq = WebRequest.Create("http://www.deviantart.com/random/deviation");
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();
                log.Debug("Gotcha!");

                message.Chat.SendMessage(String.Format(
                    @"Random deviation: {0}",
                    response.ResponseUri
                ));
            }
        }
    }
}   