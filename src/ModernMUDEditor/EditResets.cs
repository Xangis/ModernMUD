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
    public partial class EditResets : Form
    {
        Area _area = null;
        private MainForm _parent = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public EditResets(MainForm parent)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler( EditResets_FormClosing );
            _parent = parent;
            SetControlAvailability();
        }

        public void UpdateData(ModernMUD.Area area)
        {
            resetList.Items.Clear();
            _area = area;
            if (area != null)
            {
                UpdateResetList();
            }
            SetControlAvailability();
        }

        public void UpdateResetList()
        {
            int index = resetList.SelectedIndex;
            bool empty = (resetList.Items.Count == 0);

            resetList.Items.Clear();
            foreach (Reset reset in _area.Resets)
            {
                resetList.Items.Add(reset.Command + " " + reset.Arg0.ToString() + " " + reset.Arg1.ToString());
            }
            if (resetList.Items.Count > 0)
            {
                resetList.SelectedIndex = 0;
                UpdateWindowContents(_area.Resets[resetList.SelectedIndex]);
            }
            else
            {
                resetList.SelectedIndex = index;
            }
        }

        private void EditResets_Load(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ApplyWindowContents();
            Reset reset = new Reset();
            _area.Resets.Add( reset );
            UpdateResetList();
            SetControlAvailability();
            resetList.SelectedIndex = resetList.Items.Count - 1;
            UpdateWindowContents(reset);
            _parent.UpdateStatusBar();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (resetList.Items.Count > 0 && resetList.SelectedIndex > 0)
            {
                ApplyWindowContents();
                --resetList.SelectedIndex;
                UpdateWindowContents(_area.Resets[resetList.SelectedIndex]);
            }
        }

        private void btnFwd_Click(object sender, EventArgs e)
        {
            if ((resetList.SelectedIndex + 1 ) < resetList.Items.Count)
            {
                ApplyWindowContents();
                ++resetList.SelectedIndex;
                UpdateWindowContents(_area.Resets[resetList.SelectedIndex]);
            }
        }

        void UpdateWindowContents(Reset reset)
        {
            cbCommand.Text = reset.Command.ToString();
            txtArg0.Text = reset.Arg0.ToString();
            txtArg1.Text = reset.Arg1.ToString();
            txtArg2.Text = reset.Arg2.ToString();
            txtArg3.Text = reset.Arg3.ToString();
            txtArg4.Text = reset.Arg4.ToString();
            txtArg5.Text = reset.Arg5.ToString();
            txtArg6.Text = reset.Arg6.ToString();
            txtArg7.Text = reset.Arg7.ToString();
            lblDescription.Text = reset.ToString();
        }

        private void ApplyWindowContents()
        {
            if (resetList.SelectedIndex != -1)
            {
                if (!String.IsNullOrEmpty(cbCommand.Text) && (string)resetList.Items[resetList.SelectedIndex] !=
                    cbCommand.Text + " " + txtArg0.Text + " " + txtArg1.Text)
                {
                    resetList.SelectedIndexChanged -= resetList_SelectedIndexChanged;
                    resetList.Items[resetList.SelectedIndex] = cbCommand.Text + " " + txtArg0.Text + " " + txtArg1.Text;
                    resetList.SelectedIndexChanged += resetList_SelectedIndexChanged;
                }
                int value = 0;
                if (Int32.TryParse(txtArg0.Text, out value))
                {
                    _area.Resets[resetList.SelectedIndex].Arg0 = value;
                }
                if (Int32.TryParse(txtArg1.Text, out value))
                {
                    _area.Resets[resetList.SelectedIndex].Arg1 = value;
                }
                if (Int32.TryParse(txtArg2.Text, out value))
                {
                    _area.Resets[resetList.SelectedIndex].Arg2 = value;
                }
                if (Int32.TryParse(txtArg3.Text, out value))
                {
                    _area.Resets[resetList.SelectedIndex].Arg3 = value;
                }
                if (Int32.TryParse(txtArg4.Text, out value))
                {
                    _area.Resets[resetList.SelectedIndex].Arg4 = value;
                }
                if (Int32.TryParse(txtArg5.Text, out value))
                {
                    _area.Resets[resetList.SelectedIndex].Arg5 = value;
                }
                if (Int32.TryParse(txtArg6.Text, out value))
                {
                    _area.Resets[resetList.SelectedIndex].Arg6 = value;
                }
                if (Int32.TryParse(txtArg7.Text, out value))
                {
                    _area.Resets[resetList.SelectedIndex].Arg7 = value;
                }
                if (cbCommand.SelectedIndex != -1 && !String.IsNullOrEmpty(cbCommand.Text))
                {
                    _area.Resets[resetList.SelectedIndex].Command = cbCommand.Text[0];
                }
            }
        }

        private void resetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateWindowContents(_area.Resets[resetList.SelectedIndex]);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyWindowContents();
            Close();
        }

        public string BuildResetString( Reset reset )
        {
            string str = String.Empty;
            switch( reset.Command )
            {
                case 'M':
                    str = "Load a mobile";
                    break;
                case 'O':
                    str = "Load an object";
                    break;
                case 'P':
                    str = "Put object inside object";
                    break;
                case 'G':
                    str = "Give object to mobile";
                    break;
                case 'E':
                    str = "Equip mobile with object";
                    break;
                case 'D':
                    str = "Set door state";
                    break;
                case 'R':
                    str = "Randomize room exits";
                    break;
                case 'F':
                    str = "Set mob following";
                    break;
                case '*':
                    str = "Comment";
                    break;
            }
            return str;
        }

        private void EditResets_FormClosing( object sender, FormClosingEventArgs e )
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void cbCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateResetDescriptionWithoutCommittingChanges();
            switch ((String)cbCommand.SelectedItem)
            {
                case "D":
                    lblArg0.Text = "Not Used";
                    btnFindValue0.Enabled = false;
                    lblArg1.Text = "Room Number";
                    btnFindValue1.Enabled = true;
                    lblArg2.Text = "Door Direction";
                    btnFindValue2.Enabled = true;
                    lblArg3.Text = "Door State";
                    btnFindValue3.Enabled = true;
                    lblArg4.Text = "Not Used";
                    btnFindValue4.Enabled = false;
                    lblArg5.Text = "Not Used";
                    btnFindValue5.Enabled = false;
                    lblArg6.Text = "Not Used";
                    btnFindValue6.Enabled = false;
                    lblArg7.Text = "Not Used";
                    btnFindValue7.Enabled = false;
                    break;
                case "E":
                    lblArg0.Text = "Not Used";
                    btnFindValue0.Enabled = false;
                    lblArg1.Text = "Object Number";
                    btnFindValue1.Enabled = true;
                    lblArg2.Text = "Not Used";
                    btnFindValue2.Enabled = false;
                    lblArg3.Text = "EQ Slot";
                    btnFindValue3.Enabled = true;
                    lblArg4.Text = "Room Number";
                    btnFindValue4.Enabled = true;
                    lblArg5.Text = "Mob Number";
                    btnFindValue5.Enabled = true;
                    lblArg6.Text = "Not Used";
                    btnFindValue6.Enabled = false;
                    lblArg7.Text = "Not Used";
                    btnFindValue7.Enabled = false;
                    break;
                case "F":
                    lblArg0.Text = "Not Used";
                    btnFindValue0.Enabled = false;
                    lblArg1.Text = "Mob Number";
                    btnFindValue1.Enabled = true;
                    lblArg2.Text = "Not Used";
                    btnFindValue2.Enabled = false;
                    lblArg3.Text = "Room Number";
                    btnFindValue3.Enabled = true;
                    lblArg4.Text = "Not Used";
                    btnFindValue4.Enabled = false;
                    lblArg5.Text = "Not Used";
                    btnFindValue5.Enabled = false;
                    lblArg6.Text = "Not Used";
                    btnFindValue6.Enabled = false;
                    lblArg7.Text = "Not Used";
                    btnFindValue7.Enabled = false;
                    break;
                case "G":
                    lblArg0.Text = "Not Used";
                    btnFindValue0.Enabled = false;
                    lblArg1.Text = "Object Number";
                    btnFindValue1.Enabled = true;
                    lblArg2.Text = "Not Used";
                    btnFindValue2.Enabled = false;
                    lblArg3.Text = "Room Number";
                    btnFindValue3.Enabled = true;
                    lblArg4.Text = "Not Used";
                    btnFindValue4.Enabled = false;
                    lblArg5.Text = "Mob Number";
                    btnFindValue5.Enabled = true;
                    lblArg6.Text = "Not Used";
                    btnFindValue6.Enabled = false;
                    lblArg7.Text = "Not Used";
                    btnFindValue7.Enabled = false;
                    break;
                case "M":
                    lblArg0.Text = "Not Used";
                    btnFindValue0.Enabled = false;
                    lblArg1.Text = "Mob Number";
                    btnFindValue1.Enabled = true;
                    lblArg2.Text = "Not Used";
                    btnFindValue2.Enabled = false;
                    lblArg3.Text = "Room Number";
                    btnFindValue3.Enabled = true;
                    lblArg4.Text = "Not Used";
                    btnFindValue4.Enabled = false;
                    lblArg5.Text = "Not Used";
                    btnFindValue5.Enabled = false;
                    lblArg6.Text = "Not Used";
                    btnFindValue6.Enabled = false;
                    lblArg7.Text = "Not Used";
                    btnFindValue7.Enabled = false;
                    break;
                case "O":
                    lblArg0.Text = "Not Used";
                    btnFindValue0.Enabled = false;
                    lblArg1.Text = "Object Number";
                    btnFindValue1.Enabled = true;
                    lblArg2.Text = "Not Used";
                    btnFindValue2.Enabled = false;
                    lblArg3.Text = "Room Number";
                    btnFindValue3.Enabled = true;
                    lblArg4.Text = "Not Used";
                    btnFindValue4.Enabled = false;
                    lblArg5.Text = "Not Used";
                    btnFindValue5.Enabled = false;
                    lblArg6.Text = "Not Used";
                    btnFindValue6.Enabled = false;
                    lblArg7.Text = "Not Used";
                    btnFindValue7.Enabled = false;
                    break;
                case "P":
                    lblArg0.Text = "Not Used";
                    btnFindValue0.Enabled = false;
                    lblArg1.Text = "Object Number";
                    btnFindValue1.Enabled = true;
                    lblArg2.Text = "Not Used";
                    btnFindValue2.Enabled = false;
                    lblArg3.Text = "Target Object";
                    btnFindValue3.Enabled = true;
                    lblArg4.Text = "Not Used";
                    btnFindValue4.Enabled = false;
                    lblArg5.Text = "Room Number";
                    btnFindValue5.Enabled = true;
                    lblArg6.Text = "Not Used";
                    btnFindValue6.Enabled = false;
                    lblArg7.Text = "Not Used";
                    btnFindValue7.Enabled = false;
                    break;
                case "R":
                    lblArg0.Text = "Not Used";
                    btnFindValue0.Enabled = false;
                    lblArg1.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    lblArg2.Text = "Not Used";
                    btnFindValue2.Enabled = false;
                    lblArg3.Text = "Room Number";
                    btnFindValue3.Enabled = true;
                    lblArg4.Text = "Not Used";
                    btnFindValue4.Enabled = false;
                    lblArg5.Text = "Not Used";
                    btnFindValue5.Enabled = false;
                    lblArg6.Text = "Not Used";
                    btnFindValue6.Enabled = false;
                    lblArg7.Text = "Not Used";
                    btnFindValue7.Enabled = false;
                    break;
                case "*":
                    lblArg0.Text = "Not Used";
                    btnFindValue0.Enabled = false;
                    lblArg1.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    lblArg2.Text = "Not Used";
                    btnFindValue2.Enabled = false;
                    lblArg3.Text = "Not Used";
                    btnFindValue3.Enabled = false;
                    lblArg4.Text = "Not Used";
                    btnFindValue4.Enabled = false;
                    lblArg5.Text = "Not Used";
                    btnFindValue5.Enabled = false;
                    lblArg6.Text = "Not Used";
                    btnFindValue6.Enabled = false;
                    lblArg7.Text = "Not Used";
                    btnFindValue7.Enabled = false;
                    break;
                default:
                    btnFindValue0.Enabled = false;
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    btnFindValue5.Enabled = false;
                    btnFindValue6.Enabled = false;
                    btnFindValue7.Enabled = false;
                    break;
            }
        }

        private void UpdateResetDescriptionWithoutCommittingChanges()
        {
            Reset reset = new Reset();
            int value = 0;
            if (Int32.TryParse(txtArg0.Text, out value))
                reset.Arg0 = value;
            if (Int32.TryParse(txtArg1.Text, out value))
                reset.Arg1 = value;
            if (Int32.TryParse(txtArg2.Text, out value))
                reset.Arg2 = value;
            if (Int32.TryParse(txtArg3.Text, out value))
                reset.Arg3 = value;
            if (Int32.TryParse(txtArg4.Text, out value))
                reset.Arg4 = value;
            if (Int32.TryParse(txtArg5.Text, out value))
                reset.Arg5 = value;
            if (Int32.TryParse(txtArg6.Text, out value))
                reset.Arg6 = value;
            if (Int32.TryParse(txtArg7.Text, out value))
                reset.Arg7 = value;
            string cmd = (string)cbCommand.Text;
            if (!String.IsNullOrEmpty(cmd))
            {
                reset.Command = cmd[0];
            }
            lblDescription.Text = reset.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = resetList.SelectedIndex;
            if (index == -1) return;
            if (index < resetList.Items.Count)
            {
                _area.Resets.RemoveAt(index);
                resetList.Items.RemoveAt(index);
                SetControlAvailability();
            }
            if (index > 0)
            {
                --index;
            }
            if (index >= 0 && index < resetList.Items.Count)
            {
                resetList.SelectedIndex = index;
                UpdateWindowContents(_area.Resets[resetList.SelectedIndex]);
            }
        }

        private void SetControlAvailability()
        {
            bool enabled = true;
            if (_area == null || _area.Resets.Count < 1)
            {
                enabled = false;
            }
            txtArg0.Enabled = enabled;
            txtArg1.Enabled = enabled;
            txtArg2.Enabled = enabled;
            txtArg3.Enabled = enabled;
            txtArg4.Enabled = enabled;
            txtArg5.Enabled = enabled;
            txtArg6.Enabled = enabled;
            txtArg7.Enabled = enabled;
            btnBack.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btnFwd.Enabled = enabled;
            cbCommand.Enabled = enabled;
            lblDescription.Enabled = enabled;
        }

        public void SetActiveReset(int resetNumber)
        {
            if (resetNumber >= 0 && resetNumber < _area.Resets.Count)
            {
                resetList.SelectedIndex = resetNumber;
            }
        }

        private void txtArg0_TextChanged(object sender, EventArgs e)
        {
            UpdateResetDescriptionWithoutCommittingChanges();
        }

        private void txtArg1_TextChanged(object sender, EventArgs e)
        {
            UpdateResetDescriptionWithoutCommittingChanges();
        }

        private void txtArg2_TextChanged(object sender, EventArgs e)
        {
            UpdateResetDescriptionWithoutCommittingChanges();
        }

        private void txtArg3_TextChanged(object sender, EventArgs e)
        {
            UpdateResetDescriptionWithoutCommittingChanges();
        }

        private void txtArg4_TextChanged(object sender, EventArgs e)
        {
            UpdateResetDescriptionWithoutCommittingChanges();
        }

        private void txtArg5_TextChanged(object sender, EventArgs e)
        {
            UpdateResetDescriptionWithoutCommittingChanges();
        }

        private void txtArg6_TextChanged(object sender, EventArgs e)
        {
            UpdateResetDescriptionWithoutCommittingChanges();
        }

        private void txtArg7_TextChanged(object sender, EventArgs e)
        {
            UpdateResetDescriptionWithoutCommittingChanges();
        }

        private void btnFindValue0_Click(object sender, EventArgs e)
        {
            // Value 0 is never used.
            return;
        }

        private void btnFindValue1_Click(object sender, EventArgs e)
        {
            switch ((String)cbCommand.SelectedItem)
            {
                case "D": // Room Number
                    {
                        SelectRoom dlg = new SelectRoom(_area);
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtArg1.Text = dlg.GetIndexNumberString();
                        }
                    }
                    break;
                case "F":
                case "M": // Mob Number
                    {
                        SelectMob dlg = new SelectMob(_area);
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtArg1.Text = dlg.GetIndexNumberString();
                        }
                    }
                    break;
                case "E":
                case "G":
                case "O":
                case "P": // Object Number
                    {
                        SelectObject dlg = new SelectObject(_area);
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtArg1.Text = dlg.GetIndexNumberString();
                        }
                    }
                    break;
                default:
                    return;
            }
        }

        private void btnFindValue2_Click(object sender, EventArgs e)
        {
            switch ((String)cbCommand.SelectedItem)
            {
                case "D": // Door Direction
                    SelectDirection dlg = new SelectDirection();
                    dlg.ShowDialog();
                    if (dlg.DialogResult == DialogResult.OK)
                    {
                        txtArg2.Text = dlg.GetDirection().ToString();
                    }
                    break;
                default:
                    return;
            }
        }

        private void btnFindValue3_Click(object sender, EventArgs e)
        {
            switch ((String)cbCommand.SelectedItem)
            {
                case "F":
                case "G":
                case "M":
                case "O":
                case "R": // Room Number
                    {
                        SelectRoom dlg = new SelectRoom(_area);
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtArg3.Text = dlg.GetIndexNumberString();
                        }
                    }
                    break;
                case "E": // Equipment Slot
                    {
                        SelectEquipmentSlot dlg = new SelectEquipmentSlot();
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtArg3.Text = dlg.GetNumber().ToString();
                        }
                    }
                    break;
                case "P": // Object Number
                    {
                        SelectObject dlg = new SelectObject(_area);
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtArg3.Text = dlg.GetIndexNumberString();
                        }
                    }
                    break;
                case "D": // Door State
                    {
                        FlagEditor dlg = new FlagEditor(BitvectorFlagType.ExitFlags, 0, 0);
                        DialogResult result = dlg.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            dlg.Text = dlg.Value.ToString();
                        }
                    }
                    break;
                default:
                    return;
            }
        }

        private void btnFindValue4_Click(object sender, EventArgs e)
        {
            // Value 4 is never used.
            return;
        }

        private void btnFindValue5_Click(object sender, EventArgs e)
        {
            switch ((String)cbCommand.SelectedItem)
            {
                case "G": // Mob Number
                    {
                        SelectMob dlg = new SelectMob(_area);
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtArg5.Text = dlg.GetIndexNumberString();
                        }
                    }
                    break;
                case "P": // Room Number
                    {
                        SelectRoom dlg = new SelectRoom(_area);
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtArg5.Text = dlg.GetIndexNumberString();
                        }
                    }
                    break;
                default:
                    return;
            }
        }

        private void btnFindValue6_Click(object sender, EventArgs e)
        {
            // Value 6 is never used.
            return;
        }

        private void btnFindValue7_Click(object sender, EventArgs e)
        {
            // Value 7 is never used.
            return;
        }
    }
}