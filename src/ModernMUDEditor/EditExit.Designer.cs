namespace ModernMUDEditor
{
    partial class EditExit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblIndexNumber = new System.Windows.Forms.Label();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.lblFlags = new System.Windows.Forms.Label();
            this.lblKeyIndexNumber = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblEditStatus = new System.Windows.Forms.Label();
            this.txtIndexNumber = new System.Windows.Forms.TextBox();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.txtKeyIndexNumber = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtFlags = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnEditFlags = new System.Windows.Forms.Button();
            this.btnFindRoom = new System.Windows.Forms.Button();
            this.rtbTargetRoom = new System.Windows.Forms.RichTextBox();
            this.btnFindKey = new System.Windows.Forms.Button();
            this.rtbKeyName = new System.Windows.Forms.RichTextBox();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewRoom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblIndexNumber
            // 
            this.lblIndexNumber.AutoSize = true;
            this.lblIndexNumber.Location = new System.Drawing.Point(12, 39);
            this.lblIndexNumber.Name = "lblIndexNumber";
            this.lblIndexNumber.Size = new System.Drawing.Size(69, 13);
            this.lblIndexNumber.TabIndex = 0;
            this.lblIndexNumber.Text = "Target Room";
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(12, 92);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(48, 13);
            this.lblKeyword.TabIndex = 1;
            this.lblKeyword.Text = "Keyword";
            // 
            // lblFlags
            // 
            this.lblFlags.AutoSize = true;
            this.lblFlags.Location = new System.Drawing.Point(12, 127);
            this.lblFlags.Name = "lblFlags";
            this.lblFlags.Size = new System.Drawing.Size(32, 13);
            this.lblFlags.TabIndex = 2;
            this.lblFlags.Text = "Flags";
            // 
            // lblKeyIndexNumber
            // 
            this.lblKeyIndexNumber.AutoSize = true;
            this.lblKeyIndexNumber.Location = new System.Drawing.Point(12, 155);
            this.lblKeyIndexNumber.Name = "lblKeyIndexNumber";
            this.lblKeyIndexNumber.Size = new System.Drawing.Size(75, 13);
            this.lblKeyIndexNumber.TabIndex = 3;
            this.lblKeyIndexNumber.Text = "Key (Obj Num)";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 210);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description";
            // 
            // lblEditStatus
            // 
            this.lblEditStatus.AutoSize = true;
            this.lblEditStatus.Location = new System.Drawing.Point(13, 13);
            this.lblEditStatus.Name = "lblEditStatus";
            this.lblEditStatus.Size = new System.Drawing.Size(190, 13);
            this.lblEditStatus.TabIndex = 5;
            this.lblEditStatus.Text = "Currently Editing XXX exit in room XXX.";
            // 
            // txtIndexNumber
            // 
            this.txtIndexNumber.Location = new System.Drawing.Point(86, 36);
            this.txtIndexNumber.Name = "txtIndexNumber";
            this.txtIndexNumber.Size = new System.Drawing.Size(65, 20);
            this.txtIndexNumber.TabIndex = 0;
            this.txtIndexNumber.TextChanged += new System.EventHandler(this.txtIndexNumber_TextChanged);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(86, 89);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(100, 20);
            this.txtKeyword.TabIndex = 2;
            // 
            // txtKeyIndexNumber
            // 
            this.txtKeyIndexNumber.Location = new System.Drawing.Point(86, 152);
            this.txtKeyIndexNumber.Name = "txtKeyIndexNumber";
            this.txtKeyIndexNumber.Size = new System.Drawing.Size(100, 20);
            this.txtKeyIndexNumber.TabIndex = 5;
            this.txtKeyIndexNumber.TextChanged += new System.EventHandler(this.txtKeyIndexNumber_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(86, 210);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(143, 20);
            this.txtDescription.TabIndex = 7;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // txtFlags
            // 
            this.txtFlags.Location = new System.Drawing.Point(86, 119);
            this.txtFlags.Name = "txtFlags";
            this.txtFlags.Size = new System.Drawing.Size(100, 20);
            this.txtFlags.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(90, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(20, 264);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(64, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnEditFlags
            // 
            this.btnEditFlags.Location = new System.Drawing.Point(193, 117);
            this.btnEditFlags.Name = "btnEditFlags";
            this.btnEditFlags.Size = new System.Drawing.Size(39, 23);
            this.btnEditFlags.TabIndex = 4;
            this.btnEditFlags.Text = "Edit";
            this.btnEditFlags.UseVisualStyleBackColor = true;
            this.btnEditFlags.Click += new System.EventHandler(this.btnEditFlags_Click);
            // 
            // btnFindRoom
            // 
            this.btnFindRoom.Location = new System.Drawing.Point(154, 34);
            this.btnFindRoom.Name = "btnFindRoom";
            this.btnFindRoom.Size = new System.Drawing.Size(36, 23);
            this.btnFindRoom.TabIndex = 1;
            this.btnFindRoom.Text = "Find";
            this.btnFindRoom.UseVisualStyleBackColor = true;
            this.btnFindRoom.Click += new System.EventHandler(this.btnFindRoom_Click);
            // 
            // rtbTargetRoom
            // 
            this.rtbTargetRoom.BackColor = System.Drawing.Color.Black;
            this.rtbTargetRoom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbTargetRoom.Location = new System.Drawing.Point(86, 63);
            this.rtbTargetRoom.Multiline = false;
            this.rtbTargetRoom.Name = "rtbTargetRoom";
            this.rtbTargetRoom.ReadOnly = true;
            this.rtbTargetRoom.Size = new System.Drawing.Size(143, 21);
            this.rtbTargetRoom.TabIndex = 40;
            this.rtbTargetRoom.TabStop = false;
            this.rtbTargetRoom.Text = "";
            // 
            // btnFindKey
            // 
            this.btnFindKey.Location = new System.Drawing.Point(193, 150);
            this.btnFindKey.Name = "btnFindKey";
            this.btnFindKey.Size = new System.Drawing.Size(39, 23);
            this.btnFindKey.TabIndex = 6;
            this.btnFindKey.Text = "Find";
            this.btnFindKey.UseVisualStyleBackColor = true;
            this.btnFindKey.Click += new System.EventHandler(this.btnFindKey_Click);
            // 
            // rtbKeyName
            // 
            this.rtbKeyName.BackColor = System.Drawing.Color.Black;
            this.rtbKeyName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbKeyName.Location = new System.Drawing.Point(86, 179);
            this.rtbKeyName.Multiline = false;
            this.rtbKeyName.Name = "rtbKeyName";
            this.rtbKeyName.ReadOnly = true;
            this.rtbKeyName.Size = new System.Drawing.Size(143, 21);
            this.rtbKeyName.TabIndex = 42;
            this.rtbKeyName.TabStop = false;
            this.rtbKeyName.Text = "";
            // 
            // rtbDescription
            // 
            this.rtbDescription.BackColor = System.Drawing.Color.Black;
            this.rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDescription.Location = new System.Drawing.Point(86, 236);
            this.rtbDescription.Multiline = false;
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.ReadOnly = true;
            this.rtbDescription.Size = new System.Drawing.Size(143, 21);
            this.rtbDescription.TabIndex = 43;
            this.rtbDescription.TabStop = false;
            this.rtbDescription.Text = "";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(160, 264);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 23);
            this.btnDelete.TabIndex = 44;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNewRoom
            // 
            this.btnNewRoom.Location = new System.Drawing.Point(193, 34);
            this.btnNewRoom.Name = "btnNewRoom";
            this.btnNewRoom.Size = new System.Drawing.Size(39, 23);
            this.btnNewRoom.TabIndex = 45;
            this.btnNewRoom.Text = "New";
            this.btnNewRoom.UseVisualStyleBackColor = true;
            this.btnNewRoom.Click += new System.EventHandler(this.btnNewRoom_Click);
            // 
            // EditExit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 295);
            this.Controls.Add(this.btnNewRoom);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.rtbDescription);
            this.Controls.Add(this.rtbKeyName);
            this.Controls.Add(this.btnFindKey);
            this.Controls.Add(this.rtbTargetRoom);
            this.Controls.Add(this.btnFindRoom);
            this.Controls.Add(this.btnEditFlags);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtFlags);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtKeyIndexNumber);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.txtIndexNumber);
            this.Controls.Add(this.lblEditStatus);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblKeyIndexNumber);
            this.Controls.Add(this.lblFlags);
            this.Controls.Add(this.lblKeyword);
            this.Controls.Add(this.lblIndexNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditExit";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Exit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIndexNumber;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.Label lblFlags;
        private System.Windows.Forms.Label lblKeyIndexNumber;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblEditStatus;
        private System.Windows.Forms.TextBox txtIndexNumber;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.TextBox txtKeyIndexNumber;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtFlags;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnEditFlags;
        private System.Windows.Forms.Button btnFindRoom;
        private System.Windows.Forms.RichTextBox rtbTargetRoom;
        private System.Windows.Forms.Button btnFindKey;
        private System.Windows.Forms.RichTextBox rtbKeyName;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNewRoom;
    }
}