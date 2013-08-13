using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace ModernMUD
{
    /// <summary>
    /// Represents an entire zone/area.
    /// </summary>
    [Serializable]
    public class Area
    {
        private static int _count;
        private List<Reset> _resets = new List<Reset>();
        private List<RoomTemplate> _rooms = new List<RoomTemplate>();
        private List<ObjTemplate> _objects = new List<ObjTemplate>();
        private List<MobTemplate> _mobs = new List<MobTemplate>();
        private List<Shop> _shops = new List<Shop>();
        private List<RepopulationPoint> _repops = new List<RepopulationPoint>();
        private List<QuestTemplate> _quests = new List<QuestTemplate>();
        private int _version;
        private string _name;
        private int _defenderTemplateNumber; // index number of defending mobs for invasion
        private int _defendersPerSquad; // number of defenders that spawn
        private int _barracksRoom; // index number of room where guards spawn, if not given guards will spawn in judge room
        private int _judgeRoom; // index number of room in which judge presides
        private int _ageInSeconds;
        private int _numPlayers;
        private JusticeType _justiceType;
        private string _filename;
        private string _builders;
        private int _security;
        private int[] _areaFlags = new int[Limits.NUM_AREA_FLAGS];
        private int _minRecommendedLevel;
        private int _maxRecommendedLevel;
        private string _author;
        private string _resetMessage;
        private int _timesReset;
        private ResetMode _resetMode;
        private int _minutesBetweenResets;
        private int _clanId;
        private int _recall;
        private int _jailRoom;
        private int _defenderSquads;
        private int _defendersDispatched;
        private int _width;
        private int _height;
        private int _lowRoomIndexNumber;
        private int _highRoomIndexNumber;
        private int _lowMobIndexNumber;
        private int _highMobIndexNumber;
        private int _lowObjIndexNumber;
        private int _highObjIndexNumber;
        /// <summary>
        /// Used to track the version of the zone data assembly that the area was built with.
        /// </summary>
        public double ZoneDataVersion { get; set; }
        /// <summary>
        /// Used to track the version of the editor that the area was last saved with.
        /// </summary>
        public double EditorVersion { get; set; }

        /// <summary>
        /// The conditions under which an area will reset.
        /// </summary>
        public enum ResetMode
        {
            /// <summary>
            /// Reset normally.
            /// </summary>
            normal = 0,
            /// <summary>
            /// Reset only when all quests have been completed.
            /// </summary>
            all_quests_completed,
            /// <summary>
            /// Reset only when the area is empty of mobs.
            /// </summary>
            empty_of_mobiles,
            /// <summary>
            /// Reset only when the area is empty of objects.
            /// </summary>
            empty_of_objects,
            /// <summary>
            /// Reset only when the area is empty of players.
            /// </summary>
            empty_of_players,
            /// <summary>
            /// Never reset the area, only at boot time.
            /// </summary>
            never
        }

        // Area flags.
        /// <summary>
        /// No flags.
        /// </summary>
        public static readonly Bitvector AREA_NONE = new Bitvector(0, 0);
        /// <summary>
        /// Cannot use dimension door and similar spells.
        /// </summary>
        public static readonly Bitvector AREA_NO_DIMDOOR = new Bitvector(0, Bitvector.BV00);
        /// <summary>
        /// Cannot cast gate/portal spells.
        /// </summary>
        public static readonly Bitvector AREA_NO_GATE = new Bitvector(0, Bitvector.BV01);
        /// <summary>
        /// Area is part of the surface world map.
        /// </summary>
        public static readonly Bitvector AREA_WORLDMAP = new Bitvector(0, Bitvector.BV02);
        /// <summary>
        /// Area is underground and has no sky.
        /// </summary>
        public static readonly Bitvector AREA_UNDERGROUND = new Bitvector(0, Bitvector.BV03);

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Area()
        {
            ++_count;
            _recall = -1;
            for (int i = 0; i < Limits.NUM_AREA_FLAGS; i++)
            {
                _areaFlags[i] = 0;
            }
            _security = 1;
            _ageInSeconds = 0;
            _numPlayers = 0;
            _version = 0;
            _height = 0;
            _width = 0;
            _defenderTemplateNumber = 0;
            _defendersPerSquad = 0;
            _barracksRoom = 0;
            _judgeRoom = 0;
            _jailRoom = 0;
            _defenderSquads = 0;
            _defendersDispatched = 0;
            _justiceType = 0;
            _minRecommendedLevel = 1;
            _maxRecommendedLevel = 40;
            _timesReset = 0;
            _resetMode = ResetMode.normal;
            _minutesBetweenResets = 15;
            _clanId = 0;
            _name = "New Area";
            _builders = "None";
            _author = "None";
            _resetMessage = "You hear some noises in the distance.";
            _filename = "area" + _count + ".are.xml";
        }

        /// <summary>
        /// Destructor, decrements in-memory count of objects of this type.
        /// </summary>
        ~Area()
        {
            --_count;
        }

        /// <summary>
        /// The version number of the area.
        /// </summary>
        public int Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// Number of times that the area has been reset.
        /// </summary>
        [XmlIgnore]
        public int TimesReset
        {
            get { return _timesReset; }
            set { _timesReset = value; }
        }

        /// <summary>
        /// Age of the area since last reset, in seconds.  Not saved -- handled at runtime.
        /// </summary>
        [XmlIgnore]
        public int AgeInSeconds
        {
            get { return _ageInSeconds; }
            set { _ageInSeconds = value; }
        }

        /// <summary>
        /// Height of the area.  Only matters for worldmap zones.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Width of the area.  Only matters for worldmap zones.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The number of defenders that have been dispatched.  While this is less than the
        /// number of defenders per squad times the number of squads, new defenders can be
        /// dispatched.
        /// </summary>
        public int NumDefendersDispatched
        {
            get { return _defendersDispatched; }
            set { _defendersDispatched = value; }
        }

        /// <summary>
        /// Number of players currently in the area.  Not saved -- determined at runtime.
        /// </summary>
        [XmlIgnore]
        public int NumPlayers
        {
            get { return _numPlayers; }
            set { _numPlayers = value; }
        }

        /// <summary>
        /// Minimum number of minutes between zone resets.
        /// </summary>
        public int MinutesBetweenResets
        {
            get { return _minutesBetweenResets; }
            set { _minutesBetweenResets = value; }
        }

        /// <summary>
        /// The clan ID associated with this zone, used for guild halls.
        /// </summary>
        public int ClanId
        {
            get { return _clanId; }
            set { _clanId = value; }
        }

        /// <summary>
        /// The index number of defender mobs for this area.
        /// </summary>
        public int DefenderTemplateNumber
        {
            get { return _defenderTemplateNumber; }
            set { _defenderTemplateNumber = value; }
        }

        /// <summary>
        /// The number of defenders in a squad when one is dispatched to deal with intruders.
        /// </summary>
        public int DefendersPerSquad
        {
            get { return _defendersPerSquad; }
            set { _defendersPerSquad = value; }
        }

        /// <summary>
        /// The index number where defenders spawn when they are dispatched.
        /// </summary>
        public int BarracksRoom
        {
            get { return _barracksRoom; }
            set { _barracksRoom = value; }
        }

        /// <summary>
        /// The index number where those accused of crimes are brought to trial.
        /// </summary>
        public int JudgeRoom
        {
            get { return _judgeRoom; }
            set { _judgeRoom = value; }
        }

        /// <summary>
        /// The location where those convicted of a crime are imprisoned.
        /// </summary>
        public int JailRoom
        {
            get { return _jailRoom; }
            set { _jailRoom = value; }
        }

        /// <summary>
        /// Number of defender squads that can be dispatched.
        /// </summary>
        public int DefenderSquads
        {
            get { return _defenderSquads; }
            set { _defenderSquads = value; }
        }

        /// <summary>
        /// Recall room for the area, which should be a "safe" room.
        /// </summary>
        public int Recall
        {
            get { return _recall; }
            set { _recall = value; }
        }

        /// <summary>
        /// The resets associated with this area.
        /// </summary>
        public List<Reset> Resets
        {
            get { return _resets; }
            set { _resets = value; }
        }

        /// <summary>
        /// The rooms in the area.
        /// </summary>
        public List<RoomTemplate> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; }
        }

        /// <summary>
        /// The objects associated with the area.
        /// </summary>
        public List<ObjTemplate> Objects
        {
            get { return _objects; }
            set { _objects = value; }
        }

        /// <summary>
        /// The mobiles associated with the area.
        /// </summary>
        public List<MobTemplate> Mobs
        {
            get { return _mobs; }
            set { _mobs = value; }
        }

        /// <summary>
        /// The shops associated with the area.
        /// </summary>
        public List<Shop> Shops
        {
            get { return _shops; }
            set { _shops = value; }
        }

        /// <summary>
        /// The repops (respawns) for the area.
        /// </summary>
        public List<RepopulationPoint> Repops
        {
            get { return _repops; }
            set { _repops = value; }
        }

        /// <summary>
        /// The quests associated with the area.
        /// </summary>
        public List<QuestTemplate> Quests
        {
            get { return _quests; }
            set { _quests = value; }
        }

        /// <summary>
        /// The security level of the area.
        /// </summary>
        public int Security
        {
            get { return _security; }
            set { _security = value; }
        }

        /// <summary>
        /// The flags associated with the area.
        /// </summary>
        public int[] AreaFlags
        {
            get { return _areaFlags; }
            set { _areaFlags = value; }
        }

        /// <summary>
        /// The way in which this area resets (preconditions).
        /// </summary>
        public ResetMode AreaResetMode
        {
            get { return _resetMode; }
            set { _resetMode = value; }
        }

        /// <summary>
        /// The builders for this zone.  The author wrote the original, and the builders have
        /// modified the zone over time.  When displaying zone credits to the public, it is
        /// the Author that should be shown.
        /// </summary>
        public string Builders
        {
            get { return _builders; }
            set { _builders = value; }
        }

        /// <summary>
        /// The message displayed to occupants when this zone is reset.
        /// </summary>
        public string ResetMessage
        {
            get { return _resetMessage; }
            set { _resetMessage = value; }
        }

        /// <summary>
        /// The minimum level recommended for entering this area.
        /// </summary>
        public int MinRecommendedLevel
        {
            get { return _minRecommendedLevel; }
            set { _minRecommendedLevel = value; }
        }

        /// <summary>
        /// The maximum level recommended for entering this area.
        /// </summary>
        public int MaxRecommendedLevel
        {
            get { return _maxRecommendedLevel; }
            set { _maxRecommendedLevel = value; }
        }

        /// <summary>
        /// The type of law and order enforced in this area.  Also reflects which side(s)
        /// of the racewars are allowed to enter.
        /// </summary>
        public JusticeType JusticeType
        {
            get { return _justiceType; }
            set { _justiceType = value; }
        }

        /// <summary>
        /// The on-disk storage location for this area.
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        /// <summary>
        /// The number of areas currently in memory.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// The name of the area.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The zone's original author.
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        /// <summary>
        /// Recalculates the index number ranges for an area.
        /// </summary>
        public void RebuildIndexes()
        {
            int lowIndexNumber = 0;
            int highIndexNumber = 0;
            if (_rooms.Count > 0)
            {
                lowIndexNumber = _rooms[0].IndexNumber;
                highIndexNumber = _rooms[0].IndexNumber;
                foreach (RoomTemplate room in _rooms)
                {
                    if (room.IndexNumber < lowIndexNumber)
                    {
                        lowIndexNumber = room.IndexNumber;
                    }
                    if (room.IndexNumber > highIndexNumber)
                    {
                        highIndexNumber = room.IndexNumber;
                    }
                }
            }
            _lowRoomIndexNumber = lowIndexNumber;
            _highRoomIndexNumber = highIndexNumber;
            lowIndexNumber = _rooms[0].IndexNumber;
            highIndexNumber = _rooms[0].IndexNumber;
            foreach (ObjTemplate obj in _objects)
            {
                if (obj.IndexNumber < lowIndexNumber)
                {
                    lowIndexNumber = obj.IndexNumber;
                }
                if (obj.IndexNumber > highIndexNumber)
                {
                    highIndexNumber = obj.IndexNumber;
                }
            }
            _lowObjIndexNumber = lowIndexNumber;
            _highObjIndexNumber = highIndexNumber;
            foreach(MobTemplate mob in _mobs)
            {
                if (mob.IndexNumber < lowIndexNumber)
                {
                    lowIndexNumber = mob.IndexNumber;
                }
                if (mob.IndexNumber > highIndexNumber)
                {
                    highIndexNumber = mob.IndexNumber;
                }
            }
            _lowMobIndexNumber = lowIndexNumber;
            _highMobIndexNumber = highIndexNumber;
        }

        /// <summary>
        /// The minimum room index number for the area.
        /// </summary>
        public int LowRoomIndexNumber
        {
            get
            {
                return _lowRoomIndexNumber;
            }
        }

        /// <summary>
        /// The maximum room index number for the area.
        /// </summary>
        public int HighRoomIndexNumber
        {
            get
            {
                return _highRoomIndexNumber;
            }
        }

        /// <summary>
        /// The minimum object index number for the area.
        /// </summary>
        public int LowObjIndexNumber
        {
            get
            {
                return _lowObjIndexNumber;
            }
        }

        /// <summary>
        /// The maximum object index number for the area.
        /// </summary>
        public int HighObjIndexNumber
        {
            get
            {
                return _highObjIndexNumber;
            }
        }

        /// <summary>
        /// The minimum mobile index number for the area.
        /// </summary>
        public int LowMobIndexNumber
        {
            get
            {
                return _lowMobIndexNumber;
            }
        }

        /// <summary>
        /// The maximum mobile index number for the area.
        /// </summary>
        public int HighMobIndexNumber
        {
            get
            {
                return _highMobIndexNumber;
            }
        }

        /// <summary>
        /// The lowest index number of any type for the area.
        /// </summary>
        public int LowIndexNumber
        {
            get
            {
                // Have to account for indexes being zero when there are none of a type in the area.
                int indexNumber = Int32.MaxValue;
                int i = LowMobIndexNumber;
                int j = LowObjIndexNumber;
                int k = LowRoomIndexNumber;
                if (i > 0 && i < indexNumber)
                {
                    indexNumber = i;
                }
                if (j > 0 && j < indexNumber)
                {
                    indexNumber = j;
                }
                if (k > 0 && k < indexNumber)
                {
                    indexNumber = k;
                }
                if (indexNumber < Int32.MaxValue)
                {
                    return indexNumber;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// The highest index number of any type for the area.
        /// </summary>
        public int HighIndexNumber
        {
            get
            {
                int indexNumber = Math.Max( HighMobIndexNumber, HighObjIndexNumber );
                indexNumber = Math.Max( indexNumber, HighRoomIndexNumber );
                return indexNumber;
            }
        }

        /// <summary>
        /// Creates and returns a new Area based on the filename.
        /// </summary>
        /// <param name="filename">The file to load from.</param>
        /// <returns>Area containing file data.</returns>
        public static Area Load( string filename )
        {
            if (String.IsNullOrEmpty(filename))
            {
                return null;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Area));
                XmlTextReader xtr = new XmlTextReader(new StreamReader(filename));
                Area newarea = (Area)serializer.Deserialize(xtr);
                xtr.Close();
                // Add area flags if they don't exist.
                if (newarea.AreaFlags.Length == 0)
                {
                    newarea.AreaFlags = new int[Limits.NUM_AREA_FLAGS];
                }
                // Set each mob's class since we don't save the actual class data.
                //
                // Set and validate other values in the mob data.
                //
                // Associate each mob with this area.
                foreach (MobTemplate mob in newarea._mobs)
                {
                    mob.Area = newarea;
                    if (mob.AffectedBy.Length < Limits.NUM_AFFECT_VECTORS)
                    {
                        mob.ExtendAffects();
                    }
                    if (!String.IsNullOrEmpty(mob.ClassName))
                    {
                        mob.CharacterClass = CharClass.ClassList[(int)CharClass.ClassLookup(mob.ClassName)];
                    }
                    //if( !String.IsNullOrEmpty( mob._chatterBotName ) )
                    //{
                    //    foreach (ChatterBot bot in Database.ChatterBotList)
                    //    {
                    //        if( bot._name == mob._chatterBotName )
                    //        {
                    //            mob._chatterBot = bot;
                    //            break;
                    //        }
                    //    }
                    //}
                    if (!String.IsNullOrEmpty(mob.SpecFunNames))
                    {
                        mob.SpecFun = MobSpecial.SpecMobLookup(mob.SpecFunNames);
                    }
                    if (!String.IsNullOrEmpty(mob.DeathFunName))
                        mob.DeathFun = MobSpecial.SpecMobLookup(mob.DeathFunName);
                }
                // Associate each room with this area.
                foreach (RoomTemplate room in newarea._rooms)
                {
                    room.Area = newarea;
                }
                // Associate each object with this area.
                foreach (ObjTemplate obj in newarea._objects)
                {
                    obj.Area = newarea;
                    obj.SetCost();
                    if (obj.AffectedBy.Length < Limits.NUM_AFFECT_VECTORS)
                    {
                        obj.ExtendAffects();
                    }
                }
                // Associate each shop with this area and bind it to its keeper.
                foreach (Shop shop in newarea._shops)
                {
                    shop.Area = newarea;
                    foreach (MobTemplate mob in newarea._mobs)
                    {
                        if (mob.IndexNumber == shop.Keeper)
                        {
                            mob.ShopData = shop;
                        }
                    }
                }
                // Add repops in this area to the global repop list.
                foreach (RepopulationPoint repop in newarea._repops)
                {
                    repop.Setup();
                    RepopulationPoint.RepopList.Add(repop);
                }
                newarea._filename = filename;
                newarea.RebuildIndexes();
                return newarea;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File NotFound loading area " + filename + ":" + ex);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception loading area " + filename + ":" + ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the version of the zone data as described by this DLL.
        /// </summary>
        /// <returns></returns>
        public static double GetVersion()
        {
            int minVer = typeof(Area).Assembly.GetName().Version.MinorRevision;
            int majVer = typeof(Area).Assembly.GetName().Version.Major;
            double ver = majVer + (minVer / 1000.0);
            return ver;
        }

        /// <summary>
        /// Saves an area to the stored filename.
        /// </summary>
        public bool Save()
        {
            if (String.IsNullOrEmpty(_filename))
            {
                throw new InvalidOperationException("Cannot save area: Filename is null or empty.");
            }

            // Set the file version of zone data.
            ZoneDataVersion = GetVersion();

            // Make sure that mob class names are accurate since we use them to set the mob's class at load time.
            //
            // Set other variables that are required at load-time.
            foreach( MobTemplate mob in _mobs )
            {
                mob.ClassName = mob.CharacterClass.Name;
                //if( mob._chatterBot != null )
                //{
                //    mob._chatterBotName = mob._chatterBot._name;
                //}
                // Even though this should already be built, we rebuild it just to ensure accuracy.
                if( mob.SpecFun.Count > 0 )
                {
                    mob.SpecFunNames = String.Empty;
                    foreach( MobSpecial spec in mob.SpecFun )
                    {
                        mob.SpecFunNames += spec.SpecName + " ";
                    }
                }
                if( mob.DeathFun != null )
                {
                    mob.DeathFunName = String.Empty;
                    foreach (MobSpecial spec in mob.DeathFun)
                    {
                        mob.DeathFunName += spec.SpecName + " ";
                    }
                }
            }

            XmlSerializer serializer = new XmlSerializer( GetType() );
            XmlTextWriter xtw = new XmlTextWriter(new StreamWriter(_filename));
            try
            {
                serializer.Serialize(xtw, this);
            }
            catch( Exception ex )
            {
                string error = ex.ToString();
                if (xtw != null)
                {
                    xtw.Close();
                }
                throw ex;
            }
            xtw.Flush();
            xtw.Close();
            return true;
        }

        /// <summary>
        /// Saves the area to a specific filename.
        /// </summary>
        /// <returns></returns>
        public bool Save(string filename)
        {
            _filename = filename;
            return Save();
        }

        /// <summary>
        /// Checks whether an area has a specific flag set.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool HasFlag(Bitvector bvect)
        {
            // Protect against index out of range exceptions.
            if (_areaFlags.Length <= bvect.Group)
                return false;
            if ((_areaFlags[bvect.Group] & bvect.Vector) != 0)
            {
                return true;
            }
            return false;
        }
    };
}
