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
    public partial class EditAreaSettings : Form
    {
        Area _area = null;
        public EditAreaSettings()
        {
            InitializeComponent();
            for (int count = 0; count <= 41; ++count)
            {
                cbMinLevel.Items.Add(count.ToString());
                cbMaxLevel.Items.Add(count.ToString());
            }
            Type types = typeof(Area.ResetMode);
            foreach (string s in Enum.GetNames(types))
            {
                cbResetMode.Items.Add(s);
            }
            types = typeof(JusticeType);
            foreach (string s in Enum.GetNames(types))
            {
                cbJusticeType.Items.Add(s);
            }
            this.FormClosing += new FormClosingEventHandler( EditAreaSettings_FormClosing );
        }

        /// <summary>
        /// Sets the current area and updates the window contents.
        /// </summary>
        /// <param name="area"></param>
        public void UpdateData(Area area)
        {
            _area = area;
            if (_area != null)
            {
                txtAuthor.Text = area.Author;
                txtBuilders.Text = area.Builders;
                txtName.Text = area.Name;
                cbMinLevel.SelectedIndex = area.MinRecommendedLevel;
                txtResetMsg.Text = area.ResetMessage;
                txtVersion.Text = area.Version.ToString();
                txtRecall.Text = area.Recall.ToString();
                txtSquadSize.Text = area.DefendersPerSquad.ToString();
                txtNumSquads.Text = area.DefenderSquads.ToString();
                cbJusticeType.Text = area.JusticeType.ToString();
                txtJudgeRoom.Text = area.JudgeRoom.ToString();
                txtJailRoom.Text = area.JailRoom.ToString();
                txtGuardIndexNumber.Text = area.DefenderTemplateNumber.ToString();
                txtBarracks.Text = area.BarracksRoom.ToString();
                txtMinutesBetweenResets.Text = area.MinutesBetweenResets.ToString();
                txtHeight.Text = area.Height.ToString();
                txtWidth.Text = area.Width.ToString();
                txtAreaFlags.Text = area.AreaFlags[0].ToString();
                if (area.MaxRecommendedLevel < cbMaxLevel.Items.Count)
                {
                    cbMaxLevel.SelectedIndex = area.MaxRecommendedLevel;
                }
                if ((int)area.AreaResetMode < cbResetMode.Items.Count)
                {
                    cbResetMode.SelectedIndex = (int)area.AreaResetMode;
                }
            }
        }

        /// <summary>
        /// Resets all fields in the window by telling it to refresh itself
        /// with the currently loaded area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            _area.Name = txtName.Text;
            _area.Author = txtAuthor.Text;
            _area.Builders = txtBuilders.Text;
            _area.MinRecommendedLevel = cbMinLevel.SelectedIndex;
            _area.MaxRecommendedLevel = cbMaxLevel.SelectedIndex;
            int value = 0;
            if (Int32.TryParse(txtVersion.Text, out value))
                _area.Version = value;
            if (Int32.TryParse(txtRecall.Text, out value))
                _area.Recall = value;
            if (Int32.TryParse(txtSquadSize.Text, out value))
                _area.DefendersPerSquad = value;
            if (Int32.TryParse(txtNumSquads.Text, out value))
                _area.DefenderSquads = value;
            if (Int32.TryParse(txtJudgeRoom.Text, out value))
                _area.JudgeRoom = value;
            if (Int32.TryParse(txtJailRoom.Text, out value))
                _area.JailRoom = value;
            if (Int32.TryParse(txtGuardIndexNumber.Text, out value))
                _area.DefenderTemplateNumber = value;
            if( Int32.TryParse( txtBarracks.Text, out value ))
                 _area.BarracksRoom = value;
            if( Int32.TryParse(txtMinutesBetweenResets.Text, out value))
                _area.MinutesBetweenResets = value;
            if (Int32.TryParse(txtWidth.Text, out value))
                _area.Width = value;
            if (Int32.TryParse(txtHeight.Text, out value))
                _area.Height = value;
            if (Int32.TryParse(txtAreaFlags.Text, out value))
                _area.AreaFlags[0] = value;
            if (cbResetMode.SelectedIndex != -1)
            {
                _area.AreaResetMode = (Area.ResetMode)cbResetMode.SelectedIndex;
            }
            if (cbJusticeType.SelectedIndex != -1)
            {
                _area.JusticeType = (JusticeType)cbJusticeType.SelectedIndex;
            }
            _area.ResetMessage = txtResetMsg.Text;
            Close();
        }

        private void txtName_TextChanged( object sender, EventArgs e )
        {
            MainForm.BuildRTFString( txtName.Text, rtbAreaName );
        }

        private void EditAreaSettings_FormClosing( object sender, FormClosingEventArgs e )
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void txtResetMsg_TextChanged( object sender, EventArgs e )
        {
            MainForm.BuildRTFString( txtResetMsg.Text, rtbResetMsg );
        }

        private void btnFindRecall_Click( object sender, EventArgs e )
        {
            SelectRoom dlg = new SelectRoom( _area );
            if( dlg.ShowDialog() == DialogResult.OK )
            {
                txtRecall.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindJudge_Click( object sender, EventArgs e )
        {
            SelectRoom dlg = new SelectRoom( _area );
            if( dlg.ShowDialog() == DialogResult.OK )
            {
                txtJudgeRoom.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindJail_Click( object sender, EventArgs e )
        {
            SelectRoom dlg = new SelectRoom( _area );
            if( dlg.ShowDialog() == DialogResult.OK )
            {
                txtJailRoom.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindBarracks_Click( object sender, EventArgs e )
        {
            SelectRoom dlg = new SelectRoom( _area );
            if( dlg.ShowDialog() == DialogResult.OK )
            {
                txtBarracks.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindGuards_Click( object sender, EventArgs e )
        {
            SelectMob dlg = new SelectMob( _area );
            if( dlg.ShowDialog() == DialogResult.OK )
            {
                txtGuardIndexNumber.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnEditFlags_Click(object sender, EventArgs e)
        {
            int value = 0;
            bool parsed = Int32.TryParse(txtAreaFlags.Text, out value);
            FlagEditor editor = new FlagEditor(BitvectorFlagType.AreaFlags, value, 0);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAreaFlags.Text = editor.Value.ToString();
            }
        }
    }
}