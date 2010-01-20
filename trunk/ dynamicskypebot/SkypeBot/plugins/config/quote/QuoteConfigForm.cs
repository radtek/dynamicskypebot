using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SkypeBot.plugins;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace SkypeBot.plugins.config.quote {
    public partial class QuoteConfigForm : Form {
        private Boolean inited;

        public QuoteConfigForm() {
            InitializeComponent();

            this.approvedBindingSource.DataSource = PluginSettings.Default.Quotes.Cast<QuotePlugin.Quote>();
            this.pendingBindingSource.DataSource = PluginSettings.Default.UnapprovedQuotes.Cast<QuotePlugin.Quote>();

            approvedBindingSource.AllowNew = true;
            pendingBindingSource.AllowNew = true;

            inited = true;
        }

        private void approvedBindingSource_ListChanged(object sender, ListChangedEventArgs e) {
            if (inited) {
                PluginSettings.Default.Quotes = new ArrayList(approvedBindingSource.List);
                PluginSettings.Default.UnapprovedQuotes = new ArrayList(pendingBindingSource.List);
                PluginSettings.Default.Save();
            }
        }

        private void approveButton_Click(object sender, EventArgs e) {
            QuotePlugin.Quote quote = pendingBindingSource.Current as QuotePlugin.Quote;
            quote.Id = PluginSettings.Default.NextQuoteID ++;
            approvedBindingSource.Add(quote);
            pendingBindingSource.RemoveCurrent();
        }

        private void pendingSaveBtn_Click(object sender, EventArgs e) {
            exportQuotes(pendingBindingSource.List);
        }

        private void approvedExportBtn_Click(object sender, EventArgs e) {
            exportQuotes(approvedBindingSource.List);
        }

        private void exportQuotes(IList list) {
            saveFileDialog.ShowDialog();

            if (!saveFileDialog.FileName.Equals("")) {
                XmlSerializer xsl = new XmlSerializer(typeof(QuotePlugin.Quote[]));
                Stream stream = saveFileDialog.OpenFile();
                xsl.Serialize(stream, new ArrayList(list).ToArray(typeof(QuotePlugin.Quote)));
                stream.Flush();
                stream.Close();
            }
        }

        private void approvedImportBtn_Click(object sender, EventArgs e) {
            importQuotes(approvedBindingSource.List);
        }

        private void pendingLoadBtn_Click(object sender, EventArgs e) {
            importQuotes(pendingBindingSource.List);
        }

        private void importQuotes(IList list) {
            openFileDialog.ShowDialog();

            if (!openFileDialog.FileName.Equals("")) {
                XmlSerializer xsl = new XmlSerializer(typeof(QuotePlugin.Quote[]));
                Stream stream = openFileDialog.OpenFile();
                list.Clear();
                
                QuotePlugin.Quote[] quotes = xsl.Deserialize(stream) as QuotePlugin.Quote[];
                stream.Close();

                foreach (QuotePlugin.Quote quote in quotes) {
                    quote.QuoteText = quote.QuoteText.Replace("\n", "\r\n");
                    list.Add(quote);
                }
            }
        }
    }
}
