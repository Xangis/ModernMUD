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
    public partial class SelectLiquid : Form
    {
        public SelectLiquid()
        {
            InitializeComponent();
            for (int i = 0; i <  Liquid.Table.Length; i++)
            {
                cbType.Items.Add(Liquid.Table[i].Name);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public string GetText()
        {
            return cbType.Text;
        }
    }
}
