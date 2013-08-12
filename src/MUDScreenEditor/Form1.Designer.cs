namespace MUDScreenEditor
{
    partial class MudScreenEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MudScreenEditor));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewScreen = new System.Windows.Forms.ToolStripButton();
            this.btnRed = new System.Windows.Forms.ToolStripButton();
            this.btnGreen = new System.Windows.Forms.ToolStripButton();
            this.btnBlue = new System.Windows.Forms.ToolStripButton();
            this.btnCyan = new System.Windows.Forms.ToolStripButton();
            this.btnMagenta = new System.Windows.Forms.ToolStripButton();
            this.btnYellow = new System.Windows.Forms.ToolStripButton();
            this.btnBlack = new System.Windows.Forms.ToolStripButton();
            this.btnWhite = new System.Windows.Forms.ToolStripButton();
            this.btnBrtRed = new System.Windows.Forms.ToolStripButton();
            this.btnBrtGreen = new System.Windows.Forms.ToolStripButton();
            this.btnBrtBlue = new System.Windows.Forms.ToolStripButton();
            this.btnBrtCyan = new System.Windows.Forms.ToolStripButton();
            this.btnBrtMagenta = new System.Windows.Forms.ToolStripButton();
            this.btnBrtYellow = new System.Windows.Forms.ToolStripButton();
            this.btnBrtBlack = new System.Windows.Forms.ToolStripButton();
            this.btnBrtWhite = new System.Windows.Forms.ToolStripButton();
            this.cbScreenType = new System.Windows.Forms.ComboBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.lblPosition = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 79);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(561, 224);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 309);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(561, 223);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(585, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.importScreenToolStripMenuItem,
            this.exportScreenToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // importScreenToolStripMenuItem
            // 
            this.importScreenToolStripMenuItem.Name = "importScreenToolStripMenuItem";
            this.importScreenToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.importScreenToolStripMenuItem.Text = "&Import Screen";
            this.importScreenToolStripMenuItem.Click += new System.EventHandler(this.importScreenToolStripMenuItem_Click);
            // 
            // exportScreenToolStripMenuItem
            // 
            this.exportScreenToolStripMenuItem.Name = "exportScreenToolStripMenuItem";
            this.exportScreenToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exportScreenToolStripMenuItem.Text = "E&xport Screen";
            this.exportScreenToolStripMenuItem.Click += new System.EventHandler(this.exportScreenToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem1.Text = "He&lp";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewScreen,
            this.btnRed,
            this.btnGreen,
            this.btnBlue,
            this.btnCyan,
            this.btnMagenta,
            this.btnYellow,
            this.btnBlack,
            this.btnWhite,
            this.btnBrtRed,
            this.btnBrtGreen,
            this.btnBrtBlue,
            this.btnBrtCyan,
            this.btnBrtMagenta,
            this.btnBrtYellow,
            this.btnBrtBlack,
            this.btnBrtWhite});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(585, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNewScreen
            // 
            this.btnNewScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewScreen.Image = global::MUDScreenEditor.Properties.Resources.Icon17;
            this.btnNewScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewScreen.Name = "btnNewScreen";
            this.btnNewScreen.Size = new System.Drawing.Size(23, 22);
            this.btnNewScreen.Text = "toolStripButton1";
            this.btnNewScreen.ToolTipText = "New Screen";
            this.btnNewScreen.Click += new System.EventHandler(this.btnNewScreen_Click);
            // 
            // btnRed
            // 
            this.btnRed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRed.Image = global::MUDScreenEditor.Properties.Resources.Icon1;
            this.btnRed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(23, 22);
            this.btnRed.Text = "toolStripButton1";
            this.btnRed.ToolTipText = "Red";
            this.btnRed.Click += new System.EventHandler(this.btnRed_Click);
            // 
            // btnGreen
            // 
            this.btnGreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGreen.Image = global::MUDScreenEditor.Properties.Resources.Icon15;
            this.btnGreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(23, 22);
            this.btnGreen.Text = "toolStripButton2";
            this.btnGreen.ToolTipText = "Green";
            this.btnGreen.Click += new System.EventHandler(this.btnGreen_Click);
            // 
            // btnBlue
            // 
            this.btnBlue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBlue.Image = global::MUDScreenEditor.Properties.Resources.Icon3;
            this.btnBlue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(23, 22);
            this.btnBlue.Text = "toolStripButton3";
            this.btnBlue.ToolTipText = "Blue";
            this.btnBlue.Click += new System.EventHandler(this.btnBlue_Click);
            // 
            // btnCyan
            // 
            this.btnCyan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCyan.Image = global::MUDScreenEditor.Properties.Resources.Icon2;
            this.btnCyan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCyan.Name = "btnCyan";
            this.btnCyan.Size = new System.Drawing.Size(23, 22);
            this.btnCyan.Text = "toolStripButton4";
            this.btnCyan.ToolTipText = "Cyan";
            this.btnCyan.Click += new System.EventHandler(this.btnCyan_Click);
            // 
            // btnMagenta
            // 
            this.btnMagenta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMagenta.Image = global::MUDScreenEditor.Properties.Resources.Icon5;
            this.btnMagenta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMagenta.Name = "btnMagenta";
            this.btnMagenta.Size = new System.Drawing.Size(23, 22);
            this.btnMagenta.Text = "toolStripButton5";
            this.btnMagenta.ToolTipText = "Magenta";
            this.btnMagenta.Click += new System.EventHandler(this.btnMagenta_Click);
            // 
            // btnYellow
            // 
            this.btnYellow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnYellow.Image = global::MUDScreenEditor.Properties.Resources.Icon6;
            this.btnYellow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(23, 22);
            this.btnYellow.Text = "toolStripButton6";
            this.btnYellow.ToolTipText = "Yellow";
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            // 
            // btnBlack
            // 
            this.btnBlack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBlack.Image = global::MUDScreenEditor.Properties.Resources.Icon7;
            this.btnBlack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(23, 22);
            this.btnBlack.Text = "toolStripButton7";
            this.btnBlack.ToolTipText = "Black";
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // btnWhite
            // 
            this.btnWhite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWhite.Image = global::MUDScreenEditor.Properties.Resources.Icon16;
            this.btnWhite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.Size = new System.Drawing.Size(23, 22);
            this.btnWhite.Text = "toolStripButton8";
            this.btnWhite.ToolTipText = "White";
            this.btnWhite.Click += new System.EventHandler(this.btnWhite_Click);
            // 
            // btnBrtRed
            // 
            this.btnBrtRed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrtRed.Image = global::MUDScreenEditor.Properties.Resources.Icon8;
            this.btnBrtRed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrtRed.Name = "btnBrtRed";
            this.btnBrtRed.Size = new System.Drawing.Size(23, 22);
            this.btnBrtRed.Text = "toolStripButton9";
            this.btnBrtRed.ToolTipText = "Bright Red";
            this.btnBrtRed.Click += new System.EventHandler(this.btnBrtRed_Click);
            // 
            // btnBrtGreen
            // 
            this.btnBrtGreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrtGreen.Image = global::MUDScreenEditor.Properties.Resources.Icon9;
            this.btnBrtGreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrtGreen.Name = "btnBrtGreen";
            this.btnBrtGreen.Size = new System.Drawing.Size(23, 22);
            this.btnBrtGreen.Text = "toolStripButton10";
            this.btnBrtGreen.ToolTipText = "Bright Green";
            this.btnBrtGreen.Click += new System.EventHandler(this.btnBrtGreen_Click);
            // 
            // btnBrtBlue
            // 
            this.btnBrtBlue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrtBlue.Image = global::MUDScreenEditor.Properties.Resources.Icon10;
            this.btnBrtBlue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrtBlue.Name = "btnBrtBlue";
            this.btnBrtBlue.Size = new System.Drawing.Size(23, 22);
            this.btnBrtBlue.Text = "toolStripButton11";
            this.btnBrtBlue.ToolTipText = "Bright Blue";
            this.btnBrtBlue.Click += new System.EventHandler(this.btnBrtBlue_Click);
            // 
            // btnBrtCyan
            // 
            this.btnBrtCyan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrtCyan.Image = global::MUDScreenEditor.Properties.Resources.Icon11;
            this.btnBrtCyan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrtCyan.Name = "btnBrtCyan";
            this.btnBrtCyan.Size = new System.Drawing.Size(23, 22);
            this.btnBrtCyan.Text = "toolStripButton12";
            this.btnBrtCyan.ToolTipText = "Bright Cyan";
            this.btnBrtCyan.Click += new System.EventHandler(this.btnBrtCyan_Click);
            // 
            // btnBrtMagenta
            // 
            this.btnBrtMagenta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrtMagenta.Image = global::MUDScreenEditor.Properties.Resources.Icon13;
            this.btnBrtMagenta.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.btnBrtMagenta.Name = "btnBrtMagenta";
            this.btnBrtMagenta.Size = new System.Drawing.Size(23, 22);
            this.btnBrtMagenta.Text = "toolStripButton13";
            this.btnBrtMagenta.ToolTipText = "Bright Magenta";
            this.btnBrtMagenta.Click += new System.EventHandler(this.btnBrtMagenta_Click);
            // 
            // btnBrtYellow
            // 
            this.btnBrtYellow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrtYellow.Image = global::MUDScreenEditor.Properties.Resources.Icon12;
            this.btnBrtYellow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrtYellow.Name = "btnBrtYellow";
            this.btnBrtYellow.Size = new System.Drawing.Size(23, 22);
            this.btnBrtYellow.Text = "toolStripButton14";
            this.btnBrtYellow.ToolTipText = "Bright Yellow";
            this.btnBrtYellow.Click += new System.EventHandler(this.btnBrtYellow_Click);
            // 
            // btnBrtBlack
            // 
            this.btnBrtBlack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrtBlack.Image = global::MUDScreenEditor.Properties.Resources.Icon4;
            this.btnBrtBlack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrtBlack.Name = "btnBrtBlack";
            this.btnBrtBlack.Size = new System.Drawing.Size(23, 22);
            this.btnBrtBlack.Text = "toolStripButton15";
            this.btnBrtBlack.ToolTipText = "Bright Black";
            this.btnBrtBlack.Click += new System.EventHandler(this.btnBrtBlack_Click);
            // 
            // btnBrtWhite
            // 
            this.btnBrtWhite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBrtWhite.Image = global::MUDScreenEditor.Properties.Resources.Icon14;
            this.btnBrtWhite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrtWhite.Name = "btnBrtWhite";
            this.btnBrtWhite.Size = new System.Drawing.Size(23, 22);
            this.btnBrtWhite.Text = "toolStripButton16";
            this.btnBrtWhite.ToolTipText = "Bright White";
            this.btnBrtWhite.Click += new System.EventHandler(this.btnBrtWhite_Click);
            // 
            // cbScreenType
            // 
            this.cbScreenType.FormattingEnabled = true;
            this.cbScreenType.Location = new System.Drawing.Point(173, 50);
            this.cbScreenType.Name = "cbScreenType";
            this.cbScreenType.Size = new System.Drawing.Size(118, 21);
            this.cbScreenType.TabIndex = 4;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(507, 53);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(56, 17);
            this.chkEnabled.TabIndex = 5;
            this.chkEnabled.Text = "Active";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(338, 52);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(163, 20);
            this.txtName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(297, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Type";
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(12, 49);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(23, 23);
            this.btnLeft.TabIndex = 9;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(41, 50);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(23, 23);
            this.btnRight.TabIndex = 10;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(70, 55);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(34, 13);
            this.lblPosition.TabIndex = 11;
            this.lblPosition.Text = "0 of 0";
            // 
            // MudScreenEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 544);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.cbScreenType);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MudScreenEditor";
            this.Text = "MUD Screen Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRed;
        private System.Windows.Forms.ToolStripButton btnGreen;
        private System.Windows.Forms.ToolStripButton btnBlue;
        private System.Windows.Forms.ToolStripButton btnCyan;
        private System.Windows.Forms.ToolStripButton btnMagenta;
        private System.Windows.Forms.ToolStripButton btnYellow;
        private System.Windows.Forms.ToolStripButton btnBlack;
        private System.Windows.Forms.ToolStripButton btnWhite;
        private System.Windows.Forms.ToolStripButton btnBrtRed;
        private System.Windows.Forms.ToolStripButton btnBrtGreen;
        private System.Windows.Forms.ToolStripButton btnBrtBlue;
        private System.Windows.Forms.ToolStripButton btnBrtCyan;
        private System.Windows.Forms.ToolStripButton btnBrtMagenta;
        private System.Windows.Forms.ToolStripButton btnBrtYellow;
        private System.Windows.Forms.ToolStripButton btnBrtBlack;
        private System.Windows.Forms.ToolStripButton btnBrtWhite;
        private System.Windows.Forms.ToolStripMenuItem importScreenToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbScreenType;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.ToolStripButton btnNewScreen;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
    }
}

