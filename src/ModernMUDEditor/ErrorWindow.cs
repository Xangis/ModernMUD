using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ModernMUDEditor
{
    public partial class ErrorWindow : Form
    {
        private MainForm _parent = null;

        public ErrorWindow(MainForm parent, List<String> errors)
        {
            _parent = parent;
            InitializeComponent();
            lstErrors.Items.Clear();
            foreach (String str in errors)
            {
                lstErrors.Items.Add(str);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Parse the text in a row when double-clicked and go to edit the offending item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lstErrors_DoubleClick(object sender, System.EventArgs e)
        {
            if (lstErrors.SelectedItem != null)
            {
                String text = lstErrors.SelectedItem as String;
                String[] pieces = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int value = 0;
                if (pieces.Length < 1) 
                    return;
                // Process single-argument (zone-wide) errors.
                if (pieces[0].StartsWith("Area", StringComparison.CurrentCultureIgnoreCase))
                {
                    _parent.ShowAreaSettings();
                    return;
                }
                if (pieces.Length < 2)
                    return;
                // Process double-argument (item-specific) errors.
                if (!Int32.TryParse(pieces[1], out value))
                {
                    return;
                }

                if (pieces[0].StartsWith("Room", StringComparison.CurrentCultureIgnoreCase))
                {
                    _parent.NavigateToEditRoom(value);
                }
                else if (pieces[0].StartsWith("Mob", StringComparison.CurrentCultureIgnoreCase))
                {
                    _parent.NavigateToEditMob(value);
                }
                else if (pieces[0].StartsWith("Object", StringComparison.CurrentCultureIgnoreCase))
                {
                    _parent.NavigateToEditObject(value);
                }
            }
        }
    }
}
