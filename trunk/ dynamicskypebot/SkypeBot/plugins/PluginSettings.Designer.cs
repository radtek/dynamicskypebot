﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Configuration;
namespace SkypeBot.plugins {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class PluginSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static PluginSettings defaultInstance = ((PluginSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new PluginSettings())));
        
        public static PluginSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public global::System.Collections.Hashtable HighlowScores {
            get {
                return ((global::System.Collections.Hashtable)(this["HighlowScores"]));
            }
            set {
                this["HighlowScores"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public global::System.Collections.ArrayList Quotes {
            get {
                return ((global::System.Collections.ArrayList)(this["Quotes"]));
            }
            set {
                this["Quotes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public global::System.Collections.ArrayList UnapprovedQuotes {
            get {
                return ((global::System.Collections.ArrayList)(this["UnapprovedQuotes"]));
            }
            set {
                this["UnapprovedQuotes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int NextQuoteID {
            get {
                return ((int)(this["NextQuoteID"]));
            }
            set {
                this["NextQuoteID"] = value;
            }
        }
    }
}
