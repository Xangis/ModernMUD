using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using ModernMUD;

namespace ModernMUDEditor
{
    public partial class MainForm : Form
    {
        private Area _area;
        private bool _isDirty; // Has the area been modified and needs to be saved?
        private EditAreaSettings _dlgArea;
        private EditMobs _dlgMobs;
        private EditObjects _dlgObjects;
        private EditQuests _dlgQuests;
        private EditRooms _dlgRooms;
        private EditShops _dlgShops;
        private EditResets _dlgResets;
        private EditRepopPoints _dlgRepops;
        private Dictionary<int, RoomLocation> _roomLocations = new Dictionary<int, RoomLocation>();
        private int _currentRoom = 0;
        private int _renderLevel = 0;
        private int _minRenderLevel = 0;
        private int _maxRenderLevel = 0;
        private int _xOffset = 0;
        private int _yOffset = 0;

        public MainForm()
        {
            InitializeComponent();
            this.DragEnter += new DragEventHandler(this_DragEnter);
            tabMapView.DragEnter += new DragEventHandler(this_DragEnter);
            this.DragDrop += new DragEventHandler(this_DragDrop);
            tabMapView.DragDrop += new DragEventHandler(this_DragDrop);
            _isDirty = false;
            _area = null;
            // Must be before LoadClasses since classes reference races (in the class availability list).
            try
            {
                if (!Race.LoadRaces())
                {
                    MessageBox.Show("Error loading races.  Please make sure that the race files exist.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading race files: " + ex.ToString() + "\n\nAre you sure the race files exist in ../races?");
                return;
            }
            try
            {
                if (!CharClass.LoadClasses(false))
                {
                    MessageBox.Show("Error loading classes.  Please make sure that the class files exist.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading class files: " + ex.ToString() + "\n\nAre you sure the class files exist in ../classes?");
                return;
            }
            BuildRTFString(String.Empty, txtOutputText);
            this.Resize += new EventHandler(MainForm_Resize);
        }

        void MainForm_Resize(object sender, EventArgs e)
        {
            txtInputText.Location = new System.Drawing.Point(txtInputText.Location.X, (tabControl1.Size.Height - 52));
            txtInputText.Width = tabControl1.Width - 20;
            txtOutputText.Width = tabControl1.Width - 20;
            txtOutputText.Height = tabControl1.Height - 64;
            Refresh();
        }

        /// <summary>
        /// Handles files dragged into the editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void this_DragEnter(object sender, DragEventArgs e)
        {
           if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
              e.Effect = DragDropEffects.Copy;
           else
              e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Opens an area dropped onto the editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void this_DragDrop(object sender, DragEventArgs e)
        {
            Array a = (Array)e.Data.GetData(DataFormats.FileDrop);

            if (a != null)
            {
                string filename = a.GetValue(0).ToString();
                LoadArea(filename);
            }

        }

        /// <summary>
        /// Gets the version of the zone data as described by this DLL.
        /// </summary>
        /// <returns></returns>
        public static double GetVersion()
        {
            int minVer = typeof(MainForm).Assembly.GetName().Version.Minor;
            int majVer = typeof(MainForm).Assembly.GetName().Version.Major;
            double ver = majVer + (minVer / 100.0);
            return ver;
        }

        /// <summary>
        /// Repositions the text input window and level up/down buttons when dialog is resized.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            txtInputText.Location = new System.Drawing.Point(txtInputText.Location.X, (tabControl1.Size.Height - 52));
            txtInputText.Width = tabControl1.Width - 20;
            txtOutputText.Width = tabControl1.Width - 20;
            txtOutputText.Height = tabControl1.Height - 64;
            base.OnSizeChanged(e);
        }

        private string BuildVersionString()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyCopyrightAttribute copyright =
                (AssemblyCopyrightAttribute)AssemblyCopyrightAttribute.GetCustomAttribute(
                    assembly, typeof( AssemblyCopyrightAttribute ) );
            AssemblyTitleAttribute title =
                (AssemblyTitleAttribute)AssemblyTitleAttribute.GetCustomAttribute(
                    assembly, typeof( AssemblyTitleAttribute ) );
            FileInfo info = new FileInfo( assembly.Location );
            DateTime date = info.LastWriteTime;

            // Create an area class just so we can get info about the base assembly.
            Area area = new Area();
            string baseName = area.GetType().Assembly.GetName().Name;
            string baseVersion = Area.GetVersion().ToString();
            FileInfo baseInfo = new FileInfo(area.GetType().Assembly.Location);
            DateTime baseDate = baseInfo.LastWriteTime;

            string version = title.Title +
                " version " +
                GetVersion() +
                " built on " +
                date.ToShortDateString() + 
                ".\nBased on version " +
                baseVersion +
                " of " +
                baseName +
                " built on " +
                baseDate.ToShortDateString() +
                ".\nThis application is " +
                copyright.Copyright +
                "\nWritten by Jason Champion (Xangis).\nFor the latest version, visit http://www.basternae.org.";
            return version;
        }

        /// <summary>
        /// Shows the about box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show( BuildVersionString(), "About the ModernMUD Editor" );
        }

        /// <summary>
        /// Loads a zone from disk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "xml files (*.xml)|*.xml";
            fd.InitialDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Name + Path.DirectorySeparatorChar + "area";
            DialogResult dr = fd.ShowDialog();
            if( dr == DialogResult.OK )
            {
                LoadArea(fd.FileName);
                if (_area != null && _area.EditorVersion > GetVersion())
                {
                    MessageBox.Show("This area was created with version " + _area.EditorVersion +
                        " of the ModernMUD Editor.  You are running an earlier version, " + GetVersion().ToString() +
                        ".  If you save the zone in this editor, you may lose data that is only understood by newer versions of the editor.  You have been warned.");
                }
            }
        }

        /// <summary>
        /// Loads an area file.
        /// </summary>
        /// <param name="filename"></param>
        public void LoadArea(string filename)
        {
            _area = Area.Load(filename);
            if (_area == null)
            {
                MessageBox.Show("Unable to load area " + filename + ".", "Load Error");
                return;
            }
            // Clear the output window.
            txtOutputText.Text = "Loaded new area: " + filename + ".\r\n";
            txtOutputText.ScrollToCaret();
            // Update dialogs.
            UpdateStatusBar();
            UpdateDialogs();
            _renderLevel = 0;
            UpdateLevelText();
            Refresh();
            if (_area.Rooms.Count > 0)
                _currentRoom = _area.Rooms[0].IndexNumber;
            // Build a dictionary of rooms we can use to display the map.
            SerializableDictionary<int, Location> dict = MapBuilder.BuildMapGrid(_area.Rooms);
            int len = dict.Count;
        }

