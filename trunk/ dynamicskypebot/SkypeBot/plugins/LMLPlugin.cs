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
    public class LMLPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Random random;

        public String name() { return "LML Plugin"; }

        public String help() { return "!lml [number]"; }

        public String description() { return "Gives a random LML from lmylife.com."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public LMLPlugin() {
        }

        public void load() {
            if (random == null) {
                random = new Random();
            }
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!lml ?(\d*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                String url;
                if (output.Groups[1].Length != 0) {
                    url = "http://www.lmylife.com/index.php?rant=" + output.Groups[1].Value;
                } else {
                    url = "http://www.lmylife.com/?sort=random";
                }

                WebRequest webReq = WebRequest.Create(url);
                webReq.Timeout = 10000;
                log.Info("Connecting to LMyLife.com...");

                WebResponse response = webReq.GetResponse();
                log.Info("Response received; parsing...");
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Regex lmlRx = new Regex(@"
                        <div\sclass=""item_box"">\s*<p>
                        (?:<img\ssrc=""images/new.gif"">)?\s*
                        (?:<a\shref=""index.php\?rant=\d+"">)?    
                        (.+?)                                         # text
                        (?:</a>)?</p>
                        .+?
                        ref='(\d+)'                                   # number
                        .+?
                        I\ssalute\syou</a>\s\((\d+)\)\s*</span>       # positives
                        .+?
                        You\sreally\ssuck</a>\s\((\d+)\)\s*</span>    # negatives
                    ",
                    RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline
                );

                MatchCollection lmlColl = lmlRx.Matches(responseText);
                if (lmlColl.Count == 0) {
                    message.Chat.SendMessage("LML not found.");

                    if (url.EndsWith("random")) {
                        log.Warn("There appears to be a problem with LMyLife.com.");
                        log.Warn("Please go check if the site works okay. If it doesn't, it'll fix itself, hopefully.");
                        log.Warn("If the site does work but the bot refuses to give LMLs, please report them on the suggestion site.");
                    }
                    return;
                }

                Match lml = lmlColl[random.Next(lmlColl.Count)];

                String text = lml.Groups[1].Value;
                text = Regex.Replace(text, "<.+?>", "");
                text = HttpUtility.HtmlDecode(text);

                message.Chat.SendMessage(String.Format(
                    @"LML #{0} (+{2}/-{3}): {1}",
                    lml.Groups[2].Value,
                    text,
                    lml.Groups[3].Value,
                    lml.Groups[4].Value
                ));

            }
        }
    }
}   