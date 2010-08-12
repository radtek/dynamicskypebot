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
using log4net;

namespace SkypeBot.plugins {
    public class GoogleImageSearchPlugin : Plugin {
        private Random random;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override String name() { return "Google ImageSearch Plugin"; }

        public override String help() { return "!gis <text>"; }

        public override String description() { return "Searches Google ImageSearch."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public GoogleImageSearchPlugin() {
            random = new Random();
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!gis (.*)", RegexOptions.IgnoreCase);
            if (output.Success) {
                String queryString = output.Groups[1].Value;
                log.Info(String.Format(@"Performing Google Image Search for ""{0}""", queryString));
                log.Debug("Constructing query...");
                WebRequest webReq = WebRequest.Create("http://www.google.com/uds/GimageSearch?context=0&lstkp=0&rsz=small&hl=en&source=gsc&gss=.com&sig=ceae2b35bf374d27b9d2d55288c6b495&q=" + queryString + "&safe=off&key=ABQIAAAAIlzQlYE_XUpT2_ADo1nSfRTv5U0terELrwVCXRNUOBWU3xrL0RRPairAEpQ82pc0k6AubNWYEj1e5g&v=1.0");
                //WebRequest webReq = WebRequest.Create("http://ajax.googleapis.com/ajax/services/search/images?v=1.0&q=" + queryString);
                webReq.Timeout = 10000;
                log.Info("Sending request to Google...");
                WebResponse response = webReq.GetResponse();
                log.Info("Response received; parsing...");
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Hashtable responseParsed = (Hashtable)JSON.JsonDecode(responseText);
                ArrayList resultsList = (ArrayList)((Hashtable)responseParsed["responseData"])["results"];
                log.Info(String.Format("Got {0} images.", resultsList.Count));
                if (resultsList.Count == 0)
                    message.Chat.SendMessage(String.Format(@"GIS for ""{0}"": No results found.", queryString));
                else {
                    Hashtable imageResult = (Hashtable)resultsList[random.Next(resultsList.Count)];
                    message.Chat.SendMessage(String.Format(@"GIS for ""{0}"": {1}", queryString, imageResult["url"]));
                }
            }
        }
    }
}
