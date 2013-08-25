using System;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Functions related to movement.
    /// </summary>
    public class Movement
    {
        public static Dictionary<TerrainType, int> MovementLoss = new Dictionary<TerrainType, int>();
        public static bool _initialized;

        /// <summary>
        /// Directional trap flags.
        /// </summary>
        public static Trap.TriggerType[] TrapDirectionFlag = new[]            
        {
            Trap.TriggerType.north, Trap.TriggerType.east, Trap.TriggerType.south, Trap.TriggerType.west,
            Trap.TriggerType.up, Trap.TriggerType.down, Trap.TriggerType.northwest,
            Trap.TriggerType.southwest, Trap.TriggerType.northeast, Trap.TriggerType.southeast
        };

        /// <summary>
        /// Defines movement loss by terrain and various other values.
        /// </summary>
        public static void Initialize()
        {
            MovementLoss.Add(TerrainType.air, 1);
            MovementLoss.Add(TerrainType.arctic, 3);
            MovementLoss.Add(TerrainType.city, 1);
            MovementLoss.Add(TerrainType.desert, 3);
            MovementLoss.Add(TerrainType.field, 2);
            MovementLoss.Add(TerrainType.forest, 3);
            MovementLoss.Add(TerrainType.glacier, 4);
            MovementLoss.Add(TerrainType.hills, 4);
            MovementLoss.Add(TerrainType.inside, 1);
            MovementLoss.Add(TerrainType.jungle, 4);
            MovementLoss.Add(TerrainType.lava, 5);
            MovementLoss.Add(TerrainType.mountain, 6);
            MovementLoss.Add(TerrainType.ocean, 10);
            MovementLoss.Add(TerrainType.plane_astral, 1);
            MovementLoss.Add(TerrainType.plane_ethereal, 1);
            MovementLoss.Add(TerrainType.plane_of_air, 1);
            MovementLoss.Add(TerrainType.plane_of_earth, 8);
            MovementLoss.Add(TerrainType.plane_of_fire, 3);
            MovementLoss.Add(TerrainType.plane_of_water, 6);
            MovementLoss.Add(TerrainType.road, 1);
            MovementLoss.Add(TerrainType.swamp, 3);
            MovementLoss.Add(TerrainType.swimmable_water, 8);
            MovementLoss.Add(TerrainType.tundra, 3);
            MovementLoss.Add(TerrainType.underground_city, 1);
            MovementLoss.Add(TerrainType.underground_frozen, 4);
            MovementLoss.Add(TerrainType.underground_impassable, 500);
            MovementLoss.Add(TerrainType.underground_indoors, 1);
            MovementLoss.Add(TerrainType.underground_no_ground, 2);
            MovementLoss.Add(TerrainType.underground_ocean, 10);
            MovementLoss.Add(TerrainType.underground_swimmable_water, 8);
            MovementLoss.Add(TerrainType.underground_unswimmable_water, 8);
            MovementLoss.Add(TerrainType.underground_wild, 4);
            MovementLoss.Add(TerrainType.underwater_has_ground, 8);
            MovementLoss.Add(TerrainType.underwater_no_ground, 8);
            MovementLoss.Add(TerrainType.unswimmable_water, 3);
            foreach (int o in Enum.GetValues(typeof(TerrainType)))
            {
                if (!MovementLoss.ContainsKey((TerrainType)o))
                {
                    throw new InvalidOperationException("The terrain type " + ((TerrainType)o) + " does not have a corresponding movement loss entry in the movement cost table.  Edit the Initialize() member of the Movement class to fix this.");
                }
            }
            _initialized = true;
        }

        void SetFlyLevel( CharData ch, CharData.FlyLevel new_level )
        {
            if (new_level < 0 || new_level > CharData.FlyLevel.high )
            {
                Log.Error( "SetFlyLevel: level out of range", 0 );
                return;
            }
            ch._flyLevel = new_level;
            foreach( Object obj in ch._carrying )
            {
                obj.FlyLevel = new_level;
            }
            return;
        }

        void SetObjectFlyLevel( Object obj, CharData.FlyLevel newLevel )
        {
            obj.FlyLevel = newLevel;
            if (newLevel < 0 || newLevel > CharData.FlyLevel.high)
            {
                Log.Error( "SetObjectFlyLevel: level out of range", 0 );
                return;
            }
            foreach( Object item in obj.Contains )
            {
                SetObjectFlyLevel( item, newLevel );
            }
            return;
        }

        /// <summary>
        /// Scanning function.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="room"></param>
        /// <param name="text"></param>
        /// <param name="distance"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static int ScanRoom( CharData ch, Room room, ref string text, int distance, int dir )
        {
            int numberFound = 0;
            string distanceMsg;

            if( dir < 0 || dir >= Limits.MAX_DIRECTION )
            {
                Log.Error( "ScanRoom: direction {0} out of bounds!", dir );
                ch.SendText( "Bug while scanning, direction out of bounds!\r\n" );
                return 0;
            }

            // Not going to find anything in a room that is unscannable - Xangis
            if (room.HasFlag(RoomTemplate.ROOM_NO_SCAN))
            {
                return 0;
            }

            switch( distance )
            {
                case 1:
                    distanceMsg = String.Format( "&n who is close by to the " );
                    break;
                case 2:
                    distanceMsg = String.Format( "&n who is not far off to the " );
                    break;
                case 3:
                    distanceMsg = String.Format( "&n who is a brief walk away to the " );
                    break;
                default:
                    distanceMsg = String.Format( "&n who is an unknown distance to the " );
                    break;
            }

            foreach( CharData target in room.People )
            {
                if( ch._flyLevel == target._flyLevel )
                {
                    Visibility visibility = Look.HowSee(ch, target);
                    switch( visibility )
                    {
                        case Visibility.sense_hidden:
                        case Visibility.invisible:
                        case Visibility.too_dark:
                        default:
                            break;
                        case Visibility.visible:
                            text += ( target.ShowNameTo( ch, true ));
                            text += distanceMsg;
                            text += Exit.DirectionName[ dir ];
                            text += ".&n\r\n";
                            numberFound++;
                            break;
                        case Visibility.sense_infravision:
                            text += "&+rYou sense a being within the darkness";
                            text += distanceMsg;
                            text += Exit.DirectionName[ dir ];
                            text += ".&n\r\n";
                            numberFound++;
                            break;
                    }
                }
            }
            return numberFound;
        }

        public static int GolemGuardDirection( CharData ch )
        {
            // TODO: Verify that this works correctly since it was rewritten.
            
            int index = ch._name.IndexOf( "guild_" );
            if (index == -1)
            {
                return -1;
            }
            string tmp = ch._name.Substring( index + 6 );
            index = tmp.IndexOf( "_" );
            if (index == -1)
            {
                return -1;
            }
            tmp = tmp.Substring( 0, index );
            int dir = FindExit( ch, tmp );
            return dir;
        }

        /// <summary>
        /// Random room generation function.
        /// </summary>
        /// <returns></returns>
        public static Room GetRandomRoom()
        {
            Room room;

            for( ; ; )
            {
                room = Room.GetRoom( MUDMath.NumberRange( 0, 65535 ) );
                if (room)
                {
                    if (!room.HasFlag(RoomTemplate.ROOM_PRIVATE) && !room.HasFlag(RoomTemplate.ROOM_SOLITARY))
                    {
                        break;
                    }
                }
            }

            return room;
        }

        /// <summary>
        /// Returns a random room on one of the maps.
        /// Argument: 0 = any map
        ///           1 = Surface map 1
        ///           2 = Surface map 2
        ///           3 = Underdark map 1
        ///           4 = Underdark map 2
        ///           5 = Underdark map 3
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Room GetRandomMapRoom( Area map )
        {
            if (Room.Count < 1)
            {
                throw new IndexOutOfRangeException("No rooms loaded, cannot get a random map room.");
            }

            if (map == null)
            {
                List<Area> zones = new List<Area>();
                foreach( Area area in Database.AreaList )
                {
                    if (area.HasFlag(Area.AREA_WORLDMAP) && area.Rooms.Count > 0)
                    {
                        zones.Add(area);
                    }
                }
                if (zones.Count < 1)
                {
                    throw new IndexOutOfRangeException("GetRandomMapRoom(): No zones found with AREA_WORLDMAP flag set.");
                }
                int val = MUDMath.NumberRange(0, (zones.Count - 1));
                map = zones[val];
            }

            int max = map.Rooms.Count;
            if (max < 1)
            {
                throw new IndexOutOfRangeException("GetRandomMapRoom(): No rooms found in target zone.");
            }

            int target = MUDMath.NumberRange(0, (map.Rooms.Count - 1));
            Room room = Room.GetRoom( target );
            if (!room ||
                    room.WorldmapTerrainType == 92 ||
                    room.WorldmapTerrainType == 101 ||
                    room.WorldmapTerrainType == 102 ||
                    room.WorldmapTerrainType == 116 ||
                    room.WorldmapTerrainType == 130 ||
                    room.WorldmapTerrainType == 131 ||
                    room.WorldmapTerrainType == 132 ||
                    room.WorldmapTerrainType == 136 ||
                    room.WorldmapTerrainType == 137 ||
                    room.HasFlag(RoomTemplate.ROOM_PRIVATE) ||
                    room.HasFlag(RoomTemplate.ROOM_SOLITARY) ||
                    room.HasFlag(RoomTemplate.ROOM_NO_TELEPORT) ||
                    room.TerrainType == TerrainType.underground_impassable)
            {
                room = GetRandomMapRoom(map);
            }
            return room;
        }

        /// <summary>
        /// Gets the scan info for the provided room.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="room"></param>
        /// <param name="buffer"></param>
        /// <param name="flyLevel"></param>
        /// <returns></returns>
        public static int ScanThisRoom( CharData ch, Room room, string buffer, CharData.FlyLevel flyLevel )
        {
            int numberFound = 0;
            string levelMsg;

            int diff = flyLevel - ch._flyLevel;
            switch( diff )
            {
                case -3:
                    levelMsg = String.Format( "&n who is very far beneath you." );
                    break;
                case -2:
                    levelMsg = String.Format( "&n who is not far beneath you." );
                    break;
                case -1:
                    levelMsg = String.Format( "&n who is close by beneath you." );
                    break;
                case 1:
                    levelMsg = String.Format( "&n who is flying close by above you." );
                    break;
                case 2:
                    levelMsg = String.Format( "&n who is flying not far above you." );
                    break;
                case 3:
                    levelMsg = String.Format( "&n who is flying very far above you." );
                    break;
                default:
                    levelMsg = String.Format( "&n who is an unknown distance away from you." );
                    break;
            }

            foreach( CharData target in room.People )
            {
                if( target._flyLevel == flyLevel )
                {
                    Visibility visibility = Look.HowSee(ch, target);
                    switch( visibility )
                    {
                        case Visibility.sense_hidden:
                        case Visibility.invisible:
                        case Visibility.too_dark:
                        default:
                            break;
                        case Visibility.visible:
                            buffer += ( target.ShowNameTo( ch, true ));
                            buffer += levelMsg;
                            buffer += "&n\r\n";
                            numberFound++;
                            break;
                        case Visibility.sense_infravision:
                            buffer += "&+rYou sense a being within the darkness";
                            buffer += levelMsg;
                            buffer += "&n\r\n";
                            numberFound++;
                            break;
                    }
                }
            }
            return numberFound;
        }

        /// <summary>
        /// Find a door based on its name.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static Room FindRoom( CharData ch, string argument )
        {
            int dir = FindExit( ch, argument );
            if( dir == -1 )
            {
                SocketConnection.Act( "I see no door $T here.", ch, null, argument, SocketConnection.MessageTarget.character );
                return null;
            }
            if( ch._inRoom.ExitData[ dir ] )
            {
                Room room = Room.GetRoom(ch._inRoom.ExitData[ dir ].IndexNumber);
                return room;
            }
            return null;
        }

        /// <summary>
        /// Finds a door based on a keyword or direction.  Use FindExit if you only need to get an exit,
        /// no door required.  This should _not_ tell the character anything; it is an internal function.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static int FindDoor( CharData ch, string arg )
        {
            Exit exit;
            int door = Exit.DoorLookup(arg);
            if (door == -1)
            {
                for( door = 0; door < Limits.MAX_DIRECTION; door++ )
                {
                    if ((exit = ch._inRoom.ExitData[door]) && exit.HasFlag(Exit.ExitFlag.is_door)
                            && !(ch._level < Limits.LEVEL_AVATAR && exit.ExitFlags != 0
                                  && (exit.HasFlag(Exit.ExitFlag.secret) ||
                                      exit.HasFlag(Exit.ExitFlag.blocked)))
                            && exit.Keyword.Length != 0 && ("door".Equals(arg, StringComparison.CurrentCultureIgnoreCase)
                                 || MUDString.NameContainedIn(arg, exit.Keyword)))
                    {
                        return door;
                    }
                }
                return -1;
            }

            exit = ch._inRoom.ExitData[ door ];
            if( !exit )
            {
                return -1;
            }

            if (!exit.HasFlag(Exit.ExitFlag.is_door))
            {
                return -1;
            }

            return door;
        }

        /// <summary>
        /// Finds an exit based on a keyword.  Use FindDoor if you are looking for a door.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static int FindExit( CharData ch, string arg )
        {
            Exit exit;
            int door = Exit.DoorLookup(arg);
            if( door == -1 )
            {
                for( door = 0; door < Limits.MAX_DIRECTION; door++ )
                {
                    if ((exit = ch._inRoom.ExitData[door]) && exit.HasFlag(Exit.ExitFlag.is_door)
                            && !(ch._level < Limits.LEVEL_AVATAR && exit.ExitFlags != 0
                                  && (exit.HasFlag(Exit.ExitFlag.secret) ||
                                      exit.HasFlag(Exit.ExitFlag.blocked)))
                            && exit.Keyword.Length != 0 && ("door".Equals(arg, StringComparison.CurrentCultureIgnoreCase)
                                 || MUDString.NameContainedIn(arg, exit.Keyword)))
                    {
                        return door;
                    }
                }
                SocketConnection.Act( "I see no $T here.", ch, null, arg, SocketConnection.MessageTarget.character );
                return -1;
            }

            if( !( exit = ch._inRoom.ExitData[ door ] ) )
            {
                SocketConnection.Act( "I see no door $T here.", ch, null, arg, SocketConnection.MessageTarget.character );
                return -1;
            }

            return door;
        }
    }
}
