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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.updateInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.updateCheckToggle = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.helpOnMinimized = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateInterval)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.groupBox2.Controls.Add(this.helpOnMinimized);
            this.groupBox2.Location = new System.Drawing.Point(12, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 44);
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
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 140);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox updateCheckToggle;
        private System.Windows.Forms.NumericUpDown updateInterval;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox helpOnMinimized;

    }
}