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
using log4net;

namespace SkypeBot.plugins {
    public class WordFilterPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                        new Filter("fuck -> gently caress", @"\bfuck\b", "gently caress", false, 0, false),
                        new Filter("are -> am", @"\bare\b", "am", false, 0, false),
                        new Filter("Phone number", @"\b(\d{3})(\d{3})(\d{4})\b", "($1) $2-$3", false, 0, false),
                        new Filter("penis -> jesus", @"\bp(e+)nis\b", "j$1sus", false, 0, false),
                        new Filter("lol -> lots of love", @"\bl(o+)l\b", "lots of l$1ve", false, 0, false),
                    }
                );
            }
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            if (!message.IsEditable) {
                return;
            }

            String messageText = message.Body;

            foreach (Filter filter in PluginSettings.Default.WordFilters) {
                if (filter.disabled) continue;
                try {
                    if (filter.caseSensitive) {
                        messageText = Regex.Replace(messageText, filter.regex, filter.replacement);
                    } else {
                        messageText = Regex.Replace(messageText, filter.regex, filter.replacement, RegexOptions.IgnoreCase);
                    }
                } catch {
                    log.Error("Error in filter: " + filter);
                }
            }

            if (messageText != message.Body) {
                message.Body = messageText;
            }
        }

        [Serializable]
        public class Filter : IComparable<Filter> {
            public String name = "";
            public String regex = "";
            public String replacement = "";
            public Boolean caseSensitive = false;
            public int priority = 0;
            public Boolean disabled = false;

            public Filter(String name, String regex, String replacement,
                          Boolean caseSensitive, int priority, Boolean disabled) {
                this.name = name;
                this.regex = regex;
                this.replacement = replacement;
                this.caseSensitive = caseSensitive;
                this.priority = priority;
                this.disabled = disabled;
            }

            public override string ToString() {
                return String.Format(
                    "{0} [rx: {1}, rep: {2}, cs: {3}, pri: {4}]",
                    name,
                    regex,
                    replacement,
                    caseSensitive,
                    priority
                );
            }

            #region IComparable<Filter> Members

            public int CompareTo(Filter other) {
                if (other == null) return 1;

                if (priority < other.priority) {
                    return -1;
                } else if (priority > other.priority) {
                    return 1;
                } else {
                    return name.CompareTo(other.name);
                }
            }

            #endregion
        }
    }
}   