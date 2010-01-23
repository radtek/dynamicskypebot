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
using Procurios.Public;

namespace SkypeBot.plugins {
    public class GoogleImageSearchPlugin : Plugin {
        private Random random;

        public event MessageDelegate onMessage;

        public String name() { return "Google ImageSearch Plugin"; }

        public String help() { return "!gis <text>"; }

        public String description() { return "Searches Google ImageSearch."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public GoogleImageSearchPlugin() {
            random = new Random();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!gis (.*)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String queryString = output.Groups[1].Value;
                logMessage(String.Format(@"Performing Google Image Search for ""{0}""", queryString), false);
                logMessage("Constructing query...", false);
                WebRequest webReq = WebRequest.Create("http://www.google.com/uds/GimageSearch?context=0&lstkp=0&rsz=small&hl=en&source=gsc&gss=.com&sig=ceae2b35bf374d27b9d2d55288c6b495&q=" + queryString + "&safe=off&key=ABQIAAAAIlzQlYE_XUpT2_ADo1nSfRTv5U0terELrwVCXRNUOBWU3xrL0RRPairAEpQ82pc0k6AubNWYEj1e5g&v=1.0");
                //WebRequest webReq = WebRequest.Create("http://ajax.googleapis.com/ajax/services/search/images?v=1.0&q=" + queryString);
                webReq.Timeout = 10000;
                logMessage("Sending request to Google...", false);
                WebResponse response = webReq.GetResponse();
                logMessage("Response received; parsing...", false);
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Hashtable responseParsed = (Hashtable)JSON.JsonDecode(responseText);
                ArrayList resultsList = (ArrayList)((Hashtable)responseParsed["responseData"])["results"];
                logMessage(String.Format("Got {0} images.", resultsList.Count), false);
                if (resultsList.Count == 0)
                    message.Chat.SendMessage(String.Format(@"GIS for ""{0}"": No results found.", queryString));
                else {
                    Hashtable imageResult = (Hashtable)resultsList[random.Next(resultsList.Count)];
                    message.Chat.SendMessage(String.Format(@"GIS for ""{0}"": {1}", queryString, imageResult["url"]));
                }
                logMessage("Result sent to chat.", false);
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}
