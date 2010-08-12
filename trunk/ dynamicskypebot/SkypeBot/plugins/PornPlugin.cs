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

namespace SkypeBot.plugins {
    public class PornPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Random random;

        public override String name() { return "Porn Plugin"; }

        public override String help() { return "!porn"; }

        public override String description() { return "Returns random porn."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public PornPlugin() {
            random = new Random();
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!porn", RegexOptions.IgnoreCase);
            if (output.Success) {
                log.Debug("Loading category list...");
                WebRequest webReq = WebRequest.Create("http://www.easygals.com/");
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                log.Debug("Picking a category...");
                Regex categoryFinderRx = new Regex(@"<a class=""catLink"" href=""([^\s]+)"".*?>(.+?)</a>");
                MatchCollection categoryFinderColl = categoryFinderRx.Matches(responseText);
                if (categoryFinderColl.Count <= 0) {
                    log.Warn("Couldn't find any porn categories.");
                    log.Warn("Please check if http://www.easygals.com/ works okay for you.");
                    log.Warn("If it appears to work fine and the problem persists, please submit a bug report.");
                    message.Chat.SendMessage("Sorry, some kind of error occurred in trying to obtain porn. :(");
                    return;
                }
                Match categoryFinder = categoryFinderColl[random.Next(categoryFinderColl.Count)];

                log.Info("I think I'll go for some " + categoryFinder.Groups[2].Value + " today!");

                log.Debug("Attempting to find some " + categoryFinder.Groups[2].Value + "...");

                webReq = WebRequest.Create("http://www.easygals.com/" + categoryFinder.Groups[1].Value + "&rs=1");
                webReq.Timeout = 10000;
                response = webReq.GetResponse();
                responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Regex pornFinderRx = new Regex(@"<a href=""/cgi-bin/atx/out.+?u=(http:.+?)""");
                MatchCollection pornFinderColl = pornFinderRx.Matches(responseText);
                if (pornFinderColl.Count <= 0) {
                    log.Warn("Couldn't find any " + categoryFinder.Groups[2].Value + " porn.");
                    log.Warn("Either the category is empty or the format of the site has changed.");
                    log.Warn("Please check if http://www.easygals.com/" + categoryFinder.Groups[1].Value + "&rs=1 loads okay.");
                    log.Warn("If it appears to work fine and the problem persists, please submit a bug report.");
                    message.Chat.SendMessage("Argh, I couldn't find any " + categoryFinder.Groups[2].Value + "! Bummer.");
                    return;
                }
                Match pornFinder = pornFinderColl[random.Next(pornFinderColl.Count)];

                log.Info("Porn found! Linking to chat.");

                message.Chat.SendMessage(String.Format(
                    @"Random porn link: {0}",
                    pornFinder.Groups[1].Value
                ));
            }
        }
    }
}