using System.Configuration;

/**
 * Certain settings need to be serialized in binary form.
 * The settings designer can't cope with that, so I do this here.
 */

namespace SkypeBot.plugins {
    internal sealed partial class PluginSettings : ApplicationSettingsBase {
        [UserScopedSettingAttribute()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public global::System.Collections.Hashtable HighlowScores {
            get {
                return ((global::System.Collections.Hashtable)(this["HighlowScores"]));
            }
            set {
                this["HighlowScores"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public global::System.Collections.ArrayList Quotes {
            get {
                return ((global::System.Collections.ArrayList)(this["Quotes"]));
            }
            set {
                this["Quotes"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public global::System.Collections.ArrayList UnapprovedQuotes {
            get {
                return ((global::System.Collections.ArrayList)(this["UnapprovedQuotes"]));
            }
            set {
                this["UnapprovedQuotes"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public global::System.Collections.Generic.List<string> EightBallReplies {
            get {
                return ((global::System.Collections.Generic.List<string>)(this["EightBallReplies"]));
            }
            set {
                this["EightBallReplies"] = value;
            }
        }

        [UserScopedSettingAttribute()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public global::System.Collections.Generic.List<WordFilterPlugin.Filter> WordFilters {
            get {
                return ((global::System.Collections.Generic.List<WordFilterPlugin.Filter>)(this["WordFilters"]));
            }
            set {
                this["WordFilters"] = value;
            }
        }
    }
}
