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
    public partial class SelectDirection : Form
    {
        public SelectDirection()
        {
            InitializeComponent();
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

        public String GetText()
        {
            return comboBox1.Text;
        }

        public int GetDirectionNumber()
        {
            return Exit.DoorLookup(comboBox1.Text);
        }
    }
}
