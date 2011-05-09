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
using log4net;
using System.Web;

namespace SkypeBot.plugins {
    public class Rule34Plugin : Plugin {
        private Random random;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public String name() { return "Rule34 Plugin"; }

        public String help() { return "!rule34 <name>"; }

        public String description() { return "Gets a rule34 picture of something."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public Rule34Plugin() {
            random = new Random();
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!rule34 (.+)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String query = output.Groups[1].Value;
                String searchUri = "http://rule34.paheal.net/post/list?search=" + System.Uri.EscapeDataString(query);

                log.Info(String.Format("Searching rule34 for '{0}'", query));
                WebRequest webReq = WebRequest.Create(searchUri);
                webReq.Timeout = 20000;
                try {
                    webReq.GetResponse();
                } catch (WebException) {
                    log.Warn("rule34.paheal.net appears to be unavailable at the moment.");
                    message.Chat.SendMessage("Sorry, the website failed to respond in time. It may be down, or just slow.\nIf you want to try the search for yourself, go to http://rule34.paheal.net/post/list?search=" + System.Uri.EscapeDataString(query));
                    return;   
                }
                log.Info("Search completed; looking up result...");

                webReq = WebRequest.Create("http://rule34.paheal.net/post/list/" + System.Uri.EscapeDataString(query) + "/1");
                webReq.Timeout = 20000;
                WebResponse response;
                try {
                    response = webReq.GetResponse();
                } catch (WebException) {
                    log.Warn("rule34.paheal.net appears to be unavailable at the moment.");
                    message.Chat.SendMessage("Sorry, the website failed to respond in time. It may be down, or just slow.\nIf you want to try the search for yourself, go to http://rule34.paheal.net/post/list?search=" + System.Uri.EscapeDataString(query));
                    return;
                }

                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Regex imgFinderRx = new Regex(@"(?<=id='Imagesmain-toggle'.*)<a href='([^']*?)'>Image Only</a>", RegexOptions.Singleline);
                MatchCollection imgFinderColl = imgFinderRx.Matches(responseText);
                if (imgFinderColl.Count <= 0) {
                    message.Chat.SendMessage(String.Format("Sorry, couldn't find any rule34 pictures of {0}.", query));
                    return;
                }
                Match imgFinder = imgFinderColl[random.Next(imgFinderColl.Count)];
                log.Info("Picture found! Linking to chat.");

                message.Chat.SendMessage(String.Format(
                    @"Rule 34 picture of {0}: {1}",
                    query,
                    HttpUtility.UrlPathEncode(imgFinder.Groups[1].Value)
                ));
            }
        }
    }
}