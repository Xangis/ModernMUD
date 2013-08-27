using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ModernMUD;

namespace MapGenerator
{
    /// <summary>
    /// Main Map Generator program window.
    /// </summary>
    public partial class MainWindow : Form
    {
        Bitmap _bitmap;
        List<MiniRoomTemplate> _roomTemplates;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow()
        {
            _roomTemplates = new List<MiniRoomTemplate>();
            InitializeComponent();
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".bmp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap bmp = new Bitmap(dialog.FileName);
                    _bitmap = new Bitmap(dialog.FileName);
                }
                catch
                {
                    MessageBox.Show("Failed to load " + dialog.FileName + ".  This program can only load valid bitmaps.");
                }
            }
            else
            {
                return;
            }
            pictureBox1.Image = _bitmap;
        }

        private void generateRoomMappingTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_bitmap == null)
                return;

            List<Color> colors = new List<Color>();
            Color color;
            for (int y = 0; y < _bitmap.Height; y++)
            {
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    color = _bitmap.GetPixel(x, y);
                    if (!colors.Contains(color))
                    {
                        colors.Add(color);
                    }
                }
            }
            _roomTemplates.Clear();
            MiniRoomTemplate temp;
            foreach (Color col in colors)
            {
                temp = new MiniRoomTemplate();
                temp.Title = "No Title";
                temp.Description = String.Empty;
                temp.ImageColor = col;
                temp.Terrain = TerrainType.field;
                _roomTemplates.Add(temp);
            }
            BindData();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                DataGridView dgv = sender as DataGridView;
                MiniRoomTemplate data = dgv.Rows[e.RowIndex].DataBoundItem as MiniRoomTemplate;

                e.CellStyle.BackColor = data.ImageColor;
            }

        }

        private void saveRoomDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<MiniRoomTemplate> temps = (List<MiniRoomTemplate>)dataGridView1.DataSource;
                    FileStream stream = new FileStream(dialog.FileName, FileMode.Create);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<MiniRoomTemplate>));
                    serializer.Serialize(stream, temps);
                }
                catch
                {
                    // Nevermind
                }
            }
        }

        private void openRoomDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = new FileStream(dialog.FileName, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(List<MiniRoomTemplate>));
                _roomTemplates = (List<MiniRoomTemplate>)serializer.Deserialize(stream);
                BindData();
            }
        }

        private void BindData()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _roomTemplates;
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Title";
            column.Name = "Room Title";
            dataGridView1.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Description";
            column.Name = "Room Description";
            column.Width = 210;
            dataGridView1.Columns.Add(column);
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(TerrainType));
            combo.DataPropertyName = "Terrain";
            combo.Name = "TerrainType";
            dataGridView1.Columns.Add(combo);
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ImageColor";
            column.Name = "Image Color";
            dataGridView1.Columns.Add(column);
        }

        /// <summary>
        /// Generates a zone file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "xml files (*.xml)|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int vnum = Int32.Parse(txtStartVnum.Text);
                Area area = new Area();
                area.AreaFlags[Area.AREA_WORLDMAP.Group] = Area.AREA_WORLDMAP.Vector;
                area.Width = _bitmap.Width;
                area.Height = _bitmap.Height;
                area.Author = txtAuthor.Text;
                area.Builders = txtAuthor.Text;
                area.Name = txtZoneName.Text;
                for (int y = 0; y < _bitmap.Height; y++)
                {
                    for (int x = 0; x < _bitmap.Width; x++)
                    {
                        Color col = _bitmap.GetPixel(x, y);
                        MiniRoomTemplate temp = GetTemplate(col);
                        RoomTemplate room = new RoomTemplate();
                        room.Title = temp.Title;
                        room.Description = temp.Description;
                        room.TerrainType = temp.Terrain;
                        room.IndexNumber = vnum + x + (y * _bitmap.Width);
                        // Only add exits and add the room to the zone if the room is not impassable.
                        //
                        // TODO: Don't create exits that point to impassable rooms.
                        Color pixel;
                        MiniRoomTemplate target;
                        if (room.TerrainType != TerrainType.underground_impassable || (x == 0 && y == 0))
                        {
                            if (x > 0)
                            {
                                pixel = _bitmap.GetPixel(x-1, y);
                                target = GetTemplate(pixel);
                                if (target.Terrain != TerrainType.underground_impassable)
                                {
                                    Exit exit = new Exit();
                                    exit.IndexNumber = vnum + x + (y * _bitmap.Width) - 1;
                                    room.ExitData[(int)Exit.Direction.west] = exit;
                                }
                            }
                            if (x < (_bitmap.Width - 1))
                            {
                                pixel = _bitmap.GetPixel(x+1, y);
                                target = GetTemplate(pixel);
                                if (target.Terrain != TerrainType.underground_impassable)
                                {
                                    Exit exit = new Exit();
                                    exit.IndexNumber = vnum + x + (y * _bitmap.Width) + 1;
                                    room.ExitData[(int)Exit.Direction.east] = exit;
                                }
                            }
                            if (y > 0)
                            {
                                pixel = _bitmap.GetPixel(x, y-1);
                                target = GetTemplate(pixel);
                                if (target.Terrain != TerrainType.underground_impassable)
                                {
                                    Exit exit = new Exit();
                                    exit.IndexNumber = vnum + x + ((y - 1) * _bitmap.Width);
                                    room.ExitData[(int)Exit.Direction.north] = exit;
                                }
                            }
                            if( y < (_bitmap.Height - 1 ))
                            {
                                pixel = _bitmap.GetPixel(x, y+1);
                                target = GetTemplate(pixel);
                                if (target.Terrain != TerrainType.underground_impassable)
                                {
                                    Exit exit = new Exit();
                                    exit.IndexNumber = vnum + x + ((y + 1) * _bitmap.Width);
                                    room.ExitData[(int)Exit.Direction.south] = exit;
                                }
                            }
                            area.Rooms.Add(room);
                        }
                    }
                }
                area.Save(dialog.FileName);
            }
        }

        MiniRoomTemplate GetTemplate(Color color)
        {
            foreach (MiniRoomTemplate tmp in _roomTemplates)
            {
                if (tmp.ImageColor == color)
                    return tmp;
            }
            return null;
        }
    }
}
