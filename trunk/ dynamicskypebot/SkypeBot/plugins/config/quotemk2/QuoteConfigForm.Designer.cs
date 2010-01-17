namespace SkypeBot.plugins.config.quotemk2 {
    partial class QuoteConfigForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuoteConfigForm));
            this.groupApproved = new System.Windows.Forms.GroupBox();
            this.approvedId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.approvedQuote = new System.Windows.Forms.TextBox();
            this.approvedTime = new System.Windows.Forms.DateTimePicker();
            this.approvedName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.approvedNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pendingId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pendingQuote = new System.Windows.Forms.TextBox();
            this.pendingTime = new System.Windows.Forms.DateTimePicker();
            this.pendingName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pendingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.approvedBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pendingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.approveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.groupApproved.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvedNavigator)).BeginInit();
            this.approvedNavigator.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pendingNavigator)).BeginInit();
            this.pendingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvedBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pendingBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupApproved
            // 
            this.groupApproved.Controls.Add(this.approvedId);
            this.groupApproved.Controls.Add(this.label1);
            this.groupApproved.Controls.Add(this.approvedQuote);
            this.groupApproved.Controls.Add(this.approvedTime);
            this.groupApproved.Controls.Add(this.approvedName);
            this.groupApproved.Controls.Add(this.label2);
            this.groupApproved.Controls.Add(this.approvedNavigator);
            this.groupApproved.Location = new System.Drawing.Point(12, 12);
            this.groupApproved.Name = "groupApproved";
            this.groupApproved.Size = new System.Drawing.Size(579, 328);
            this.groupApproved.TabIndex = 1;
            this.groupApproved.TabStop = false;
            this.groupApproved.Text = "Approved Quotes";
            // 
            // approvedId
            // 
            this.approvedId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.approvedBindingSource, "Id", true));
            this.approvedId.Location = new System.Drawing.Point(33, 46);
            this.approvedId.Name = "approvedId";
            this.approvedId.Size = new System.Drawing.Size(68, 20);
            this.approvedId.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "ID:";
            // 
            // approvedQuote
            // 
            this.approvedQuote.BackColor = System.Drawing.SystemColors.Control;
            this.approvedQuote.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.approvedBindingSource, "QuoteText", true));
            this.approvedQuote.Location = new System.Drawing.Point(6, 72);
            this.approvedQuote.Multiline = true;
            this.approvedQuote.Name = "approvedQuote";
            this.approvedQuote.Size = new System.Drawing.Size(566, 250);
            this.approvedQuote.TabIndex = 10;
            // 
            // approvedTime
            // 
            this.approvedTime.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.approvedBindingSource, "Submitted", true));
            this.approvedTime.Location = new System.Drawing.Point(441, 46);
            this.approvedTime.Name = "approvedTime";
            this.approvedTime.Size = new System.Drawing.Size(131, 20);
            this.approvedTime.TabIndex = 9;
            // 
            // approvedName
            // 
            this.approvedName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.approvedBindingSource, "Submitter", true));
            this.approvedName.Location = new System.Drawing.Point(184, 46);
            this.approvedName.Name = "approvedName";
            this.approvedName.Size = new System.Drawing.Size(251, 20);
            this.approvedName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Submitted by:";
            // 
            // approvedNavigator
            // 
            this.approvedNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.approvedNavigator.BindingSource = this.approvedBindingSource;
            this.approvedNavigator.CountItem = this.bindingNavigatorCountItem;
            this.approvedNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.approvedNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.approvedNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.approvedNavigator.Location = new System.Drawing.Point(3, 16);
            this.approvedNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.approvedNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.approvedNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.approvedNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.approvedNavigator.Name = "approvedNavigator";
            this.approvedNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.approvedNavigator.Size = new System.Drawing.Size(573, 25);
            this.approvedNavigator.TabIndex = 0;
            this.approvedNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pendingId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pendingQuote);
            this.groupBox1.Controls.Add(this.pendingTime);
            this.groupBox1.Controls.Add(this.pendingName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.pendingNavigator);
            this.groupBox1.Location = new System.Drawing.Point(12, 346);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 328);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pending Quotes";
            // 
            // pendingId
            // 
            this.pendingId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pendingBindingSource, "Id", true));
            this.pendingId.Location = new System.Drawing.Point(33, 46);
            this.pendingId.Name = "pendingId";
            this.pendingId.Size = new System.Drawing.Size(68, 20);
            this.pendingId.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "ID:";
            // 
            // pendingQuote
            // 
            this.pendingQuote.BackColor = System.Drawing.SystemColors.Control;
            this.pendingQuote.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pendingBindingSource, "QuoteText", true));
            this.pendingQuote.Location = new System.Drawing.Point(6, 72);
            this.pendingQuote.Multiline = true;
            this.pendingQuote.Name = "pendingQuote";
            this.pendingQuote.Size = new System.Drawing.Size(566, 250);
            this.pendingQuote.TabIndex = 10;
            // 
            // pendingTime
            // 
            this.pendingTime.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.pendingBindingSource, "Submitted", true));
            this.pendingTime.Location = new System.Drawing.Point(441, 46);
            this.pendingTime.Name = "pendingTime";
            this.pendingTime.Size = new System.Drawing.Size(131, 20);
            this.pendingTime.TabIndex = 9;
            // 
            // pendingName
            // 
            this.pendingName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pendingBindingSource, "Submitter", true));
            this.pendingName.Location = new System.Drawing.Point(184, 46);
            this.pendingName.Name = "pendingName";
            this.pendingName.Size = new System.Drawing.Size(251, 20);
            this.pendingName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(107, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Submitted by:";
            // 
            // pendingNavigator
            // 
            this.pendingNavigator.AddNewItem = this.toolStripButton1;
            this.pendingNavigator.BindingSource = this.pendingBindingSource;
            this.pendingNavigator.CountItem = this.toolStripLabel1;
            this.pendingNavigator.DeleteItem = this.toolStripButton2;
            this.pendingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.pendingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripSeparator1,
            this.toolStripTextBox1,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripSeparator3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator4,
            this.approveButton});
            this.pendingNavigator.Location = new System.Drawing.Point(3, 16);
            this.pendingNavigator.MoveFirstItem = this.toolStripButton3;
            this.pendingNavigator.MoveLastItem = this.toolStripButton6;
            this.pendingNavigator.MoveNextItem = this.toolStripButton5;
            this.pendingNavigator.MovePreviousItem = this.toolStripButton4;
            this.pendingNavigator.Name = "pendingNavigator";
            this.pendingNavigator.PositionItem = this.toolStripTextBox1;
            this.pendingNavigator.Size = new System.Drawing.Size(573, 25);
            this.pendingNavigator.TabIndex = 0;
            this.pendingNavigator.Text = "bindingNavigator2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.RightToLeftAutoMirrorImage = true;
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Add new";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel1.Text = "of {0}";
            this.toolStripLabel1.ToolTipText = "Total number of items";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.RightToLeftAutoMirrorImage = true;
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Delete";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.RightToLeftAutoMirrorImage = true;
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Move first";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.RightToLeftAutoMirrorImage = true;
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Move previous";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.AccessibleName = "Position";
            this.toolStripTextBox1.AutoSize = false;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(50, 23);
            this.toolStripTextBox1.Text = "0";
            this.toolStripTextBox1.ToolTipText = "Current position";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.RightToLeftAutoMirrorImage = true;
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Move next";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.RightToLeftAutoMirrorImage = true;
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "Move last";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // approvedBindingSource
            // 
            this.approvedBindingSource.DataSource = typeof(SkypeBot.plugins.QuotePluginMk2.Quote);
            this.approvedBindingSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.approvedBindingSource_ListChanged);
            // 
            // pendingBindingSource
            // 
            this.pendingBindingSource.DataSource = typeof(SkypeBot.plugins.QuotePluginMk2.Quote);
            this.pendingBindingSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.approvedBindingSource_ListChanged);
            // 
            // approveButton
            // 
            this.approveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.approveButton.Image = ((System.Drawing.Image)(resources.GetObject("approveButton.Image")));
            this.approveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.approveButton.Name = "approveButton";
            this.approveButton.Size = new System.Drawing.Size(23, 22);
            this.approveButton.Text = "Approve";
            this.approveButton.Click += new System.EventHandler(this.approveButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // QuoteConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 686);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupApproved);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "QuoteConfigForm";
            this.Text = "QuoteConfigForm";
            this.groupApproved.ResumeLayout(false);
            this.groupApproved.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvedNavigator)).EndInit();
            this.approvedNavigator.ResumeLayout(false);
            this.approvedNavigator.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pendingNavigator)).EndInit();
            this.pendingNavigator.ResumeLayout(false);
            this.pendingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.approvedBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pendingBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupApproved;
        private System.Windows.Forms.BindingNavigator approvedNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.TextBox approvedId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox approvedQuote;
        private System.Windows.Forms.DateTimePicker approvedTime;
        private System.Windows.Forms.TextBox approvedName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox pendingId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pendingQuote;
        private System.Windows.Forms.DateTimePicker pendingTime;
        private System.Windows.Forms.TextBox pendingName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingNavigator pendingNavigator;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.BindingSource approvedBindingSource;
        private System.Windows.Forms.BindingSource pendingBindingSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton approveButton;


    }
}