using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Data which only PCs have.
    /// </summary>
    [Serializable]
    public class PC : CharData
    {
        public static int NumPc { get; private set; }
        // Consent list does not save -- it's built at runtime.
        private readonly List<CharData> _consenting = new List<CharData>();
        // Ignore list does not save -- it's built at runtime.
        private readonly List<CharData> _ignoring = new List<CharData>();

        [XmlIgnore]
        public CharData Guarding { get; set; }
        private List<MemorizeData> _memorized = new List<MemorizeData>();
        public string Password { get; set; }
        public string NewPassword { get; set; }
        /// <summary>
        /// Title, displayed on the "who list".
        /// </summary>
        public string Title { get; set; }
        public string Prompt { get; set; }
        public double Score { get; set; }
        /// <summary>
        /// Thirst level. 0 = dehydrating.
        /// </summary>
        public int Thirst { get; set; }
        /// <summary>
        /// Hungry level. 0 = starving.
        /// </summary>
        public int Hunger { get; set; }
        /// <summary>
        /// Drunk level, 0 = sober.
        /// </summary>
        public int Drunk { get; set; }
        public int PageLength { get; set; }
        public ImmortalData ImmortalData { get; set; }
        public SerializableDictionary<String, Int32> MonkAptitude; // monk-mystic stuff
        public SerializableDictionary<String, Int32> SpellAptitude;
        public SerializableDictionary<String, Int32> SkillAptitude;
        public SerializableDictionary<String, Int32> SongAptitude;
        /// <summary>
        /// Ability levels with the different racial languages.
        /// </summary>
        public int[] LanguageAptitude = new int[Race.MAX_LANG];
        /// <summary>
        /// Language currently being spoken.
        /// </summary>
        public Race.Language Speaking { get; set; }
        private List<AliasData> _aliases = new List<AliasData>();
        public TrophyData[] TrophyData { get; set; }
        public int Security { get; set; }
        public Guild Clan { get; set; }
        public int HitpointModifier { get; set; }
        public Guild.Rank ClanRank { get; set; }
        public int Train { get; set; }
        public int Frags { get; set; }
        public int PlayerKills { get; set; }
        public int PlayerDeaths { get; set; }
        public int MobKills { get; set; }
        public int MobDeaths { get; set; }
        public int FirstaidTimer { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime Birthdate { get; set; }
        public int OriginalHome { get; set; }
        public int CurrentHome { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int RaceStrMod { get; set; }
        public int RaceIntMod { get; set; }
        public int RaceWisMod { get; set; }
        public int RaceDexMod { get; set; }
        public int RaceConMod { get; set; }
        public int RaceAgiMod { get; set; }
        public int RaceChaMod { get; set; }
        public int RacePowMod { get; set; }
        public int RaceLukMod { get; set; }
        public int MaxStrMod { get; set; }
        public int MaxIntMod { get; set; }
        public int MaxWisMod { get; set; }
        public int MaxDexMod { get; set; }
        public int MaxConMod { get; set; }
        public int MaxAgiMod { get; set; }
        public int MaxChaMod { get; set; }
        public int MaxPowMod { get; set; }
        public int MaxLukMod { get; set; }
        public Coins Bank { get; set; }
        public int AggressiveLevel { get; set; }
        public int Tradition { get; set; }
        public int SkillPoints { get; set; }
        public int Chi { get; set; }
        public int MaxChi { get; set; }
        /// <summary>
        /// monk-mystic stuff
        /// </summary>
        public int MonkRestriction { get; set; }
        /// <summary>
        /// monk-mystic stuff
        /// </summary>
        public string Stance { get; set; }
        /// <summary>
        /// IndexNumber of room last rented or camped in.
        /// </summary>
        public int LastRentLocation { get; set; }
        /// <summary>
        /// Number of hitpoints lost last level, always the maximum.
        /// </summary>
        public int LostHp { get; set; }
        private List<InnateTimerData> _innateTimers = new List<InnateTimerData>();
        public bool IsWieldingTwohanded { get; set; }
        public SocketConnection.EditState Editing { get; set; }
        public string Editbuf { get; set; }
        // Faction standings with individual races.  This is a "how they feel about us" value.
        private double[] _raceFaction = new double[Limits.MAX_RACE];
        public ItemStatus Created { get; set; }
        public ItemStatus Destroyed { get; set; }
        public List<Message> PlayerNotes { get; set; }

        // Action bits for players.  Used primarily for configuration, terminal settings,
        // and player-specific state data.  Not used for affects and such.
        public static readonly Bitvector PLAYER_NONE = new Bitvector(0, 0);
        public static readonly Bitvector PLAYER_AUTOWRAP = new Bitvector(0, Bitvector.BV00);
        public static readonly Bitvector PLAYER_SHOUT = new Bitvector(0, Bitvector.BV01);
        public static readonly Bitvector PLAYER_CAMPING = new Bitvector(0, Bitvector.BV02);
        public static readonly Bitvector PLAYER_IS_NPC = new Bitvector(0, Bitvector.BV03);
        public static readonly Bitvector PLAYER_MEMORIZING = new Bitvector(0, Bitvector.BV04);
        public static readonly Bitvector PLAYER_MEDITATING = new Bitvector(0, Bitvector.BV05);
        public static readonly Bitvector PLAYER_BLANK = new Bitvector(0, Bitvector.BV06);
        public static readonly Bitvector PLAYER_BRIEF = new Bitvector(0, Bitvector.BV07);
        public static readonly Bitvector PLAYER_VICIOUS = new Bitvector(0, Bitvector.BV08);
        public static readonly Bitvector PLAYER_COMBINE = new Bitvector(0, Bitvector.BV09);
        public static readonly Bitvector PLAYER_PROMPT = new Bitvector(0, Bitvector.BV10);
        public static readonly Bitvector PLAYER_TELNET_GA = new Bitvector(0, Bitvector.BV11);
        public static readonly Bitvector PLAYER_GODMODE = new Bitvector(0, Bitvector.BV12);
        public static readonly Bitvector PLAYER_WIZINVIS = new Bitvector(0, Bitvector.BV13);
        public static readonly Bitvector PLAYER_WIZBIT = new Bitvector(0, Bitvector.BV14);
        public static readonly Bitvector PLAYER_SILENCE = new Bitvector(0, Bitvector.BV15);
        public static readonly Bitvector PLAYER_NO_EMOTE = new Bitvector(0, Bitvector.BV16);
        public static readonly Bitvector PLAYER_MOVED = new Bitvector(0, Bitvector.BV17);
        public static readonly Bitvector PLAYER_TELL = new Bitvector(0, Bitvector.BV18);
        public static readonly Bitvector PLAYER_LOG = new Bitvector(0, Bitvector.BV19);
        public static readonly Bitvector PLAYER_DENY = new Bitvector(0, Bitvector.BV20);
        public static readonly Bitvector PLAYER_FREEZE = new Bitvector(0, Bitvector.BV21);
        public static readonly Bitvector PLAYER_COLOR_CON = new Bitvector(0, Bitvector.BV22);
        public static readonly Bitvector PLAYER_MAP = new Bitvector(0, Bitvector.BV23);
        public static readonly Bitvector PLAYER_CAST_TICK = new Bitvector(0, Bitvector.BV24);
        public static readonly Bitvector PLAYER_AFK = new Bitvector(0, Bitvector.BV25);
        public static readonly Bitvector PLAYER_COLOR = new Bitvector(0, Bitvector.BV26);
        public static readonly Bitvector PLAYER_JUST_DIED = new Bitvector(0, Bitvector.BV27);
        public static readonly Bitvector PLAYER_PAGER = new Bitvector(0, Bitvector.BV28);
        public static readonly Bitvector PLAYER_MOUNTABLE = new Bitvector(0, Bitvector.BV29);
        public static readonly Bitvector PLAYER_FOG = new Bitvector(0, Bitvector.BV30);
        public static readonly Bitvector PLAYER_MSP = new Bitvector(1, Bitvector.BV00);
        public static readonly Bitvector PLAYER_BOTTING = new Bitvector(1, Bitvector.BV01);

        /// <summary>
        /// Initialize a new PC with default values.
        /// </summary>
        public PC()
        {
            ++NumPc;
            Prompt = "&+g<%hhp %mm %vmv>\r\n<&n%D %B&+g>&n ";
            Score = 0.0;
            PageLength = 25;
            IsSwitched = false;
            Speaking = 0;
            _actionFlags[0] = PLAYER_CAST_TICK.Vector | PLAYER_TELL.Vector | PLAYER_SHOUT.Vector |
                PLAYER_PROMPT.Vector | PLAYER_COMBINE.Vector | PLAYER_MAP.Vector |
                PLAYER_PAGER.Vector | PLAYER_AUTOWRAP.Vector | PLAYER_COLOR.Vector | PLAYER_VICIOUS.Vector;
            HitpointModifier = 0;
            ClanRank = 0;
            Train = 0;
            Frags = 0;
            PlayerKills = 0;
            PlayerDeaths = 0;
            MobKills = 0;
            MobDeaths = 0;
            FirstaidTimer = 0;
            CreationTime = new DateTime(); // Creation time, actual.
            Created = new ItemStatus();
            Destroyed = new ItemStatus();
            Birthdate = new DateTime(); // Creation time used for age calculations.  Unlike creation time, it can change.
            OriginalHome = StaticRooms.GetRoomNumber("ROOM_NUMBER_START");
            CurrentHome = StaticRooms.GetRoomNumber("ROOM_NUMBER_START");
            Height = 60 + MUDMath.Dice( 2, 10 );
            Weight = 15 + ( Height * 2 );
            RaceStrMod = 0;
            RaceIntMod = 0;
            RaceWisMod = 0;
            RaceDexMod = 0;
            RaceConMod = 0;
            RaceAgiMod = 0;
            RaceChaMod = 0;
            RacePowMod = 0;
            RaceLukMod = 0;
            MaxStrMod = 0;
            MaxIntMod = 0;
            MaxWisMod = 0;
            MaxDexMod = 0;
            MaxConMod = 0;
            MaxAgiMod = 0;
            MaxChaMod = 0;
            MaxPowMod = 0;
            MaxLukMod = 0;
            AggressiveLevel = -1;
            Tradition = 0;
            SkillPoints = 0;
            Chi = 0;
            MaxChi = 0;
            Stance = String.Empty;
            LastRentLocation = 0;
            LostHp = 0;
            MonkRestriction = 0;
            IsWieldingTwohanded = false;
            Editing = SocketConnection.EditState.none;
            Security = 0;
            int count;

            SpellAptitude = new SerializableDictionary<String, Int32>();
            SkillAptitude = new SerializableDictionary<String, Int32>();
            SongAptitude = new SerializableDictionary<String, Int32>();
            MonkAptitude = new SerializableDictionary<String, Int32>();
           
            LanguageAptitude = new int[Race.MAX_LANG];
            for (count = 0; count < Race.MAX_LANG; ++count)
            {
                LanguageAptitude[ count ] = 0;
            }
            TrophyData = new TrophyData[ Limits.MAX_LEVEL ];
            for( count = 0; count < Limits.MAX_LEVEL; ++count )
            {
                TrophyData[ count ] = new TrophyData();
                TrophyData[ count ].MobIndexNumber = 0;
                TrophyData[ count ].NumberKilled = 0;
            }
            Thirst = 48;
            Hunger = 48;
            Drunk = 0;
            Bank = new Coins();
            Bank.Copper = 0;
            Bank.Silver = 0;
            Bank.Gold = 0;
            Bank.Platinum = 0;
        }

        /// <summary>
        /// Destructor. Decrements the in-memory count of PCs.
        /// </summary>
        ~PC()
        {
            --NumPc;
        }

        /// <summary>
        /// Gets the number of PCs currently in memory.
        /// </summary>
        new public static int Count
        {
            get
            {
                return NumPc;
            }
        }

        /// <summary>
        /// Gets or sets the coin data for the PC.
        /// </summary>
        public Coins Money
        {
            get { return _money; }
            set { _money = value; }
        }

        /// <summary>
        /// Gets or sets the PC's spell memorization list.
        /// </summary>
        public List<MemorizeData> Memorized
        {
            get { return _memorized; }
            set { _memorized = value; }
        }

        public List<CharData> Consenting
        {
            get { return _consenting; }
        }

        public List<AliasData> Aliases
        {
            get { return _aliases; }
            set { _aliases = value; }
        }

        public double[] RaceFaction
        {
            get { return _raceFaction; }
            set { _raceFaction = value; }
        }

        public List<InnateTimerData> InnateTimers
        {
            get { return _innateTimers; }
            set { _innateTimers = value; }
        }

        public override bool IsIgnoring( CharData victim )
        {
            return _ignoring.Contains( victim );
        }

        public override bool IsConsenting( CharData victim )
        {
            // Check whether the victim's consent list contains the character.
            return ( Consenting.Contains( victim ) );
        }

        public override void StartConsenting( CharData victim )
        {
            if( !Consenting.Contains( victim ) )
                Consenting.Add( victim );
        }

        public override void StartIgnoring( CharData victim )
        {
            if( !_ignoring.Contains( victim ) )
                _ignoring.Add( victim );
        }

        public override void StopConsenting( CharData victim )
        {
            if( Consenting.Contains( victim ) )
                Consenting.Remove( victim );
        }

        public override void StopIgnoring( CharData victim )
        {
            if( _ignoring.Contains( victim ) )
                _ignoring.Remove( victim );
        }

        public static BitvectorFlagType[] PlayerActFlags = 
        {
            new BitvectorFlagType( "autowrap", PLAYER_AUTOWRAP, true ),
            new BitvectorFlagType( "pager", PLAYER_PAGER, true ),
            new BitvectorFlagType( "tell", PLAYER_TELL, true ),
            new BitvectorFlagType( "log", PLAYER_LOG, true ),
            new BitvectorFlagType( "deny", PLAYER_DENY, true ),
            new BitvectorFlagType( "camping", PLAYER_CAMPING, true ),
            new BitvectorFlagType( "prompt", PLAYER_PROMPT, true ),
            new BitvectorFlagType( "blank", PLAYER_BLANK, true ),
            new BitvectorFlagType( "combine", PLAYER_COMBINE, true ),
            new BitvectorFlagType( "shout", PLAYER_SHOUT, true ),
            new BitvectorFlagType( "silence", PLAYER_SILENCE, true ),
            new BitvectorFlagType( "noemote", PLAYER_NO_EMOTE, true ),
            new BitvectorFlagType( "moved", PLAYER_MOVED, true ),
            new BitvectorFlagType( "brief", PLAYER_BRIEF, true ),
            new BitvectorFlagType( "wizinvis", PLAYER_WIZINVIS, true ),
            new BitvectorFlagType( "wizbit", PLAYER_WIZBIT, true ),
            new BitvectorFlagType( "vicious", PLAYER_VICIOUS, true ),
            new BitvectorFlagType( "godmode", PLAYER_GODMODE, true ),
            new BitvectorFlagType( "memorizing", PLAYER_MEMORIZING, true ),
            new BitvectorFlagType( "meditating", PLAYER_MEDITATING, true ),
            new BitvectorFlagType( "just_died", PLAYER_JUST_DIED, true ),
            new BitvectorFlagType( "telnet_ga", PLAYER_TELNET_GA, true ),
            new BitvectorFlagType( "map", PLAYER_MAP, true ),
            new BitvectorFlagType( "frozen", PLAYER_FREEZE, true ),
            new BitvectorFlagType( "colorcon", PLAYER_COLOR_CON, true ),
            new BitvectorFlagType( "fog", PLAYER_FOG, true ),
            new BitvectorFlagType( "casttick", PLAYER_CAST_TICK, true ),
            new BitvectorFlagType( "afk", PLAYER_AFK, true ),
            new BitvectorFlagType( "color", PLAYER_COLOR, true ),
            new BitvectorFlagType( "mountable", PLAYER_MOUNTABLE, true ),
            new BitvectorFlagType( "is_npc", PLAYER_IS_NPC, true ),
            new BitvectorFlagType( "msp", PLAYER_MSP, true ),
            new BitvectorFlagType( String.Empty, null, false )
        };

        /// <summary>
        /// Returns the act flags for a player or mob as a string.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="mortal"></param>
        /// <param name="npc"></param>
        /// <returns></returns>
        public static string ActString(int[] vector, bool mortal, bool npc)
        {
            string text = String.Empty;

            if (!npc)
            {
                int count;
                for (count = 0; PlayerActFlags[count].BitvectorData; count++)
                {
                    if (Macros.IsSet(vector[(PlayerActFlags[count].BitvectorData.Group)], PlayerActFlags[count].BitvectorData.Vector))
                    {
                        text += " ";
                        text += PlayerActFlags[count].Name;
                    }
                }
            }
            else
            {
                foreach( BitvectorFlagType bft in BitvectorFlagType.MobActFlags )
                {
                    if (Macros.IsSet( vector[bft.BitvectorData.Group], bft.BitvectorData.Vector))
                    {
                        text += " ";
                        text += bft.Name;
                    }
                }
            }

            return (!String.IsNullOrEmpty(text)) ? text.Substring(1) : "none";
        }

        /// <summary>
        /// Loads a player file and returns a PC object populated with the data.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        new public static PC LoadFile( string filename )
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PC));
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                PC data = (PC)serializer.Deserialize(stream);
                stream.Close();
                // Fix up any data references that can't be saved.
                foreach (Object obj in data._carrying)
                {
                    obj.CarriedBy = data;
                    if((obj.ObjIndexData = Database.GetObjTemplate(obj.ObjIndexNumber)) == null)
                    {
                        Log.Error("Object index data not found for object " + obj.ObjIndexNumber + " (" + obj.Name + ") on player " + filename + ".");
                    }
                }
                data.RemoveActBit(PLAYER_CAMPING);
                if (data._level >= Limits.LEVEL_AVATAR && data.ImmortalData == null)
                {
                    data.ImmortalData = new ImmortalData();
                }
                // Extend affect vectors if the number has been increased.
                if (data._affectedBy.Length < Limits.NUM_AFFECT_VECTORS)
                {
                    data.ExtendAffectData();
                }
                return data;
            }
            catch (FileNotFoundException)
            {
                Log.Trace("Player file not found.");
                return null;
            }
            catch (Exception ex)
            {
                Log.Trace("Unable to load player file: " + filename + ".  This may be a new player.  Exception is:" + ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the player's faction standing with the victim.  Uses race, clan, and player standings in calculation.
        /// </summary>
        /// <param name="victim"></param>
        /// <returns></returns>
        public override double GetFaction(CharData victim)
        {
            int race = victim.GetOrigRace();
            return GetFaction(race);
        }

        /// <summary>
        /// Gets the player's faction standing with the victim.  Uses race, clan, and player standings in calculation.
        /// </summary>
        /// <param name="race"></param>
        /// <returns></returns>
        public override double GetFaction( int race )
        {
            if (Clan != null)
            {
                return ((_raceFaction[race] * 3) + (Clan.RaceFactionStandings[race] * 2) + Race.RaceList[GetOrigRace()].RaceFaction[race]) / 6.0;
            }
            return ((_raceFaction[race] * 3) + Race.RaceList[GetOrigRace()].RaceFaction[race]) / 4.0;
        }

        /// <summary>
        /// Provides an override to get the character's current repop point, or the default repop
        /// point if they don't have a current home set.
        /// </summary>
        /// <returns></returns>
        public override Room GetRepopPoint()
        {
            if (CurrentHome != 0)
            {
                return Room.GetRoom(CurrentHome);
            }
            return base.GetRepopPoint();
        }

        /// <summary>
        /// Saves a player file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public override bool SaveFile(string filename)
        {
            // Store the room when we save so we can come back to the same place.
            if (_inRoom != null)
            {
                LastRentLocation = _inRoom.IndexNumber;
            }
            return base.SaveFile(filename);
        }

    }
}