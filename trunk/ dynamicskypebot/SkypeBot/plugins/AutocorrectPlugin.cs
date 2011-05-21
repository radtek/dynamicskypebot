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
using SkypeBot.utils;
using log4net;
using System.Web;

namespace SkypeBot.plugins {
    public class AutoCorrectPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Random random = new Random();

        private Queue<DYACEntry> buffer;

        public String name() { return "Damn You Auto Correct Plugin"; }

        public String help() { return "!dyac, !autocorrect"; }

        public String description() { return "Fetches an example of failed autocorrection from damnyouautocorrect.com."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public AutoCorrectPlugin() {
            buffer = new Queue<DYACEntry>(PluginSettings.Default.AutocorrectBufferSize);
        }

        public void load() {
            RefillBuffer();
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!(?:dyac|autocorrect)", RegexOptions.IgnoreCase);
            if (output.Success) {
                if (buffer.Count == 0) {
                    RefillBuffer();
                }

                lock (buffer) {
                    if (buffer.Count == 0) {
                        message.Chat.SendMessage("Unable to fetch any DYAC entries. Try again later.");
                        return;
                    }

                    DYACEntry entry = buffer.Dequeue();

                    message.Chat.SendMessage(
                        "{0}: {1}".FormatWith(entry.Title, entry.Url)
                    );
                }

                RefillBuffer();
            }
        }

        private void RefillBuffer() {
            int failures = 0;

            lock (buffer) {
                if (buffer.Count >= PluginSettings.Default.AutocorrectBufferSize) {
                    return;
                }

                log.Debug("Starting to refill buffer...");

                log.Debug("Contacting DYAC to fetch max page number...");
                WebRequest webReq = WebRequest.Create("http://damnyouautocorrect.com/");
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                log.Debug("Received response; parsing...");

                Regex maxPageFinderRx = new Regex(
                    @"<span class='gap'>...</span></li><li><a href='http://damnyouautocorrect.com/page/(\d+)/'",
                    RegexOptions.Singleline
                );
                Match maxPageRes = maxPageFinderRx.Match(responseText);
                if (!maxPageRes.Success) {
                    log.Error("Cannot find maximum page number!");
                    return;
                }

                int maxPage = Int32.Parse(maxPageRes.Groups[1].Value);
                log.Debug("Max page number is {0}.".FormatWith(maxPage));
                
                while (buffer.Count < PluginSettings.Default.AutocorrectBufferSize) {
                    if (failures > 5) {
                        log.Error("Failed to fetch entry 5 times, aborting.");
                        return;
                    }

                    int targetPage = random.Next(1, maxPage + 1);

                    log.Debug("Fetching page {0}...".FormatWith(targetPage));

                    webReq = WebRequest.Create("http://damnyouautocorrect.com/page/{0}/".FormatWith(targetPage));
                    webReq.Timeout = 10000;
                    response = webReq.GetResponse();
                    responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    Regex entryFinderRx = new Regex(
                        @"<h2>\s*<a href=""([^""]+)"" rel=""bookmark"".*?>(.+?)</a>",
                        RegexOptions.Singleline
                    );
                    MatchCollection entryColl = entryFinderRx.Matches(responseText);

                    if (entryColl.Count <= 0) {
                        log.Error("Failed to find entry.");
                        failures += 1;
                        continue;
                    }

                    Match entry = entryColl[random.Next(entryColl.Count)];

                    String name = HttpUtility.HtmlDecode(entry.Groups[2].Value);
                    String url = entry.Groups[1].Value;

                    log.Debug(String.Format("Adding {0} to buffer.", name));
                    buffer.Enqueue(new DYACEntry(name, url));
                }
            }

            log.Debug("Buffer refilled.");
        }

        private struct DYACEntry {
            public String Title, Url;

            public DYACEntry(String title, String url) {
                Title = title;
                Url = url;
            }
        }

    }
}   