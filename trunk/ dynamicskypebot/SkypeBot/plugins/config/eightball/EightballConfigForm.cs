using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkypeBot.plugins.config.eightball {
    public partial class EightballConfigForm : Form {
        public EightballConfigForm() {
            InitializeComponent();
            loadList();
        }

        private void addBtn_Click(object sender, EventArgs e) {
            responseList.Items.Add(addBox.Text);
            addBox.Clear();

            saveList();
        }

        private void saveList() {
            List<String> responses = new List<String>();

            foreach (String s in responseList.Items.Cast<String>()) {
                responses.Add(s);
            }

            PluginSettings.Default.EightBallReplies = responses;
            PluginSettings.Default.Save();
        }

        private void loadList() {
            responseList.Items.Clear();
            foreach (String s in PluginSettings.Default.EightBallReplies) {
                responseList.Items.Add(s);
            }
        }

        private void resetList() {
            EightBallPlugin.InitializeReplies();
            loadList();
        }

        private void resetBtn_Click(object sender, EventArgs e) {
            resetList();
        }

        private void delBtn_Click(object sender, EventArgs e) {
            foreach (String s in responseList.SelectedItems.Cast<String>()) {
                PluginSettings.Default.EightBallReplies.Remove(s);
            }
            PluginSettings.Default.Save();
            loadList();
        }
    }
}
