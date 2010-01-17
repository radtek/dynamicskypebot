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

namespace SkypeBot.plugins.config.quotemk2 {
    public partial class QuoteConfigForm : Form {
        private Boolean inited;

        public QuoteConfigForm() {
            InitializeComponent();

            this.approvedBindingSource.DataSource = PluginSettings.Default.Quotes.Cast<QuotePluginMk2.Quote>();
            this.pendingBindingSource.DataSource = PluginSettings.Default.UnapprovedQuotes.Cast<QuotePluginMk2.Quote>();

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
            QuotePluginMk2.Quote quote = pendingBindingSource.Current as QuotePluginMk2.Quote;
            quote.Id = PluginSettings.Default.NextQuoteID ++;
            approvedBindingSource.Add(quote);
            pendingBindingSource.RemoveCurrent();
        }
    }
}
