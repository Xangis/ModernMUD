using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;

namespace ModernMUD
{
    /// <summary>
    /// Base class for a room.
    /// </summary>
    public class RoomTemplate
    {
        private List<ExtendedDescription> _extraDescriptions = new List<ExtendedDescription>();
        private Exit[] _exitData = new Exit[Limits.MAX_DIRECTION];
        private int[] _currentRoomFlags = new int[Limits.NUM_ROOM_FLAGS];
        private int[] _baseRoomFlags = new int[Limits.NUM_ROOM_FLAGS];
        protected static int _numRooms;
        private Race.RacewarSide _controlledBy; // Racewar side control.  Used only for worldmap.
        public static readonly int TERRAIN_MAX = Enum.GetValues(typeof(TerrainType)).Length;

        // TODO: Verify that all of these flags are implemented.

        /// <summary>
        /// No flag set.
        /// </summary>
        public static readonly Bitvector ROOM_NONE = new Bitvector(0, 0);
        /// <summary>
        /// Dark.
        /// </summary>
        public static readonly Bitvector ROOM_DARK = new Bitvector(0, Bitvector.BV00);
        /// <summary>
        /// Blocks scanning - you can scan into this room, but not past it.
        /// </summary>
        public static readonly Bitvector ROOM_LIMITSCAN = new Bitvector(0, Bitvector.BV01);
        /// <summary>
        /// Mobs can't normally go here.
        /// </summary>
        public static readonly Bitvector ROOM_NO_MOB = new Bitvector(0, Bitvector.BV02);
        /// <summary>
        /// Inside.
        /// </summary>
        public static readonly Bitvector ROOM_INDOORS = new Bitvector(0, Bitvector.BV03);
        /// <summary>
        /// Can't speak or cast spells.
        /// </summary>
        public static readonly Bitvector ROOM_SILENT = new Bitvector(0, Bitvector.BV04);
        /// <summary>
        /// Filled with water.
        /// </summary>
        public static readonly Bitvector ROOM_UNDERWATER = new Bitvector(0, Bitvector.BV05);
        /// <summary>
        /// Cannot use word of recall.
        /// </summary>
        public static readonly Bitvector ROOM_NO_RECALL = new Bitvector(0, Bitvector.BV06);
        /// <summary>
        /// Can't cast spells.
        /// </summary>
        public static readonly Bitvector ROOM_NO_MAGIC = new Bitvector(0, Bitvector.BV07);
        /// <summary>
        /// Tight, cramped room.
        /// </summary>
        public static readonly Bitvector ROOM_TUNNEL = new Bitvector(0, Bitvector.BV08);
        /// <summary>
        /// Only two people can be in this room.
        /// </summary>
        public static readonly Bitvector ROOM_PRIVATE = new Bitvector(0, Bitvector.BV09);
        // Bitvector.BV10 unused.
        /// <summary>
        /// Cannot fight in this room.
        /// </summary>
        public static readonly Bitvector ROOM_SAFE = new Bitvector(0, Bitvector.BV11);
        /// <summary>
        /// It won't rain here (no weather).
        /// </summary>
        public static readonly Bitvector ROOM_NO_PRECIP = new Bitvector(0, Bitvector.BV12);
        /// <summary>
        /// Can only travel in single-file in this room.
        /// </summary>
        public static readonly Bitvector ROOM_SINGLE_FILE = new Bitvector(0, Bitvector.BV13);
        /// <summary>
        /// Prison cell.
        /// </summary>
        public static readonly Bitvector ROOM_JAIL = new Bitvector(0, Bitvector.BV14);
        /// <summary>
        /// Cannot teleport to or from this room.
        /// </summary>
        public static readonly Bitvector ROOM_NO_TELEPORT = new Bitvector(0, Bitvector.BV15);
        /// <summary>
        /// Only one person can be in this room.
        /// </summary>
        public static readonly Bitvector ROOM_SOLITARY = new Bitvector(0, Bitvector.BV16);
        /// <summary>
        /// You heal faster in this room.
        /// </summary>
        public static readonly Bitvector ROOM_HEAL = new Bitvector(0, Bitvector.BV17);
        /// <summary>
        /// You don't heal at all in this room.
        /// </summary>
        public static readonly Bitvector ROOM_NO_HEAL = new Bitvector(0, Bitvector.BV18);
        /// <summary>
        /// You can log out of the game (rent) in this room.
        /// </summary>
        public static readonly Bitvector ROOM_INN = new Bitvector(0, Bitvector.BV19);
        /// <summary>
        /// You can dock a ship here.
        /// </summary>
        public static readonly Bitvector ROOM_DOCKABLE = new Bitvector(0, Bitvector.BV20);
        /// <summary>
        /// Magical darkness, torches useless.
        /// </summary>
        public static readonly Bitvector ROOM_MAGICDARK = new Bitvector(0, Bitvector.BV21);
        /// <summary>
        /// Magical light.
        /// </summary>
        public static readonly Bitvector ROOM_MAGICLIGHT = new Bitvector(0, Bitvector.BV22);
        /// <summary>
        /// Cannot summon anything into or out of the room.
        /// </summary>
        public static readonly Bitvector ROOM_NO_SUMMON = new Bitvector(0, Bitvector.BV23);
        /// <summary>
        /// Room is part of a guildhall.
        /// </summary>
        public static readonly Bitvector ROOM_GUILDROOM = new Bitvector(0, Bitvector.BV24);
        /// <summary>
        /// Room is twilight - everyone can see here regardless of vision type.
        /// </summary>
        public static readonly Bitvector ROOM_TWILIGHT = new Bitvector(0, Bitvector.BV25);
        /// <summary>
        /// Cannot use psionic powers.
        /// </summary>
        public static readonly Bitvector ROOM_NO_PSIONICS = new Bitvector(0, Bitvector.BV26);
        /// <summary>
        /// Cannot create gates or portals to/from this room.
        /// </summary>
        public static readonly Bitvector ROOM_NO_GATE = new Bitvector(0, Bitvector.BV27);
        /// <summary>
        /// The room is a bank. You can deposit and withdraw coins here.
        /// </summary>
        public static readonly Bitvector ROOM_BANK = new Bitvector(0, Bitvector.BV28);
        /// <summary>
        /// Room is a pet shop.
        /// </summary>
        public static readonly Bitvector ROOM_PET_SHOP = new Bitvector(0, Bitvector.BV29);
        /// <summary>
        /// You cannot scan into this room.
        /// </summary>
        public static readonly Bitvector ROOM_NO_SCAN = new Bitvector(0, Bitvector.BV30);
        /// <summary>
        /// Room is covered in an earthen starshell.
        /// </summary>
        public static readonly Bitvector ROOM_EARTHEN_STARSHELL = new Bitvector(1, Bitvector.BV00);
        /// <summary>
        /// Room is covered in an airy starshell.
        /// </summary>
        public static readonly Bitvector ROOM_AIRY_STARSHELL = new Bitvector(1, Bitvector.BV01);
        /// <summary>
        /// Room is covered in a fiery starshell.
        /// </summary>
        public static readonly Bitvector ROOM_FIERY_STARSHELL = new Bitvector(1, Bitvector.BV02);
        /// <summary>
        /// Room is covered in a watery starshell.
        /// </summary>
        public static readonly Bitvector ROOM_WATERY_STARSHELL = new Bitvector(1, Bitvector.BV03);
        /// <summary>
        /// There is a hypnotic pattern present.
        /// </summary>
        public static readonly Bitvector ROOM_HYPNOTIC_PATTERN = new Bitvector(1, Bitvector.BV04);

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RoomTemplate()
        {
            this.Description = "(no description)";
            this.Title = "(no room title)";
            ++_numRooms;
        }

