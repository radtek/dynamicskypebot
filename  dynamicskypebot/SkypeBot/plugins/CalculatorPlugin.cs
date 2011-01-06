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
using SkypeBot.plugins.calc;
using bsn.GoldParser;
using bsn.GoldParser.Grammar;
using bsn.GoldParser.Semantic;
using System.Globalization;
using bsn.GoldParser.Parser;

namespace SkypeBot.plugins {
    public class CalculatorPlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private CompiledGrammar grammar;
        SemanticTypeActions<MathToken> actions;

        public String name() { return "Calculator Plugin"; }

        public String help() { return "!calc <exp>"; }

        public String description() { return "Evaluates an expression"; }

        public bool canConfig() { return false; }
        public void openConfig() { }

        public CalculatorPlugin() {
        }

        public void load() {
            log.Debug("Loading grammar...");
            grammar = CompiledGrammar.Load(typeof(MathToken), "math-grammar.cgt");
            log.Debug("Loading actions...");
            actions = new SemanticTypeActions<MathToken>(grammar);
            try {
                actions.Initialize(true);
            } catch (InvalidOperationException e) {
                log.Error("Error initializing actions", e);
                return;
            }

            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!(?:calc|eval) (.+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                String exp = output.Groups[1].Value;
                SemanticProcessor<MathToken> processor = new SemanticProcessor<MathToken>(new StringReader(exp), actions);
                ParseMessage parseMessage = processor.ParseAll();
                if (parseMessage == ParseMessage.Accept) {
                    message.Chat.SendMessage(
                        String.Format(
                            "{0} = {1}",
                            exp,
                            ((Computable)processor.CurrentToken).GetValue()));
                } else {
                    IToken token = processor.CurrentToken;
                    message.Chat.SendMessage(string.Format("{0} ({1} on line {2}, column {3})",
                                                           parseMessage, token.Symbol,
                                                           token.Position.Line, token.Position.Column));
                }
            }
        }

    }
}   