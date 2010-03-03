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
    public class OverheardPlugin : Plugin {
        public event MessageDelegate onMessage;

        private OverheardSite[] sites;
        private Random random;

        public String name() { return "Overheard In... Plugin"; }

        public String help() { return "!overheard [ny/office/beach]"; }

        public String description() { return "Gives a random quote from one of the 'Overheard In...' sites."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public OverheardPlugin() {
        }

        public void load() {
            if (sites == null) {
                sites = new OverheardSite[] {
                    new OverheardSite("ny", "overheardinnewyork.com", "Overheard In New York"),
                    new OverheardSite("office", "overheardintheoffice.com", "Overheard In The Office"),
                    new OverheardSite("beach", "overheardatthebeach.com", "Overheard At The Beach"),
                    // no random feature atm :
                    //new OverheardSite("everywhere", "overheardeverywhere.com", "Overheard Everywhere"),
                    //new OverheardSite("celebrity", "celebritywit.com", "Celebrity Wit"), 
                };
            }
            if (random == null) {
                this.random = new Random();
            }
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!overheard ?(\w*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                logMessage("It's a-me! Determining active site...", false);
                OverheardSite activeSite = null;

                if (output.Groups[1].Length > 0) {
                    activeSite = (from site in sites
                                  where site.command == output.Groups[1].Value.ToLower()
                                  select site).SingleOrDefault();
                }

                if (activeSite == null) {
                    logMessage("Site not found or not chosen; picking at random...", false);
                    activeSite = sites[random.Next(sites.Length)];
                }

                logMessage("Picked " + activeSite.prettyName + "; fetching random quote...", false);
                WebRequest webReq = WebRequest.Create("http://www." + activeSite.urlname + "/bin/randomentry.cgi");
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();
                logMessage("Response received; parsing...", false);
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Regex quoteRx = new Regex(@"
                        <h3\sclass=""title"">(.+?)</h3>
                        \s*
                        <p>(.+?)</p>
                    ",
                    RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline
                );

                Match quoteMatch = quoteRx.Match(responseText);
                if (!quoteMatch.Success) {
                    logMessage("Regex failed to match contents.", true);
                    message.Chat.SendMessage("Sorry, something went wrong. :(");
                    return;
                }

                String title = quoteMatch.Groups[1].Value;
                String contents = quoteMatch.Groups[2].Value;

                title = title.Trim();

                contents = contents.Replace("<br/>", "\n");
                contents = Regex.Replace(contents, "<.+?>", "");
                contents = HttpUtility.HtmlDecode(contents);

                message.Chat.SendMessage(String.Format(
                    "{0}: {1}\n{2}",
                    activeSite.prettyName,
                    title,
                    contents
                ));
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }

        private class OverheardSite {
            public String command;
            public String urlname;
            public String prettyName;

            public OverheardSite(String command, String urlname, String prettyName) {
                this.command = command;
                this.urlname = urlname;
                this.prettyName = prettyName;
            }
        }
    }
}   