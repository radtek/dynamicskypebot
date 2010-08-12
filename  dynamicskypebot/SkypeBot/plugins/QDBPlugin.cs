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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using log4net; 

namespace SkypeBot.plugins {
    public class QDBPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Queue<Quote> randomQuotes;

        public override String name() { return "QDB Plugin"; }

        public override String help() { return "!qdb [number]"; }

        public override String description() { return "Gives a random quote from qdb.us."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public QDBPlugin() {
            randomQuotes = new Queue<Quote>();
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
            if (randomQuotes.Count == 0) {
                log.Debug("No cached quotes; fetching...");
                fetchRandomQuotes();
            }
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        private void fetchRandomQuotes() {
            WebRequest webReq = WebRequest.Create("http://qdb.us/qdb.xml?action=random&fixed=0&client=Dynamic+Skype+Bot");
            webReq.Timeout = 10000;
            WebResponse response = webReq.GetResponse();
            String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

            XDocument doc = XDocument.Parse(responseText);
            XNamespace d = "http://purl.org/rss/1.0/";

            foreach (XElement item in doc.Descendants(d + "item")) {
                Quote q = new Quote();
                q.id = item.Element(d + "title").Value;

                String quoteText = item.Element(d + "description").Value;
                quoteText = Regex.Replace(quoteText, "<.+?>", "");
                quoteText = HttpUtility.HtmlDecode(quoteText);

                q.quote = quoteText;

                randomQuotes.Enqueue(q);
            }

            log.Debug(String.Format("Fetched {0} random quotes.", randomQuotes.Count));
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!qdb ?(\d*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                WebRequest webReq;
                WebResponse response;
                String responseText;
                String quoteText;
                Quote quote;

                if (output.Groups[1].Value != "") {
                    log.Info("No cached quotes; fetching...");
                    webReq = WebRequest.Create("http://qdb.us/qdb.xml?action=quote&quote="+output.Groups[1].Value+"&fixed=0&client=Dynamic+Skype+Bot");
                    webReq.Timeout = 10000;
                    response = webReq.GetResponse();
                    responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    if (responseText == "no quotes were found for these parameters.") {
                        message.Chat.SendMessage("No quote exists with that id.");
                        return;
                    }

                    XDocument doc = XDocument.Parse(responseText);
                    XNamespace d = "http://purl.org/rss/1.0/";

                    XElement item = doc.Descendants(d + "item").First();
                    quote = new Quote();
                    quote.id = item.Element(d + "title").Value;

                    quoteText = item.Element(d + "description").Value;
                    quoteText = Regex.Replace(quoteText, "<br>", "\n\n");
                    quoteText = Regex.Replace(quoteText, "<.+?>", "");
                    quoteText = HttpUtility.HtmlDecode(quoteText);

                    quote.quote = quoteText;
                } else {
                    log.Info("Fetching random quote...");

                    if (randomQuotes.Count == 0) {
                        log.Info("No cached quotes; fetching...");
                        fetchRandomQuotes();
                    }

                    log.Debug("Picking a random quote...");
                    quote = randomQuotes.Dequeue();
                }

                message.Chat.SendMessage(String.Format(
                    "QDB Quote {0}:\n\n{1}",
                    quote.id,
                    quote.quote
                ));
            }
        }

        private class Quote {
            public String quote;
            public String id;

            public override string ToString() {
                return "Quote " + id + ": " + quote;
            }
        }
    }
}   