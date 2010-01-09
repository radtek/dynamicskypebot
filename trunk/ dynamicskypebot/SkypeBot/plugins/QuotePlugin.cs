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
using System.Data.SqlServerCe;

namespace SkypeBot.plugins {
    public class QuotePlugin : Plugin {
        public event MessageDelegate onMessage;

        private SkypeBotDB.quotesDataTable table;
        private SkypeBotDBTableAdapters.quotesTableAdapter tableAdapter;
        private Random rand;

        public String name() { return "Quote Plugin"; }

        public String help() { return "!quote [number], !addquote <quote>, !listquotes"; }

        public String description() { return "Stores quotes."; }

        public bool canConfig() { return true; }
        public void openConfig() {
            QuoteConfigForm qcf = new QuoteConfigForm(table);
            qcf.Visible = true;
        }

        public QuotePlugin() {
            tableAdapter = new SkypeBot.SkypeBotDBTableAdapters.quotesTableAdapter();
            SkypeBotDB db = new SkypeBotDB();
            db.DataSetName = "SkypeBotDB";
            tableAdapter.Fill(db.quotes);
            table = db.quotes;
            rand = new Random();
        }

        public void load() {
            logMessage("Plugin successfully loaded.", false);
        }

        public void unload() {
            logMessage("Plugin successfully unloaded.", false);
        }

        public void Skype_MessageStatus(IChatMessage message, TChatMessageStatus status) {
            Match output = Regex.Match(message.Body, @"^!(addquote|quote|listquotes) ?(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (output.Success) {
                SkypeBotDB.quotesRow row;

                String queryString = output.Groups[1].Value.ToLower();
                logMessage("It's me time!", false);

                tableAdapter.Fill(table);

                switch (queryString) {
                    case "addquote":
                        String quote = output.Groups[2].Value;

                        try {
                            tableAdapter.Insert(message.FromHandle, DateTime.Now, quote);
                            SqlCeCommand getLatestRow = tableAdapter.Connection.CreateCommand();
                            getLatestRow.CommandText = "SELECT MAX(id) FROM quotes";
                            getLatestRow.Connection.Open();
                            message.Chat.SendMessage(String.Format(@"Quote #{0} successfully added.", getLatestRow.ExecuteScalar()));
                            getLatestRow.Connection.Close();
                        }
                        catch (Exception ex) {
                            logMessage(ex.Message, true);
                            message.Chat.SendMessage(@"Error adding quote.");
                        }
                        break;
                    case "quote":
                        int quoteNo;
                        try {
                            quoteNo = int.Parse(output.Groups[2].Value);
                        }
                        catch (Exception) {
                            quoteNo = 0;
                        }

                        row = table.FindByid(quoteNo);

                        String outMsg;

                        if (row == null) {
                            if (output.Groups[2].Value.Trim().Equals("")) {
                                DataRow rowrow = table.Rows[rand.Next(table.Rows.Count)];
                                outMsg = String.Format(
                                    "Quote #{0} (added on {1} by {2}):\n\n{3}",
                                    rowrow["id"],
                                    ((DateTime)rowrow["time"]).ToShortDateString(),
                                    rowrow["name"],
                                    rowrow["quote"]
                                );
                            }
                            else {
                                outMsg = "That quote simply doesn't exist.";
                            }
                        } else {
                            outMsg = String.Format(
                                "Quote #{0} (added on {1} by {2}):\n\n{3}",
                                row.id,
                                row.time.ToShortDateString(),
                                row.name,
                                row.quote
                            );
                        }

                        message.Chat.SendMessage(
                            outMsg
                        );

                        break;
                    case "listquotes":
                        var quotes = from q in table
                                     orderby q.id
                                     select q.id;

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

        private void logMessage(String msg, Boolean isError) {
            if (onMessage != null)
                onMessage(this.name(), msg, isError);
        }
    }
}