namespace ModernMUDEditor
{
    partial class EditAreaSettings
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
            this.lblAreaName = new System.Windows.Forms.Label();
            this.lblMinLevel = new System.Windows.Forms.Label();
            this.lblMaxLevel = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.cbMinLevel = new System.Windows.Forms.ComboBox();
            this.cbMaxLevel = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.rtbAreaName = new System.Windows.Forms.RichTextBox();
            this.txtBuilders = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblResetMessage = new System.Windows.Forms.Label();
            this.txtResetMsg = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.rtbResetMsg = new System.Windows.Forms.RichTextBox();
            this.txtRecall = new System.Windows.Forms.TextBox();
            this.lblRecall = new System.Windows.Forms.Label();
            this.lblJusticeType = new System.Windows.Forms.Label();
            this.txtJudgeRoom = new System.Windows.Forms.TextBox();
            this.lblJudgeRoom = new System.Windows.Forms.Label();
            this.txtJailRoom = new System.Windows.Forms.TextBox();
            this.lblJailRoom = new System.Windows.Forms.Label();
            this.txtBarracks = new System.Windows.Forms.TextBox();
            this.lblBarracks = new System.Windows.Forms.Label();
            this.txtGuardIndexNumber = new System.Windows.Forms.TextBox();
            this.lblGuards = new System.Windows.Forms.Label();
            this.txtNumSquads = new System.Windows.Forms.TextBox();
            this.lblSquads = new System.Windows.Forms.Label();
            this.txtSquadSize = new System.Windows.Forms.TextBox();
            this.lblSquadSize = new System.Windows.Forms.Label();
            this.lblResetMode = new System.Windows.Forms.Label();
            this.cbResetMode = new System.Windows.Forms.ComboBox();
            this.txtMinutesBetweenResets = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFindRecall = new System.Windows.Forms.Button();
            this.btnFindJudge = new System.Windows.Forms.Button();
            this.btnFindJail = new System.Windows.Forms.Button();
            this.btnFindBarracks = new System.Windows.Forms.Button();
            this.btnFindGuards = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbJusticeType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEditFlags = new System.Windows.Forms.Button();
            this.txtAreaFlags = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAreaName
            // 
            this.lblAreaName.AutoSize = true;
            this.lblAreaName.Location = new System.Drawing.Point(12, 16);
            this.lblAreaName.Name = "lblAreaName";
            this.lblAreaName.Size = new System.Drawing.Size(60, 13);
            this.lblAreaName.TabIndex = 99;
            this.lblAreaName.Text = "Area Name";
            // 
            // lblMinLevel
            // 
            this.lblMinLevel.AutoSize = true;
            this.lblMinLevel.Location = new System.Drawing.Point(11, 94);
            this.lblMinLevel.Name = "lblMinLevel";
            this.lblMinLevel.Size = new System.Drawing.Size(53, 13);
            this.lblMinLevel.TabIndex = 1;
            this.lblMinLevel.Text = "Min Level";
            // 
            // lblMaxLevel
            // 
            this.lblMaxLevel.AutoSize = true;
            this.lblMaxLevel.Location = new System.Drawing.Point(11, 117);
            this.lblMaxLevel.Name = "lblMaxLevel";
            this.lblMaxLevel.Size = new System.Drawing.Size(56, 13);
            this.lblMaxLevel.TabIndex = 2;
            this.lblMaxLevel.Text = "Max Level";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(12, 146);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(38, 13);
            this.lblAuthor.TabIndex = 3;
            this.lblAuthor.Text = "Author";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(80, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(170, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(79, 146);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(171, 20);
            this.txtAuthor.TabIndex = 4;
            // 
            // cbMinLevel
            // 
            this.cbMinLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMinLevel.FormattingEnabled = true;
            this.cbMinLevel.Location = new System.Drawing.Point(78, 91);
            this.cbMinLevel.Name = "cbMinLevel";
            this.cbMinLevel.Size = new System.Drawing.Size(171, 21);
            this.cbMinLevel.TabIndex = 2;
            // 
            // cbMaxLevel
            // 
            this.cbMaxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMaxLevel.FormattingEnabled = true;
            this.cbMaxLevel.Location = new System.Drawing.Point(78, 117);
            this.cbMaxLevel.Name = "cbMaxLevel";
            this.cbMaxLevel.Size = new System.Drawing.Size(171, 21);
            this.cbMaxLevel.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(267, 308);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(186, 308);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 22;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // rtbAreaName
            // 
            this.rtbAreaName.BackColor = System.Drawing.Color.Black;
            this.rtbAreaName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbAreaName.DetectUrls = false;
            this.rtbAreaName.Location = new System.Drawing.Point(80, 39);
            this.rtbAreaName.Multiline = false;
            this.rtbAreaName.Name = "rtbAreaName";
            this.rtbAreaName.ReadOnly = true;
            this.rtbAreaName.Size = new System.Drawing.Size(170, 20);
            this.rtbAreaName.TabIndex = 39;
            this.rtbAreaName.TabStop = false;
            this.rtbAreaName.Text = "";
            this.rtbAreaName.WordWrap = false;
            // 
            // txtBuilders
            // 
            this.txtBuilders.Location = new System.Drawing.Point(79, 173);
            this.txtBuilders.Name = "txtBuilders";
            this.txtBuilders.Size = new System.Drawing.Size(171, 20);
            this.txtBuilders.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Builders";
            // 
            // lblResetMessage
            // 
            this.lblResetMessage.AutoSize = true;
            this.lblResetMessage.Location = new System.Drawing.Point(12, 204);
            this.lblResetMessage.Name = "lblResetMessage";
            this.lblResetMessage.Size = new System.Drawing.Size(58, 13);
            this.lblResetMessage.TabIndex = 42;
            this.lblResetMessage.Text = "Reset Msg";
            // 
            // txtResetMsg
            // 
            this.txtResetMsg.Location = new System.Drawing.Point(79, 201);
            this.txtResetMsg.Name = "txtResetMsg";
            this.txtResetMsg.Size = new System.Drawing.Size(170, 20);
            this.txtResetMsg.TabIndex = 6;
            this.txtResetMsg.TextChanged += new System.EventHandler(this.txtResetMsg_TextChanged);
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(79, 65);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(171, 20);
            this.txtVersion.TabIndex = 1;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(12, 68);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 44;
            this.lblVersion.Text = "Version";
            // 
            // rtbResetMsg
            // 
            this.rtbResetMsg.BackColor = System.Drawing.Color.Black;
            this.rtbResetMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbResetMsg.DetectUrls = false;
            this.rtbResetMsg.Location = new System.Drawing.Point(78, 227);
            this.rtbResetMsg.Multiline = false;
            this.rtbResetMsg.Name = "rtbResetMsg";
            this.rtbResetMsg.ReadOnly = true;
            this.rtbResetMsg.Size = new System.Drawing.Size(172, 20);
            this.rtbResetMsg.TabIndex = 46;
            this.rtbResetMsg.TabStop = false;
            this.rtbResetMsg.Text = "";
            this.rtbResetMsg.WordWrap = false;
            // 
            // txtRecall
            // 
            this.txtRecall.Location = new System.Drawing.Point(341, 12);
            this.txtRecall.Name = "txtRecall";
            this.txtRecall.Size = new System.Drawing.Size(129, 20);
            this.txtRecall.TabIndex = 7;
            // 
            // lblRecall
            // 
            this.lblRecall.AutoSize = true;
            this.lblRecall.Location = new System.Drawing.Point(264, 16);
            this.lblRecall.Name = "lblRecall";
            this.lblRecall.Size = new System.Drawing.Size(37, 13);
            this.lblRecall.TabIndex = 47;
            this.lblRecall.Text = "Recall";
            // 
            // lblJusticeType
            // 
            this.lblJusticeType.AutoSize = true;
            this.lblJusticeType.Location = new System.Drawing.Point(264, 42);
            this.lblJusticeType.Name = "lblJusticeType";
            this.lblJusticeType.Size = new System.Drawing.Size(67, 13);
            this.lblJusticeType.TabIndex = 49;
            this.lblJusticeType.Text = "Justice Type";
            // 
            // txtJudgeRoom
            // 
            this.txtJudgeRoom.Location = new System.Drawing.Point(341, 67);
            this.txtJudgeRoom.Name = "txtJudgeRoom";
            this.txtJudgeRoom.Size = new System.Drawing.Size(129, 20);
            this.txtJudgeRoom.TabIndex = 10;
            // 
            // lblJudgeRoom
            // 
            this.lblJudgeRoom.AutoSize = true;
            this.lblJudgeRoom.Location = new System.Drawing.Point(264, 71);
            this.lblJudgeRoom.Name = "lblJudgeRoom";
            this.lblJudgeRoom.Size = new System.Drawing.Size(67, 13);
            this.lblJudgeRoom.TabIndex = 51;
            this.lblJudgeRoom.Text = "Judge Room";
            // 
            // txtJailRoom
            // 
            this.txtJailRoom.Location = new System.Drawing.Point(341, 93);
            this.txtJailRoom.Name = "txtJailRoom";
            this.txtJailRoom.Size = new System.Drawing.Size(129, 20);
            this.txtJailRoom.TabIndex = 12;
            // 
            // lblJailRoom
            // 
            this.lblJailRoom.AutoSize = true;
            this.lblJailRoom.Location = new System.Drawing.Point(264, 97);
            this.lblJailRoom.Name = "lblJailRoom";
            this.lblJailRoom.Size = new System.Drawing.Size(53, 13);
            this.lblJailRoom.TabIndex = 53;
            this.lblJailRoom.Text = "Jail Room";
            // 
            // txtBarracks
            // 
            this.txtBarracks.Location = new System.Drawing.Point(341, 119);
            this.txtBarracks.Name = "txtBarracks";
            this.txtBarracks.Size = new System.Drawing.Size(129, 20);
            this.txtBarracks.TabIndex = 14;
            // 
            // lblBarracks
            // 
            this.lblBarracks.AutoSize = true;
            this.lblBarracks.Location = new System.Drawing.Point(264, 123);
            this.lblBarracks.Name = "lblBarracks";
            this.lblBarracks.Size = new System.Drawing.Size(49, 13);
            this.lblBarracks.TabIndex = 55;
            this.lblBarracks.Text = "Barracks";
            // 
            // txtGuardIndexNumber
            // 
            this.txtGuardIndexNumber.Location = new System.Drawing.Point(341, 145);
            this.txtGuardIndexNumber.Name = "txtGuardIndexNumber";
            this.txtGuardIndexNumber.Size = new System.Drawing.Size(129, 20);
            this.txtGuardIndexNumber.TabIndex = 16;
            // 
            // lblGuards
            // 
            this.lblGuards.AutoSize = true;
            this.lblGuards.Location = new System.Drawing.Point(264, 149);
            this.lblGuards.Name = "lblGuards";
            this.lblGuards.Size = new System.Drawing.Size(41, 13);
            this.lblGuards.TabIndex = 57;
            this.lblGuards.Text = "Guards";
            // 
            // txtNumSquads
            // 
            this.txtNumSquads.Location = new System.Drawing.Point(341, 172);
            this.txtNumSquads.Name = "txtNumSquads";
            this.txtNumSquads.Size = new System.Drawing.Size(171, 20);
            this.txtNumSquads.TabIndex = 18;
            // 
            // lblSquads
            // 
            this.lblSquads.AutoSize = true;
            this.lblSquads.Location = new System.Drawing.Point(264, 176);
            this.lblSquads.Name = "lblSquads";
            this.lblSquads.Size = new System.Drawing.Size(43, 13);
            this.lblSquads.TabIndex = 59;
            this.lblSquads.Text = "Squads";
            // 
            // txtSquadSize
            // 
            this.txtSquadSize.Location = new System.Drawing.Point(341, 200);
            this.txtSquadSize.Name = "txtSquadSize";
            this.txtSquadSize.Size = new System.Drawing.Size(171, 20);
            this.txtSquadSize.TabIndex = 19;
            // 
            // lblSquadSize
            // 
            this.lblSquadSize.AutoSize = true;
            this.lblSquadSize.Location = new System.Drawing.Point(264, 204);
            this.lblSquadSize.Name = "lblSquadSize";
            this.lblSquadSize.Size = new System.Drawing.Size(61, 13);
            this.lblSquadSize.TabIndex = 61;
            this.lblSquadSize.Text = "Squad Size";
            // 
            // lblResetMode
            // 
            this.lblResetMode.AutoSize = true;
            this.lblResetMode.Location = new System.Drawing.Point(264, 230);
            this.lblResetMode.Name = "lblResetMode";
            this.lblResetMode.Size = new System.Drawing.Size(65, 13);
            this.lblResetMode.TabIndex = 63;
            this.lblResetMode.Text = "Reset Mode";
            // 
            // cbResetMode
            // 
            this.cbResetMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResetMode.FormattingEnabled = true;
            this.cbResetMode.Location = new System.Drawing.Point(341, 226);
            this.cbResetMode.Name = "cbResetMode";
            this.cbResetMode.Size = new System.Drawing.Size(171, 21);
            this.cbResetMode.TabIndex = 20;
            // 
            // txtMinutesBetweenResets
            // 
            this.txtMinutesBetweenResets.Location = new System.Drawing.Point(341, 253);
            this.txtMinutesBetweenResets.Name = "txtMinutesBetweenResets";
            this.txtMinutesBetweenResets.Size = new System.Drawing.Size(116, 20);
            this.txtMinutesBetweenResets.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 102;
            this.label2.Text = "Reset Time";
            // 
            // btnFindRecall
            // 
            this.btnFindRecall.Location = new System.Drawing.Point(476, 10);
            this.btnFindRecall.Name = "btnFindRecall";
            this.btnFindRecall.Size = new System.Drawing.Size(36, 23);
            this.btnFindRecall.TabIndex = 8;
            this.btnFindRecall.Text = "Find";
            this.btnFindRecall.UseVisualStyleBackColor = true;
            this.btnFindRecall.Click += new System.EventHandler(this.btnFindRecall_Click);
            // 
            // btnFindJudge
            // 
            this.btnFindJudge.Location = new System.Drawing.Point(476, 65);
            this.btnFindJudge.Name = "btnFindJudge";
            this.btnFindJudge.Size = new System.Drawing.Size(36, 23);
            this.btnFindJudge.TabIndex = 11;
            this.btnFindJudge.Text = "Find";
            this.btnFindJudge.UseVisualStyleBackColor = true;
            this.btnFindJudge.Click += new System.EventHandler(this.btnFindJudge_Click);
            // 
            // btnFindJail
            // 
            this.btnFindJail.Location = new System.Drawing.Point(476, 91);
            this.btnFindJail.Name = "btnFindJail";
            this.btnFindJail.Size = new System.Drawing.Size(36, 23);
            this.btnFindJail.TabIndex = 13;
            this.btnFindJail.Text = "Find";
            this.btnFindJail.UseVisualStyleBackColor = true;
            this.btnFindJail.Click += new System.EventHandler(this.btnFindJail_Click);
            // 
            // btnFindBarracks
            // 
            this.btnFindBarracks.Location = new System.Drawing.Point(476, 117);
            this.btnFindBarracks.Name = "btnFindBarracks";
            this.btnFindBarracks.Size = new System.Drawing.Size(36, 23);
            this.btnFindBarracks.TabIndex = 15;
            this.btnFindBarracks.Text = "Find";
            this.btnFindBarracks.UseVisualStyleBackColor = true;
            this.btnFindBarracks.Click += new System.EventHandler(this.btnFindBarracks_Click);
            // 
            // btnFindGuards
            // 
            this.btnFindGuards.Location = new System.Drawing.Point(476, 143);
            this.btnFindGuards.Name = "btnFindGuards";
            this.btnFindGuards.Size = new System.Drawing.Size(36, 23);
            this.btnFindGuards.TabIndex = 17;
            this.btnFindGuards.Text = "Find";
            this.btnFindGuards.UseVisualStyleBackColor = true;
            this.btnFindGuards.Click += new System.EventHandler(this.btnFindGuards_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(463, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 103;
            this.label3.Text = "Minutes";
            // 
            // cbJusticeType
            // 
            this.cbJusticeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJusticeType.FormattingEnabled = true;
            this.cbJusticeType.Location = new System.Drawing.Point(341, 39);
            this.cbJusticeType.Name = "cbJusticeType";
            this.cbJusticeType.Size = new System.Drawing.Size(171, 21);
            this.cbJusticeType.TabIndex = 104;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 105;
            this.label4.Text = "Width";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(78, 253);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(48, 20);
            this.txtWidth.TabIndex = 106;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(202, 253);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(48, 20);
            this.txtHeight.TabIndex = 107;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(143, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 108;
            this.label5.Text = "Height";
            // 
            // btnEditFlags
            // 
            this.btnEditFlags.Location = new System.Drawing.Point(448, 277);
            this.btnEditFlags.Name = "btnEditFlags";
            this.btnEditFlags.Size = new System.Drawing.Size(64, 23);
            this.btnEditFlags.TabIndex = 109;
            this.btnEditFlags.Text = "Edit Flags";
            this.btnEditFlags.UseVisualStyleBackColor = true;
            this.btnEditFlags.Click += new System.EventHandler(this.btnEditFlags_Click);
            // 
            // txtAreaFlags
            // 
            this.txtAreaFlags.Location = new System.Drawing.Point(341, 279);
            this.txtAreaFlags.Name = "txtAreaFlags";
            this.txtAreaFlags.Size = new System.Drawing.Size(101, 20);
            this.txtAreaFlags.TabIndex = 110;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(264, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 111;
            this.label6.Text = "Area Flags";
            // 
            // EditAreaSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 343);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAreaFlags);
            this.Controls.Add(this.btnEditFlags);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbJusticeType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFindGuards);
            this.Controls.Add(this.btnFindBarracks);
            this.Controls.Add(this.btnFindJail);
            this.Controls.Add(this.btnFindJudge);
            this.Controls.Add(this.btnFindRecall);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMinutesBetweenResets);
            this.Controls.Add(this.cbResetMode);
            this.Controls.Add(this.lblResetMode);
            this.Controls.Add(this.txtSquadSize);
            this.Controls.Add(this.lblSquadSize);
            this.Controls.Add(this.txtNumSquads);
            this.Controls.Add(this.lblSquads);
            this.Controls.Add(this.txtGuardIndexNumber);
            this.Controls.Add(this.lblGuards);
            this.Controls.Add(this.txtBarracks);
            this.Controls.Add(this.lblBarracks);
            this.Controls.Add(this.txtJailRoom);
            this.Controls.Add(this.lblJailRoom);
            this.Controls.Add(this.txtJudgeRoom);
            this.Controls.Add(this.lblJudgeRoom);
            this.Controls.Add(this.lblJusticeType);
            this.Controls.Add(this.txtRecall);
            this.Controls.Add(this.lblRecall);
            this.Controls.Add(this.rtbResetMsg);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtResetMsg);
            this.Controls.Add(this.lblResetMessage);
            this.Controls.Add(this.txtBuilders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbAreaName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbMaxLevel);
            this.Controls.Add(this.cbMinLevel);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblMaxLevel);
            this.Controls.Add(this.lblMinLevel);
            this.Controls.Add(this.lblAreaName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditAreaSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Area Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAreaName;
        private System.Windows.Forms.Label lblMinLevel;
        private System.Windows.Forms.Label lblMaxLevel;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.ComboBox cbMinLevel;
        private System.Windows.Forms.ComboBox cbMaxLevel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.RichTextBox rtbAreaName;
        private System.Windows.Forms.TextBox txtBuilders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblResetMessage;
        private System.Windows.Forms.TextBox txtResetMsg;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.RichTextBox rtbResetMsg;
        private System.Windows.Forms.TextBox txtRecall;
        private System.Windows.Forms.Label lblRecall;
        private System.Windows.Forms.Label lblJusticeType;
        private System.Windows.Forms.TextBox txtJudgeRoom;
        private System.Windows.Forms.Label lblJudgeRoom;
        private System.Windows.Forms.TextBox txtJailRoom;
        private System.Windows.Forms.Label lblJailRoom;
        private System.Windows.Forms.TextBox txtBarracks;
        private System.Windows.Forms.Label lblBarracks;
        private System.Windows.Forms.TextBox txtGuardIndexNumber;
        private System.Windows.Forms.Label lblGuards;
        private System.Windows.Forms.TextBox txtNumSquads;
        private System.Windows.Forms.Label lblSquads;
        private System.Windows.Forms.TextBox txtSquadSize;
        private System.Windows.Forms.Label lblSquadSize;
        private System.Windows.Forms.Label lblResetMode;
        private System.Windows.Forms.ComboBox cbResetMode;
        private System.Windows.Forms.TextBox txtMinutesBetweenResets;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFindRecall;
        private System.Windows.Forms.Button btnFindJudge;
        private System.Windows.Forms.Button btnFindJail;
        private System.Windows.Forms.Button btnFindBarracks;
        private System.Windows.Forms.Button btnFindGuards;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbJusticeType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEditFlags;
        private System.Windows.Forms.TextBox txtAreaFlags;
        private System.Windows.Forms.Label label6;

    }
}