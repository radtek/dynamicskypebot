using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using SKYPE4COMLib;
using System.Xml.XPath;
using log4net;

namespace SkypeBot.plugins {
    public class DictionaryPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override String name() { return "Dictionary Plugin"; }

        public override String help() { return "!dict <word>"; }

        public override String description() { return "Returns dictionary definitions."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public DictionaryPlugin() {
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!dict (.*)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String queryString = output.Groups[1].Value;
                log.Info(String.Format(@"Performing dictionary lookup on ""{0}"".", queryString));

                XPathDocument response = new XPathDocument("http://www.onelook.com/?w=" + queryString + "&xml=1");
                XPathNavigator nav = response.CreateNavigator();

                XPathNodeIterator it = nav.Select("//OLQuickDef");

                String defPlural = (it.Count > 1 ? "s" : "");
                String definitions = "";
                int i = 0;
                foreach (XPathNavigator def in it) {
                    if (i >= 5) {
                        definitions += "\n\nMore than 5 matches found; only the first 5 are displayed.";
                        break;
                    }
                    definitions += Environment.NewLine + HttpUtility.HtmlDecode(def.Value.Trim());
                    i++;
                }

                if (definitions.Equals(""))
                    message.Chat.SendMessage(String.Format(@"Unable to define ""{0}"".", queryString));
                else {
                    message.Chat.SendMessage(
                        String.Format(
                            @"The word ""{0}"" has the following definition{1}:{2}",
                            queryString,
                            defPlural,
                            definitions)
                    );
                }
            }
        }
    }
}
