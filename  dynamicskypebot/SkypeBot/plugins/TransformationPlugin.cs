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
using SkypeBot.plugins.config.transformation;

namespace SkypeBot.plugins {
    public class TransformationPlugin : Plugin {
        public event MessageDelegate onMessage;

        public String name() { return "Transformation Plugin"; }

        public String help() { return null; }

        public String description() { return "Transforms all messages received in the specified way."; }

        public bool canConfig() { return true; }
        public void openConfig() {
            TransformationConfigForm tcf = new TransformationConfigForm();
            tcf.Visible = true;
        }

        public static List<Transformation> transformations {
            get {
                return new List<Transformation>(
                    new Transformation[] {
                        new Transformation("ALLCAPS", (src) => src.ToUpperInvariant()),
                        new Transformation("lowercase", (src) => src.ToLowerInvariant()),
                        new Transformation("No vowels", (src) => Regex.Replace(src, "[aeiou]", "", RegexOptions.IgnoreCase)),
                        new Transformation("Reverse", ReverseTransform)
                    }
                );
            }
        }
             
        public TransformationPlugin() {
            if (PluginSettings.Default.ActiveTransformation == null) {
                PluginSettings.Default.ActiveTransformation = transformations.First();
                PluginSettings.Default.Save();
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

            message.Body = PluginSettings.Default.ActiveTransformation.Transform(message.Body);
        }

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }

        private static string ReverseTransform(string source) {
            char[] arr = source.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        [Serializable]
        public class Transformation {
            public delegate string TransformFunction(string source);

            public string name;
            private event TransformFunction transform;

            public Transformation(string name, TransformFunction transform) {
                this.name = name;
                this.transform = transform;
            }

            public String Transform(string source) {
                if (transform == null) {
                    return source;
                }

                return transform(source);
            }

            public override string ToString() {
                return String.Format(@"{0} (Example Text -> {1})", name, Transform("Example Text"));
            }

            public override int GetHashCode() {
                return name.GetHashCode();
            }

            public override bool Equals(object obj) {
                if (obj == null || !(obj is Transformation)) return false;
                return name.Equals((obj as Transformation).name);
            }
        }
    }
}   