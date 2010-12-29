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
using System.Globalization;

namespace SkypeBot.plugins {
    public class DicePlugin : Plugin {
        private Random random;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public String name() { return "Dice Plugin"; }

        public String help() { return "![high]roll <n>d<m>[<+/-><mod>]"; }

        public String description() { return "Allows people to roll dice"; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public DicePlugin() {
            random = new Random();
        }

        public void load() {
            if (PluginSettings.Default.RollScores == null) {
                log.Debug("Initializing RollScores.");
                PluginSettings.Default.RollScores = new Dictionary<string, Roll>();
            }
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!(high|)roll (\d+)d(\d+)(([+-])(\d+))?", RegexOptions.IgnoreCase);
            if (output.Success) {
                bool high = output.Groups[1].Value.Length > 0;

                int num = Math.Max(1, Math.Min(200, Convert.ToInt32(output.Groups[2].Value)));
                int size = Math.Max(1, Math.Min(1000000, Convert.ToInt32(output.Groups[3].Value)));

                String pm = "+";
                int mod = 0;
                if (output.Groups[4].Length > 0) {
                    pm = output.Groups[5].Value;
                    mod = Math.Max(0, Math.Min(1000000, Convert.ToInt32(output.Groups[6].Value)));
                }
                
                string roll = String.Format("{0}d{1}{2}", num, size, mod > 0 ? pm + mod : "");

                if (high) {
                    if (!PluginSettings.Default.RollScores.ContainsKey(roll)) {
                        log.Debug(String.Format("Miss for {0}.", roll));
                        message.Chat.SendMessage(String.Format("{0} has never been rolled in this chat.", roll));        
                    } else {
                        log.Debug(String.Format("Hit for {0}.", roll));
                        Roll r =  PluginSettings.Default.RollScores[roll];
                        message.Chat.SendMessage(String.Format(
                            @"{0} rolled {1} on a {2} on {3}.",
                            r.Username,
                            r.Value,
                            roll,
                            r.Time.ToString("MMMM dd, yyyy", new CultureInfo("en-US"))
                        ));
                    }
                } else {
                    int[] vals = new int[num];
                    for (int i = 0; i < num; i++)
                        vals[i] = random.Next(size) + 1;

                    int value = vals.Sum() + (pm == "+" ? 1 : -1) * mod;
                    bool highscore = false;
                    if (!PluginSettings.Default.RollScores.ContainsKey(roll) ||
                         PluginSettings.Default.RollScores[roll].Value < value) {
                        log.Info(String.Format("New high-roll for {0} on chat {1} by {3}: {2}",
                                 roll, message.Chat.Name, value, message.Sender.Handle));
                        Roll r = new Roll(value, message.Sender.Handle, message.Chat.Name, message.Timestamp);
                        PluginSettings.Default.RollScores[roll] = r;
                        PluginSettings.Default.Save();
                        highscore = true;
                    }


                    message.Chat.SendMessage(String.Format(
                        @"{0} rolled; result: {1} ({2}){3}",
                        roll,
                        value,
                        String.Join(", ", Array.ConvertAll<int, String>(vals, new Converter<int, string>(Convert.ToString))),
                        highscore ? " - new high!" : ""
                    ));
                }
            }
        }

        [Serializable]
        public class Roll {
            public int Value { get; set; }
            public string Username { get; set; }
            public string Chat { get; set; }
            public DateTime Time { get; set; }

            public Roll(int value, string username, string chat, DateTime time) {
                Value = value;
                Username = username;
                Chat = chat;
                Time = time;
            }
        }
    }
}