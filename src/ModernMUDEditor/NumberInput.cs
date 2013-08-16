using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ModernMUDEditor
{
    public partial class NumberInput : Form
    {
        public NumberInput()
        {
            InitializeComponent();
        }

        public int Value
        {
            get
            {
                int value = 0;
                Int32.TryParse(textBox1.Text, out value);
                return value;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
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