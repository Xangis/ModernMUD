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
    public partial class EditRooms : Form
    {
        private Area _area = null;
        private MainForm _parent = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public EditRooms(MainForm parent)
        {
            InitializeComponent();
            for (int count = 0; count < RoomTemplate.TERRAIN_MAX; count++)
            {
                cbTerrain.Items.Add(((TerrainType)count).ToString());
            }
            for (int count = 0; count < Limits.MAX_DIRECTION; count++)
            {
                cbCurrentDir.Items.Add(((Exit.Direction)count).ToString());
            }
            String[] names = Enum.GetNames(typeof(SecondaryTerrainType));
            foreach( string name in names )
            {
                cbMapSector.Items.Add(name);
            }
            this.FormClosing += new FormClosingEventHandler( EditRooms_FormClosing );
            _parent = parent;
            SetControlAvailability();
        }

        public void UpdateData(Area area)
        {
            roomList.Items.Clear();
            _area = area;
            if (area != null)
            {
                UpdateRoomList();
            }
            SetControlAvailability();
        }

        private void UpdateRoomList()
        {
            int index = roomList.SelectedIndex;
            bool empty = (roomList.Items.Count == 0);

            roomList.Items.Clear();
            foreach (RoomTemplate room in _area.Rooms)
            {
                roomList.Items.Add(ModernMUD.Color.RemoveColorCodes(room.Title));
            }
            if (roomList.Items.Count > 0)
            {
                roomList.SelectedIndex = 0;
                UpdateWindowContents(_area.Rooms[roomList.SelectedIndex]); 
            }
            else
            {
                roomList.SelectedIndex = index;
            }
        }

        public void SetActiveRoom(int indexNumber)
        {
            for(int i = 0; i < _area.Rooms.Count; i++ )
            {
                if (_area.Rooms[i].IndexNumber == indexNumber)
                {
                    roomList.SelectedIndex = i;
                    UpdateWindowContents(_area.Rooms[i]);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if( roomList.Items.Count > 0 && roomList.SelectedIndex > 0)
            {
                ApplyWindowContents();
                --roomList.SelectedIndex;
                UpdateWindowContents(_area.Rooms[roomList.SelectedIndex]);
            }
        }

        private void UpdateWindowContents(RoomTemplate room)
        {
            txtName.Text = room.Title;
            txtIndexNumber.Text = room.IndexNumber.ToString();
            txtDescription.Text = room.Description;
            txtFlags.Text = room.BaseRoomFlags[0].ToString();
            txtFlags2.Text = room.BaseRoomFlags[1].ToString();
            cbCurrentDir.SelectedIndex = room.CurrentDirection;
            cbTerrain.SelectedItem = room.TerrainType.ToString();
            UpdateExitButtons(room);
            lstExtraDesc.Items.Clear();
            foreach (ExtendedDescription desc in room.ExtraDescriptions)
            {
                lstExtraDesc.Items.Add(desc);
            }
            txtCurrent.Text = room.Current.ToString();
            txtFallChance.Text = room.FallChance.ToString();
            SecondaryTerrainType mapSector = (SecondaryTerrainType)room.WorldmapTerrainType;
            cbMapSector.Text = mapSector.ToString();
        }

        private void UpdateExitButtons(RoomTemplate room)
        {
            if (room.ExitData[(int)Exit.Direction.north] == null)
            {
                btnn.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btnn.ForeColor = System.Drawing.Color.Black;
            }
            if (room.ExitData[(int)Exit.Direction.northwest] == null)
            {
                btnnw.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btnnw.ForeColor = System.Drawing.Color.Black;
            }
            if (room.ExitData[(int)Exit.Direction.northeast] == null)
            {
                btnne.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btnne.ForeColor = System.Drawing.Color.Black;
            }
            if (room.ExitData[(int)Exit.Direction.south] == null)
            {
                btns.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btns.ForeColor = System.Drawing.Color.Black;
            }
            if (room.ExitData[(int)Exit.Direction.southeast] == null)
            {
                btnse.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btnse.ForeColor = System.Drawing.Color.Black;
            }
            if (room.ExitData[(int)Exit.Direction.southwest] == null)
            {
                btnsw.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btnsw.ForeColor = System.Drawing.Color.Black;
            }
            if (room.ExitData[(int)Exit.Direction.east] == null)
            {
                btne.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btne.ForeColor = System.Drawing.Color.Black;
            }
            if (room.ExitData[(int)Exit.Direction.west] == null)
            {
                btnw.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btnw.ForeColor = System.Drawing.Color.Black;
            }
            if (room.ExitData[(int)Exit.Direction.up] == null)
            {
                btnup.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btnup.ForeColor = System.Drawing.Color.Black;
            }
            if (room.ExitData[(int)Exit.Direction.down] == null)
            {
                btndn.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                btndn.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void btnFwd_Click(object sender, EventArgs e)
        {
            if ((roomList.SelectedIndex + 1 ) < roomList.Items.Count )
            {
                ApplyWindowContents();
                ++roomList.SelectedIndex;
                UpdateWindowContents(_area.Rooms[roomList.SelectedIndex]);
            }
        }

        public void AddNewRoom(int originalRoom, Exit.Direction direction)
        {
            btnNew_Click(null, null);
            RoomTemplate newRoom = _area.Rooms[(_area.Rooms.Count - 1)];
            RoomTemplate oldRoom = null;
            for (int i = 0; i < _area.Rooms.Count; i++ )
            {
                if (_area.Rooms[i].IndexNumber == originalRoom)
                {
                    oldRoom = _area.Rooms[i];
                    break;
                }
            }
            if (oldRoom != null)
            {
                if (oldRoom.ExitData[(int)direction] == null)
                {
                    oldRoom.ExitData[(int)direction] = new Exit();
                }
                oldRoom.ExitData[(int)direction].IndexNumber = newRoom.IndexNumber;
                if (newRoom.ExitData[(int)Exit.ReverseDirection(direction)] == null)
                {
                    newRoom.ExitData[(int)Exit.ReverseDirection(direction)] = new Exit();
                }
                newRoom.ExitData[(int)Exit.ReverseDirection(direction)].IndexNumber = oldRoom.IndexNumber;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AddNewRoom(true);
        }

        /// <summary>
        /// Saves current window contents, creates a new room, navigates to it, and returns the number.
        /// This can be called from the edit window itself or from elsewhere, such as creating rooms
        /// from within exit editing.  Navigating to the created room is optional, and inadvisable
        /// when creating rooms from exits.
        /// </summary>
        /// <returns></returns>
        public int AddNewRoom(bool navigate)
        {
            // We need to store current index so we can return to it later if need be.
            int selectedIndex = roomList.SelectedIndex;
            ApplyWindowContents();
            RoomTemplate room = new RoomTemplate();
            if (_area.HighRoomIndexNumber >= 0)
            {
                room.IndexNumber = _area.HighRoomIndexNumber + 1;
            }
            _area.Rooms.Add(room);
            _area.RebuildIndexes();
            UpdateRoomList();
            SetControlAvailability();
            // Navigate either to the new room or the original room we were editing.
            if (navigate)
            {
                roomList.SelectedIndex = roomList.Items.Count - 1;
                UpdateWindowContents(room);
            }
            else
            {
                roomList.SelectedIndex = selectedIndex;
            }
            _parent.UpdateStatusBar();
            _parent.UpdateRoomMap();
            return room.IndexNumber;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = roomList.SelectedIndex;
            if (index == -1) return;
            if (index < roomList.Items.Count)
            {
                _area.Rooms.RemoveAt(index);
                roomList.Items.RemoveAt(index);
                SetControlAvailability();
            }
            if (index > 0)
            {
                --index;
            }
            if (index >= 0 && index < roomList.Items.Count)
            {
                roomList.SelectedIndex = index;
                UpdateWindowContents(_area.Rooms[roomList.SelectedIndex]);
            }
        }

        private void btnnw_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.northwest);
        }

        private void btnn_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.north);
        }

        private void btnne_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.northeast);
        }

        private void btnw_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.west);
        }

        private void btne_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.east);
        }

        private void btnsw_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.southwest);
        }

        private void btns_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.south);
        }

        private void btnse_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.southeast);
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.up);
        }

        private void btndn_Click(object sender, EventArgs e)
        {
            ShowExitDlg(Exit.Direction.down);
        }

        private void ShowExitDlg(Exit.Direction direction)
        {
            if( roomList.SelectedIndex != -1 )
            {
                EditExit exitdlg = new EditExit(_parent, _area);
                exitdlg.UpdateWindowContents( _area.Rooms[ roomList.SelectedIndex ], direction );
                DialogResult result = exitdlg.ShowDialog();
                if( result == DialogResult.OK )
                {
                    Exit exitData = exitdlg.GetExitData();
                    _area.Rooms[roomList.SelectedIndex].ExitData[(int)direction] = exitData;
                    UpdateExitButtons(_area.Rooms[roomList.SelectedIndex]);
                    this._parent.UpdateRoomMap();
                }
            }
            else
            {
                MessageBox.Show( "You can't edit exits without first selecting or creating a room." );
            }
        }

        private void roomList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateWindowContents(_area.Rooms[roomList.SelectedIndex]);
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
            if( roomList.SelectedIndex != -1 )
            {
                if (!String.IsNullOrEmpty(txtName.Text) && (string)roomList.Items[roomList.SelectedIndex] !=
                    ModernMUD.Color.RemoveColorCodes(txtName.Text))
                {
                    roomList.SelectedIndexChanged -= roomList_SelectedIndexChanged;
                    roomList.Items[roomList.SelectedIndex] = ModernMUD.Color.RemoveColorCodes(txtName.Text);
                    roomList.SelectedIndexChanged += roomList_SelectedIndexChanged;
                }
                int indexNumber = 0;
                if (Int32.TryParse(txtIndexNumber.Text, out indexNumber))
                {
                    _area.Rooms[roomList.SelectedIndex].IndexNumber = indexNumber;
                }
                _area.Rooms[ roomList.SelectedIndex ].Title = txtName.Text;
                _area.Rooms[ roomList.SelectedIndex ].Description = txtDescription.Text;
                _area.Rooms[ roomList.SelectedIndex ].CurrentDirection = cbCurrentDir.SelectedIndex;
                _area.Rooms[ roomList.SelectedIndex ].TerrainType = (TerrainType)Enum.Parse(typeof(TerrainType), (String)(cbTerrain.SelectedItem));
                // First we parse the map terrain type as an enum, then we convert it to an int for storage.
                _area.Rooms[ roomList.SelectedIndex ].WorldmapTerrainType = (int)((SecondaryTerrainType)Enum.Parse(typeof(SecondaryTerrainType), (String)(cbMapSector.SelectedItem)));
                int value;
                if (Int32.TryParse( txtFlags.Text, out value ))
                {
                    _area.Rooms[ roomList.SelectedIndex ].BaseRoomFlags[ 0 ] = value;
                }
                if( Int32.TryParse( txtFlags2.Text, out value ))
                {
                    _area.Rooms[ roomList.SelectedIndex ].BaseRoomFlags[ 1 ] = value;
                }
                _area.Rooms[ roomList.SelectedIndex ].CurrentRoomFlags[ 0 ] = _area.Rooms[ roomList.SelectedIndex ].BaseRoomFlags[ 0 ];
                _area.Rooms[ roomList.SelectedIndex ].CurrentRoomFlags[ 1 ] = _area.Rooms[ roomList.SelectedIndex ].BaseRoomFlags[ 1 ];
                if( Int32.TryParse( txtFallChance.Text, out value ))
                {
                    _area.Rooms[ roomList.SelectedIndex ].FallChance = value;
                }
                if (Int32.TryParse(cbMapSector.Text, out value))
                {
                    _area.Rooms[roomList.SelectedIndex].WorldmapTerrainType = value;
                }
                if (Int32.TryParse(txtCurrent.Text, out value))
                {
                    _area.Rooms[roomList.SelectedIndex].Current = value;
                }
                _area.Rooms[ roomList.SelectedIndex ].ExtraDescriptions.Clear();
                foreach( object obj in lstExtraDesc.Items )
                {
                    if (obj is ExtendedDescription)
                    {
                        _area.Rooms[roomList.SelectedIndex].ExtraDescriptions.Add((ExtendedDescription)obj);
                    }
                }
                // Refresh the room map because we may have changed terrain type or other relevant info.
                this._parent.UpdateRoomMap();
            }
        }

        private void txtName_TextChanged( object sender, EventArgs e )
        {
            MainForm.BuildRTFString( txtName.Text, rtbName );
        }

        private void txtDescription_TextChanged( object sender, EventArgs e )
        {
            MainForm.BuildRTFString( txtDescription.Text, rtbDescription );
        }

        private void EditRooms_FormClosing( object sender, FormClosingEventArgs e )
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void btnEditFlags1_Click( object sender, EventArgs e )
        {
            if( roomList.SelectedIndex == -1 )
            {
                MessageBox.Show( "You can't edit room flags without first selecting or creating a room." );
                return;
            }
            int value = 0;
            bool parsed = Int32.TryParse( txtFlags.Text, out value );
            FlagEditor editor = new FlagEditor( BitvectorFlagType.RoomFlags, value, 0 );
            DialogResult result = editor.ShowDialog();
            if( result == DialogResult.OK )
            {
                txtFlags.Text = editor.Value.ToString();
            }
        }

        private void btnEditFlags2_Click( object sender, EventArgs e )
        {
            if( roomList.SelectedIndex == -1 )
            {
                MessageBox.Show( "You can't edit room flags without first selecting or creating a room." );
                return;
            }
            int value = 0;
            bool parsed = Int32.TryParse( txtFlags2.Text, out value );
            FlagEditor editor = new FlagEditor( BitvectorFlagType.RoomFlags, value, 1 );
            DialogResult result = editor.ShowDialog();
            if( result == DialogResult.OK )
            {
                txtFlags2.Text = editor.Value.ToString();
            }
        }

        private void btnEditExtraDescr_Click(object sender, EventArgs e)
        {
            int index = lstExtraDesc.SelectedIndex;
            if (index != -1)
            {
                ExtendedDescription desc = lstExtraDesc.SelectedItem as ExtendedDescription;
                EditExtendedDescription dialog = new EditExtendedDescription(desc);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    lstExtraDesc.Items.RemoveAt(index);
                    lstExtraDesc.Items.Insert(index,dialog.GetExtendedDescription());
                }
            }

        }

        private void btnAddExtraDesc_Click(object sender, EventArgs e)
        {
            EditExtendedDescription dialog = new EditExtendedDescription(new ExtendedDescription());
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                lstExtraDesc.Items.Add(dialog.GetExtendedDescription());
            }
        }

        private void btnRemoveExtraDesc_Click(object sender, EventArgs e)
        {
            if (lstExtraDesc.SelectedIndex != -1)
            {
                lstExtraDesc.Items.RemoveAt(lstExtraDesc.SelectedIndex);
            }
        }

        private void SetControlAvailability()
        {
            bool enabled = true;
            if (_area == null || _area.Rooms.Count < 1)
            {
                enabled = false;
            }
            txtCurrent.Enabled = enabled;
            txtDescription.Enabled = enabled;
            txtFallChance.Enabled = enabled;
            txtFlags.Enabled = enabled;
            txtFlags2.Enabled = enabled;
            txtIndexNumber.Enabled = enabled;
            cbMapSector.Enabled = enabled;
            txtName.Enabled = enabled;
            cbCurrentDir.Enabled = enabled;
            cbTerrain.Enabled = enabled;
            btnAddExtraDesc.Enabled = enabled;
            btnBack.Enabled = enabled;
            btnFwd.Enabled = enabled;
            btnEditExtraDescr.Enabled = enabled;
            btnRemoveExtraDesc.Enabled = enabled;
            btnEditFlags1.Enabled = enabled;
            btnEditFlags2.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btndn.Enabled = enabled;
            btnup.Enabled = enabled;
            btnn.Enabled = enabled;
            btns.Enabled = enabled;
            btne.Enabled = enabled;
            btnw.Enabled = enabled;
            btnne.Enabled = enabled;
            btnnw.Enabled = enabled;
            btnse.Enabled = enabled;
            btnsw.Enabled = enabled;
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            if (roomList.SelectedIndex == -1 || roomList.SelectedIndex > (roomList.Items.Count - 1))
            {
                MessageBox.Show("Cannot clone an object without a valid object selected.");
                return;
            }
            ApplyWindowContents();
            RoomTemplate room = new RoomTemplate(_area.Rooms[roomList.SelectedIndex]);
            if (_area.HighObjIndexNumber >= 0)
            {
                room.IndexNumber = _area.HighRoomIndexNumber + 1;
            }
            _area.Rooms.Add(room);
            _area.RebuildIndexes();
            UpdateRoomList();
            SetControlAvailability();
            roomList.SelectedIndex = roomList.Items.Count - 1;
            UpdateWindowContents(room);
            _parent.UpdateStatusBar();
        }
    }
}