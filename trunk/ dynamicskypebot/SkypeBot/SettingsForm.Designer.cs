namespace SkypeBot {
    partial class SettingsForm {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.updateInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.updateCheckToggle = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.helpOnMinimized = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clearWhitelist = new System.Windows.Forms.Button();
            this.deleteFromWhitelist = new System.Windows.Forms.Button();
            this.addToWhitelist = new System.Windows.Forms.Button();
            this.whiteList = new System.Windows.Forms.ListBox();
            this.domainHelpButton = new System.Windows.Forms.Button();
            this.domainMode = new System.Windows.Forms.ComboBox();
            this.domainHelp = new System.Windows.Forms.ToolTip(this.components);
            this.verboseConsole = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateInterval)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.updateInterval);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.updateCheckToggle);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Updates";
            // 
            // updateInterval
            // 
            this.updateInterval.Location = new System.Drawing.Point(180, 37);
            this.updateInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.updateInterval.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.updateInterval.Name = "updateInterval";
            this.updateInterval.Size = new System.Drawing.Size(69, 20);
            this.updateInterval.TabIndex = 2;
            this.updateInterval.Tag = "UpdateCheckInterval";
            this.updateInterval.ThousandsSeparator = true;
            this.updateInterval.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.updateInterval.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Update check interval (in minutes):";
            // 
            // updateCheckToggle
            // 
            this.updateCheckToggle.AutoSize = true;
            this.updateCheckToggle.Location = new System.Drawing.Point(6, 19);
            this.updateCheckToggle.Name = "updateCheckToggle";
            this.updateCheckToggle.Size = new System.Drawing.Size(131, 17);
            this.updateCheckToggle.TabIndex = 0;
            this.updateCheckToggle.Tag = "UpdateCheck";
            this.updateCheckToggle.Text = "Perform update check";
            this.updateCheckToggle.UseVisualStyleBackColor = true;
            this.updateCheckToggle.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.verboseConsole);
            this.groupBox2.Controls.Add(this.helpOnMinimized);
            this.groupBox2.Location = new System.Drawing.Point(12, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 66);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Miscellaneous";
            // 
            // helpOnMinimized
            // 
            this.helpOnMinimized.AutoSize = true;
            this.helpOnMinimized.Location = new System.Drawing.Point(6, 19);
            this.helpOnMinimized.Name = "helpOnMinimized";
            this.helpOnMinimized.Size = new System.Drawing.Size(237, 17);
            this.helpOnMinimized.TabIndex = 0;
            this.helpOnMinimized.Tag = "ShowMinimizeHelpBubble";
            this.helpOnMinimized.Text = "Show help bubble when window is minimized";
            this.helpOnMinimized.UseVisualStyleBackColor = true;
            this.helpOnMinimized.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clearWhitelist);
            this.groupBox3.Controls.Add(this.deleteFromWhitelist);
            this.groupBox3.Controls.Add(this.addToWhitelist);
            this.groupBox3.Controls.Add(this.whiteList);
            this.groupBox3.Controls.Add(this.domainHelpButton);
            this.groupBox3.Controls.Add(this.domainMode);
            this.groupBox3.Location = new System.Drawing.Point(12, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(257, 166);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Domain";
            // 
            // clearWhitelist
            // 
            this.clearWhitelist.Location = new System.Drawing.Point(206, 136);
            this.clearWhitelist.Name = "clearWhitelist";
            this.clearWhitelist.Size = new System.Drawing.Size(45, 23);
            this.clearWhitelist.TabIndex = 5;
            this.clearWhitelist.Text = "Clear";
            this.clearWhitelist.UseVisualStyleBackColor = true;
            this.clearWhitelist.Click += new System.EventHandler(this.clearWhitelist_Click);
            // 
            // deleteFromWhitelist
            // 
            this.deleteFromWhitelist.Location = new System.Drawing.Point(146, 136);
            this.deleteFromWhitelist.Name = "deleteFromWhitelist";
            this.deleteFromWhitelist.Size = new System.Drawing.Size(54, 23);
            this.deleteFromWhitelist.TabIndex = 4;
            this.deleteFromWhitelist.Text = "Delete";
            this.deleteFromWhitelist.UseVisualStyleBackColor = true;
            this.deleteFromWhitelist.Click += new System.EventHandler(this.deleteFromWhitelist_Click);
            // 
            // addToWhitelist
            // 
            this.addToWhitelist.Location = new System.Drawing.Point(6, 136);
            this.addToWhitelist.Name = "addToWhitelist";
            this.addToWhitelist.Size = new System.Drawing.Size(42, 23);
            this.addToWhitelist.TabIndex = 3;
            this.addToWhitelist.Text = "Add";
            this.addToWhitelist.UseVisualStyleBackColor = true;
            this.addToWhitelist.Click += new System.EventHandler(this.addToWhitelist_Click);
            // 
            // whiteList
            // 
            this.whiteList.FormattingEnabled = true;
            this.whiteList.Location = new System.Drawing.Point(6, 48);
            this.whiteList.Name = "whiteList";
            this.whiteList.Size = new System.Drawing.Size(245, 82);
            this.whiteList.TabIndex = 2;
            this.whiteList.Tag = "Whitelist";
            // 
            // domainHelpButton
            // 
            this.domainHelpButton.Image = global::SkypeBot.Properties.Resources.help;
            this.domainHelpButton.Location = new System.Drawing.Point(229, 21);
            this.domainHelpButton.Name = "domainHelpButton";
            this.domainHelpButton.Size = new System.Drawing.Size(22, 22);
            this.domainHelpButton.TabIndex = 1;
            this.domainHelp.SetToolTip(this.domainHelpButton, "Determines where the bot reacts.\r\nIf you select \"Blacklist\", it reacts in all cha" +
                    "ts except the ones listed.\r\nIf you select \"Whitelist\", it reacts only in the cha" +
                    "ts listed.");
            this.domainHelpButton.UseVisualStyleBackColor = true;
            // 
            // domainMode
            // 
            this.domainMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.domainMode.FormattingEnabled = true;
            this.domainMode.Items.AddRange(new object[] {
            "Blacklist",
            "Whitelist"});
            this.domainMode.Location = new System.Drawing.Point(6, 21);
            this.domainMode.Name = "domainMode";
            this.domainMode.Size = new System.Drawing.Size(217, 21);
            this.domainMode.TabIndex = 0;
            this.domainMode.SelectedIndexChanged += new System.EventHandler(this.domainMode_SelectedIndexChanged);
            // 
            // domainHelp
            // 
            this.domainHelp.AutomaticDelay = 100;
            this.domainHelp.AutoPopDelay = 60000;
            this.domainHelp.InitialDelay = 100;
            this.domainHelp.IsBalloon = true;
            this.domainHelp.ReshowDelay = 20;
            // 
            // verboseConsole
            // 
            this.verboseConsole.AutoSize = true;
            this.verboseConsole.Location = new System.Drawing.Point(6, 42);
            this.verboseConsole.Name = "verboseConsole";
            this.verboseConsole.Size = new System.Drawing.Size(187, 17);
            this.verboseConsole.TabIndex = 1;
            this.verboseConsole.Tag = "verboseConsole";
            this.verboseConsole.Text = "Show debug messages in console";
            this.verboseConsole.UseVisualStyleBackColor = true;
            this.verboseConsole.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 334);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsForm";
            this.Text = "Dynamic Skype Bot Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateInterval)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox updateCheckToggle;
        private System.Windows.Forms.NumericUpDown updateInterval;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox helpOnMinimized;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox domainMode;
        private System.Windows.Forms.Button domainHelpButton;
        private System.Windows.Forms.ToolTip domainHelp;
        private System.Windows.Forms.Button clearWhitelist;
        private System.Windows.Forms.Button deleteFromWhitelist;
        private System.Windows.Forms.Button addToWhitelist;
        private System.Windows.Forms.ListBox whiteList;
        private System.Windows.Forms.CheckBox verboseConsole;

    }
}