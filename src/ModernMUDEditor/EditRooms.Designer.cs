namespace ModernMUDEditor
{
    partial class EditRooms
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
            this.roomList = new System.Windows.Forms.ComboBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnFwd = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblIndexNumber = new System.Windows.Forms.Label();
            this.txtIndexNumber = new System.Windows.Forms.TextBox();
            this.lblTerrain = new System.Windows.Forms.Label();
            this.cbTerrain = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblFlags = new System.Windows.Forms.Label();
            this.txtFlags = new System.Windows.Forms.TextBox();
            this.txtFlags2 = new System.Windows.Forms.TextBox();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblCurrentDir = new System.Windows.Forms.Label();
            this.lblFallChance = new System.Windows.Forms.Label();
            this.lblMapSector = new System.Windows.Forms.Label();
            this.cbCurrentDir = new System.Windows.Forms.ComboBox();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.txtFallChance = new System.Windows.Forms.TextBox();
            this.lblExits = new System.Windows.Forms.Label();
            this.btnn = new System.Windows.Forms.Button();
            this.btnnw = new System.Windows.Forms.Button();
            this.btnw = new System.Windows.Forms.Button();
            this.btnsw = new System.Windows.Forms.Button();
            this.btnne = new System.Windows.Forms.Button();
            this.btne = new System.Windows.Forms.Button();
            this.btnse = new System.Windows.Forms.Button();
            this.btns = new System.Windows.Forms.Button();
            this.btnup = new System.Windows.Forms.Button();
            this.btndn = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.rtbName = new System.Windows.Forms.RichTextBox();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.btnEditFlags1 = new System.Windows.Forms.Button();
            this.btnEditFlags2 = new System.Windows.Forms.Button();
            this.lstExtraDesc = new System.Windows.Forms.ListBox();
            this.lblExtraDesc = new System.Windows.Forms.Label();
            this.btnAddExtraDesc = new System.Windows.Forms.Button();
            this.btnRemoveExtraDesc = new System.Windows.Forms.Button();
            this.btnEditExtraDescr = new System.Windows.Forms.Button();
            this.btnClone = new System.Windows.Forms.Button();
            this.cbMapSector = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // roomList
            // 
            this.roomList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roomList.FormattingEnabled = true;
            this.roomList.Location = new System.Drawing.Point(13, 12);
            this.roomList.Name = "roomList";
            this.roomList.Size = new System.Drawing.Size(241, 21);
            this.roomList.TabIndex = 0;
            this.roomList.SelectedIndexChanged += new System.EventHandler(this.roomList_SelectedIndexChanged);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(327, 9);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(327, 37);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnFwd
            // 
            this.btnFwd.Location = new System.Drawing.Point(290, 10);
            this.btnFwd.Name = "btnFwd";
            this.btnFwd.Size = new System.Drawing.Size(25, 23);
            this.btnFwd.TabIndex = 2;
            this.btnFwd.Text = ">";
            this.btnFwd.UseVisualStyleBackColor = true;
            this.btnFwd.Click += new System.EventHandler(this.btnFwd_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(260, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(25, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblIndexNumber
            // 
            this.lblIndexNumber.AutoSize = true;
            this.lblIndexNumber.Location = new System.Drawing.Point(10, 244);
            this.lblIndexNumber.Name = "lblIndexNumber";
            this.lblIndexNumber.Size = new System.Drawing.Size(43, 13);
            this.lblIndexNumber.TabIndex = 6;
            this.lblIndexNumber.Text = "Index #";
            // 
            // txtIndexNumber
            // 
            this.txtIndexNumber.Location = new System.Drawing.Point(76, 242);
            this.txtIndexNumber.Name = "txtIndexNumber";
            this.txtIndexNumber.Size = new System.Drawing.Size(100, 20);
            this.txtIndexNumber.TabIndex = 7;
            // 
            // lblTerrain
            // 
            this.lblTerrain.AutoSize = true;
            this.lblTerrain.Location = new System.Drawing.Point(10, 271);
            this.lblTerrain.Name = "lblTerrain";
            this.lblTerrain.Size = new System.Drawing.Size(40, 13);
            this.lblTerrain.TabIndex = 8;
            this.lblTerrain.Text = "Terrain";
            // 
            // cbTerrain
            // 
            this.cbTerrain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTerrain.FormattingEnabled = true;
            this.cbTerrain.Location = new System.Drawing.Point(76, 268);
            this.cbTerrain.Name = "cbTerrain";
            this.cbTerrain.Size = new System.Drawing.Size(121, 21);
            this.cbTerrain.Sorted = true;
            this.cbTerrain.TabIndex = 8;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(76, 39);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(245, 20);
            this.txtName.TabIndex = 5;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(10, 42);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "Name";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 91);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 12;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.Location = new System.Drawing.Point(76, 91);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(326, 71);
            this.txtDescription.TabIndex = 6;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // lblFlags
            // 
            this.lblFlags.AutoSize = true;
            this.lblFlags.Location = new System.Drawing.Point(10, 301);
            this.lblFlags.Name = "lblFlags";
            this.lblFlags.Size = new System.Drawing.Size(32, 13);
            this.lblFlags.TabIndex = 14;
            this.lblFlags.Text = "Flags";
            // 
            // txtFlags
            // 
            this.txtFlags.Location = new System.Drawing.Point(76, 298);
            this.txtFlags.Name = "txtFlags";
            this.txtFlags.Size = new System.Drawing.Size(100, 20);
            this.txtFlags.TabIndex = 9;
            // 
            // txtFlags2
            // 
            this.txtFlags2.Location = new System.Drawing.Point(76, 324);
            this.txtFlags2.Name = "txtFlags2";
            this.txtFlags2.Size = new System.Drawing.Size(100, 20);
            this.txtFlags2.TabIndex = 11;
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(10, 359);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(41, 13);
            this.lblCurrent.TabIndex = 17;
            this.lblCurrent.Text = "Current";
            // 
            // lblCurrentDir
            // 
            this.lblCurrentDir.AutoSize = true;
            this.lblCurrentDir.Location = new System.Drawing.Point(10, 388);
            this.lblCurrentDir.Name = "lblCurrentDir";
            this.lblCurrentDir.Size = new System.Drawing.Size(57, 13);
            this.lblCurrentDir.TabIndex = 18;
            this.lblCurrentDir.Text = "Current Dir";
            // 
            // lblFallChance
            // 
            this.lblFallChance.AutoSize = true;
            this.lblFallChance.Location = new System.Drawing.Point(10, 417);
            this.lblFallChance.Name = "lblFallChance";
            this.lblFallChance.Size = new System.Drawing.Size(63, 13);
            this.lblFallChance.TabIndex = 19;
            this.lblFallChance.Text = "Fall Chance";
            // 
            // lblMapSector
            // 
            this.lblMapSector.AutoSize = true;
            this.lblMapSector.Location = new System.Drawing.Point(10, 444);
            this.lblMapSector.Name = "lblMapSector";
            this.lblMapSector.Size = new System.Drawing.Size(62, 13);
            this.lblMapSector.TabIndex = 20;
            this.lblMapSector.Text = "Map Sector";
            // 
            // cbCurrentDir
            // 
            this.cbCurrentDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrentDir.FormattingEnabled = true;
            this.cbCurrentDir.Location = new System.Drawing.Point(76, 385);
            this.cbCurrentDir.Name = "cbCurrentDir";
            this.cbCurrentDir.Size = new System.Drawing.Size(121, 21);
            this.cbCurrentDir.TabIndex = 14;
            // 
            // txtCurrent
            // 
            this.txtCurrent.Location = new System.Drawing.Point(76, 356);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new System.Drawing.Size(100, 20);
            this.txtCurrent.TabIndex = 13;
            // 
            // txtFallChance
            // 
            this.txtFallChance.Location = new System.Drawing.Point(76, 414);
            this.txtFallChance.Name = "txtFallChance";
            this.txtFallChance.Size = new System.Drawing.Size(100, 20);
            this.txtFallChance.TabIndex = 15;
            // 
            // lblExits
            // 
            this.lblExits.AutoSize = true;
            this.lblExits.Location = new System.Drawing.Point(286, 416);
            this.lblExits.Name = "lblExits";
            this.lblExits.Size = new System.Drawing.Size(29, 13);
            this.lblExits.TabIndex = 25;
            this.lblExits.Text = "Exits";
            // 
            // btnn
            // 
            this.btnn.Location = new System.Drawing.Point(286, 380);
            this.btnn.Name = "btnn";
            this.btnn.Size = new System.Drawing.Size(29, 23);
            this.btnn.TabIndex = 22;
            this.btnn.Text = "n";
            this.btnn.UseVisualStyleBackColor = true;
            this.btnn.Click += new System.EventHandler(this.btnn_Click);
            // 
            // btnnw
            // 
            this.btnnw.Location = new System.Drawing.Point(253, 380);
            this.btnnw.Name = "btnnw";
            this.btnnw.Size = new System.Drawing.Size(29, 23);
            this.btnnw.TabIndex = 21;
            this.btnnw.Text = "nw";
            this.btnnw.UseVisualStyleBackColor = true;
            this.btnnw.Click += new System.EventHandler(this.btnnw_Click);
            // 
            // btnw
            // 
            this.btnw.Location = new System.Drawing.Point(253, 411);
            this.btnw.Name = "btnw";
            this.btnw.Size = new System.Drawing.Size(29, 23);
            this.btnw.TabIndex = 25;
            this.btnw.Text = "w";
            this.btnw.UseVisualStyleBackColor = true;
            this.btnw.Click += new System.EventHandler(this.btnw_Click);
            // 
            // btnsw
            // 
            this.btnsw.Location = new System.Drawing.Point(253, 441);
            this.btnsw.Name = "btnsw";
            this.btnsw.Size = new System.Drawing.Size(29, 23);
            this.btnsw.TabIndex = 28;
            this.btnsw.Text = "sw";
            this.btnsw.UseVisualStyleBackColor = true;
            this.btnsw.Click += new System.EventHandler(this.btnsw_Click);
            // 
            // btnne
            // 
            this.btnne.Location = new System.Drawing.Point(319, 380);
            this.btnne.Name = "btnne";
            this.btnne.Size = new System.Drawing.Size(29, 23);
            this.btnne.TabIndex = 23;
            this.btnne.Text = "ne";
            this.btnne.UseVisualStyleBackColor = true;
            this.btnne.Click += new System.EventHandler(this.btnne_Click);
            // 
            // btne
            // 
            this.btne.Location = new System.Drawing.Point(319, 411);
            this.btne.Name = "btne";
            this.btne.Size = new System.Drawing.Size(29, 23);
            this.btne.TabIndex = 26;
            this.btne.Text = "e";
            this.btne.UseVisualStyleBackColor = true;
            this.btne.Click += new System.EventHandler(this.btne_Click);
            // 
            // btnse
            // 
            this.btnse.Location = new System.Drawing.Point(319, 441);
            this.btnse.Name = "btnse";
            this.btnse.Size = new System.Drawing.Size(29, 23);
            this.btnse.TabIndex = 30;
            this.btnse.Text = "se";
            this.btnse.UseVisualStyleBackColor = true;
            this.btnse.Click += new System.EventHandler(this.btnse_Click);
            // 
            // btns
            // 
            this.btns.Location = new System.Drawing.Point(287, 441);
            this.btns.Name = "btns";
            this.btns.Size = new System.Drawing.Size(29, 23);
            this.btns.TabIndex = 29;
            this.btns.Text = "s";
            this.btns.UseVisualStyleBackColor = true;
            this.btns.Click += new System.EventHandler(this.btns_Click);
            // 
            // btnup
            // 
            this.btnup.Location = new System.Drawing.Point(352, 380);
            this.btnup.Name = "btnup";
            this.btnup.Size = new System.Drawing.Size(29, 23);
            this.btnup.TabIndex = 24;
            this.btnup.Text = "up";
            this.btnup.UseVisualStyleBackColor = true;
            this.btnup.Click += new System.EventHandler(this.btnup_Click);
            // 
            // btndn
            // 
            this.btndn.Location = new System.Drawing.Point(352, 412);
            this.btndn.Name = "btndn";
            this.btndn.Size = new System.Drawing.Size(29, 23);
            this.btndn.TabIndex = 27;
            this.btndn.Text = "dn";
            this.btndn.UseVisualStyleBackColor = true;
            this.btndn.Click += new System.EventHandler(this.btndn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(207, 470);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(126, 470);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 31;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // rtbName
            // 
            this.rtbName.BackColor = System.Drawing.Color.Black;
            this.rtbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbName.DetectUrls = false;
            this.rtbName.Location = new System.Drawing.Point(76, 65);
            this.rtbName.Multiline = false;
            this.rtbName.Name = "rtbName";
            this.rtbName.ReadOnly = true;
            this.rtbName.Size = new System.Drawing.Size(245, 20);
            this.rtbName.TabIndex = 39;
            this.rtbName.TabStop = false;
            this.rtbName.Text = "";
            this.rtbName.WordWrap = false;
            // 
            // rtbDescription
            // 
            this.rtbDescription.BackColor = System.Drawing.Color.Black;
            this.rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDescription.DetectUrls = false;
            this.rtbDescription.Location = new System.Drawing.Point(76, 168);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.ReadOnly = true;
            this.rtbDescription.Size = new System.Drawing.Size(326, 66);
            this.rtbDescription.TabIndex = 40;
            this.rtbDescription.TabStop = false;
            this.rtbDescription.Text = "";
            // 
            // btnEditFlags1
            // 
            this.btnEditFlags1.Location = new System.Drawing.Point(182, 296);
            this.btnEditFlags1.Name = "btnEditFlags1";
            this.btnEditFlags1.Size = new System.Drawing.Size(36, 23);
            this.btnEditFlags1.TabIndex = 10;
            this.btnEditFlags1.Text = "Edit";
            this.btnEditFlags1.UseVisualStyleBackColor = true;
            this.btnEditFlags1.Click += new System.EventHandler(this.btnEditFlags1_Click);
            // 
            // btnEditFlags2
            // 
            this.btnEditFlags2.Location = new System.Drawing.Point(182, 322);
            this.btnEditFlags2.Name = "btnEditFlags2";
            this.btnEditFlags2.Size = new System.Drawing.Size(36, 23);
            this.btnEditFlags2.TabIndex = 12;
            this.btnEditFlags2.Text = "Edit";
            this.btnEditFlags2.UseVisualStyleBackColor = true;
            this.btnEditFlags2.Click += new System.EventHandler(this.btnEditFlags2_Click);
            // 
            // lstExtraDesc
            // 
            this.lstExtraDesc.FormattingEnabled = true;
            this.lstExtraDesc.Location = new System.Drawing.Point(238, 268);
            this.lstExtraDesc.Name = "lstExtraDesc";
            this.lstExtraDesc.Size = new System.Drawing.Size(156, 82);
            this.lstExtraDesc.TabIndex = 20;
            // 
            // lblExtraDesc
            // 
            this.lblExtraDesc.AutoSize = true;
            this.lblExtraDesc.Location = new System.Drawing.Point(256, 353);
            this.lblExtraDesc.Name = "lblExtraDesc";
            this.lblExtraDesc.Size = new System.Drawing.Size(113, 13);
            this.lblExtraDesc.TabIndex = 44;
            this.lblExtraDesc.Text = "Extended Descriptions";
            // 
            // btnAddExtraDesc
            // 
            this.btnAddExtraDesc.Location = new System.Drawing.Point(235, 240);
            this.btnAddExtraDesc.Name = "btnAddExtraDesc";
            this.btnAddExtraDesc.Size = new System.Drawing.Size(50, 23);
            this.btnAddExtraDesc.TabIndex = 17;
            this.btnAddExtraDesc.Text = "Add";
            this.btnAddExtraDesc.UseVisualStyleBackColor = true;
            this.btnAddExtraDesc.Click += new System.EventHandler(this.btnAddExtraDesc_Click);
            // 
            // btnRemoveExtraDesc
            // 
            this.btnRemoveExtraDesc.Location = new System.Drawing.Point(343, 240);
            this.btnRemoveExtraDesc.Name = "btnRemoveExtraDesc";
            this.btnRemoveExtraDesc.Size = new System.Drawing.Size(50, 23);
            this.btnRemoveExtraDesc.TabIndex = 19;
            this.btnRemoveExtraDesc.Text = "Rem";
            this.btnRemoveExtraDesc.UseVisualStyleBackColor = true;
            this.btnRemoveExtraDesc.Click += new System.EventHandler(this.btnRemoveExtraDesc_Click);
            // 
            // btnEditExtraDescr
            // 
            this.btnEditExtraDescr.Location = new System.Drawing.Point(289, 240);
            this.btnEditExtraDescr.Name = "btnEditExtraDescr";
            this.btnEditExtraDescr.Size = new System.Drawing.Size(50, 23);
            this.btnEditExtraDescr.TabIndex = 18;
            this.btnEditExtraDescr.Text = "Edit";
            this.btnEditExtraDescr.UseVisualStyleBackColor = true;
            this.btnEditExtraDescr.Click += new System.EventHandler(this.btnEditExtraDescr_Click);
            // 
            // btnClone
            // 
            this.btnClone.Location = new System.Drawing.Point(327, 64);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(75, 23);
            this.btnClone.TabIndex = 45;
            this.btnClone.Text = "Clone";
            this.btnClone.UseVisualStyleBackColor = true;
            this.btnClone.Click += new System.EventHandler(this.btnClone_Click);
            // 
            // cbMapSector
            // 
            this.cbMapSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMapSector.FormattingEnabled = true;
            this.cbMapSector.Location = new System.Drawing.Point(76, 440);
            this.cbMapSector.Name = "cbMapSector";
            this.cbMapSector.Size = new System.Drawing.Size(121, 21);
            this.cbMapSector.Sorted = true;
            this.cbMapSector.TabIndex = 16;
            // 
            // EditRooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 500);
            this.Controls.Add(this.cbMapSector);
            this.Controls.Add(this.btnClone);
            this.Controls.Add(this.btnEditExtraDescr);
            this.Controls.Add(this.btnRemoveExtraDesc);
            this.Controls.Add(this.btnAddExtraDesc);
            this.Controls.Add(this.lblExtraDesc);
            this.Controls.Add(this.lstExtraDesc);
            this.Controls.Add(this.btnEditFlags2);
            this.Controls.Add(this.btnEditFlags1);
            this.Controls.Add(this.rtbDescription);
            this.Controls.Add(this.rtbName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btndn);
            this.Controls.Add(this.btnup);
            this.Controls.Add(this.btns);
            this.Controls.Add(this.btnse);
            this.Controls.Add(this.btne);
            this.Controls.Add(this.btnne);
            this.Controls.Add(this.btnsw);
            this.Controls.Add(this.btnw);
            this.Controls.Add(this.btnnw);
            this.Controls.Add(this.btnn);
            this.Controls.Add(this.lblExits);
            this.Controls.Add(this.txtFallChance);
            this.Controls.Add(this.txtCurrent);
            this.Controls.Add(this.cbCurrentDir);
            this.Controls.Add(this.lblMapSector);
            this.Controls.Add(this.lblFallChance);
            this.Controls.Add(this.lblCurrentDir);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.txtFlags2);
            this.Controls.Add(this.txtFlags);
            this.Controls.Add(this.lblFlags);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cbTerrain);
            this.Controls.Add(this.lblTerrain);
            this.Controls.Add(this.txtIndexNumber);
            this.Controls.Add(this.lblIndexNumber);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnFwd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.roomList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditRooms";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Rooms";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox roomList;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnFwd;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblIndexNumber;
        private System.Windows.Forms.TextBox txtIndexNumber;
        private System.Windows.Forms.Label lblTerrain;
        private System.Windows.Forms.ComboBox cbTerrain;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblFlags;
        private System.Windows.Forms.TextBox txtFlags;
        private System.Windows.Forms.TextBox txtFlags2;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblCurrentDir;
        private System.Windows.Forms.Label lblFallChance;
        private System.Windows.Forms.Label lblMapSector;
        private System.Windows.Forms.ComboBox cbCurrentDir;
        private System.Windows.Forms.TextBox txtCurrent;
        private System.Windows.Forms.TextBox txtFallChance;
        private System.Windows.Forms.Label lblExits;
        private System.Windows.Forms.Button btnn;
        private System.Windows.Forms.Button btnnw;
        private System.Windows.Forms.Button btnw;
        private System.Windows.Forms.Button btnsw;
        private System.Windows.Forms.Button btnne;
        private System.Windows.Forms.Button btne;
        private System.Windows.Forms.Button btnse;
        private System.Windows.Forms.Button btns;
        private System.Windows.Forms.Button btnup;
        private System.Windows.Forms.Button btndn;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.RichTextBox rtbName;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Button btnEditFlags1;
        private System.Windows.Forms.Button btnEditFlags2;
        private System.Windows.Forms.ListBox lstExtraDesc;
        private System.Windows.Forms.Label lblExtraDesc;
        private System.Windows.Forms.Button btnAddExtraDesc;
        private System.Windows.Forms.Button btnRemoveExtraDesc;
        private System.Windows.Forms.Button btnEditExtraDescr;
        private System.Windows.Forms.Button btnClone;
        private System.Windows.Forms.ComboBox cbMapSector;
    }
}