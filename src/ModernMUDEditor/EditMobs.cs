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
    public partial class EditMobs : Form
    {
        private Area _area = null;
        private MainForm _parent = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public EditMobs(MainForm parent)
        {
            InitializeComponent();
            _parent = parent;
            cbPosition.Items.Add(Position.PositionString(Position.standing));
            cbPosition.Items.Add(Position.PositionString(Position.kneeling));
            cbPosition.Items.Add(Position.PositionString(Position.reclining));
            cbPosition.Items.Add(Position.PositionString(Position.sitting));
            cbPosition.Items.Add(Position.PositionString(Position.resting));
            cbPosition.Items.Add(Position.PositionString(Position.sleeping));
            for (int cclass = 0; cclass < CharClass.ClassList.Length; cclass++)
            {
                cbClass.Items.Add(CharClass.ClassList[cclass].Name);
            }
            for (int race = 0; race < Race.RaceList.Length; race++)
            {
                cbRace.Items.Add(Race.RaceList[race].Name);
            }
            cbSex.Items.Add(MobTemplate.Sex.male);
            cbSex.Items.Add(MobTemplate.Sex.female);
            cbSex.Items.Add(MobTemplate.Sex.neutral);
            this.FormClosing += new FormClosingEventHandler( EditMobs_FormClosing );
            SetControlAvailability();
        }

        public void AddNewMob()
        {
            btnNew_Click(null, null);
        }

        public void UpdateData(Area area)
        {
            mobList.Items.Clear();
            _area = area;
            if (area != null)
            {
                UpdateMobList();
            }
            SetControlAvailability();
        }

        public void SetActiveMob(int indexNumber)
        {
            for (int i = 0; i < _area.Mobs.Count; i++)
            {
                if (_area.Mobs[i].IndexNumber == indexNumber)
                {
                    mobList.SelectedIndex = i;
                    UpdateWindowContents(_area.Mobs[i]);
                }
            }
        }

        private void UpdateMobList()
        {
            int index = mobList.SelectedIndex;
            bool empty = (mobList.Items.Count == 0);

            mobList.Items.Clear();
            foreach (MobTemplate mob in _area.Mobs)
            {
                mobList.Items.Add(ModernMUD.Color.RemoveColorCodes(mob.ShortDescription));
            }
            if (empty && mobList.Items.Count > 0)
            {
                mobList.SelectedIndex = 0;
                UpdateWindowContents(_area.Mobs[mobList.SelectedIndex]);
            }
            else
            {
                mobList.SelectedIndex = index;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (mobList.Items.Count > 0 && mobList.SelectedIndex > 0)
            {
                ApplyWindowContents();
                --mobList.SelectedIndex;
                UpdateWindowContents(_area.Mobs[mobList.SelectedIndex]);
            }
        }

        private void btnFwd_Click(object sender, EventArgs e)
        {
            if ((mobList.SelectedIndex + 1) < mobList.Items.Count)
            {
                ApplyWindowContents();
                ++mobList.SelectedIndex;
                UpdateWindowContents(_area.Mobs[mobList.SelectedIndex]);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ApplyWindowContents();
            MobTemplate mob = new MobTemplate();
            if( _area.HighMobIndexNumber >= 0 )
            {
                mob.IndexNumber = _area.HighMobIndexNumber + 1;
            }
            _area.Mobs.Add( mob );
            _area.RebuildIndexes();
            UpdateMobList();
            SetControlAvailability();
            mobList.SelectedIndex = mobList.Items.Count - 1;
            UpdateWindowContents(mob);
            _parent.UpdateStatusBar();
         }

        private void UpdateWindowContents(MobTemplate mob)
        {
            txtAlignment.Text = mob.Alignment.ToString();
            txtDescription.Text = mob.Description;
            txtFullDescription.Text = mob.FullDescription;
            txtKeywords.Text = mob.PlayerName;
            txtLevel.Text = mob.Level.ToString();
            txtShortDescription.Text = mob.ShortDescription;
            txtIndexNumber.Text = mob.IndexNumber.ToString();
            cbSex.SelectedIndex = (int)mob.Gender;
            //cbPosition.Text = StringConversion.position_string(mob._position);
            cbPosition.SelectedItem = Position.PositionString( mob.DefaultPosition );
            //cbClass.Text = mob._characterClass._name;
            cbClass.SelectedItem = CharClass.ClassList[(int)mob.CharacterClass.ClassNumber].Name;
            //cbRace.Text = Race.RaceList[mob._race]._name;
            cbRace.SelectedItem = Race.RaceList[ mob.Race ].Name;
            txtActFlags.Text = mob.ActionFlags[0].ToString();
            txtActFlags2.Text = mob.ActionFlags[1].ToString();
            txtAffectFlags1.Text = mob.AffectedBy[0].ToString();
            txtAffectFlags2.Text = mob.AffectedBy[1].ToString();
            txtAffectFlags3.Text = mob.AffectedBy[2].ToString();
            txtAffectFlags4.Text = mob.AffectedBy[3].ToString();
            txtAffectFlags5.Text = mob.AffectedBy[4].ToString();
            btnEditShop.ForeColor = System.Drawing.Color.Gray;
            foreach (Shop shop in _area.Shops)
            {
                if (mob.IndexNumber == shop.Keeper)
                {
                    btnEditShop.ForeColor = System.Drawing.Color.Black;
                    break;
                }
            }
            btnEditQuests.ForeColor = System.Drawing.Color.Gray;
            foreach (QuestTemplate qst in _area.Quests)
            {
                if (mob.IndexNumber == qst.IndexNumber)
                {
                    btnEditQuests.ForeColor = System.Drawing.Color.Black;
                    break;
                }
            }
            if (mob.SpecFun.Count > 0)
            {
                btnEditSpecials.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                btnEditSpecials.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void mobList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateWindowContents(_area.Mobs[mobList.SelectedIndex]);
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
            if( mobList.SelectedIndex != -1 )
            {
                if (!String.IsNullOrEmpty(txtShortDescription.Text) && (string)mobList.Items[mobList.SelectedIndex] !=
                    ModernMUD.Color.RemoveColorCodes(txtShortDescription.Text))
                {
                    mobList.SelectedIndexChanged -= mobList_SelectedIndexChanged;
                    mobList.Items[mobList.SelectedIndex] = ModernMUD.Color.RemoveColorCodes(txtShortDescription.Text);
                    mobList.SelectedIndexChanged += mobList_SelectedIndexChanged;
                }
                int value;
                if (Int32.TryParse(txtIndexNumber.Text, out value))
                {
                    _area.Mobs[mobList.SelectedIndex].IndexNumber = value;
                }
                _area.Mobs[ mobList.SelectedIndex ].Description = txtDescription.Text;
                _area.Mobs[ mobList.SelectedIndex ].FullDescription = txtFullDescription.Text;
                if (Int32.TryParse(txtAlignment.Text, out value))
                {
                    _area.Mobs[mobList.SelectedIndex].Alignment = value;
                }
                if(Int32.TryParse( txtLevel.Text, out value ))
                {
                    _area.Mobs[ mobList.SelectedIndex ].Level = value;
                }
                _area.Mobs[ mobList.SelectedIndex ].ShortDescription = txtShortDescription.Text;
                _area.Mobs[mobList.SelectedIndex].PlayerName = txtKeywords.Text;

                // TODO: Retrieve mob special functions
                // TODO: Grab all data from dialog.

                if( cbSex.SelectedIndex != -1 )
                    _area.Mobs[ mobList.SelectedIndex ].Gender = (MobTemplate.Sex)cbSex.SelectedIndex;
                if( cbPosition.SelectedIndex != -1 )
                    _area.Mobs[ mobList.SelectedIndex ].DefaultPosition = Position.PositionLookup( cbPosition.Items[ cbPosition.SelectedIndex ] as string );
                if (cbClass.SelectedIndex != -1)
                {
                    _area.Mobs[mobList.SelectedIndex].CharacterClass = CharClass.ClassList[(int)CharClass.ClassLookup((String)cbClass.SelectedItem)];
                    _area.Mobs[mobList.SelectedIndex].ClassName = (String)cbClass.SelectedItem;
                }
                if (cbRace.SelectedIndex != -1)
                {
                    _area.Mobs[mobList.SelectedIndex].Race = Race.RaceLookup( (string)cbRace.SelectedItem );
                }
                if(Int32.TryParse( txtAffectFlags1.Text, out value ))
                {
                    _area.Mobs[ mobList.SelectedIndex ].AffectedBy[ 0 ] = value;
                }
                if(Int32.TryParse( txtAffectFlags2.Text, out value ))
                {
                    _area.Mobs[ mobList.SelectedIndex ].AffectedBy[ 1 ] = value;
                }
                if(Int32.TryParse( txtAffectFlags3.Text, out value ))
                {
                    _area.Mobs[ mobList.SelectedIndex ].AffectedBy[ 2 ] = value;
                }
                if(Int32.TryParse( txtAffectFlags4.Text, out value ))
                {
                    _area.Mobs[ mobList.SelectedIndex ].AffectedBy[ 3 ] = value;
                }
                if(Int32.TryParse( txtAffectFlags5.Text, out value ))
                {
                    _area.Mobs[ mobList.SelectedIndex ].AffectedBy[ 4 ] = value;
                }
                if(Int32.TryParse( txtActFlags.Text, out value))
                {
                    _area.Mobs[ mobList.SelectedIndex ].ActionFlags[ 0 ] = value;
                }
                if(Int32.TryParse( txtActFlags2.Text, out value ))
                {
                    _area.Mobs[ mobList.SelectedIndex ].ActionFlags[ 1 ] = value;
                }
            }
        }

        private void txtShortDescription_TextChanged(object sender, EventArgs e)
        {
            MainForm.BuildRTFString(txtShortDescription.Text, rtbShortDesc);
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            MainForm.BuildRTFString(txtDescription.Text, rtbDescription);
        }

        private void txtFullDescription_TextChanged(object sender, EventArgs e)
        {
            MainForm.BuildRTFString(txtFullDescription.Text, rtbFullDescription);
        }

        private void EditMobs_FormClosing( object sender, FormClosingEventArgs e )
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void btnEditActFlags_Click( object sender, EventArgs e )
        {
            int value = 0;
            bool parsed = Int32.TryParse( txtActFlags.Text, out value );
            FlagEditor editor = new FlagEditor( BitvectorFlagType.MobActFlags, value, 0 );
            DialogResult result = editor.ShowDialog();
            if( result == DialogResult.OK )
            {
                txtActFlags.Text = editor.Value.ToString();
            }
        }

        private void btnEditShop_Click(object sender, EventArgs e)
        {
            int indexNumber = 0;
            Int32.TryParse( txtIndexNumber.Text, out indexNumber );
            if (indexNumber != 0)
            {
                for (int x = 0; x < _area.Shops.Count; x++)
                {
                    if (_area.Shops[x].Keeper == indexNumber)
                    {
                        _parent.ShowShop(x);
                        return;
                    }
                }
            }
            MessageBox.Show("No shop data to edit.");
            return;
         }

        private void btnEditQuests_Click(object sender, EventArgs e)
        {
            int indexNumber = 0;
            Int32.TryParse(txtIndexNumber.Text, out indexNumber);
            if (indexNumber != 0)
            {
                for (int x = 0; x < _area.Quests.Count; x++)
                {
                    if (_area.Quests[x].IndexNumber == indexNumber)
                    {
                        _parent.ShowQuest(x);
                        return;
                    }
                }
            }
            MessageBox.Show("No quest data to edit.");
            return;
        }

        private void btnEditSpecials_Click(object sender, EventArgs e)
        {
            if( mobList.SelectedIndex == -1 )
            {
                MessageBox.Show( "No valid mob data to edit special functions on.");
                return;
            }
            EditSpecial dlg = new EditSpecial(_area.Mobs[mobList.SelectedIndex].SpecFun);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _area.Mobs[mobList.SelectedIndex].SpecFun = dlg.GetMobSpecials();
            }
        }

        private void btnEditActFlags2_Click(object sender, EventArgs e)
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtActFlags2.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.MobActFlags, value, 1);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtActFlags2.Text = editor.Value.ToString();
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

        /// <summary>
        /// Deletes the current mob and moves to the previous, if available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = mobList.SelectedIndex;
            if (index == -1) return;
            if (index < mobList.Items.Count)
            {
                _area.Mobs.RemoveAt(index);
                mobList.Items.RemoveAt(index);
                SetControlAvailability();
            }
            if (index > 0)
            {
                --index;
            }
            if (index >= 0 && index < mobList.Items.Count)
            {
                mobList.SelectedIndex = index;
                UpdateWindowContents(_area.Mobs[mobList.SelectedIndex]);
            }
        }

        private void SetControlAvailability()
        {
            bool enabled = true;
            if (_area == null || _area.Mobs.Count < 1)
            {
                enabled = false;
            }
            txtActFlags.Enabled = enabled;
            txtActFlags2.Enabled = enabled;
            txtAffectFlags1.Enabled = enabled;
            txtAffectFlags2.Enabled = enabled;
            txtAffectFlags3.Enabled = enabled;
            txtAffectFlags4.Enabled = enabled;
            txtAffectFlags5.Enabled = enabled;
            txtAlignment.Enabled = enabled;
            txtDescription.Enabled = enabled;
            txtFullDescription.Enabled = enabled;
            txtIndexNumber.Enabled = enabled;
            txtLevel.Enabled = enabled;
            txtShortDescription.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btnClone.Enabled = enabled;
            btnEditActFlags.Enabled = enabled;
            btnEditActFlags2.Enabled = enabled;
            btnEditAffectFlags1.Enabled = enabled;
            btnEditAffectFlags2.Enabled = enabled;
            btnEditAffectFlags3.Enabled = enabled;
            btnEditAffectFlags4.Enabled = enabled;
            btnEditAffectFlags5.Enabled = enabled;
            btnEditQuests.Enabled = enabled;
            btnEditShop.Enabled = enabled;
            btnEditSpecials.Enabled = enabled;
            btnFwd.Enabled = enabled;
            btnBack.Enabled = enabled;
            cbClass.Enabled = enabled;
            cbPosition.Enabled = enabled;
            cbRace.Enabled = enabled;
            cbSex.Enabled = enabled;
            txtKeywords.Enabled = enabled;
            btnEditCustomActions.Enabled = enabled;
        }

        private void btnEditCustomActions_Click(object sender, EventArgs e)
        {
            if (mobList.SelectedIndex == -1 || mobList.SelectedIndex > (mobList.Items.Count - 1))
            {
                MessageBox.Show("Cannot edit custom actions without a valid mobile selected.");
                return;
            }

            EditCustomActions dlg = new EditCustomActions(_area, _area.Mobs[mobList.SelectedIndex].CustomActions);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _area.Mobs[mobList.SelectedIndex].CustomActions = dlg.GetActions();
            }
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            if (mobList.SelectedIndex == -1 || mobList.SelectedIndex > (mobList.Items.Count - 1))
            {
                MessageBox.Show("Cannot clone a mob without a valid mob selected.");
                return;
            }
            ApplyWindowContents();
            MobTemplate mob = new MobTemplate(_area.Mobs[mobList.SelectedIndex]);
            if (_area.HighMobIndexNumber >= 0)
            {
                mob.IndexNumber = _area.HighMobIndexNumber + 1;
            }
            _area.Mobs.Add(mob);
            _area.RebuildIndexes();
            UpdateMobList();
            SetControlAvailability();
            mobList.SelectedIndex = mobList.Items.Count - 1;
            UpdateWindowContents(mob);
            _parent.UpdateStatusBar();
        }
    }
}