namespace ModernMUDEditor
{
    partial class EditQuestData
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
            this.btnRemoveReward = new System.Windows.Forms.Button();
            this.btnAddReward = new System.Windows.Forms.Button();
            this.btnRemoveWanted = new System.Windows.Forms.Button();
            this.btnAddWanted = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblRewards = new System.Windows.Forms.Label();
            this.lblItemsWanted = new System.Windows.Forms.Label();
            this.lstRewards = new System.Windows.Forms.ListBox();
            this.lstItemsWanted = new System.Windows.Forms.ListBox();
            this.txtCompleteMsg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDisappearMsg = new System.Windows.Forms.Label();
            this.txtDisappearMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnRemoveReward
            // 
            this.btnRemoveReward.Location = new System.Drawing.Point(275, 146);
            this.btnRemoveReward.Name = "btnRemoveReward";
            this.btnRemoveReward.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveReward.TabIndex = 5;
            this.btnRemoveReward.Text = "Remove";
            this.btnRemoveReward.UseVisualStyleBackColor = true;
            this.btnRemoveReward.Click += new System.EventHandler(this.btnRemoveReward_Click);
            // 
            // btnAddReward
            // 
            this.btnAddReward.Location = new System.Drawing.Point(194, 145);
            this.btnAddReward.Name = "btnAddReward";
            this.btnAddReward.Size = new System.Drawing.Size(75, 23);
            this.btnAddReward.TabIndex = 4;
            this.btnAddReward.Text = "Add";
            this.btnAddReward.UseVisualStyleBackColor = true;
            this.btnAddReward.Click += new System.EventHandler(this.btnAddReward_Click);
            // 
            // btnRemoveWanted
            // 
            this.btnRemoveWanted.Location = new System.Drawing.Point(104, 146);
            this.btnRemoveWanted.Name = "btnRemoveWanted";
            this.btnRemoveWanted.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveWanted.TabIndex = 2;
            this.btnRemoveWanted.Text = "Remove";
            this.btnRemoveWanted.UseVisualStyleBackColor = true;
            this.btnRemoveWanted.Click += new System.EventHandler(this.btnRemoveWanted_Click);
            // 
            // btnAddWanted
            // 
            this.btnAddWanted.Location = new System.Drawing.Point(23, 146);
            this.btnAddWanted.Name = "btnAddWanted";
            this.btnAddWanted.Size = new System.Drawing.Size(75, 23);
            this.btnAddWanted.TabIndex = 1;
            this.btnAddWanted.Text = "Add";
            this.btnAddWanted.UseVisualStyleBackColor = true;
            this.btnAddWanted.Click += new System.EventHandler(this.btnAddWanted_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(194, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(104, 305);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblRewards
            // 
            this.lblRewards.AutoSize = true;
            this.lblRewards.Location = new System.Drawing.Point(248, 15);
            this.lblRewards.Name = "lblRewards";
            this.lblRewards.Size = new System.Drawing.Size(49, 13);
            this.lblRewards.TabIndex = 45;
            this.lblRewards.Text = "Rewards";
            // 
            // lblItemsWanted
            // 
            this.lblItemsWanted.AutoSize = true;
            this.lblItemsWanted.Location = new System.Drawing.Point(61, 15);
            this.lblItemsWanted.Name = "lblItemsWanted";
            this.lblItemsWanted.Size = new System.Drawing.Size(73, 13);
            this.lblItemsWanted.TabIndex = 44;
            this.lblItemsWanted.Text = "Items Wanted";
            // 
            // lstRewards
            // 
            this.lstRewards.FormattingEnabled = true;
            this.lstRewards.Location = new System.Drawing.Point(194, 32);
            this.lstRewards.Name = "lstRewards";
            this.lstRewards.Size = new System.Drawing.Size(156, 108);
            this.lstRewards.TabIndex = 3;
            // 
            // lstItemsWanted
            // 
            this.lstItemsWanted.FormattingEnabled = true;
            this.lstItemsWanted.Location = new System.Drawing.Point(21, 32);
            this.lstItemsWanted.Name = "lstItemsWanted";
            this.lstItemsWanted.Size = new System.Drawing.Size(157, 108);
            this.lstItemsWanted.TabIndex = 0;
            // 
            // txtCompleteMsg
            // 
            this.txtCompleteMsg.Location = new System.Drawing.Point(21, 187);
            this.txtCompleteMsg.Multiline = true;
            this.txtCompleteMsg.Name = "txtCompleteMsg";
            this.txtCompleteMsg.Size = new System.Drawing.Size(329, 51);
            this.txtCompleteMsg.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Completion Message";
            // 
            // lblDisappearMsg
            // 
            this.lblDisappearMsg.AutoSize = true;
            this.lblDisappearMsg.Location = new System.Drawing.Point(35, 241);
            this.lblDisappearMsg.Name = "lblDisappearMsg";
            this.lblDisappearMsg.Size = new System.Drawing.Size(292, 13);
            this.lblDisappearMsg.TabIndex = 55;
            this.lblDisappearMsg.Text = "Disappearance Message (Leave Blank if Doesn\'t Disappear)";
            // 
            // txtDisappearMsg
            // 
            this.txtDisappearMsg.Location = new System.Drawing.Point(21, 257);
            this.txtDisappearMsg.Multiline = true;
            this.txtDisappearMsg.Name = "txtDisappearMsg";
            this.txtDisappearMsg.Size = new System.Drawing.Size(329, 43);
            this.txtDisappearMsg.TabIndex = 7;
            // 
            // EditQuestData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 339);
            this.Controls.Add(this.lblDisappearMsg);
            this.Controls.Add(this.txtDisappearMsg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCompleteMsg);
            this.Controls.Add(this.btnRemoveReward);
            this.Controls.Add(this.btnAddReward);
            this.Controls.Add(this.btnRemoveWanted);
            this.Controls.Add(this.btnAddWanted);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblRewards);
            this.Controls.Add(this.lblItemsWanted);
            this.Controls.Add(this.lstRewards);
            this.Controls.Add(this.lstItemsWanted);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditQuestData";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Quest Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRemoveReward;
        private System.Windows.Forms.Button btnAddReward;
        private System.Windows.Forms.Button btnRemoveWanted;
        private System.Windows.Forms.Button btnAddWanted;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblRewards;
        private System.Windows.Forms.Label lblItemsWanted;
        private System.Windows.Forms.ListBox lstRewards;
        private System.Windows.Forms.ListBox lstItemsWanted;
        private System.Windows.Forms.TextBox txtCompleteMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDisappearMsg;
        private System.Windows.Forms.TextBox txtDisappearMsg;
    }
}