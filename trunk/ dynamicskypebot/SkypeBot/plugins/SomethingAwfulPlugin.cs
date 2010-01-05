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

namespace SkypeBot.plugins {
    public class SomethingAwfulPlugin : Plugin {
        public event MessageDelegate onMessage;

        public String name() { return "Something Awful Plugin"; }

        public String help() { return null; }

        public String description() { return "Shows thread info when SA links are posted."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public SomethingAwfulPlugin() {
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"forums.somethingawful.com/showthread.php.*threadid=(\d+)", RegexOptions.IgnoreCase);
            Match output2 = Regex.Match(message.Body, @"forums.somethingawful.com/showthread.php.*postid=(\d+)", RegexOptions.IgnoreCase);
            // Use non-breaking space as a marker for when to not show info.
            if ((output.Success || output2.Success) && !message.Body.Contains(" ")) {
                logMessage("Hey, it's my turn now!", false);

                String url;
                WebRequest webReq;
                WebResponse response;
                String responseText;

                if (output.Success) {
                    logMessage("Thread ID = " + output.Groups[1].Value, false);
                    url = "http://forums.somethingawful.com/showthread.php?threadid=" + output.Groups[1].Value;
                } else {
                    logMessage("Post ID = " + output2.Groups[1].Value, false);
                    logMessage("Finding thread ID...", false);
                    webReq = WebRequest.Create("http://forums.somethingawful.com/showthread.php?action=showpost&noseen=1&postid=" + output2.Groups[1].Value);
                    webReq.Timeout = 10000;
                    response = webReq.GetResponse();
                    responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    Match threadIDMatch = Regex.Match(responseText, @"<td class=""postdate"">.*threadid=(\d+).*</td>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    if (!threadIDMatch.Success) {
                        message.Chat.SendMessage("Unable to find thread.");
                        return;
                    }
                    logMessage("Thread ID = " + threadIDMatch.Groups[1].Value, false);
                    url = "http://forums.somethingawful.com/showthread.php?threadid=" + threadIDMatch.Groups[1].Value;
                }

                logMessage("Fetching thread...", false);
                webReq = WebRequest.Create(url);
                webReq.Timeout = 10000;
                response = webReq.GetResponse();
                responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                logMessage("Extracting information...", false);
                Match titleMatch = Regex.Match(responseText, @"<a[^>]*class=""bclast""[^>]*>(.*)</a>", RegexOptions.IgnoreCase);
                String title = titleMatch.Success ? titleMatch.Groups[1].Value : "Unknown Title";

                Match opMatch = Regex.Match(responseText, @"<dt class=""author"">(.*)</dt>", RegexOptions.IgnoreCase);
                String op = opMatch.Success ? opMatch.Groups[1].Value : "Unknown OP";

                Match forumMatch = Regex.Match(responseText, @">([^>]*)</a> &gt; <a[^>]*class=""bclast""[^>]*>", RegexOptions.IgnoreCase);
                String forum = forumMatch.Success ? forumMatch.Groups[1].Value : "Unknown Subforum";

                message.Chat.SendMessage(String.Format(
                    "SA: {0} > {1} ({2})",
                    forum,
                    title,
                    op
                ));
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   