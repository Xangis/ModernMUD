namespace SpellEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTextTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblIndex = new System.Windows.Forms.Label();
            this.chkCastCombat = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAIPower = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAIChance = new System.Windows.Forms.TextBox();
            this.chkCanBeScribed = new System.Windows.Forms.CheckBox();
            this.txtCastingTime = new System.Windows.Forms.TextBox();
            this.lblCastingTime = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDamageMessage = new System.Windows.Forms.TextBox();
            this.txtWearOffMessage = new System.Windows.Forms.TextBox();
            this.txtKillMessage = new System.Windows.Forms.TextBox();
            this.txtDamageMessageToRoom = new System.Windows.Forms.TextBox();
            this.txtDamageMessageToSelf = new System.Windows.Forms.TextBox();
            this.txtDamageMessageToVictim = new System.Windows.Forms.TextBox();
            this.txtDamageMessageSelfToRoom = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMinMana = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBaseDamage = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLevelCap = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDamageDicePerLevel = new System.Windows.Forms.TextBox();
            this.chkDetrimental = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtManaType = new System.Windows.Forms.TextBox();
            this.lblSchool = new System.Windows.Forms.Label();
            this.txtSchool = new System.Windows.Forms.TextBox();
            this.btnProvides = new System.Windows.Forms.Button();
            this.btnNegates = new System.Windows.Forms.Button();
            this.btnModifies = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.cbAIType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cbDamageType = new System.Windows.Forms.ComboBox();
            this.cbSelectSpell = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cbSpellDuration = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cbSavingThrow = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbTargetType = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbStackType = new System.Windows.Forms.ComboBox();
            this.txtCompletedMessageToRoom = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCompletedMessageToTarget = new System.Windows.Forms.TextBox();
            this.lblmsg2 = new System.Windows.Forms.Label();
            this.txtCompletedMessage = new System.Windows.Forms.TextBox();
            this.lblmsg6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(610, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.showTextTokensToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // showTextTokensToolStripMenuItem
            // 
            this.showTextTokensToolStripMenuItem.Name = "showTextTokensToolStripMenuItem";
            this.showTextTokensToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.showTextTokensToolStripMenuItem.Text = "Show Text Tokens";
            this.showTextTokensToolStripMenuItem.Click += new System.EventHandler(this.showTextTokensToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 656);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(610, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(92, 28);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(40, 23);
            this.btnPrevious.TabIndex = 2;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(138, 28);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(39, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(49, 62);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 65);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Name";
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(14, 33);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(64, 13);
            this.lblIndex.TabIndex = 6;
            this.lblIndex.Text = "XXX of YYY";
            // 
            // chkCastCombat
            // 
            this.chkCastCombat.AutoSize = true;
            this.chkCastCombat.Location = new System.Drawing.Point(15, 142);
            this.chkCastCombat.Name = "chkCastCombat";
            this.chkCastCombat.Size = new System.Drawing.Size(119, 17);
            this.chkCastCombat.TabIndex = 7;
            this.chkCastCombat.Text = "Can Cast in Combat";
            this.chkCastCombat.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 624);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "AI Power";
            // 
            // txtAIPower
            // 
            this.txtAIPower.Location = new System.Drawing.Point(61, 621);
            this.txtAIPower.Name = "txtAIPower";
            this.txtAIPower.Size = new System.Drawing.Size(47, 20);
            this.txtAIPower.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 624);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "AI Chance";
            // 
            // txtAIChance
            // 
            this.txtAIChance.Location = new System.Drawing.Point(171, 621);
            this.txtAIChance.Name = "txtAIChance";
            this.txtAIChance.Size = new System.Drawing.Size(42, 20);
            this.txtAIChance.TabIndex = 11;
            // 
            // chkCanBeScribed
            // 
            this.chkCanBeScribed.AutoSize = true;
            this.chkCanBeScribed.Location = new System.Drawing.Point(147, 142);
            this.chkCanBeScribed.Name = "chkCanBeScribed";
            this.chkCanBeScribed.Size = new System.Drawing.Size(100, 17);
            this.chkCanBeScribed.TabIndex = 12;
            this.chkCanBeScribed.Text = "Can Be Scribed";
            this.chkCanBeScribed.UseVisualStyleBackColor = true;
            // 
            // txtCastingTime
            // 
            this.txtCastingTime.Location = new System.Drawing.Point(256, 113);
            this.txtCastingTime.Name = "txtCastingTime";
            this.txtCastingTime.Size = new System.Drawing.Size(55, 20);
            this.txtCastingTime.TabIndex = 13;
            // 
            // lblCastingTime
            // 
            this.lblCastingTime.AutoSize = true;
            this.lblCastingTime.Location = new System.Drawing.Point(196, 116);
            this.lblCastingTime.Name = "lblCastingTime";
            this.lblCastingTime.Size = new System.Drawing.Size(54, 13);
            this.lblCastingTime.TabIndex = 14;
            this.lblCastingTime.Text = "Cast Time";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(12, 417);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCode.Size = new System.Drawing.Size(583, 197);
            this.txtCode.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 401);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Code";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 288);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Damage Message (to caster)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Wear Off Message";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Kill Message";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 311);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Damage Message (to room)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 333);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Damage Message (to self)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 355);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(141, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Damage Message (to victim)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 377);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(156, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Damage Message (self to room)";
            // 
            // txtDamageMessage
            // 
            this.txtDamageMessage.Location = new System.Drawing.Point(171, 285);
            this.txtDamageMessage.Name = "txtDamageMessage";
            this.txtDamageMessage.Size = new System.Drawing.Size(424, 20);
            this.txtDamageMessage.TabIndex = 24;
            // 
            // txtWearOffMessage
            // 
            this.txtWearOffMessage.Location = new System.Drawing.Point(171, 238);
            this.txtWearOffMessage.Name = "txtWearOffMessage";
            this.txtWearOffMessage.Size = new System.Drawing.Size(424, 20);
            this.txtWearOffMessage.TabIndex = 25;
            // 
            // txtKillMessage
            // 
            this.txtKillMessage.Location = new System.Drawing.Point(171, 262);
            this.txtKillMessage.Name = "txtKillMessage";
            this.txtKillMessage.Size = new System.Drawing.Size(424, 20);
            this.txtKillMessage.TabIndex = 26;
            // 
            // txtDamageMessageToRoom
            // 
            this.txtDamageMessageToRoom.Location = new System.Drawing.Point(171, 308);
            this.txtDamageMessageToRoom.Name = "txtDamageMessageToRoom";
            this.txtDamageMessageToRoom.Size = new System.Drawing.Size(424, 20);
            this.txtDamageMessageToRoom.TabIndex = 27;
            // 
            // txtDamageMessageToSelf
            // 
            this.txtDamageMessageToSelf.Location = new System.Drawing.Point(171, 330);
            this.txtDamageMessageToSelf.Name = "txtDamageMessageToSelf";
            this.txtDamageMessageToSelf.Size = new System.Drawing.Size(424, 20);
            this.txtDamageMessageToSelf.TabIndex = 28;
            // 
            // txtDamageMessageToVictim
            // 
            this.txtDamageMessageToVictim.Location = new System.Drawing.Point(171, 352);
            this.txtDamageMessageToVictim.Name = "txtDamageMessageToVictim";
            this.txtDamageMessageToVictim.Size = new System.Drawing.Size(424, 20);
            this.txtDamageMessageToVictim.TabIndex = 29;
            // 
            // txtDamageMessageSelfToRoom
            // 
            this.txtDamageMessageSelfToRoom.Location = new System.Drawing.Point(171, 374);
            this.txtDamageMessageSelfToRoom.Name = "txtDamageMessageSelfToRoom";
            this.txtDamageMessageSelfToRoom.Size = new System.Drawing.Size(424, 20);
            this.txtDamageMessageSelfToRoom.TabIndex = 30;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(315, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Min Mana";
            // 
            // txtMinMana
            // 
            this.txtMinMana.Location = new System.Drawing.Point(375, 113);
            this.txtMinMana.Name = "txtMinMana";
            this.txtMinMana.Size = new System.Drawing.Size(51, 20);
            this.txtMinMana.TabIndex = 32;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(427, 116);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Base Damage";
            // 
            // txtBaseDamage
            // 
            this.txtBaseDamage.Location = new System.Drawing.Point(507, 113);
            this.txtBaseDamage.Name = "txtBaseDamage";
            this.txtBaseDamage.Size = new System.Drawing.Size(88, 20);
            this.txtBaseDamage.TabIndex = 34;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(253, 143);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "Level Cap";
            // 
            // txtLevelCap
            // 
            this.txtLevelCap.Location = new System.Drawing.Point(314, 140);
            this.txtLevelCap.Name = "txtLevelCap";
            this.txtLevelCap.Size = new System.Drawing.Size(61, 20);
            this.txtLevelCap.TabIndex = 36;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(381, 143);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "Damage Dice Per Level";
            // 
            // txtDamageDicePerLevel
            // 
            this.txtDamageDicePerLevel.Location = new System.Drawing.Point(507, 140);
            this.txtDamageDicePerLevel.Name = "txtDamageDicePerLevel";
            this.txtDamageDicePerLevel.Size = new System.Drawing.Size(88, 20);
            this.txtDamageDicePerLevel.TabIndex = 38;
            // 
            // chkDetrimental
            // 
            this.chkDetrimental.AutoSize = true;
            this.chkDetrimental.Location = new System.Drawing.Point(519, 64);
            this.chkDetrimental.Name = "chkDetrimental";
            this.chkDetrimental.Size = new System.Drawing.Size(79, 17);
            this.chkDetrimental.TabIndex = 39;
            this.chkDetrimental.Text = "Detrimental";
            this.chkDetrimental.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(276, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "Mana Type";
            // 
            // txtManaType
            // 
            this.txtManaType.Location = new System.Drawing.Point(343, 62);
            this.txtManaType.Name = "txtManaType";
            this.txtManaType.Size = new System.Drawing.Size(50, 20);
            this.txtManaType.TabIndex = 41;
            // 
            // lblSchool
            // 
            this.lblSchool.AutoSize = true;
            this.lblSchool.Location = new System.Drawing.Point(155, 65);
            this.lblSchool.Name = "lblSchool";
            this.lblSchool.Size = new System.Drawing.Size(40, 13);
            this.lblSchool.TabIndex = 42;
            this.lblSchool.Text = "School";
            // 
            // txtSchool
            // 
            this.txtSchool.Location = new System.Drawing.Point(199, 62);
            this.txtSchool.Name = "txtSchool";
            this.txtSchool.Size = new System.Drawing.Size(71, 20);
            this.txtSchool.TabIndex = 43;
            // 
            // btnProvides
            // 
            this.btnProvides.Location = new System.Drawing.Point(361, 619);
            this.btnProvides.Name = "btnProvides";
            this.btnProvides.Size = new System.Drawing.Size(75, 23);
            this.btnProvides.TabIndex = 44;
            this.btnProvides.Text = "Provides";
            this.btnProvides.UseVisualStyleBackColor = true;
            this.btnProvides.Click += new System.EventHandler(this.btnProvides_Click);
            // 
            // btnNegates
            // 
            this.btnNegates.Location = new System.Drawing.Point(442, 619);
            this.btnNegates.Name = "btnNegates";
            this.btnNegates.Size = new System.Drawing.Size(75, 23);
            this.btnNegates.TabIndex = 45;
            this.btnNegates.Text = "Negates";
            this.btnNegates.UseVisualStyleBackColor = true;
            this.btnNegates.Click += new System.EventHandler(this.btnNegates_Click);
            // 
            // btnModifies
            // 
            this.btnModifies.Location = new System.Drawing.Point(523, 619);
            this.btnModifies.Name = "btnModifies";
            this.btnModifies.Size = new System.Drawing.Size(75, 23);
            this.btnModifies.TabIndex = 46;
            this.btnModifies.Text = "Modifies";
            this.btnModifies.UseVisualStyleBackColor = true;
            this.btnModifies.Click += new System.EventHandler(this.btnModifies_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(219, 624);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 13);
            this.label16.TabIndex = 47;
            this.label16.Text = "AI Type";
            // 
            // cbAIType
            // 
            this.cbAIType.FormattingEnabled = true;
            this.cbAIType.Location = new System.Drawing.Point(267, 621);
            this.cbAIType.Name = "cbAIType";
            this.cbAIType.Size = new System.Drawing.Size(85, 21);
            this.cbAIType.Sorted = true;
            this.cbAIType.TabIndex = 48;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 116);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 13);
            this.label17.TabIndex = 49;
            this.label17.Text = "Damage Type";
            // 
            // cbDamageType
            // 
            this.cbDamageType.FormattingEnabled = true;
            this.cbDamageType.Location = new System.Drawing.Point(92, 113);
            this.cbDamageType.Name = "cbDamageType";
            this.cbDamageType.Size = new System.Drawing.Size(98, 21);
            this.cbDamageType.TabIndex = 50;
            // 
            // cbSelectSpell
            // 
            this.cbSelectSpell.FormattingEnabled = true;
            this.cbSelectSpell.Location = new System.Drawing.Point(183, 30);
            this.cbSelectSpell.Name = "cbSelectSpell";
            this.cbSelectSpell.Size = new System.Drawing.Size(121, 21);
            this.cbSelectSpell.Sorted = true;
            this.cbSelectSpell.TabIndex = 51;
            this.cbSelectSpell.SelectedIndexChanged += new System.EventHandler(this.cbSelectSpell_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 91);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 13);
            this.label18.TabIndex = 52;
            this.label18.Text = "Duration";
            // 
            // cbSpellDuration
            // 
            this.cbSpellDuration.FormattingEnabled = true;
            this.cbSpellDuration.Location = new System.Drawing.Point(69, 88);
            this.cbSpellDuration.Name = "cbSpellDuration";
            this.cbSpellDuration.Size = new System.Drawing.Size(121, 21);
            this.cbSpellDuration.TabIndex = 53;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(197, 91);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 13);
            this.label19.TabIndex = 54;
            this.label19.Text = "Saving Throw";
            // 
            // cbSavingThrow
            // 
            this.cbSavingThrow.FormattingEnabled = true;
            this.cbSavingThrow.Location = new System.Drawing.Point(276, 88);
            this.cbSavingThrow.Name = "cbSavingThrow";
            this.cbSavingThrow.Size = new System.Drawing.Size(136, 21);
            this.cbSavingThrow.TabIndex = 55;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(418, 91);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 13);
            this.label20.TabIndex = 56;
            this.label20.Text = "Target";
            // 
            // cbTargetType
            // 
            this.cbTargetType.FormattingEnabled = true;
            this.cbTargetType.Location = new System.Drawing.Point(462, 88);
            this.cbTargetType.Name = "cbTargetType";
            this.cbTargetType.Size = new System.Drawing.Size(133, 21);
            this.cbTargetType.TabIndex = 57;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(401, 65);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 13);
            this.label21.TabIndex = 58;
            this.label21.Text = "Stack";
            // 
            // cbStackType
            // 
            this.cbStackType.FormattingEnabled = true;
            this.cbStackType.Location = new System.Drawing.Point(438, 62);
            this.cbStackType.Name = "cbStackType";
            this.cbStackType.Size = new System.Drawing.Size(75, 21);
            this.cbStackType.TabIndex = 59;
            // 
            // txtCompletedMessageToRoom
            // 
            this.txtCompletedMessageToRoom.Location = new System.Drawing.Point(171, 215);
            this.txtCompletedMessageToRoom.Name = "txtCompletedMessageToRoom";
            this.txtCompletedMessageToRoom.Size = new System.Drawing.Size(424, 20);
            this.txtCompletedMessageToRoom.TabIndex = 61;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 218);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(149, 13);
            this.label22.TabIndex = 60;
            this.label22.Text = "Completion Message (to room)";
            // 
            // txtCompletedMessageToTarget
            // 
            this.txtCompletedMessageToTarget.Location = new System.Drawing.Point(171, 192);
            this.txtCompletedMessageToTarget.Name = "txtCompletedMessageToTarget";
            this.txtCompletedMessageToTarget.Size = new System.Drawing.Size(424, 20);
            this.txtCompletedMessageToTarget.TabIndex = 63;
            // 
            // lblmsg2
            // 
            this.lblmsg2.AutoSize = true;
            this.lblmsg2.Location = new System.Drawing.Point(12, 195);
            this.lblmsg2.Name = "lblmsg2";
            this.lblmsg2.Size = new System.Drawing.Size(153, 13);
            this.lblmsg2.TabIndex = 62;
            this.lblmsg2.Text = "Completion Message (to target)";
            // 
            // txtCompletedMessage
            // 
            this.txtCompletedMessage.Location = new System.Drawing.Point(171, 170);
            this.txtCompletedMessage.Name = "txtCompletedMessage";
            this.txtCompletedMessage.Size = new System.Drawing.Size(424, 20);
            this.txtCompletedMessage.TabIndex = 65;
            // 
            // lblmsg6
            // 
            this.lblmsg6.AutoSize = true;
            this.lblmsg6.Location = new System.Drawing.Point(12, 173);
            this.lblmsg6.Name = "lblmsg6";
            this.lblmsg6.Size = new System.Drawing.Size(155, 13);
            this.lblmsg6.TabIndex = 64;
            this.lblmsg6.Text = "Completion Message (to caster)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 678);
            this.Controls.Add(this.txtCompletedMessage);
            this.Controls.Add(this.lblmsg6);
            this.Controls.Add(this.txtCompletedMessageToTarget);
            this.Controls.Add(this.lblmsg2);
            this.Controls.Add(this.txtCompletedMessageToRoom);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.cbStackType);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cbTargetType);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cbSavingThrow);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.cbSpellDuration);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cbSelectSpell);
            this.Controls.Add(this.cbDamageType);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cbAIType);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnModifies);
            this.Controls.Add(this.btnNegates);
            this.Controls.Add(this.btnProvides);
            this.Controls.Add(this.txtSchool);
            this.Controls.Add(this.lblSchool);
            this.Controls.Add(this.txtManaType);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.chkDetrimental);
            this.Controls.Add(this.txtDamageDicePerLevel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtLevelCap);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtBaseDamage);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtMinMana);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDamageMessageSelfToRoom);
            this.Controls.Add(this.txtDamageMessageToVictim);
            this.Controls.Add(this.txtDamageMessageToSelf);
            this.Controls.Add(this.txtDamageMessageToRoom);
            this.Controls.Add(this.txtKillMessage);
            this.Controls.Add(this.txtWearOffMessage);
            this.Controls.Add(this.txtDamageMessage);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCastingTime);
            this.Controls.Add(this.txtCastingTime);
            this.Controls.Add(this.chkCanBeScribed);
            this.Controls.Add(this.txtAIChance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAIPower);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkCastCombat);
            this.Controls.Add(this.lblIndex);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Basternae Spell Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.CheckBox chkCastCombat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAIPower;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAIChance;
        private System.Windows.Forms.CheckBox chkCanBeScribed;
        private System.Windows.Forms.TextBox txtCastingTime;
        private System.Windows.Forms.Label lblCastingTime;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDamageMessage;
        private System.Windows.Forms.TextBox txtWearOffMessage;
        private System.Windows.Forms.TextBox txtKillMessage;
        private System.Windows.Forms.TextBox txtDamageMessageToRoom;
        private System.Windows.Forms.TextBox txtDamageMessageToSelf;
        private System.Windows.Forms.TextBox txtDamageMessageToVictim;
        private System.Windows.Forms.TextBox txtDamageMessageSelfToRoom;
        private System.Windows.Forms.ToolStripMenuItem showTextTokensToolStripMenuItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMinMana;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBaseDamage;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtLevelCap;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDamageDicePerLevel;
        private System.Windows.Forms.CheckBox chkDetrimental;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtManaType;
        private System.Windows.Forms.Label lblSchool;
        private System.Windows.Forms.TextBox txtSchool;
        private System.Windows.Forms.Button btnProvides;
        private System.Windows.Forms.Button btnNegates;
        private System.Windows.Forms.Button btnModifies;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbAIType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbDamageType;
        private System.Windows.Forms.ComboBox cbSelectSpell;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbSpellDuration;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbSavingThrow;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbTargetType;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbStackType;
        private System.Windows.Forms.TextBox txtCompletedMessageToRoom;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtCompletedMessageToTarget;
        private System.Windows.Forms.Label lblmsg2;
        private System.Windows.Forms.TextBox txtCompletedMessage;
        private System.Windows.Forms.Label lblmsg6;
    }
}

