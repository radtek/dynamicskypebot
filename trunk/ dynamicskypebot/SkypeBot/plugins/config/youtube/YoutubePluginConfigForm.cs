using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkypeBot.plugins.config.youtube {
    public partial class YoutubePluginConfigForm : Form {
        public YoutubePluginConfigForm() {
            InitializeComponent();
        }

        private void YoutubePluginConfigForm_Load(object sender, EventArgs e) {
            this.iterationBox.Value = PluginSettings.Default.YoutubeIterations;
            this.cacheSizeBox.Value = PluginSettings.Default.YoutubeCacheSize;
        }

        private void iterationBox_ValueChanged(object sender, EventArgs e) {
            PluginSettings.Default.YoutubeIterations = (int)this.iterationBox.Value;
            PluginSettings.Default.Save();
        }

        private void cacheSizeBox_ValueChanged(object sender, EventArgs e) {
            PluginSettings.Default.YoutubeCacheSize = (int)this.cacheSizeBox.Value;
            PluginSettings.Default.Save();
        }
    }
}
