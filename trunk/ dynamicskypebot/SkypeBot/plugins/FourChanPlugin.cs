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
    public class FourChanPlugin : Plugin {
        private Random random;
        
        public event MessageDelegate onMessage;

        public String name() { return "4chan Plugin"; }

        public String help() { return "!4chan"; }

        public String description() { return "Links to a random picture from 4chan."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public FourChanPlugin() {
            random = new Random();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!4chan", RegexOptions.IgnoreCase);
            if (output.Success) {
                logMessage("Going to visit /b/ to find a thread...", false);
                WebRequest webReq = WebRequest.Create("http://boards.4chan.org/b/");
                webReq.Timeout = 10000;
                WebResponse response;
                try {
                    response = webReq.GetResponse();
                } catch (WebException e) {
                    message.Chat.SendMessage("Sorry, some kind of error occurred in trying to contact 4chan.");
                    return;   
                }
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Regex threadFinderRx = new Regex(@"<a href=""res/(\d+)"">Reply</a>");
                MatchCollection threadFinderColl = threadFinderRx.Matches(responseText);
                if (threadFinderColl.Count <= 0) {
                    message.Chat.SendMessage("Sorry, some kind of error occurred in trying to contact 4chan.");
                    return;
                }
                Match threadFinder = threadFinderColl[random.Next(threadFinderColl.Count)];
                

                logMessage("Thread located. Opening thread...", false);
                String threadId = threadFinder.Groups[1].Value;
                webReq = WebRequest.Create("http://boards.4chan.org/b/res/"+threadId);
                response = webReq.GetResponse();
                responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                logMessage("Thread opened. Locating a random picture...", false);
                Regex picFinderRx = new Regex(@"<a href=""http://images.4chan.org/b/src/(\d+\.\w+)"" target=_blank><img .+? md5=""[^""]+""></a>");
                MatchCollection picFinderColl = picFinderRx.Matches(responseText);
                if (picFinderColl.Count <= 0) {
                    message.Chat.SendMessage("Sorry, some kind of error occurred in trying to contact 4chan.");
                    return;
                }
                Match picFinder = picFinderColl[random.Next(picFinderColl.Count)];

                logMessage("Picture found! Linking to chat.", false);

                message.Chat.SendMessage(String.Format(
                    @"Random picture from 4chan: http://images.4chan.org/b/src/{0}",
                    picFinder.Groups[1].Value
                ));
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}