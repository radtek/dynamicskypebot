using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using SKYPE4COMLib;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using Google.GData.YouTube;
using Google.YouTube;
using Google.GData.Client;
using SkypeBot.plugins.config.youtube;
using System.Threading;


namespace SkypeBot.plugins {
    public class YouTubePlugin : Plugin {
        public event MessageDelegate onMessage;
        private Random random;
        private YouTubeRequest ytr;
        private Queue<String> randomCache;

        public String name() { return "YouTube Plugin"; }

        public String help() { return "!youtube [query]"; }

        public String description() { return "Gives title and rating information on posted YouTube links.\n" +
                                             "Also lets people search for YouTube videos.\n" +
                                             "Also gives random YouTube links."; }

        public bool canConfig() { return true; }
        public void openConfig() {
            YoutubePluginConfigForm ycf = new YoutubePluginConfigForm();
            ycf.Visible = true;
        }

        public YouTubePlugin() {
            random = new Random();
            ytr = new YouTubeRequest(
                    new YouTubeRequestSettings("Dynamic Skype Bot",
                                               "ytapi-SebastianPaaske-DynamicSkypeBot-b3hp906d-0",
                                               "AI39si59QPSboGTxgVnE0OD5nO49p1ok9KAoM0BuT9KkyL-VNzkrUA2F1O46FqArUrppYc5AGwrE-xQhaefb_cp4mgHuw36F9Q")
                    );
            randomCache = new Queue<string>();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
            if (randomCache.Count < PluginSettings.Default.YoutubeCacheSize) {
                generateRandomVideos(false);
            }
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"youtube\.\w{2,3}\S+v=([\w-]+)", RegexOptions.IgnoreCase);
            // Use non-breaking space as a marker for when to not show info.
            if (output.Success && !message.Body.Contains(" ")) {
                String youtubeId = output.Groups[1].Value;
                logMessage("Sending request to YouTube...", false);

                YouTubeQuery ytq = new YouTubeQuery("http://gdata.youtube.com/feeds/api/videos/" + youtubeId);

                Feed<Video> feed = ytr.Get<Video>(ytq);
                Video vid = feed.Entries.ElementAt<Video>(0);
                String title = vid.Title;
                String user = vid.Author;
                String rating = vid.RatingAverage.ToString();

                message.Chat.SendMessage(String.Format(@"YouTube: ""{0}"" (uploaded by: {1}) (avg rating: {2})", title, user, rating));
                return;
            }
            
            output = Regex.Match(message.Body, @"^!youtube (.+)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String query = output.Groups[1].Value;

                YouTubeQuery ytq = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
                ytq.Query = query;
                ytq.SafeSearch = YouTubeQuery.SafeSearchValues.None;
                ytq.NumberToRetrieve = 10;

                Feed<Video> feed = ytr.Get<Video>(ytq);
                int count = feed.Entries.Count<Video>();

                string url;
                if (count > 0) {
                    Video vid = feed.Entries.ElementAt<Video>(random.Next(count));
                    url = vid.WatchPage.ToString();
                } else {
                    url = "No matches found.";
                }

                message.Chat.SendMessage(String.Format(@"YouTube search for ""{0}"": {1}", query, url));
                return;
            }

            output = Regex.Match(message.Body, @"^!youtube", RegexOptions.IgnoreCase);
            if (output.Success) {
                logMessage("Got a request for a random video.", false);

                String url = randomCache.Count > 0 ? randomCache.Dequeue() : generateRandomVideos(true);

                message.Chat.SendMessage(String.Format(@"Random YouTube video: {0}", url));

                generateRandomVideos(false);
                return;
            }
        }

        public String generateRandomVideos(bool onlyOne) {
            if (onlyOne) {
                logMessage("Cache is empty; generating video...", false);
            } else {
                logMessage(String.Format("Cache currently contains {0} items; refilling to {1}...", randomCache.Count, PluginSettings.Default.YoutubeCacheSize), false);
            }
            while (randomCache.Count < PluginSettings.Default.YoutubeCacheSize) {
                try {
                    logMessage("Generating a random video...", false);
                    YouTubeQuery ytq = new YouTubeQuery(YouTubeQuery.MostPopular);
                    ytq.SafeSearch = YouTubeQuery.SafeSearchValues.None;
                    ytq.NumberToRetrieve = 40;

                    logMessage("Fetching list of most popular videos...", false);

                    Feed<Video> feed = ytr.Get<Video>(ytq);
                    int count = feed.Entries.Count<Video>();

                    Video vid = feed.Entries.ElementAt<Video>(random.Next(count));
                    logMessage("Picked \"" + vid.Title + "\" as my starting point.", false);
                    for (int i = 0; i < PluginSettings.Default.YoutubeIterations; i++) {
                        Feed<Video> related = ytr.GetRelatedVideos(vid);
                        count = related.Entries.Count<Video>();
                        vid = related.Entries.ElementAt<Video>(random.Next(count));
                        logMessage("Next link: " + vid.Title, false);
                    }

                    logMessage("Found my random video!", false);
                    String url = vid.WatchPage.ToString();

                    if (onlyOne) {
                        return url;
                    }

                    logMessage("Adding to cache...", false);
                    randomCache.Enqueue(url);
                } catch (Exception) {
                    logMessage("Failed in generating a video.", true);
                }
            }

            return null;
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}
