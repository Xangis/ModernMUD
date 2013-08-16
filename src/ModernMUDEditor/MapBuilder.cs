using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ModernMUD;

namespace ModernMUDEditor
{
    public struct Location
    {
        public int x;
        public int y;
        public int z;
    }

    public class MapBuilder
    {
        public static SerializableDictionary<int, Location> BuildMapGrid(List<RoomTemplate> rooms)
        {
            SerializableDictionary<int, Location> mapGrid = new SerializableDictionary<int, Location>();

            if( rooms.Count < 1 )
            {
                return mapGrid;
            }

            Location location = new Location();
            location.x = 0;
            location.y = 0;
            location.z = 0;

            for(int count = 0; count < rooms.Count; count++ )
            {
                if (!mapGrid.ContainsKey(rooms[count].IndexNumber))
                {
                    mapGrid.Add(rooms[count].IndexNumber, location);
                }
                else
                {
                    location = mapGrid[rooms[count].IndexNumber];
                }

                for (int exit = 0; exit < Limits.MAX_DIRECTION; exit++)
                {
                    if (rooms[count].ExitData[exit] != null)
                    {
                        if( !mapGrid.ContainsKey( rooms[count].ExitData[exit].IndexNumber ))
                        {
                            Location newLocation = new Location();
                            newLocation.x = location.x;
                            newLocation.y = location.y;
                            newLocation.z = location.z;
                            switch( exit )
                            {
                                case Exit.DIRECTION_DOWN:
                                    newLocation.z -= 1;
                                    break;
                                case Exit.DIRECTION_EAST:
                                    newLocation.x += 1;
                                    break;
                                case Exit.DIRECTION_NORTH:
                                    newLocation.y += 1;
                                    break;
                                case Exit.DIRECTION_NORTHEAST:
                                    newLocation.x += 1;
                                    newLocation.y += 1;
                                    break;
                                case Exit.DIRECTION_NORTHWEST:
                                    newLocation.x -= 1;
                                    newLocation.y += 1;
                                    break;
                                case Exit.DIRECTION_SOUTH:
                                    newLocation.y -= 1;
                                    break;
                                case Exit.DIRECTION_SOUTHEAST:
                                    newLocation.y -= 1;
                                    newLocation.x += 1;
                                    break;
                                case Exit.DIRECTION_SOUTHWEST:
                                    newLocation.x -= 1;
                                    newLocation.y -= 1;
                                    break;
                                case Exit.DIRECTION_UP:
                                    newLocation.z += 1;
                                    break;
                                case Exit.DIRECTION_WEST:
                                    newLocation.x -= 1;
                                    break;
                            }
                            mapGrid.Add(rooms[count].ExitData[exit].IndexNumber, newLocation);
                        }
                    }

                    if (!mapGrid.ContainsKey(rooms[count].IndexNumber))
                    {
                        MessageBox.Show("Bad room " + rooms[count].IndexNumber + ": not in entry list");
                        mapGrid.Add(rooms[count].IndexNumber, location);
                    }
                }
            }

            return mapGrid;
        }
    }
}
