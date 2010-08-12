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
    public class TextsFromLastNightPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Queue<String> randomTexts;

        public override String name() { return "Texts From Last Night Plugin"; }

        public override String help() { return "!text"; }

        public override String description() { return "Returns a random text from textsfromlastnight.com."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public TextsFromLastNightPlugin() {
            randomTexts = new Queue<String>();
        }

        public override void load() {
            if (randomTexts.Count == 0) {
                log.Debug("No random texts. Fetching new ones.");
                fetchRandomTexts();
            }
            log.Info("Plugin successfully loaded.");
        }

        private void fetchRandomTexts() {
            WebRequest webReq = WebRequest.Create("http://www.textsfromlastnight.com/Random-Texts-From-Last-Night.html");
            webReq.Timeout = 10000;
            log.Info("Connecting to TextsFromLastNight.com...");

            WebResponse response = webReq.GetResponse();
            log.Info("Response received; parsing...");
            String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Regex textRx = new Regex(@"
                    <textarea\sreadonly=""readonly"">(.*?)\s(http://tfl\.nu/.+?)</textarea>
                    .+?
                    Good\snight\s<span>\((\d+)\)</span>
                    .+?
                    Bad\snight\s<span>\((\d+)\)</span>
                ",
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline
            );

            MatchCollection textColl = textRx.Matches(responseText);
            if (textColl.Count == 0) {
                log.Warn("Couldn't find any texts. Site layout changed?");
                log.Warn("If the problem persists, file a bug.");
                return;
            }

            int cnt = 0;
            foreach (Match text in textColl) {
                String msg = text.Groups[1].Value;
                msg = HttpUtility.HtmlDecode(msg);

                String url = text.Groups[2].Value;
                String good = text.Groups[3].Value;
                String bad = text.Groups[4].Value;

                randomTexts.Enqueue(String.Format(
                    "Texts From Last Night (+{0}/-{1}): {3}\n{2}",
                    good, bad, msg, url
                ));
                cnt++;
            }

            log.Debug(String.Format("Added {1} new texts to the cache, which now contains {0} random texts.", randomTexts.Count, cnt));
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!text", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                if (randomTexts.Count == 0) {
                    message.Chat.SendMessage("No random text messages available; try again in a few seconds.");
                } else {
                    message.Chat.SendMessage(randomTexts.Dequeue());
                }
                if (randomTexts.Count < 4) {
                    log.Debug("Running low on cached messages; refilling...");
                    fetchRandomTexts();
                }
            }
        }
    }
}   