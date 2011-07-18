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

        public String name() { return "Lolcat Plugin"; }

        public String help() { return "!lolcat"; }

        public String description() { return "Returns a random lolcat."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public LolcatPlugin() {
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!lolcat", RegexOptions.IgnoreCase);
            if (output.Success) {
                WebRequest webReq = WebRequest.Create("http://api.cheezburger.com/xml/category/cats/lol/random");
                webReq.Timeout = 10000;
                log.Info("Contacting service...");
                WebResponse response;
                try {
                    response = webReq.GetResponse();
                } catch (Exception e) {
                    log.Warn("Failed to obtain random lolcat.", e);
                    message.Chat.SendMessage("Failed to obtain random lolcat. Please try again later.");
                    return;
                }
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