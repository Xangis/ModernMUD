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
    public partial class EditSpells : Form
    {
        List<SpellEntry> _spellEffects = new List<SpellEntry>();

        public EditSpells(List<SpellEntry> entries)
        {
            InitializeComponent();
            _spellEffects = entries;
            foreach (SpellEntry entry in entries)
            {
                lstSpells.Items.Add(entry.Name);
            }
        }

        public List<SpellEntry> GetSpellEffects()
        {
            return _spellEffects;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbSpells.Text))
                return;
            SpellEntry entry = new SpellEntry();
            entry.Name = cbSpells.Text;
            lstSpells.Items.Add(entry.Name);
            _spellEffects.Add(entry);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSpells.SelectedIndex != -1)
            {
                _spellEffects.RemoveAt(lstSpells.SelectedIndex);
                lstSpells.Items.RemoveAt(lstSpells.SelectedIndex);
            }
        }
    }
}
