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
    public class PenisPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override String name() { return "Penis Plugin"; }

        public override String help() { return null; }

        public override String description() { return "Responds to penis queries."; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public PenisPlugin() {
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            MatchCollection outputColl = Regex.Matches(message.Body, 
                "p(e+)nis|c(o+)ck|d(o+)ng|clitoris|d(i+)ck|sc?(h+)l(o+)ng|p(e+)cker|j(o+)hns(o+)n|w(a+)ng|"
              + "b(o+)ner|one eyed m(o+)nster|phal(l+)[uo]s|pr(i+)ck|winki(e+)|r(i+)ch(a+)rd|will(y|i+e)|j(o+)ystick"
              + "lightning r(o+)d|st(i+)ck ?sh(i+)ft",
                RegexOptions.IgnoreCase);
            if (outputColl.Count > 0) {
                String outputString = "";

                foreach (Match output in outputColl) {
                    String midPart = "";
                    foreach (System.Text.RegularExpressions.Group g in output.Groups) {
                        if (g != output.Groups[0])
                            midPart += g.Value;
                    }

                    midPart = Regex.Replace(midPart, ".", "=");

                    outputString += (outputString == "" ? "" : " ") +
                                    "8==" +
                                    midPart +
                                    "D";
                }
                message.Chat.SendMessage(outputString);
            }
        }
    }
}   