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

        public String name() { return "Random Link Plugin"; }

        public String help() { return "!link"; }

        public String description() { return "Provides a random link."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public RandomLinkPlugin() {
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
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