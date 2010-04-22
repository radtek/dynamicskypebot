using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkypeBot.plugins.config.transformation {
    public partial class TransformationConfigForm : Form {
        public TransformationConfigForm() {
            InitializeComponent();

            transCombo.Items.Clear();
            transCombo.Items.AddRange(TransformationPlugin.transformations.ToArray());

            transCombo.SelectedItem = PluginSettings.Default.ActiveTransformation;
        }

        private void transCombo_SelectionChangeCommitted(object sender, EventArgs e) {
            PluginSettings.Default.ActiveTransformation = transCombo.SelectedItem as TransformationPlugin.Transformation;
            PluginSettings.Default.Save();
        }

    }
}
