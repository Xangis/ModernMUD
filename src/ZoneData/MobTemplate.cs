
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ModernMUD
{
    /// <summary>
    /// Prototype for a mob.
    /// 
    /// This is a template from which a Mob (CharData) is instantiated.
    /// </summary>
    [Serializable]
    public class MobTemplate
    {
        private List<MobSpecial> _specFun;
        private string _specFunNames;
        public delegate bool MobFun( Object ch, int cmd );
        private List<MobSpecial> _deathFun;
        private List<CustomAction> _customActions;
        private string _deathFunName;
        private Shop _shopData;
        [XmlIgnore]
        private Area _area;
        private string _playerName;
        private string _shortDescription;
        private string _fullDescription;
        private string _description;
        private string _chatterBotName;
        private int _indexNumber;
        private int _numActive;
        // Number of times this mob or player has been killed (saved).  May want a per-boot value.
        private int _numberKilled;
        private Sex _sex;
        private CharClass _characterClass;
        private string _className;
        private int _level;
        private int[] _actionFlags = new int[Limits.NUM_ACTION_VECTORS];
        private int[] _affectedBy = new int[Limits.NUM_AFFECT_VECTORS];
        private int _alignment;
        private int _race;
        private int _defaultPosition;
        private static int _numMobIndex;
        private Race.DamageType _resistant;
        private Race.DamageType _immune;
        private Race.DamageType _susceptible;
        private Race.DamageType _vulnerable;

        // Action bits for mobs.  Used in area files.
        public static readonly Bitvector ACT_NONE = new Bitvector( 0, 0 );
        public static readonly Bitvector ACT_SIZEMINUS = new Bitvector(0, Bitvector.BV00);
        public static readonly Bitvector ACT_SENTINEL = new Bitvector(0, Bitvector.BV01);
        public static readonly Bitvector ACT_SCAVENGER = new Bitvector(0, Bitvector.BV02);
        public static readonly Bitvector ACT_IS_NPC = new Bitvector(0, Bitvector.BV03);
        public static readonly Bitvector ACT_GUARDIAN = new Bitvector(0, Bitvector.BV04);
        public static readonly Bitvector ACT_AGGRESSIVE = new Bitvector(0, Bitvector.BV05);
        public static readonly Bitvector ACT_STAY_AREA = new Bitvector(0, Bitvector.BV06);
        public static readonly Bitvector ACT_WIMPY = new Bitvector(0, Bitvector.BV07);
        public static readonly Bitvector ACT_AGGROEVIL = new Bitvector(0, Bitvector.BV08);
        public static readonly Bitvector ACT_AGGROGOOD = new Bitvector(0, Bitvector.BV09);
        public static readonly Bitvector ACT_AGGRONEUT = new Bitvector(0, Bitvector.BV10);
        public static readonly Bitvector ACT_MEMORY = new Bitvector(0, Bitvector.BV11);
        public static readonly Bitvector ACT_NOPARA = new Bitvector(0, Bitvector.BV12);
        public static readonly Bitvector ACT_NOSUMMON = new Bitvector(0, Bitvector.BV13);
        public static readonly Bitvector ACT_NOBASH = new Bitvector(0, Bitvector.BV14);
        public static readonly Bitvector ACT_TEACHER = new Bitvector(0, Bitvector.BV15);
        public static readonly Bitvector ACT_OUTLAW = new Bitvector(0, Bitvector.BV16);
        public static readonly Bitvector ACT_CANFLY = new Bitvector(0, Bitvector.BV17);
        public static readonly Bitvector ACT_CANSWIM = new Bitvector(0, Bitvector.BV18);
        public static readonly Bitvector ACT_CANT_TK = new Bitvector(0, Bitvector.BV19);
        public static readonly Bitvector ACT_MOVED = new Bitvector(0, Bitvector.BV20);
        public static readonly Bitvector ACT_PET = new Bitvector(0, Bitvector.BV21);
        public static readonly Bitvector ACT_NOEXP = new Bitvector(0, Bitvector.BV22);
        public static readonly Bitvector ACT_SIZEPLUS = new Bitvector(0, Bitvector.BV23);
        public static readonly Bitvector ACT_WITNESS = new Bitvector(0, Bitvector.BV24);
        public static readonly Bitvector ACT_NOCHARM = new Bitvector(0, Bitvector.BV25);
        public static readonly Bitvector ACT_PROTECTOR = new Bitvector(0, Bitvector.BV26);
        public static readonly Bitvector ACT_MOUNT = new Bitvector(0, Bitvector.BV27);
        public static readonly Bitvector ACT_AGGROEVILRACE = new Bitvector(0, Bitvector.BV28);
        public static readonly Bitvector ACT_AGGROGOODRACE = new Bitvector(0, Bitvector.BV29);
        public static readonly Bitvector ACT_HUNTER = new Bitvector(0, Bitvector.BV30);
        public static readonly Bitvector ACT_FACTION = new Bitvector(1, Bitvector.BV00); // Faction adjustments when killed.
        public static readonly Bitvector ACT_DISPEL_WALL = new Bitvector(1, Bitvector.BV01); // Dispels walls, even if doesn't have dispel magic spell.

        /// <summary>
        /// Represents the sex of a mobile or player.
        /// </summary>
        public enum Sex
        {
            neutral = 0,
            male = 1,
            female = 2,
        };

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MobTemplate()
        {
            ++_numMobIndex;
            _playerName = "none";
            _shortDescription = "(no short description)";
            _fullDescription = "(no long description)";
            _description = "(no description)";
            //_chatterBot = null;
            _chatterBotName = String.Empty;
            _indexNumber = 0;
            _numActive = 0;
            _numberKilled = 0;
            _sex = Sex.male;
            _level = 1;
            _actionFlags[ACT_IS_NPC.Group] |= ACT_IS_NPC.Vector;
            _actionFlags[ACT_MEMORY.Group] |= ACT_MEMORY.Vector;
            _defaultPosition = Position.standing;
            _characterClass = CharClass.ClassList[0];
            _specFun = new List<MobSpecial>();
            _deathFun = new List<MobSpecial>();
            _immune = 0;
            _race = 0;
            _resistant = 0;
            _susceptible = 0;
            _alignment = 0;
            for( int iter = 0; iter < Limits.NUM_AFFECT_VECTORS; ++iter )
            {
                _affectedBy[ iter ] = 0;
            }
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="mob"></param>
        public MobTemplate(MobTemplate mob)
        {
            _actionFlags = new int[mob._actionFlags.Length];
            mob._actionFlags.CopyTo(_actionFlags, 0);
            _affectedBy = new int[mob._affectedBy.Length];
            mob._affectedBy.CopyTo(_affectedBy, 0);
            _alignment = mob._alignment;
            _area = mob._area;
            _characterClass = mob._characterClass;
            _chatterBotName = mob._chatterBotName;
            _className = mob._className;
            _customActions = new List<CustomAction>(mob._customActions);
            _deathFun = new List<MobSpecial>(mob._deathFun);
            _deathFunName = mob._deathFunName;
            _defaultPosition = mob._defaultPosition;
            _description = mob._description;
            _fullDescription = mob._fullDescription;
            _immune = mob._immune;
            _indexNumber = _area.HighMobIndexNumber + 1;
            _level = mob._level;
            _numActive = 0;
            _numberKilled = 0;
            ++_numMobIndex;
            _playerName = mob._playerName;
            _race = mob._race;
            _resistant = mob._resistant;
            _sex = mob._sex;
            // TODO: Deep copy this.
            _shopData = mob._shopData;
            _shortDescription = mob._shortDescription;
            _specFun = new List<MobSpecial>(mob._specFun);
            _specFunNames = mob._specFunNames;
            _susceptible = mob._susceptible;
            _vulnerable = mob._vulnerable;
        }

        /// <summary>
        /// Decrements the in-memory count of objects of this type on destruction.
        /// </summary>
        ~MobTemplate()
        {
            if( _shopData != null )
            {
                _shopData = null;
            }

            --_numMobIndex;
            return;
        }

        /// <summary>
        /// Custom actions.  Used for special functions.
        /// </summary>
        public List<CustomAction> CustomActions
        {
            get { return _customActions; }
            set { _customActions = value; }
        }

        /// <summary>
        /// Shop data associated with this mob template, if any.  Not saved -- associated
        /// at runtime from shop data in area file.
        /// </summary>
        [XmlIgnore]
        public Shop ShopData
        {
            get { return _shopData; }
            set { _shopData = value; }
        }

        /// <summary>
        /// The default position of this mobile type.
        /// </summary>
        public int DefaultPosition
        {
            get { return _defaultPosition; }
            set { _defaultPosition = value; }
        }

        /// <summary>
        /// Number of living mobiles instantiated from this template.
        /// </summary>
        [XmlIgnore]
        public int NumActive
        {
            get { return _numActive; }
            set { _numActive = value; }
        }

        /// <summary>
        /// The virtual number (index) of this mob template.
        /// </summary>
        public int IndexNumber
        {
            get { return _indexNumber; }
            set { _indexNumber = value; }
        }

        /// <summary>
        /// The special functions associated with this mob template.  Set at runtime.
        /// </summary>
        [XmlIgnore]
        public List<MobSpecial> SpecFun
        {
            get { return _specFun; }
            set { _specFun = value; }
        }

        /// <summary>
        /// Death functions/triggers associated with this mob template.  Set at runtime.
        /// </summary>
        [XmlIgnore]
        public List<MobSpecial> DeathFun
        {
            get { return _deathFun; }
            set { _deathFun = value; }
        }

        /// <summary>
        /// The area that this mob template is associated with.  Set at runtime.
        /// </summary>
        [XmlIgnore]
        public Area Area
        {
            get { return _area; }
            set { _area = value; }
        }

        /// <summary>
        /// The character class of this mobile.  Set at runtime via lookup.
        /// </summary>
        [XmlIgnore]
        public CharClass CharacterClass
        {
            get { return _characterClass; }
            set { _characterClass = value; }
        }

        /// <summary>
        /// The action flags set on this mob template.
        /// </summary>
        public int[] ActionFlags
        {
            get { return _actionFlags; }
            set { _actionFlags = value; }
        }

        /// <summary>
        /// The affect flags set on this mob template.
        /// </summary>
        public int[] AffectedBy
        {
            get { return _affectedBy; }
            set { _affectedBy = value; }
        }

        /// <summary>
        /// The number of mobiles of this type that have been killed.
        /// </summary>
        public int NumberKilled
        {
            get { return _numberKilled; }
            set { _numberKilled = value; }
        }

        /// <summary>
        /// The sex of this mobile type.
        /// </summary>
        public Sex Gender
        {
            get { return _sex; }
            set { _sex = value; }
        }

        /// <summary>
        /// The level of this mobile type.
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// The alignment of this mobile type.
        /// </summary>
        public int Alignment
        {
            get { return _alignment; }
            set { _alignment = value; }
        }

        /// <summary>
        /// The list of special functions associated with this mobile.
        /// </summary>
        public string SpecFunNames
        {
            get { return _specFunNames; }
            set { _specFunNames = value; }
        }

        /// <summary>
        /// The list of death functions associated with this mobile.
        /// </summary>
        public string DeathFunName
        {
            get { return _deathFunName; }
            set { _deathFunName = value; }
        }

        /// <summary>
        /// The player name for this mobile.
        /// </summary>
        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }

        /// <summary>
        /// The short description of this mobile.
        /// </summary>
        public string ShortDescription
        {
            get { return _shortDescription; }
            set { _shortDescription = value; }
        }

        /// <summary>
        /// The full, detailed description of this mobile.
        /// </summary>
        public string FullDescription
        {
            get { return _fullDescription; }
            set { _fullDescription = value; }
        }

        /// <summary>
        /// The description of this mobile.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// The name of the chatterbot associated with this mobile.
        /// </summary>
        public string ChatterBotName
        {
            get { return _chatterBotName; }
            set { _chatterBotName = value; }
        }

        /// <summary>
        /// The name of the class for this mobile.
        /// </summary>
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        /// <summary>
        /// The race of this mobile.
        /// </summary>
        public int Race
        {
            get { return _race; }
            set { _race = value; }
        }

        /// <summary>
        /// The damage types that this mobile is resistant to.
        /// </summary>
        public Race.DamageType Resistant
        {
            get { return _resistant; }
            set { _resistant = value; }
        }

        /// <summary>
        /// The damage types that this mobile is immune to.
        /// </summary>
        public Race.DamageType Immune
        {
            get { return _immune; }
            set { _immune = value; }
        }

        /// <summary>
        /// The damage types that this mobile is susceptible to (slightly more damage taken).
        /// </summary>
        public Race.DamageType Susceptible
        {
            get { return _susceptible; }
            set { _susceptible = value; }
        }

        /// <summary>
        /// The damage types that this mobile is vulnerable to (a whole lot more damage taken).
        /// </summary>
        public Race.DamageType Vulnerable
        {
            get { return _vulnerable; }   
            set { _vulnerable = value; }
        }

        /// <summary>
        /// Allows use of the NOT operator to check for null.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool operator !( MobTemplate m )
        {
            if (m == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets a sex value based on a provided string.
        /// </summary>
        /// <param name="sex"></param>
        /// <returns>The sex value, or SEXIT_NEUTRAL if invalid.</returns>
        public static Sex SexLookup(string sex)
        {
            switch (sex)
            {
                case "male":
                    return Sex.male;
                case "female":
                    return Sex.female;
                case "neutral":
                case "neuter":
                    return Sex.neutral;
            }
            return Sex.neutral;
        }

        /// <summary>
        /// Adds a special function to this mobile type.
        /// </summary>
        /// <param name="fun"></param>
        public void AddSpecFun( MobSpecial fun )
        {
             // No duplicate specials allowed.
            foreach( MobSpecial existing in _specFun )
            {
                if( existing == fun )
                    return;
            }

            _specFun.Add( fun );
            _specFunNames += fun.SpecName;
            return;
        }

        /// <summary>
        /// Adds additional affect vectors to this mobile type if there are more vectors available
        /// in the MUD engine than in the area file containing the MobTemplate.
        /// </summary>
        public void ExtendAffects()
        {
            if (_affectedBy.Length < Limits.NUM_AFFECT_VECTORS)
            {
                int[] oldData = _affectedBy;
                _affectedBy = new int[Limits.NUM_AFFECT_VECTORS];
                for (int i = 0; i < oldData.Length; i++)
                {
                    _affectedBy[i] = oldData[i];
                }
            }
        }

        /// <summary>
        /// Gets the total number of in-memory instances of the MobTemplate type.
        /// </summary>
        public static int Count
        {
            get
            {
                return _numMobIndex;
            }
        }

        /// <summary>
        /// Displays this mobile as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _description;
        }

        /// <summary>
        /// Sets an action flag on this mobile type.
        /// </summary>
        /// <param name="bvect"></param>
        public void SetActBit(Bitvector bvect)
        {
            _actionFlags[bvect.Group] |= bvect.Vector;
            return;
        }

        /// <summary>
        /// Removes an action flag from this mobile type.
        /// </summary>
        /// <param name="bvect"></param>
        public void RemoveActBit(Bitvector bvect)
        {
            _actionFlags[bvect.Group] &= ~(bvect.Vector);
            return;
        }

        /// <summary>
        /// Toggles an action bit for this mobile type.
        /// </summary>
        /// <param name="bvect"></param>
        public void ToggleActBit(Bitvector bvect)
        {
            _actionFlags[bvect.Group] ^= bvect.Vector;
            return;
        }

        /// <summary>
        /// Checks for an action flag on this mobile type.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool HasActBit(Bitvector bvect)
        {
            if ((_actionFlags[bvect.Group] & bvect.Vector) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a special function to this mobile type.
        /// </summary>
        /// <param name="name"></param>
        public void AddSpecFun( string name )
        {
            List<MobSpecial> fun = MobSpecial.SpecMobLookup( name );

            if( fun.Count < 1 )
            {
                return;
            }

            // Prevent duplicate special functions.  Each mob can have only one copy of a special function.
            foreach( MobSpecial spec in _specFun )
            {
                if( spec == fun[0] )
                {
                    return;
                }
            }

            _specFun.Add( fun[0] );
            _specFunNames += fun[0].SpecName + " ";
            return;
        }

        /// <summary>
        /// Checks this mobile type for existence of a special function.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasSpecFun( string name )
        {
            List<MobSpecial> specs = MobSpecial.SpecMobLookup(name);
            if( specs.Count < 1 )
            {
                return false;
            }
            foreach( MobSpecial specfun in _specFun )
            {
                if( specfun == specs[0] )
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds the special function for a character class to this mobile's list of
        /// special functions.
        /// </summary>
        /// <param name="cClass"></param>
        public void AddSpecClass( int cClass )
        {
            MobTemplate mobTemplate = this;
            switch( cClass )
            {
                default:
                    break;
                case (int)CharClass.Names.shaman:
                    mobTemplate.AddSpecFun( "spec_cast_shaman" );
                    break;
                case (int)CharClass.Names.elementAir:
                    mobTemplate.AddSpecFun( "spec_cast_air_ele" );
                    break;
                case (int)CharClass.Names.elementEarth:
                    mobTemplate.AddSpecFun( "spec_cast_earth_ele" );
                    break;
                case (int)CharClass.Names.elementFire:
                    mobTemplate.AddSpecFun( "spec_cast_fire_ele" );
                    break;
                case (int)CharClass.Names.elementWater:
                    mobTemplate.AddSpecFun( "spec_cast_water_ele" );
                    break;
                case (int)CharClass.Names.druid:
                    mobTemplate.AddSpecFun( "spec_cast_druid" );
                    break;
                case (int)CharClass.Names.ranger:
                    mobTemplate.AddSpecFun( "spec_cast_ranger" );
                    break;
                case (int)CharClass.Names.hunter:
                    mobTemplate.AddSpecFun( "spec_cast_hunter" );
                    break;
                case (int)CharClass.Names.warrior:
                    mobTemplate.AddSpecFun( "spec_warrior" );
                    break;
                case (int)CharClass.Names.monk:
                    mobTemplate.AddSpecFun( "spec_monk" );
                    break;
                case (int)CharClass.Names.bard:
                    mobTemplate.AddSpecFun( "spec_bard" );
                    break;
                case (int)CharClass.Names.illusionist:
                    mobTemplate.AddSpecFun( "spec_cast_illusionist" );
                    break;
                case (int)CharClass.Names.paladin:
                    mobTemplate.AddSpecFun( "spec_cast_paladin" );
                    break;
                case (int)CharClass.Names.antipaladin:
                    mobTemplate.AddSpecFun( "spec_cast_antipaladin" );
                    break;
                case (int)CharClass.Names.thief:
                    mobTemplate.AddSpecFun( "spec_thief" );
                    break;
                case (int)CharClass.Names.mercenary:
                    mobTemplate.AddSpecFun( "spec_mercenary" );
                    break;
                case (int)CharClass.Names.assassin:
                    mobTemplate.AddSpecFun( "spec_assassin" );
                    break;
                case (int)CharClass.Names.cleric:
                    mobTemplate.AddSpecFun( "spec_cast_cleric" );
                    break;
                case (int)CharClass.Names.sorcerer:
                    mobTemplate.AddSpecFun( "spec_cast_sorcerer" );
                    break;
                case (int)CharClass.Names.psionicist:
                    mobTemplate.AddSpecFun( "spec_cast_psionicist" );
                    break;
                case (int)CharClass.Names.necromancer:
                    mobTemplate.AddSpecFun( "spec_cast_necromancer" );
                    break;
                case (int)CharClass.Names.chronomancer:
                    mobTemplate.AddSpecFun( "spec_cast_chronomancer" );
                    break;
            }
            return;
        }

    }
}