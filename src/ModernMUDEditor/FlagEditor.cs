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
    public partial class FlagEditor : Form
    {
        CheckBoxArray _myArray;
        int _initialValue = 0;

        // Allows us to edit a single page of bitvector flags.
        public FlagEditor(BitvectorFlagType[] flags, int initialValue, int group)
        {
            InitializeComponent();
            _initialValue = initialValue;
            _myArray = new CheckBoxArray(this);
            Value = initialValue;
            for (int i = 0; i < 32; i++)
            {
                _myArray.AddNewCheckBox();
                _myArray[i].Checked = ((_initialValue & (1 << i)) != 0);
                _myArray[i].Enabled = false;
            }
            _myArray.AddValueLabel();
            for (int x = 0; x < flags.Length; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    if ((flags[x].BitvectorData.Group == group) && (flags[x].BitvectorData.Vector == 1 << y))
                    {
                        _myArray[y].Text = flags[x].Name;
                        _myArray[y].Enabled = flags[x].Settable;
                    }
                }
            }
            // TODO: Set text for each control based on the flags we were passed.
            //_myArray[1].BackColor = System.Drawing.Color.Red;
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

        public int Value
        {
            get
            {
                return _myArray.Value;
            }
            set
            {
                _myArray.Value = value;
            }
        }
    }
}