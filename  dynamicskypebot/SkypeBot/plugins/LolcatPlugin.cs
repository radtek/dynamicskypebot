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
using log4net;

namespace SkypeBot.plugins {
    public class LolcatPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override String name() { return "Lolcat Plugin"; }

        public override String help() { return "!lolcat"; }

        public override String description() { return "Returns a random lolcat."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public LolcatPlugin() {
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!lolcat", RegexOptions.IgnoreCase);
            if (output.Success) {
                WebRequest webReq = WebRequest.Create("http://api.cheezburger.com/xml/category/cats/lol/random");
                webReq.Timeout = 10000;
                log.Info("Contacting service...");
                WebResponse response = webReq.GetResponse();
                log.Info("Response received; parsing...");
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseText);
                XmlNode pic = xmlDoc.SelectSingleNode("/Lol/LolImageUrl");
                message.Chat.SendMessage("Random lolcat: "+pic.InnerText);
            }
        }
    }
}   