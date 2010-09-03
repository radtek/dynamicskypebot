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
            this.taskIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitSkypeBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DescriptionBox = new System.Windows.Forms.TextBox();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.settingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.suggestionBugPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.reportAProblemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskIconMenu.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
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
            this.taskIcon.ContextMenuStrip = this.taskIconMenu;
            this.taskIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("taskIcon.Icon")));
            this.taskIcon.Text = "Skype Bot";
            this.taskIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.taskIcon_MouseDoubleClick);
            // 
            // taskIconMenu
            // 
            this.taskIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreWindowToolStripMenuItem,
            this.toolStripMenuItem7,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.exitSkypeBotToolStripMenuItem});
            this.taskIconMenu.Name = "taskIconMenu";
            this.taskIconMenu.Size = new System.Drawing.Size(159, 126);
            // 
            // restoreWindowToolStripMenuItem
            // 
            this.restoreWindowToolStripMenuItem.Name = "restoreWindowToolStripMenuItem";
            this.restoreWindowToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.restoreWindowToolStripMenuItem.Text = "Restore window";
            this.restoreWindowToolStripMenuItem.Click += new System.EventHandler(this.restoreWindowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem1.Text = "Settings";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.settingsItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem2.Text = "Links";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem3.Text = "Website";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.downloadPageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem4.Text = "Command list";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem5.Text = "Suggestions";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.suggestionBugPageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem6.Text = "Changelog";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.changelogToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // exitSkypeBotToolStripMenuItem
            // 
            this.exitSkypeBotToolStripMenuItem.Name = "exitSkypeBotToolStripMenuItem";
            this.exitSkypeBotToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitSkypeBotToolStripMenuItem.Text = "Exit";
            this.exitSkypeBotToolStripMenuItem.Click += new System.EventHandler(this.exitSkypeBotToolStripMenuItem_Click);
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
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsItem,
            this.helpToolStripMenuItem,
            this.helpToolStripMenuItem2});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(695, 24);
            this.mainMenuStrip.TabIndex = 4;
            this.mainMenuStrip.Text = "menuStrip1";
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
            this.downloadPageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.downloadPageToolStripMenuItem.Text = "Website";
            this.downloadPageToolStripMenuItem.Click += new System.EventHandler(this.downloadPageToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.helpToolStripMenuItem1.Text = "Command list";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // suggestionBugPageToolStripMenuItem
            // 
            this.suggestionBugPageToolStripMenuItem.Name = "suggestionBugPageToolStripMenuItem";
            this.suggestionBugPageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.suggestionBugPageToolStripMenuItem.Text = "Suggestions";
            this.suggestionBugPageToolStripMenuItem.Click += new System.EventHandler(this.suggestionBugPageToolStripMenuItem_Click);
            // 
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.changelogToolStripMenuItem.Text = "Changelog";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.changelogToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem2
            // 
            this.helpToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportAProblemToolStripMenuItem});
            this.helpToolStripMenuItem2.Name = "helpToolStripMenuItem2";
            this.helpToolStripMenuItem2.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem2.Text = "Help";
            // 
            // reportAProblemToolStripMenuItem
            // 
            this.reportAProblemToolStripMenuItem.Name = "reportAProblemToolStripMenuItem";
            this.reportAProblemToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.reportAProblemToolStripMenuItem.Text = "Report a problem";
            this.reportAProblemToolStripMenuItem.Click += new System.EventHandler(this.reportAProblemToolStripMenuItem_Click);
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
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "ConfigForm";
            this.Text = "Skype Bot Configuration Panel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.Resize += new System.EventHandler(this.ConfigForm_Resize);
            this.taskIconMenu.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox PluginListBox;
        private System.Windows.Forms.TextBox messageLog;
        private System.Windows.Forms.NotifyIcon taskIcon;
        private System.Windows.Forms.TextBox DescriptionBox;
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem settingsItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem suggestionBugPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip taskIconMenu;
        private System.Windows.Forms.ToolStripMenuItem restoreWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitSkypeBotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem reportAProblemToolStripMenuItem;
    }
}

