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
using SkypeBot.plugins.config.wordfilter;

namespace SkypeBot.plugins {
    public class WordFilterPlugin : Plugin {
        public event MessageDelegate onMessage;

        public String name() { return "Word Filter Plugin"; }

        public String help() { return null; }

        public String description() { return "Filters chat based on filters you set."; }

        public bool canConfig() { return true; }
        public void openConfig() {
            WordFilterConfigForm wfcf = new WordFilterConfigForm();
            wfcf.Visible = true;
        }

        public WordFilterPlugin() {
            if (PluginSettings.Default.WordFilters == null)
                InitializeFilters();
        }

        private static void InitializeFilters() {
            PluginSettings.Default.WordFilters = new List<Filter>();

            PluginSettings.Default.Save();
        }

        public static List<Filter> SampleFilters {
            get {
                return new List<Filter>(
                    new Filter[] {
                        new Filter("fuck -> gently caress", @"\bfuck\b", "gently caress", false),
                        new Filter("are -> am", @"\bare\b", "am", false),
                        new Filter("Phone number", @"\b(\d{3})(\d{3})(\d{4})\b", "($1) $2-$3", false),
                        new Filter("penis -> jesus", @"\bp(e+)nis\b", "j$1sus", false),
                        new Filter("lol -> lots of love", @"\bl(o+)l\b", "lots of l$1ve", false),
                    }
                );
            }
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            if (!message.IsEditable) {
                return;
            }

            String messageText = message.Body;

            foreach (Filter filter in PluginSettings.Default.WordFilters) {
                try {
                    if (filter.caseSensitive) {
                        messageText = Regex.Replace(messageText, filter.regex, filter.replacement);
                    } else {
                        messageText = Regex.Replace(messageText, filter.regex, filter.replacement, RegexOptions.IgnoreCase);
                    }
                } catch {
                    logMessage("Error in filter: " + filter, true);
                }
            }

            if (messageText != message.Body) {
                message.Body = messageText;
            }
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }

        [Serializable]
        public class Filter {
            public String name = "";
            public String regex = "";
            public String replacement = "";
            public Boolean caseSensitive = false;

            public Filter(String name, String regex, String replacement, Boolean caseSensitive) {
                this.name = name;
                this.regex = regex;
                this.replacement = replacement;
                this.caseSensitive = false;
            }

            public override string ToString() {
                return name + " [regex: " + regex + ", replacement: " + replacement + ", case-sensitive: " + caseSensitive + "]";
            }
        }
    }
}   