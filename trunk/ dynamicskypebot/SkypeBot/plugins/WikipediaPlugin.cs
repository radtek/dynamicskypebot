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
    public class WikipediaPlugin : Plugin {
        public event MessageDelegate onMessage;
        private Random random;

        public String name() { return "Wikipedia Plugin"; }

        public String help() { return "!wiki"; }

        public String description() { return "Generates a random Wikipedia link."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public WikipediaPlugin() {
            random = new Random();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!wiki", RegexOptions.IgnoreCase);
            if (output.Success) {
                logMessage("Requesting random page.", false);
                WebRequest webReq = WebRequest.Create("http://en.wikipedia.org/w/api.php?action=query&generator=random&grnnamespace=0&prop=info&inprop=url&format=xml");
                (webReq as HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.1.249.1021 Safari/532.5";
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();
                logMessage("Gotcha!", false);
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Match getUrl = Regex.Match(responseText, @"fullurl=""([^""]+)""", RegexOptions.IgnoreCase);

                if (getUrl.Success) {
                    message.Chat.SendMessage(String.Format(
                        @"Random Wikipedia page: {0}",
                        getUrl.Groups[1].Value
                    ));
                } else {
                    logMessage("Something went wrong.", true);
                }
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   