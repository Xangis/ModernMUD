using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ModernMUD
{
    /// <summary>
    /// Represents a player or mob race.
    /// 
    /// Many of the constants used here are a legacy of the Basternae 2 area format.
    /// 
    /// TODO: Modify the constants and enumerations so that races are more dynamic and do not
    /// have to have a specific number assigned.
    /// </summary>
    public class Race
    {
        public static Race[] RaceList = new Race[Limits.MAX_RACE];
        private string _name;
        private string _key;
        private List<KickMessage> _kickMessages = new List<KickMessage>();
        private int[] _innateAbilities = new int[Limits.NUM_INNATE_VECTORS];
        private string _filename;
        // Faction standings with individual races.  This is a "how they feel about us" value.
        private double[] _raceFaction = new double[Limits.MAX_RACE];
        private int _number; // must match Race.RACE_ definitions.
        private int[] _saveModifiers = new int[6];
        private List<CustomAction> _customActions;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Race()
        {
            //kickmsg ;
            ClassesAvailable = CharClass.Available.none;
            _filename = String.Empty;
            ExperienceModifier = 1000;
            NaturalAC = 100;
            int count;
            for( count = 0; count < Limits.NUM_INNATE_VECTORS; ++count )
            {
                _innateAbilities[ count ] = 0;
            }
            DefaultSize = Size.medium;
            Height = 66;
            Weight = 140;
            BaseAlignment = 0;
            StrModifier = 0;
            IntModifier = 0;
            WisModifier = 0;
            DexModifier = 0;
            ConModifier = 0;
            AgiModifier = 0;
            ChaModifier = 0;
            PowModifier = 0;
            LukModifier = 0;
            NaturalAC = 100;
            HpGain = 1;
            ManaGain = 1;
            MoveGain = 1;
            ThirstModifier = 1;
            HungerModifier = 1;
            BodyParts = 0;
            Immune = 0;
            Susceptible = 0;
            Vulnerable = 0;
            Resistant = 0;
            PrimaryLanguage = 0;
            WarSide = RacewarSide.neutral;
            BaseAge = 18;
            MaxAge = new TimeSpan(Limits.TIMESPAN_GAME_YEAR.Ticks * 80);
            MagicResistance = 0;
            Coins = false;
            ExperienceModifier = 1.0;
            _number = 0;
            _saveModifiers[ 0 ] = 0;
            _saveModifiers[ 1 ] = 0;
            _saveModifiers[ 2 ] = 0;
            _saveModifiers[ 3 ] = 0;
            _saveModifiers[ 4 ] = 0;
            _saveModifiers[ 5 ] = 0;
        }

        /// <summary>
        /// Gets the number of races currently in memory.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return RaceList.Length;
            }
        }

        /// <summary>
        /// Custom race-based actions -- AI, etc.
        /// </summary>
        public List<CustomAction> CustomActions
        {
            get { return _customActions; }
            set { _customActions = value; }
        }

        /// <summary>
        /// The primary language for this race.
        /// </summary>
        public Language PrimaryLanguage { get; set; }

        /// <summary>
        /// The natural size of members of this race.
        /// </summary>
        public Size DefaultSize { get; set; }

        /// <summary>
        /// The id number of the race.
        /// </summary>
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        /// <summary>
        /// The side of the racewar this race is on.
        /// </summary>
        public RacewarSide WarSide { get; set; }

        /// <summary>
        /// How thirsty the race gets.
        /// </summary>
        public int ThirstModifier { get; set; }

        /// <summary>
        /// How hungry the race gets.
        /// </summary>
        public int HungerModifier { get; set; }

        /// <summary>
        /// The naked armor class for this race.
        /// </summary>
        public int NaturalAC { get; set; }

        /// <summary>
        /// The strength modifier for this race.
        /// </summary>
        public int StrModifier { get; set; }

        /// <summary>
        /// The intelligence modifier for this race.
        /// </summary>
        public int IntModifier { get; set; }

        /// <summary>
        /// The wisdom modifier for this race.
        /// </summary>
        public int WisModifier { get; set; }

        /// <summary>
        /// The dexterity modifier for this race.
        /// </summary>
        public int DexModifier { get; set; }

        /// <summary>
        /// The constitution modifier for this race.
        /// </summary>
        public int ConModifier { get; set; }

        /// <summary>
        /// The agility modifier for this race.
        /// </summary>
        public int AgiModifier { get; set; }

        /// <summary>
        /// The charisma modifier for this race.
        /// </summary>
        public int ChaModifier { get; set; }

        /// <summary>
        /// Power modifier for this race.
        /// </summary>
        public int PowModifier { get; set; }

        /// <summary>
        /// Luck modifier for this race.
        /// </summary>
        public int LukModifier { get; set; }

        /// <summary>
        /// The additional hp regen for this race.
        /// </summary>
        public int HpGain { get; set; }

        /// <summary>
        /// The additional mana regen for this race.
        /// </summary>
        public int ManaGain { get; set; }

        /// <summary>
        /// The additional movement point regen for members of this race.
        /// </summary>
        public int MoveGain { get; set; }

        /// <summary>
        /// The decayed name used for corpses of this race.
        /// </summary>
        public string DecayedName { get; set; }

        /// <summary>
        /// the key letter combination used for this race.
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// The messages displayed when this race kick an enemy.
        /// </summary>
        public List<KickMessage> KickMessages
        {
            get { return _kickMessages; }
            set { _kickMessages = value; }
        }

        /// <summary>
        /// The innate abilities that this race has.
        /// </summary>
        public int[] InnateAbilities
        {
            get { return _innateAbilities; }
            set { _innateAbilities = value; }
        }

        /// <summary>
        /// The average height of this race.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The average weight for this race.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// The base/innate alignment of this race.
        /// </summary>
        public int BaseAlignment { get; set; }

        /// <summary>
        /// The character classes available to this race.
        /// </summary>
        public CharClass.Available ClassesAvailable { get; set; }

        /// <summary>
        /// The damage verb used when this race is in barehanded combat.
        /// </summary>
        public string DamageMessage { get; set; }

        /// <summary>
        /// The verb displayed when this race walks into or out of a room.
        /// </summary>
        public string WalkMessage { get; set; }

        /// <summary>
        /// The list of races that this race has mortal hatred of.
        /// </summary>
        public string Hate { get; set; }

        /// <summary>
        /// The body parts that this race has.
        /// </summary>
        public Parts BodyParts { get; set; }

        /// <summary>
        /// The damage types that this race is resistant to.
        /// </summary>
        public DamageType Resistant { get; set; }

        /// <summary>
        /// The damage types that this race is immune to.
        /// </summary>
        public DamageType Immune { get; set; }

        /// <summary>
        /// The damage types that this race is susceptible to.
        /// </summary>
        public DamageType Susceptible { get; set; }

        /// <summary>
        /// The damage types that this race is vulnerable to.
        /// </summary>
        public DamageType Vulnerable { get; set; }

        /// <summary>
        /// The age at which this race starts adventuring.
        /// </summary>
        public int BaseAge { get; set; }

        /// <summary>
        /// The lifespan of this race.
        /// </summary>
        public TimeSpan MaxAge { get; set; }

        /// <summary>
        /// The level of magic resistance for this race.
        /// </summary>
        public int MagicResistance { get; set; }

        /// <summary>
        /// Does the race carry coins?
        /// </summary>
        public bool Coins { get; set; }

        /// <summary>
        /// How difficult is it for players of this race to gain levels?
        /// </summary>
        public double ExperienceModifier { get; set; }

        /// <summary>
        /// Saving throw modfiers for members of this race.
        /// </summary>
        public int[] SaveModifiers
        {
            get { return _saveModifiers; }
            set { _saveModifiers = value; }
        }

        /// <summary>
        /// The faction rankings that this race has among all the other races.
        /// </summary>
        public double[] RaceFaction
        {
            get { return _raceFaction; }
            set { _raceFaction = value; }
        }

        /// <summary>
        /// The location of the file containing information for this race.
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        /// <summary>
        /// The plain-text name of the race.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The ANSI-colorized name of the race.
        /// </summary>
        public string ColorName { get; set; }

        public static readonly Bitvector RACE_NO_ABILITIES = new Bitvector( 0, 0 );
        public static readonly Bitvector RACE_PC_AVAIL = new Bitvector( 0, Bitvector.BV00 );
        public static readonly Bitvector RACE_WATERBREATH = new Bitvector( 0, Bitvector.BV01 );
        public static readonly Bitvector RACE_FLY = new Bitvector(0, Bitvector.BV02);
        public static readonly Bitvector RACE_SWIM = new Bitvector(0, Bitvector.BV03);
        public static readonly Bitvector RACE_WATERWALK = new Bitvector(0, Bitvector.BV04);
        public static readonly Bitvector RACE_PASSDOOR = new Bitvector(0, Bitvector.BV05);
        public static readonly Bitvector RACE_INFRAVISION = new Bitvector(0, Bitvector.BV06);
        public static readonly Bitvector RACE_DETECT_ALIGN = new Bitvector(0, Bitvector.BV07);
        public static readonly Bitvector RACE_DETECT_INVIS = new Bitvector(0, Bitvector.BV08);
        public static readonly Bitvector RACE_DETECT_HIDDEN = new Bitvector(0, Bitvector.BV09);
        public static readonly Bitvector RACE_EXTRA_ARMS = new Bitvector(0, Bitvector.BV10);
        public static readonly Bitvector RACE_FAERIE_FIRE = new Bitvector(0, Bitvector.BV11);
        public static readonly Bitvector RACE_WEAPON_WIELD = new Bitvector(0, Bitvector.BV12);
        public static readonly Bitvector RACE_MUTE = new Bitvector(0, Bitvector.BV13);
        public static readonly Bitvector RACE_BODYSLAM = new Bitvector(0, Bitvector.BV14);
        public static readonly Bitvector RACE_CHARGE = new Bitvector(0, Bitvector.BV15);
        public static readonly Bitvector RACE_ULTRAVISION = new Bitvector(0, Bitvector.BV16);
        public static readonly Bitvector RACE_DOORBASH = new Bitvector(0, Bitvector.BV17);
        public static readonly Bitvector RACE_SHRUG = new Bitvector(0, Bitvector.BV18);
        public static readonly Bitvector RACE_ODSNEAK = new Bitvector( 0, Bitvector.BV19 );
        public static readonly Bitvector RACE_UDSNEAK = new Bitvector(0, Bitvector.BV20);
        public static readonly Bitvector RACE_STRENGTH = new Bitvector(0, Bitvector.BV21);
        public static readonly Bitvector RACE_UNDERDARK_VIS = new Bitvector(0, Bitvector.BV22);
        public static readonly Bitvector RACE_ENLARGE = new Bitvector(0, Bitvector.BV23);
        public static readonly Bitvector RACE_INVIS = new Bitvector(0, Bitvector.BV24);
        public static readonly Bitvector RACE_TRAMPLE = new Bitvector(0, Bitvector.BV25);
        public static readonly Bitvector RACE_SHIFT_PRIME = new Bitvector(0, Bitvector.BV26);
        public static readonly Bitvector RACE_SHIFT_ASTRAL = new Bitvector(0, Bitvector.BV27);
        public static readonly Bitvector RACE_LEVITATE = new Bitvector(0, Bitvector.BV28);
        public static readonly Bitvector RACE_BITE = new Bitvector(0, Bitvector.BV29);
        public static readonly Bitvector RACE_EXTRA_STRONG_WIELD = new Bitvector(0, Bitvector.BV30);
        public static readonly Bitvector RACE_NEGATE_SPELL = new Bitvector(1, Bitvector.BV00);
        public static readonly Bitvector RACE_CHANNEL_BERZERK = new Bitvector(1, Bitvector.BV01);
        public static readonly Bitvector RACE_NOBASH = new Bitvector(1, Bitvector.BV02);
        public static readonly Bitvector RACE_NOSPRING = new Bitvector(1, Bitvector.BV03);
        public static readonly Bitvector RACE_NOSLAM = new Bitvector(1, Bitvector.BV04);
        public static readonly Bitvector RACE_NOTRIP = new Bitvector(1, Bitvector.BV05);
        public static readonly Bitvector RACE_AWARE = new Bitvector(1, Bitvector.BV06);
        public static readonly Bitvector RACE_BLOODRAGE = new Bitvector(1, Bitvector.BV07);
        public static readonly Bitvector RACE_DARKNESS = new Bitvector(1, Bitvector.BV08);
        public static readonly Bitvector RACE_CLIMB = new Bitvector(1, Bitvector.BV09);
        public static readonly Bitvector RACE_ORIG_LIQUID = new Bitvector(1, Bitvector.BV10);
        public static readonly Bitvector RACE_ORIG_GAS = new Bitvector(1, Bitvector.BV11);
        public static readonly Bitvector RACE_ORIG_SOLID = new Bitvector(1, Bitvector.BV12);
        public static readonly Bitvector RACE_ORIG_FIRE = new Bitvector(1, Bitvector.BV13);
        public static readonly Bitvector RACE_ORIG_PLANAR = new Bitvector(1, Bitvector.BV14);
        public static readonly Bitvector RACE_ORIG_NEGATIVE = new Bitvector(1, Bitvector.BV15);
        public static readonly Bitvector RACE_ORIG_VOID = new Bitvector(1, Bitvector.BV16);
        public static readonly Bitvector RACE_PC_NEUTRAL = new Bitvector(1, Bitvector.BV17);
        public static readonly Bitvector RACE_CONFUSE = new Bitvector(1, Bitvector.BV18);
        public static readonly Bitvector RACE_DIG = new Bitvector(1, Bitvector.BV19);
        public static readonly Bitvector RACE_BAD_DODGE = new Bitvector(1, Bitvector.BV20);
        public static readonly Bitvector RACE_GOOD_DODGE = new Bitvector(1, Bitvector.BV21);
        public static readonly Bitvector RACE_REGENERATE = new Bitvector(1, Bitvector.BV22);
        public static readonly Bitvector RACE_SLAM_LARGER = new Bitvector(1, Bitvector.BV23);

        /// <summary>
        /// Load in all of the races that exist in-game, much like we do with class files.
        /// </summary>
        public static bool LoadRaces()
        {
            string raceslist = FileLocation.RaceDirectory + FileLocation.RaceLoadList;

            try
            {
                FileStream fpList = File.OpenRead( raceslist );
                StreamReader sr = new StreamReader( fpList );

                while( !sr.EndOfStream )
                {
                    string filename = sr.ReadLine();

                    if( filename[ 0 ] == '$' )
                    {
                        break;
                    }

                    if (!Load(FileLocation.RaceDirectory + filename))
                    {
                        string bugbuf = "Cannot load race file: " + filename;
                        throw new Exception(bugbuf);
                    }
                }
                sr.Close();
            }
            catch( Exception ex )
            {
                throw new Exception("Error loading races in Race.LoadRaces(): " + ex);
            }
            for (int count = 0; count < RaceList.Length; count++)
            {
                if (RaceList[count] == null)
                {
                    throw new Exception("Race " + count + " not loaded, not defined in any race file.");
                }
            }
            return true;
        }

        /// <summary>
        /// Converts a size to a string.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string SizeString(Size size)
        {
            switch (size)
            {
                default:
                    return "unknown-invalid";
                case Size.any:
                    return "unknown";
                case Size.tiny:
                    return "tiny";
                case Size.small:
                    return "small";
                case Size.medium:
                    return "medium";
                case Size.large:
                    return "large";
                case Size.huge:
                    return "huge";
                case Size.giant:
                    return "giant";
                case Size.titanic:
                    return "titanic";
                case Size.gargantuan:
                    return "gargantuan";
                case Size.none:
                    return "none";
            }
        }

        /// <summary>
        /// Gets a race based on its key letter combination.
        /// </summary>
        /// <param name="race"></param>
        /// <returns></returns>
        public static int RaceKeyLookup(string race)
        {
            int index;

            for (index = 0; index < RaceList.Length; index++)
            {
                if (RaceList[index]._name.Length == 0)
                {
                    throw new Exception(String.Format("RaceKeyLookup: race table entry {0} empty.", index));
                }
                if (race == RaceList[index]._key)
                {
                    return index;
                }
            }

            return -1;

        }

        /// <summary>
        /// Gets a race number based on its name.
        /// </summary>
        /// <param name="race"></param>
        /// <returns>The race number if lookup succeeds, otherwise -1.</returns>
        public static int RaceFullLookup(string race)
        {
            int index;

            for (index = 0; index < RaceList.Length; index++)
            {
                if (race == RaceList[index]._name)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Return a random race number based on a group type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetRandomRace( int type )
        {
            int race;
            int[] animals = new[] {
                RACE_WORM, RACE_BIRD, RACE_HERBIVORE, RACE_RAT, RACE_FISH, RACE_BAT,
                RACE_SNAKE, RACE_BOAR, RACE_BEAR, RACE_HORSE, RACE_PRIMATE, RACE_INSECT
            };

            Random rand = new Random();

            switch( type )
            {
                case RACE_RANGE_ANY:
                    race = rand.Next(0, RaceList.Length - 1);
                    break;
                case RACE_RANGE_PLAYER:
                    race = rand.Next(0, Limits.MAX_PC_RACE - 1);
                    break;
                case RACE_RANGE_HUMANOID:
                    race = rand.Next(RACE_HUMAN, RACE_HUMANOID);
                    break;
                case RACE_RANGE_ANIMAL:
                    {
                        int index = rand.Next(0, animals.GetLength(0) / sizeof(int) - 1);
                        race = animals[ index ];
                    }
                    break;
                default:
                    throw new Exception("Race.GetRandomRace: invalid type");
            }
            if (race == RACE_GOD)
            {
                race = GetRandomRace(type);
            }

            return race;
        }

        /// <summary>
        /// New code for loading races from file.
        ///
        /// Similar to character class load code.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool Load( string filename )
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof( Race ) );
                Stream stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.None );
                Race race = (Race)serializer.Deserialize( stream );
                stream.Close();
                if (race._number < RaceList.Length)
                {
                    if (RaceList[race._number] != null)
                    {
                        throw new Exception("Race.Load(): Race number " + race._number + " already taken by " + RaceList[race._number]._name + ".  Can't load " + race._name);
                    }
                    RaceList[ race._number ] = race;
                    race._filename = filename;
                    return true;
                }
                return false;
            }
            catch( Exception ex )
            {
                throw new Exception("Error loading race in Race.Load(): " + filename + ex);
            }
        }

        /// <summary>
        /// Savesa race to its XML-based race file.
        /// </summary>
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer( GetType() );
            Stream stream = new FileStream(FileLocation.RaceDirectory + _filename, FileMode.Create,
                FileAccess.Write, FileShare.None );
            serializer.Serialize( stream, this );
            stream.Close();
        }

        /// <summary>
        /// Gets a race by name.
        /// </summary>
        /// <param name="race"></param>
        /// <returns></returns>
        public static int RaceLookup(string race)
        {
            int index;

            for (index = 0; index < RaceList.Length; index++)
            {
                if (RaceList[index]._name.StartsWith(race, StringComparison.CurrentCultureIgnoreCase))
                    return index;
            }

            return -1;
        }

        /// <summary>
        /// Checks a race for a specific innate ability.
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        public bool HasInnate( Bitvector bit )
        {
            if (_innateAbilities == null || _innateAbilities.Length < 1)
            {
                return false;
            }

            if( ( _innateAbilities[ bit.Group ] & bit.Vector ) != 0 )
            {
                return true;
            }
            return false;
        }

        // Humanoids
        public const int RACE_HUMAN = 0;
        public const int RACE_BARBARIAN = 1;
        public const int RACE_GREYELF = 2;
        public const int RACE_RAKSHASA = 3;
        public const int RACE_HALFELF = 4;
        public const int RACE_GNOLL = 5;
        public const int RACE_DROW = 6;
        public const int RACE_DWARF = 7;
        public const int RACE_DUERGAR = 8;
        public const int RACE_HALFLING = 9;
        public const int RACE_TROLL = 10;
        public const int RACE_OGRE = 11;
        public const int RACE_ORC = 12;
        public const int RACE_GNOME = 13;
        public const int RACE_CENTAUR = 14;
        public const int RACE_GITHYANKI = 15;
        public const int RACE_GOBLIN = 16;
        public const int RACE_MINOTAUR = 17;
        public const int RACE_GITHZERAI = 18;
        public const int RACE_THRIKREEN = 19;
        public const int RACE_KOBOLD = 20;
        public const int RACE_HALFORC = 21;
        // Non-player races
        public const int RACE_SAHAUGIN = 22;
        public const int RACE_UMBERHULK = 23;
        public const int RACE_HALFDWARF = 24;
        public const int RACE_HALFKOBOLD = 25;
        public const int RACE_GIANT = 26;
        public const int RACE_ILLITHID = 27;
        public const int RACE_AQUATICELF = 28;
        public const int RACE_NEOGI = 29;
        public const int RACE_HOBGOBLIN = 30;
        public const int RACE_WEMIC = 31;
        public const int RACE_HUMANOID = 32;
        // Monsters
        public const int RACE_DEMON = 33;
        public const int RACE_GOD = 34;
        public const int RACE_DEVIL = 35;
        public const int RACE_ANGEL = 36;
        public const int RACE_FAERIE = 37;
        public const int RACE_VAMPIRE = 38;
        public const int RACE_WEREWOLF = 39;
        public const int RACE_DRAGON = 40;
        public const int RACE_BEHOLDER = 41;
        public const int RACE_DERRO = 42;
        public const int RACE_SLAAD = 43;
        public const int RACE_GOLEM = 44;
        public const int RACE_DRACOLICH = 45;
        public const int RACE_DRAGONKIN = 46;
        public const int RACE_UNDEAD = 47;
        public const int RACE_GHOST = 48;
        public const int RACE_HARPY = 49;
        public const int RACE_RUSTMONSTER = 50;
        public const int RACE_FIRE_ELE = 51;
        public const int RACE_AIR_ELE = 52;
        public const int RACE_WATER_ELE = 53;
        public const int RACE_EARTH_ELE = 54;
        public const int RACE_LYCANTHROPE = 55;
        public const int RACE_OBJECT = 56;
        public const int RACE_MIST = 57;
        public const int RACE_IXITXACHITL = 58;
        public const int RACE_ABOLETH = 59;
        public const int RACE_HYDRA = 60;
        // Animals
        public const int RACE_REPTILE = 61;
        public const int RACE_BOAR = 62;
        public const int RACE_INSECT = 63;
        public const int RACE_ANIMAL = 64;
        public const int RACE_HERBIVORE = 65;
        public const int RACE_CARNIVORE = 66;
        public const int RACE_BIRD = 67;
        public const int RACE_HORSE = 68;
        public const int RACE_PRIMATE = 69;
        public const int RACE_BEAR = 70;
        public const int RACE_BAT = 71;
        public const int RACE_PLANT = 72;
        public const int RACE_TREE = 73;
        public const int RACE_RAT = 74;
        public const int RACE_PARASITE = 75;
        public const int RACE_ARACHNID = 76;
        public const int RACE_SNAKE = 77;
        public const int RACE_WORM = 78;
        public const int RACE_FISH = 79;
        public const int RACE_ALIEN = 80;
        public const int RACE_SHADOW_ELE = 81;

        public enum RacewarSide
        {
            neutral = 0,
            good = 1,
            evil = 2,
            enslaver = 3,
        }
        public static readonly int MAX_RACEWAR_SIDE = Enum.GetValues(typeof(RacewarSide)).Length;

        public const int RACE_RANGE_ANY = 0;
        public const int RACE_RANGE_PLAYER = 1;
        public const int RACE_RANGE_ANIMAL = 2;
        public const int RACE_RANGE_HUMANOID = 3;
        public const int RACE_RANGE_SLUG = 4;

        public enum Size
        {
            any = 0,
            tiny = 1,
            small = 2,
            medium = 3, // (straight medium - humans)
            large = 4,
            huge = 5,
            giant = 6,
            titanic = 7,
            gargantuan = 8,
            none = 9 // insubstantial
        }
        public static readonly int MAX_SIZE = Enum.GetValues(typeof(Size)).Length;

        // Language bits.
        //
        // Keep in mind that these are stored in pfiles as a number, so it's a bad idea
        // to delete or change anything that already exists.
        public enum Language
        {
            unknown = 0,  // Anything else  
            common = 1,  // Human base language  
            elven = 2,  // Elven base language  
            dwarven = 3,  // Dwarven base language  
            centaur = 4,  // Centaur base language  
            ogre = 5,  // Ogre base language  
            orcish = 6,  // Orc base language  
            troll = 7,  // Troll base language  
            aquaticelf = 8,
            neogi = 9,
            thri = 10,  // Thri base language  
            dragon = 11,  // Large reptiles, Dragons  
            magical = 12,  // Spells maybe? Magical creatures  
            goblin = 13,  // Goblin base language  
            god = 14,  // The Universoul Language  
            halfling = 15,  // Halfling base language  
            githyanki = 16,  // Githyanki base language  
            drow = 17,  // Drow base language  
            kobold = 18,  // Kobold base language  
            gnome = 19,  // Gnome base language  
            animal = 20,  // Animal language
            duergar = 21,
            githzerai = 22,
            gnoll = 23,
            rakshasa = 24,
            minotaur = 25,
            illithid = 26,
            barbarian = 27
        }
        public static readonly int MAX_LANG = Enum.GetValues(typeof(Language)).Length;

        static public string[] LanguageTable = new string[]
        {
            "unknown",    // 0  
            "common",     // 1  
            "elven",      // 2  
            "dwarven",    // 3  
            "centaur",    // 4  
            "ogre",       // 5  
            "orcish",     // 6  
            "troll",      // 7  
            "aquatic elf",// 8  
            "saurial",    // 9  
            "thri-kreen", // 10 
            "dragon",     // 11 
            "magical",    // 12 
            "goblin",     // 13 
            "god",        // 14 
            "halfling",   // 15 
            "githyanki",  // 16 
            "drow",       // 17 
            "kobold",     // 18 
            "gnome",      // 19 
            "animal",     // 20 
            "duergar",    // 21 
            "githzerai",  // 22 
            "gnoll",      // 23 
            "rakshasa",   // 24 
            "minotaur",   // 25 
            "illithid",   // 26 
            "barbarian"   // 27 
        };

        // Race attribute bits.
        public const int RATT_NO_ATTRIBUTES = 0;
        public const int RATT_RAISABLE = Bitvector.BV00;

        // Race Behavior bits.
        public const int RBEH_NO_BEHAVIORS = 0;
        public const int RBEH_COGNATIVE = Bitvector.BV00; // Controled by Thought, rather than instinct.

        /// <summary>
        /// Body part bits.
        /// </summary>
        [Flags]
        public enum Parts
        {
            none = 0,
            skull = Bitvector.BV00,
            arms = Bitvector.BV01,
            legs = Bitvector.BV02,
            heart = Bitvector.BV03,
            brains = Bitvector.BV04,
            guts = Bitvector.BV05,
            hands = Bitvector.BV06,
            feet = Bitvector.BV07,
            fingers = Bitvector.BV08,
            ears = Bitvector.BV09,
            eyes = Bitvector.BV10,
            tongue = Bitvector.BV11,
            eyestalks = Bitvector.BV12,
            tentacles = Bitvector.BV13,
            fins = Bitvector.BV14,
            wings = Bitvector.BV15,
            tail = Bitvector.BV16,
            // Body parts used in combat.
            claws = Bitvector.BV17,
            fangs = Bitvector.BV18,
            horns = Bitvector.BV19,
            scales = Bitvector.BV20,
            tusks = Bitvector.BV21,
            // Plants
            bark = Bitvector.BV22,
            leaves = Bitvector.BV23,
            branches = Bitvector.BV24,
            trunk = Bitvector.BV25,
            // Trophies
            scalp = Bitvector.BV26,
            cranial_chitin = Bitvector.BV27,
            hooves = Bitvector.BV28
        }

        /// <summary>
        /// Used fpr Resistant, Immune, Susceptible, and Vulnerable checks.
        /// </summary>
        [Flags]
        public enum DamageType
        {
            none = 0,
            /// <summary>
            /// Burning
            /// </summary>
            fire = Bitvector.BV00,
            /// <summary>
            /// Freezing
            /// </summary>
            cold = Bitvector.BV01,
            /// <summary>
            /// Shocking
            /// </summary>
            electricity = Bitvector.BV02,
            energy = Bitvector.BV03,
            /// <summary>
            /// Corrosion.
            /// </summary>
            acid = Bitvector.BV04,
            /// <summary>
            /// Poison damage.
            /// </summary>
            poison = Bitvector.BV05,
            charm = Bitvector.BV06,
            /// <summary>
            /// Brain damage.
            /// </summary>
            mental = Bitvector.BV07,
            whiteMana = Bitvector.BV08,
            blackMana = Bitvector.BV09,
            /// <summary>
            /// Disease damage.
            /// </summary>
            disease = Bitvector.BV10,
            drowning = Bitvector.BV11,
            light = Bitvector.BV12,
            sound = Bitvector.BV13,
            magic = Bitvector.BV14,
            nonmagic = Bitvector.BV15,
            silver = Bitvector.BV16,
            iron = Bitvector.BV17,
            wood = Bitvector.BV18,
            weapon = Bitvector.BV19,
            bash = Bitvector.BV20,
            pierce = Bitvector.BV21,
            slash = Bitvector.BV22,
            /// <summary>
            /// Asphyxiation rather than poison.  See BV05 for poison.
            /// </summary>
            gas = Bitvector.BV23,
            sleep = Bitvector.BV24
        }

        /// <summary>
        /// Resistance types. Used for checking immunity/vulnerability to different damage types.
        /// </summary>
        public enum ResistanceType
        {
            /// <summary>
            /// Normal damage.
            /// </summary>
            normal = 0,
            /// <summary>
            /// Reduced damage.
            /// </summary>
            resistant,
            /// <summary>
            /// No damage.
            /// </summary>
            immune,
            /// <summary>
            /// Extra damage.
            /// </summary>
            susceptible,
            /// <summary>
            /// Massive damage.
            /// </summary>
            vulnerable
        }
    }
}