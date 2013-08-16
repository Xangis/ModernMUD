namespace ModernMUDEditor
{
    partial class EditCustomActions
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
            this.cbTriggers = new System.Windows.Forms.ComboBox();
            this.cbActions = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTriggerData = new System.Windows.Forms.TextBox();
            this.txtActionData = new System.Windows.Forms.TextBox();
            this.lstCustomActions = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAddSubaction = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPercent = new System.Windows.Forms.TextBox();
            this.btnFindRoomAction = new System.Windows.Forms.Button();
            this.btnFindObjectAction = new System.Windows.Forms.Button();
            this.btnFindMobAction = new System.Windows.Forms.Button();
            this.btnFindDirAction = new System.Windows.Forms.Button();
            this.btnFindSpellAction = new System.Windows.Forms.Button();
            this.btnFindCmdAction = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFindRoomTrigger = new System.Windows.Forms.Button();
            this.btnFindSpellTrigger = new System.Windows.Forms.Button();
            this.btnFindMobTrigger = new System.Windows.Forms.Button();
            this.btnFindDirTrigger = new System.Windows.Forms.Button();
            this.btnFindCmdTrigger = new System.Windows.Forms.Button();
            this.btnFindObjTrigger = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbTriggers
            // 
            this.cbTriggers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTriggers.FormattingEnabled = true;
            this.cbTriggers.Location = new System.Drawing.Point(13, 144);
            this.cbTriggers.Name = "cbTriggers";
            this.cbTriggers.Size = new System.Drawing.Size(172, 21);
            this.cbTriggers.TabIndex = 0;
            this.cbTriggers.SelectedIndexChanged += new System.EventHandler(this.cbTriggers_SelectedIndexChanged);
            // 
            // cbActions
            // 
            this.cbActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActions.FormattingEnabled = true;
            this.cbActions.Location = new System.Drawing.Point(360, 143);
            this.cbActions.Name = "cbActions";
            this.cbActions.Size = new System.Drawing.Size(158, 21);
            this.cbActions.TabIndex = 1;
            this.cbActions.SelectedIndexChanged += new System.EventHandler(this.cbActions_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(290, 229);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(371, 229);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTriggerData
            // 
            this.txtTriggerData.Location = new System.Drawing.Point(190, 144);
            this.txtTriggerData.Name = "txtTriggerData";
            this.txtTriggerData.Size = new System.Drawing.Size(166, 20);
            this.txtTriggerData.TabIndex = 4;
            // 
            // txtActionData
            // 
            this.txtActionData.Location = new System.Drawing.Point(524, 143);
            this.txtActionData.Name = "txtActionData";
            this.txtActionData.Size = new System.Drawing.Size(166, 20);
            this.txtActionData.TabIndex = 5;
            // 
            // lstCustomActions
            // 
            this.lstCustomActions.FormattingEnabled = true;
            this.lstCustomActions.Location = new System.Drawing.Point(13, 13);
            this.lstCustomActions.Name = "lstCustomActions";
            this.lstCustomActions.Size = new System.Drawing.Size(729, 121);
            this.lstCustomActions.TabIndex = 6;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(421, 173);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(248, 173);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAddSubaction
            // 
            this.btnAddSubaction.Location = new System.Drawing.Point(329, 173);
            this.btnAddSubaction.Name = "btnAddSubaction";
            this.btnAddSubaction.Size = new System.Drawing.Size(86, 23);
            this.btnAddSubaction.TabIndex = 9;
            this.btnAddSubaction.Text = "Add Subaction";
            this.btnAddSubaction.UseVisualStyleBackColor = true;
            this.btnAddSubaction.Click += new System.EventHandler(this.btnAddSubaction_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(696, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "%";
            // 
            // txtPercent
            // 
            this.txtPercent.Location = new System.Drawing.Point(711, 143);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new System.Drawing.Size(31, 20);
            this.txtPercent.TabIndex = 11;
            this.txtPercent.Text = "100";
            // 
            // btnFindRoomAction
            // 
            this.btnFindRoomAction.Location = new System.Drawing.Point(6, 19);
            this.btnFindRoomAction.Name = "btnFindRoomAction";
            this.btnFindRoomAction.Size = new System.Drawing.Size(61, 23);
            this.btnFindRoomAction.TabIndex = 12;
            this.btnFindRoomAction.Text = "Room";
            this.btnFindRoomAction.UseVisualStyleBackColor = true;
            this.btnFindRoomAction.Click += new System.EventHandler(this.btnFindRoomAction_Click);
            // 
            // btnFindObjectAction
            // 
            this.btnFindObjectAction.Location = new System.Drawing.Point(6, 44);
            this.btnFindObjectAction.Name = "btnFindObjectAction";
            this.btnFindObjectAction.Size = new System.Drawing.Size(61, 23);
            this.btnFindObjectAction.TabIndex = 13;
            this.btnFindObjectAction.Text = "Object";
            this.btnFindObjectAction.UseVisualStyleBackColor = true;
            this.btnFindObjectAction.Click += new System.EventHandler(this.btnFindObjectAction_Click);
            // 
            // btnFindMobAction
            // 
            this.btnFindMobAction.Location = new System.Drawing.Point(72, 19);
            this.btnFindMobAction.Name = "btnFindMobAction";
            this.btnFindMobAction.Size = new System.Drawing.Size(61, 23);
            this.btnFindMobAction.TabIndex = 14;
            this.btnFindMobAction.Text = "Mob";
            this.btnFindMobAction.UseVisualStyleBackColor = true;
            this.btnFindMobAction.Click += new System.EventHandler(this.btnFindMobAction_Click);
            // 
            // btnFindDirAction
            // 
            this.btnFindDirAction.Location = new System.Drawing.Point(72, 44);
            this.btnFindDirAction.Name = "btnFindDirAction";
            this.btnFindDirAction.Size = new System.Drawing.Size(61, 23);
            this.btnFindDirAction.TabIndex = 15;
            this.btnFindDirAction.Text = "Dir";
            this.btnFindDirAction.UseVisualStyleBackColor = true;
            this.btnFindDirAction.Click += new System.EventHandler(this.btnFindDirAction_Click);
            // 
            // btnFindSpellAction
            // 
            this.btnFindSpellAction.Location = new System.Drawing.Point(136, 44);
            this.btnFindSpellAction.Name = "btnFindSpellAction";
            this.btnFindSpellAction.Size = new System.Drawing.Size(61, 23);
            this.btnFindSpellAction.TabIndex = 17;
            this.btnFindSpellAction.Text = "Spell";
            this.btnFindSpellAction.UseVisualStyleBackColor = true;
            this.btnFindSpellAction.Click += new System.EventHandler(this.btnFindSpellAction_Click);
            // 
            // btnFindCmdAction
            // 
            this.btnFindCmdAction.Location = new System.Drawing.Point(136, 19);
            this.btnFindCmdAction.Name = "btnFindCmdAction";
            this.btnFindCmdAction.Size = new System.Drawing.Size(61, 23);
            this.btnFindCmdAction.TabIndex = 16;
            this.btnFindCmdAction.Text = "Cmd";
            this.btnFindCmdAction.UseVisualStyleBackColor = true;
            this.btnFindCmdAction.Click += new System.EventHandler(this.btnFindCmdAction_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFindRoomAction);
            this.groupBox1.Controls.Add(this.btnFindSpellAction);
            this.groupBox1.Controls.Add(this.btnFindMobAction);
            this.groupBox1.Controls.Add(this.btnFindDirAction);
            this.groupBox1.Controls.Add(this.btnFindCmdAction);
            this.groupBox1.Controls.Add(this.btnFindObjectAction);
            this.groupBox1.Location = new System.Drawing.Point(533, 173);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 73);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFindRoomTrigger);
            this.groupBox2.Controls.Add(this.btnFindSpellTrigger);
            this.groupBox2.Controls.Add(this.btnFindMobTrigger);
            this.groupBox2.Controls.Add(this.btnFindDirTrigger);
            this.groupBox2.Controls.Add(this.btnFindCmdTrigger);
            this.groupBox2.Controls.Add(this.btnFindObjTrigger);
            this.groupBox2.Location = new System.Drawing.Point(12, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(203, 73);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Find";
            // 
            // btnFindRoomTrigger
            // 
            this.btnFindRoomTrigger.Location = new System.Drawing.Point(6, 19);
            this.btnFindRoomTrigger.Name = "btnFindRoomTrigger";
            this.btnFindRoomTrigger.Size = new System.Drawing.Size(61, 23);
            this.btnFindRoomTrigger.TabIndex = 12;
            this.btnFindRoomTrigger.Text = "Room";
            this.btnFindRoomTrigger.UseVisualStyleBackColor = true;
            this.btnFindRoomTrigger.Click += new System.EventHandler(this.btnFindRoomTrigger_Click);
            // 
            // btnFindSpellTrigger
            // 
            this.btnFindSpellTrigger.Location = new System.Drawing.Point(136, 44);
            this.btnFindSpellTrigger.Name = "btnFindSpellTrigger";
            this.btnFindSpellTrigger.Size = new System.Drawing.Size(61, 23);
            this.btnFindSpellTrigger.TabIndex = 17;
            this.btnFindSpellTrigger.Text = "Spell";
            this.btnFindSpellTrigger.UseVisualStyleBackColor = true;
            this.btnFindSpellTrigger.Click += new System.EventHandler(this.btnFindSpellTrigger_Click);
            // 
            // btnFindMobTrigger
            // 
            this.btnFindMobTrigger.Location = new System.Drawing.Point(72, 19);
            this.btnFindMobTrigger.Name = "btnFindMobTrigger";
            this.btnFindMobTrigger.Size = new System.Drawing.Size(61, 23);
            this.btnFindMobTrigger.TabIndex = 14;
            this.btnFindMobTrigger.Text = "Mob";
            this.btnFindMobTrigger.UseVisualStyleBackColor = true;
            this.btnFindMobTrigger.Click += new System.EventHandler(this.btnFindMobTrigger_Click);
            // 
            // btnFindDirTrigger
            // 
            this.btnFindDirTrigger.Location = new System.Drawing.Point(72, 44);
            this.btnFindDirTrigger.Name = "btnFindDirTrigger";
            this.btnFindDirTrigger.Size = new System.Drawing.Size(61, 23);
            this.btnFindDirTrigger.TabIndex = 15;
            this.btnFindDirTrigger.Text = "Dir";
            this.btnFindDirTrigger.UseVisualStyleBackColor = true;
            this.btnFindDirTrigger.Click += new System.EventHandler(this.btnFindDirTrigger_Click);
            // 
            // btnFindCmdTrigger
            // 
            this.btnFindCmdTrigger.Location = new System.Drawing.Point(136, 19);
            this.btnFindCmdTrigger.Name = "btnFindCmdTrigger";
            this.btnFindCmdTrigger.Size = new System.Drawing.Size(61, 23);
            this.btnFindCmdTrigger.TabIndex = 16;
            this.btnFindCmdTrigger.Text = "Cmd";
            this.btnFindCmdTrigger.UseVisualStyleBackColor = true;
            this.btnFindCmdTrigger.Click += new System.EventHandler(this.btnFindCmdTrigger_Click);
            // 
            // btnFindObjTrigger
            // 
            this.btnFindObjTrigger.Location = new System.Drawing.Point(6, 44);
            this.btnFindObjTrigger.Name = "btnFindObjTrigger";
            this.btnFindObjTrigger.Size = new System.Drawing.Size(61, 23);
            this.btnFindObjTrigger.TabIndex = 13;
            this.btnFindObjTrigger.Text = "Object";
            this.btnFindObjTrigger.UseVisualStyleBackColor = true;
            this.btnFindObjTrigger.Click += new System.EventHandler(this.btnFindObjTrigger_Click);
            // 
            // EditCustomActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 264);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPercent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddSubaction);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstCustomActions);
            this.Controls.Add(this.txtActionData);
            this.Controls.Add(this.txtTriggerData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbActions);
            this.Controls.Add(this.cbTriggers);
            this.MaximizeBox = false;
            this.Name = "EditCustomActions";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Custom Actions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTriggers;
        private System.Windows.Forms.ComboBox cbActions;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtTriggerData;
        private System.Windows.Forms.TextBox txtActionData;
        private System.Windows.Forms.ListBox lstCustomActions;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAddSubaction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPercent;
        private System.Windows.Forms.Button btnFindRoomAction;
        private System.Windows.Forms.Button btnFindObjectAction;
        private System.Windows.Forms.Button btnFindMobAction;
        private System.Windows.Forms.Button btnFindDirAction;
        private System.Windows.Forms.Button btnFindSpellAction;
        private System.Windows.Forms.Button btnFindCmdAction;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnFindRoomTrigger;
        private System.Windows.Forms.Button btnFindSpellTrigger;
        private System.Windows.Forms.Button btnFindMobTrigger;
        private System.Windows.Forms.Button btnFindDirTrigger;
        private System.Windows.Forms.Button btnFindCmdTrigger;
        private System.Windows.Forms.Button btnFindObjTrigger;
    }
}