using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ModernMUD;

namespace ModernMUDEditor
{
    public partial class EditSpecial : Form
    {
        public EditSpecial(List<MobSpecial> data)
        {
            InitializeComponent();
            foreach (KeyValuePair<string, MobSpecial> spec in MobSpecial.MobSpecialTable)
            {
                cbSpecialList.Items.Add(spec.Value);
            }
            if (cbSpecialList.Items.Count > 0)
            {
                cbSpecialList.SelectedIndex = 0;
            }
            foreach( MobSpecial spec in data )
            {
                lstSpecials.Items.Add(spec);
            }
        }

        public EditSpecial(List<ObjSpecial> data)
        {
            InitializeComponent();
            foreach (KeyValuePair<string, ObjSpecial> spec in ObjSpecial.ObjectSpecialTable)
            {
                cbSpecialList.Items.Add(spec);
            }
            if (cbSpecialList.Items.Count > 0)
            {
                cbSpecialList.SelectedIndex = 0;
            }
            foreach( ObjSpecial spec in data )
            {
                lstSpecials.Items.Add(spec);
            }
        }

        public List<MobSpecial> GetMobSpecials()
        {
            List<MobSpecial> list = new List<MobSpecial>();
            foreach (object obj in lstSpecials.Items)
            {
                if (obj is MobSpecial)
                {
                    MobSpecial spec = obj as MobSpecial;
                    list.Add(spec);
                }
            }
            return list;
        }

        public List<ObjSpecial> GetObjSpecials()
        {
            List<ObjSpecial> list = new List<ObjSpecial>();
            foreach (object obj in lstSpecials.Items)
            {
                if (obj is ObjSpecial)
                {
                    ObjSpecial spec = obj as ObjSpecial;
                    list.Add(spec);
                }
            }
            return list;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAddSpecial_Click(object sender, EventArgs e)
        {
            if (cbSpecialList.SelectedIndex > -1)
            {
                lstSpecials.Items.Add(cbSpecialList.SelectedItem);
            }
        }

        private void btnRemoveSpecial_Click(object sender, EventArgs e)
        {
            if (lstSpecials.SelectedIndex != -1)
            {
                lstSpecials.Items.RemoveAt(lstSpecials.SelectedIndex);
            }
        }

    }
}