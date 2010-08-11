using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;
using System.IO;
using System.Windows.Forms;
using SKYPE4COMLib;
using log4net;

namespace SkypeBot.plugins {
    public class BashPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Random random;

        public String name() { return "Bash.org Plugin"; }

        public String help() { return "!bash [number]"; }

        public String description() { return "Gives a random quote from bash.org."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public BashPlugin() {
            random = new Random();
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!bash ?(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                WebRequest webReq;
                WebResponse response;
                String responseText;

                int quoteNo;
                try {
                    quoteNo = int.Parse(output.Groups[1].Value);
                } catch (Exception) {
                    webReq = WebRequest.Create("http://bash.org/?random");
                    webReq.Timeout = 10000;
                    log.Info("Finding random quote...");
                    response = webReq.GetResponse();
                    log.Info("Response received; parsing...");
                    responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    Regex quoteNoRx = new Regex(@"<b>#(\d+)</b>");
                    MatchCollection quoteNoColl = quoteNoRx.Matches(responseText);
                    if (quoteNoColl.Count <= 0) {
                        message.Chat.SendMessage("Sorry, some kind of error occurred in trying to contact bash.org.");
                        return;
                    }
                    Match quoteNoFinder = quoteNoColl[random.Next(quoteNoColl.Count)];
                    quoteNo = int.Parse(quoteNoFinder.Groups[1].Value);
                }

                log.Info("Fetching bash.org quote #" + quoteNo);
                webReq = WebRequest.Create("http://bash.org/?" + quoteNo);
                webReq.Timeout = 10000;
                response = webReq.GetResponse();
                log.Info("Response received; parsing...");
                responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                /*
                <p class="quote"><a href="?4680" title="Permanent link to this quote."><b>#4680</b>
                </a> <a href="./?le=373af74fe92bc1e44c88f68b427855c4&amp;rox=4680" class="qa">+</a>
                (6567)<a href="./?le=373af74fe92bc1e44c88f68b427855c4&amp;sox=4680" class="qa">-</a>
                <a href="./?le=373af74fe92bc1e44c88f68b427855c4&amp;sux=4680"
                onClick="return confirm('Flag quote for review?');" class="qa">[X]</a></p>
                <p class="qt">&lt;Raize&gt; can you guys see what I type? <br />

&lt;vecna&gt; no, raize <br />
&lt;Raize&gt; How do I set it up so you can see it?</p>*/
                Regex quoteRx = new Regex(
                    @"<p class=""quote"">.*\+</a>\((-?\d+)\)<a.*</p><p class=""qt"">(.*)</p>",
                    RegexOptions.Singleline | RegexOptions.IgnoreCase
                );

                Match quoteMatch = quoteRx.Match(responseText);

                if (!quoteMatch.Success) {
                    log.Error("bash.org failed to respond or has changed format. Please submit bug report.");
                    message.Chat.SendMessage("Sorry, some kind of error occurred in trying to contact bash.org.");
                    return;
                }

                String rating = quoteMatch.Groups[1].Value;
                String quote = quoteMatch.Groups[2].Value;

                quote = quote.Replace("<br />", "");
                quote = HttpUtility.HtmlDecode(quote);

                message.Chat.SendMessage(String.Format(
                    "bash.org Quote #{0} ({1}):\n\n{2}",
                    quoteNo,
                    rating,
                    quote
                ));

            }
        }
    }
}   