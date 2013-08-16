namespace ModernMUDEditor
{
    partial class EditObjects
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
            this.objectList = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFwd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblIndexNumber = new System.Windows.Forms.Label();
            this.txtIndexNumber = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblShortDescription = new System.Windows.Forms.Label();
            this.lblFullDescription = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblCondition = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtShortDescription = new System.Windows.Forms.TextBox();
            this.txtFullDescription = new System.Windows.Forms.TextBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.lblCraftsmanship = new System.Windows.Forms.Label();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.cbMaterial = new System.Windows.Forms.ComboBox();
            this.cbSize = new System.Windows.Forms.ComboBox();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.cbCraftsmanship = new System.Windows.Forms.ComboBox();
            this.lblItemType = new System.Windows.Forms.Label();
            this.cbItemType = new System.Windows.Forms.ComboBox();
            this.lblMaxInGame = new System.Windows.Forms.Label();
            this.txtMaxInGame = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.rtbShortDesc = new System.Windows.Forms.RichTextBox();
            this.rtbFullDescription = new System.Windows.Forms.RichTextBox();
            this.lblExtraFlags = new System.Windows.Forms.Label();
            this.lblWearFlags = new System.Windows.Forms.Label();
            this.lblUseFlags = new System.Windows.Forms.Label();
            this.lblUseFlags2 = new System.Windows.Forms.Label();
            this.txtExtraFlags = new System.Windows.Forms.TextBox();
            this.txtWearFlags = new System.Windows.Forms.TextBox();
            this.txtUseFlags = new System.Windows.Forms.TextBox();
            this.txtUseFlags2 = new System.Windows.Forms.TextBox();
            this.btnEditExtraFlags = new System.Windows.Forms.Button();
            this.btnEditWearFlags = new System.Windows.Forms.Button();
            this.btnEditUseFlags = new System.Windows.Forms.Button();
            this.btnEditUseFlags2 = new System.Windows.Forms.Button();
            this.btnEditExtraDescr = new System.Windows.Forms.Button();
            this.btnRemoveExtraDesc = new System.Windows.Forms.Button();
            this.btnAddExtraDesc = new System.Windows.Forms.Button();
            this.lblExtraDesc = new System.Windows.Forms.Label();
            this.lstExtraDesc = new System.Windows.Forms.ListBox();
            this.btnEditSpecials = new System.Windows.Forms.Button();
            this.btnEditExtraFlags2 = new System.Windows.Forms.Button();
            this.txtExtraFlags2 = new System.Windows.Forms.TextBox();
            this.lblExtraFlags2 = new System.Windows.Forms.Label();
            this.btnEditAffectFlags2 = new System.Windows.Forms.Button();
            this.txtAffectFlags2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditAffectFlags5 = new System.Windows.Forms.Button();
            this.btnEditAffectFlags4 = new System.Windows.Forms.Button();
            this.btnEditAffectFlags3 = new System.Windows.Forms.Button();
            this.btnEditAffectFlags1 = new System.Windows.Forms.Button();
            this.txtAffectFlags5 = new System.Windows.Forms.TextBox();
            this.txtAffectFlags4 = new System.Windows.Forms.TextBox();
            this.txtAffectFlags3 = new System.Windows.Forms.TextBox();
            this.txtAffectFlags1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEditValues = new System.Windows.Forms.Button();
            this.btnEditAffects = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnEditSpells = new System.Windows.Forms.Button();
            this.btnEditCustomActions = new System.Windows.Forms.Button();
            this.btnClone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // objectList
            // 
            this.objectList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectList.FormattingEnabled = true;
            this.objectList.Location = new System.Drawing.Point(12, 12);
            this.objectList.Name = "objectList";
            this.objectList.Size = new System.Drawing.Size(159, 21);
            this.objectList.TabIndex = 0;
            this.objectList.SelectedIndexChanged += new System.EventHandler(this.objectList_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(177, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(25, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnFwd
            // 
            this.btnFwd.Location = new System.Drawing.Point(208, 12);
            this.btnFwd.Name = "btnFwd";
            this.btnFwd.Size = new System.Drawing.Size(25, 23);
            this.btnFwd.TabIndex = 2;
            this.btnFwd.Text = ">";
            this.btnFwd.UseVisualStyleBackColor = true;
            this.btnFwd.Click += new System.EventHandler(this.btnFwd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(318, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(239, 12);
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
            this.lblIndexNumber.Location = new System.Drawing.Point(9, 46);
            this.lblIndexNumber.Name = "lblIndexNumber";
            this.lblIndexNumber.Size = new System.Drawing.Size(73, 13);
            this.lblIndexNumber.TabIndex = 10;
            this.lblIndexNumber.Text = "Index Number";
            // 
            // txtIndexNumber
            // 
            this.txtIndexNumber.Location = new System.Drawing.Point(103, 43);
            this.txtIndexNumber.Name = "txtIndexNumber";
            this.txtIndexNumber.Size = new System.Drawing.Size(157, 20);
            this.txtIndexNumber.TabIndex = 5;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(9, 72);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(84, 13);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Name Keywords";
            // 
            // lblShortDescription
            // 
            this.lblShortDescription.AutoSize = true;
            this.lblShortDescription.Location = new System.Drawing.Point(9, 103);
            this.lblShortDescription.Name = "lblShortDescription";
            this.lblShortDescription.Size = new System.Drawing.Size(88, 13);
            this.lblShortDescription.TabIndex = 13;
            this.lblShortDescription.Text = "Short Description";
            // 
            // lblFullDescription
            // 
            this.lblFullDescription.AutoSize = true;
            this.lblFullDescription.Location = new System.Drawing.Point(9, 129);
            this.lblFullDescription.Name = "lblFullDescription";
            this.lblFullDescription.Size = new System.Drawing.Size(79, 13);
            this.lblFullDescription.TabIndex = 14;
            this.lblFullDescription.Text = "Full Description";
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(9, 245);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(41, 13);
            this.lblWeight.TabIndex = 15;
            this.lblWeight.Text = "Weight";
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(9, 272);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(51, 13);
            this.lblCondition.TabIndex = 16;
            this.lblCondition.Text = "Condition";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(103, 70);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(157, 20);
            this.txtName.TabIndex = 7;
            // 
            // txtShortDescription
            // 
            this.txtShortDescription.Location = new System.Drawing.Point(103, 100);
            this.txtShortDescription.Name = "txtShortDescription";
            this.txtShortDescription.Size = new System.Drawing.Size(186, 20);
            this.txtShortDescription.TabIndex = 9;
            this.txtShortDescription.TextChanged += new System.EventHandler(this.txtShortDescription_TextChanged);
            // 
            // txtFullDescription
            // 
            this.txtFullDescription.AcceptsReturn = true;
            this.txtFullDescription.Location = new System.Drawing.Point(103, 126);
            this.txtFullDescription.Multiline = true;
            this.txtFullDescription.Name = "txtFullDescription";
            this.txtFullDescription.Size = new System.Drawing.Size(369, 53);
            this.txtFullDescription.TabIndex = 10;
            this.txtFullDescription.TextChanged += new System.EventHandler(this.txtFullDescription_TextChanged);
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(103, 242);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(84, 20);
            this.txtWeight.TabIndex = 11;
            // 
            // txtCondition
            // 
            this.txtCondition.Location = new System.Drawing.Point(103, 269);
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Size = new System.Drawing.Size(84, 20);
            this.txtCondition.TabIndex = 13;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(9, 300);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(33, 13);
            this.lblLevel.TabIndex = 22;
            this.lblLevel.Text = "Level";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(9, 324);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(27, 13);
            this.lblSize.TabIndex = 23;
            this.lblSize.Text = "Size";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(9, 347);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(42, 13);
            this.lblVolume.TabIndex = 24;
            this.lblVolume.Text = "Volume";
            // 
            // lblCraftsmanship
            // 
            this.lblCraftsmanship.AutoSize = true;
            this.lblCraftsmanship.Location = new System.Drawing.Point(260, 245);
            this.lblCraftsmanship.Name = "lblCraftsmanship";
            this.lblCraftsmanship.Size = new System.Drawing.Size(73, 13);
            this.lblCraftsmanship.TabIndex = 25;
            this.lblCraftsmanship.Text = "Craftsmanship";
            // 
            // lblMaterial
            // 
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.Location = new System.Drawing.Point(260, 272);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(44, 13);
            this.lblMaterial.TabIndex = 26;
            this.lblMaterial.Text = "Material";
            // 
            // cbMaterial
            // 
            this.cbMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMaterial.FormattingEnabled = true;
            this.cbMaterial.Location = new System.Drawing.Point(341, 269);
            this.cbMaterial.Name = "cbMaterial";
            this.cbMaterial.Size = new System.Drawing.Size(131, 21);
            this.cbMaterial.Sorted = true;
            this.cbMaterial.TabIndex = 14;
            // 
            // cbSize
            // 
            this.cbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSize.FormattingEnabled = true;
            this.cbSize.Location = new System.Drawing.Point(103, 320);
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new System.Drawing.Size(121, 21);
            this.cbSize.TabIndex = 16;
            // 
            // txtLevel
            // 
            this.txtLevel.Location = new System.Drawing.Point(103, 296);
            this.txtLevel.Name = "txtLevel";
            this.txtLevel.Size = new System.Drawing.Size(84, 20);
            this.txtLevel.TabIndex = 15;
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(103, 347);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(100, 20);
            this.txtVolume.TabIndex = 17;
            // 
            // cbCraftsmanship
            // 
            this.cbCraftsmanship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCraftsmanship.FormattingEnabled = true;
            this.cbCraftsmanship.Location = new System.Drawing.Point(341, 242);
            this.cbCraftsmanship.Name = "cbCraftsmanship";
            this.cbCraftsmanship.Size = new System.Drawing.Size(131, 21);
            this.cbCraftsmanship.TabIndex = 12;
            // 
            // lblItemType
            // 
            this.lblItemType.AutoSize = true;
            this.lblItemType.Location = new System.Drawing.Point(272, 44);
            this.lblItemType.Name = "lblItemType";
            this.lblItemType.Size = new System.Drawing.Size(54, 13);
            this.lblItemType.TabIndex = 32;
            this.lblItemType.Text = "Item Type";
            // 
            // cbItemType
            // 
            this.cbItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemType.FormattingEnabled = true;
            this.cbItemType.Location = new System.Drawing.Point(332, 41);
            this.cbItemType.Name = "cbItemType";
            this.cbItemType.Size = new System.Drawing.Size(140, 21);
            this.cbItemType.TabIndex = 6;
            // 
            // lblMaxInGame
            // 
            this.lblMaxInGame.AutoSize = true;
            this.lblMaxInGame.Location = new System.Drawing.Point(9, 376);
            this.lblMaxInGame.Name = "lblMaxInGame";
            this.lblMaxInGame.Size = new System.Drawing.Size(70, 13);
            this.lblMaxInGame.TabIndex = 34;
            this.lblMaxInGame.Text = "Max In Game";
            // 
            // txtMaxInGame
            // 
            this.txtMaxInGame.Location = new System.Drawing.Point(103, 373);
            this.txtMaxInGame.Name = "txtMaxInGame";
            this.txtMaxInGame.Size = new System.Drawing.Size(100, 20);
            this.txtMaxInGame.TabIndex = 18;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(251, 577);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(170, 577);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 43;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // rtbShortDesc
            // 
            this.rtbShortDesc.BackColor = System.Drawing.Color.Black;
            this.rtbShortDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbShortDesc.DetectUrls = false;
            this.rtbShortDesc.Location = new System.Drawing.Point(295, 99);
            this.rtbShortDesc.Multiline = false;
            this.rtbShortDesc.Name = "rtbShortDesc";
            this.rtbShortDesc.ReadOnly = true;
            this.rtbShortDesc.Size = new System.Drawing.Size(177, 20);
            this.rtbShortDesc.TabIndex = 38;
            this.rtbShortDesc.TabStop = false;
            this.rtbShortDesc.Text = "";
            this.rtbShortDesc.WordWrap = false;
            // 
            // rtbFullDescription
            // 
            this.rtbFullDescription.BackColor = System.Drawing.Color.Black;
            this.rtbFullDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbFullDescription.DetectUrls = false;
            this.rtbFullDescription.Location = new System.Drawing.Point(103, 185);
            this.rtbFullDescription.Multiline = false;
            this.rtbFullDescription.Name = "rtbFullDescription";
            this.rtbFullDescription.ReadOnly = true;
            this.rtbFullDescription.Size = new System.Drawing.Size(369, 51);
            this.rtbFullDescription.TabIndex = 39;
            this.rtbFullDescription.TabStop = false;
            this.rtbFullDescription.Text = "";
            // 
            // lblExtraFlags
            // 
            this.lblExtraFlags.AutoSize = true;
            this.lblExtraFlags.Location = new System.Drawing.Point(9, 405);
            this.lblExtraFlags.Name = "lblExtraFlags";
            this.lblExtraFlags.Size = new System.Drawing.Size(59, 13);
            this.lblExtraFlags.TabIndex = 40;
            this.lblExtraFlags.Text = "Extra Flags";
            // 
            // lblWearFlags
            // 
            this.lblWearFlags.AutoSize = true;
            this.lblWearFlags.Location = new System.Drawing.Point(9, 457);
            this.lblWearFlags.Name = "lblWearFlags";
            this.lblWearFlags.Size = new System.Drawing.Size(61, 13);
            this.lblWearFlags.TabIndex = 41;
            this.lblWearFlags.Text = "Wear Flags";
            // 
            // lblUseFlags
            // 
            this.lblUseFlags.AutoSize = true;
            this.lblUseFlags.Location = new System.Drawing.Point(9, 483);
            this.lblUseFlags.Name = "lblUseFlags";
            this.lblUseFlags.Size = new System.Drawing.Size(54, 13);
            this.lblUseFlags.TabIndex = 42;
            this.lblUseFlags.Text = "Use Flags";
            // 
            // lblUseFlags2
            // 
            this.lblUseFlags2.AutoSize = true;
            this.lblUseFlags2.Location = new System.Drawing.Point(9, 508);
            this.lblUseFlags2.Name = "lblUseFlags2";
            this.lblUseFlags2.Size = new System.Drawing.Size(63, 13);
            this.lblUseFlags2.TabIndex = 43;
            this.lblUseFlags2.Text = "Use Flags 2";
            // 
            // txtExtraFlags
            // 
            this.txtExtraFlags.Location = new System.Drawing.Point(103, 402);
            this.txtExtraFlags.Name = "txtExtraFlags";
            this.txtExtraFlags.Size = new System.Drawing.Size(100, 20);
            this.txtExtraFlags.TabIndex = 23;
            // 
            // txtWearFlags
            // 
            this.txtWearFlags.Location = new System.Drawing.Point(103, 454);
            this.txtWearFlags.Name = "txtWearFlags";
            this.txtWearFlags.Size = new System.Drawing.Size(100, 20);
            this.txtWearFlags.TabIndex = 27;
            // 
            // txtUseFlags
            // 
            this.txtUseFlags.Location = new System.Drawing.Point(103, 480);
            this.txtUseFlags.Name = "txtUseFlags";
            this.txtUseFlags.Size = new System.Drawing.Size(100, 20);
            this.txtUseFlags.TabIndex = 29;
            // 
            // txtUseFlags2
            // 
            this.txtUseFlags2.Location = new System.Drawing.Point(103, 505);
            this.txtUseFlags2.Name = "txtUseFlags2";
            this.txtUseFlags2.Size = new System.Drawing.Size(100, 20);
            this.txtUseFlags2.TabIndex = 31;
            // 
            // btnEditExtraFlags
            // 
            this.btnEditExtraFlags.Location = new System.Drawing.Point(209, 400);
            this.btnEditExtraFlags.Name = "btnEditExtraFlags";
            this.btnEditExtraFlags.Size = new System.Drawing.Size(36, 23);
            this.btnEditExtraFlags.TabIndex = 24;
            this.btnEditExtraFlags.Text = "Edit";
            this.btnEditExtraFlags.UseVisualStyleBackColor = true;
            this.btnEditExtraFlags.Click += new System.EventHandler(this.btnEditExtraFlags_Click);
            // 
            // btnEditWearFlags
            // 
            this.btnEditWearFlags.Location = new System.Drawing.Point(209, 452);
            this.btnEditWearFlags.Name = "btnEditWearFlags";
            this.btnEditWearFlags.Size = new System.Drawing.Size(36, 23);
            this.btnEditWearFlags.TabIndex = 28;
            this.btnEditWearFlags.Text = "Edit";
            this.btnEditWearFlags.UseVisualStyleBackColor = true;
            this.btnEditWearFlags.Click += new System.EventHandler(this.btnEditWearFlags_Click);
            // 
            // btnEditUseFlags
            // 
            this.btnEditUseFlags.Location = new System.Drawing.Point(209, 477);
            this.btnEditUseFlags.Name = "btnEditUseFlags";
            this.btnEditUseFlags.Size = new System.Drawing.Size(36, 23);
            this.btnEditUseFlags.TabIndex = 30;
            this.btnEditUseFlags.Text = "Edit";
            this.btnEditUseFlags.UseVisualStyleBackColor = true;
            this.btnEditUseFlags.Click += new System.EventHandler(this.btnEditUseFlags_Click);
            // 
            // btnEditUseFlags2
            // 
            this.btnEditUseFlags2.Location = new System.Drawing.Point(209, 503);
            this.btnEditUseFlags2.Name = "btnEditUseFlags2";
            this.btnEditUseFlags2.Size = new System.Drawing.Size(36, 23);
            this.btnEditUseFlags2.TabIndex = 32;
            this.btnEditUseFlags2.Text = "Edit";
            this.btnEditUseFlags2.UseVisualStyleBackColor = true;
            this.btnEditUseFlags2.Click += new System.EventHandler(this.btnEditUseFlags2_Click);
            // 
            // btnEditExtraDescr
            // 
            this.btnEditExtraDescr.Location = new System.Drawing.Point(263, 342);
            this.btnEditExtraDescr.Name = "btnEditExtraDescr";
            this.btnEditExtraDescr.Size = new System.Drawing.Size(50, 23);
            this.btnEditExtraDescr.TabIndex = 20;
            this.btnEditExtraDescr.Text = "Edit";
            this.btnEditExtraDescr.UseVisualStyleBackColor = true;
            this.btnEditExtraDescr.Click += new System.EventHandler(this.btnEditExtraDescr_Click);
            // 
            // btnRemoveExtraDesc
            // 
            this.btnRemoveExtraDesc.Location = new System.Drawing.Point(263, 370);
            this.btnRemoveExtraDesc.Name = "btnRemoveExtraDesc";
            this.btnRemoveExtraDesc.Size = new System.Drawing.Size(50, 23);
            this.btnRemoveExtraDesc.TabIndex = 21;
            this.btnRemoveExtraDesc.Text = "Rem";
            this.btnRemoveExtraDesc.UseVisualStyleBackColor = true;
            this.btnRemoveExtraDesc.Click += new System.EventHandler(this.btnRemoveExtraDesc_Click);
            // 
            // btnAddExtraDesc
            // 
            this.btnAddExtraDesc.Location = new System.Drawing.Point(263, 314);
            this.btnAddExtraDesc.Name = "btnAddExtraDesc";
            this.btnAddExtraDesc.Size = new System.Drawing.Size(50, 23);
            this.btnAddExtraDesc.TabIndex = 19;
            this.btnAddExtraDesc.Text = "Add";
            this.btnAddExtraDesc.UseVisualStyleBackColor = true;
            this.btnAddExtraDesc.Click += new System.EventHandler(this.btnAddExtraDesc_Click);
            // 
            // lblExtraDesc
            // 
            this.lblExtraDesc.AutoSize = true;
            this.lblExtraDesc.Location = new System.Drawing.Point(338, 295);
            this.lblExtraDesc.Name = "lblExtraDesc";
            this.lblExtraDesc.Size = new System.Drawing.Size(113, 13);
            this.lblExtraDesc.TabIndex = 53;
            this.lblExtraDesc.Text = "Extended Descriptions";
            // 
            // lstExtraDesc
            // 
            this.lstExtraDesc.FormattingEnabled = true;
            this.lstExtraDesc.Location = new System.Drawing.Point(318, 311);
            this.lstExtraDesc.Name = "lstExtraDesc";
            this.lstExtraDesc.Size = new System.Drawing.Size(156, 82);
            this.lstExtraDesc.TabIndex = 22;
            // 
            // btnEditSpecials
            // 
            this.btnEditSpecials.Location = new System.Drawing.Point(255, 541);
            this.btnEditSpecials.Name = "btnEditSpecials";
            this.btnEditSpecials.Size = new System.Drawing.Size(68, 23);
            this.btnEditSpecials.TabIndex = 8;
            this.btnEditSpecials.Text = "Edit Specs";
            this.btnEditSpecials.UseVisualStyleBackColor = true;
            this.btnEditSpecials.Click += new System.EventHandler(this.btnEditSpecials_Click);
            // 
            // btnEditExtraFlags2
            // 
            this.btnEditExtraFlags2.Location = new System.Drawing.Point(209, 426);
            this.btnEditExtraFlags2.Name = "btnEditExtraFlags2";
            this.btnEditExtraFlags2.Size = new System.Drawing.Size(36, 23);
            this.btnEditExtraFlags2.TabIndex = 26;
            this.btnEditExtraFlags2.Text = "Edit";
            this.btnEditExtraFlags2.UseVisualStyleBackColor = true;
            this.btnEditExtraFlags2.Click += new System.EventHandler(this.btnEditExtraFlags2_Click);
            // 
            // txtExtraFlags2
            // 
            this.txtExtraFlags2.Location = new System.Drawing.Point(103, 428);
            this.txtExtraFlags2.Name = "txtExtraFlags2";
            this.txtExtraFlags2.Size = new System.Drawing.Size(100, 20);
            this.txtExtraFlags2.TabIndex = 25;
            // 
            // lblExtraFlags2
            // 
            this.lblExtraFlags2.AutoSize = true;
            this.lblExtraFlags2.Location = new System.Drawing.Point(9, 431);
            this.lblExtraFlags2.Name = "lblExtraFlags2";
            this.lblExtraFlags2.Size = new System.Drawing.Size(68, 13);
            this.lblExtraFlags2.TabIndex = 58;
            this.lblExtraFlags2.Text = "Extra Flags 2";
            // 
            // btnEditAffectFlags2
            // 
            this.btnEditAffectFlags2.Location = new System.Drawing.Point(438, 426);
            this.btnEditAffectFlags2.Name = "btnEditAffectFlags2";
            this.btnEditAffectFlags2.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags2.TabIndex = 36;
            this.btnEditAffectFlags2.Text = "Edit";
            this.btnEditAffectFlags2.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags2.Click += new System.EventHandler(this.btnEditAffectFlags2_Click);
            // 
            // txtAffectFlags2
            // 
            this.txtAffectFlags2.Location = new System.Drawing.Point(332, 428);
            this.txtAffectFlags2.Name = "txtAffectFlags2";
            this.txtAffectFlags2.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags2.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 431);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Affect Flags 2";
            // 
            // btnEditAffectFlags5
            // 
            this.btnEditAffectFlags5.Location = new System.Drawing.Point(438, 503);
            this.btnEditAffectFlags5.Name = "btnEditAffectFlags5";
            this.btnEditAffectFlags5.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags5.TabIndex = 42;
            this.btnEditAffectFlags5.Text = "Edit";
            this.btnEditAffectFlags5.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags5.Click += new System.EventHandler(this.btnEditAffectFlags5_Click);
            // 
            // btnEditAffectFlags4
            // 
            this.btnEditAffectFlags4.Location = new System.Drawing.Point(438, 477);
            this.btnEditAffectFlags4.Name = "btnEditAffectFlags4";
            this.btnEditAffectFlags4.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags4.TabIndex = 40;
            this.btnEditAffectFlags4.Text = "Edit";
            this.btnEditAffectFlags4.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags4.Click += new System.EventHandler(this.btnEditAffectFlags4_Click);
            // 
            // btnEditAffectFlags3
            // 
            this.btnEditAffectFlags3.Location = new System.Drawing.Point(438, 452);
            this.btnEditAffectFlags3.Name = "btnEditAffectFlags3";
            this.btnEditAffectFlags3.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags3.TabIndex = 38;
            this.btnEditAffectFlags3.Text = "Edit";
            this.btnEditAffectFlags3.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags3.Click += new System.EventHandler(this.btnEditAffectFlags3_Click);
            // 
            // btnEditAffectFlags1
            // 
            this.btnEditAffectFlags1.Location = new System.Drawing.Point(438, 400);
            this.btnEditAffectFlags1.Name = "btnEditAffectFlags1";
            this.btnEditAffectFlags1.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags1.TabIndex = 34;
            this.btnEditAffectFlags1.Text = "Edit";
            this.btnEditAffectFlags1.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags1.Click += new System.EventHandler(this.btnEditAffectFlags1_Click);
            // 
            // txtAffectFlags5
            // 
            this.txtAffectFlags5.Location = new System.Drawing.Point(332, 505);
            this.txtAffectFlags5.Name = "txtAffectFlags5";
            this.txtAffectFlags5.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags5.TabIndex = 41;
            // 
            // txtAffectFlags4
            // 
            this.txtAffectFlags4.Location = new System.Drawing.Point(332, 480);
            this.txtAffectFlags4.Name = "txtAffectFlags4";
            this.txtAffectFlags4.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags4.TabIndex = 39;
            // 
            // txtAffectFlags3
            // 
            this.txtAffectFlags3.Location = new System.Drawing.Point(332, 454);
            this.txtAffectFlags3.Name = "txtAffectFlags3";
            this.txtAffectFlags3.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags3.TabIndex = 37;
            // 
            // txtAffectFlags1
            // 
            this.txtAffectFlags1.Location = new System.Drawing.Point(332, 402);
            this.txtAffectFlags1.Name = "txtAffectFlags1";
            this.txtAffectFlags1.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags1.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 508);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Affect Flags 5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 483);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Affect Flags 4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 457);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Affect Flags 3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(254, 405);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 61;
            this.label5.Text = "Affect Flags 1";
            // 
            // btnEditValues
            // 
            this.btnEditValues.Location = new System.Drawing.Point(31, 541);
            this.btnEditValues.Name = "btnEditValues";
            this.btnEditValues.Size = new System.Drawing.Size(68, 23);
            this.btnEditValues.TabIndex = 74;
            this.btnEditValues.Text = "Edit Values";
            this.btnEditValues.UseVisualStyleBackColor = true;
            this.btnEditValues.Click += new System.EventHandler(this.btnEditValues_Click);
            // 
            // btnEditAffects
            // 
            this.btnEditAffects.Location = new System.Drawing.Point(105, 541);
            this.btnEditAffects.Name = "btnEditAffects";
            this.btnEditAffects.Size = new System.Drawing.Size(68, 23);
            this.btnEditAffects.TabIndex = 75;
            this.btnEditAffects.Text = "Edit Affect";
            this.btnEditAffects.UseVisualStyleBackColor = true;
            this.btnEditAffects.Click += new System.EventHandler(this.btnEditAffects_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(190, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 76;
            this.label6.Text = "Pounds";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 272);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 77;
            this.label7.Text = "Percent";
            // 
            // btnEditSpells
            // 
            this.btnEditSpells.Location = new System.Drawing.Point(179, 541);
            this.btnEditSpells.Name = "btnEditSpells";
            this.btnEditSpells.Size = new System.Drawing.Size(68, 23);
            this.btnEditSpells.TabIndex = 78;
            this.btnEditSpells.Text = "Edit Spells";
            this.btnEditSpells.UseVisualStyleBackColor = true;
            this.btnEditSpells.Click += new System.EventHandler(this.btnEditSpells_Click);
            // 
            // btnEditCustomActions
            // 
            this.btnEditCustomActions.Location = new System.Drawing.Point(331, 541);
            this.btnEditCustomActions.Name = "btnEditCustomActions";
            this.btnEditCustomActions.Size = new System.Drawing.Size(120, 23);
            this.btnEditCustomActions.TabIndex = 79;
            this.btnEditCustomActions.Text = "Edit Custom Actions";
            this.btnEditCustomActions.UseVisualStyleBackColor = true;
            this.btnEditCustomActions.Click += new System.EventHandler(this.btnEditCustomActions_Click);
            // 
            // btnClone
            // 
            this.btnClone.Location = new System.Drawing.Point(399, 12);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(75, 23);
            this.btnClone.TabIndex = 80;
            this.btnClone.Text = "Clone";
            this.btnClone.UseVisualStyleBackColor = true;
            this.btnClone.Click += new System.EventHandler(this.btnClone_Click);
            // 
            // EditObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 612);
            this.Controls.Add(this.btnClone);
            this.Controls.Add(this.btnEditCustomActions);
            this.Controls.Add(this.btnEditSpells);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnEditAffects);
            this.Controls.Add(this.btnEditValues);
            this.Controls.Add(this.btnEditAffectFlags2);
            this.Controls.Add(this.txtAffectFlags2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditAffectFlags5);
            this.Controls.Add(this.btnEditAffectFlags4);
            this.Controls.Add(this.btnEditAffectFlags3);
            this.Controls.Add(this.btnEditAffectFlags1);
            this.Controls.Add(this.txtAffectFlags5);
            this.Controls.Add(this.txtAffectFlags4);
            this.Controls.Add(this.txtAffectFlags3);
            this.Controls.Add(this.txtAffectFlags1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnEditExtraFlags2);
            this.Controls.Add(this.txtExtraFlags2);
            this.Controls.Add(this.lblExtraFlags2);
            this.Controls.Add(this.btnEditSpecials);
            this.Controls.Add(this.btnEditExtraDescr);
            this.Controls.Add(this.btnRemoveExtraDesc);
            this.Controls.Add(this.btnAddExtraDesc);
            this.Controls.Add(this.lblExtraDesc);
            this.Controls.Add(this.lstExtraDesc);
            this.Controls.Add(this.btnEditUseFlags2);
            this.Controls.Add(this.btnEditUseFlags);
            this.Controls.Add(this.btnEditWearFlags);
            this.Controls.Add(this.btnEditExtraFlags);
            this.Controls.Add(this.txtUseFlags2);
            this.Controls.Add(this.txtUseFlags);
            this.Controls.Add(this.txtWearFlags);
            this.Controls.Add(this.txtExtraFlags);
            this.Controls.Add(this.lblUseFlags2);
            this.Controls.Add(this.lblUseFlags);
            this.Controls.Add(this.lblWearFlags);
            this.Controls.Add(this.lblExtraFlags);
            this.Controls.Add(this.rtbFullDescription);
            this.Controls.Add(this.rtbShortDesc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtMaxInGame);
            this.Controls.Add(this.lblMaxInGame);
            this.Controls.Add(this.cbItemType);
            this.Controls.Add(this.lblItemType);
            this.Controls.Add(this.cbCraftsmanship);
            this.Controls.Add(this.txtVolume);
            this.Controls.Add(this.txtLevel);
            this.Controls.Add(this.cbSize);
            this.Controls.Add(this.cbMaterial);
            this.Controls.Add(this.lblMaterial);
            this.Controls.Add(this.lblCraftsmanship);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.txtCondition);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.txtFullDescription);
            this.Controls.Add(this.txtShortDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblCondition);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.lblFullDescription);
            this.Controls.Add(this.lblShortDescription);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtIndexNumber);
            this.Controls.Add(this.lblIndexNumber);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnFwd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.objectList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditObjects";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Objects";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox objectList;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFwd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblIndexNumber;
        private System.Windows.Forms.TextBox txtIndexNumber;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblShortDescription;
        private System.Windows.Forms.Label lblFullDescription;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtShortDescription;
        private System.Windows.Forms.TextBox txtFullDescription;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lblCraftsmanship;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.ComboBox cbMaterial;
        private System.Windows.Forms.ComboBox cbSize;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.ComboBox cbCraftsmanship;
        private System.Windows.Forms.Label lblItemType;
        private System.Windows.Forms.ComboBox cbItemType;
        private System.Windows.Forms.Label lblMaxInGame;
        private System.Windows.Forms.TextBox txtMaxInGame;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.RichTextBox rtbShortDesc;
        private System.Windows.Forms.RichTextBox rtbFullDescription;
        private System.Windows.Forms.Label lblExtraFlags;
        private System.Windows.Forms.Label lblWearFlags;
        private System.Windows.Forms.Label lblUseFlags;
        private System.Windows.Forms.Label lblUseFlags2;
        private System.Windows.Forms.TextBox txtExtraFlags;
        private System.Windows.Forms.TextBox txtWearFlags;
        private System.Windows.Forms.TextBox txtUseFlags;
        private System.Windows.Forms.TextBox txtUseFlags2;
        private System.Windows.Forms.Button btnEditExtraFlags;
        private System.Windows.Forms.Button btnEditWearFlags;
        private System.Windows.Forms.Button btnEditUseFlags;
        private System.Windows.Forms.Button btnEditUseFlags2;
        private System.Windows.Forms.Button btnEditExtraDescr;
        private System.Windows.Forms.Button btnRemoveExtraDesc;
        private System.Windows.Forms.Button btnAddExtraDesc;
        private System.Windows.Forms.Label lblExtraDesc;
        private System.Windows.Forms.ListBox lstExtraDesc;
        private System.Windows.Forms.Button btnEditSpecials;
        private System.Windows.Forms.Button btnEditExtraFlags2;
        private System.Windows.Forms.TextBox txtExtraFlags2;
        private System.Windows.Forms.Label lblExtraFlags2;
        private System.Windows.Forms.Button btnEditAffectFlags2;
        private System.Windows.Forms.TextBox txtAffectFlags2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditAffectFlags5;
        private System.Windows.Forms.Button btnEditAffectFlags4;
        private System.Windows.Forms.Button btnEditAffectFlags3;
        private System.Windows.Forms.Button btnEditAffectFlags1;
        private System.Windows.Forms.TextBox txtAffectFlags5;
        private System.Windows.Forms.TextBox txtAffectFlags4;
        private System.Windows.Forms.TextBox txtAffectFlags3;
        private System.Windows.Forms.TextBox txtAffectFlags1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEditValues;
        private System.Windows.Forms.Button btnEditAffects;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnEditSpells;
        private System.Windows.Forms.Button btnEditCustomActions;
        private System.Windows.Forms.Button btnClone;
    }
}