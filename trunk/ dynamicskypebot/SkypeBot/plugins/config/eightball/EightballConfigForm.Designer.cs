namespace SkypeBot.plugins.config.eightball {
    partial class EightballConfigForm {
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.addBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.responseList = new System.Windows.Forms.ListBox();
            this.delBtn = new System.Windows.Forms.Button();
            this.resetBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addBox);
            this.groupBox1.Controls.Add(this.addBtn);
            this.groupBox1.Location = new System.Drawing.Point(14, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add new response";
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(418, 17);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(46, 23);
            this.addBtn.TabIndex = 0;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // addBox
            // 
            this.addBox.Location = new System.Drawing.Point(6, 19);
            this.addBox.Name = "addBox";
            this.addBox.Size = new System.Drawing.Size(406, 20);
            this.addBox.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.resetBtn);
            this.groupBox2.Controls.Add(this.delBtn);
            this.groupBox2.Controls.Add(this.responseList);
            this.groupBox2.Location = new System.Drawing.Point(14, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 241);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current responses";
            // 
            // responseList
            // 
            this.responseList.FormattingEnabled = true;
            this.responseList.Location = new System.Drawing.Point(6, 19);
            this.responseList.Name = "responseList";
            this.responseList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.responseList.Size = new System.Drawing.Size(458, 186);
            this.responseList.TabIndex = 0;
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(6, 211);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(114, 24);
            this.delBtn.TabIndex = 1;
            this.delBtn.Text = "Delete selected";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // resetBtn
            // 
            this.resetBtn.Location = new System.Drawing.Point(358, 211);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(106, 24);
            this.resetBtn.TabIndex = 2;
            this.resetBtn.Text = "Reset to default";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // EightballConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 331);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EightballConfigForm";
            this.Text = "Configure the 8-ball";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox addBox;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.ListBox responseList;

    }
}