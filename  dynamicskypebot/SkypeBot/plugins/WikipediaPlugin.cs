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
using System.Xml.Linq;
using System.Web;
using System.Xml;
using log4net;

namespace SkypeBot.plugins {
    public class WikipediaPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Random random;

        public override String name() { return "Wikipedia Plugin"; }

        public override String help() { return "!wiki"; }

        public override String description() { return "Generates a random Wikipedia link."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public WikipediaPlugin() {
            random = new Random();
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!wiki ?(.*)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String query = output.Groups[1].Value.Trim();
                if (query.Length > 0) {
                    lookupArticle(message, query);
                } else {
                    findRandomPage(message);
                }
            }
        }

        private void lookupArticle(IChatMessage message, String query) {
            log.Info("Looking up article on '"+query+"'...");
            WebRequest webReq = WebRequest.Create("http://en.wikipedia.org/w/api.php?action=query&titles="+HttpUtility.UrlEncode(query)+"&export&redirects&format=xml");
            (webReq as HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.1.249.1021 Safari/532.5";
            webReq.Timeout = 10000;
            WebResponse response = webReq.GetResponse();
            log.Debug("Gotcha!");
            String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

            XDocument doc = XDocument.Parse(responseText);
            var exps = doc.Descendants("export");
            if (exps.Count() == 0) {
                message.Chat.SendMessage("Cannot find that article; sorry.");
                return;
            }
            XElement exp = exps.First();

            doc = XDocument.Parse(exp.Value);
            XNamespace d = "http://www.mediawiki.org/xml/export-0.4/";

            var contents = doc.Descendants(d + "text");
            if (contents.Count() == 0) {
                message.Chat.SendMessage("Cannot find that article; sorry.");
                return;
            }
            XElement content = contents.First();
            String articleText = content.Value;

            //////////////////////////////
            // Clean up the article text
            //////////////////////////////
            // A few important templates
            articleText = Regex.Replace(articleText, @"\{\{pron-\w+\|([^|}]*)\|?(.*?)\}\}", "Pronounced: $1");
            articleText = Regex.Replace(articleText, @"\{\{IPA-\w+\|([^|}]*)\|?(.*?)\}\}", "$2 [$1]");
            articleText = Regex.Replace(articleText, @"\{\{lang-(\w+)\|(.*?)\}\}", "$1: $2");
            // Remove templates
            int cnt = 0;
            while (Regex.Match(articleText, "{{").Success && cnt++ < 10) {
                articleText = Regex.Replace(articleText, @"\{\{[^{]+?\}\}", "", RegexOptions.Singleline);
            }
            // Remove comments
            articleText = Regex.Replace(articleText, "<!--.+?-->", "", RegexOptions.Singleline);
            // Remove math tags
            articleText = Regex.Replace(articleText, "<math>(.+?)</math>", "$1", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            // Remove "behavior switches" : http://en.wikipedia.org/wiki/Help:Magic_words#Behavior_switches
            articleText = Regex.Replace(articleText, "__(NOTOC|FORCETOC|TOC|NOEDITSECTION|NEWSECTIONLINK|NONEWSECTIONLINK|NOGALLERY|HIDDENCAT|INDEX|NOINDEX)__", "", RegexOptions.IgnoreCase);
            // Turn wikilinks into plaintext
            articleText = Regex.Replace(articleText, @"\[\[(?!\w+:)(?:.*?|)?([^\]\|]*)\]\]", "$1");
            // Remove images, inter-wiki links, ...
            articleText = Regex.Replace(articleText, @"\[\[\w+:.+?\]\]", ""); 
            // Remove emphasis
            articleText = Regex.Replace(articleText, "'{2,}", "");
            // Remove references
            articleText = Regex.Replace(articleText, "<ref.*?>.*?</ref>", "", RegexOptions.Singleline);
            // Remove nowiki-tags
            articleText = Regex.Replace(articleText, "<nowiki.*?>.*?</nowiki>", "", RegexOptions.Singleline);
            // Strip remaining HTML-tags
            articleText = Regex.Replace(articleText, "<.+?>", "");
            // Remove all text after first header
            articleText = Regex.Replace(articleText, "==.+?==.*", "", RegexOptions.Singleline);
            // Decode HTML entities
            articleText = HttpUtility.HtmlDecode(articleText);
            // Trim spaces
            articleText = articleText.Trim();

            message.Chat.SendMessage(articleText);
        }

        private void findRandomPage(IChatMessage message) {
            log.Info("Requesting random page.");
            WebRequest webReq = WebRequest.Create("http://en.wikipedia.org/w/api.php?action=query&generator=random&grnnamespace=0&prop=info&inprop=url&format=xml");
            (webReq as HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.1.249.1021 Safari/532.5";
            webReq.Timeout = 10000;
            WebResponse response = webReq.GetResponse();
            log.Debug("Gotcha!");
            String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Match getUrl = Regex.Match(responseText, @"fullurl=""([^""]+)""", RegexOptions.IgnoreCase);

            if (getUrl.Success) {
                message.Chat.SendMessage(String.Format(
                    @"Random Wikipedia page: {0}",
                    getUrl.Groups[1].Value
                ));
            } else {
                log.Warn("Something went wrong in finding a random page.");
            }
        }
    }
}   