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
using System.Web;

namespace SkypeBot.plugins {
    public class FMLPlugin : Plugin {
        public event MessageDelegate onMessage;
        private Random random;

        public String name() { return "FML Plugin"; }

        public String help() { return "!fml [number]"; }

        public String description() { return "Gives a random FML from fmylife.com."; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public FMLPlugin() {
        }

        public void load() {
            if (random == null) {
                random = new Random();
            }
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!fml ?(\d*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                String url;
                if (output.Groups[1].Length != 0) {
                    url = "http://www.fmylife.com/miscellaneous/" + output.Groups[1].Value;
                } else {
                    url = "http://www.fmylife.com/random";
                }

                WebRequest webReq = WebRequest.Create(url);
                webReq.Timeout = 10000;
                logMessage("Connecting to FMyLife.com...", false);

                WebResponse response = webReq.GetResponse();
                logMessage("Response received; parsing...", false);
                String responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

                /**
                 * <div class="post"><p><a href="/miscellaneous/8492061" class="fmllink">
                 * Today, I learned the hard way that my foundation shows up under a black light.</a>
                 * <a href="/miscellaneous/8492061" class="fmllink"> At a black light party.</a>
                 * <a href="/miscellaneous/8492061" class="fmllink"> No one told me until afterwards.</a>
                 * <a href="/miscellaneous/8492061" class="fmllink"> Everyone took pictures.</a>
                 * <a href="/miscellaneous/8492061" class="fmllink"> FML</a>
                 * </p><div class="date"><div class="left_part">
                 * <a href="/miscellaneous/8492061" id="article_8492061" name="/resume/article/8492061?width=500" class="jTip">
                 * #8492061</a> (139)</div><div class="right_part"><p><span id="vote8492061"><a href="javascript:;"
                 * onclick="vote('8492061','10782','agree');">I agree, your life sucks</a> (10782)
                 * </span> - <span id="votebf8492061"><a href="javascript:;" onclick="vote('8492061','1998','deserve');" 
                 * class="bf">you totally deserved it</a> (1998)</span></p><p style="margin-top:2px;">On 02/20/2010 at 8:52pm -
                 * <a class="liencat" href="/miscellaneous">misc</a> - by makeuuuuup 
                 * (<a href="/gender/woman" class="light">woman</a>) - <a href="/country/Canada" class="liencat">Canada</a> 
                 * (<a href="/region/Alberta" class="light">Alberta</a>)</p></div></div><div class="more"><a href="javascript:;"
                 * onclick="plusToggle('8492061');"><img src="http://betacie.cachefly.net/fmylife/images/plus_mini.gif"></a>
                 * </div><div class="clear"></div><div id="plus8492061" class="plus"><div class="plus_bouton"
                 * onclick="document.location='/advantages';" id="subscribe"><div class="icone">
                 * <img src="http://betacie.cachefly.net/fmylife/images/subscribe.png"/></div><div class="label">
                 * Sign up for more!</div></div><div class="plus_bouton" 
                 * onclick="fbs_click('http://www.fmylife.com/miscellaneous/8492061','Fmylife.com - FML #8492061');">
                 * <div class="icone"><img src="http://betacie.cachefly.net/fmylife/images/facebook.gif"/></div>
                 * <div class="label">Share on Facebook</div></div><div class="plus_bouton" 
                 * onclick="document.location='/apps/retweet.php?id=8492061';"><div class="icone">
                 * <img src="http://betacie.cachefly.net/fmylife/images/retweet.png"/></div>
                 * <div class="label">ReTweet</div></div><div class="clear"></div><div class="content">
                 * </div><div class="clear"></div></div> 
                 */

                Regex fmlRx = new Regex(@"
                        <div\sclass=""post""><p>(.*?)</p>               # text
                        .*?
                        id=""article_(\d+)""                            # number
                        .*?
                        I\sagree,\syour\slife\ssucks</a>\s\((\d+)\)     # pro
                        .*?
                        you\stotally\sdeserved\sit</a>\s\((\d+)\)       # con
                    ",
                    RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline
                );

                MatchCollection fmlColl = fmlRx.Matches(responseText);
                if (fmlColl.Count == 0) {
                    message.Chat.SendMessage("FML not found.");
                    return;
                }

                Match fml = fmlColl[random.Next(fmlColl.Count)];

                String text = fml.Groups[1].Value;
                text = Regex.Replace(text, "<.+?>", "");
                text = HttpUtility.HtmlDecode(text);

                message.Chat.SendMessage(String.Format(
                    @"FML #{0} (+{2}/-{3}): {1}",
                    fml.Groups[2].Value,
                    text,
                    fml.Groups[3].Value,
                    fml.Groups[4].Value
                ));

            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   