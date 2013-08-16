namespace ModernMUDEditor
{
    partial class EditQuests
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
            this.questList = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFwd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblIndexNumber = new System.Windows.Forms.Label();
            this.txtIndexNumber = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.lstMessages = new System.Windows.Forms.ListBox();
            this.lstQuests = new System.Windows.Forms.ListBox();
            this.btnAddMessage = new System.Windows.Forms.Button();
            this.btnRemoveMessage = new System.Windows.Forms.Button();
            this.btnAddQuest = new System.Windows.Forms.Button();
            this.btnRemoveQuest = new System.Windows.Forms.Button();
            this.btnEditMessage = new System.Windows.Forms.Button();
            this.btnEditQuest = new System.Windows.Forms.Button();
            this.lblQuests = new System.Windows.Forms.Label();
            this.lblMessages = new System.Windows.Forms.Label();
            this.rtbMobName = new System.Windows.Forms.RichTextBox();
            this.btnFindIndexNumber = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // questList
            // 
            this.questList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.questList.FormattingEnabled = true;
            this.questList.Location = new System.Drawing.Point(13, 13);
            this.questList.Name = "questList";
            this.questList.Size = new System.Drawing.Size(121, 21);
            this.questList.TabIndex = 0;
            this.questList.SelectedIndexChanged += new System.EventHandler(this.questList_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(144, 13);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(25, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnFwd
            // 
            this.btnFwd.Location = new System.Drawing.Point(175, 13);
            this.btnFwd.Name = "btnFwd";
            this.btnFwd.Size = new System.Drawing.Size(25, 23);
            this.btnFwd.TabIndex = 2;
            this.btnFwd.Text = ">";
            this.btnFwd.UseVisualStyleBackColor = true;
            this.btnFwd.Click += new System.EventHandler(this.btnFwd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(287, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(206, 13);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblIndexNumber
            // 
            this.lblIndexNumber.AutoSize = true;
            this.lblIndexNumber.Location = new System.Drawing.Point(10, 45);
            this.lblIndexNumber.Name = "lblIndexNumber";
            this.lblIndexNumber.Size = new System.Drawing.Size(43, 13);
            this.lblIndexNumber.TabIndex = 14;
            this.lblIndexNumber.Text = "Index #";
            // 
            // txtIndexNumber
            // 
            this.txtIndexNumber.Location = new System.Drawing.Point(54, 42);
            this.txtIndexNumber.Name = "txtIndexNumber";
            this.txtIndexNumber.Size = new System.Drawing.Size(86, 20);
            this.txtIndexNumber.TabIndex = 5;
            this.txtIndexNumber.TextChanged += new System.EventHandler(this.txtIndexNumber_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(185, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(104, 310);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 14;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lstMessages
            // 
            this.lstMessages.FormattingEnabled = true;
            this.lstMessages.Location = new System.Drawing.Point(13, 82);
            this.lstMessages.MultiColumn = true;
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(341, 69);
            this.lstMessages.Sorted = true;
            this.lstMessages.TabIndex = 6;
            // 
            // lstQuests
            // 
            this.lstQuests.FormattingEnabled = true;
            this.lstQuests.Location = new System.Drawing.Point(13, 207);
            this.lstQuests.Name = "lstQuests";
            this.lstQuests.Size = new System.Drawing.Size(339, 69);
            this.lstQuests.TabIndex = 10;
            // 
            // btnAddMessage
            // 
            this.btnAddMessage.Location = new System.Drawing.Point(65, 157);
            this.btnAddMessage.Name = "btnAddMessage";
            this.btnAddMessage.Size = new System.Drawing.Size(75, 23);
            this.btnAddMessage.TabIndex = 7;
            this.btnAddMessage.Text = "Add";
            this.btnAddMessage.UseVisualStyleBackColor = true;
            this.btnAddMessage.Click += new System.EventHandler(this.btnAddMessage_Click);
            // 
            // btnRemoveMessage
            // 
            this.btnRemoveMessage.Location = new System.Drawing.Point(227, 157);
            this.btnRemoveMessage.Name = "btnRemoveMessage";
            this.btnRemoveMessage.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveMessage.TabIndex = 9;
            this.btnRemoveMessage.Text = "Remove";
            this.btnRemoveMessage.UseVisualStyleBackColor = true;
            this.btnRemoveMessage.Click += new System.EventHandler(this.btnRemoveMessage_Click);
            // 
            // btnAddQuest
            // 
            this.btnAddQuest.Location = new System.Drawing.Point(65, 281);
            this.btnAddQuest.Name = "btnAddQuest";
            this.btnAddQuest.Size = new System.Drawing.Size(75, 23);
            this.btnAddQuest.TabIndex = 11;
            this.btnAddQuest.Text = "Add";
            this.btnAddQuest.UseVisualStyleBackColor = true;
            this.btnAddQuest.Click += new System.EventHandler(this.btnAddQuest_Click);
            // 
            // btnRemoveQuest
            // 
            this.btnRemoveQuest.Location = new System.Drawing.Point(227, 282);
            this.btnRemoveQuest.Name = "btnRemoveQuest";
            this.btnRemoveQuest.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveQuest.TabIndex = 13;
            this.btnRemoveQuest.Text = "Remove";
            this.btnRemoveQuest.UseVisualStyleBackColor = true;
            this.btnRemoveQuest.Click += new System.EventHandler(this.btnRemoveQuest_Click);
            // 
            // btnEditMessage
            // 
            this.btnEditMessage.Location = new System.Drawing.Point(146, 157);
            this.btnEditMessage.Name = "btnEditMessage";
            this.btnEditMessage.Size = new System.Drawing.Size(75, 23);
            this.btnEditMessage.TabIndex = 8;
            this.btnEditMessage.Text = "Edit";
            this.btnEditMessage.UseVisualStyleBackColor = true;
            this.btnEditMessage.Click += new System.EventHandler(this.btnEditMessage_Click);
            // 
            // btnEditQuest
            // 
            this.btnEditQuest.Location = new System.Drawing.Point(146, 282);
            this.btnEditQuest.Name = "btnEditQuest";
            this.btnEditQuest.Size = new System.Drawing.Size(75, 23);
            this.btnEditQuest.TabIndex = 12;
            this.btnEditQuest.Text = "Edit";
            this.btnEditQuest.UseVisualStyleBackColor = true;
            this.btnEditQuest.Click += new System.EventHandler(this.btnEditQuest_Click);
            // 
            // lblQuests
            // 
            this.lblQuests.AutoSize = true;
            this.lblQuests.Location = new System.Drawing.Point(141, 186);
            this.lblQuests.Name = "lblQuests";
            this.lblQuests.Size = new System.Drawing.Size(88, 13);
            this.lblQuests.TabIndex = 46;
            this.lblQuests.Text = "Individual Quests";
            // 
            // lblMessages
            // 
            this.lblMessages.AutoSize = true;
            this.lblMessages.Location = new System.Drawing.Point(155, 64);
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(55, 13);
            this.lblMessages.TabIndex = 47;
            this.lblMessages.Text = "Messages";
            // 
            // rtbMobName
            // 
            this.rtbMobName.BackColor = System.Drawing.Color.Black;
            this.rtbMobName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbMobName.Location = new System.Drawing.Point(188, 41);
            this.rtbMobName.Multiline = false;
            this.rtbMobName.Name = "rtbMobName";
            this.rtbMobName.ReadOnly = true;
            this.rtbMobName.Size = new System.Drawing.Size(166, 20);
            this.rtbMobName.TabIndex = 48;
            this.rtbMobName.TabStop = false;
            this.rtbMobName.Text = "";
            // 
            // btnFindIndexNumber
            // 
            this.btnFindIndexNumber.Location = new System.Drawing.Point(146, 40);
            this.btnFindIndexNumber.Name = "btnFindIndexNumber";
            this.btnFindIndexNumber.Size = new System.Drawing.Size(36, 23);
            this.btnFindIndexNumber.TabIndex = 49;
            this.btnFindIndexNumber.Text = "Find";
            this.btnFindIndexNumber.UseVisualStyleBackColor = true;
            this.btnFindIndexNumber.Click += new System.EventHandler(this.btnFindIndexNumber_Click);
            // 
            // EditQuests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 338);
            this.Controls.Add(this.btnFindIndexNumber);
            this.Controls.Add(this.rtbMobName);
            this.Controls.Add(this.lblMessages);
            this.Controls.Add(this.lblQuests);
            this.Controls.Add(this.btnEditQuest);
            this.Controls.Add(this.btnEditMessage);
            this.Controls.Add(this.btnRemoveQuest);
            this.Controls.Add(this.btnAddQuest);
            this.Controls.Add(this.btnRemoveMessage);
            this.Controls.Add(this.btnAddMessage);
            this.Controls.Add(this.lstQuests);
            this.Controls.Add(this.lstMessages);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtIndexNumber);
            this.Controls.Add(this.lblIndexNumber);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnFwd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.questList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditQuests";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Quests";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox questList;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFwd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblIndexNumber;
        private System.Windows.Forms.TextBox txtIndexNumber;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ListBox lstMessages;
        private System.Windows.Forms.ListBox lstQuests;
        private System.Windows.Forms.Button btnAddMessage;
        private System.Windows.Forms.Button btnRemoveMessage;
        private System.Windows.Forms.Button btnAddQuest;
        private System.Windows.Forms.Button btnRemoveQuest;
        private System.Windows.Forms.Button btnEditMessage;
        private System.Windows.Forms.Button btnEditQuest;
        private System.Windows.Forms.Label lblQuests;
        private System.Windows.Forms.Label lblMessages;
        private System.Windows.Forms.RichTextBox rtbMobName;
        private System.Windows.Forms.Button btnFindIndexNumber;
    }
}