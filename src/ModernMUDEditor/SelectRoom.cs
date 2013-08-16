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
    public partial class SelectRoom : Form
    {
        private Area _area;

        public SelectRoom(Area area)
        {
            InitializeComponent();
            foreach( RoomTemplate room in area.Rooms )
            {
                cbType.Items.Add(ModernMUD.Color.RemoveColorCodes(room.Title));
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
            txtValue.Text = _area.Rooms[cbType.SelectedIndex].IndexNumber.ToString();
        }
    }
}