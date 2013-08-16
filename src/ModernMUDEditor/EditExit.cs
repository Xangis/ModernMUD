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
    public partial class EditExit : Form
    {
        private RoomTemplate _room = null;
        private int _direction = 0;
        private Area _area = null;
        bool _delete = false;
        private MainForm _parent = null;
        public EditExit(MainForm parent, Area area)
        {
            _parent = parent;
            InitializeComponent();
            _area = area;
        }

        /// <summary>
        /// Updates the data in the window.  We pass in a room and a direction
        /// because we may or may not change the data depending on whether we
        /// click OK or Cancel.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="direction"></param>
        public void UpdateWindowContents(RoomTemplate room, int direction)
        {
            _room = room;
            _direction = direction;
            Exit exit = _room.ExitData[direction];
            if (exit != null)
            {
                lblEditStatus.Text = "Editing " + Exit.DirectionName[direction] + " exit in room " + room.IndexNumber.ToString() + ".";
                txtDescription.Text = exit.Description;
                txtFlags.Text = exit.ExitFlags.ToString();
                txtKeyIndexNumber.Text = exit.Key.ToString();
                txtKeyword.Text = exit.Keyword;
                txtIndexNumber.Text = exit.IndexNumber.ToString();
            }
            else
            {
                lblEditStatus.Text = "Creating new " + Exit.DirectionName[direction] + " exit in room " + room.IndexNumber.ToString() + ".";
            }
        }

        /// <summary>
        /// Resets the window by telling it to refresh with the currently selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnEditFlags_Click(object sender, EventArgs e)
        {
            //int value = 0;
            //bool parsed = Int32.TryParse(txtFlags.Text, out value);
            //FlagEditor editor = new FlagEditor(Exit.ExitFlags, value);
            //DialogResult result = editor.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    txtFlags.Text = editor.Value.ToString();
            //}
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        public Exit GetExitData()
        {
            if (_delete)
            {
                return null;
            }
            Exit exit = new Exit();
            exit.Description = txtDescription.Text;
            int val;
            Int32.TryParse(txtFlags.Text, out val);
            exit.ExitFlags = (Exit.ExitFlag)val;
            int key;
            Int32.TryParse(txtKeyIndexNumber.Text, out key);
            exit.Key = key;
            exit.Keyword = txtKeyword.Text;
            int indexNumber;
            Int32.TryParse(txtIndexNumber.Text, out indexNumber);
            exit.IndexNumber = indexNumber;
            return exit;
        }

        private void txtIndexNumber_TextChanged(object sender, EventArgs e)
        {
            int indexNumber;
            Int32.TryParse(txtIndexNumber.Text, out indexNumber);
            foreach (RoomTemplate room in _area.Rooms)
            {
                if (room.IndexNumber == indexNumber)
                {
                    MainForm.BuildRTFString(room.Title, rtbTargetRoom);
                    return;
                }
            }
            rtbTargetRoom.Text = String.Empty;
        }

        private void txtKeyIndexNumber_TextChanged(object sender, EventArgs e)
        {
            int indexNumber;
            Int32.TryParse(txtKeyIndexNumber.Text, out indexNumber );
            foreach (ObjTemplate obj in _area.Objects)
            {
                if (obj.IndexNumber == indexNumber)
                {
                    MainForm.BuildRTFString(obj.ShortDescription, rtbKeyName);
                    return;
                }
            }
            rtbKeyName.Text = String.Empty;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            MainForm.BuildRTFString(txtDescription.Text, rtbDescription);
        }

        private void btnFindRoom_Click(object sender, EventArgs e)
        {
            SelectRoom dlg = new SelectRoom(_area);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtIndexNumber.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindKey_Click(object sender, EventArgs e)
        {
            SelectObject dlg = new SelectObject(_area);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtKeyIndexNumber.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _delete = true;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnNewRoom_Click(object sender, EventArgs e)
        {
            int currentRoom = _room.IndexNumber;
            int roomNumber = _parent.AddNewRoomFromExit();
            this.txtIndexNumber.Text = roomNumber.ToString();
            // Create reverse-direction exit by default.
            int dir = Exit.ReverseDirection[_direction];
            for( int i = 0; i < _area.Rooms.Count; i++ )
            {
                if (_area.Rooms[i].IndexNumber == roomNumber)
                {
                    if (_area.Rooms[i].ExitData != null && _area.Rooms[i].ExitData[dir] == null)
                    {
                        Exit exit = new Exit();
                        exit.IndexNumber = currentRoom;
                        _area.Rooms[i].ExitData[dir] = exit;
                    }
                    return;
                }
            }
        }
    }
}