        /// <summary>
        /// Saves the current zone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( _area == null )
            {
                MessageBox.Show("Nothing to save: no file loaded or created.", "Save Error");
                return;
            }
            _area.EditorVersion = GetVersion();
            _area.Save();
            txtOutputText.AppendText("Area saved as " + _area.Filename + "\r\n");
            txtOutputText.ScrollToCaret();
        }

        /// <summary>
        /// Closes the current zone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _area = null;
            UpdateStatusBar();
            UpdateDialogs();
            Refresh();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _area = new Area();
            _area.Filename = "newarea" + Area.Count + ".are.xml";
            UpdateStatusBar();
            UpdateDialogs();
            UpdateRoomMap();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            return;
        }

        private void toolStripObjects_Click(object sender, EventArgs e)
        {

        }

        public void UpdateStatusBar()
        {
            if( _area != null )
            {
                toolStripStatusLabel1.Text = Color.RemoveColorCodes(_area.Name);
                toolStripFilename.Text = "[" + _area.Filename + "]";
                toolStripRooms.Text = String.Format("Rooms: {0}", _area.Rooms.Count);
                toolStripMobs.Text = String.Format("Mobs: {0}", _area.Mobs.Count);
                toolStripObjects.Text = String.Format("Objects: {0}", _area.Objects.Count);
                toolStripShops.Text = String.Format("Shops: {0}", _area.Shops.Count);
                toolStripQuests.Text = String.Format("Quests: {0}", _area.Quests.Count);
                toolStripResets.Text = String.Format("Resets: {0}", _area.Resets.Count);
            }
            else
            {
                toolStripStatusLabel1.Text = "(No Area Loaded)";
                toolStripRooms.Text = "Rooms: 0";
                toolStripMobs.Text = "Mobs: 0";
                toolStripObjects.Text = "Objects: 0";
                toolStripShops.Text = "Shops: 0";
                toolStripQuests.Text = "Quests: 0";
                toolStripResets.Text = "Resets: 0";
            }
        }

        public void UpdateRoomMap()
        {
            tabMapView.Invalidate();
            tabMapView.Refresh();
        }

        private void UpdateDialogs()
        {
            if (_dlgArea != null)
            {
                _dlgArea.UpdateData(_area);
                _dlgArea.Close();
            }
            if (_dlgMobs != null)
            {
                _dlgMobs.UpdateData(_area);
                _dlgMobs.Close();
            }
            if (_dlgObjects != null)
            {
                _dlgObjects.UpdateData(_area);
                _dlgObjects.Close();
            }
            if (_dlgRooms != null)
            {
                _dlgRooms.UpdateData(_area);
                _dlgRooms.Close();
            }
            if (_dlgQuests != null)
            {
                _dlgQuests.UpdateData(_area);
                _dlgQuests.Close();
            }
            if (_dlgResets != null)
            {
                _dlgResets.UpdateData(_area);
                _dlgResets.Close();
            }
            if (_dlgShops != null)
            {
                _dlgShops.UpdateData(_area);
                _dlgShops.Close();
            }
            if (_dlgRepops != null)
            {
                _dlgRepops.UpdateData(_area);
                _dlgRepops.Close();
            }
        }

        private void showMobs_Click(object sender, EventArgs e)
        {
            if( _area == null )
            {
                MessageBox.Show( "Can't edit mobs without creating or loading an area first.  Try File->New or File->Open." );
                return;
            }
            if( _dlgMobs == null )
            {
                _dlgMobs = new EditMobs(this);
                _dlgMobs.UpdateData(_area);
            }
            _dlgMobs.Show();
        }

        private void showObjects_Click(object sender, EventArgs e)
        {
            if( _area == null )
            {
                MessageBox.Show("Can't edit object without creating or loading an area first.  Try File->New or File->Open.");
                return;
            }

            if (_dlgObjects == null)
            {
                _dlgObjects = new EditObjects(this);
                _dlgObjects.UpdateData(_area);
            }
            _dlgObjects.Show();
        }

        private void showRooms_Click(object sender, EventArgs e)
        {
            if( _area == null )
            {
                MessageBox.Show("Can't edit rooms without creating or loading an area first.  Try File->New or File->Open.");
                return;
            }

            if (_dlgRooms == null)
            {
                _dlgRooms = new EditRooms(this);
                _dlgRooms.UpdateData(_area);
            }
            _dlgRooms.Show();
        }

        private void showShops_Click(object sender, EventArgs e)
        {
            ShowShopDialog();
        }

        private bool ShowShopDialog()
        {
            if (_area == null)
            {
                MessageBox.Show("Can't edit shops without creating or loading an area first.  Try File->New or File->Open.");
                return false;
            }

            if (_dlgShops == null)
            {
                _dlgShops = new EditShops(this);
                _dlgShops.UpdateData(_area);
            }
            _dlgShops.Show();
            return true;
        }

        public void ShowShop(int index)
        {
            if (ShowShopDialog())
            {
                _dlgShops.NavigateTo(index);
            }
        }

        public void ShowQuest(int index)
        {
            if (ShowQuestDialog())
            {
                _dlgQuests.NavigateTo(index);
            }
        }

        private bool ShowQuestDialog()
        {
            if (_area == null)
            {
                MessageBox.Show("Can't edit quests without creating or loading an area first.  Try File->New or File->Open.");
                return false;
            }

            if (_dlgQuests == null)
            {
                _dlgQuests = new EditQuests(this);
                _dlgQuests.UpdateData(_area);
            }
            _dlgQuests.Show();
            return true;
        }

        private void showQuests_Click(object sender, EventArgs e)
        {
            ShowQuestDialog();
        }

        private void showResets_Click(object sender, EventArgs e)
        {
            if( _area == null )
            {
                MessageBox.Show("Can't edit resets without creating or loading an area first.  Try File->New or File->Open.");
                return;
            }

            if (_dlgResets == null)
            {
                _dlgResets = new EditResets(this);
                _dlgResets.UpdateData(_area);
            }
            _dlgResets.Show();
        }

        private void showAreaSettings_Click(object sender, EventArgs e)
        {
            if( _area == null )
            {
                MessageBox.Show("Can't edit area settings without creating or loading an area first.  Try File->New or File->Open.");
                return;
            }
            ShowAreaSettings();
        }

        /// <summary>
        /// Shows the area settings edit window.
        /// </summary>
        public void ShowAreaSettings()
        {
            if (_dlgArea == null)
            {
                _dlgArea = new EditAreaSettings();
                _dlgArea.UpdateData(_area);
            }
            _dlgArea.Show();
        }

        /// <summary>
        /// Populates a RichTextBox with text generated from ANSI color code strings.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="target"></param>
        public static void BuildRTFString(string text, RichTextBox target)
        {
            // Add header and build color table.
            const string rtfHeader = "{\\rtf\\ansi{\\colortbl\\red192\\green192\\blue192;\\red0\\green0\\blue0;\\red0\\green0\\blue255;\\red0\\green255\\blue255;\\red0\\green255\\blue0;\\red255\\green0\\blue255;\\red255\\green0\\blue0;\\red255\\green255\\blue0;\\red255\\green255\\blue255;\\red0\\green0\\blue128;\\red0\\green128\\blue128;\\red0\\green128\\blue0;\\red128\\green0\\blue128;\\red128\\green0\blue0;\\red128\\green128\\blue0;\\red128\\green128\\blue128;\\red192\\green192\\blue192;}\\cf0 ";
            // Replace each color one by one.
            string parsedText = text.Replace("&+l", "\\cf1 ");
            parsedText = parsedText.Replace("&+B", "\\cf2 ");
            parsedText = parsedText.Replace("&+C", "\\cf3 ");
            parsedText = parsedText.Replace("&+G", "\\cf4 ");
            parsedText = parsedText.Replace("&+M", "\\cf5 ");
            parsedText = parsedText.Replace("&+R", "\\cf6 ");
            parsedText = parsedText.Replace("&+Y", "\\cf7 ");
            parsedText = parsedText.Replace("&+W", "\\cf8 ");
            parsedText = parsedText.Replace("&+b", "\\cf9 ");
            parsedText = parsedText.Replace("&+c", "\\cf10 ");
            parsedText = parsedText.Replace("&+g", "\\cf11 ");
            parsedText = parsedText.Replace("&+m", "\\cf12 ");
            parsedText = parsedText.Replace("&+r", "\\cf13 ");
            parsedText = parsedText.Replace("&+y", "\\cf14 ");
            parsedText = parsedText.Replace("&+L", "\\cf15 ");
            parsedText = parsedText.Replace("&+w", "\\cf16 ");
            parsedText = parsedText.Replace("&n", "\\cf16 ");
            parsedText = parsedText.Replace("&N", "\\cf16 ");
            target.Rtf = rtfHeader + parsedText + "}";
        }

        private void showColorCodesToolStripMenuItem_Click( object sender, EventArgs e )
        {
            MessageBox.Show(
                "&+r Dark Red\n&+R Bright Red\n&+y Orange\n&+Y Yellow\n&+g Dark Green\n&+G Bright Green\n&+b Dark Blue\n&+B Bright Blue\n" +
                "&+c Dark Cyan\n&+C Bright Cyan\n&+m Dark Purple\n&+M Bright Purple\n&+w Grey\n&+W White\n&+l Black (not normally visible)\n&+L Dark Grey\n&n Reset Color",
                "ModernMUD Color Codes" );
        }

        private void btnEditRepopPoints_Click(object sender, EventArgs e)
        {
            if (_area == null)
            {
                MessageBox.Show("Can't edit repop points without creating or loading an area first.  Try File->New or File->Open.");
                return;
            }

            if (_dlgRepops == null)
            {
                _dlgRepops = new EditRepopPoints();
                _dlgRepops.UpdateData(_area);
            }
            _dlgRepops.Show();
        }

        private void saveAsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if( _area == null )
            {
                MessageBox.Show( "Nothing to save: no file loaded or created.", "Save Error" );
                return;
            }
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "xml files (*.xml)|*.xml";
            fd.InitialDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Name + Path.DirectorySeparatorChar + "area";
            DialogResult dr = fd.ShowDialog();
            if( dr == DialogResult.OK )
            {
                _area.EditorVersion = GetVersion();
                _area.Save(fd.FileName);
            }
            toolStripFilename.Text = "[" + _area.Filename + "]";
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "editorhelp.chm");
        }

        private void tabMapView_Paint(object sender, PaintEventArgs e)
        {
            if (_area == null || _area.Rooms.Count < 1)
                return;
            int xpos = 320;
            int ypos = 190;
            int xmin = 10;
            int ymin = 10;
            int xmax = 10;
            int ymax = 10;
            int minLevel = 0;
            int maxLevel = 0;
            int level = 0;

            System.Drawing.Pen pen = System.Drawing.Pens.Brown;
            System.Drawing.Brush secondaryBrush = System.Drawing.Brushes.Brown;
            System.Drawing.Pen greenPen = System.Drawing.Pens.DarkGreen;
            System.Drawing.Pen brownPen = System.Drawing.Pens.Brown;
            _roomLocations = new Dictionary<int, RoomLocation>();
            // Build a database of all of our room locations.
            foreach (RoomTemplate room in _area.Rooms)
            {
                if (!_roomLocations.ContainsKey(room.IndexNumber))
                {
                    _roomLocations[room.IndexNumber] = new RoomLocation(new System.Drawing.Point(xpos, ypos), level, pen, GetBrushColorFromTerrain(room.TerrainType));
                    xpos = xmax + 30;
                    ypos = ymax + 30;
                    xmax = xpos;
                    ymax = ypos;
                }

                level = _roomLocations[room.IndexNumber].Level;

                for (int i = 0; i < room.ExitData.Length; i++)
                {
                    if (room.ExitData[i] != null )
                    {
                        if (!_roomLocations.ContainsKey(room.ExitData[i].IndexNumber))
                        {
                            if (room.ExitData[i].IndexNumber < _area.LowRoomIndexNumber || room.ExitData[i].IndexNumber > _area.HighRoomIndexNumber)
                            {
                                pen = greenPen;
                            }
                            else
                            {
                                pen = brownPen;
                            }
                            secondaryBrush = GetBrushColorFromRoomIndex(room.ExitData[i].IndexNumber);


                            switch (i)
                            {
                                case (int)Exit.Direction.east:
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location +
                                        new System.Drawing.Size(20, 0), level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.Y > ymax)
                                        ymax = _roomLocations[room.ExitData[i].IndexNumber].Location.Y;
                                    break;
                                case (int)Exit.Direction.west:
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location +
                                        new System.Drawing.Size(-20, 0), level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.Y < ymin)
                                        ymin = _roomLocations[room.ExitData[i].IndexNumber].Location.Y;
                                    break;
                                case (int)Exit.Direction.north:
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location +
                                        new System.Drawing.Size(0, -20), level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.X < xmin)
                                        xmin = _roomLocations[room.ExitData[i].IndexNumber].Location.X;
                                    break;
                                case (int)Exit.Direction.south:
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location +
                                        new System.Drawing.Size(0, 20), level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.X > xmax)
                                        xmax = _roomLocations[room.ExitData[i].IndexNumber].Location.X;
                                    break;
                                case (int)Exit.Direction.northeast:
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location +
                                        new System.Drawing.Size(20, -20), level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.X > xmax)
                                        xmax = _roomLocations[room.ExitData[i].IndexNumber].Location.X;
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.Y < ymin)
                                        ymin = _roomLocations[room.ExitData[i].IndexNumber].Location.Y;
                                    break;
                                case (int)Exit.Direction.southeast:
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location +
                                        new System.Drawing.Size(20, 20), level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.X > xmax)
                                        xmax = _roomLocations[room.ExitData[i].IndexNumber].Location.X;
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.Y > ymax)
                                        ymax = _roomLocations[room.ExitData[i].IndexNumber].Location.Y;
                                    break;
                                case (int)Exit.Direction.northwest:
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location +
                                        new System.Drawing.Size(-20, -20), level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.X < xmin)
                                        xmin = _roomLocations[room.ExitData[i].IndexNumber].Location.X;
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.Y < ymin)
                                        ymin = _roomLocations[room.ExitData[i].IndexNumber].Location.Y;
                                    break;
                                case (int)Exit.Direction.southwest:
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location +
                                        new System.Drawing.Size(20, -20), level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.X > xmax)
                                        xmax = _roomLocations[room.ExitData[i].IndexNumber].Location.X;
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.Y < ymin)
                                        ymin = _roomLocations[room.ExitData[i].IndexNumber].Location.Y;
                                    break;
                                case (int)Exit.Direction.up:
                                    ++level;
                                    if (level > maxLevel) maxLevel = level;
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location,
                                        level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.X > xmax)
                                        xmax = _roomLocations[room.ExitData[i].IndexNumber].Location.X;
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.Y < ymin)
                                        ymin = _roomLocations[room.ExitData[i].IndexNumber].Location.Y;
                                    break;
                                case (int)Exit.Direction.down:
                                    --level;
                                    if (level < minLevel) minLevel = level;
                                    _roomLocations[room.ExitData[i].IndexNumber] = new RoomLocation(_roomLocations[room.IndexNumber].Location,
                                        level, pen, secondaryBrush);
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.X > xmax)
                                        xmax = _roomLocations[room.ExitData[i].IndexNumber].Location.X;
                                    if (_roomLocations[room.ExitData[i].IndexNumber].Location.Y < ymin)
                                        ymin = _roomLocations[room.ExitData[i].IndexNumber].Location.Y;
                                    break;
                            }
                        }
                        level = _roomLocations[room.IndexNumber].Level + GetLevelChangeFromExitDirection(i);
                        _roomLocations[room.IndexNumber].Exits[i] = true;
                    }
                }
            }
            _maxRenderLevel = maxLevel;
            _minRenderLevel = minLevel;
            UpdateLevelText();

            // Render all of the room locations that were found.
            foreach (KeyValuePair<int, RoomLocation> point in _roomLocations)
            {
                if (point.Value.Level != _renderLevel) continue;
                // Outer room rectangle.
                e.Graphics.DrawRectangle(point.Value.RoomColor, (point.Value.Location.X + _xOffset),
                    (point.Value.Location.Y + _yOffset), 10, 10);
                // Inner room rectangle indicating terrain type.
                e.Graphics.FillRectangle(point.Value.SecondaryColor, (point.Value.Location.X + 1 + _xOffset),
                    (point.Value.Location.Y + 1 + _yOffset), 9, 9);
                // North exit connection.
                if (point.Value.Exits[(int)Exit.Direction.north])
                    e.Graphics.DrawLine(point.Value.RoomColor, (point.Value.Location.X + 5 + _xOffset),
                        (point.Value.Location.Y + _yOffset), (point.Value.Location.X + 5 + _xOffset),
                        (point.Value.Location.Y - 10 + _yOffset));
                // South exit connection.
                if (point.Value.Exits[(int)Exit.Direction.south])
                    e.Graphics.DrawLine(point.Value.RoomColor, (point.Value.Location.X + 6 + _xOffset),
                        (point.Value.Location.Y + 10 + _yOffset), (point.Value.Location.X + 6 + _xOffset),
                        (point.Value.Location.Y + 20 + _yOffset));
                // East exit connection.
                if (point.Value.Exits[(int)Exit.Direction.east])
                    e.Graphics.DrawLine(point.Value.RoomColor, (point.Value.Location.X + 10 + _xOffset),
                        (point.Value.Location.Y + 5 + _yOffset), (point.Value.Location.X + 20 + _xOffset),
                        (point.Value.Location.Y + 5 + _yOffset));
                // West exit connection.
                if (point.Value.Exits[(int)Exit.Direction.west])
                    e.Graphics.DrawLine(point.Value.RoomColor, (point.Value.Location.X + _xOffset),
                        (point.Value.Location.Y + 6 + _yOffset), (point.Value.Location.X - 10 + _xOffset),
                        (point.Value.Location.Y + 6 + _yOffset));
                // Northeast exit connection.
                if (point.Value.Exits[(int)Exit.Direction.northeast])
                    e.Graphics.DrawLine(point.Value.RoomColor, (point.Value.Location.X + 10 + _xOffset),
                        (point.Value.Location.Y + _yOffset), (point.Value.Location.X + 20 + _xOffset),
                        (point.Value.Location.Y - 10 + _yOffset));
                // Southeast exit connection.
                if (point.Value.Exits[(int)Exit.Direction.southeast])
                    e.Graphics.DrawLine(point.Value.RoomColor, (point.Value.Location.X + 10 + _xOffset),
                        (point.Value.Location.Y + 10 + _yOffset), (point.Value.Location.X + 20 + _xOffset),
                        (point.Value.Location.Y + 20 + _yOffset));
                // Northwest exit connection.
                if (point.Value.Exits[(int)Exit.Direction.northwest])
                    e.Graphics.DrawLine(point.Value.RoomColor, (point.Value.Location.X + _xOffset),
                        (point.Value.Location.Y + _yOffset), (point.Value.Location.X - 10 + _xOffset),
                        (point.Value.Location.Y - 10 + _yOffset));
                // Southwest exit connection.
                if (point.Value.Exits[(int)Exit.Direction.southwest])
                    e.Graphics.DrawLine(point.Value.RoomColor, (point.Value.Location.X + _xOffset),
                        (point.Value.Location.Y + 10 + _yOffset), (point.Value.Location.X - 10 + _xOffset),
                        (point.Value.Location.Y + 20 + _yOffset));
            }
        }

        int GetLevelChangeFromExitDirection(int direction)
        {
            switch(direction)
            {
                default:
                    return 0;
                case (int)Exit.Direction.up:
                    return 1;
                case (int)Exit.Direction.down:
                    return -1;
            }
        }

        /// <summary>
        /// Gets the brush color that is used for the room index's terrain type.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        System.Drawing.Brush GetBrushColorFromRoomIndex(int indexNumber)
        {
            foreach (RoomTemplate room in _area.Rooms)
            {
                if (room.IndexNumber == indexNumber)
                {
                    return GetBrushColorFromTerrain(room.TerrainType);
                }
            }
            return System.Drawing.Brushes.Brown;
        }

        /// <summary>
        /// Gets the brush color that is used for the specified terrain type.
        /// </summary>
        /// <param name="terrainType"></param>
        /// <returns></returns>
        System.Drawing.Brush GetBrushColorFromTerrain(TerrainType terrainType)
        {
            switch (terrainType)
            {
                case TerrainType.underground_no_ground:
                    return System.Drawing.Brushes.DarkCyan;
                case TerrainType.air:
                case TerrainType.plane_of_air:
                    return System.Drawing.Brushes.LightCyan;
                case TerrainType.arctic:
                case TerrainType.underground_frozen:
                    return System.Drawing.Brushes.LightBlue;
                case TerrainType.underground_city:
                case TerrainType.city:
                    return System.Drawing.Brushes.LightGray;
                case TerrainType.desert:
                    return System.Drawing.Brushes.Yellow;
                case TerrainType.underground_wild:
                    return System.Drawing.Brushes.Violet;
                case TerrainType.field:
                    return System.Drawing.Brushes.GreenYellow;
                case TerrainType.forest:
                    return System.Drawing.Brushes.Green;
                case TerrainType.glacier:
                    return System.Drawing.Brushes.White;
                case TerrainType.hills:
                    return System.Drawing.Brushes.Tan;
                case TerrainType.mountain:
                    return System.Drawing.Brushes.Brown;
                case TerrainType.ocean:
                case TerrainType.underground_ocean:
                case TerrainType.unswimmable_water:
                case TerrainType.underground_unswimmable_water:
                case TerrainType.plane_of_water:
                    return System.Drawing.Brushes.DarkBlue;
                case TerrainType.road:
                    return System.Drawing.Brushes.Gray;
                case TerrainType.swamp:
                    return System.Drawing.Brushes.DarkGreen;
                case TerrainType.underground_swimmable_water:
                case TerrainType.swimmable_water:
                case TerrainType.underwater_has_ground:
                case TerrainType.underwater_no_ground:
                    return System.Drawing.Brushes.Blue;
                case TerrainType.tundra:
                    return System.Drawing.Brushes.SlateGray;
                case TerrainType.jungle:
                    return System.Drawing.Brushes.LightGreen;
                case TerrainType.lava:
                case TerrainType.plane_of_fire:
                    return System.Drawing.Brushes.Red;
                case TerrainType.inside:
                    return System.Drawing.Brushes.DarkGray;
                case TerrainType.underground_impassable:
                    return System.Drawing.Brushes.Black;
                case TerrainType.plane_of_earth:
                case TerrainType.underground_indoors:
                    return System.Drawing.Brushes.Chocolate;
                default:
                    return System.Drawing.Brushes.Brown;
            }
        }

        void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                txtInputText.Focus();
            }
        }

        /// <summary>
        /// Parses commands from the input window and executes commands if necessary.
        /// </summary>
        /// <param name="text"></param>
        private void ParseCommand(string text)
        {
            if (text.StartsWith("load", StringComparison.CurrentCultureIgnoreCase))
                LoadCommand(text);
            else if (text.StartsWith("list", StringComparison.CurrentCultureIgnoreCase))
                ListCommand(text);
            else if ("look".StartsWith(text, StringComparison.CurrentCultureIgnoreCase) ||
                text.StartsWith("l", StringComparison.CurrentCultureIgnoreCase))
                LookCommand(text);
            else if ("north".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.north);
            else if ("south".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.south);
            else if ("east".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.east);
            else if ("west".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.west);
            else if ("up".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.up);
            else if ("down".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.down);
            else if ("southeast".StartsWith(text, StringComparison.CurrentCultureIgnoreCase) ||
                "se".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.southeast);
            else if ("southwest".StartsWith(text, StringComparison.CurrentCultureIgnoreCase) ||
                "sw".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.southwest);
            else if ("northeast".StartsWith(text, StringComparison.CurrentCultureIgnoreCase) ||
                "ne".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.northeast);
            else if ("northwest".StartsWith(text, StringComparison.CurrentCultureIgnoreCase) ||
                "nw".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                MoveCommand(Exit.Direction.northwest);
            else if (text.StartsWith("goto", StringComparison.CurrentCultureIgnoreCase))
                GotoCommand(text);
            else if (text.StartsWith("createedit", StringComparison.CurrentCultureIgnoreCase))
                CreateeditCommand(text);
            else if (text.StartsWith("edit", StringComparison.CurrentCultureIgnoreCase))
                EditCommand(text);
            else if ("start".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                StartCommand();
            else if ("purge".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                PurgeCommand();
            else if ("exits".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                ExitsCommand();
            else if (text.StartsWith("equip", StringComparison.CurrentCultureIgnoreCase))
                EquipCommand(text);
            else if (text.StartsWith("give", StringComparison.CurrentCultureIgnoreCase))
                GiveCommand(text);
            else if ("save".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                saveToolStripMenuItem_Click(null, null);
            else if ("help".StartsWith(text, StringComparison.CurrentCultureIgnoreCase) ||
                     "commands".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
            {
                txtOutputText.AppendText("look\tnorth\tsouth\teast\twest\tup\tdown\tnorthwest\tsouthwest\tnortheast\tsoutheast\tnw\r\nsw\tne\tse\tgoto\tlist\tcreateedit\tedit\tload\tstart\tpurge\texits\tsave\thelp\tcommands\r\nversion\r\n");
                txtOutputText.ScrollToCaret();
            }
            else if ("version".StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
            {
                txtOutputText.AppendText(BuildVersionString() + "\r\n");
                txtOutputText.ScrollToCaret();
            }
            else
            {
                txtOutputText.AppendText("Unrecognized command.\r\n");
                txtOutputText.ScrollToCaret();
            }
        }

        private void ExitsCommand()
        {
            RoomTemplate room = FindRoom(_currentRoom);
            if (room == null)
            {
                txtOutputText.AppendText("Not currently in a room.\r\n");
                txtOutputText.ScrollToCaret();
            }

            string exits = String.Empty;
            for (int i = 0; i < Limits.MAX_DIRECTION; i++)
            {
                if (room.ExitData[i] != null)
                {
                    RoomTemplate target = FindRoom(room.ExitData[i].IndexNumber);
                    if (target == null)
                    {
                        exits += ((Exit.Direction)i).ToString() + ": (Target Room Not Found)" + "\r\n";
                    }
                    else
                    {
                        exits += ((Exit.Direction)i).ToString() + ": " + target.Title + "\r\n";
                    }
                }
            }
            if (exits.Length < 1)
            {
                txtOutputText.AppendText("No exits found.\r\n");
            }
            else
            {
                txtOutputText.AppendText("Exits:\r\n" + exits + "\r\n");
            }
            txtOutputText.ScrollToCaret();
        }

        private void LoadCommand(string text)
        {
            if (_area == null)
            {
                txtOutputText.AppendText("No area loaded yet.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            String[] pieces = text.Split(' ');
            if (pieces.Length < 2 || String.IsNullOrEmpty(pieces[1]))
            {
                txtOutputText.AppendText("What do you want to load?  You can load a mob or object.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            
            string list = String.Empty;
            if ("mob".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                if( pieces.Length < 3 )
                {
                    txtOutputText.AppendText("Which mob number do you want to load?\r\n");
                    txtOutputText.ScrollToCaret();
                }
                int value = 0;
                if (Int32.TryParse(pieces[2], out value))
                {
                    Reset reset = new Reset();
                    reset.Arg1 = value;
                    reset.Arg3 = _currentRoom;
                    reset.Command = 'M';
                    _area.Resets.Add(reset);
                    if (_dlgResets != null)
                    {
                        _dlgResets.UpdateResetList();
                        _dlgResets.SetActiveReset(_area.Resets.Count - 1);
                    }
                    txtOutputText.AppendText("Done.\r\n");
                    txtOutputText.ScrollToCaret();
                    return;
                }
            }
            else if ("object".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                if( pieces.Length < 3 )
                {
                    txtOutputText.AppendText("Which object number do you want to load?\r\n");
                    txtOutputText.ScrollToCaret();
                }
                int value = 0;
                if( Int32.TryParse(pieces[2], out value ))
                {
                    Reset reset = new Reset();
                    reset.Arg1 = value;
                    reset.Arg3 = _currentRoom;
                    reset.Command = 'O';
                    _area.Resets.Add(reset);
                    if (_dlgResets != null)
                    {
                        _dlgResets.UpdateResetList();
                        _dlgResets.SetActiveReset(_area.Resets.Count - 1);
                    }
                    txtOutputText.AppendText("Done.\r\n");
                    txtOutputText.ScrollToCaret();
                    return;
                }
            }
            else
            {
                txtOutputText.AppendText("What do you want to load?  You can load mobs or objects.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            txtOutputText.AppendText(list + "\r\n");
            txtOutputText.ScrollToCaret();
        }

        private void PurgeCommand()
        {
            if (_area == null)
            {
                txtOutputText.AppendText("No area loaded yet.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            for (int i = _area.Resets.Count - 1; i >= 0; i--)
            {
                switch (_area.Resets[i].Command)
                {
                    case 'M':
                    case 'F':
                    case 'G':
                    case 'O': // Clear mob, follow, object, and give resets in the current room.
                        if( _area.Resets[i].Arg3 == _currentRoom )
                        {
                            _area.Resets.RemoveAt(i);
                        }
                        break;
                    case 'E': // Clear equip object on mob resets in the current room.
                        if (_area.Resets[i].Arg4 == _currentRoom)
                        {
                            _area.Resets.RemoveAt(i);
                        }
                        break;
                    case 'P': // Clear put object in object resets in the current room.
                        if (_area.Resets[i].Arg5 == _currentRoom)
                        {
                            _area.Resets.RemoveAt(i);
                        }
                        break;
                }
            }
            txtOutputText.AppendText("Room purged.\r\n");
            txtOutputText.ScrollToCaret();
        }

        private void CreateeditCommand(string text)
        {
            if (_area == null)
            {
                txtOutputText.AppendText("No area loaded yet.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            String[] pieces = text.Split(' ');
            if (pieces.Length < 2 || String.IsNullOrEmpty(pieces[1]))
            {
                txtOutputText.AppendText("What do you want to create?  You can createedit a mob, room, or object.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            string list = String.Empty;
            if ("room".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                if (_dlgRooms == null)
                {
                    _dlgRooms = new EditRooms(this);
                    _dlgRooms.UpdateData(_area);
                }
                if (pieces.Length > 2 && !String.IsNullOrEmpty(pieces[2]))
                {
                    Exit.Direction direction = Exit.DoorLookup(pieces[2]);
                    if( direction == Exit.Direction.invalid )
                    {
                        txtOutputText.AppendText("That's not a valid direction.  Try again.\r\n");
                        return;
                    }
                    _dlgRooms.AddNewRoom(_currentRoom, direction);
                }
                else
                {
                    _dlgRooms.AddNewRoom(true);
                }
                _dlgRooms.Show();
            }
            else if ("mob".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                if (_dlgMobs == null)
                {
                    _dlgMobs = new EditMobs(this);
                    _dlgMobs.UpdateData(_area);
                }
                _dlgMobs.AddNewMob();
                _dlgMobs.Show();
            }
            else if ("object".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                if (_dlgObjects == null)
                {
                    _dlgObjects = new EditObjects(this);
                    _dlgObjects.UpdateData(_area);
                }
                _dlgObjects.AddNewObject();
                _dlgObjects.Show();
            }
            else
            {
                txtOutputText.AppendText("What do you want to create?  You can createedit mobs, rooms, or objects.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            txtOutputText.AppendText(list + "\r\n");
            txtOutputText.ScrollToCaret();
        }


        private void CommandsCommand()
        {
            txtOutputText.AppendText("Available Commands:\r\ncommands    start       goto        look        north       south\r\neast        west        southeast   southwest   northeast   northwest\r\nup          down        give        equip       \r\n");
            txtOutputText.ScrollToCaret();
        }

        private void StartCommand()
        {
            if (_area == null)
            {
                txtOutputText.AppendText("No zone has been loaded.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            if (_area.Rooms.Count < 1)
            {
                txtOutputText.AppendText("There are no rooms in this zone.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            _currentRoom = _area.Rooms[0].IndexNumber;
            LookCommand(String.Empty);
        }

        private void LookCommand(string text)
        {
            if (_currentRoom == 0)
            {
                txtOutputText.AppendText("You are not in a room.  Either no rooms exist, or you are not in the zone yet.  Type 'start' to enter the zone.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            String[] str = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if(str.Length < 2 || "room".StartsWith(str[1], StringComparison.CurrentCultureIgnoreCase))
            {
                RoomTemplate room = FindRoom(_currentRoom);
                if( room != null)
                {
                    txtOutputText.AppendText(room.ToString());
                    // Show Mobs
                    foreach (Reset reset in _area.Resets)
                    {
                        if ((reset.Command == 'M' || reset.Command == 'F') && reset.Arg3 == _currentRoom)
                        {
                            MobTemplate mob = FindMob(reset.Arg1);
                            if (mob != null)
                            {
                                txtOutputText.AppendText(mob.ToString() + "\r\n");
                            }
                        }
                    }
                    // Show Objects
                    foreach (Reset reset in _area.Resets)
                    {
                        if (reset.Command == 'O' && reset.Arg3 == _currentRoom)
                        {
                            ObjTemplate obj = FindObj(reset.Arg1);
                            if (obj != null)
                            {
                                txtOutputText.AppendText(obj.FullDescription + "\r\n");
                            }
                        }
                    }
                    txtOutputText.AppendText("\r\n");
                    txtOutputText.ScrollToCaret();
                    return;
                }
            }
            // Not the room - check for mobs and objects.
            foreach( Reset reset in _area.Resets )
            {
                // Mob and Follow reset.
                if( reset.Command == 'M' || reset.Command == 'F')
                {
                    if( reset.Arg3 != _currentRoom )
                    {
                        continue;
                    }
                    MobTemplate mob = FindMob(reset.Arg1);
                    String[] args = mob.PlayerName.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    foreach( String strng in args )
                    {
                        if( str[1].StartsWith(strng, StringComparison.CurrentCultureIgnoreCase))
                        {
                            txtOutputText.AppendText(mob.FullDescription + "\r\n");
                            foreach (Reset rst in _area.Resets)
                            {
                                if (rst.Command == 'E' && rst.Arg5 == mob.IndexNumber && rst.Arg4 == _currentRoom)
                                {
                                    ObjTemplate obj = FindObj(rst.Arg1);
                                    if (obj != null)
                                    {
                                        txtOutputText.AppendText(((ObjTemplate.WearLocation)rst.Arg3).ToString() + ":  " + obj.ShortDescription + "\r\n");
                                    }
                                }
                                else if (rst.Command == 'G' && rst.Arg5 == mob.IndexNumber && rst.Arg3 == _currentRoom)
                                {
                                    ObjTemplate obj = FindObj(rst.Arg1);
                                    if (obj != null)
                                    {
                                        txtOutputText.AppendText("inventory:  " + obj.ShortDescription + "\r\n");
                                    }
                                }
                            }
                            txtOutputText.ScrollToCaret(); 
                            return;
                        }
                    }
                }
                // Object reset.
                else if( reset.Command == 'O' )
                {
                    if( reset.Arg3 != _currentRoom )
                    {
                        continue;
                    }
                    ObjTemplate obj = FindObj(reset.Arg1);
                    String[] args = obj.Name.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    foreach( String strng in args )
                    {
                        if (str[1].StartsWith(strng, StringComparison.CurrentCultureIgnoreCase))
                        {
                            txtOutputText.AppendText(obj.FullDescription + "\r\n");
                            txtOutputText.ScrollToCaret();
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to retrieve a room from the zone by its index number.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        private RoomTemplate FindRoom(int indexNumber)
        {
            if (_area == null)
            {
                txtOutputText.AppendText("No zone has been loaded.\r\n");
                txtOutputText.ScrollToCaret();
                return null;
            }

            if (_area.Rooms.Count < 1)
            {
                txtOutputText.AppendText("There are no rooms in this zone.\r\n");
                txtOutputText.ScrollToCaret();
                return null;
            }

            for (int i = 0; i < _area.Rooms.Count; i++)
            {
                if (_area.Rooms[i].IndexNumber == indexNumber)
                {
                    return _area.Rooms[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Attempts to retrieve a mob from the zone by its index number.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        private MobTemplate FindMob(int indexNumber)
        {
            if (_area == null)
            {
                txtOutputText.AppendText("No zone has been loaded.\r\n");
                txtOutputText.ScrollToCaret();
                return null;
            }

            if (_area.Mobs.Count < 1)
            {
                txtOutputText.AppendText("There are no mobiles in this zone.\r\n");
                txtOutputText.ScrollToCaret();
                return null;
            }

            for (int i = 0; i < _area.Mobs.Count; i++)
            {
                if (_area.Mobs[i].IndexNumber == indexNumber)
                {
                    return _area.Mobs[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Attempts to retrieve an object from the zone by its index number.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        private ObjTemplate FindObj(int indexNumber)
        {
            if (_area == null)
            {
                txtOutputText.AppendText("No zone has been loaded.\r\n");
                txtOutputText.ScrollToCaret();
                return null;
            }

            if (_area.Objects.Count < 1)
            {
                txtOutputText.AppendText("There are no objects in this zone.\r\n");
                txtOutputText.ScrollToCaret();
                return null;
            }

            for (int i = 0; i < _area.Objects.Count; i++)
            {
                if (_area.Objects[i].IndexNumber == indexNumber)
                {
                    return _area.Objects[i];
                }
            }
            return null;
        }

        private void GotoCommand(string text)
        {
            String[] pieces = text.Split(' ');
            if (pieces.Length < 2)
            {
                txtOutputText.AppendText("What room ID do you want to go to?\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            int roomId = 0;
            if( Int32.TryParse(pieces[1], out roomId ))
            {
                RoomTemplate room = FindRoom(roomId);
                if( room != null )
                {
                    _currentRoom = room.IndexNumber;
                    LookCommand(String.Empty);
                }
                else
                {
                    txtOutputText.AppendText("No such room.\r\n");
                    txtOutputText.ScrollToCaret();
                }
            }
        }

        private void EditCommand(string text)
        {
            if (_area == null)
            {
                txtOutputText.AppendText("No area loaded yet.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            String[] pieces = text.Split(' ');
            if (pieces.Length < 2 || String.IsNullOrEmpty(pieces[1]))
            {
                txtOutputText.AppendText("What do you want to edit?  You can edit a mob, room, or object.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            int itemNumber = 0;
            // Get the item number to edit.
            if ((pieces.Length < 3 || String.IsNullOrEmpty(pieces[2])) && !"room".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                txtOutputText.AppendText("What item number do you want to edit?\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            else if (pieces.Length < 3)
            {
                // An 'edit room' without an argument number edits the current room.
                itemNumber = _currentRoom;
            }
            else if (!Int32.TryParse(pieces[2], out itemNumber))
            {
                txtOutputText.AppendText("What item number do you want to edit?\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            
            string list = String.Empty;
            if ("room".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                NavigateToEditRoom(itemNumber);
            }
            else if ("mob".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                NavigateToEditMob(itemNumber);
            }
            else if ("object".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                NavigateToEditObject(itemNumber);
            }
            else
            {
                txtOutputText.AppendText("What do you want to edit?  You can list mobs, rooms, or objects.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
        }

        private void EquipCommand(string text)
        {
            string[] args = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length < 3)
            {
                txtOutputText.AppendText("Equip who with what?\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            // Check if object is valid.
            int value = 0;
            ObjTemplate obj;
            if (!Int32.TryParse(args[2], out value) || (obj = FindObj(value)) == null)
            {
                txtOutputText.AppendText("That's not a valid object number.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            foreach (Reset reset in _area.Resets)
            {
                if ((reset.Command == 'M' || reset.Command == 'F') && reset.Arg3 == _currentRoom)
                {
                    MobTemplate mob = FindMob(reset.Arg1);
                    if (mob != null && mob.PlayerName.Contains(args[1]))
                    {
                        Reset newReset = new Reset();
                        newReset.Command = 'E';
                        newReset.Arg1 = obj.IndexNumber;
                        newReset.Arg4 = _currentRoom;
                        // Automatically determine equipment slot.
                        // TODO: Allow use of second eq slot for wrist, ear, neck, etc. if first is filled already.
                        newReset.Arg3 = 0;
                        if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_WIELD.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.hand_one;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_WRIST.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.wrist_left;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_WAIST.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.waist;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_TAIL.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.tail;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_SHIELD.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.hand_two;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_QUIVER.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.quiver;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_ONBACK.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.on_back;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_NOSE.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.nose;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_NECK.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.neck_one;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_LEGS.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.legs;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_HORSE_BODY.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.horse_body;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_HORNS.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.horns;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_HOLD.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.hand_two;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_HEAD.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.head;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_HANDS.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.hands;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_FINGER.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.finger_left;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_FEET.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.feet;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_FACE.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.face;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_EYES.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.eyes;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_EAR.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.ear_left;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_BODY.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.body;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_BADGE.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.badge;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_ATTACH_BELT.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.belt_attach_one;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_ARMS.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.arms;
                        }
                        else if ((obj.WearFlags[0] & ObjTemplate.WEARABLE_ABOUT.Vector) != 0)
                        {
                            newReset.Arg3 = (int)ObjTemplate.WearLocation.about_body;
                        }
                        newReset.Arg5 = mob.IndexNumber;
                        _area.Resets.Add(newReset);
                        txtOutputText.AppendText("Object " + obj.ShortDescription + " equipped on mob " + mob.ShortDescription + " on " + ((ObjTemplate.WearLocation)newReset.Arg3).ToString() + " in room " + _currentRoom + ".\r\n");
                        txtOutputText.ScrollToCaret();
                        return;
                    }
                }
            }
            txtOutputText.AppendText("That mob is not loaded in this room.\r\n");
            txtOutputText.ScrollToCaret();
            return;
        }

        private void GiveCommand(string text)
        {
            string[] args = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length < 3)
            {
                txtOutputText.AppendText("Give who what?\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            // Check if object is valid.
            int value = 0;
            ObjTemplate obj;
            if (!Int32.TryParse(args[2], out value) || (obj = FindObj(value)) == null )
            {
                txtOutputText.AppendText("That's not a valid object number.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            foreach (Reset reset in _area.Resets)
            {
                if ((reset.Command == 'M' || reset.Command == 'F') && reset.Arg3 == _currentRoom)
                {
                    MobTemplate mob = FindMob(reset.Arg1);
                    if (mob != null && mob.PlayerName.Contains(args[1]))
                    {
                        Reset newReset = new Reset();
                        newReset.Command = 'G';
                        newReset.Arg1 = obj.IndexNumber;
                        newReset.Arg3 = _currentRoom;
                        newReset.Arg5 = mob.IndexNumber;
                        _area.Resets.Add(newReset);
                        txtOutputText.AppendText("Object " + obj.ShortDescription + " given to mob " + mob.ShortDescription + " in room " + _currentRoom + ".\r\n");
                        txtOutputText.ScrollToCaret();
                        return;
                    }
                }
            }
            txtOutputText.AppendText("That mob is not loaded in this room.\r\n");
            txtOutputText.ScrollToCaret();
            return;
        }

        private void ListCommand(string text)
        {
            if( _area == null )
            {
                txtOutputText.AppendText("No area loaded yet.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            String[] pieces = text.Split(' ');
            if (pieces.Length < 2 || String.IsNullOrEmpty(pieces[1]))
            {
                txtOutputText.AppendText("What do you want to list?  You can list mobs, rooms, or objects.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }
            string list = String.Empty;
            if( "rooms".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                list = "List of rooms:\r\n";
                foreach( RoomTemplate room in _area.Rooms )
                {
                    list += "[" + room.IndexNumber + "] " + room.Title + "\r\n";
                }
            }
            else if( "mobs".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                list = "List of mobs:\r\n";
                foreach( MobTemplate mob in _area.Mobs )
                {
                    list += "[" + mob.IndexNumber + "] " + mob.ShortDescription + "\r\n";
                }
            }
            else if( "objects".StartsWith(pieces[1], StringComparison.CurrentCultureIgnoreCase))
            {
                list = "List of obejcts:\r\n";
                foreach( ObjTemplate obj in _area.Objects )
                {
                    list += "[" + obj.IndexNumber + "] " + obj.ShortDescription + "\r\n";
                }
            }
            else
            {
                txtOutputText.AppendText("What do you want to list?  You can list mobs, rooms, or objects.\r\n");
                txtOutputText.ScrollToCaret();
                return;
            }

            txtOutputText.AppendText(list + "\r\n");
            txtOutputText.ScrollToCaret();
        }

        private void MoveCommand(Exit.Direction direction)
        {
            RoomTemplate room = FindRoom(_currentRoom);
            if( room != null)
            {
                if (room.ExitData[(int)direction] != null)
                {
                    _currentRoom = room.ExitData[(int)direction].IndexNumber;
                    if (_dlgRooms != null)
                    {
                        _dlgRooms.SetActiveRoom(room.ExitData[(int)direction].IndexNumber);
                    }
                    
                }
                else
                {
                    txtOutputText.AppendText("No exit in that direction.\r\n");
                    txtOutputText.ScrollToCaret();
                    return;
                }
            }
            LookCommand(String.Empty);
        }

        private void txtInputText_TextChanged(object sender, EventArgs e)
        {
            string text = String.Empty;
            if (txtInputText.Text.Contains("\r") || txtInputText.Text.Contains("\n"))
            {
                text = txtInputText.Text;
                text = text.Replace("\r", "");
                text = text.Replace("\n", "");
                ParseCommand(text);
                txtInputText.Text = String.Empty;
            }
            // No echo.
            //txtOutputText.AppendText(text);
        }

        private void knownIssuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Known issues with the ModernMUD editor:\n\n" +
                "* Walkthrough mode is only partially functional and does not include color.\n"
                );
        }

        void tabMapView_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            foreach (KeyValuePair<int, RoomLocation> loc in _roomLocations)
            {
                if (loc.Value.Contains(e.X - _xOffset, e.Y - _yOffset))
                {
                    // This should theoretically not be possible.
                    if (_area == null)
                    {
                        MessageBox.Show("Can't edit rooms without creating or loading an area first.  Try File->New or File->Open.");
                        return;
                    }

                    NavigateToEditRoom(loc.Key);
                    return;
                }
            }
        }

        /// <summary>
        /// Opens the room edit window and navigates to the specified room vnum.
        /// </summary>
        /// <param name="key"></param>
        public void NavigateToEditRoom(int key)
        {
            if (_dlgRooms == null)
            {
                _dlgRooms = new EditRooms(this);
                _dlgRooms.UpdateData(_area);
            }
            _dlgRooms.SetActiveRoom(key);
            _dlgRooms.Show();
        }

        /// <summary>
        /// Creates a new room in the room dialog, navigates to it, then returns the number of the new room.
        /// Used primarily for creating new rooms from the exit dialog.
        /// </summary>
        /// <returns></returns>
        public int AddNewRoomFromExit()
        {
            if (_area == null)
            {
                MessageBox.Show("Cannot create a new room without loading or creating an area first.");
                return 0;
            }

            if (_dlgRooms == null)
            {
                _dlgRooms = new EditRooms(this);
                _dlgRooms.UpdateData(_area);
            }
            _dlgRooms.Show();
            return _dlgRooms.AddNewRoom(false);
        }

        /// <summary>
        /// Opens the object edit window and navigates the the selected object number.
        /// </summary>
        /// <param name="key"></param>
        public void NavigateToEditObject(int key)
        {
            if (_dlgObjects == null)
            {
                _dlgObjects = new EditObjects(this);
                _dlgObjects.UpdateData(_area);
            }
            _dlgObjects.SetActiveObject(key);
            _dlgObjects.Show();
        }

        /// <summary>
        /// Opens the mob edit window and navigates to the selected mob number.
        /// </summary>
        /// <param name="key"></param>
        public void NavigateToEditMob(int key)
        {
            if (_dlgMobs == null)
            {
                _dlgMobs = new EditMobs(this);
                _dlgMobs.UpdateData(_area);
            }
            _dlgMobs.SetActiveMob(key);
            _dlgMobs.Show();
        }

        private void UpdateLevelText()
        {
            lblLevel.Text = _renderLevel.ToString() + " (" + _minRenderLevel + " to " + _maxRenderLevel + ")";
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (_renderLevel < _maxRenderLevel)
            {
                ++_renderLevel;
                tabControl1.Refresh();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (_renderLevel > _minRenderLevel)
            {
                --_renderLevel;
                tabControl1.Refresh();
            }
        }

        private void renumberZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenumberDlg dlg = new RenumberDlg();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Renumber(dlg.GetNumber());
            }
        }

        /// <summary>
        /// Renumbers the zone starting with a particular number.
        /// </summary>
        /// <param name="number"></param>
        private void Renumber(int number)
        {
            int offset = number - _area.LowIndexNumber;

            // Convert area settings.
            if( _area.BarracksRoom > 0 )
                _area.BarracksRoom += offset;
            if (_area.JudgeRoom > 0)
                _area.JudgeRoom += offset;
            if (_area.JailRoom > 0)
                _area.JailRoom += offset;
            if (_area.DefenderTemplateNumber > 0)
                _area.DefenderTemplateNumber += offset;
            if (_area.Recall > 0)
                _area.Recall += offset;
            // Convert repop points.
            foreach (RepopulationPoint point in _area.Repops)
            {
                point.RoomIndexNumber += offset;
            }
            int i;
            // Convert rooms.
            for (i = 0; i < _area.Rooms.Count; i++)
            {
                _area.Rooms[i].IndexNumber += offset;
                for (int j = 0; j < Limits.MAX_DIRECTION; j++)
                {
                    if (_area.Rooms[i].ExitData[j] != null)
                    {
                        _area.Rooms[i].ExitData[j].IndexNumber += offset;
                        if (_area.Rooms[i].ExitData[j].Key > 0)
                        {
                            _area.Rooms[i].ExitData[j].Key += offset;
                        }
                    }
                }
            }
            // Convert mobs.
            for (i = 0; i < _area.Mobs.Count; i++)
            {
                _area.Mobs[i].IndexNumber += offset;
            }
            // Convert objects.
            for (i = 0; i < _area.Objects.Count; i++)
            {
                _area.Objects[i].IndexNumber += offset;
            }
            // Convert shops.
            for (i = 0; i < _area.Shops.Count; i++)
            {
                _area.Shops[i].Keeper += offset;
                for (int j = 0; j < _area.Shops[i].ItemsForSale.Count; j++)
                {
                    _area.Shops[i].ItemsForSale[j] += offset;
                }
            }
            // Convert resets.
            for (i = 0; i < _area.Resets.Count; i++)
            {
                switch (_area.Resets[i].Command)
                { 
                    default:
                        MessageBox.Show("Unrecognized reset command, cannot renumber.");
                        break;
                    case 'M':
                    case 'F':
                        _area.Resets[i].Arg1 += offset;
                        _area.Resets[i].Arg3 += offset;
                        break;
                    case 'E':
                        _area.Resets[i].Arg1 += offset;
                        _area.Resets[i].Arg4 += offset;
                        _area.Resets[i].Arg5 += offset;
                        break;
                    case 'O':
                        _area.Resets[i].Arg1 += offset;
                        _area.Resets[i].Arg3 += offset;
                        break;
                    case 'G':
                    case 'P':
                        _area.Resets[i].Arg1 += offset;
                        _area.Resets[i].Arg3 += offset;
                        _area.Resets[i].Arg5 += offset;
                        break;
                    case 'D':
                        _area.Resets[i].Arg1 += offset;
                        break;
                }
                // ?
            }
            // Convert quests.
            for (i = 0; i < _area.Quests.Count; i++)
            {
                _area.Quests[i].IndexNumber += offset;
                foreach (QuestData data in _area.Quests[i].Quests)
                {
                    foreach( QuestItem give in data.Give )
                    {
                        if (give.Type == QuestItem.QuestType.item)
                        {
                            give.Value += offset;
                        }
                    }
                    foreach( QuestItem receive in data.Receive )
                    {
                        if (receive.Type == QuestItem.QuestType.item)
                        {
                            receive.Value += offset;
                        }
                    }
                }
            }
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            _yOffset -= 100;
            lblYOffset.Text = _yOffset.ToString();
            Refresh();
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            _yOffset += 100;
            lblYOffset.Text = _yOffset.ToString();
            Refresh();
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            _xOffset -= 100;
            lblXOffset.Text = _xOffset.ToString();
            Refresh();
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            _xOffset += 100;
            lblXOffset.Text = _xOffset.ToString();
            Refresh();
        }

        private void checkZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_area == null)
            {
                MessageBox.Show("Can't check the area without creating or loading an area first.  Try File->New or File->Open.");
                return;
            }

            List<string> errors = new List<string>();

            if( String.IsNullOrEmpty(_area.Name) || _area.Name == "New Area" )
            {
                errors.Add("Area name is not set.");
            }

            if (String.IsNullOrEmpty(_area.Author) || _area.Author == "None")
            {
                errors.Add("Area author name is not set.");
            }

            if (String.IsNullOrEmpty(_area.Builders) || _area.Builders == "None")
            {
                errors.Add("Area builder name(s) are not set.");
            }

            foreach (MobTemplate mob in _area.Mobs)
            {
                if (String.IsNullOrEmpty(mob.ShortDescription) || mob.ShortDescription == "(no short description)" )
                {
                    errors.Add("Mob " + mob.IndexNumber + " has no description (name).");
                }
                if (String.IsNullOrEmpty(mob.PlayerName) || mob.PlayerName == "none" )
                {
                    errors.Add("Mob " + mob.IndexNumber + " has no keywords.");
                }
                if (String.IsNullOrEmpty(mob.Description) || mob.Description == "(no description)" )
                {
                    errors.Add("Mob " + mob.IndexNumber + " has no short (in room) description.");
                }
                if (String.IsNullOrEmpty(mob.FullDescription) || mob.FullDescription == "(no long description)" )
                {
                    errors.Add("Mob " + mob.IndexNumber + " has no long description.");
                }
            }

            foreach (RoomTemplate room in _area.Rooms)
            {
                if (String.IsNullOrEmpty(room.Description) || room.Description == "(no description)")
                {
                    errors.Add("Room " + room.IndexNumber + " has no description.");
                }
                if (String.IsNullOrEmpty(room.Title) || room.Title == "(no room title)" )
                {
                    errors.Add("Room " + room.IndexNumber + " has no title.");
                }
            }

            foreach (ObjTemplate obj in _area.Objects)
            {
                if (String.IsNullOrEmpty(obj.FullDescription) || obj.FullDescription == "(no description)")
                {
                    errors.Add("Object " + obj.IndexNumber + " has no description.");
                }
                if (String.IsNullOrEmpty(obj.Name) || obj.Name == "no name")
                {
                    errors.Add("Object " + obj.IndexNumber + " has no name.");
                }
                if (String.IsNullOrEmpty(obj.ShortDescription) || obj.ShortDescription == "(no short description)")
                {
                    errors.Add("Object " + obj.IndexNumber + " has no short description (short name).");
                }
            }

            ErrorWindow errorDlg = new ErrorWindow(this, errors);
            errorDlg.Show();
        }
    }
}