        /// <summary>
        /// Copy constructor.  Copies room data but does not copy exit data.
        /// </summary>
        /// <param name="room"></param>
        public RoomTemplate(RoomTemplate room)
        {
            this._controlledBy = room._controlledBy;
            this.Area = room.Area;
            this.BaseRoomFlags = new int[room.BaseRoomFlags.Length];
            room.BaseRoomFlags.CopyTo(this.BaseRoomFlags, 0);
            this.Current = room.Current;
            this.CurrentDirection = room.CurrentDirection;
            this.CurrentRoomFlags = new int[room.CurrentRoomFlags.Length];
            room.CurrentRoomFlags.CopyTo(this.CurrentRoomFlags, 0);
            this.Description = room.Description;
            this.ExitData = new Exit[Limits.MAX_DIRECTION];
            this.ExtraDescriptions = new List<ExtendedDescription>(room.ExtraDescriptions);
            this.FallChance = room.FallChance;
            this.IndexNumber = Area.HighRoomIndexNumber + 1;
            this.Light = room.Light;
            this.TerrainType = room.TerrainType;
            this.Title = room.Title;
            this.WorldmapTerrainType = room.WorldmapTerrainType;
            ++_numRooms;
        }
        
        /// <summary>
        /// Destructor. Decrements in-memory room template count.
        /// </summary>
        ~RoomTemplate()
        {
            --_numRooms;
        }

