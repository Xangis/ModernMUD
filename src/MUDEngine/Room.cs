using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents a room in the game (at runtime).
    /// </summary>
    public class Room : RoomTemplate
    {
        private List<CharData> _people = new List<CharData>();
        private List<Object> _contents = new List<Object>();
        private WarfareStructure _warfareStructure;

        /// <summary>
        /// Creates a room from a room template.
        /// </summary>
        /// <param name="room"></param>
        public Room(RoomTemplate room)
        {
            Area = room.Area;
            BaseRoomFlags = room.BaseRoomFlags;
            Current = room.Current;
            CurrentDirection = room.CurrentDirection;
            CurrentRoomFlags = room.CurrentRoomFlags;
            Description = room.Description;
            ExitData = room.ExitData;
            ExtraDescriptions = room.ExtraDescriptions;
            FallChance = room.FallChance;
            Light = room.Light;
            TerrainType = room.TerrainType;
            Title = room.Title;
            IndexNumber = room.IndexNumber;
            WorldmapTerrainType = room.WorldmapTerrainType;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Room()
        {
            ++_numRooms;

            Title = "Unnamed Room";
            Description = "This room has no description.";
            ControlledBy = Race.RacewarSide.neutral;
            WarfareStructure = null;
            IndexNumber = 0;
            Light = 0;
            Current = 0;
            CurrentDirection = 0;
            FallChance = 0;
            TerrainType = 0;
            WorldmapTerrainType = 0;

            int count;
            for( count = 0; count < Limits.NUM_ROOM_FLAGS; ++count )
            {
                CurrentRoomFlags[ count ] = 0;
                BaseRoomFlags[ count ] = 0;
            }
            for( count = 0; count < Limits.MAX_DIRECTION; ++count )
            {
            }
        }

        /// <summary>
        /// Gets the total number of rooms in game.
        /// </summary>
        public static int Count
        {
            get
            {
                return _numRooms;
            }
        }

        /// <summary>
        /// The people and creatures present in the room.
        /// </summary>
        [XmlIgnore]
        public List<CharData> People
        {
            get { return _people; }
            set { _people = value; }
        }

        /// <summary>
        /// The objects present in the room.
        /// </summary>
        [XmlIgnore]
        public List<Object> Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }

        /// <summary>
        /// Gets/sets who controls this room.
        /// </summary>
        public Race.RacewarSide ControlledBy { get; set; }

        /// <summary>
        /// Accessor for warfare structures.
        /// </summary>
        public WarfareStructure WarfareStructure
        {
            get { return _warfareStructure; }
            set { _warfareStructure = value; }
        }

        /// <summary>
        /// Checks whether a spell cast by the player is swallowed by one of the starshell types.
        /// </summary>
        /// <param name="ch">The caster</param>
        /// <returns>true if the spell was eaten.</returns>
        public bool CheckStarshell(CharData ch)
        {
            if( !ch.IsClass( CharClass.Names.bard ) )
            {
                if( ch.InRoom.HasFlag( ROOM_EARTHEN_STARSHELL ) )
                {
                    ch.SendText( "You start casting...\r\n" );
                    ch.SendText( "&+lThe &+yearth&n &+lcomes up &+yand engulfs &+lyour spell.\r\n" );
                    Combat.InflictSpellDamage( ch, ch, 1, "earthen starshell", AttackType.DamageType.fire );
                    ch.WaitState( 6 );
                    return true;
                }
                if( ch.InRoom.HasFlag( ROOM_AIRY_STARSHELL ) )
                {
                    ch.SendText( "You start casting...\r\n" );
                    ch.SendText( "&+CAir swir&n&+cls a&+Cnd absorbs y&n&+cour spell.&n\r\n" );
                    ch.WaitState( 6 );
                    if( ch.CurrentPosition > Position.reclining && MUDMath.NumberPercent() + 50 > ch.GetCurrAgi() )
                    {
                        ch.CurrentPosition = Position.reclining;
                        ch.WaitState( 6 );
                        ch.SendText( "You are knocked over!\r\n" );
                    }
                    return true;
                }
                if( ch.InRoom.HasFlag( ROOM_WATERY_STARSHELL ) )
                {
                    ch.SendText( "You start casting...\r\n" );
                    ch.SendText( "&+bWater b&+Bursts up a&n&+bnd absor&+Bbs your spell.&n\r\n" );
                    ch.WaitState( 6 );
                    ch.CurrentMoves -= 20;
                    ch.SendText( "You feel tired!\r\n" );
                    return true;
                }
                if( ch.InRoom.HasFlag( ROOM_FIERY_STARSHELL ) )
                {
                    ch.SendText( "You start casting...\r\n" );
                    ch.SendText( "&+RFire&n&+r engu&+Rlfs y&n&+rour s&+Rpell.&n\r\n" );
                    Combat.InflictSpellDamage( ch, ch, 1, "fiery starshell", AttackType.DamageType.fire );
                    ch.WaitState( 6 );
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether spells can be cast in this room.
        /// </summary>
        /// <param name="ch">The caster</param>
        /// <param name="song">Is this a bard song?</param>
        /// <param name="start">true if just starting to cast, false if finishing up the spell.</param>
        /// <returns>true if the caster is able to cast spells here.</returns> 
        public bool CheckCastable( CharData ch, bool song, bool start )
        {
            if( !ch.IsClass( CharClass.Names.psionicist ) )
            {
                if( HasFlag( ROOM_SILENT ) )
                {
                    ch.SendText( "Your voice makes no sound in this room!\r\n" );
                    return false;
                }

                if( !song && HasFlag( ROOM_NO_MAGIC ) )
                {
                    // Extra message and a wait state if this happens when they start casting.
                    if( start )
                    {
                        ch.SendText( "You start casting..." );
                        ch.WaitState( 6 );
                    }
                    ch.SendText( "After a brief gathering of energy, your spell fizzles!\r\n" );
                    return false;
                }
            }
            else if( HasFlag( ROOM_NO_PSIONICS ) )
            {
                ch.SendText( "Something here prevents you from focusing your will.\r\n" );
                ch.WaitState( 2 );
                return false;
            }
            // No reason they can't cast.
            return true;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~Room()
        {
            for( int door = 0; door < Limits.MAX_DIRECTION; door++ )
            {
                if( ExitData[ door ] != null )
                {
                    ExitData[ door ] = null;
                }
            }
        }

        /// <summary>
        /// Searches the database for a room with the specified virtual number and
        /// return it if that room is found.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns>The RoomIndex if found, null if not.</returns>
        public static Room GetRoom( int indexNumber )
        {
            // There is a possibility of indexNumber passed is negative or zero.
            if (indexNumber <= 0)
            {
                return null;
            }

            foreach( Area area in Database.AreaList )
            {
                foreach( Room room in area.Rooms )
                {
                    if( room.IndexNumber == indexNumber )
                    {
                        return room;
                    }
                }
            }

            if( Database.DatabaseIsBooting )
            {
                Log.Error( "RoomIndex.GetRoom(): Bad indexNumber - room not found: " + indexNumber );
            }

            return null;
        }

        /// <summary>
        /// Display the room flags as a string.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public static string RoomString(Room room)
        {
            string text = String.Empty;

            foreach(BitvectorFlagType bvf in BitvectorFlagType.RoomFlags)
            {
                if (room.HasFlag(bvf.BitvectorData))
                {
                    text += " ";
                    text += bvf.Name;
                }
            }

            return (!String.IsNullOrEmpty(text)) ? text.Substring(1) : "none";
        }

        /// <summary>
        /// Checks whether a room has a particular flag set.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool HasFlag( Bitvector bvect )
        {
            if( Macros.IsSet( CurrentRoomFlags[ bvect.Group ], bvect.Vector ) )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove a bitvector flag from a room.
        /// </summary>
        /// <param name="bvect"></param>
        public void RemoveFlag( Bitvector bvect )
        {
            Macros.RemoveBit( ref CurrentRoomFlags[ bvect.Group ], bvect.Vector );
            return;
        }

        /// <summary>
        /// Add a bitvector flag to a room.
        /// </summary>
        /// <param name="bvect"></param>
        public void AddFlag( Bitvector bvect )
        {
            Macros.SetBit( ref CurrentRoomFlags[ bvect.Group ], bvect.Vector );
            return;
        }

        /// <summary>
        /// Lets us do boolean checks for null.
        /// </summary>
        /// <param name="ri"></param>
        /// <returns></returns>
        public static bool operator !( Room room )
        {
            if (room == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lets us do boolean checks for null.
        /// </summary>
        /// <param name="ri"></param>
        /// <returns></returns>
        public static implicit operator bool( Room ri )
        {
            if (ri == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks whether the room is dark.
        /// </summary>
        /// <returns></returns>
        public bool IsDark()
        {
            // Magic light overcomes magic darkness.  May want to cause
            // these flags to cancel each other out if both are set.
            if( HasFlag( ROOM_MAGICLIGHT ) )
            {
                return false;
            }

            // Magical darkness should overcome infravision and twilight.
            if( HasFlag( ROOM_MAGICDARK ) )
            {
                return true;
            }

            // You can always see in a twilight room.
            if( HasFlag( ROOM_TWILIGHT ) )
            {
                return false;
            }

            if( Light > 0 )
            {
                return false;
            }

            foreach( Object obj in _contents )
            {
                if (obj.ItemType == ObjTemplate.ObjectType.light && obj.Values[2] != 0)
                {
                    return false;
                }
                if (obj.HasFlag(ObjTemplate.ITEM_LIT))
                {
                    return false;
                }
            }

            if( HasFlag( ROOM_DARK ) )
            {
                return true;
            }

            if( TerrainType == TerrainType.city
                // Uncomment to make light sources unnecessary on the surface map.
                //|| TerrainType == TerrainType.inside
                //|| TerrainType == TerrainType.arctic
                //|| TerrainType == TerrainType.field
                //|| TerrainType == TerrainType.forest
                //|| TerrainType == TerrainType.hills
                //|| TerrainType == TerrainType.mountain
                //|| TerrainType == TerrainType.desert
                //|| TerrainType == TerrainType.underground_city
                //|| TerrainType == TerrainType.glacier
                //|| TerrainType == TerrainType.tundra
                //|| TerrainType == TerrainType.jungle
                //|| TerrainType == TerrainType.lava
                //|| TerrainType == TerrainType.ocean
                //|| TerrainType == TerrainType.swimmable_water
                )
                return false;

            if( Database.SystemData.WeatherData.Sunlight == SunType.sunset || Database.SystemData.WeatherData.Sunlight == SunType.night )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns True if room is private.
        /// </summary>
        /// <returns></returns>
        public bool IsPrivate()
        {
            int count = _people.Count;

            if( HasFlag( ROOM_PRIVATE ) && count >= 2 )
            {
                return true;
            }
            if( HasFlag( ROOM_SOLITARY ) && count >= 1 )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the room as a string, returning the title.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Title;
        }

        /// <summary>
        /// Resets a room, reloading according to resets.
        /// </summary>
        /// <param name="notInitial"></param>
        /// <returns></returns>
        public bool ResetRoom( int notInitial )
        {
            Exit exit = null;
            CharData mobile;
            CharData lastMob = null;
            int level = 0;
            bool last = true;
            // Added for debugging.
            string text = String.Empty;

            foreach( Reset reset in Area.Resets )
            {
                if (!reset.IsRoomReset(this))
                {
                    continue;
                }
                Object obj;
                Object lastObj;
                MobTemplate mobIndex;
                ObjTemplate objTemplate;
                Room room;

                switch( reset.Command )
                {
                    default:
                        Log.Error( "RoomIndex.Reset(): bad command %c.", reset.Command );
                        break;
                    case 'M':
                        mobIndex = Database.GetMobTemplate( reset.Arg1 );
                        if( !mobIndex )
                        {
                            Log.Error( "RoomIndex.Reset(): 'M': bad mob index number {0} for arg1.", reset.Arg1 );
                            continue;
                        }

                        room = GetRoom( reset.Arg3 );
                        if( !room )
                        {
                            Log.Error( "RoomIndex.Reset(): 'R': bad room index number {0} for arg3.", reset.Arg3 );
                            continue;
                        }

                        if( ( mobIndex.HasSpecFun( "spec_cast_ghost" ) )
                                && ( Database.SystemData.WeatherData.Sunlight > SunType.night )
                                && ( Database.SystemData.WeatherData.Sunlight < SunType.moonrise ) )
                        {
                            last = false;
                            continue;
                        }

                        level = Macros.Range( 0, mobIndex.Level - 2, Limits.LEVEL_HERO );

                        if( mobIndex.NumActive >= reset.Arg2 )
                        {
                            last = false;
                            break;
                        }
                        mobile = Database.CreateMobile( mobIndex );

                        // Check for pet shop.
                        Room prevRoom = GetRoom( room.IndexNumber - 1 );
                        if (prevRoom && prevRoom.HasFlag(ROOM_PET_SHOP))
                        {
                            mobile.SetActionBit(MobTemplate.ACT_PET);
                        }

                        if (room.IsDark())
                        {
                            mobile.SetAffectBit(Affect.AFFECT_INFRAVISION);
                        }

                        mobile.AddToRoom( room );
                        mobile.LoadRoomIndexNumber = room.IndexNumber;

                        // This code makes mounts make their mounters mount them.
                        if( reset.Arg0 == -1 && lastMob )
                        {
                            // If set to be mounted.
                            String[] keywords = mobile.Name.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                            Command.Mount(lastMob, keywords);
                        }

                        lastMob = mobile;

                        level = Macros.Range( 0, mobile.Level - 2, Limits.LEVEL_HERO );
                        last = true;
                        break;

                    case 'F':
                        mobIndex = Database.GetMobTemplate( reset.Arg1 );
                        if( !mobIndex )
                        {
                            Log.Error( "RoomIndex.Reset(): 'F': bad mob index number {0} for arg1.", reset.Arg1 );
                            continue;
                        }

                        room = GetRoom( reset.Arg3 );
                        if( !room )
                        {
                            Log.Error( "RoomIndex.Reset(): 'F': bad room index number {0} for arg3.", reset.Arg3 );
                            continue;
                        }

                        if( mobIndex.HasSpecFun( "spec_cast_ghost" ) && ( Database.SystemData.WeatherData.Sunlight > SunType.night )
                                && ( Database.SystemData.WeatherData.Sunlight < SunType.moonrise ) )
                        {
                            last = false;
                            continue;
                        }

                        level = Macros.Range( 0, mobIndex.Level - 2, Limits.LEVEL_HERO );

                        if( mobIndex.NumActive >= reset.Arg2 )
                        {
                            last = false;
                            break;
                        }
                        mobile = Database.CreateMobile( mobIndex );

                        Room prev = GetRoom( room.IndexNumber - 1 );
                        if (prev && prev.HasFlag(ROOM_PET_SHOP))
                        {
                            mobile.SetActionBit(MobTemplate.ACT_PET);
                        }

                        if( room.IsDark() )
                        {
                            mobile.SetAffectBit( Affect.AFFECT_INFRAVISION );
                        }

                        // Set following bit. Can't have a message sent because
                        // there is no valid room number (causes a segmentation fault)
                        CharData.AddFollowerWithoutMessage( mobile, lastMob );

                        lastMob = mobile;
                        mobile.AddToRoom( room );
                        mobile.LoadRoomIndexNumber = room.IndexNumber;
                        level = Macros.Range( 0, mobile.Level - 2, Limits.LEVEL_HERO );
                        last = true;
                        break;

                    case 'O':
                        if( notInitial != 0 )
                        {
                            last = false;
                            continue;
                        }
                        objTemplate = Database.GetObjTemplate( reset.Arg1 );
                        if( !objTemplate )
                        {
                            Log.Error( "RoomIndex.Reset(): 'O': bad obj index number {0} for arg1.", reset.Arg1 );
                            continue;
                        }

                        room = GetRoom( reset.Arg3 );
                        if( !room )
                        {
                            Log.Error( "RoomIndex.Reset(): 'O': bad room index number {0} for arg3.", reset.Arg3 );
                            continue;
                        }

                        if( Area.NumPlayers > 0 || room._contents.Count != 0 && ( Object.CountObjectInList( objTemplate, room._contents ) > 0 ) )
                        {
                            last = false;
                            break;
                        }
                        // check if is unique/arti
                        if( Database.IsArtifact( objTemplate.IndexNumber ) )
                        {
                            text += "RoomIndex.Reset(): Artifact found: " + objTemplate.Name + " (" + objTemplate.IndexNumber + ")";
                            Log.Trace( text );
                        } //end if artiact

                        obj = Database.CreateObject( objTemplate, MUDMath.FuzzyNumber( level ) );
                        if( obj != null )
                        {
                            obj.Cost = 0;
                            obj.AddToRoom( this );
                            last = true;
                        }
                        else
                        {
                            Log.Error( "RoomIndex.Reset(): Unable to Database.CreateObject {0}", reset.Arg3 );
                        }
                        break;

                    case 'P':
                        if( notInitial != 0 )
                        {
                            last = false;
                            continue;
                        }
                        objTemplate = Database.GetObjTemplate( reset.Arg1 );
                        if( !objTemplate )
                        {
                            Log.Error( "RoomIndex.Reset(): 'P': bad obj index number {0} for arg3.", reset.Arg1 );
                            continue;
                        }

                        ObjTemplate targetObjTemplate = Database.GetObjTemplate( reset.Arg3 );
                        if( !targetObjTemplate )
                        {
                            Log.Error( "RoomIndex.Reset(): 'P': bad obj index number {0} for arg3.", reset.Arg3 );
                            continue;
                        }

                        if( Area.NumPlayers > 0 || !( lastObj = Object.GetFirstObjectOfTemplateType( targetObjTemplate ) )
                                || lastObj.Contains.Count != 0 && ( Object.CountObjectInList( objTemplate, lastObj.Contains ) > 0 ) )
                        {
                            last = false;
                            break;
                        }
                        // check if is unique/arti
                        if( Database.IsArtifact( objTemplate.IndexNumber ) )
                        {
                            text += "RoomIndex.Reset(): Artifact found: " + objTemplate.Name + " (" + objTemplate.IndexNumber + ")";
                            Log.Trace( text );
                        } //end if artifact

                        obj = Database.CreateObject( objTemplate, MUDMath.FuzzyNumber( lastObj.Level ) );
                        lastObj.AddToObject(obj);
                        last = true;

                        // Ensure that the container gets Reset.
                        if( ( lastObj.ItemType == ObjTemplate.ObjectType.container ) || ( lastObj.ItemType == ObjTemplate.ObjectType.drink_container ) )
                        {
                            lastObj.Values[ 1 ] = lastObj.ObjIndexData.Values[ 1 ];
                        }
                        break;

                    case 'G':
                    case 'E':
                        if( notInitial != 0 )
                        {
                            last = false;
                            continue;
                        }
                        objTemplate = Database.GetObjTemplate( reset.Arg1 );
                        if( !objTemplate )
                        {
                            Log.Error( "RoomIndex.Reset(): 'E' or 'G': bad obj index number {0} for arg1.", reset.Arg1 );
                            continue;
                        }

                        if( !last )
                            break;

                        if( !lastMob )
                        {
                            Log.Error( "RoomIndex.Reset(): 'E' or 'G': null mob for index number {0} for arg1.", reset.Arg1 );
                            last = false;
                            break;
                        }
                        // check if is unique/arti
                        if( Database.IsArtifact( objTemplate.IndexNumber ) )
                        {
                            text += "RoomIndex.Reset(): Artifact found: " + objTemplate.Name + " (" + objTemplate.IndexNumber + ")";
                            Log.Trace( text );
                        } //end if artifact

                        if( lastMob.MobileTemplate.ShopData )   /* Shop-keeper? */
                        {
                            int olevel;

                            switch( objTemplate.ItemType )
                            {
                                default:
                                    olevel = 0;
                                    break;
                                case ObjTemplate.ObjectType.pill:
                                    olevel = MUDMath.NumberRange( 0, 10 );
                                    break;
                                case ObjTemplate.ObjectType.potion:
                                    olevel = MUDMath.NumberRange( 0, 10 );
                                    break;
                                case ObjTemplate.ObjectType.scroll:
                                    olevel = MUDMath.NumberRange( 5, 15 );
                                    break;
                                case ObjTemplate.ObjectType.wand:
                                    olevel = MUDMath.NumberRange( 10, 20 );
                                    break;
                                case ObjTemplate.ObjectType.staff:
                                    olevel = MUDMath.NumberRange( 15, 25 );
                                    break;
                                case ObjTemplate.ObjectType.armor:
                                    olevel = MUDMath.NumberRange( 5, 15 );
                                    break;
                                case ObjTemplate.ObjectType.other:
                                    olevel = MUDMath.NumberRange( 5, 15 );
                                    break;
                                case ObjTemplate.ObjectType.clothing:
                                    olevel = MUDMath.NumberRange( 5, 15 );
                                    break;
                                case ObjTemplate.ObjectType.weapon:
                                    if( reset.Command == 'G' )
                                        olevel = MUDMath.NumberRange( 5, 15 );
                                    else
                                        olevel = MUDMath.FuzzyNumber( level );
                                    break;
                            }

                            obj = Database.CreateObject( objTemplate, olevel );
                            if( reset.Command == 'G' )
                            {
                                obj.AddFlag( ObjTemplate.ITEM_INVENTORY );
                            }
                        }
                        else
                        {
                            obj = Database.CreateObject( objTemplate, MUDMath.FuzzyNumber( level ) );
                        }
                        obj.ObjToChar( lastMob );
                        if( reset.Command == 'E' )
                        {
                            lastMob.EquipObject( ref obj, (ObjTemplate.WearLocation)reset.Arg3 );
                        }
                        last = true;
                        break;

                    case 'D':
                        if( reset.Arg2 < 0 || reset.Arg2 >= Limits.MAX_DIRECTION || !( exit = ExitData[ reset.Arg2 ] )
                                || !exit.HasFlag(Exit.ExitFlag.is_door))
                        {
                            Log.Error( "RoomIndex.Reset(): 'D': exit {0} not door for arg2.", reset.Arg2 );
                        }

                        switch( reset.Arg3 )
                        {
                            default:
                                Log.Error( "RoomIndex.Reset(): 'D': bad 'locks': {0} for arg3.", reset.Arg3 );
                                break;
                            case 0:
                                break;
                            case 1:
                                exit.AddFlag(Exit.ExitFlag.closed);
                                break;
                            case 2:
                                exit.AddFlag(Exit.ExitFlag.closed | Exit.ExitFlag.locked);
                                break;
                            case 4:
                                exit.AddFlag(Exit.ExitFlag.secret);
                                break;
                            case 5:
                                exit.AddFlag(Exit.ExitFlag.secret | Exit.ExitFlag.closed);
                                break;
                            case 6:
                                exit.AddFlag(Exit.ExitFlag.secret | Exit.ExitFlag.closed | Exit.ExitFlag.locked);
                                break;
                            case 8:
                                exit.AddFlag(Exit.ExitFlag.blocked);
                                break;
                            case 9:
                                exit.AddFlag(Exit.ExitFlag.blocked | Exit.ExitFlag.closed);
                                break;
                            case 10:
                                exit.AddFlag(Exit.ExitFlag.blocked | Exit.ExitFlag.closed | Exit.ExitFlag.locked);
                                break;
                            case 12:
                                exit.AddFlag(Exit.ExitFlag.blocked | Exit.ExitFlag.secret);
                                break;
                            case 13:
                                exit.AddFlag(Exit.ExitFlag.blocked | Exit.ExitFlag.secret | Exit.ExitFlag.closed);
                                break;
                            case 14:
                                exit.AddFlag( Exit.ExitFlag.blocked | Exit.ExitFlag.secret | Exit.ExitFlag.closed | Exit.ExitFlag.locked );
                                break;
                        }
                        break;

                    case 'R':
                        Log.Trace("Unsupported randomize room exits call.  Please implement this.");
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns true if the room is water.
        /// </summary>
        /// <returns></returns>
        public bool IsWater()
        {
            switch( TerrainType )
            {
                case TerrainType.swimmable_water:
                case TerrainType.unswimmable_water:
                case TerrainType.underwater_has_ground:
                case TerrainType.ocean:
                case TerrainType.underwater_no_ground:
                case TerrainType.underground_swimmable_water:
                case TerrainType.underground_unswimmable_water:
                case TerrainType.plane_of_water:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns true if the room is floorless.
        /// </summary>
        /// <returns></returns>
        public bool IsMidair()
        {
            switch( TerrainType )
            {
                case TerrainType.air:
                case TerrainType.plane_of_air:
                case TerrainType.underground_no_ground:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns true if you can fly in the room.
        /// </summary>
        /// <returns></returns>
        public bool IsFlyable()
        {

            if (TerrainType == TerrainType.underground_city
                    || TerrainType == TerrainType.underground_indoors
                    || TerrainType == TerrainType.underground_swimmable_water
                    || TerrainType == TerrainType.underground_unswimmable_water
                    || TerrainType == TerrainType.underground_no_ground
                    || TerrainType == TerrainType.inside
                    || TerrainType == TerrainType.underwater_has_ground
                    || TerrainType == TerrainType.underwater_no_ground
                    || TerrainType == TerrainType.plane_of_earth
                    || TerrainType == TerrainType.plane_of_fire
                    || TerrainType == TerrainType.plane_of_water
                    || TerrainType == TerrainType.plane_ethereal
                    || TerrainType == TerrainType.plane_astral)
            {
                return false;
            }
            if( HasFlag( ROOM_INDOORS ) )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets a location based on its name.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Room FindLocation( CharData ch, string arg )
        {
            if( MUDString.IsNumber( arg ) )
            {
                int val;
                Int32.TryParse(arg, out val);
                return GetRoom( val );
            }

            CharData victim = ch.GetCharWorld( arg );
            if( victim )
            {
                return victim.InRoom;
            }

            Object obj = Object.GetObjectInWorld( ch, arg );
            if( obj )
            {
                return obj.InRoom;
            }

            return null;
        }

        /// <summary>
        /// Utility function that gets a room's exit for a particular direction.
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        public Exit GetExit(Exit.Direction door)
        {
            // Exit direction could have been stored somewhere as an invalid value,
            // i.e. as a bad integer on a wall value. Check for it.
            if (!Enum.IsDefined(typeof(Exit.Direction), door))
            {
                return null;
            }
            return ExitData[(int)door];
        }
    }
}