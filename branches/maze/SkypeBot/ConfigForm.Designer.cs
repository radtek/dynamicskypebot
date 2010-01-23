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
            this.SuspendLayout();
            // 
            // PluginListBox
            // 
            this.PluginListBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PluginListBox.FormattingEnabled = true;
            this.PluginListBox.Location = new System.Drawing.Point(12, 12);
            this.PluginListBox.Name = "PluginListBox";
            this.PluginListBox.ScrollAlwaysVisible = true;
            this.PluginListBox.Size = new System.Drawing.Size(671, 202);
            this.PluginListBox.TabIndex = 0;
            // 
            // messageLog
            // 
            this.messageLog.BackColor = System.Drawing.Color.LightGray;
            this.messageLog.ForeColor = System.Drawing.Color.Black;
            this.messageLog.Location = new System.Drawing.Point(12, 280);
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
            this.DescriptionBox.Location = new System.Drawing.Point(12, 220);
            this.DescriptionBox.Multiline = true;
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.ReadOnly = true;
            this.DescriptionBox.Size = new System.Drawing.Size(671, 54);
            this.DescriptionBox.TabIndex = 2;
            // 
            // ConfigButton
            // 
            this.ConfigButton.BackColor = System.Drawing.SystemColors.Control;
            this.ConfigButton.Location = new System.Drawing.Point(563, 184);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(96, 23);
            this.ConfigButton.TabIndex = 3;
            this.ConfigButton.Text = "Configure...";
            this.ConfigButton.UseVisualStyleBackColor = false;
            this.ConfigButton.Visible = false;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 497);
            this.Controls.Add(this.ConfigButton);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.messageLog);
            this.Controls.Add(this.PluginListBox);
            this.Name = "ConfigForm";
            this.Text = "Skype Bot Configuration Panel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.Resize += new System.EventHandler(this.ConfigForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox PluginListBox;
        private System.Windows.Forms.TextBox messageLog;
        private System.Windows.Forms.NotifyIcon taskIcon;
        private System.Windows.Forms.TextBox DescriptionBox;
        private System.Windows.Forms.Button ConfigButton;
    }
}

