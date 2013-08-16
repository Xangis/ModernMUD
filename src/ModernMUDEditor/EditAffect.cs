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
    public partial class EditAffect : Form
    {
        List<Affect> _affects;
        int _selectedIndex;

        public EditAffect(List<Affect> affects)
        {
            InitializeComponent();
            Type types = typeof(Affect.Apply);
            foreach (string s in Enum.GetNames(types))
            {
                cbApplyType.Items.Add(s);
            }
            _affects = affects;
            if (_affects.Count > 0)
            {
                _selectedIndex = 0;
            }
            else
            {
                _selectedIndex = -1;
            }
            UpdateWindowContents();
        }

        private void UpdateWindowContents()
        {
            // Disable controls if selected index is -1, otherwise update data.
            lstModifiers.Items.Clear();
            if (_selectedIndex == -1 || _selectedIndex >= _affects.Count)
            {
                lblDuration.Text = String.Empty;
                lblLevel.Text = String.Empty;
                lblType.Text = String.Empty;
                lblValue.Text = String.Empty;
                lblBits.Text = String.Empty;
            }
            else
            {
                lblDuration.Text = _affects[_selectedIndex].Duration.ToString();
                lblLevel.Text = _affects[_selectedIndex].Level.ToString();
                lblType.Text = _affects[_selectedIndex].Type.ToString();
                lblValue.Text = _affects[_selectedIndex].Value.ToString();
                string bits = "{ ";
                for (int i = 0; i < _affects[_selectedIndex].BitVectors.Length; i++)
                {
                    bits += _affects[_selectedIndex].BitVectors[i].ToString() + " ";
                }
                bits += "}";
                lblBits.Text = bits;
                foreach (AffectApplyType modifier in _affects[_selectedIndex].Modifiers)
                {
                    lstModifiers.Items.Add(modifier);
                }
            }

            //private List<AffectApplyType> _modifiers;
        }

        private void ApplyWindowContents()
        {
            if (_selectedIndex == -1 || _selectedIndex >= _affects.Count)
            {
                return;
            }

            if (_affects[_selectedIndex].Modifiers == null)
            {
                _affects[_selectedIndex].Modifiers = new List<AffectApplyType>();
            }
            else
            {
                _affects[_selectedIndex].Modifiers.Clear();
            }

            for (int i = 0; i < lstModifiers.Items.Count; i++)
            {
                _affects[_selectedIndex].Modifiers.Add((AffectApplyType)lstModifiers.Items[i]);
            }
        }

        public List<Affect> GetAffects()
        {
            return _affects;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyWindowContents();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_affects.Count < 1)
            {
                _affects.Add(new Affect());
                _selectedIndex = 0;
                UpdateWindowContents();
            }

            if (_selectedIndex == -1 || _selectedIndex >= _affects.Count)
            {
                return;
            }

            lstModifiers.Items.Add(new AffectApplyType());
        }

        private void lstModifiers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstModifiers.SelectedIndex == -1 || lstModifiers.SelectedIndex >= lstModifiers.Items.Count)
            {
                return;
            }

            cbApplyType.Text = ((AffectApplyType)lstModifiers.SelectedItem).Location.ToString();
            txtAmount.Text = ((AffectApplyType)lstModifiers.SelectedItem).Amount.ToString();
        }

        private void cbApplyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedIndex == -1 || _selectedIndex > _affects.Count || lstModifiers.SelectedIndex == -1 ||
                lstModifiers.SelectedIndex >= lstModifiers.Items.Count)
            {
                return;
            }

            if (cbApplyType.SelectedIndex != -1)
            {
                AffectApplyType apply = lstModifiers.Items[lstModifiers.SelectedIndex] as AffectApplyType;
                if (apply != null)
                {
                    apply.Location = (Affect.Apply)Enum.Parse(typeof(Affect.Apply), cbApplyType.Text);
                    lstModifiers.Items[lstModifiers.SelectedIndex] = apply;
                }
            }

            // TODO: Replace selected item data in list box with new item data.
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstModifiers.Items.Count < 1)
            {
                return;
            }

            if( lstModifiers.SelectedIndex == -1 )
            {
                lstModifiers.Items.RemoveAt((lstModifiers.Items.Count-1));
                return;
            }

            if (lstModifiers.SelectedIndex < lstModifiers.Items.Count)
            {
                lstModifiers.Items.RemoveAt(lstModifiers.SelectedIndex);
                if (lstModifiers.SelectedIndex >= lstModifiers.Items.Count)
                {
                    lstModifiers.SelectedIndex = lstModifiers.Items.Count - 1;
                }
                return;
            }

        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (_selectedIndex == -1 || _selectedIndex > _affects.Count || lstModifiers.SelectedIndex == -1 ||
                lstModifiers.SelectedIndex >= lstModifiers.Items.Count)
            {
                return;
            }

            AffectApplyType apply = lstModifiers.Items[lstModifiers.SelectedIndex] as AffectApplyType;
            if (apply != null)
            {
                int val;
                if (Int32.TryParse(txtAmount.Text, out val))
                {
                    apply.Amount = val;
                    lstModifiers.Items[lstModifiers.SelectedIndex] = apply;
                }
            }
        }
    }
}
