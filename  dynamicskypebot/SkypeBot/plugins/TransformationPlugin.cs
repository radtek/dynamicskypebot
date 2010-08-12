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
using log4net;

namespace SkypeBot.plugins {
    public class TransformationPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Random random = new Random();

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
                        new Transformation("Reverse", ReverseTransform),
                        new Transformation("MiXeD CaSe", MixedCaseTransform),
                        new Transformation("Random poop (1/20 chance)", (src) => PoopTransform(src, 20)),
                        new Transformation("Random poop (1/500 chance)", (src) => PoopTransform(src, 500)),
                        new Transformation("Random poop (1/3000 chance)", (src) => PoopTransform(src, 3000)),
                        new Transformation("Random poop (1/10000 chance)", (src) => PoopTransform(src, 10000))
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
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            if (!message.IsEditable) {
                return;
            }

            String newBody = PluginSettings.Default.ActiveTransformation.Transform(message.Body);
            if (newBody != message.Body) // avoid editing if the message doesn't change
                message.Body = newBody;
        }

        private static string ReverseTransform(string source) {
            char[] arr = source.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private static string MixedCaseTransform(string source) {
            char[] arr = source.ToCharArray();
            StringBuilder result = new StringBuilder(arr.Length);
            for (int i = 0; i < arr.Length; i ++) {
                result.Append(i % 2 == 0 ? Char.ToUpperInvariant(arr[i])
                                         : Char.ToLowerInvariant(arr[i]));
            }
            return result.ToString();
        }

        private static string PoopTransform(string source, int probability) {
            StringBuilder result = new StringBuilder(source);
            MatchCollection words = Regex.Matches(source, @"\w+");
            int indexDiff = 0;
            foreach (Match match in words) {
                if (random.Next(probability) == 0) {
                    String word = match.Value;
                    result = result.Replace(word, word == word.ToLowerInvariant() ? "poop" :
                                                  word == word.ToUpperInvariant() ? "POOP" : "Poop", match.Index + indexDiff, match.Length);
                    indexDiff -= match.Length - 4;
                }
            }
            return result.ToString();
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