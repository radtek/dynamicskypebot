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
    public class DicePlugin : Plugin {
        private Random random;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override String name() { return "Dice Plugin"; }

        public override String help() { return "!roll <n>d<m>[<+/-><mod>]"; }

        public override String description() { return "Allows people to roll dice"; }

        public override bool canConfig() { return false; }
        public override void openConfig() { }

        public DicePlugin() {
            random = new Random();
        }

        public override void load() {
            log.Info("Plugin successfully loaded.");
        }

        public override void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public override void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!roll (\d+)d(\d+)(?:([+-])(\d+))?", RegexOptions.IgnoreCase);
            if (output.Success) {
                int num = Math.Max(1, Math.Min(200, Convert.ToInt32(output.Groups[1].Value)));
                int size = Math.Max(1, Math.Min(1000000, Convert.ToInt32(output.Groups[2].Value)));

                String pm = "+";
                int mod = 0;
                if (output.Groups[3].Length > 0) {
                    pm = output.Groups[3].Value;
                    mod = Math.Max(0, Math.Min(1000000, Convert.ToInt32(output.Groups[4].Value)));
                }


                int[] vals = new int[num];
                for (int i = 0; i < num; i++)
                    vals[i] = random.Next(size)+1;

                message.Chat.SendMessage(String.Format(
                    @"{0}d{1}{4} rolled; result: {2} ({3})", 
                    num, 
                    size, 
                    vals.Sum() + (pm == "+" ? 1 : -1) * mod,
                    String.Join(", ", Array.ConvertAll<int, String>(vals, new Converter<int, string>(Convert.ToString))),
                    mod > 0 ? pm + mod : ""
                ));
            }
        }
    }
}