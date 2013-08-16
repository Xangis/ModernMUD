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
    public partial class EditObjects : Form
    {
        private Area _area = null;
        private MainForm _parent = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public EditObjects(MainForm parent)
        {
            InitializeComponent();
            // TODO: Make this initialize combo boxes properly.
            // TODO: Create a material_string function.
            Type types = typeof(Material.MaterialType);
            foreach (string s in Enum.GetNames(types))
            {
                cbMaterial.Items.Add(s);
            }
            for (int count = 0; count < ObjTemplate.MAX_CRAFT; count++)
            {
                cbCraftsmanship.Items.Add(ObjTemplate.CraftsmanshipString((ObjTemplate.Craftsmanship)count));
            }
            for (int count = 0; count < Race.MAX_SIZE; count++)
            {
                cbSize.Items.Add(Race.SizeString((Race.Size)count));
            }
            for (int count = 0; count < ObjTemplate.MAX_ITEM_TYPE; count++)
            {
                string str = ((ObjTemplate.ObjectType)count).ToString();
                if( !String.IsNullOrEmpty(str))
                {
                    cbItemType.Items.Add(str);
                }
            }
            this.FormClosing += new FormClosingEventHandler( EditObjects_FormClosing );
            _parent = parent;
            SetControlAvailability();
        }

        public void UpdateData(Area area)
        {
            objectList.Items.Clear();
            _area = area;
            if (area != null)
            {
                UpdateObjList();
            }
            SetControlAvailability();
        }

        public void AddNewObject()
        {
            btnNew_Click(null, null);
        }

        public void SetActiveObject(int indexNumber)
        {
            for (int i = 0; i < _area.Objects.Count; i++)
            {
                if (_area.Objects[i].IndexNumber == indexNumber)
                {
                    objectList.SelectedIndex = i;
                    UpdateWindowContents(_area.Objects[i]);
                }
            }
        }

        private void UpdateObjList()
        {
            int index = objectList.SelectedIndex;
            bool empty = (objectList.Items.Count == 0);

            objectList.Items.Clear();
            foreach (ObjTemplate obj in _area.Objects)
            {
                objectList.Items.Add(ModernMUD.Color.RemoveColorCodes(obj.ShortDescription));
            }
            if (objectList.Items.Count > 0)
            {
                objectList.SelectedIndex = 0;
                UpdateWindowContents(_area.Objects[objectList.SelectedIndex]);
            }
            else
            {
                objectList.SelectedIndex = index;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (objectList.Items.Count > 0 && objectList.SelectedIndex > 0)
            {
                ApplyWindowContents();
                --objectList.SelectedIndex;
                UpdateWindowContents(_area.Objects[objectList.SelectedIndex]);
            }
        }

        private void btnFwd_Click(object sender, EventArgs e)
        {
            if ((objectList.SelectedIndex + 1) < objectList.Items.Count)
            {
                ApplyWindowContents();
                ++objectList.SelectedIndex;
                UpdateWindowContents(_area.Objects[objectList.SelectedIndex]);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ApplyWindowContents();
            ObjTemplate obj = new ObjTemplate();
            if( _area.HighObjIndexNumber >= 0 )
            {
                obj.IndexNumber = _area.HighObjIndexNumber + 1;
            }
            _area.Objects.Add( obj );
            _area.RebuildIndexes();
            UpdateObjList();
            SetControlAvailability();
            objectList.SelectedIndex = objectList.Items.Count - 1;
            UpdateWindowContents(obj);
            _parent.UpdateStatusBar();
        }

        private void UpdateWindowContents( ObjTemplate obj )
        {
            txtCondition.Text = obj.Condition.ToString();
            txtFullDescription.Text = obj.FullDescription;
            txtName.Text = obj.Name;
            txtShortDescription.Text = obj.ShortDescription;
            txtIndexNumber.Text = obj.IndexNumber.ToString();
            txtWeight.Text = obj.Weight.ToString();
            txtLevel.Text = obj.Level.ToString();
            txtVolume.Text = obj.Volume.ToString();
            cbCraftsmanship.SelectedIndex = (int)obj.CraftsmanshipLevel;
            int item = cbMaterial.Items.IndexOf(obj.Material.ToString());
            cbMaterial.SelectedIndex = item;
            cbSize.SelectedIndex = (int)obj.Size;
            cbItemType.SelectedItem = obj.ItemType.ToString();
            txtMaxInGame.Text = obj.MaxNumber.ToString();
            txtExtraFlags.Text = obj.ExtraFlags[0].ToString();
            txtExtraFlags2.Text = obj.ExtraFlags[1].ToString();
            txtAffectFlags1.Text = obj.AffectedBy[0].ToString();
            txtAffectFlags2.Text = obj.AffectedBy[1].ToString();
            txtAffectFlags3.Text = obj.AffectedBy[2].ToString();
            txtAffectFlags4.Text = obj.AffectedBy[3].ToString();
            txtAffectFlags5.Text = obj.AffectedBy[4].ToString();
            txtUseFlags.Text = obj.UseFlags[0].ToString();
            txtUseFlags2.Text = obj.UseFlags[1].ToString();
            txtWearFlags.Text = obj.WearFlags[0].ToString();
            lstExtraDesc.Items.Clear();
            foreach (ExtendedDescription desc in obj.ExtraDescriptions)
            {
                lstExtraDesc.Items.Add( desc );
            }
            if (obj.SpecFun.Count > 0)
            {
                btnEditSpecials.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                btnEditSpecials.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void objectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateWindowContents(_area.Objects[objectList.SelectedIndex]);
        }

        /// <summary>
        /// Resets the window by telling it to refresh with the currently selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyWindowContents();
            Close();
        }

        private void ApplyWindowContents()
        {
            if( objectList.SelectedIndex != -1 )
            {
                if (!String.IsNullOrEmpty(txtShortDescription.Text) && (string)objectList.Items[objectList.SelectedIndex] !=
                    ModernMUD.Color.RemoveColorCodes(txtShortDescription.Text))
                {
                    objectList.SelectedIndexChanged -= objectList_SelectedIndexChanged;
                    objectList.Items[objectList.SelectedIndex] = ModernMUD.Color.RemoveColorCodes(txtShortDescription.Text);
                    objectList.SelectedIndexChanged += objectList_SelectedIndexChanged;
                }
                int value;
                if (Int32.TryParse(txtIndexNumber.Text, out value))
                {
                    _area.Objects[objectList.SelectedIndex].IndexNumber = value;
                }
                _area.Objects[ objectList.SelectedIndex ].FullDescription = txtFullDescription.Text;
                _area.Objects[ objectList.SelectedIndex ].Name = txtName.Text;
                _area.Objects[ objectList.SelectedIndex ].ShortDescription = txtShortDescription.Text;
                if(Int32.TryParse( txtWeight.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].Weight = value;
                }
                if(Int32.TryParse( txtLevel.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].Level = value;
                }
                if(Int32.TryParse( txtVolume.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].Volume = value;
                }
                _area.Objects[ objectList.SelectedIndex ].CraftsmanshipLevel = (ObjTemplate.Craftsmanship)cbCraftsmanship.SelectedIndex;
                _area.Objects[ objectList.SelectedIndex ].Size = (Race.Size)cbSize.SelectedIndex;
                _area.Objects[ objectList.SelectedIndex ].ItemType = (ObjTemplate.ObjectType)(Enum.Parse(typeof(ObjTemplate.ObjectType), (String)cbItemType.SelectedItem));
                _area.Objects[ objectList.SelectedIndex ].Material = (Material.MaterialType)Enum.Parse(typeof(Material.MaterialType), (String)cbMaterial.SelectedItem);
                if (Int32.TryParse(txtMaxInGame.Text, out value))
                {
                    _area.Objects[objectList.SelectedIndex].MaxNumber = value;
                }
                if(Int32.TryParse( txtExtraFlags.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].ExtraFlags[ 0 ] = value;
                }
                if(Int32.TryParse( txtExtraFlags2.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].ExtraFlags[ 1 ] = value;
                }
                if(Int32.TryParse( txtAffectFlags1.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].AffectedBy[ 0 ] = value;
                }
                if(Int32.TryParse( txtAffectFlags2.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].AffectedBy[ 1 ] = value;
                }
                if(Int32.TryParse( txtAffectFlags3.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].AffectedBy[ 2 ] = value;
                }
                if(Int32.TryParse( txtAffectFlags4.Text, out value))
                {
                    _area.Objects[ objectList.SelectedIndex ].AffectedBy[ 3 ] = value;
                }
                if(Int32.TryParse( txtAffectFlags5.Text, out value))
                {
                    _area.Objects[ objectList.SelectedIndex ].AffectedBy[ 4 ] = value;
                }
                if(Int32.TryParse( txtUseFlags2.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].UseFlags[1] = value;
                }
                if(Int32.TryParse( txtUseFlags.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].UseFlags[0] = value;
                }
                if(Int32.TryParse( txtWearFlags.Text, out value ))
                {
                    _area.Objects[ objectList.SelectedIndex ].WearFlags[0] = value;
                }
                _area.Objects[objectList.SelectedIndex].ExtraDescriptions.Clear();
                foreach( object obj in lstExtraDesc.Items )
                {
                    if (obj is ExtendedDescription)
                    {
                        _area.Objects[objectList.SelectedIndex].ExtraDescriptions.Add((ExtendedDescription)obj);
                    }
                }
            }
        }

        private void txtShortDescription_TextChanged( object sender, EventArgs e )
        {
            MainForm.BuildRTFString( txtShortDescription.Text, rtbShortDesc );
        }

        private void txtFullDescription_TextChanged( object sender, EventArgs e )
        {
            MainForm.BuildRTFString( txtFullDescription.Text, rtbFullDescription );
        }

        private void EditObjects_FormClosing( object sender, FormClosingEventArgs e )
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void btnEditExtraFlags_Click( object sender, EventArgs e )
        {
            int value = 0;
            bool parsed = Int32.TryParse( txtExtraFlags.Text, out value );
            FlagEditor editor = new FlagEditor( BitvectorFlagType.ItemFlags, value, 0 );
            DialogResult result = editor.ShowDialog();
            if( result == DialogResult.OK )
            {
                txtExtraFlags.Text = editor.Value.ToString();
            }
        }

        private void btnEditWearFlags_Click( object sender, EventArgs e )
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtWearFlags.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.WearFlags, value, 0);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtWearFlags.Text = editor.Value.ToString();
            }
        }

        private void btnEditUseFlags_Click( object sender, EventArgs e )
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtUseFlags.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.UseFlags, value, 0);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtUseFlags.Text = editor.Value.ToString();
            }
        }

        private void btnEditUseFlags2_Click( object sender, EventArgs e )
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtUseFlags2.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.UseFlags, value, 1);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtUseFlags2.Text = editor.Value.ToString();
            }
        }

        private void btnAddExtraDesc_Click( object sender, EventArgs e )
        {
            EditExtendedDescription dialog = new EditExtendedDescription(new ExtendedDescription());
            if( dialog.ShowDialog() == DialogResult.OK )
            {
                lstExtraDesc.Items.Add( dialog.GetExtendedDescription() );
            }
        }

        private void btnEditExtraDescr_Click( object sender, EventArgs e )
        {
            int index = lstExtraDesc.SelectedIndex;
            if( index != -1 )
            {
                ExtendedDescription desc = lstExtraDesc.SelectedItem as ExtendedDescription;
                EditExtendedDescription dialog = new EditExtendedDescription( desc );
                if( dialog.ShowDialog() == DialogResult.OK )
                {
                    lstExtraDesc.Items.RemoveAt( index );
                    lstExtraDesc.Items.Insert( index, dialog.GetExtendedDescription() );
                }
            }
        }

        private void btnRemoveExtraDesc_Click( object sender, EventArgs e )
        {
            if( lstExtraDesc.SelectedIndex != -1 )
            {
                lstExtraDesc.Items.RemoveAt( lstExtraDesc.SelectedIndex );
            }
        }

        private void btnEditSpecials_Click(object sender, EventArgs e)
        {
            if (objectList.SelectedIndex == -1)
            {
                MessageBox.Show("No valid object data to edit special functions on.");
                return;
            }
            EditSpecial dlg = new EditSpecial(_area.Objects[objectList.SelectedIndex].SpecFun);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _area.Objects[objectList.SelectedIndex].SpecFun = dlg.GetObjSpecials();
            }
        }

        private void btnEditExtraFlags2_Click(object sender, EventArgs e)
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtExtraFlags2.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.ItemFlags, value, 1);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtExtraFlags2.Text = editor.Value.ToString();
            }
        }

        private void btnEditAffectFlags1_Click(object sender, EventArgs e)
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtAffectFlags1.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.AffectFlags, value, 0);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAffectFlags1.Text = editor.Value.ToString();
            }
        }

        private void btnEditAffectFlags2_Click(object sender, EventArgs e)
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtAffectFlags2.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.AffectFlags, value, 1);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAffectFlags2.Text = editor.Value.ToString();
            }
        }

        private void btnEditAffectFlags3_Click(object sender, EventArgs e)
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtAffectFlags3.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.AffectFlags, value, 2);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAffectFlags3.Text = editor.Value.ToString();
            }
        }

        private void btnEditAffectFlags4_Click(object sender, EventArgs e)
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtAffectFlags4.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.AffectFlags, value, 3);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAffectFlags4.Text = editor.Value.ToString();
            }
        }

        private void btnEditAffectFlags5_Click(object sender, EventArgs e)
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtAffectFlags5.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.AffectFlags, value, 4);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAffectFlags5.Text = editor.Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = objectList.SelectedIndex;
            if (index == -1) return;
            if (index < objectList.Items.Count)
            {
                _area.Objects.RemoveAt(index);
                objectList.Items.RemoveAt(index);
                SetControlAvailability();
            }
            if (index > 0)
            {
                --index;
            }
            if (index >= 0 && index < objectList.Items.Count)
            {
                objectList.SelectedIndex = index;
                UpdateWindowContents(_area.Objects[objectList.SelectedIndex]);
            }
        }

        private void SetControlAvailability()
        {
            bool enabled = true;
            if (_area == null || _area.Objects.Count < 1)
            {
                enabled = false;
            }
            txtAffectFlags1.Enabled = enabled;
            txtAffectFlags2.Enabled = enabled;
            txtAffectFlags3.Enabled = enabled;
            txtAffectFlags4.Enabled = enabled;
            txtAffectFlags5.Enabled = enabled;
            txtUseFlags.Enabled = enabled;
            txtUseFlags2.Enabled = enabled;
            txtCondition.Enabled = enabled;
            txtExtraFlags.Enabled = enabled;
            txtExtraFlags2.Enabled = enabled;
            txtFullDescription.Enabled = enabled;
            txtIndexNumber.Enabled = enabled;
            txtLevel.Enabled = enabled;
            txtMaxInGame.Enabled = enabled;
            txtName.Enabled = enabled;
            txtShortDescription.Enabled = enabled;
            txtVolume.Enabled = enabled;
            txtWearFlags.Enabled = enabled;
            txtWeight.Enabled = enabled;
            cbCraftsmanship.Enabled = enabled;
            cbItemType.Enabled = enabled;
            cbMaterial.Enabled = enabled;
            cbSize.Enabled = enabled;
            btnAddExtraDesc.Enabled = enabled;
            btnBack.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btnClone.Enabled = enabled;
            btnEditAffectFlags1.Enabled = enabled;
            btnEditAffectFlags2.Enabled = enabled;
            btnEditAffectFlags3.Enabled = enabled;
            btnEditAffectFlags4.Enabled = enabled;
            btnEditAffectFlags5.Enabled = enabled;
            btnEditUseFlags.Enabled = enabled;
            btnEditUseFlags2.Enabled = enabled;
            btnEditExtraDescr.Enabled = enabled;
            btnEditExtraFlags.Enabled = enabled;
            btnEditExtraFlags2.Enabled = enabled;
            btnEditSpecials.Enabled = enabled;
            btnEditValues.Enabled = enabled;
            btnEditWearFlags.Enabled = enabled;
            btnFwd.Enabled = enabled;
            btnRemoveExtraDesc.Enabled = enabled;
            btnEditAffects.Enabled = enabled;
            btnEditSpells.Enabled = enabled;
            btnEditCustomActions.Enabled = enabled;
        }

        private void btnEditValues_Click(object sender, EventArgs e)
        {
            if (objectList.SelectedIndex == -1 || objectList.SelectedIndex >= _area.Objects.Count)
            {
                MessageBox.Show("No valid object data to edit values for.");
                return;
            }
            EditValues dlg = new EditValues((ObjTemplate.ObjectType)(Enum.Parse(typeof(ObjTemplate.ObjectType), (String)cbItemType.SelectedItem)),
                _area.Objects[objectList.SelectedIndex].Values,
                "Editing " + txtShortDescription.Text + " (" + txtIndexNumber.Text + ")", _area );
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _area.Objects[objectList.SelectedIndex].Values = dlg.GetValues();
            }
        }

        private void btnEditAffects_Click(object sender, EventArgs e)
        {
            if (objectList.SelectedIndex == -1 || objectList.SelectedIndex >= _area.Objects.Count)
            {
                MessageBox.Show("No valid object data to edit affects for.");
            }
            EditAffect dlg = new EditAffect(_area.Objects[objectList.SelectedIndex].Affected);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _area.Objects[objectList.SelectedIndex].Affected = dlg.GetAffects();
            }
        }

        private void btnEditSpells_Click(object sender, EventArgs e)
        {
            ObjTemplate.ObjectType type = (ObjTemplate.ObjectType)(Enum.Parse(typeof(ObjTemplate.ObjectType), (String)cbItemType.SelectedItem));

            switch (type)
            {
                default:
                    MessageBox.Show("This item type cannot contain spells.");
                    break;
                case ObjTemplate.ObjectType.herb:
                case ObjTemplate.ObjectType.pill:
                case ObjTemplate.ObjectType.potion:
                case ObjTemplate.ObjectType.scroll:
                case ObjTemplate.ObjectType.spellbook:
                case ObjTemplate.ObjectType.staff:
                case ObjTemplate.ObjectType.trap:
                case ObjTemplate.ObjectType.wand:
                    EditSpells dlg = new EditSpells(_area.Objects[objectList.SelectedIndex].SpellEffects);
                    dlg.ShowDialog();
                    if (dlg.DialogResult == DialogResult.OK)
                    {
                        _area.Objects[objectList.SelectedIndex].SpellEffects = dlg.GetSpellEffects();
                    }
                    break;
            }
        }

        private void btnEditCustomActions_Click(object sender, EventArgs e)
        {
            if (objectList.SelectedIndex == -1 || objectList.SelectedIndex > (objectList.Items.Count - 1))
            {
                MessageBox.Show("Cannot edit custom actions without a valid object selected.");
                return;
            }

            EditCustomActions dlg = new EditCustomActions(_area, _area.Objects[objectList.SelectedIndex].CustomActions);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _area.Objects[objectList.SelectedIndex].CustomActions = dlg.GetActions();
            }
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            if (objectList.SelectedIndex == -1 || objectList.SelectedIndex > (objectList.Items.Count - 1))
            {
                MessageBox.Show("Cannot clone an object without a valid object selected.");
                return;
            }
            ApplyWindowContents();
            ObjTemplate obj = new ObjTemplate(_area.Objects[objectList.SelectedIndex]);
            if (_area.HighObjIndexNumber >= 0)
            {
                obj.IndexNumber = _area.HighObjIndexNumber + 1;
            }
            _area.Objects.Add(obj);
            _area.RebuildIndexes();
            UpdateObjList();
            SetControlAvailability();
            objectList.SelectedIndex = objectList.Items.Count - 1;
            UpdateWindowContents(obj);
            _parent.UpdateStatusBar();
        }
    }
}