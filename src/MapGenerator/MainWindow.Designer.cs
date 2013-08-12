namespace MapGenerator
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRoomDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRoomDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateRoomMappingTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartVnum = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZoneName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(745, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.openRoomDefinitionsToolStripMenuItem,
            this.generateZoneToolStripMenuItem,
            this.saveRoomDefinitionsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.openImageToolStripMenuItem.Text = "Open Image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // openRoomDefinitionsToolStripMenuItem
            // 
            this.openRoomDefinitionsToolStripMenuItem.Name = "openRoomDefinitionsToolStripMenuItem";
            this.openRoomDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.openRoomDefinitionsToolStripMenuItem.Text = "Open Room Definitions";
            this.openRoomDefinitionsToolStripMenuItem.Click += new System.EventHandler(this.openRoomDefinitionsToolStripMenuItem_Click);
            // 
            // generateZoneToolStripMenuItem
            // 
            this.generateZoneToolStripMenuItem.Name = "generateZoneToolStripMenuItem";
            this.generateZoneToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.generateZoneToolStripMenuItem.Text = "Generate Zone";
            this.generateZoneToolStripMenuItem.Click += new System.EventHandler(this.generateZoneToolStripMenuItem_Click);
            // 
            // saveRoomDefinitionsToolStripMenuItem
            // 
            this.saveRoomDefinitionsToolStripMenuItem.Name = "saveRoomDefinitionsToolStripMenuItem";
            this.saveRoomDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.saveRoomDefinitionsToolStripMenuItem.Text = "Save Room Definitions";
            this.saveRoomDefinitionsToolStripMenuItem.Click += new System.EventHandler(this.saveRoomDefinitionsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateRoomMappingTemplateToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // generateRoomMappingTemplateToolStripMenuItem
            // 
            this.generateRoomMappingTemplateToolStripMenuItem.Name = "generateRoomMappingTemplateToolStripMenuItem";
            this.generateRoomMappingTemplateToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.generateRoomMappingTemplateToolStripMenuItem.Text = "Generate Room Mapping Template";
            this.generateRoomMappingTemplateToolStripMenuItem.Click += new System.EventHandler(this.generateRoomMappingTemplateToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(608, 228);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(745, 289);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(638, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start Vnum";
            // 
            // txtStartVnum
            // 
            this.txtStartVnum.Location = new System.Drawing.Point(614, 24);
            this.txtStartVnum.Name = "txtStartVnum";
            this.txtStartVnum.Size = new System.Drawing.Size(119, 20);
            this.txtStartVnum.TabIndex = 4;
            this.txtStartVnum.Text = "100000";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtAuthor);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtZoneName);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.txtStartVnum);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(745, 521);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(638, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Zone Name";
            // 
            // txtZoneName
            // 
            this.txtZoneName.Location = new System.Drawing.Point(614, 72);
            this.txtZoneName.Name = "txtZoneName";
            this.txtZoneName.Size = new System.Drawing.Size(119, 20);
            this.txtZoneName.TabIndex = 6;
            this.txtZoneName.Text = "&+gUnnamed Map&n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(638, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Author";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(614, 122);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(119, 20);
            this.txtAuthor.TabIndex = 8;
            this.txtAuthor.Text = "Unknown";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 545);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Map Generator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRoomDefinitionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRoomDefinitionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem generateRoomMappingTemplateToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.TextBox txtStartVnum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtZoneName;
        private System.Windows.Forms.Label label2;
    }
}

