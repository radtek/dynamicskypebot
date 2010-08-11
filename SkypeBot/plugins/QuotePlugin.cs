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
using SkypeBot.plugins.config.quote;
using System.Data;
using System.ComponentModel;
using log4net;

namespace SkypeBot.plugins {
    public class QuotePlugin : Plugin {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Random rand;

        public String name() { return "Quote Plugin"; }

        public String help() { return "!quote [number], !addquote <quote>, !listquotes"; }

        public String description() { return "Stores quotes."; }

        public bool canConfig() { return true; }
        public void openConfig() {
            QuoteConfigForm qcf = new QuoteConfigForm();
            qcf.Visible = true;
        }

        public QuotePlugin() {
            rand = new Random();
            if (PluginSettings.Default.Quotes == null)
                PluginSettings.Default.Quotes = new ArrayList();
            if (PluginSettings.Default.UnapprovedQuotes == null)
                PluginSettings.Default.UnapprovedQuotes = new ArrayList();
        }

        public void load() {
            log.Info("Plugin successfully loaded.");
        }

        public void unload() {
            log.Info("Plugin successfully unloaded.");
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!(addquote|quote|listquotes) ?(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                String queryString = output.Groups[1].Value.ToLower();
                log.Debug("It's me time!");

                switch (queryString) {
                    case "addquote":
                        String quoteText = output.Groups[2].Value;

                        Quote quote = new Quote();
                        quote.Submitted = DateTime.Now;
                        quote.Submitter = message.FromHandle;
                        quote.QuoteText = quoteText;

                        PluginSettings.Default.UnapprovedQuotes.Add(quote);
                        PluginSettings.Default.Save();


                        message.Chat.SendMessage(@"Quote has been submitted for approval.");
                        break;
                    case "quote":
                        int quoteNo;
                        try {
                            quoteNo = int.Parse(output.Groups[2].Value);
                        }
                        catch (Exception) {
                            quoteNo = 0;
                        }

                        var row = from Quote qt in PluginSettings.Default.Quotes
                                  where qt.Id == quoteNo
                                  select qt;

                        Quote q;

                        if (row.Count<Quote>() == 0) {
                            if (output.Groups[2].Value.Trim().Equals("")) {
                                q = PluginSettings.Default.Quotes[rand.Next(PluginSettings.Default.Quotes.Count)] as Quote;
                            }
                            else {
                                message.Chat.SendMessage(
                                    "That quote simply doesn't exist."
                                );
                                return;
                            }
                        } else {
                            q = row.First<Quote>();
                        }

                        message.Chat.SendMessage(
                            String.Format(
                                "Quote #{0} (added on {1} by {2}):\n\n{3}",
                                q.Id,
                                q.Submitted.ToShortDateString(),
                                q.Submitter,
                                q.QuoteText
                            )
                        );

                        break;
                    case "listquotes":
                        var quotes = from Quote qt in PluginSettings.Default.Quotes
                                     orderby qt.Id
                                     select qt.Id;

                        String quoteStr = "";
                        foreach (int id in quotes) {
                            if (quoteStr != "")
                                quoteStr += ", ";
                            quoteStr += "#" + id;
                        }

                        message.Chat.SendMessage(
                            "Available quotes: "+quoteStr
                        );
                        break;
                }
            }
        }

        [Serializable]
        public class Quote {
            private int id;
            private String submitter;
            private DateTime submitted;
            private String quote;

            public int Id {
                get { return id; }
                set { id = value; }
            }

            public String Submitter {
                get { return submitter; }
                set { submitter = value; }
            }

            public DateTime Submitted {
                get { return submitted; }
                set { submitted = value; }
            }

            public String QuoteText {
                get { return quote; }
                set { quote = value; }
            }
        }
    }
}