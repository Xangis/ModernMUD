namespace ModernMUDEditor
{
    partial class EditShops
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
            this.shopList = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFwd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblKeeperIndexNumber = new System.Windows.Forms.Label();
            this.lblBuyPercent = new System.Windows.Forms.Label();
            this.lblSellPercent = new System.Windows.Forms.Label();
            this.txtKeeperIndexNumber = new System.Windows.Forms.TextBox();
            this.txtBuyPercent = new System.Windows.Forms.TextBox();
            this.txtSellPercent = new System.Windows.Forms.TextBox();
            this.lstItemsForSale = new System.Windows.Forms.ListBox();
            this.lstBuyTypes = new System.Windows.Forms.ListBox();
            this.lblItemsForSale = new System.Windows.Forms.Label();
            this.lblBuyTypes = new System.Windows.Forms.Label();
            this.lblOpenHour = new System.Windows.Forms.Label();
            this.lblCloseHour = new System.Windows.Forms.Label();
            this.txtOpenHour = new System.Windows.Forms.TextBox();
            this.txtCloseHour = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnAddSale = new System.Windows.Forms.Button();
            this.btnRemoveSale = new System.Windows.Forms.Button();
            this.btnAddBuy = new System.Windows.Forms.Button();
            this.btnRemoveBuy = new System.Windows.Forms.Button();
            this.rtbKeeperName = new System.Windows.Forms.RichTextBox();
            this.btnFindKeeper = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // shopList
            // 
            this.shopList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shopList.FormattingEnabled = true;
            this.shopList.Location = new System.Drawing.Point(13, 13);
            this.shopList.Name = "shopList";
            this.shopList.Size = new System.Drawing.Size(121, 21);
            this.shopList.TabIndex = 0;
            this.shopList.SelectedIndexChanged += new System.EventHandler(this.shopList_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(145, 13);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(25, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnFwd
            // 
            this.btnFwd.Location = new System.Drawing.Point(176, 13);
            this.btnFwd.Name = "btnFwd";
            this.btnFwd.Size = new System.Drawing.Size(25, 23);
            this.btnFwd.TabIndex = 2;
            this.btnFwd.Text = ">";
            this.btnFwd.UseVisualStyleBackColor = true;
            this.btnFwd.Click += new System.EventHandler(this.btnFwd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(288, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(207, 13);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblKeeperIndexNumber
            // 
            this.lblKeeperIndexNumber.AutoSize = true;
            this.lblKeeperIndexNumber.Location = new System.Drawing.Point(10, 53);
            this.lblKeeperIndexNumber.Name = "lblKeeperIndexNumber";
            this.lblKeeperIndexNumber.Size = new System.Drawing.Size(70, 13);
            this.lblKeeperIndexNumber.TabIndex = 14;
            this.lblKeeperIndexNumber.Text = "Keeper Index";
            // 
            // lblBuyPercent
            // 
            this.lblBuyPercent.AutoSize = true;
            this.lblBuyPercent.Location = new System.Drawing.Point(10, 79);
            this.lblBuyPercent.Name = "lblBuyPercent";
            this.lblBuyPercent.Size = new System.Drawing.Size(65, 13);
            this.lblBuyPercent.TabIndex = 15;
            this.lblBuyPercent.Text = "Buy Percent";
            // 
            // lblSellPercent
            // 
            this.lblSellPercent.AutoSize = true;
            this.lblSellPercent.Location = new System.Drawing.Point(10, 105);
            this.lblSellPercent.Name = "lblSellPercent";
            this.lblSellPercent.Size = new System.Drawing.Size(64, 13);
            this.lblSellPercent.TabIndex = 16;
            this.lblSellPercent.Text = "Sell Percent";
            // 
            // txtKeeperIndexNumber
            // 
            this.txtKeeperIndexNumber.Location = new System.Drawing.Point(91, 50);
            this.txtKeeperIndexNumber.Name = "txtKeeperIndexNumber";
            this.txtKeeperIndexNumber.Size = new System.Drawing.Size(64, 20);
            this.txtKeeperIndexNumber.TabIndex = 5;
            this.txtKeeperIndexNumber.TextChanged += new System.EventHandler(this.txtKeeperIndexNumber_TextChanged);
            // 
            // txtBuyPercent
            // 
            this.txtBuyPercent.Location = new System.Drawing.Point(91, 76);
            this.txtBuyPercent.Name = "txtBuyPercent";
            this.txtBuyPercent.Size = new System.Drawing.Size(100, 20);
            this.txtBuyPercent.TabIndex = 6;
            // 
            // txtSellPercent
            // 
            this.txtSellPercent.Location = new System.Drawing.Point(91, 102);
            this.txtSellPercent.Name = "txtSellPercent";
            this.txtSellPercent.Size = new System.Drawing.Size(100, 20);
            this.txtSellPercent.TabIndex = 7;
            // 
            // lstItemsForSale
            // 
            this.lstItemsForSale.FormattingEnabled = true;
            this.lstItemsForSale.Location = new System.Drawing.Point(34, 146);
            this.lstItemsForSale.Name = "lstItemsForSale";
            this.lstItemsForSale.Size = new System.Drawing.Size(157, 108);
            this.lstItemsForSale.TabIndex = 10;
            // 
            // lstBuyTypes
            // 
            this.lstBuyTypes.FormattingEnabled = true;
            this.lstBuyTypes.Location = new System.Drawing.Point(207, 146);
            this.lstBuyTypes.Name = "lstBuyTypes";
            this.lstBuyTypes.Size = new System.Drawing.Size(156, 108);
            this.lstBuyTypes.TabIndex = 13;
            // 
            // lblItemsForSale
            // 
            this.lblItemsForSale.AutoSize = true;
            this.lblItemsForSale.Location = new System.Drawing.Point(74, 129);
            this.lblItemsForSale.Name = "lblItemsForSale";
            this.lblItemsForSale.Size = new System.Drawing.Size(74, 13);
            this.lblItemsForSale.TabIndex = 23;
            this.lblItemsForSale.Text = "Items For Sale";
            // 
            // lblBuyTypes
            // 
            this.lblBuyTypes.AutoSize = true;
            this.lblBuyTypes.Location = new System.Drawing.Point(253, 129);
            this.lblBuyTypes.Name = "lblBuyTypes";
            this.lblBuyTypes.Size = new System.Drawing.Size(62, 13);
            this.lblBuyTypes.TabIndex = 24;
            this.lblBuyTypes.Text = "Buys Types";
            // 
            // lblOpenHour
            // 
            this.lblOpenHour.AutoSize = true;
            this.lblOpenHour.Location = new System.Drawing.Point(204, 79);
            this.lblOpenHour.Name = "lblOpenHour";
            this.lblOpenHour.Size = new System.Drawing.Size(59, 13);
            this.lblOpenHour.TabIndex = 25;
            this.lblOpenHour.Text = "Open Hour";
            // 
            // lblCloseHour
            // 
            this.lblCloseHour.AutoSize = true;
            this.lblCloseHour.Location = new System.Drawing.Point(204, 105);
            this.lblCloseHour.Name = "lblCloseHour";
            this.lblCloseHour.Size = new System.Drawing.Size(59, 13);
            this.lblCloseHour.TabIndex = 26;
            this.lblCloseHour.Text = "Close Hour";
            // 
            // txtOpenHour
            // 
            this.txtOpenHour.Location = new System.Drawing.Point(269, 76);
            this.txtOpenHour.Name = "txtOpenHour";
            this.txtOpenHour.Size = new System.Drawing.Size(91, 20);
            this.txtOpenHour.TabIndex = 8;
            // 
            // txtCloseHour
            // 
            this.txtCloseHour.Location = new System.Drawing.Point(269, 102);
            this.txtCloseHour.Name = "txtCloseHour";
            this.txtCloseHour.Size = new System.Drawing.Size(91, 20);
            this.txtCloseHour.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(205, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(115, 287);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 16;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnAddSale
            // 
            this.btnAddSale.Location = new System.Drawing.Point(34, 261);
            this.btnAddSale.Name = "btnAddSale";
            this.btnAddSale.Size = new System.Drawing.Size(75, 23);
            this.btnAddSale.TabIndex = 11;
            this.btnAddSale.Text = "Add";
            this.btnAddSale.UseVisualStyleBackColor = true;
            this.btnAddSale.Click += new System.EventHandler(this.btnAddSale_Click);
            // 
            // btnRemoveSale
            // 
            this.btnRemoveSale.Location = new System.Drawing.Point(115, 261);
            this.btnRemoveSale.Name = "btnRemoveSale";
            this.btnRemoveSale.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveSale.TabIndex = 12;
            this.btnRemoveSale.Text = "Remove";
            this.btnRemoveSale.UseVisualStyleBackColor = true;
            this.btnRemoveSale.Click += new System.EventHandler(this.btnRemoveSale_Click);
            // 
            // btnAddBuy
            // 
            this.btnAddBuy.Location = new System.Drawing.Point(205, 260);
            this.btnAddBuy.Name = "btnAddBuy";
            this.btnAddBuy.Size = new System.Drawing.Size(75, 23);
            this.btnAddBuy.TabIndex = 14;
            this.btnAddBuy.Text = "Add";
            this.btnAddBuy.UseVisualStyleBackColor = true;
            this.btnAddBuy.Click += new System.EventHandler(this.btnAddBuy_Click);
            // 
            // btnRemoveBuy
            // 
            this.btnRemoveBuy.Location = new System.Drawing.Point(286, 261);
            this.btnRemoveBuy.Name = "btnRemoveBuy";
            this.btnRemoveBuy.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveBuy.TabIndex = 15;
            this.btnRemoveBuy.Text = "Remove";
            this.btnRemoveBuy.UseVisualStyleBackColor = true;
            this.btnRemoveBuy.Click += new System.EventHandler(this.btnRemoveBuy_Click);
            // 
            // rtbKeeperName
            // 
            this.rtbKeeperName.BackColor = System.Drawing.Color.Black;
            this.rtbKeeperName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbKeeperName.DetectUrls = false;
            this.rtbKeeperName.Location = new System.Drawing.Point(204, 50);
            this.rtbKeeperName.Multiline = false;
            this.rtbKeeperName.Name = "rtbKeeperName";
            this.rtbKeeperName.ReadOnly = true;
            this.rtbKeeperName.Size = new System.Drawing.Size(156, 20);
            this.rtbKeeperName.TabIndex = 42;
            this.rtbKeeperName.TabStop = false;
            this.rtbKeeperName.Text = "";
            this.rtbKeeperName.WordWrap = false;
            // 
            // btnFindKeeper
            // 
            this.btnFindKeeper.Location = new System.Drawing.Point(161, 48);
            this.btnFindKeeper.Name = "btnFindKeeper";
            this.btnFindKeeper.Size = new System.Drawing.Size(36, 23);
            this.btnFindKeeper.TabIndex = 43;
            this.btnFindKeeper.Text = "Find";
            this.btnFindKeeper.UseVisualStyleBackColor = true;
            this.btnFindKeeper.Click += new System.EventHandler(this.btnFindKeeper_Click);
            // 
            // EditShops
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 322);
            this.Controls.Add(this.btnFindKeeper);
            this.Controls.Add(this.rtbKeeperName);
            this.Controls.Add(this.btnRemoveBuy);
            this.Controls.Add(this.btnAddBuy);
            this.Controls.Add(this.btnRemoveSale);
            this.Controls.Add(this.btnAddSale);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtCloseHour);
            this.Controls.Add(this.txtOpenHour);
            this.Controls.Add(this.lblCloseHour);
            this.Controls.Add(this.lblOpenHour);
            this.Controls.Add(this.lblBuyTypes);
            this.Controls.Add(this.lblItemsForSale);
            this.Controls.Add(this.lstBuyTypes);
            this.Controls.Add(this.lstItemsForSale);
            this.Controls.Add(this.txtSellPercent);
            this.Controls.Add(this.txtBuyPercent);
            this.Controls.Add(this.txtKeeperIndexNumber);
            this.Controls.Add(this.lblSellPercent);
            this.Controls.Add(this.lblBuyPercent);
            this.Controls.Add(this.lblKeeperIndexNumber);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnFwd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.shopList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditShops";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Shops";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox shopList;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFwd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblKeeperIndexNumber;
        private System.Windows.Forms.Label lblBuyPercent;
        private System.Windows.Forms.Label lblSellPercent;
        private System.Windows.Forms.TextBox txtKeeperIndexNumber;
        private System.Windows.Forms.TextBox txtBuyPercent;
        private System.Windows.Forms.TextBox txtSellPercent;
        private System.Windows.Forms.ListBox lstItemsForSale;
        private System.Windows.Forms.ListBox lstBuyTypes;
        private System.Windows.Forms.Label lblItemsForSale;
        private System.Windows.Forms.Label lblBuyTypes;
        private System.Windows.Forms.Label lblOpenHour;
        private System.Windows.Forms.Label lblCloseHour;
        private System.Windows.Forms.TextBox txtOpenHour;
        private System.Windows.Forms.TextBox txtCloseHour;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnAddSale;
        private System.Windows.Forms.Button btnRemoveSale;
        private System.Windows.Forms.Button btnAddBuy;
        private System.Windows.Forms.Button btnRemoveBuy;
        private System.Windows.Forms.RichTextBox rtbKeeperName;
        private System.Windows.Forms.Button btnFindKeeper;
    }
}