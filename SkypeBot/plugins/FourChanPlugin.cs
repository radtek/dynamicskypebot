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
    public class FourChanPlugin : Plugin {
        private Random random;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public String name() { return "4chan Plugin"; }

        public String help() { return "!4chan"; }

        public String description() { return "Links to a random picture from 4chan."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public FourChanPlugin() {
            random = new Random();
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!4chan", RegexOptions.IgnoreCase);
            if (output.Success) {
                log.Info("Going to visit /b/ to find a thread...");
                WebRequest webReq = WebRequest.Create("http://boards.4chan.org/b/");
                webReq.Timeout = 10000;
                WebResponse response;
                try {
                    response = webReq.GetResponse();
                } catch (WebException) {
                    log.Warn("4chan.org appears to be unavailable at the moment.");
                    message.Chat.SendMessage("Sorry, some kind of error occurred in trying to contact 4chan.");
                    return;   
                }
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Regex threadFinderRx = new Regex(@"<a href=""res/(\d+)"">Reply</a>");
                MatchCollection threadFinderColl = threadFinderRx.Matches(responseText);
                if (threadFinderColl.Count <= 0) {
                    log.Warn("4chan appears to have changed its thread-list format. Please report this on the suggestion page.");
                    message.Chat.SendMessage("Sorry, some kind of error occurred in trying to contact 4chan.");
                    return;
                }
                Match threadFinder = threadFinderColl[random.Next(threadFinderColl.Count)];
                

                log.Info("Thread located. Opening thread...");
                String threadId = threadFinder.Groups[1].Value;
                webReq = WebRequest.Create("http://boards.4chan.org/b/res/"+threadId);
                response = webReq.GetResponse();
                responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                log.Info("Thread opened. Locating a random picture...");
                Regex picFinderRx = new Regex(@"<a href=""http://images.4chan.org/b/src/(\d+\.\w+)"" target=_blank><img .+? md5=""[^""]+""></a>");
                MatchCollection picFinderColl = picFinderRx.Matches(responseText);
                if (picFinderColl.Count <= 0) {
                    log.Warn("For some reason, we couldn't find a picture on the page. Please report this on the suggestion page.");
                    message.Chat.SendMessage("Sorry, some kind of error occurred in trying to contact 4chan.");
                    return;
                }
                Match picFinder = picFinderColl[random.Next(picFinderColl.Count)];

                log.Warn("Picture found! Linking to chat.");

                message.Chat.SendMessage(String.Format(
                    @"Random picture from 4chan: http://images.4chan.org/b/src/{0}",
                    picFinder.Groups[1].Value
                ));
            }
        }
    }
}