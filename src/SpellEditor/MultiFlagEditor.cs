using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModernMUD;

namespace SpellEditor
{
    public partial class MultiFlagEditor : Form
    {
        CheckBoxArray _myArray;
        private int[] _initialValue;
        BitvectorFlagType[] _flags;
        int _currentGroup;

        // Allows us to edit a flag type.
        //public FlagEditor(int flags, int initialValue)
        //{
        //    InitializeComponent();
        //    _initialValue = initialValue;
        //    _myArray = new CheckBoxArray(this);
        //    Value = initialValue;
        //    for (int i = 0; i < 32; i++)
        //    {
        //        _myArray.AddNewCheckBox();
        //        _myArray[i].Checked = ((_initialValue & (1 << i)) != 0);
        //        _myArray[i].Enabled = false;
        //    }
        //    _myArray.AddValueLabel();
        //    for (int x = 0; x < flags.Length; x++)
        //    {
        //        for (int y = 0; y < 32; y++)
        //        {
        //            if (flags[x].bit == 1 << y)
        //            {
        //                _myArray[y].Text = flags[x].name;
        //                _myArray[y].Enabled = flags[x].settable;
        //            }
        //        }
        //    }
        //    // TODO: Set text for each control based on the flags we were passed.
        //    //_myArray[1].BackColor = System.Drawing.Color.Red;
        //}

        // Allows us to edit a single page of bitvector flags.
        public MultiFlagEditor(BitvectorFlagType[] flags, int[] initialValues)
        {
            InitializeComponent();
            _initialValue = initialValues;
            _flags = flags;
            _currentGroup = 0;
            _myArray = new CheckBoxArray(this);
            for (int i = 0; i < 32; i++)
            {
                _myArray.AddNewCheckBox();
                //_myArray[i].Checked = ((_initialValue[0] & (1 << i)) != 0);
                _myArray[i].Enabled = false;
            }
            _myArray.AddValueLabel();
            UpdateDialogWithBitvector();
            // TODO: Set text for each control based on the flags we were passed.
            //_myArray[1].BackColor = System.Drawing.Color.Red;
        }

        private void UpdateDialogWithBitvector()
        {
            // Clear everything first.
            for (int i = 0; i < 32; i++)
            {
                _myArray[i].Text = String.Empty;
                _myArray[i].Enabled = false;
            }
            for (int x = 0; x < _flags.Length; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    if ((_flags[x].BitvectorData.Group == _currentGroup) && (_flags[x].BitvectorData.Vector == 1 << y))
                    {
                        _myArray[y].Text = _flags[x].Name;
                        _myArray[y].Enabled = _flags[x].Settable;
                    }
                }
            }
            Value = _initialValue[_currentGroup];
            lblGroup.Text = "Group " + (_currentGroup + 1) + " of " + _initialValue.Length;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            StoreValues();
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

        public int[] GetValues()
        {
            return _initialValue;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentGroup > 0 && _initialValue.Length > 0)
            {
                StoreValues();
                --_currentGroup;
                UpdateDialogWithBitvector();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentGroup < (_initialValue.Length - 1))
            {
                StoreValues();
                ++_currentGroup;
                UpdateDialogWithBitvector();
            }
        }

        private void StoreValues()
        {
            _initialValue[_currentGroup] = Value;
        }
    }
}