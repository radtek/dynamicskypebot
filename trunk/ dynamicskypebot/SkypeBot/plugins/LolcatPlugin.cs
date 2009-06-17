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
using System.Xml;

namespace SkypeBot.plugins {
    public class LolcatPlugin : Plugin {
        public event MessageDelegate onMessage;

        public String name() { return "Lolcat Plugin"; }

        public String help() { return "!lolcat"; }

        public String description() { return "Returns a random lolcat."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public LolcatPlugin() {
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!lolcat", RegexOptions.IgnoreCase);
            if (output.Success) {
                WebRequest webReq = WebRequest.Create("http://api.cheezburger.com/xml/category/cats/lol/random");
                webReq.Timeout = 10000;
                logMessage("Contacting service...", false);
                WebResponse response = webReq.GetResponse();
                logMessage("Response received; parsing...", false);
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseText);
                XmlNode pic = xmlDoc.SelectSingleNode("/Lol/LolImageUrl");
                message.Chat.SendMessage("Random lolcat: "+pic.InnerText);
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   