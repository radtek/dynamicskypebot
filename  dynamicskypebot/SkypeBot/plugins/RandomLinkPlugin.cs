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
    public class RandomLinkPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override String name() { return "Random Link Plugin"; }

        public override String help() { return "!link"; }

        public override String description() { return "Provides a random link."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public RandomLinkPlugin() {
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!link", RegexOptions.IgnoreCase);
            if (output.Success) {
                log.Info("Fetching link...");
                WebRequest webReq = WebRequest.Create("http://del.icio.us/recent?random");
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();

                message.Chat.SendMessage(String.Format(
                    @"Random link: {0}",
                    response.ResponseUri
                ));
            }
        }
    }
}   