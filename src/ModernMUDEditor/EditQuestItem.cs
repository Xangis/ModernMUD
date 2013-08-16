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
    public partial class EditQuestItem : Form
    {
        public EditQuestItem(QuestItem item)
        {
            InitializeComponent();
            Type types = typeof(QuestItem.QuestType);
            foreach (string s in Enum.GetNames(types))
            {
                cbType.Items.Add(s);
            }
            txtValue.Text = item.Value.ToString();
            if ((int)item.Type < cbType.Items.Count)
            {
                cbType.SelectedIndex = (int)item.Type;
            }
        }

        public QuestItem GetQuestItem()
        {
            QuestItem item = new QuestItem();
            item.Completed = false;
            if (cbType.SelectedIndex != -1)
            {
                item.Type = (QuestItem.QuestType)cbType.SelectedIndex;
            }
            int value = 0;
            Int32.TryParse(txtValue.Text, out value);
            item.Value = value;
            return item;
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