﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int NextQuoteID {
            get {
                return ((int)(this["NextQuoteID"]));
            }
            set {
                this["NextQuoteID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int YoutubeIterations {
            get {
                return ((int)(this["YoutubeIterations"]));
            }
            set {
                this["YoutubeIterations"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int YoutubeCacheSize {
            get {
                return ((int)(this["YoutubeCacheSize"]));
            }
            set {
                this["YoutubeCacheSize"] = value;
            }
        }
    }
}
