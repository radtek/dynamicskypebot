using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using SKYPE4COMLib;

namespace SkypeBot {
    public partial class SettingsForm : Form {
        private Skype skype;

        public SettingsForm(Skype skype) {
            InitializeComponent();

            this.skype = skype;
            loadCheckBoxes(updateCheckToggle, helpOnMinimized);
            loadNumericUpDowns(updateInterval);
            loadListBoxes(whiteList);
            loadDomainMode();
        }

        private void loadDomainMode() {
            domainMode.SelectedIndex = Properties.Settings.Default.UseWhitelist ? 1 : 0;
        }

        private void loadCheckBoxes(params CheckBox[] checkBoxes) {
            foreach (CheckBox checkbox in checkBoxes) {
                checkbox.Checked = (bool)Properties.Settings.Default[checkbox.Tag as String];
            }
        }

        private void loadListBoxes(params ListBox[] listBoxes) {
            foreach (ListBox listbox in listBoxes) {
                listbox.Items.Clear();
                foreach (String line in (StringCollection)Properties.Settings.Default[listbox.Tag as String]) {
                    listbox.Items.Add(line);
                }
            }
        }

        private void loadNumericUpDowns(params NumericUpDown[] numericUpDowns) {
            foreach (NumericUpDown numericUpDown in numericUpDowns) {
                numericUpDown.Value = (int)Properties.Settings.Default[numericUpDown.Tag as String];
            }
        }

        private void checkbox_CheckedChanged(object sender, EventArgs e) {
            CheckBox checkbox = sender as CheckBox;
            Properties.Settings.Default[checkbox.Tag as String] = (bool)checkbox.Checked;
            Properties.Settings.Default.Save();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e) {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            Properties.Settings.Default[numericUpDown.Tag as String] = (int)numericUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void domainMode_SelectedIndexChanged(object sender, EventArgs e) {
            Properties.Settings.Default.UseWhitelist = domainMode.SelectedIndex == 1;
            Properties.Settings.Default.Save();
        }

        private void addToWhitelist_Click(object sender, EventArgs e) {
            SkypeChatPicker scp = new SkypeChatPicker(skype);
            DialogResult result = scp.ShowDialog();
            if (result == DialogResult.OK) {
                Properties.Settings.Default.Whitelist.Add(scp.selectedChat);
                Properties.Settings.Default.Save();
                loadListBoxes(whiteList);
            }
            scp.Close();
        }

        private void deleteFromWhitelist_Click(object sender, EventArgs e) {
            if (whiteList.SelectedIndex > -1) {
                Properties.Settings.Default.Whitelist.RemoveAt(whiteList.SelectedIndex);
                Properties.Settings.Default.Save();
                loadListBoxes(whiteList);
            }
        }

        private void clearWhitelist_Click(object sender, EventArgs e) {
            Properties.Settings.Default.Whitelist.Clear();
            Properties.Settings.Default.Save();
            loadListBoxes(whiteList);
        }
    }
}
