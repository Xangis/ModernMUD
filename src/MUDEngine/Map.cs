using System.Xml.Serialization;
using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Worldmap utility functions.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// If in graphical client mode, returns the character pair representing foreground
        /// and background terrain types.
        /// 
        /// Otherwise, returns the colorized ASCII string representing that map square.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="graphicalClient"></param>
        /// <returns></returns>
        public static string GetMapCharacters(RoomTemplate room, bool graphicalClient)
        {
            int offset = 64;
            if (graphicalClient)
            {
                // Convert terrain type to terminal-friendly characters.
                char val1 = (char)(room.TerrainType + offset);
                char val2 = (char)offset;
                if (room.WorldmapTerrainType != 0)
                {
                    val2 = (char)(room.WorldmapTerrainType + offset);
                }
                else
                {
                    // Set default overlays for certain terrain types (trees, mountains, hills).
                    switch (room.TerrainType)
                    {
                        default:
                            break;
                        case TerrainType.city:
                            // Neutral city by default.
                            val2 = (char)(41 + offset);
                            break;
                        case TerrainType.forest:
                            // Oak tree by default.
                            val2 = (char)(42 + offset);
                            break;
                        case TerrainType.hills:
                            // Green hills by default.
                            val2 = (char)(19 + offset);
                            break;
                        case TerrainType.jungle:
                            val2 = (char)(31 + offset);
                            break;
                        case TerrainType.mountain:
                            val2 = (char)(37 + offset);
                            break;
                        case TerrainType.road:
                            val2 = (char)(55 + offset);
                            // TODO: Determine tile type based on roads nearby, or populate the map with better data.
                            // for example, these are the road tiles available in the Basternae client:
                            //
                            //_foregroundTile[52] =  new wxBitmap( pathPrefix + _("road_ew.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[53] =  new wxBitmap( pathPrefix + _("road_ns.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[54] =  new wxBitmap( pathPrefix + _("road_4way.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[55] =  new wxBitmap( pathPrefix + _("road_corner_ne.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[56] =  new wxBitmap( pathPrefix + _("road_corner_nw.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[57] =  new wxBitmap( pathPrefix + _("road_corner_se.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[58] =  new wxBitmap( pathPrefix + _("road_corner_sw.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[59] =  new wxBitmap( pathPrefix + _("road_tshape_e.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[60] =  new wxBitmap( pathPrefix + _("road_tshape_n.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[61] =  new wxBitmap( pathPrefix + _("road_tshape_s.png"), wxBITMAP_TYPE_PNG ) ;
                            //_foregroundTile[62] =  new wxBitmap( pathPrefix + _("road_tshape_w.png"), wxBITMAP_TYPE_PNG ) ;
                            break;
                    }
                }
                return val1.ToString() + val2.ToString();
            }

            if (room.WorldmapTerrainType == 0)
            {
                switch (room.TerrainType)
                {
                    default:
                        return "&+w.&n";
                    case TerrainType.city:
                        return "&+W^&n";
                    case TerrainType.field:
                        return "&+G.&n";
                    case TerrainType.forest:
                        return "&+g*&n";
                    case TerrainType.hills:
                        return "&+y^&n";
                    case TerrainType.jungle:
                        return "&+G*&n";
                    case TerrainType.lava:
                        return "&+r#&n";
                    case TerrainType.mountain:
                        return "&+yM&n";
                    case TerrainType.ocean:
                    case TerrainType.unswimmable_water:
                        return "&-b&+B~&n";
                    case TerrainType.underground_wild:
                        return "&+m.&n";
                    case TerrainType.underground_unswimmable_water:
                    case TerrainType.underground_ocean:
                        return "&-b&+L~&n";
                    case TerrainType.underground_frozen:
                        return "&+W.&n";
                    case TerrainType.tundra:
                        return "&+W.&n";
                    case TerrainType.underground_swimmable_water:
                    case TerrainType.swimmable_water:
                        return "&-B &n";
                    case TerrainType.swamp:
                        return "&+G#&n";
                    case TerrainType.road:
                        return "&+w+&n";
                    case TerrainType.glacier:
                        return "&+W#&n";
                    case TerrainType.desert:
                        return "&+Y~&n";
                }
            }
            else
            {
                return "&-L &n";
            }
        }

        /// <summary>
        /// Modifies underground visibility based on time of day and racewar side.
        /// </summary>
        /// <param name="racewarside"></param>
        /// <returns></returns>
        public static int GetUnderdarkVisibilityModifier( Race.RacewarSide racewarside )
        {
            switch( racewarside )
            {
                case Race.RacewarSide.evil:
                    return 4;
                case Race.RacewarSide.good:
                    return 0;
                default:
                    return 2;
            }
        }

        /// <summary>
        /// Modifies daytime visibility based on time of day and racewar side.
        /// </summary>
        /// <param name="racewarside"></param>
        /// <returns></returns>
        public static int GetDaytimeVisibilityModifier(Race.RacewarSide racewarside)
        {
            switch( Database.SystemData.WeatherData.Sunlight )
            {
                case SunType.night:
                case SunType.moonset:
                case SunType.moonrise:
                    if( racewarside == Race.RacewarSide.evil )
                        return 4;
                    if( racewarside == Race.RacewarSide.good )
                        return 0;
                    return 2;
                case SunType.sunset:
                case SunType.sunrise:
                    if( racewarside == Race.RacewarSide.evil )
                        return 1;
                    if( racewarside == Race.RacewarSide.good )
                        return 2;
                    return 2;
                case SunType.daytime:
                    if( racewarside == Race.RacewarSide.evil )
                        return 0;
                    if( racewarside == Race.RacewarSide.good )
                        return 4;
                    return 2;
                default:
                    return 0;
            }
        }

    };
}