        /// <summary>
        /// Exit information for the room.  Represents passages into and out of the room,
        /// which may or may not be doors.
        /// </summary>
        public Exit[] ExitData
        {
            get { return _exitData; }
            set { _exitData = value; }
        }

        /// <summary>
        /// The light level of the room.  Not saved -- handled at runtime.
        /// </summary>
        [XmlIgnore]
        public int Light { get; set; }

        /// <summary>
        /// The terrain type for this room.
        /// </summary>
        public TerrainType TerrainType { get; set; }

        /// <summary>
        /// The current strength in this room.
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// The area that contains this room.
        /// </summary>
        [XmlIgnore]
        public Area Area { get; set; }

        /// <summary>
        /// The virtual number for this room.
        /// </summary>
        public int IndexNumber { get; set; }

        /// <summary>
        /// The current room flags for this room.
        /// </summary>
        public int[] CurrentRoomFlags
        {
            get { return _currentRoomFlags; }
            set { _currentRoomFlags = value; }
        }

        /// <summary>
        /// The current direction of this room.
        /// </summary>
        public int CurrentDirection { get; set; }

        /// <summary>
        /// The chance of falling when standing in this room.
        /// </summary>
        public int FallChance { get; set; }

        /// <summary>
        /// The world map terrain type for this room.
        /// </summary>
        public int WorldmapTerrainType { get; set; }

        /// <summary>
        /// The base flags for this room.
        /// </summary>
        public int[] BaseRoomFlags
        {
            get { return _baseRoomFlags; }
            set { _baseRoomFlags = value; }
        }

        /// <summary>
        /// The extra text descriptions of various things inside the room.
        /// </summary>
        public List<ExtendedDescription> ExtraDescriptions
        {
            get { return _extraDescriptions; }
            set { _extraDescriptions = value; }
        }

        /// <summary>
        /// The title (heading) for the room.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The long description of the room.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lets us use the NOT operator to check the room for NULL.
        /// </summary>
        /// <param name="ri"></param>
        /// <returns></returns>
        public static bool operator !(RoomTemplate ri)
        {
            if (ri == null)
                return true;
            return false;
        }

        /// <summary>
        /// Lets us use an if(Room) to check for NULL.
        /// </summary>
        /// <param name="ri"></param>
        /// <returns></returns>
        public static implicit operator bool(RoomTemplate ri)
        {
            if (ri == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the total number of room templates in existence.
        /// </summary>
        public static int Count
        {
            get
            {
                return _numRooms;
            }
        }

        /// <summary>
        /// Gets the room as a string.  Displays the title, description, and a list of items and
        /// creatures in the room.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Title);
            sb.Append("\r\n");
            sb.Append(Description);
            sb.Append("\r\n");
            sb.Append("Exits: ");
            for (int i = 0; i < Limits.MAX_DIRECTION; i++ )
            {
                if (ExitData[i] != null)
                {
                    sb.Append(Exit.DirectionName[i] + " ");
                }
            }
            sb.Append("\r\n");
            return sb.ToString();
        }
    }
}
