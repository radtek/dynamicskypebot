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
using log4net;

namespace SkypeBot.plugins {
    public class FMLPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Random random;

        public override String name() { return "FML Plugin"; }

        public override String help() { return "!fml [number]"; }

        public override String description() { return "Gives a random FML from fmylife.com."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public FMLPlugin() {
        }

        public override void load() {
            if (random == null) {
                random = new Random();
            }
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
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
                log.Info("Connecting to FMyLife.com...");

                WebResponse response = webReq.GetResponse();
                log.Info("Response received; parsing...");
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
                        <div\sclass=""post""\sid=""(\d+)"">             # id
                        <p>(.*?)</p>                                    # text
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

                    if (url.EndsWith("random")) {
                        log.Warn("There appears to be a problem with FMyLife.com.");
                        log.Warn("Please go check if the site works okay. If it doesn't, it'll fix itself, hopefully.");
                        log.Warn("If the site does work but the bot refuses to give FMLs, please report them on the suggestion site.");
                    }
                    return;
                }

                Match fml = fmlColl[random.Next(fmlColl.Count)];

                String text = fml.Groups[2].Value;
                text = Regex.Replace(text, "<.+?>", "");
                text = HttpUtility.HtmlDecode(text);

                message.Chat.SendMessage(String.Format(
                    @"FML #{0} (+{2}/-{3}): {1}",
                    fml.Groups[1].Value,
                    text,
                    fml.Groups[3].Value,
                    fml.Groups[4].Value
                ));

            }
        }
    }
}   