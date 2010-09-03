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
                        new Transformation("No vowels", (src) => Regex.Replace(src, "[aeiou]", "", RegexOptions.IgnoreCase), "!vowel"),
                        new Transformation("Reverse", ReverseTransform, "!rev"),
                        new Transformation("MiXeD CaSe", MixedCaseTransform, "!mix"),
                        new Transformation("pǝddılɟ", FlipTransform, "!flip"),
                        new Transformation("Z̈̇̿̚a͖̟̝͔ͥ̓͌͛̀ͧͅl͌g͔̦͎̦̝͇̲̒ͭ̉͒o̭̪̫̰̪̗̅̅̊", ZalgoTransform, "!zalgo"),
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

            if (PluginSettings.Default.ActiveTransformationTriggers == null) {
                PluginSettings.Default.ActiveTransformationTriggers = new List<Transformation>();
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
            foreach (Transformation trans in PluginSettings.Default.ActiveTransformationTriggers) {
                Match output = Regex.Match(message.Body, "^" + trans.trigger + " (.+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (output.Success) {
                    log.Debug("Got in with " + trans.name + ".");
                    message.Chat.SendMessage(
                        String.Format("{0}: {1}", trans.name, trans.Transform(output.Groups[1].Value))
                    );
                }
            }
            
            if (PluginSettings.Default.AutomaticTransformations) {
                if (!message.IsEditable) {
                    return;
                }

                String newBody = PluginSettings.Default.ActiveTransformation.Transform(message.Body);
                if (newBody != message.Body) // avoid editing if the message doesn't change
                    message.Body = newBody;
            }
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

        private static string FlipTransform(string source) {
            Dictionary<char, char> flipTable = new Dictionary<char, char>() {
                { 'a', '\u0250' }, { 'b', 'q' }, { 'c', '\u0254' }, { 'd', 'p' }, { 'e', '\u01DD' }, { 'f', '\u025F' }, { 'g', '\u0183' }, { 'h', '\u0265' }, { 'i', '\u0131' }, { 'j', '\u027E' }, { 'k', '\u029E' }, { 'm', '\u026F' }, { 'n', 'u' }, { 'r', '\u0279' }, { 't', '\u0287' }, { 'v', '\u028C' }, { 'w', '\u028D' }, { 'y', '\u028E' }, { 'A', '\u2200' }, { 'C', '\u0186' }, { 'E', '\u018E' }, { 'F', '\u2132' }, { 'G', '\u05E4' }, { 'H', 'H' }, { 'I', 'I' }, { 'J', '\u017F' }, { 'L', '\u02E5' }, { 'M', 'W' }, { 'N', 'N' }, { 'P', '\u0500' }, { 'T', '\u2534' }, { 'U', '\u2229' }, { 'V', '\u039B' }, { 'Y', '\u2144' }, { '1', '\u0196' }, { '2', '\u1105' }, { '3', '\u0190' }, { '4', '\u3123' }, { '5', '\u03DB' }, { '6', '9' }, { '7', '\u3125' }, { '8', '8' }, { '9', '6' }, { '0', '0' }, { '.', '\u02D9' }, { ',', '\'' }, { '\'', ',' }, { '`', ',' }, { '?', '\u00BF' }, { '!', '\u00A1' }, { '[', ']' }, { ']', '[' }, { '(', ')' }, { ')', '(' }, { '{', '}' }, { '}', '{' }, { '<', '>' }, { '>', '<' }, { '&', '\u214B' }, { '_', '\u203E' }, { '\u2234', '\u2235' }, { '\u2045', '\u2046' }
            };
            Dictionary<char, char> flipTableMore = new Dictionary<char, char>();
            foreach (KeyValuePair<char, char> kvp in flipTable) {
                if (!flipTableMore.ContainsKey(kvp.Value)) {
                    flipTableMore.Add(kvp.Value, kvp.Key);
                }
            }
            foreach (KeyValuePair<char, char> kvp in flipTableMore) {
                if (!flipTable.ContainsKey(kvp.Key)) {
                    flipTable.Add(kvp.Key, kvp.Value);
                }
            }

            String output = "";
            foreach (char c in source) {
                if (flipTable.ContainsKey(c)) {
                    output = flipTable[c] + output;
                } else {
                    output = c + output;
                }
            }

            return output;
        }

        private static string ZalgoTransform(string source) {
            // Adapted from www.marlborotech.com/Zalgo.html

            List<char> zalgoUp = new List<char>() {
		        '\u030d', '\u030e', '\u0304', '\u0305', 
		        '\u033f', '\u0311', '\u0306', '\u0310', 
		        '\u0352', '\u0357', '\u0351', '\u0307', 
		        '\u0308', '\u030a', '\u0342', '\u0343', 
		        '\u0344', '\u034a', '\u034b', '\u034c', 
		        '\u0303', '\u0302', '\u030c', '\u0350', 
		        '\u0300', '\u0301', '\u030b', '\u030f', 
		        '\u0312', '\u0313', '\u0314', '\u033d', 
		        '\u0309', '\u0363', '\u0364', '\u0365', 
		        '\u0366', '\u0367', '\u0368', '\u0369', 
		        '\u036a', '\u036b', '\u036c', '\u036d', 
		        '\u036e', '\u036f', '\u033e', '\u035b', 
		        '\u0346', '\u031a' 
            };

            List<char> zalgoMid = new List<char>() {
		        '\u0315', '\u031b', '\u0340', '\u0341', 
		        '\u0358', '\u0321', '\u0322', '\u0327', 
		        '\u0328', '\u0334', '\u0335', '\u0336', 
		        '\u034f', '\u035c', '\u035d', '\u035e', 
		        '\u035f', '\u0360', '\u0362', '\u0338', 
		        '\u0337', '\u0361', '\u0489' 
	        };

            List<char> zalgoDown = new List<char>() {
		        '\u0316', '\u0317', '\u0318', '\u0319', 
		        '\u031c', '\u031d', '\u031e', '\u031f', 
		        '\u0320', '\u0324', '\u0325', '\u0326', 
		        '\u0329', '\u032a', '\u032b', '\u032c', 
		        '\u032d', '\u032e', '\u032f', '\u0330', 
		        '\u0331', '\u0332', '\u0333', '\u0339', 
		        '\u033a', '\u033b', '\u033c', '\u0345', 
		        '\u0347', '\u0348', '\u0349', '\u034d', 
		        '\u034e', '\u0353', '\u0354', '\u0355', 
		        '\u0356', '\u0359', '\u035a', '\u0323' 
            };

            String output = "";

            foreach (char c in source) {
                output += c;

                int numUp = random.Next(16) / 2 + 1;
                int numMid = random.Next(6) / 2;
                int numDown = random.Next(16) / 2 + 1;

                for (int i = 0; i < numUp; i++) {
                    output += zalgoUp[random.Next(zalgoUp.Count)];
                }
                for (int i = 0; i < numMid; i++) {
                    output += zalgoMid[random.Next(zalgoMid.Count)];
                }
                for (int i = 0; i < numDown; i++) {
                    output += zalgoDown[random.Next(zalgoDown.Count)];
                }
            }

            return output;
        }

        [Serializable]
        public class Transformation {
            public delegate string TransformFunction(string source);

            public string name;
            private event TransformFunction transform;
            public string trigger;

            public Transformation(string name, TransformFunction transform, string trigger) {
                this.name = name;
                this.transform = transform;
                this.trigger = trigger;
            }

            public Transformation(string name, TransformFunction transform) : this(name, transform, null) { }

            public String Transform(string source) {
                if (transform == null) {
                    return source;
                }

                return transform(source);
            }

            public bool HasTrigger {
                get {
                    return this.trigger != null;
                }
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