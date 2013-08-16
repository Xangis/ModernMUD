namespace ModernMUDEditor
{
    partial class EditSpecial
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
            this.btnRemoveSpecial = new System.Windows.Forms.Button();
            this.btnAddSpecial = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblItemsWanted = new System.Windows.Forms.Label();
            this.lstSpecials = new System.Windows.Forms.ListBox();
            this.cbSpecialList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnRemoveSpecial
            // 
            this.btnRemoveSpecial.Location = new System.Drawing.Point(103, 177);
            this.btnRemoveSpecial.Name = "btnRemoveSpecial";
            this.btnRemoveSpecial.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveSpecial.TabIndex = 3;
            this.btnRemoveSpecial.Text = "Remove";
            this.btnRemoveSpecial.UseVisualStyleBackColor = true;
            this.btnRemoveSpecial.Click += new System.EventHandler(this.btnRemoveSpecial_Click);
            // 
            // btnAddSpecial
            // 
            this.btnAddSpecial.Location = new System.Drawing.Point(22, 177);
            this.btnAddSpecial.Name = "btnAddSpecial";
            this.btnAddSpecial.Size = new System.Drawing.Size(75, 23);
            this.btnAddSpecial.TabIndex = 2;
            this.btnAddSpecial.Text = "Add";
            this.btnAddSpecial.UseVisualStyleBackColor = true;
            this.btnAddSpecial.Click += new System.EventHandler(this.btnAddSpecial_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(103, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(22, 205);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblItemsWanted
            // 
            this.lblItemsWanted.AutoSize = true;
            this.lblItemsWanted.Location = new System.Drawing.Point(58, 16);
            this.lblItemsWanted.Name = "lblItemsWanted";
            this.lblItemsWanted.Size = new System.Drawing.Size(80, 13);
            this.lblItemsWanted.TabIndex = 44;
            this.lblItemsWanted.Text = "Special Abilities";
            // 
            // lstSpecials
            // 
            this.lstSpecials.FormattingEnabled = true;
            this.lstSpecials.Location = new System.Drawing.Point(12, 32);
            this.lstSpecials.Name = "lstSpecials";
            this.lstSpecials.Size = new System.Drawing.Size(171, 108);
            this.lstSpecials.TabIndex = 0;
            // 
            // cbSpecialList
            // 
            this.cbSpecialList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSpecialList.FormattingEnabled = true;
            this.cbSpecialList.Location = new System.Drawing.Point(13, 147);
            this.cbSpecialList.Name = "cbSpecialList";
            this.cbSpecialList.Size = new System.Drawing.Size(170, 21);
            this.cbSpecialList.TabIndex = 1;
            // 
            // EditSpecial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 233);
            this.Controls.Add(this.cbSpecialList);
            this.Controls.Add(this.btnRemoveSpecial);
            this.Controls.Add(this.btnAddSpecial);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblItemsWanted);
            this.Controls.Add(this.lstSpecials);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditSpecial";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Special Functions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRemoveSpecial;
        private System.Windows.Forms.Button btnAddSpecial;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblItemsWanted;
        private System.Windows.Forms.ListBox lstSpecials;
        private System.Windows.Forms.ComboBox cbSpecialList;
    }
}