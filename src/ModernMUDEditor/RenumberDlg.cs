using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ModernMUDEditor
{
    public partial class RenumberDlg : Form
    {
        public RenumberDlg()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
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
            int value = 0;
            Int32.TryParse(txtNumber.Text, out value);
            return value;
        }
    }
}
