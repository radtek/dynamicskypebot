using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKYPE4COMLib;
using SkypeBot.plugins;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using log4net;

namespace SkypeBot {
    public partial class ConfigForm : Form {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<Plugin> plugins = new List<Plugin>( new Plugin[] {
            new GoogleImageSearchPlugin(),
            new AcronymPlugin(),
            new DictionaryPlugin(),
            new YouTubePlugin(),
            new CypherPlugin(),
            new UrbanDictionaryPlugin(),
            new DicePlugin(),
            new FourChanPlugin(),
            new PornPlugin(),
            new WafflePlugin(),
            new HighLowPlugin(),
            new LolcatPlugin(),
            new WikipediaPlugin(),
            new SomethingAwfulPlugin(),
            new BashPlugin(),
            new PenisPlugin(),
            new RandomLinkPlugin(),
            new QuotePlugin(),
            new TwitterPlugin(),
            new MazePlugin(),
            new EightBallPlugin(),
            new DeviantArtPlugin(),
            new QDBPlugin(),
            new WordFilterPlugin(),
            new FMLPlugin(),
            new OverheardPlugin(),
            new TextsFromLastNightPlugin(),
            new NotAlwaysRightPlugin(),
            new TransformationPlugin(),
            new LMLPlugin(),
            new ThereIFixedItPlugin(),

        });

        private Skype skype;
        private int lastId = -1;
        private String blocked; // Blocklist of nick/channel combos that aren't allowed.
        private Timer updateTimer;

        public event _ISkypeEvents_MessageStatusEventHandler onSkypeMessage;
        public event _ISkypeEvents_MessageStatusEventHandler onSkypeMessage_earlyBird;

