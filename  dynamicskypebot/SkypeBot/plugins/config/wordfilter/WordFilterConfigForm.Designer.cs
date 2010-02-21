namespace SkypeBot.plugins.config.wordfilter {
    partial class WordFilterConfigForm {
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
            this.caseSensitive = new System.Windows.Forms.CheckBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.replacementTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.regexTxt = new System.Windows.Forms.TextBox();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.currentFilterBox = new System.Windows.Forms.ListBox();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.sampleFilterBox = new System.Windows.Forms.ListBox();
            this.priorityBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priorityBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.priorityBox);
            this.groupBox1.Controls.Add(this.caseSensitive);
            this.groupBox1.Controls.Add(this.addBtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.replacementTxt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.regexTxt);
            this.groupBox1.Controls.Add(this.nameTxt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add new filter";
            // 
            // caseSensitive
            // 
            this.caseSensitive.AutoSize = true;
            this.caseSensitive.Location = new System.Drawing.Point(9, 128);
            this.caseSensitive.Name = "caseSensitive";
            this.caseSensitive.Size = new System.Drawing.Size(94, 17);
            this.caseSensitive.TabIndex = 7;
            this.caseSensitive.Text = "Case sensitive";
            this.caseSensitive.UseVisualStyleBackColor = true;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(232, 124);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 6;
            this.addBtn.Text = "Add filter";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Replacement:";
            // 
            // replacementTxt
            // 
            this.replacementTxt.Location = new System.Drawing.Point(86, 72);
            this.replacementTxt.Name = "replacementTxt";
            this.replacementTxt.Size = new System.Drawing.Size(217, 20);
            this.replacementTxt.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Regex:";
            // 
            // regexTxt
            // 
            this.regexTxt.Location = new System.Drawing.Point(86, 46);
            this.regexTxt.Name = "regexTxt";
            this.regexTxt.Size = new System.Drawing.Size(217, 20);
            this.regexTxt.TabIndex = 2;
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(86, 20);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(217, 20);
            this.nameTxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.currentFilterBox);
            this.groupBox2.Controls.Add(this.deleteBtn);
            this.groupBox2.Controls.Add(this.editBtn);
            this.groupBox2.Location = new System.Drawing.Point(12, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(490, 237);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current filters";
            // 
            // currentFilterBox
            // 
            this.currentFilterBox.FormattingEnabled = true;
            this.currentFilterBox.Location = new System.Drawing.Point(6, 19);
            this.currentFilterBox.Name = "currentFilterBox";
            this.currentFilterBox.Size = new System.Drawing.Size(478, 186);
            this.currentFilterBox.TabIndex = 2;
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(387, 208);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(97, 23);
            this.deleteBtn.TabIndex = 1;
            this.deleteBtn.Text = "Delete selected";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(6, 208);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(87, 23);
            this.editBtn.TabIndex = 0;
            this.editBtn.Text = "Edit selected";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.loadBtn);
            this.groupBox3.Controls.Add(this.sampleFilterBox);
            this.groupBox3.Location = new System.Drawing.Point(331, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(172, 155);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sample filters";
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(68, 106);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(97, 23);
            this.loadBtn.TabIndex = 1;
            this.loadBtn.Text = "Load into editor";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // sampleFilterBox
            // 
            this.sampleFilterBox.FormattingEnabled = true;
            this.sampleFilterBox.Location = new System.Drawing.Point(6, 18);
            this.sampleFilterBox.Name = "sampleFilterBox";
            this.sampleFilterBox.Size = new System.Drawing.Size(159, 82);
            this.sampleFilterBox.TabIndex = 0;
            // 
            // priorityBox
            // 
            this.priorityBox.Location = new System.Drawing.Point(86, 98);
            this.priorityBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.priorityBox.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.priorityBox.Name = "priorityBox";
            this.priorityBox.Size = new System.Drawing.Size(58, 20);
            this.priorityBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Priority:";
            // 
            // WordFilterConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 422);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WordFilterConfigForm";
            this.ShowIcon = false;
            this.Text = "WordFilterConfigForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.priorityBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.ListBox currentFilterBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox regexTxt;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox replacementTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.ListBox sampleFilterBox;
        private System.Windows.Forms.CheckBox caseSensitive;
        private System.Windows.Forms.NumericUpDown priorityBox;
        private System.Windows.Forms.Label label4;
    }
}