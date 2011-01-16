using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace SkypeBot {
    public partial class ReportForm : Form {
        public ReportForm(IEnumerable<string> pluginNames, String message) : this(pluginNames) {
            pluginBox.SelectedItem = "Not Applicable";
            problemText.Text = "A crash occured.";
        }

        public ReportForm(IEnumerable<string> pluginNames) {
            InitializeComponent();

            pluginBox.Items.Clear();
            pluginBox.Items.Add("Not Applicable");
            pluginBox.Items.AddRange(pluginNames.ToArray<String>());
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void sendButton_Click(object sender, EventArgs e) {
            if (pluginBox.SelectedIndex == -1) {
                MessageBox.Show("Please select the problematic plugin.\n(Or \"Not Applicable\" if it doesn't concern a plugin.)");
                return;
            }

            if (problemText.Text.Length == 0) {
                MessageBox.Show("Please write what the problem is.");
                return;
            }

            String version;
            try {
                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            } catch (System.Deployment.Application.InvalidDeploymentException) {
                version = "";
            }

            String report = String.Format(@"SkypeBot bug report
-----------------------------
Filed by: {0}
Version: {1}
Plugin: {2}
-----------------------------
Problem description:

{3}
-----------------------------",
                              nameBox.Text,
                              version,
                              pluginBox.SelectedItem,
                              problemText.Text);

            if (attachLog.Checked) {
                report += "\nLog contents:\n\n";
                String logFile = Path.Combine(Directory.GetCurrentDirectory(), "debug.log");
                // The logfile is locked by the logger, so we have to open it read-only
                FileStream fs = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader reader = new StreamReader(fs);
                report += reader.ReadToEnd();
            }

            WebClient wc = new WebClient();
            wc.UploadStringCompleted += (snd, ev) => MessageBox.Show("Your report has been sent. Thank you.");
            wc.UploadStringAsync(new Uri("http://mathemaniac.org/apps/skypebot/errorlogger.php"), "POST", report);
            Close();
        }

    }
}
