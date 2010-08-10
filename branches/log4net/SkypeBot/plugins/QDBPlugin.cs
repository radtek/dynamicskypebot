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

namespace SkypeBot.plugins {
    public class QDBPlugin : Plugin {
        public event MessageDelegate onMessage;

        private Queue<Quote> randomQuotes;

        public String name() { return "QDB Plugin"; }

        public String help() { return "!qdb [number]"; }

        public String description() { return "Gives a random quote from qdb.us."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public QDBPlugin() {
            randomQuotes = new Queue<Quote>();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
            if (randomQuotes.Count == 0) {
                logMessage("No cached quotes; fetching...", false);
                fetchRandomQuotes();
            }
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
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

            logMessage(String.Format("Fetched {0} random quotes.", randomQuotes.Count), false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!qdb ?(\d*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                WebRequest webReq;
                WebResponse response;
                String responseText;
                String quoteText;
                Quote quote;

                if (output.Groups[1].Value != "") {
                    logMessage("No cached quotes; fetching...", false);
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
                    logMessage("Fetching random quote...", false);

                    if (randomQuotes.Count == 0) {
                        logMessage("No cached quotes; fetching...", false);
                        fetchRandomQuotes();
                    }

                    logMessage("Picking a random quote...", false);
                    quote = randomQuotes.Dequeue();
                }

                message.Chat.SendMessage(String.Format(
                    "QDB Quote {0}:\n\n{1}",
                    quote.id,
                    quote.quote
                ));
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
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