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
    public partial class EditRepopPoints : Form
    {
        Area _area = null;

        /// <summary>
        /// Sets the current area and updates the window contents.
        /// </summary>
        /// <param name="area"></param>
        public void UpdateData(ModernMUD.Area area)
        {
            _area = area;
            lstRepops.Items.Clear();
            if (_area != null)
            {
                foreach (RepopulationPoint repop in _area.Repops)
                {
                    lstRepops.Items.Add(repop);
                }
            }
        }

        public EditRepopPoints()
        {
            InitializeComponent();
            foreach( CharClass cl in CharClass.ClassList )
            {
                cbClass.Items.Add(cl.Name);   
            }
            foreach (Race race in Race.RaceList)
            {
                cbRace.Items.Add(race.Name);
            }
            this.FormClosing += new FormClosingEventHandler(EditRepopPoints_FormClosing);
        }

        private void EditRepopPoints_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void lstRepops_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( lstRepops.SelectedIndex != -1 )
            {
                RepopulationPoint repop = lstRepops.Items[lstRepops.SelectedIndex] as RepopulationPoint;
                if (repop != null)
                {
                    UpdateWindowContents(repop);
                }
            }
        }

        private void UpdateWindowContents(RepopulationPoint repop)
        {
            txtIndexNumber.Text = repop.RoomIndexNumber.ToString();
            cbRace.SelectedIndex = repop.RaceNumber;
            cbClass.SelectedIndex = repop.ClassNumber;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            _area.Repops.Clear();
            foreach (object obj in lstRepops.Items)
            {
                RepopulationPoint repop = obj as RepopulationPoint;
                if (repop != null)
                {
                    _area.Repops.Add(repop);
                }
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RepopulationPoint repop = new RepopulationPoint();
            if( cbClass.SelectedIndex != -1 )
            {
                repop.ClassString = cbClass.Items[cbClass.SelectedIndex] as string;
                repop.CharacterClass = CharClass.ClassList[cbClass.SelectedIndex];
            }
            if (cbRace.SelectedIndex != -1)
            {
                repop.RaceString = cbRace.Items[cbRace.SelectedIndex] as string;
                repop.Race = Race.RaceList[cbRace.SelectedIndex];
            }
            if (txtIndexNumber.Text != String.Empty)
            {
                int indexNumber = 0;
                if (Int32.TryParse(txtIndexNumber.Text, out indexNumber))
                {
                    repop.RoomIndexNumber = indexNumber;
                }
            }
            lstRepops.Items.Add(repop);
            lstRepops.SelectedIndex = lstRepops.Items.Count - 1;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = lstRepops.SelectedIndex;
            if (index != -1)
            {
                lstRepops.Items.RemoveAt(index);
                if (lstRepops.Items.Count > index)
                {
                    lstRepops.SelectedIndex = index;
                }
                else if (lstRepops.Items.Count > 0)
                {
                    lstRepops.SelectedIndex = lstRepops.Items.Count - 1;
                }
                else
                {
                    txtIndexNumber.Text = String.Empty;
                    cbRace.SelectedIndex = -1;
                    cbClass.SelectedIndex = -1;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SelectRoom dlg = new SelectRoom(_area);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtIndexNumber.Text = dlg.GetIndexNumberString();
            }
        }

        private void txtIndexNumber_TextChanged(object sender, EventArgs e)
        {
            int indexNumber;
            Int32.TryParse(txtIndexNumber.Text, out indexNumber);
            int index = lstRepops.SelectedIndex;

            // Update data
            if ( index != -1)
            {
                RepopulationPoint repop = lstRepops.Items[lstRepops.SelectedIndex] as RepopulationPoint;
                if (repop != null)
                {
                    repop.RoomIndexNumber = indexNumber;
                }
                lstRepops.Items[ lstRepops.SelectedIndex ] = repop;
            }
            
            // Update display if it matches an existing room.
            foreach (RoomTemplate room in _area.Rooms)
            {
                if (room.IndexNumber == indexNumber)
                {
                    MainForm.BuildRTFString(room.Title, rtbRoom);
                    return;
                }
            }
            rtbRoom.Text = String.Empty;
        }

        private void cbRace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRace.SelectedIndex != -1 && lstRepops.SelectedIndex != -1)
            {
                RepopulationPoint repop = lstRepops.Items[lstRepops.SelectedIndex] as RepopulationPoint;
                if (repop != null)
                {
                    string raceName = cbRace.Items[cbRace.SelectedIndex] as string;
                    repop.RaceString = raceName;
                    repop.Race = Race.RaceList[cbRace.SelectedIndex];
                    lstRepops.Items[ lstRepops.SelectedIndex ] = repop;
                }
            }
        }

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbClass.SelectedIndex != -1 && lstRepops.SelectedIndex != -1)
            {
                RepopulationPoint repop = lstRepops.Items[lstRepops.SelectedIndex] as RepopulationPoint;
                if (repop != null)
                {
                    string className = cbClass.Items[cbClass.SelectedIndex] as string;
                    repop.ClassString = className;
                    repop.CharacterClass = CharClass.ClassList[cbClass.SelectedIndex];
                    lstRepops.Items[ lstRepops.SelectedIndex ] = repop;
                }
            }
        }
    }
}