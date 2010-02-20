using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkypeBot.plugins.config.wordfilter {
    public partial class WordFilterConfigForm : Form {
        private List<WordFilterPlugin.Filter> sampleFilters;

        public WordFilterConfigForm() {
            InitializeComponent();

            sampleFilters = WordFilterPlugin.SampleFilters;

            loadCurrentList();
            loadSampleList();
        }

        private void loadCurrentList() {
            currentFilterBox.Items.Clear();
            foreach (WordFilterPlugin.Filter filter in PluginSettings.Default.WordFilters) {
                currentFilterBox.Items.Add(filter.name);
            }
        }

        private void loadSampleList() {
            sampleFilterBox.Items.Clear();
            foreach (WordFilterPlugin.Filter filter in sampleFilters) {
                sampleFilterBox.Items.Add(filter.name);
            }
        }

        private void addBtn_Click(object sender, EventArgs e) {
            WordFilterPlugin.Filter filter = new WordFilterPlugin.Filter(
                nameTxt.Text, regexTxt.Text, replacementTxt.Text
            );

            nameTxt.Text = "";
            regexTxt.Text = "";
            replacementTxt.Text = "";

            PluginSettings.Default.WordFilters.Add(filter);
            PluginSettings.Default.Save();

            loadCurrentList();
        }

        private void loadBtn_Click(object sender, EventArgs e) {
            int idx = sampleFilterBox.SelectedIndex;
            if (idx != -1) {
                WordFilterPlugin.Filter filter = sampleFilters[idx];

                nameTxt.Text = filter.name;
                regexTxt.Text = filter.regex;
                replacementTxt.Text = filter.replacement;
            }
        }

        private void editBtn_Click(object sender, EventArgs e) {
            int idx = currentFilterBox.SelectedIndex;
            if (idx != -1) {
                WordFilterPlugin.Filter filter = PluginSettings.Default.WordFilters[idx];
                deleteBtn_Click(sender, e);

                nameTxt.Text = filter.name;
                regexTxt.Text = filter.regex;
                replacementTxt.Text = filter.replacement;
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e) {
            int idx = currentFilterBox.SelectedIndex;
            if (idx != -1) {
                PluginSettings.Default.WordFilters.RemoveAt(idx);
                PluginSettings.Default.Save();
                loadCurrentList();
            }
        }
    }
}
