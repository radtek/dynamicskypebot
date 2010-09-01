namespace SkypeBot.plugins.config.transformation {
    partial class TransformationConfigForm {
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
            this.autoTransform = new System.Windows.Forms.CheckBox();
            this.transCombo = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.triggerList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.autoTransform);
            this.groupBox1.Controls.Add(this.transCombo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Automatic transformation";
            // 
            // autoTransform
            // 
            this.autoTransform.AutoSize = true;
            this.autoTransform.Location = new System.Drawing.Point(6, 19);
            this.autoTransform.Name = "autoTransform";
            this.autoTransform.Size = new System.Drawing.Size(177, 17);
            this.autoTransform.TabIndex = 1;
            this.autoTransform.Text = "Enable automatic transformation";
            this.autoTransform.UseVisualStyleBackColor = true;
            this.autoTransform.CheckedChanged += new System.EventHandler(this.autoTransform_CheckedChanged);
            // 
            // transCombo
            // 
            this.transCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transCombo.FormattingEnabled = true;
            this.transCombo.Location = new System.Drawing.Point(6, 42);
            this.transCombo.Name = "transCombo";
            this.transCombo.Size = new System.Drawing.Size(257, 21);
            this.transCombo.TabIndex = 0;
            this.transCombo.SelectionChangeCommitted += new System.EventHandler(this.transCombo_SelectionChangeCommitted);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.triggerList);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 122);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Triggered transformation";
            // 
            // triggerList
            // 
            this.triggerList.FormattingEnabled = true;
            this.triggerList.Location = new System.Drawing.Point(6, 19);
            this.triggerList.Name = "triggerList";
            this.triggerList.Size = new System.Drawing.Size(257, 94);
            this.triggerList.TabIndex = 0;
            this.triggerList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.triggerList_ItemCheck);
            // 
            // TransformationConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 220);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TransformationConfigForm";
            this.Text = "Configure...";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox transCombo;
        private System.Windows.Forms.CheckBox autoTransform;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox triggerList;
    }
}