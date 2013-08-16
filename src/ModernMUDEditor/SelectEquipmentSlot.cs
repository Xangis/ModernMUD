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
    public partial class SelectEquipmentSlot : Form
    {
        public SelectEquipmentSlot()
        {
            InitializeComponent();
            Type types = typeof(ObjTemplate.WearLocation);
            foreach (string s in Enum.GetNames(types))
            {
                cbSlot.Items.Add(s);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public int GetNumber()
        {
            return cbSlot.SelectedIndex;
        }
    }
}
