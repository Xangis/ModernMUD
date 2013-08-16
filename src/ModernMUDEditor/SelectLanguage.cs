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
    public partial class SelectLanguage : Form
    {
        public SelectLanguage()
        {
            InitializeComponent();
            Type types = typeof(Race.Language);
            foreach (string s in Enum.GetNames(types))
            {
                cbLanguage.Items.Add(s);
            }
        }

        public Race.Language GetLanguage()
        {
            Race.Language lang = (Race.Language)Enum.Parse(typeof(Race.Language), cbLanguage.Text);
            return lang;
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
    }
}
