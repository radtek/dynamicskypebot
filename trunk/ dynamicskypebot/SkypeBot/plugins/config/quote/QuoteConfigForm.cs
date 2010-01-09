using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkypeBot.plugins.config.quote {
    public partial class QuoteConfigForm : Form {
        private SkypeBotDB.quotesDataTable quotes;

        public QuoteConfigForm(SkypeBotDB.quotesDataTable quotes) {
            InitializeComponent();
            this.quotes = quotes;
            quotesTableAdapter.Fill(quotes);
            quotesBindingSource.DataSource = quotes;
            quotesBindingSource.MoveLast();
            
        }

        private void deleteBtn_Click(object sender, EventArgs e) {
            quotesBindingSource.RemoveCurrent();
        }

        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e) {
            try {
                Validate();
                quotesBindingSource.EndEdit();
                quotesTableAdapter.Update(quotes);
                MessageBox.Show("Saved!");
            }
            catch (System.Exception ex) {
                MessageBox.Show("Unable to save changes:\n"+ex.Message);
            }

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e) {
            bindingNavigatorPositionItem.Text = quotesBindingSource.Count.ToString();
            dateTimePicker1.Value = DateTime.Now;
        }

    }
}
