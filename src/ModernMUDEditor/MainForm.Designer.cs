namespace ModernMUDEditor
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
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renumberZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showColorCodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.knownIssuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripFilename = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMobs = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripObjects = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripRooms = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripShops = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripQuests = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripResets = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.showMobs = new System.Windows.Forms.ToolStripButton();
            this.showObjects = new System.Windows.Forms.ToolStripButton();
            this.showRooms = new System.Windows.Forms.ToolStripButton();
            this.showShops = new System.Windows.Forms.ToolStripButton();
            this.showQuests = new System.Windows.Forms.ToolStripButton();
            this.showResets = new System.Windows.Forms.ToolStripButton();
            this.showAreaSettings = new System.Windows.Forms.ToolStripButton();
            this.btnEditRepopPoints = new System.Windows.Forms.ToolStripButton();
            this.btnNorth = new System.Windows.Forms.ToolStripButton();
            this.btnSouth = new System.Windows.Forms.ToolStripButton();
            this.btnWest = new System.Windows.Forms.ToolStripButton();
            this.btnEast = new System.Windows.Forms.ToolStripButton();
            this.btnUp = new System.Windows.Forms.ToolStripButton();
            this.btnDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.lblXOffset = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.lblYOffset = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lblLevel = new System.Windows.Forms.ToolStripLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMapView = new System.Windows.Forms.TabPage();
            this.tabWalkthrough = new System.Windows.Forms.TabPage();
            this.txtInputText = new System.Windows.Forms.TextBox();
            this.txtOutputText = new System.Windows.Forms.RichTextBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabWalkthrough.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(774, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.newToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Sa&ve As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renumberZoneToolStripMenuItem,
            this.checkZoneToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // renumberZoneToolStripMenuItem
            // 
            this.renumberZoneToolStripMenuItem.Name = "renumberZoneToolStripMenuItem";
            this.renumberZoneToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.renumberZoneToolStripMenuItem.Text = "Renumber Area";
            this.renumberZoneToolStripMenuItem.Click += new System.EventHandler(this.renumberZoneToolStripMenuItem_Click);
            // 
            // checkZoneToolStripMenuItem
            // 
            this.checkZoneToolStripMenuItem.Name = "checkZoneToolStripMenuItem";
            this.checkZoneToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.checkZoneToolStripMenuItem.Text = "Check Area";
            this.checkZoneToolStripMenuItem.Click += new System.EventHandler(this.checkZoneToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem,
            this.showColorCodesToolStripMenuItem,
            this.knownIssuesToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // showColorCodesToolStripMenuItem
            // 
            this.showColorCodesToolStripMenuItem.Name = "showColorCodesToolStripMenuItem";
            this.showColorCodesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.showColorCodesToolStripMenuItem.Text = "Show Co&lor Codes";
            this.showColorCodesToolStripMenuItem.Click += new System.EventHandler(this.showColorCodesToolStripMenuItem_Click);
            // 
            // knownIssuesToolStripMenuItem
            // 
            this.knownIssuesToolStripMenuItem.Name = "knownIssuesToolStripMenuItem";
            this.knownIssuesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.knownIssuesToolStripMenuItem.Text = "&Known Issues";
            this.knownIssuesToolStripMenuItem.Click += new System.EventHandler(this.knownIssuesToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripFilename,
            this.toolStripMobs,
            this.toolStripObjects,
            this.toolStripRooms,
            this.toolStripShops,
            this.toolStripQuests,
            this.toolStripResets});
            this.statusStrip1.Location = new System.Drawing.Point(0, 512);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(774, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(100, 17);
            this.toolStripStatusLabel1.Text = "(No Area Loaded)";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // toolStripFilename
            // 
            this.toolStripFilename.Name = "toolStripFilename";
            this.toolStripFilename.Size = new System.Drawing.Size(52, 17);
            this.toolStripFilename.Text = "[No File]";
            // 
            // toolStripMobs
            // 
            this.toolStripMobs.Name = "toolStripMobs";
            this.toolStripMobs.Size = new System.Drawing.Size(49, 17);
            this.toolStripMobs.Text = "Mobs: 0";
            // 
            // toolStripObjects
            // 
            this.toolStripObjects.Name = "toolStripObjects";
            this.toolStripObjects.Size = new System.Drawing.Size(59, 17);
            this.toolStripObjects.Text = "Objects: 0";
            this.toolStripObjects.Click += new System.EventHandler(this.toolStripObjects_Click);
            // 
            // toolStripRooms
            // 
            this.toolStripRooms.Name = "toolStripRooms";
            this.toolStripRooms.Size = new System.Drawing.Size(56, 17);
            this.toolStripRooms.Text = "Rooms: 0";
            // 
            // toolStripShops
            // 
            this.toolStripShops.Name = "toolStripShops";
            this.toolStripShops.Size = new System.Drawing.Size(51, 17);
            this.toolStripShops.Text = "Shops: 0";
            // 
            // toolStripQuests
            // 
            this.toolStripQuests.Name = "toolStripQuests";
            this.toolStripQuests.Size = new System.Drawing.Size(55, 17);
            this.toolStripQuests.Text = "Quests: 0";
            // 
            // toolStripResets
            // 
            this.toolStripResets.Name = "toolStripResets";
            this.toolStripResets.Size = new System.Drawing.Size(52, 17);
            this.toolStripResets.Text = "Resets: 0";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMobs,
            this.showObjects,
            this.showRooms,
            this.showShops,
            this.showQuests,
            this.showResets,
            this.showAreaSettings,
            this.btnEditRepopPoints,
            this.btnNorth,
            this.btnSouth,
            this.btnWest,
            this.btnEast,
            this.btnUp,
            this.btnDown,
            this.toolStripLabel3,
            this.lblXOffset,
            this.toolStripLabel4,
            this.lblYOffset,
            this.toolStripLabel1,
            this.lblLevel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(774, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // showMobs
            // 
            this.showMobs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showMobs.Image = global::ModernMUDEditor.Properties.Resources.Mobile1;
            this.showMobs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showMobs.Name = "showMobs";
            this.showMobs.Size = new System.Drawing.Size(23, 22);
            this.showMobs.Text = "toolStripButton1";
            this.showMobs.ToolTipText = "Edit Mobs";
            this.showMobs.Click += new System.EventHandler(this.showMobs_Click);
            // 
            // showObjects
            // 
            this.showObjects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showObjects.Image = global::ModernMUDEditor.Properties.Resources.Object1;
            this.showObjects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showObjects.Name = "showObjects";
            this.showObjects.Size = new System.Drawing.Size(23, 22);
            this.showObjects.Text = "toolStripButton2";
            this.showObjects.ToolTipText = "Edit Objects";
            this.showObjects.Click += new System.EventHandler(this.showObjects_Click);
            // 
            // showRooms
            // 
            this.showRooms.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showRooms.Image = global::ModernMUDEditor.Properties.Resources.Room1;
            this.showRooms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showRooms.Name = "showRooms";
            this.showRooms.Size = new System.Drawing.Size(23, 22);
            this.showRooms.Text = "toolStripButton3";
            this.showRooms.ToolTipText = "Edit Rooms";
            this.showRooms.Click += new System.EventHandler(this.showRooms_Click);
            // 
            // showShops
            // 
            this.showShops.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showShops.Image = global::ModernMUDEditor.Properties.Resources.Shop1;
            this.showShops.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showShops.Name = "showShops";
            this.showShops.Size = new System.Drawing.Size(23, 22);
            this.showShops.Text = "toolStripButton4";
            this.showShops.ToolTipText = "Edit Shops";
            this.showShops.Click += new System.EventHandler(this.showShops_Click);
            // 
            // showQuests
            // 
            this.showQuests.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showQuests.Image = global::ModernMUDEditor.Properties.Resources.Quest1;
            this.showQuests.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showQuests.Name = "showQuests";
            this.showQuests.Size = new System.Drawing.Size(23, 22);
            this.showQuests.Text = "toolStripButton5";
            this.showQuests.ToolTipText = "Edit Quests";
            this.showQuests.Click += new System.EventHandler(this.showQuests_Click);
            // 
            // showResets
            // 
            this.showResets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showResets.Image = global::ModernMUDEditor.Properties.Resources.Reset1;
            this.showResets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showResets.Name = "showResets";
            this.showResets.Size = new System.Drawing.Size(23, 22);
            this.showResets.Text = "toolStripButton6";
            this.showResets.ToolTipText = "Edit Resets";
            this.showResets.Click += new System.EventHandler(this.showResets_Click);
            // 
            // showAreaSettings
            // 
            this.showAreaSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showAreaSettings.Image = global::ModernMUDEditor.Properties.Resources.Settings1;
            this.showAreaSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showAreaSettings.Name = "showAreaSettings";
            this.showAreaSettings.Size = new System.Drawing.Size(23, 22);
            this.showAreaSettings.Text = "toolStripButton1";
            this.showAreaSettings.ToolTipText = "Edit Area Settings";
            this.showAreaSettings.Click += new System.EventHandler(this.showAreaSettings_Click);
            // 
            // btnEditRepopPoints
            // 
            this.btnEditRepopPoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditRepopPoints.Image = global::ModernMUDEditor.Properties.Resources.Repop1;
            this.btnEditRepopPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditRepopPoints.Name = "btnEditRepopPoints";
            this.btnEditRepopPoints.Size = new System.Drawing.Size(23, 22);
            this.btnEditRepopPoints.Text = "toolStripButton1";
            this.btnEditRepopPoints.ToolTipText = "Edit Repop Points";
            this.btnEditRepopPoints.Click += new System.EventHandler(this.btnEditRepopPoints_Click);
            // 
            // btnNorth
            // 
            this.btnNorth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNorth.Image = global::ModernMUDEditor.Properties.Resources.up;
            this.btnNorth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNorth.Name = "btnNorth";
            this.btnNorth.Size = new System.Drawing.Size(23, 22);
            this.btnNorth.Text = "Move Map North";
            this.btnNorth.Click += new System.EventHandler(this.btnNorth_Click);
            // 
            // btnSouth
            // 
            this.btnSouth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSouth.Image = global::ModernMUDEditor.Properties.Resources.down;
            this.btnSouth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSouth.Name = "btnSouth";
            this.btnSouth.Size = new System.Drawing.Size(23, 22);
            this.btnSouth.Text = "Move Map South";
            this.btnSouth.Click += new System.EventHandler(this.btnSouth_Click);
            // 
            // btnWest
            // 
            this.btnWest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWest.Image = global::ModernMUDEditor.Properties.Resources.left;
            this.btnWest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWest.Name = "btnWest";
            this.btnWest.Size = new System.Drawing.Size(23, 22);
            this.btnWest.Text = "Move Map West";
            this.btnWest.Click += new System.EventHandler(this.btnWest_Click);
            // 
            // btnEast
            // 
            this.btnEast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEast.Image = global::ModernMUDEditor.Properties.Resources.right;
            this.btnEast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEast.Name = "btnEast";
            this.btnEast.Size = new System.Drawing.Size(23, 22);
            this.btnEast.Text = "Move Map East";
            this.btnEast.Click += new System.EventHandler(this.btnEast_Click);
            // 
            // btnUp
            // 
            this.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUp.Image = global::ModernMUDEditor.Properties.Resources.goup;
            this.btnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(23, 22);
            this.btnUp.Text = "Up A Level";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDown.Image = global::ModernMUDEditor.Properties.Resources.godown;
            this.btnDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(23, 22);
            this.btnDown.Text = "Down A Level";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(17, 22);
            this.toolStripLabel3.Text = "X:";
            // 
            // lblXOffset
            // 
            this.lblXOffset.Name = "lblXOffset";
            this.lblXOffset.Size = new System.Drawing.Size(25, 22);
            this.lblXOffset.Text = "000";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(17, 22);
            this.toolStripLabel4.Text = "Y:";
            // 
            // lblYOffset
            // 
            this.lblYOffset.Name = "lblYOffset";
            this.lblYOffset.Size = new System.Drawing.Size(25, 22);
            this.lblYOffset.Text = "000";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel1.Text = "Level";
            // 
            // lblLevel
            // 
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(53, 22);
            this.lblLevel.Text = "0 (0 to 0)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMapView);
            this.tabControl1.Controls.Add(this.tabWalkthrough);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(774, 463);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabMapView
            // 
            this.tabMapView.AllowDrop = true;
            this.tabMapView.Location = new System.Drawing.Point(4, 22);
            this.tabMapView.Name = "tabMapView";
            this.tabMapView.Padding = new System.Windows.Forms.Padding(3);
            this.tabMapView.Size = new System.Drawing.Size(766, 437);
            this.tabMapView.TabIndex = 0;
            this.tabMapView.Text = "Map View";
            this.tabMapView.UseVisualStyleBackColor = true;
            this.tabMapView.Paint += new System.Windows.Forms.PaintEventHandler(this.tabMapView_Paint);
            this.tabMapView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabMapView_MouseClick);
            // 
            // tabWalkthrough
            // 
            this.tabWalkthrough.BackColor = System.Drawing.Color.Black;
            this.tabWalkthrough.Controls.Add(this.txtInputText);
            this.tabWalkthrough.Controls.Add(this.txtOutputText);
            this.tabWalkthrough.ForeColor = System.Drawing.Color.White;
            this.tabWalkthrough.Location = new System.Drawing.Point(4, 22);
            this.tabWalkthrough.Name = "tabWalkthrough";
            this.tabWalkthrough.Padding = new System.Windows.Forms.Padding(3);
            this.tabWalkthrough.Size = new System.Drawing.Size(766, 437);
            this.tabWalkthrough.TabIndex = 1;
            this.tabWalkthrough.Text = "Walkthrough";
            // 
            // txtInputText
            // 
            this.txtInputText.AcceptsReturn = true;
            this.txtInputText.Location = new System.Drawing.Point(6, 411);
            this.txtInputText.Multiline = true;
            this.txtInputText.Name = "txtInputText";
            this.txtInputText.Size = new System.Drawing.Size(754, 20);
            this.txtInputText.TabIndex = 1;
            this.txtInputText.TextChanged += new System.EventHandler(this.txtInputText_TextChanged);
            // 
            // txtOutputText
            // 
            this.txtOutputText.BackColor = System.Drawing.Color.Black;
            this.txtOutputText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutputText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputText.ForeColor = System.Drawing.Color.White;
            this.txtOutputText.Location = new System.Drawing.Point(6, 6);
            this.txtOutputText.Name = "txtOutputText";
            this.txtOutputText.ReadOnly = true;
            this.txtOutputText.Size = new System.Drawing.Size(754, 388);
            this.txtOutputText.TabIndex = 0;
            this.txtOutputText.Text = "";
            // 
            // helpProvider1
            // 
            this.helpProvider1.HelpNamespace = "EditorHelp\\editorhelp.chm";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 534);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "ModernMUD Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabWalkthrough.ResumeLayout(false);
            this.tabWalkthrough.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosing += MainForm_FormClosing;
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton showMobs;
        private System.Windows.Forms.ToolStripButton showObjects;
        private System.Windows.Forms.ToolStripButton showRooms;
        private System.Windows.Forms.ToolStripButton showShops;
        private System.Windows.Forms.ToolStripButton showQuests;
        private System.Windows.Forms.ToolStripButton showResets;
        private System.Windows.Forms.ToolStripStatusLabel toolStripMobs;
        private System.Windows.Forms.ToolStripStatusLabel toolStripObjects;
        private System.Windows.Forms.ToolStripStatusLabel toolStripRooms;
        private System.Windows.Forms.ToolStripButton showAreaSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripShops;
        private System.Windows.Forms.ToolStripStatusLabel toolStripQuests;
        private System.Windows.Forms.ToolStripStatusLabel toolStripResets;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMapView;
        private System.Windows.Forms.TabPage tabWalkthrough;
        private System.Windows.Forms.ToolStripMenuItem showColorCodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnEditRepopPoints;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripFilename;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.TextBox txtInputText;
        private System.Windows.Forms.RichTextBox txtOutputText;
        private System.Windows.Forms.ToolStripMenuItem knownIssuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renumberZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnNorth;
        private System.Windows.Forms.ToolStripButton btnSouth;
        private System.Windows.Forms.ToolStripButton btnWest;
        private System.Windows.Forms.ToolStripButton btnEast;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lblLevel;
        private System.Windows.Forms.ToolStripButton btnUp;
        private System.Windows.Forms.ToolStripButton btnDown;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel lblXOffset;
        private System.Windows.Forms.ToolStripLabel lblYOffset;
        private System.Windows.Forms.ToolStripMenuItem checkZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

