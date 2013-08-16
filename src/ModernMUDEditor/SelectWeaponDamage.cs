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
    public partial class SelectWeaponDamage : Form
    {
        public SelectWeaponDamage()
        {
            InitializeComponent();
            for (int i = 0; i < AttackType.Table.Length; i++)
            {
                cbType.Items.Add((AttackType.Table[i].Name + " (" + AttackType.Table[i].DamageInflicted.ToString() + ", " + AttackType.Table[i].SkillName + ")"));
            }
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

        public int GetNumber()
        {
            return cbType.SelectedIndex;
        }
    }
}
