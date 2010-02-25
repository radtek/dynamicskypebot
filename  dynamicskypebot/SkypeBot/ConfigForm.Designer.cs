namespace SkypeBot {
    partial class ConfigForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.PluginListBox = new System.Windows.Forms.CheckedListBox();
            this.messageLog = new System.Windows.Forms.TextBox();
            this.taskIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.DescriptionBox = new System.Windows.Forms.TextBox();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.suggestionBugPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PluginListBox
            // 
            this.PluginListBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PluginListBox.FormattingEnabled = true;
            this.PluginListBox.Location = new System.Drawing.Point(12, 27);
            this.PluginListBox.Name = "PluginListBox";
            this.PluginListBox.ScrollAlwaysVisible = true;
            this.PluginListBox.Size = new System.Drawing.Size(671, 202);
            this.PluginListBox.TabIndex = 0;
            // 
            // messageLog
            // 
            this.messageLog.BackColor = System.Drawing.Color.LightGray;
            this.messageLog.ForeColor = System.Drawing.Color.Black;
            this.messageLog.Location = new System.Drawing.Point(12, 295);
            this.messageLog.Multiline = true;
            this.messageLog.Name = "messageLog";
            this.messageLog.ReadOnly = true;
            this.messageLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageLog.Size = new System.Drawing.Size(671, 205);
            this.messageLog.TabIndex = 1;
            // 
            // taskIcon
            // 
            this.taskIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("taskIcon.Icon")));
            this.taskIcon.Text = "Skype Bot";
            this.taskIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.taskIcon_MouseDoubleClick);
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.BackColor = System.Drawing.SystemColors.Control;
            this.DescriptionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DescriptionBox.Location = new System.Drawing.Point(12, 235);
            this.DescriptionBox.Multiline = true;
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.ReadOnly = true;
            this.DescriptionBox.Size = new System.Drawing.Size(671, 54);
            this.DescriptionBox.TabIndex = 2;
            // 
            // ConfigButton
            // 
            this.ConfigButton.BackColor = System.Drawing.SystemColors.Control;
            this.ConfigButton.Location = new System.Drawing.Point(564, 200);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(96, 23);
            this.ConfigButton.TabIndex = 3;
            this.ConfigButton.Text = "Configure...";
            this.ConfigButton.UseVisualStyleBackColor = false;
            this.ConfigButton.Visible = false;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(695, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsItem
            // 
            this.settingsItem.Name = "settingsItem";
            this.settingsItem.Size = new System.Drawing.Size(61, 20);
            this.settingsItem.Text = "Settings";
            this.settingsItem.Click += new System.EventHandler(this.settingsItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadPageToolStripMenuItem,
            this.helpToolStripMenuItem1,
            this.suggestionBugPageToolStripMenuItem,
            this.changelogToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.helpToolStripMenuItem.Text = "Links";
            // 
            // downloadPageToolStripMenuItem
            // 
            this.downloadPageToolStripMenuItem.Name = "downloadPageToolStripMenuItem";
            this.downloadPageToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.downloadPageToolStripMenuItem.Text = "Website";
            this.downloadPageToolStripMenuItem.Click += new System.EventHandler(this.downloadPageToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(149, 22);
            this.helpToolStripMenuItem1.Text = "Command list";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // suggestionBugPageToolStripMenuItem
            // 
            this.suggestionBugPageToolStripMenuItem.Name = "suggestionBugPageToolStripMenuItem";
            this.suggestionBugPageToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.suggestionBugPageToolStripMenuItem.Text = "Suggestions";
            this.suggestionBugPageToolStripMenuItem.Click += new System.EventHandler(this.suggestionBugPageToolStripMenuItem_Click);
            // 
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.changelogToolStripMenuItem.Text = "Changelog";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.changelogToolStripMenuItem_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 512);
            this.Controls.Add(this.ConfigButton);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.messageLog);
            this.Controls.Add(this.PluginListBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ConfigForm";
            this.Text = "Skype Bot Configuration Panel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.Resize += new System.EventHandler(this.ConfigForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox PluginListBox;
        private System.Windows.Forms.TextBox messageLog;
        private System.Windows.Forms.NotifyIcon taskIcon;
        private System.Windows.Forms.TextBox DescriptionBox;
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem suggestionBugPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
    }
}

