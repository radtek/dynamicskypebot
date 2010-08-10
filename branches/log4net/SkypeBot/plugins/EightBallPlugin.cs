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
using SkypeBot.plugins.config.eightball;

namespace SkypeBot.plugins {
    public class EightBallPlugin : Plugin {
        public event MessageDelegate onMessage;

        private Random random;

        public String name() { return "8-ball plugin"; }

        public String help() { return "!8ball"; }

        public String description() { return "Gives ambiguous answers to your queries."; }

        public bool canConfig() { return true; }
        public void openConfig() {
            EightballConfigForm ecf = new EightballConfigForm();
            ecf.Visible = true;
        }
        // TODO: Make a configuration thing.

        public EightBallPlugin() {
            if (PluginSettings.Default.EightBallReplies == null)
                InitializeReplies();

            random = new Random();
        }

        public static void InitializeReplies() {
            List<string> ebr = new List<string>(
                new string[] {
                    @"Outlook Good",
                    @"Cannot Predict Now",
                    @"Very Doubtful",
                    @"Outlook Not So Good",
                    @"Yes",
                    @"Signs Point To Yes",
                    @"My Reply Is No",
                    @"Better Not Tell You Now",
                    @"My Sources Say No",
                    @"Don't Count On It",
                    @"Yes Definitely",
                    @"Without A Doubt",
                    @"You May Rely On It",
                    @"It Is Decidedly So",
                    @"As I See It, Yes",
                    @"Ask Again Later",
                    @"It Is Certain",
                    @"Reply Hazy, Try Again",
                    @"Most Likely",
                    @"Concentrate And Ask Again"
                }
            );

            PluginSettings.Default.EightBallReplies = ebr;
            PluginSettings.Default.Save();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!8ball", RegexOptions.IgnoreCase);
            if (output.Success) {
                String reply = PluginSettings.Default.EightBallReplies[random.Next(PluginSettings.Default.EightBallReplies.Count)];

                message.Chat.SendMessage(String.Format(
                    @"The Magic 8-ball replies: {0}",
                    reply
                ));
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}   