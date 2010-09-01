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
    public class SomethingAwfulPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public String name() { return "Something Awful Plugin"; }

        public String help() { return null; }

        public String description() { return "Shows thread info when SA links are posted."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public SomethingAwfulPlugin() {
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"forums.somethingawful.com/showthread.php.*threadid=(\d+)", RegexOptions.IgnoreCase);
            Match output2 = Regex.Match(message.Body, @"forums.somethingawful.com/showthread.php.*postid=(\d+)", RegexOptions.IgnoreCase);
            // Use non-breaking space as a marker for when to not show info.
            if ((output.Success || output2.Success) && !message.Body.Contains(" ")) {
                log.Debug("Hey, it's my turn now!");

                String url;
                WebRequest webReq;
                WebResponse response;
                String responseText;

                if (output.Success) {
                    log.Debug("Thread ID = " + output.Groups[1].Value);
                    url = "http://forums.somethingawful.com/showthread.php?threadid=" + output.Groups[1].Value;
                } else {
                    log.Debug("Post ID = " + output2.Groups[1].Value);
                    log.Info("Finding thread ID...");
                    webReq = WebRequest.Create("http://forums.somethingawful.com/showthread.php?action=showpost&noseen=1&postid=" + output2.Groups[1].Value);
                    webReq.Timeout = 10000;
                    response = webReq.GetResponse();
                    responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    Match threadIDMatch = Regex.Match(responseText, @"<td class=""postdate"">.*threadid=(\d+).*</td>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    if (!threadIDMatch.Success) {
                        log.Warn("Unable to find the thread in the live forums.");
                        log.Warn("If the thread is live and public, please file a bug report.");
                        message.Chat.SendMessage("Unable to find thread.");
                        return;
                    }
                    log.Debug("Thread ID = " + threadIDMatch.Groups[1].Value);
                    url = "http://forums.somethingawful.com/showthread.php?threadid=" + threadIDMatch.Groups[1].Value;
                }

                log.Debug("Fetching thread...");
                webReq = WebRequest.Create(url);
                webReq.Timeout = 10000;
                response = webReq.GetResponse();
                responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                log.Debug("Extracting information...");
                Match titleMatch = Regex.Match(responseText, @"<a[^>]*class=""bclast""[^>]*>(.*)</a>", RegexOptions.IgnoreCase);
                String title = titleMatch.Success ? titleMatch.Groups[1].Value : "Unknown Title";
                title = HttpUtility.HtmlDecode(title);

                Match opMatch = Regex.Match(responseText, @"<dt class="".*?author.*?"".*?>(.*?)</dt>", RegexOptions.IgnoreCase);
                String op = opMatch.Success ? opMatch.Groups[1].Value : "Unknown OP";
                op = HttpUtility.HtmlDecode(op);

                Match forumMatch = Regex.Match(responseText, @">([^>]*)</a> &gt; <a[^>]*class=""bclast""[^>]*>", RegexOptions.IgnoreCase);
                String forum = forumMatch.Success ? forumMatch.Groups[1].Value : "Unknown Subforum";
                forum = HttpUtility.HtmlDecode(forum);

                message.Chat.SendMessage(String.Format(
                    "SA: {0} > {1} ({2})",
                    forum,
                    title,
                    op
                ));
            }
        }
    }
}   