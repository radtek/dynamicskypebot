using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkypeBot {
    public partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();

            loadCheckBoxes(updateCheckToggle, helpOnMinimized);
            loadNumericUpDowns(updateInterval);
        }

        private void loadCheckBoxes(params CheckBox[] checkBoxes) {
            foreach (CheckBox checkbox in checkBoxes) {
                checkbox.Checked = (bool)Properties.Settings.Default[checkbox.Tag as String];
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
    }
}
