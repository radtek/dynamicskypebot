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

namespace SkypeBot.plugins {
    public class NotAlwaysRightPlugin : Plugin {
        public event MessageDelegate onMessage;

        public String name() { return "Not Always Right Plugin"; }

        public String help() { return "!notright"; }

        public String description() { return "Gives a story from Not Always Right."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public NotAlwaysRightPlugin() {
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!notright", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                WebRequest webReq = WebRequest.Create("http://notalwaysright.com/?random");
                webReq.Timeout = 10000;
                logMessage("Connecting to NotAlwaysRight.com...", false);

                WebResponse response = webReq.GetResponse();
                logMessage("Response received; parsing...", false);
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Regex notRightRx = new Regex(@"
                        <h3\sclass=""storytitle""><.+?>(.+?)</a></h3>  # title
                        <div\sid=""jobstyle"">(.+?)</div>              # job text
                        .+?
                        <div\sclass=""storycontent"">(.+?)</div>       # story
                    ",
                    RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline
                );

                Match match = notRightRx.Match(responseText);
                if (!match.Success) {
                    logMessage("Couldn't find any stories. Site layout changed?", true);
                    message.Chat.SendMessage("Sorry, I couldn't find any stories.");
                    return;
                }

                String title = match.Groups[1].Value;
                String job = match.Groups[2].Value;
                String story = match.Groups[3].Value;

                title = HttpUtility.HtmlDecode(title);
                title = title.Trim();

                job = HttpUtility.HtmlDecode(job);
                job = job.Trim();

                story = Regex.Replace(story, "<.+?>", "");
                story = HttpUtility.HtmlDecode(story);
                story = story.Trim();

                message.Chat.SendMessage(String.Format(
                    "Not Always Right: {0}\n{1}\n\n{2}",
                    title, job, story
                ));
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   