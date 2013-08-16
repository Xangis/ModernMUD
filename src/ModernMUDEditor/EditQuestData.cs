using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModernMUD;

namespace ModernMUDEditor
{
    public partial class EditQuestData : Form
    {
        public EditQuestData(QuestData data)
        {
            InitializeComponent();
            foreach( QuestItem item in data.Receive)
            {
                lstItemsWanted.Items.Add(item);
            }
            foreach (QuestItem item in data.Give)
            {
                lstRewards.Items.Add(item);
            }
            txtCompleteMsg.Text = data.Complete;
            txtDisappearMsg.Text = data.Disappear;
        }

        public QuestData GetQuestData()
        {
            QuestData data = new QuestData();
            data.Complete = txtCompleteMsg.Text;
            data.Disappear = txtDisappearMsg.Text;
            foreach (object obj in lstItemsWanted.Items)
            {
                if (obj is QuestItem)
                {
                    data.Receive.Add((QuestItem)obj);
                }
            }
            foreach (object obj in lstRewards.Items)
            {
                if (obj is QuestItem)
                {
                    data.Give.Add((QuestItem)obj);
                }
            }
            return data;
        }

        private void btnAddWanted_Click(object sender, EventArgs e)
        {
            EditQuestItem dlg = new EditQuestItem(new QuestItem());
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                lstItemsWanted.Items.Add(dlg.GetQuestItem());
            }
        }

        private void btnRemoveWanted_Click(object sender, EventArgs e)
        {
            if (lstItemsWanted.SelectedIndex != -1)
            {
                lstItemsWanted.Items.RemoveAt(lstItemsWanted.SelectedIndex);
            }
        }

        private void btnAddReward_Click(object sender, EventArgs e)
        {
            EditQuestItem dlg = new EditQuestItem(new QuestItem());
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                lstRewards.Items.Add(dlg.GetQuestItem());
            }
        }

        private void btnRemoveReward_Click(object sender, EventArgs e)
        {
            if (lstRewards.SelectedIndex != -1)
            {
                lstRewards.Items.RemoveAt(lstRewards.SelectedIndex);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}