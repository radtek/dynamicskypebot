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
    public class PornPlugin : Plugin {
        public event MessageDelegate onMessage;

        private Random random;

        public String name() { return "Porn Plugin"; }

        public String help() { return "!porn"; }

        public String description() { return "Returns random porn."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public PornPlugin() {
            random = new Random();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!porn", RegexOptions.IgnoreCase);
            if (output.Success) {
                logMessage("Loading category list...", false);
                WebRequest webReq = WebRequest.Create("http://www.easygals.com/");
                webReq.Timeout = 10000;
                WebResponse response = webReq.GetResponse();
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                logMessage("Picking a category...", false);
                Regex categoryFinderRx = new Regex(@"<a class=""catLink"" href=""([^\s]+)"".*?>(.+?)</a>");
                MatchCollection categoryFinderColl = categoryFinderRx.Matches(responseText);
                if (categoryFinderColl.Count <= 0) {
                    message.Chat.SendMessage("Sorry, some kind of error occurred in trying to obtain porn. :(");
                    return;
                }
                Match categoryFinder = categoryFinderColl[random.Next(categoryFinderColl.Count)];

                logMessage("I think I'll go for some " + categoryFinder.Groups[2].Value + " today!", false);

                logMessage("Attempting to find some " + categoryFinder.Groups[2].Value + "...", false);

                webReq = WebRequest.Create("http://www.easygals.com/" + categoryFinder.Groups[1].Value);
                webReq.Timeout = 10000;
                response = webReq.GetResponse();
                responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Regex pornFinderRx = new Regex(@"<a href=""/cgi-bin/atx/out.+?u=(http:.+?)""");
                MatchCollection pornFinderColl = pornFinderRx.Matches(responseText);
                if (pornFinderColl.Count <= 0) {
                    message.Chat.SendMessage("Argh, I couldn't find any " + categoryFinder.Groups[2].Value + "! Bummer.");
                    return;
                }
                Match pornFinder = pornFinderColl[random.Next(pornFinderColl.Count)];

                logMessage("Porn found! Linking to chat.", false);

                message.Chat.SendMessage(String.Format(
                    @"Random porn link: {0}",
                    pornFinder.Groups[1].Value
                ));
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}