        public ConfigForm() {
            InitializeComponent();
            FormConsoleAppender.appendMethod += addLogLine;

            plugins.Sort(
                delegate(Plugin p1, Plugin p2) {
                    return Comparer<String>.Default.Compare(p1.name(), p2.name());
                }
            );

            PluginListBox.ItemCheck += (obj, e) =>
            {
                if (e.NewValue == CheckState.Checked) {
                    log.Debug("Loading " + plugins[e.Index].name());
                    loadPlugin(plugins[e.Index]);
                } else {
                    log.Debug("Unloading " + plugins[e.Index].name());
                    unloadPlugin(plugins[e.Index]);
                }
            };

            PluginListBox.SelectedIndexChanged += (obj, e) =>
            {
                int selected = PluginListBox.SelectedIndex;
                if (selected == -1) {
                    DescriptionBox.Text = "";
                    ConfigButton.Visible = false;
                }
                else {
                    Plugin plugin = plugins[selected];
                    DescriptionBox.Text = plugin.description();
                    ConfigButton.Visible = plugin.canConfig();
                }
                
            };

            if (Properties.Settings.Default.LoadedPlugins == null) {
                log.Debug("Creating LoadedPlugins property.");
                Properties.Settings.Default.LoadedPlugins = new System.Collections.Specialized.StringCollection();
            }
            if (Properties.Settings.Default.Whitelist == null) {
                log.Debug("Creating LoadedPlugins property.");
                Properties.Settings.Default.Whitelist = new System.Collections.Specialized.StringCollection();
            }

            log.Debug("Initiating connection to Skype...");
            skype = new Skype();
            if (!skype.Client.IsRunning) {
                log.Debug("Skype is not running; starting...");
                skype.Client.Start(false, false);
            }

            log.Debug("Attaching to Skype...");
            skype.Attach(9, true);

            log.Debug("Attached to Skype.");

            skype.MessageStatus += (ChatMessage message, TChatMessageStatus status) =>
            {
                if (Properties.Settings.Default.UseWhitelist ^
                    Properties.Settings.Default.Whitelist.Contains(message.Chat.Name)) {
                    return;
                }

                Boolean isBlocked = blocked.Contains(
                        skype.CurrentUser.Handle + " :: " + message.ChatName
                    );

                if ((status.Equals(TChatMessageStatus.cmsReceived) || status.Equals(TChatMessageStatus.cmsSent) ||
                    (status.Equals(TChatMessageStatus.cmsRead) && message.Id > lastId) ) && !isBlocked) {
                    
                    log.Info(String.Format("{0}MSG: {1}", status.Equals(TChatMessageStatus.cmsRead) ? "r" :
                                                          status.Equals(TChatMessageStatus.cmsSent) ? "s" : "", message.Body));

                    lastId = message.Id;

                    // Ignore messages older than 1 hour.
                    if (message.Timestamp.CompareTo(DateTime.Now.AddHours(-1.0)) < 0) {
                        log.Debug("Message too old; not going to react.");
                    }

                    Match output = Regex.Match(message.Body, @"^!help", RegexOptions.IgnoreCase);
                    if (output.Success) {
                        String outputMsg = "Help for the bot can be found at http://mathemaniac.org/apps/skypebot/help/.";
                        message.Chat.SendMessage(outputMsg);
                        
                        return;
                    }

                    output = Regex.Match(message.Body, @"^!loaded", RegexOptions.IgnoreCase);
                    if (output.Success) {
                        String outputMsg = "";
                        foreach (Plugin p in plugins) {
                            if (isLoaded(p)) {
                                outputMsg += (outputMsg == "" ? "" : ", ");
                                outputMsg += Regex.Replace(p.name(), @"\sPlugin$", "", RegexOptions.IgnoreCase);
                            }
                        }
                        outputMsg = "The following plugins are loaded:\n" + outputMsg;

                        message.Chat.SendMessage(outputMsg);

                        return;
                    }

                    output = Regex.Match(message.Body, @"^!version", RegexOptions.IgnoreCase);
                    if (output.Success && System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) {
                        message.Chat.SendMessage(
                            "Running Dynamic Skype Bot v" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion
                            + " (http://mathemaniac.org/wp/dynamic-skype-bot/)"
                        );
                        return;
                    }

                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += (obj, e) => {
                            if (onSkypeMessage_earlyBird != null) {
                                onSkypeMessage_earlyBird(message, status);
                            }
                            if (onSkypeMessage != null) {
                                onSkypeMessage(message, status);
                            }
                        };
                        bw.RunWorkerAsync();
                }
            };

            populatePluginList();

            blocked = "";
            BackgroundWorker baw = new BackgroundWorker();
            baw.DoWork += (obj, e) => {
                log.Debug("Fetching list of blocked people/chat combinations...");
                WebRequest webReq = WebRequest.Create("http://mathemaniac.org/apps/skypebot/blocked.txt");
                try {
                    webReq.Timeout = 10000;
                    WebResponse response = webReq.GetResponse();
                    blocked = new StreamReader(response.GetResponseStream()).ReadToEnd();
                } catch (Exception ex) {
                    log.Warn("Failed to fetch list of blocked people/chat combinations.", ex);
                }
            };
            baw.RunWorkerAsync();

            // Update check
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed &&
                Properties.Settings.Default.UpdateCheck) {
                updateTimer = new Timer();
                updateTimer.Interval = Properties.Settings.Default.UpdateCheckInterval * 60 * 1000;
                updateTimer.Tick += (obj, e) => {
                    log.Info("Checking for updates...");
                    try {
                        if (System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CheckForUpdate()) {
                            log.Info("Update available for download!");
                            if (taskIcon.Visible) {
                                taskIcon.BalloonTipTitle = "Skype Bot";
                                taskIcon.BalloonTipText = "A new version of the Skype Bot is ready for download.";
                                taskIcon.BalloonTipIcon = ToolTipIcon.Info;
                                taskIcon.ShowBalloonTip(Properties.Settings.Default.UpdateCheckInterval * 60 * 1000);
                            } else {
                                updateTimer.Stop();
                                MessageBox.Show("A new version is ready for download!");
                            }
                        } else {
                            log.Info("No updates found.");
                        }
                    } catch(Exception ex) {
                        log.Warn("Exception arose while checking for update.", ex);
                    }
                };
                updateTimer.Start();
            }
        }

        private void populatePluginList() {
            log.Debug("Populating plugin list...");
            foreach (Plugin plugin in plugins) {
                PluginListBox.Items.Add(plugin.name(), Properties.Settings.Default.LoadedPlugins.Contains(plugin.name()));
            }
        }

        public void loadPlugin(Plugin plugin) {
            BackgroundWorker baw = new BackgroundWorker();
            baw.DoWork += (obj, e) => {
                plugin.load();
                if (plugin.earlyBird()) {
                    onSkypeMessage_earlyBird += plugin.Skype_MessageStatus;
                } else {
                    onSkypeMessage += plugin.Skype_MessageStatus;
                }
            };
            baw.RunWorkerAsync();
            
            Properties.Settings.Default.LoadedPlugins.Add(plugin.name());
            Properties.Settings.Default.Save();
        }

        public bool isLoaded(Plugin plugin) {
            return Properties.Settings.Default.LoadedPlugins.Contains(plugin.name());
        }

