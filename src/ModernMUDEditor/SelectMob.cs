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
    public partial class SelectMob : Form
    {
        private Area _area;

        public SelectMob(Area area)
        {
            InitializeComponent();
            foreach (MobTemplate mob in area.Mobs)
            {
                cbType.Items.Add(ModernMUD.Color.RemoveColorCodes(mob.ShortDescription));
            }
            _area = area;
        }

        public string GetIndexNumberString()
        {
            return txtValue.Text;
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

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValue.Text = _area.Mobs[cbType.SelectedIndex].IndexNumber.ToString();
        }
    }
}