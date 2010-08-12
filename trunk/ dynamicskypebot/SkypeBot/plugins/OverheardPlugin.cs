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
    public class OverheardPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private OverheardSite[] sites;
        private Random random;

        public override String name() { return "Overheard In... Plugin"; }

        public override String help() { return "!overheard [ny/office/beach]"; }

        public override String description() { return "Gives a random quote from one of the 'Overheard In...' sites."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public OverheardPlugin() {
        }

        public override void load() {
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
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!overheard ?(\w*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                log.Info("It's a-me! Determining active site...");
                OverheardSite activeSite = null;

                if (output.Groups[1].Length > 0) {
                    activeSite = (from site in sites
                                  where site.command == output.Groups[1].Value.ToLower()
                                  select site).SingleOrDefault();
                }

                if (activeSite == null) {
                    log.Info("Site not found or not chosen; picking at random...");
                    activeSite = sites[random.Next(sites.Length)];
                }

                log.Info("Picked " + activeSite.prettyName + "; fetching random quote...");
                WebRequest webReq = WebRequest.Create("http://www." + activeSite.urlname + "/bin/randomentry.cgi");
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();
                log.Info("Response received; parsing...");
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
                    log.Warn("Regex failed to match contents. Please file a bug report about this if the problem persists.");
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