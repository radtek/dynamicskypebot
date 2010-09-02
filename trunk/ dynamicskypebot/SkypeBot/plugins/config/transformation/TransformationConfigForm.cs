using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace SkypeBot.plugins.config.transformation {
    public partial class TransformationConfigForm : Form {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TransformationConfigForm() {
            InitializeComponent();

            transCombo.Items.Clear();
            triggerList.Items.Clear();
            foreach (TransformationPlugin.Transformation trans in TransformationPlugin.transformations) {
                transCombo.Items.Add(trans);
                if (trans.HasTrigger) {
                    triggerList.Items.Add(trans, PluginSettings.Default.ActiveTransformationTriggers.Contains(trans));
                }
            }

            transCombo.SelectedItem = PluginSettings.Default.ActiveTransformation;

            autoTransform.Checked = PluginSettings.Default.AutomaticTransformations;

            // Set ItemCheck listener after triggerList is populated,
            // to avoid populating it triggering the event.
            triggerList.ItemCheck += new ItemCheckEventHandler(triggerList_ItemCheck);
        }

        private void transCombo_SelectionChangeCommitted(object sender, EventArgs e) {
            PluginSettings.Default.ActiveTransformation = transCombo.SelectedItem as TransformationPlugin.Transformation;
            PluginSettings.Default.Save();
        }

        private void autoTransform_CheckedChanged(object sender, EventArgs e) {
            PluginSettings.Default.AutomaticTransformations = autoTransform.Checked;
            PluginSettings.Default.Save();
        }

        private void triggerList_ItemCheck(object sender, ItemCheckEventArgs e) {
            PluginSettings.Default.ActiveTransformationTriggers.Clear();
            PluginSettings.Default.ActiveTransformationTriggers.AddRange(triggerList.CheckedItems.Cast<TransformationPlugin.Transformation>());
            if (e.NewValue == CheckState.Unchecked) {
                PluginSettings.Default.ActiveTransformationTriggers.Remove(triggerList.Items[e.Index] as TransformationPlugin.Transformation);
            } else {
                PluginSettings.Default.ActiveTransformationTriggers.Add(triggerList.Items[e.Index] as TransformationPlugin.Transformation);
            }
            PluginSettings.Default.Save();
        }

    }
}
