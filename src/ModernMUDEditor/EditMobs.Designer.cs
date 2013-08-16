namespace ModernMUDEditor
{
    partial class EditMobs
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
            this.mobList = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFwd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblIndexNumber = new System.Windows.Forms.Label();
            this.txtIndexNumber = new System.Windows.Forms.TextBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.lblAlignment = new System.Windows.Forms.Label();
            this.txtAlignment = new System.Windows.Forms.TextBox();
            this.lblShortDescription = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtShortDescription = new System.Windows.Forms.TextBox();
            this.lblFullDescription = new System.Windows.Forms.Label();
            this.txtFullDescription = new System.Windows.Forms.TextBox();
            this.lblRace = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.cbRace = new System.Windows.Forms.ComboBox();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.cbPosition = new System.Windows.Forms.ComboBox();
            this.cbSex = new System.Windows.Forms.ComboBox();
            this.lblActFlags = new System.Windows.Forms.Label();
            this.txtActFlags = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rtbShortDesc = new System.Windows.Forms.RichTextBox();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.rtbFullDescription = new System.Windows.Forms.RichTextBox();
            this.btnEditActFlags = new System.Windows.Forms.Button();
            this.btnEditShop = new System.Windows.Forms.Button();
            this.btnEditQuests = new System.Windows.Forms.Button();
            this.btnEditSpecials = new System.Windows.Forms.Button();
            this.btnEditActFlags2 = new System.Windows.Forms.Button();
            this.txtActFlags2 = new System.Windows.Forms.TextBox();
            this.lblActionFlags2 = new System.Windows.Forms.Label();
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
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEditCustomActions = new System.Windows.Forms.Button();
            this.btnClone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mobList
            // 
            this.mobList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mobList.FormattingEnabled = true;
            this.mobList.Location = new System.Drawing.Point(13, 13);
            this.mobList.Name = "mobList";
            this.mobList.Size = new System.Drawing.Size(150, 21);
            this.mobList.TabIndex = 0;
            this.mobList.SelectedIndexChanged += new System.EventHandler(this.mobList_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(169, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(25, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnFwd
            // 
            this.btnFwd.Location = new System.Drawing.Point(200, 12);
            this.btnFwd.Name = "btnFwd";
            this.btnFwd.Size = new System.Drawing.Size(25, 23);
            this.btnFwd.TabIndex = 2;
            this.btnFwd.Text = ">";
            this.btnFwd.UseVisualStyleBackColor = true;
            this.btnFwd.Click += new System.EventHandler(this.btnFwd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(312, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(231, 12);
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
            this.lblIndexNumber.Location = new System.Drawing.Point(10, 48);
            this.lblIndexNumber.Name = "lblIndexNumber";
            this.lblIndexNumber.Size = new System.Drawing.Size(73, 13);
            this.lblIndexNumber.TabIndex = 10;
            this.lblIndexNumber.Text = "Index Number";
            // 
            // txtIndexNumber
            // 
            this.txtIndexNumber.Location = new System.Drawing.Point(106, 44);
            this.txtIndexNumber.Name = "txtIndexNumber";
            this.txtIndexNumber.Size = new System.Drawing.Size(100, 20);
            this.txtIndexNumber.TabIndex = 5;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(10, 75);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(33, 13);
            this.lblLevel.TabIndex = 12;
            this.lblLevel.Text = "Level";
            // 
            // txtLevel
            // 
            this.txtLevel.Location = new System.Drawing.Point(106, 72);
            this.txtLevel.Name = "txtLevel";
            this.txtLevel.Size = new System.Drawing.Size(100, 20);
            this.txtLevel.TabIndex = 7;
            // 
            // lblAlignment
            // 
            this.lblAlignment.AutoSize = true;
            this.lblAlignment.Location = new System.Drawing.Point(10, 103);
            this.lblAlignment.Name = "lblAlignment";
            this.lblAlignment.Size = new System.Drawing.Size(53, 13);
            this.lblAlignment.TabIndex = 14;
            this.lblAlignment.Text = "Alignment";
            // 
            // txtAlignment
            // 
            this.txtAlignment.Location = new System.Drawing.Point(106, 100);
            this.txtAlignment.Name = "txtAlignment";
            this.txtAlignment.Size = new System.Drawing.Size(100, 20);
            this.txtAlignment.TabIndex = 9;
            // 
            // lblShortDescription
            // 
            this.lblShortDescription.AutoSize = true;
            this.lblShortDescription.Location = new System.Drawing.Point(10, 189);
            this.lblShortDescription.Name = "lblShortDescription";
            this.lblShortDescription.Size = new System.Drawing.Size(88, 13);
            this.lblShortDescription.TabIndex = 16;
            this.lblShortDescription.Text = "Short Description";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 215);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 17;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(100, 213);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(363, 20);
            this.txtDescription.TabIndex = 17;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // txtShortDescription
            // 
            this.txtShortDescription.Location = new System.Drawing.Point(100, 186);
            this.txtShortDescription.Name = "txtShortDescription";
            this.txtShortDescription.Size = new System.Drawing.Size(168, 20);
            this.txtShortDescription.TabIndex = 16;
            this.txtShortDescription.TextChanged += new System.EventHandler(this.txtShortDescription_TextChanged);
            // 
            // lblFullDescription
            // 
            this.lblFullDescription.AutoSize = true;
            this.lblFullDescription.Location = new System.Drawing.Point(10, 267);
            this.lblFullDescription.Name = "lblFullDescription";
            this.lblFullDescription.Size = new System.Drawing.Size(79, 13);
            this.lblFullDescription.TabIndex = 20;
            this.lblFullDescription.Text = "Full Description";
            // 
            // txtFullDescription
            // 
            this.txtFullDescription.AcceptsReturn = true;
            this.txtFullDescription.Location = new System.Drawing.Point(100, 265);
            this.txtFullDescription.Multiline = true;
            this.txtFullDescription.Name = "txtFullDescription";
            this.txtFullDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFullDescription.Size = new System.Drawing.Size(363, 73);
            this.txtFullDescription.TabIndex = 18;
            this.txtFullDescription.TextChanged += new System.EventHandler(this.txtFullDescription_TextChanged);
            // 
            // lblRace
            // 
            this.lblRace.AutoSize = true;
            this.lblRace.Location = new System.Drawing.Point(243, 48);
            this.lblRace.Name = "lblRace";
            this.lblRace.Size = new System.Drawing.Size(33, 13);
            this.lblRace.TabIndex = 22;
            this.lblRace.Text = "Race";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(243, 75);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(32, 13);
            this.lblClass.TabIndex = 23;
            this.lblClass.Text = "Class";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(243, 98);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(44, 13);
            this.lblPosition.TabIndex = 24;
            this.lblPosition.Text = "Position";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(243, 125);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(25, 13);
            this.lblSex.TabIndex = 25;
            this.lblSex.Text = "Sex";
            // 
            // cbRace
            // 
            this.cbRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRace.FormattingEnabled = true;
            this.cbRace.Location = new System.Drawing.Point(298, 45);
            this.cbRace.Name = "cbRace";
            this.cbRace.Size = new System.Drawing.Size(121, 21);
            this.cbRace.Sorted = true;
            this.cbRace.TabIndex = 6;
            // 
            // cbClass
            // 
            this.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Location = new System.Drawing.Point(298, 71);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(121, 21);
            this.cbClass.Sorted = true;
            this.cbClass.TabIndex = 8;
            // 
            // cbPosition
            // 
            this.cbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPosition.FormattingEnabled = true;
            this.cbPosition.Location = new System.Drawing.Point(298, 95);
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.Size = new System.Drawing.Size(121, 21);
            this.cbPosition.TabIndex = 10;
            // 
            // cbSex
            // 
            this.cbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSex.FormattingEnabled = true;
            this.cbSex.Location = new System.Drawing.Point(298, 122);
            this.cbSex.Name = "cbSex";
            this.cbSex.Size = new System.Drawing.Size(121, 21);
            this.cbSex.TabIndex = 14;
            // 
            // lblActFlags
            // 
            this.lblActFlags.AutoSize = true;
            this.lblActFlags.Location = new System.Drawing.Point(12, 425);
            this.lblActFlags.Name = "lblActFlags";
            this.lblActFlags.Size = new System.Drawing.Size(65, 13);
            this.lblActFlags.TabIndex = 30;
            this.lblActFlags.Text = "Action Flags";
            // 
            // txtActFlags
            // 
            this.txtActFlags.Location = new System.Drawing.Point(100, 425);
            this.txtActFlags.Name = "txtActFlags";
            this.txtActFlags.Size = new System.Drawing.Size(104, 20);
            this.txtActFlags.TabIndex = 19;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(154, 560);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 33;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(235, 560);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rtbShortDesc
            // 
            this.rtbShortDesc.BackColor = System.Drawing.Color.Black;
            this.rtbShortDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbShortDesc.DetectUrls = false;
            this.rtbShortDesc.Location = new System.Drawing.Point(276, 186);
            this.rtbShortDesc.Multiline = false;
            this.rtbShortDesc.Name = "rtbShortDesc";
            this.rtbShortDesc.ReadOnly = true;
            this.rtbShortDesc.Size = new System.Drawing.Size(187, 20);
            this.rtbShortDesc.TabIndex = 36;
            this.rtbShortDesc.TabStop = false;
            this.rtbShortDesc.Text = "";
            this.rtbShortDesc.WordWrap = false;
            // 
            // rtbDescription
            // 
            this.rtbDescription.BackColor = System.Drawing.Color.Black;
            this.rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDescription.DetectUrls = false;
            this.rtbDescription.Location = new System.Drawing.Point(100, 239);
            this.rtbDescription.Multiline = false;
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.ReadOnly = true;
            this.rtbDescription.Size = new System.Drawing.Size(363, 20);
            this.rtbDescription.TabIndex = 37;
            this.rtbDescription.TabStop = false;
            this.rtbDescription.Text = "";
            this.rtbDescription.WordWrap = false;
            // 
            // rtbFullDescription
            // 
            this.rtbFullDescription.BackColor = System.Drawing.Color.Black;
            this.rtbFullDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbFullDescription.DetectUrls = false;
            this.rtbFullDescription.Location = new System.Drawing.Point(100, 344);
            this.rtbFullDescription.Name = "rtbFullDescription";
            this.rtbFullDescription.ReadOnly = true;
            this.rtbFullDescription.Size = new System.Drawing.Size(363, 73);
            this.rtbFullDescription.TabIndex = 38;
            this.rtbFullDescription.TabStop = false;
            this.rtbFullDescription.Text = "";
            // 
            // btnEditActFlags
            // 
            this.btnEditActFlags.Location = new System.Drawing.Point(206, 423);
            this.btnEditActFlags.Name = "btnEditActFlags";
            this.btnEditActFlags.Size = new System.Drawing.Size(36, 23);
            this.btnEditActFlags.TabIndex = 20;
            this.btnEditActFlags.Text = "Edit";
            this.btnEditActFlags.UseVisualStyleBackColor = true;
            this.btnEditActFlags.Click += new System.EventHandler(this.btnEditActFlags_Click);
            // 
            // btnEditShop
            // 
            this.btnEditShop.Location = new System.Drawing.Point(80, 125);
            this.btnEditShop.Name = "btnEditShop";
            this.btnEditShop.Size = new System.Drawing.Size(68, 23);
            this.btnEditShop.TabIndex = 12;
            this.btnEditShop.Text = "Edit Shop";
            this.btnEditShop.UseVisualStyleBackColor = true;
            this.btnEditShop.Click += new System.EventHandler(this.btnEditShop_Click);
            // 
            // btnEditQuests
            // 
            this.btnEditQuests.Location = new System.Drawing.Point(152, 125);
            this.btnEditQuests.Name = "btnEditQuests";
            this.btnEditQuests.Size = new System.Drawing.Size(68, 23);
            this.btnEditQuests.TabIndex = 13;
            this.btnEditQuests.Text = "Edit Quest";
            this.btnEditQuests.UseVisualStyleBackColor = true;
            this.btnEditQuests.Click += new System.EventHandler(this.btnEditQuests_Click);
            // 
            // btnEditSpecials
            // 
            this.btnEditSpecials.Location = new System.Drawing.Point(7, 125);
            this.btnEditSpecials.Name = "btnEditSpecials";
            this.btnEditSpecials.Size = new System.Drawing.Size(68, 23);
            this.btnEditSpecials.TabIndex = 11;
            this.btnEditSpecials.Text = "Edit Specs";
            this.btnEditSpecials.UseVisualStyleBackColor = true;
            this.btnEditSpecials.Click += new System.EventHandler(this.btnEditSpecials_Click);
            // 
            // btnEditActFlags2
            // 
            this.btnEditActFlags2.Location = new System.Drawing.Point(206, 449);
            this.btnEditActFlags2.Name = "btnEditActFlags2";
            this.btnEditActFlags2.Size = new System.Drawing.Size(36, 23);
            this.btnEditActFlags2.TabIndex = 22;
            this.btnEditActFlags2.Text = "Edit";
            this.btnEditActFlags2.UseVisualStyleBackColor = true;
            this.btnEditActFlags2.Click += new System.EventHandler(this.btnEditActFlags2_Click);
            // 
            // txtActFlags2
            // 
            this.txtActFlags2.Location = new System.Drawing.Point(100, 451);
            this.txtActFlags2.Name = "txtActFlags2";
            this.txtActFlags2.Size = new System.Drawing.Size(104, 20);
            this.txtActFlags2.TabIndex = 21;
            // 
            // lblActionFlags2
            // 
            this.lblActionFlags2.AutoSize = true;
            this.lblActionFlags2.Location = new System.Drawing.Point(12, 454);
            this.lblActionFlags2.Name = "lblActionFlags2";
            this.lblActionFlags2.Size = new System.Drawing.Size(74, 13);
            this.lblActionFlags2.TabIndex = 49;
            this.lblActionFlags2.Text = "Action Flags 2";
            // 
            // btnEditAffectFlags2
            // 
            this.btnEditAffectFlags2.Location = new System.Drawing.Point(432, 449);
            this.btnEditAffectFlags2.Name = "btnEditAffectFlags2";
            this.btnEditAffectFlags2.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags2.TabIndex = 26;
            this.btnEditAffectFlags2.Text = "Edit";
            this.btnEditAffectFlags2.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags2.Click += new System.EventHandler(this.btnEditAffectFlags2_Click);
            // 
            // txtAffectFlags2
            // 
            this.txtAffectFlags2.Location = new System.Drawing.Point(326, 451);
            this.txtAffectFlags2.Name = "txtAffectFlags2";
            this.txtAffectFlags2.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags2.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 454);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "Affect Flags 2";
            // 
            // btnEditAffectFlags5
            // 
            this.btnEditAffectFlags5.Location = new System.Drawing.Point(432, 526);
            this.btnEditAffectFlags5.Name = "btnEditAffectFlags5";
            this.btnEditAffectFlags5.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags5.TabIndex = 32;
            this.btnEditAffectFlags5.Text = "Edit";
            this.btnEditAffectFlags5.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags5.Click += new System.EventHandler(this.btnEditAffectFlags5_Click);
            // 
            // btnEditAffectFlags4
            // 
            this.btnEditAffectFlags4.Location = new System.Drawing.Point(432, 500);
            this.btnEditAffectFlags4.Name = "btnEditAffectFlags4";
            this.btnEditAffectFlags4.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags4.TabIndex = 30;
            this.btnEditAffectFlags4.Text = "Edit";
            this.btnEditAffectFlags4.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags4.Click += new System.EventHandler(this.btnEditAffectFlags4_Click);
            // 
            // btnEditAffectFlags3
            // 
            this.btnEditAffectFlags3.Location = new System.Drawing.Point(432, 475);
            this.btnEditAffectFlags3.Name = "btnEditAffectFlags3";
            this.btnEditAffectFlags3.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags3.TabIndex = 28;
            this.btnEditAffectFlags3.Text = "Edit";
            this.btnEditAffectFlags3.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags3.Click += new System.EventHandler(this.btnEditAffectFlags3_Click);
            // 
            // btnEditAffectFlags1
            // 
            this.btnEditAffectFlags1.Location = new System.Drawing.Point(432, 423);
            this.btnEditAffectFlags1.Name = "btnEditAffectFlags1";
            this.btnEditAffectFlags1.Size = new System.Drawing.Size(36, 23);
            this.btnEditAffectFlags1.TabIndex = 24;
            this.btnEditAffectFlags1.Text = "Edit";
            this.btnEditAffectFlags1.UseVisualStyleBackColor = true;
            this.btnEditAffectFlags1.Click += new System.EventHandler(this.btnEditAffectFlags1_Click);
            // 
            // txtAffectFlags5
            // 
            this.txtAffectFlags5.Location = new System.Drawing.Point(326, 528);
            this.txtAffectFlags5.Name = "txtAffectFlags5";
            this.txtAffectFlags5.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags5.TabIndex = 31;
            // 
            // txtAffectFlags4
            // 
            this.txtAffectFlags4.Location = new System.Drawing.Point(326, 503);
            this.txtAffectFlags4.Name = "txtAffectFlags4";
            this.txtAffectFlags4.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags4.TabIndex = 29;
            // 
            // txtAffectFlags3
            // 
            this.txtAffectFlags3.Location = new System.Drawing.Point(326, 477);
            this.txtAffectFlags3.Name = "txtAffectFlags3";
            this.txtAffectFlags3.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags3.TabIndex = 27;
            // 
            // txtAffectFlags1
            // 
            this.txtAffectFlags1.Location = new System.Drawing.Point(326, 425);
            this.txtAffectFlags1.Name = "txtAffectFlags1";
            this.txtAffectFlags1.Size = new System.Drawing.Size(100, 20);
            this.txtAffectFlags1.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 531);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 79;
            this.label2.Text = "Affect Flags 5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 506);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 78;
            this.label3.Text = "Affect Flags 4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(248, 480);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Affect Flags 3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 428);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "Affect Flags 1";
            // 
            // txtKeywords
            // 
            this.txtKeywords.Location = new System.Drawing.Point(100, 158);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(168, 20);
            this.txtKeywords.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 90;
            this.label6.Text = "Name Keywords";
            // 
            // btnEditCustomActions
            // 
            this.btnEditCustomActions.Location = new System.Drawing.Point(59, 496);
            this.btnEditCustomActions.Name = "btnEditCustomActions";
            this.btnEditCustomActions.Size = new System.Drawing.Size(120, 23);
            this.btnEditCustomActions.TabIndex = 91;
            this.btnEditCustomActions.Text = "Edit Custom Actions";
            this.btnEditCustomActions.UseVisualStyleBackColor = true;
            this.btnEditCustomActions.Click += new System.EventHandler(this.btnEditCustomActions_Click);
            // 
            // btnClone
            // 
            this.btnClone.Location = new System.Drawing.Point(393, 12);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(75, 23);
            this.btnClone.TabIndex = 92;
            this.btnClone.Text = "Clone";
            this.btnClone.UseVisualStyleBackColor = true;
            this.btnClone.Click += new System.EventHandler(this.btnClone_Click);
            // 
            // EditMobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 595);
            this.Controls.Add(this.btnClone);
            this.Controls.Add(this.btnEditCustomActions);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.label6);
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
            this.Controls.Add(this.lblActionFlags2);
            this.Controls.Add(this.btnEditActFlags2);
            this.Controls.Add(this.txtActFlags2);
            this.Controls.Add(this.btnEditSpecials);
            this.Controls.Add(this.btnEditQuests);
            this.Controls.Add(this.btnEditShop);
            this.Controls.Add(this.btnEditActFlags);
            this.Controls.Add(this.rtbFullDescription);
            this.Controls.Add(this.rtbDescription);
            this.Controls.Add(this.rtbShortDesc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtActFlags);
            this.Controls.Add(this.lblActFlags);
            this.Controls.Add(this.cbSex);
            this.Controls.Add(this.cbPosition);
            this.Controls.Add(this.cbClass);
            this.Controls.Add(this.cbRace);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.lblRace);
            this.Controls.Add(this.txtFullDescription);
            this.Controls.Add(this.lblFullDescription);
            this.Controls.Add(this.txtShortDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblShortDescription);
            this.Controls.Add(this.txtAlignment);
            this.Controls.Add(this.lblAlignment);
            this.Controls.Add(this.txtLevel);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.txtIndexNumber);
            this.Controls.Add(this.lblIndexNumber);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnFwd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.mobList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditMobs";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Mobs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox mobList;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFwd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblIndexNumber;
        private System.Windows.Forms.TextBox txtIndexNumber;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.Label lblAlignment;
        private System.Windows.Forms.TextBox txtAlignment;
        private System.Windows.Forms.Label lblShortDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtShortDescription;
        private System.Windows.Forms.Label lblFullDescription;
        private System.Windows.Forms.TextBox txtFullDescription;
        private System.Windows.Forms.Label lblRace;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.ComboBox cbRace;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.ComboBox cbPosition;
        private System.Windows.Forms.ComboBox cbSex;
        private System.Windows.Forms.Label lblActFlags;
        private System.Windows.Forms.TextBox txtActFlags;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RichTextBox rtbShortDesc;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.RichTextBox rtbFullDescription;
        private System.Windows.Forms.Button btnEditActFlags;
        private System.Windows.Forms.Button btnEditShop;
        private System.Windows.Forms.Button btnEditQuests;
        private System.Windows.Forms.Button btnEditSpecials;
        private System.Windows.Forms.Button btnEditActFlags2;
        private System.Windows.Forms.TextBox txtActFlags2;
        private System.Windows.Forms.Label lblActionFlags2;
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
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnEditCustomActions;
        private System.Windows.Forms.Button btnClone;
    }
}