        public void unloadPlugin(Plugin plugin) {
            BackgroundWorker baw = new BackgroundWorker();
            baw.DoWork += (obj, e) => { plugin.unload(); };
            baw.RunWorkerAsync();
            onSkypeMessage -= plugin.Skype_MessageStatus;
            while (Properties.Settings.Default.LoadedPlugins.Contains(plugin.name()))
                Properties.Settings.Default.LoadedPlugins.Remove(plugin.name());
            Properties.Settings.Default.Save();
        }

        delegate void AddLogLineCallback(String sender, String msg, log4net.Core.Level severity);

        public void addLogLine(String sender, String msg, log4net.Core.Level severity) {
            if (messageLog.InvokeRequired) {
                AddLogLineCallback ac = new AddLogLineCallback(addLogLine);
                this.Invoke(ac, new object[] { sender, msg, severity });
            } else {
                if (severity.Equals(log4net.Core.Level.Error)) {
                    messageLog.Text += "(!!!) ";
                } else if (severity.Equals(log4net.Core.Level.Warn)) {
                    messageLog.Text += "(!) ";
                } else if (severity.Equals(log4net.Core.Level.Debug)) {
                    messageLog.Text += "(D) ";
                }

                if (sender.StartsWith("SkypeBot.plugins.")) {
                    messageLog.Text += String.Format("{0}: {1}", sender.Split('.').Last<String>(), msg) + Environment.NewLine;
                } else {
                    messageLog.Text += msg + Environment.NewLine;
                }
                messageLog.SelectionStart = messageLog.Text.Length;
                messageLog.ScrollToCaret();
            }
        }

        private void ConfigForm_Resize(object sender, EventArgs e) {
            taskIcon.BalloonTipTitle = "Skype Bot";
            taskIcon.BalloonTipText = "Skype Bot now lives down here!";
            taskIcon.BalloonTipIcon = ToolTipIcon.Info;

            if (WindowState == FormWindowState.Minimized) {
                taskIcon.Visible = true;
                if (Properties.Settings.Default.ShowMinimizeHelpBubble) {
                    taskIcon.ShowBalloonTip(1000);
                }
                Hide();

                log.Debug("Minimized window to tray.");
            } else {
                taskIcon.Visible = false;
                log.Debug("Restored window from tray.");
            }
        }

        private void taskIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            ShowWindow();
        }

        private void ShowWindow() {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void ConfigButton_Click(object sender, EventArgs e) {
            log.Debug(String.Format("Opening configuration window for {0}.", plugins[PluginListBox.SelectedIndex].name()));
            plugins[PluginListBox.SelectedIndex].openConfig();
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e) {
            log.Info("Main window closed; shutting down. Goodbye.");
            Properties.Settings.Default.Save();
            PluginSettings.Default.Save();
        }

        private void downloadPageToolStripMenuItem_Click(object sender, EventArgs e) {
            log.Debug("Link clicked: Website");
            System.Diagnostics.Process.Start("http://mathemaniac.org/wp/dynamic-skype-bot/");
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e) {
            log.Debug("Link clicked: Help");
            System.Diagnostics.Process.Start("http://mathemaniac.org/apps/skypebot/help/");
        }

        private void suggestionBugPageToolStripMenuItem_Click(object sender, EventArgs e) {
            log.Debug("Link clicked: Suggestions");
            System.Diagnostics.Process.Start("http://skypebot.uservoice.com");
        }

        private void changelogToolStripMenuItem_Click(object sender, EventArgs e) {
            log.Debug("Link clicked: Changelog");
            System.Diagnostics.Process.Start("http://code.google.com/p/dynamicskypebot/wiki/ChangeLog");
        }

        private void settingsItem_Click(object sender, EventArgs e) {
            log.Debug("Opening settings window...");
            SettingsForm sf = new SettingsForm(skype);
            sf.Visible = true;
        }

        private void restoreWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            ShowWindow();
        }

        private void exitSkypeBotToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Windows.Forms.Application.Exit();
        }
    }

    public class FormConsoleAppender : log4net.Appender.AppenderSkeleton {
        public static Action<String, String, log4net.Core.Level> appendMethod = null;

        protected override void Append(log4net.Core.LoggingEvent loggingEvent) {
            // Cannot log to screen before we know how to append.
            if (appendMethod == null)
                return;

            if (!Properties.Settings.Default.VerboseConsole && loggingEvent.Level.Equals(log4net.Core.Level.Debug))
                return;

            try {
                appendMethod.Invoke(loggingEvent.LoggerName, loggingEvent.RenderedMessage, loggingEvent.Level);
            } catch (Exception e) {
                ErrorHandler.Error("Something went wrong trying to write to the visible 'console'.", e);
            }
        
        }
    }
}
