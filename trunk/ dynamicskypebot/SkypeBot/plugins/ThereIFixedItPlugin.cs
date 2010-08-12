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
using System.Web;
using log4net;

namespace SkypeBot.plugins {
    public class ThereIFixedItPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public String name() { return "There, I Fixed It Plugin"; }

        public String help() { return "!fixedit"; }

        public String description() { return "Grab a random picture from ThereIFixedIt.com."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public ThereIFixedItPlugin() {
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!fixedit", RegexOptions.IgnoreCase);
            if (output.Success) {
                WebRequest webReq = WebRequest.Create("http://thereifixedit.com/?random");
                webReq.Timeout = 10000;
                log.Debug("Contacting site...");
                WebResponse response = webReq.GetResponse();
                log.Debug("Response received; parsing...");
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Match fixImg = Regex.Match(
                    responseText,
                    @"<title>\s*(.*?) - There, I Fixed It.*?<link rel=""image_src"" href="".*?(http://thereifixedit\.files\.wordpress.*?)""",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline
                );
                if (!fixImg.Success) {
                    message.Chat.SendMessage("Unable to fetch an image. Try again, maybe?");
                    return;
                }

                String title = fixImg.Groups[1].Value;
                title = Regex.Replace(title, "<.+?>", "");
                title = HttpUtility.HtmlDecode(title);

                message.Chat.SendMessage(
                    String.Format(
                        "There, I Fixed It: {0}\n{1}",
                        title,
                        fixImg.Groups[2].Value
                    )
                );
            }
            
        }
    }
}   