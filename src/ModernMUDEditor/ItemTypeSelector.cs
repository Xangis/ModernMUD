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
    public partial class ItemTypeSelector : Form
    {
        public ItemTypeSelector()
        {
            InitializeComponent();
            for (int x = 0; x < ObjTemplate.MAX_ITEM_TYPE; x++)
            {
                cbItemType.Items.Add(ObjTemplate.ItemTypeString((ObjTemplate.ObjectType)x));
            }
        }

        public string Value
        {
            get
            {
                if (cbItemType.SelectedIndex != -1)
                {
                    return cbItemType.Items[cbItemType.SelectedIndex] as string;
                }
                return String.Empty;
            }
        }

        private void btnOK_Click_1(object sender, EventArgs e)
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