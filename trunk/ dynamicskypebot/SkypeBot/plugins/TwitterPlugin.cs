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
using Dimebrain.TweetSharp.Fluent;
using Dimebrain.TweetSharp.Extensions;
using log4net;

namespace SkypeBot.plugins {
    public class TwitterPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public String name() { return "Twitter Plugin"; }

        public String help() { return "!twitter <username>"; }

        public String description() { return "Fetches the latest tweet by someone."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public TwitterPlugin() {
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!twitter (.+)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String query = output.Groups[1].Value;

                var tweets = FluentTwitter.CreateRequest()
                                          .Statuses()
                                          .OnUserTimeline()
                                          .For(query)
                                          .Request()
                                          .AsStatuses();

                if (tweets == null) {
                    message.Chat.SendMessage(
                        String.Format("I don't think \"{0}\" is a Twitter username.", query)
                    );
                } else {
                    var tweet = tweets.First();

                    message.Chat.SendMessage(
                        String.Format("{0} ({1})", tweet.Text, tweet.CreatedDate.ToRelativeTime(false))
                    );
                }
            }
        }
    }
}   