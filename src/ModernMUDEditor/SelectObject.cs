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
    public partial class SelectObject : Form
    {
        private Area _area;

        public SelectObject(Area area)
        {
            InitializeComponent();
            foreach (ObjTemplate obj in area.Objects)
            {
                cbType.Items.Add(ModernMUD.Color.RemoveColorCodes(obj.ShortDescription));
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
            txtValue.Text = _area.Objects[cbType.SelectedIndex].IndexNumber.ToString();
        }
    }
}