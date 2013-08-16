using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;

namespace WPFMudClient
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        int[,] _foregroundTiles = new int[9,9];
        int[,] _backgroundTiles = new int[9,9];

        public Map()
        {
            InitializeComponent();
            Uri uri = new Uri("pack://application:,,,/B_32x32.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(uri);           
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void ParseZoneText(string text)
        {
            // TODO: Properly parse ANSI and use it in text display.
            lblZone.Content = SocketConnection.RemoveANSICodes(text);
        }

        public void ParseRoomText(string text)
        {
            // TODO: Properly parse ANSI and use it in text display.
            lblRoom.Content = SocketConnection.RemoveANSICodes(text);
        }

        public void ParseExitText(string text)
        {
            lblExits.Content = SocketConnection.RemoveANSICodes(text);
        }

        public Uri GetForegroundImageURIFromNumber(int number)
        {
            switch (number)
            {
                    // We can't include the Basternae map tiles because they're public domain.

                    // TODO: Make the tiles configurable in a file.

                    /*
                case 1:
                    return new Uri("MapTiles/ash_cloud.png", UriKind.RelativeOrAbsolute);
                case 2:
                    return new Uri("MapTiles/ash_tree.png", UriKind.RelativeOrAbsolute);
                case 3:
                    return new Uri("MapTiles/bridge_ew.png", UriKind.RelativeOrAbsolute);
                case 4:
                    return new Uri("MapTiles/bushes.png", UriKind.RelativeOrAbsolute);
                case 5:
                    return new Uri("MapTiles/cactus.png", UriKind.RelativeOrAbsolute);
                case 6:
                    return new Uri("MapTiles/castle.png", UriKind.RelativeOrAbsolute);
                case 7:
                    return new Uri("MapTiles/cave_entrance.png", UriKind.RelativeOrAbsolute);
                case 8:
                    return new Uri("MapTiles/crater.png", UriKind.RelativeOrAbsolute);
                case 9:
                    return new Uri("MapTiles/dragon_shadow.png", UriKind.RelativeOrAbsolute);
                case 10:
                    return new Uri("MapTiles/dust_cloud.png", UriKind.RelativeOrAbsolute);
                case 11:
                    return new Uri("MapTiles/enslaver_city.png", UriKind.RelativeOrAbsolute);
                case 12:
                    return new Uri("MapTiles/evil_city.png", UriKind.RelativeOrAbsolute);
                case 13:
                    return new Uri("MapTiles/fog.png", UriKind.RelativeOrAbsolute);
                case 14:
                    return new Uri("MapTiles/fourlegged_shadow.png", UriKind.RelativeOrAbsolute);
                case 15:
                    return new Uri("MapTiles/good_city.png", UriKind.RelativeOrAbsolute);
                case 16:
                    return new Uri("MapTiles/green_grass.png", UriKind.RelativeOrAbsolute);
                case 17:
                    return new Uri("MapTiles/hills_brown.png", UriKind.RelativeOrAbsolute);
                case 18:
                    return new Uri("MapTiles/hills_brown_cave.png", UriKind.RelativeOrAbsolute);
                case 19:
                    return new Uri("MapTiles/hills_green.png", UriKind.RelativeOrAbsolute);
                case 20:
                    return new Uri("MapTiles/hills_green_cave.png", UriKind.RelativeOrAbsolute);
                case 21:
                    return new Uri("MapTiles/hills_icysnow.png", UriKind.RelativeOrAbsolute);
                case 22:
                    return new Uri("MapTiles/hills_icysnow_cave.png", UriKind.RelativeOrAbsolute);
                case 23:
                    return new Uri("MapTiles/humanoid_shadow_large.png", UriKind.RelativeOrAbsolute);
                case 24:
                    return new Uri("MapTiles/humanoid_shadow_large_w.png", UriKind.RelativeOrAbsolute);
                case 25:
                    return new Uri("MapTiles/humanoid_shadow_medium.png", UriKind.RelativeOrAbsolute);
                case 26:
                    return new Uri("MapTiles/humanoid_shadow_medium_w.png", UriKind.RelativeOrAbsolute);
                case 27:
                    return new Uri("MapTiles/humanoid_shadow_small.png", UriKind.RelativeOrAbsolute);
                case 28:
                    return new Uri("MapTiles/humanoid_shadow_small_w.png", UriKind.RelativeOrAbsolute);
                case 29:
                    return new Uri("MapTiles/humanoid_statue.png", UriKind.RelativeOrAbsolute);
                case 30:
                    return new Uri("MapTiles/island.png", UriKind.RelativeOrAbsolute);
                case 31:
                    return new Uri("MapTiles/jungle_tree.png", UriKind.RelativeOrAbsolute);
                case 32:
                    return new Uri("MapTiles/ladder_down.png", UriKind.RelativeOrAbsolute);
                case 33:
                    return new Uri("MapTiles/ladder_up.png", UriKind.RelativeOrAbsolute);
                case 34:
                    return new Uri("MapTiles/library.png", UriKind.RelativeOrAbsolute);
                case 35:
                    return new Uri("MapTiles/mansion.png", UriKind.RelativeOrAbsolute);
                case 36:
                    return new Uri("MapTiles/maple_tree.png", UriKind.RelativeOrAbsolute);
                case 37:
                    return new Uri("MapTiles/mountain.png", UriKind.RelativeOrAbsolute);
                case 38:
                    return new Uri("MapTiles/mountain_cave.png", UriKind.RelativeOrAbsolute);
                case 39:
                    return new Uri("MapTiles/mountain_snow.png", UriKind.RelativeOrAbsolute);
                case 40:
                    return new Uri("MapTiles/mountain_snow_cave.png", UriKind.RelativeOrAbsolute);
                case 41:
                    return new Uri("MapTiles/neutral_city.png", UriKind.RelativeOrAbsolute);
                case 42:
                    return new Uri("MapTiles/oak_tree.png", UriKind.RelativeOrAbsolute);
                case 43:
                    return new Uri("MapTiles/obelisk.png", UriKind.RelativeOrAbsolute);
                case 44:
                    return new Uri("MapTiles/palm_tree.png", UriKind.RelativeOrAbsolute);
                case 45:
                    return new Uri("MapTiles/pier_ns.png", UriKind.RelativeOrAbsolute);
                case 46:
                    return new Uri("MapTiles/pine_tree.png", UriKind.RelativeOrAbsolute);
                case 47:
                    return new Uri("MapTiles/pit.png", UriKind.RelativeOrAbsolute);
                case 48:
                    return new Uri("MapTiles/poison_cloud.png", UriKind.RelativeOrAbsolute);
                case 49:
                    return new Uri("MapTiles/pool.png", UriKind.RelativeOrAbsolute);
                case 50:
                    return new Uri("MapTiles/primitive_hut.png", UriKind.RelativeOrAbsolute);
                case 51:
                    return new Uri("MapTiles/primitive_village.png", UriKind.RelativeOrAbsolute);
                case 52:
                    return new Uri("MapTiles/pyramid.png", UriKind.RelativeOrAbsolute);
                case 53: 
                    return new Uri( "MapTiles/road_ew.png", UriKind.RelativeOrAbsolute );
                case 54: 
                    return new Uri( "MapTiles/road_ns.png", UriKind.RelativeOrAbsolute );
                case 55: 
                    return new Uri( "MapTiles/road_4way.png", UriKind.RelativeOrAbsolute );
                case 56: 
                    return new Uri( "MapTiles/road_corner_ne.png", UriKind.RelativeOrAbsolute );
                case 57: 
                    return new Uri( "MapTiles/road_corner_nw.png", UriKind.RelativeOrAbsolute );
                case 58: 
                    return new Uri( "MapTiles/road_corner_se.png", UriKind.RelativeOrAbsolute );
                case 59: 
                    return new Uri( "MapTiles/road_corner_sw.png", UriKind.RelativeOrAbsolute );
                case 60: 
                    return new Uri( "MapTiles/road_tshape_e.png", UriKind.RelativeOrAbsolute );
                case 61: 
                    return new Uri( "MapTiles/road_tshape_n.png", UriKind.RelativeOrAbsolute );
                case 62: 
                    return new Uri( "MapTiles/road_tshape_s.png", UriKind.RelativeOrAbsolute );
                case 63: 
                    return new Uri( "MapTiles/road_tshape_w.png", UriKind.RelativeOrAbsolute );
                case 64:
                    return new Uri("MapTiles/skeleton_fourlegged.png", UriKind.RelativeOrAbsolute);
                case 65:
                    return new Uri("MapTiles/skeleton_human.png", UriKind.RelativeOrAbsolute);
                case 66:
                    return new Uri("MapTiles/skull.png", UriKind.RelativeOrAbsolute);
                case 67:
                    return new Uri("MapTiles/stalactite.png", UriKind.RelativeOrAbsolute);
                case 68:
                    return new Uri("MapTiles/stalactite_icy.png", UriKind.RelativeOrAbsolute);
                case 69:
                    return new Uri("MapTiles/stalagmite.png", UriKind.RelativeOrAbsolute);
                case 70:
                    return new Uri("MapTiles/stalagmite_icy.png", UriKind.RelativeOrAbsolute);
                case 71:
                    return new Uri("MapTiles/stones_circle.png", UriKind.RelativeOrAbsolute);
                case 72:
                    return new Uri("MapTiles/stones_grey.png", UriKind.RelativeOrAbsolute);
                case 73:
                    return new Uri("MapTiles/stones_mossy.png", UriKind.RelativeOrAbsolute);
                case 74:
                    return new Uri("MapTiles/stones_sandy.png", UriKind.RelativeOrAbsolute);
                case 75:
                    return new Uri("MapTiles/swamptree.png", UriKind.RelativeOrAbsolute);
                case 76:
                    return new Uri("MapTiles/temple.png", UriKind.RelativeOrAbsolute);
                case 77:
                    return new Uri("MapTiles/tombstone.png", UriKind.RelativeOrAbsolute);
                case 78:
                    return new Uri("MapTiles/tower.png", UriKind.RelativeOrAbsolute);
                case 79:
                    return new Uri("MapTiles/ud_mushroom_large.png", UriKind.RelativeOrAbsolute);
                case 80:
                    return new Uri("MapTiles/ud_mushrooms_small.png", UriKind.RelativeOrAbsolute);
                case 81:
                    return new Uri("MapTiles/volcano.png", UriKind.RelativeOrAbsolute);
                case 82:
                    return new Uri("MapTiles/well.png", UriKind.RelativeOrAbsolute);
                case 83:
                    return new Uri("MapTiles/whirlpool.png", UriKind.RelativeOrAbsolute);
                case 84:
                    return new Uri("MapTiles/yellow_grass.png", UriKind.RelativeOrAbsolute);
                case 85:
                    return new Uri("MapTiles/bridge_ns.png", UriKind.RelativeOrAbsolute);
                case 86:
                    return new Uri("MapTiles/pier_ew.png", UriKind.RelativeOrAbsolute);
                     * */
                default:
                    return new Uri("MapTiles/blank.png", UriKind.RelativeOrAbsolute);
            }
        }

        public Uri GetBackgroundImageURIFromNumber(int number)
        {
            switch (number)
            {
                // We can't include the Basternae map tiles because they're not public domain.

                // TODO: Make the tiles configurable in a file.

                /*
                case 0:
                    return new Uri("MapTiles/field_green.png", UriKind.RelativeOrAbsolute);
                case 1:
                    return new Uri("MapTiles/field_green.png", UriKind.RelativeOrAbsolute);
                case 2:
                    return new Uri("MapTiles/field_green.png", UriKind.RelativeOrAbsolute);
                case 3:
                    return new Uri("MapTiles/field_green.png", UriKind.RelativeOrAbsolute);
                case 4:
                    return new Uri("MapTiles/field_green.png", UriKind.RelativeOrAbsolute);
                case 5:
                    return new Uri("MapTiles/field_green.png", UriKind.RelativeOrAbsolute);
                case 6:
                    return new Uri("MapTiles/desert.png", UriKind.RelativeOrAbsolute);
                case 7:
                    return new Uri("MapTiles/tundra.png", UriKind.RelativeOrAbsolute);
                case 8:
                    return new Uri("MapTiles/swamp.png", UriKind.RelativeOrAbsolute);
                case 9:
                    return new Uri("MapTiles/field_green.png", UriKind.RelativeOrAbsolute);
                case 10:
                    return new Uri("MapTiles/lava.png", UriKind.RelativeOrAbsolute);
                case 11:
                    return new Uri("MapTiles/glacier.png", UriKind.RelativeOrAbsolute);
                case 12:
                    return new Uri("MapTiles/tundra.png", UriKind.RelativeOrAbsolute);
                case 13:
                    return new Uri("MapTiles/field_green.png", UriKind.RelativeOrAbsolute);
                case 14:
                    return new Uri("MapTiles/river.png", UriKind.RelativeOrAbsolute);
                case 15:
                    return new Uri("MapTiles/ocean.png", UriKind.RelativeOrAbsolute);
                case 16:
                    return new Uri("MapTiles/ocean.png", UriKind.RelativeOrAbsolute);
                case 17:
                    return new Uri("MapTiles/ocean.png", UriKind.RelativeOrAbsolute);
                case 18:
                    return new Uri("MapTiles/ocean.png", UriKind.RelativeOrAbsolute);
                case 19:
                    return new Uri("MapTiles/white.png", UriKind.RelativeOrAbsolute);
                case 20:
                    return new Uri("MapTiles/cavefloor.png", UriKind.RelativeOrAbsolute);
                case 21:
                    return new Uri("MapTiles/cavefloor.png", UriKind.RelativeOrAbsolute);
                case 22:
                    return new Uri("MapTiles/cavefloor.png", UriKind.RelativeOrAbsolute);
                case 23:
                    return new Uri("MapTiles/ocean.png", UriKind.RelativeOrAbsolute);
                case 24:
                    return new Uri("MapTiles/ocean.png", UriKind.RelativeOrAbsolute);
                case 25:
                    return new Uri("MapTiles/solidrock.png", UriKind.RelativeOrAbsolute);
                case 26:
                    return new Uri("MapTiles/blank.png", UriKind.RelativeOrAbsolute);
                case 27:
                    return new Uri("MapTiles/ocean.png", UriKind.RelativeOrAbsolute);
                case 28:
                    return new Uri("MapTiles/glacier.png", UriKind.RelativeOrAbsolute);
                case 29:
                    return new Uri("MapTiles/lava.png", UriKind.RelativeOrAbsolute);
                case 30:
                    return new Uri("MapTiles/blue.bmp", UriKind.RelativeOrAbsolute);
                case 31:
                    return new Uri("MapTiles/blue.bmp", UriKind.RelativeOrAbsolute);
                case 32:
                    return new Uri("MapTiles/cavefloor.png", UriKind.RelativeOrAbsolute);
                case 33:
                    return new Uri("MapTiles/white.png", UriKind.RelativeOrAbsolute);
                case 34:
                    return new Uri("MapTiles/white.png", UriKind.RelativeOrAbsolute);
                     * */
                default:
                    return new Uri("MapTiles/blank.png", UriKind.RelativeOrAbsolute);
            }
        }

        public void ParseMapText(string text)
        {
            mapGrid.Visibility = Visibility.Visible;
            lblDescription.Visibility = Visibility.Collapsed;
            char[] pipe = { '|' };
            char[] newline = { '\n' };
            char[] colon = { ':' };
            string[] lines = text.Split(colon, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in lines)
            {
                if (str.Length < 2)
                    continue;
                string[] pieces = { str.Substring(0,1), str.Substring(2) };
                if( pieces.Length > 1 )
                {
                    int line = 0;
                    if( Int32.TryParse(pieces[0], out line) && line < 9)
                    {
                        string data = pieces[1];
                        if (data.Length > 17)
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                _backgroundTiles[line, i] = data[i * 2] - 64;
                                _foregroundTiles[line, i] = data[i * 2 + 1] - 64;

                                System.Windows.Controls.Image img = mapGrid.Children.Cast<System.Windows.Controls.Image>().First(e => Grid.GetRow(e) == line && Grid.GetColumn(e) == i);

                                if (img != null)
                                {
                                    // This may be a bit of a memory leak that we have to look at later.
                                    Uri fore = GetForegroundImageURIFromNumber(_foregroundTiles[line, i]);
                                    Uri back = GetBackgroundImageURIFromNumber(_backgroundTiles[line, i]);
                                    //img.Source = new BitmapImage(back);
                                    ImageDrawing background = new ImageDrawing(new BitmapImage(back), new Rect(0, 0, 32, 32));
                                    Bitmap bitmap = new Bitmap(fore.OriginalString);
                                    bitmap.MakeTransparent();
                                    ImageDrawing foreground = new ImageDrawing(System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                        IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()), new Rect(0, 0, 32, 32));
                                    DrawingGroup myDrawingGroup = new DrawingGroup();
                                    myDrawingGroup.Children.Add(background);
                                    myDrawingGroup.Children.Add(foreground);
                                    // Add player icon to center of map.
                                    //if (line == 4 && i == 4)
                                    //{
                                    //    Bitmap bitmap2 = new Bitmap("MapTiles/humanoid_shadow_medium_w.png");
                                    //    bitmap2.MakeTransparent();
                                    //    ImageDrawing player = new ImageDrawing(System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                    //        IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()), new Rect(0, 0, 32, 32));
                                    //    myDrawingGroup.Children.Add(player);
                                    //}
                                    img.Source = new DrawingImage(myDrawingGroup);
                                }
                                else
                                {
                                    throw new IndexOutOfRangeException("Bad tile location on map: " + line + "," + i);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes the room description from incoming data.
        /// </summary>
        /// <param name="text"></param>
        public void ParseRoomDescriptionText(string text)
        {
            mapGrid.Visibility = Visibility.Collapsed;
            lblDescription.Visibility = Visibility.Visible;
            // TODO: Parse ANSI codes and use them in text display.
            lblDescription.Text = SocketConnection.RemoveANSICodes(text);
        }
    }
}
