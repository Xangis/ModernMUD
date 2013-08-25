using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ModernMUD
{
    /// <summary>
    /// An affect, typically created by a spell, skill, song, or innate ability.
    /// 
    /// If additional bitvectors are added, the loading code will set them to zero
    /// automatically if they are not definied. If you do that, zones saved with a
    /// newer editor will not load correctly with an older one.
    /// 
    /// Many affect flags (and their values) are a legacy of the original area format
    /// used with Basternae 2.
    /// </summary>
    [Serializable]
    public class Affect
    {
        private AffectType _type;
        private string _value;
        private int _duration;
        private List<AffectApplyType> _modifiers;
        private int _level;
        private int[] _bitVectors = new int[Limits.NUM_AFFECT_VECTORS];
        private static int _count;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Affect()
        {
            ++_count;
            _modifiers = new List<AffectApplyType>();
            _value = String.Empty;
            _type = AffectType.spell;
            _duration = 0;
            _level = 0;
            foreach( int bv in _bitVectors )
            {
                _bitVectors[ bv ] = 0;
            }
        }

        /// <summary>
        /// Destructor.  Decrements in-memory object count.
        /// </summary>
        ~Affect()
        {
            --_count;
        }

        /// <summary>
        /// Allows us to use the NOT operator to check for NULL.
        /// </summary>
        /// <param name="af"></param>
        /// <returns></returns>
        public static implicit operator bool( Affect af )
        {
            if( af == null )
                return false;
            return true;
        }

        /// <summary>
        /// The array of integers representing the bitvectors set for an affect.
        /// </summary>
        // TODO: Because an affect should only ever have one bitvector value set at a time,
        // we should make this a "gatekeeper" that enforces that requirement.
        public int[] BitVectors
        {
            get { return _bitVectors; }
            set { _bitVectors = value; }
        }

        /// <summary>
        /// The specific affect type of this affect.
        /// </summary>
        public AffectType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Represents where this affect is applied to (the affect location).
        /// </summary>
        public List<AffectApplyType> Modifiers
        {
            get { return _modifiers; }
            set { _modifiers = value; }
        }

        /// <summary>
        /// The amount that this affect modifies the affected attribute or variable by.
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
   
        /// <summary>
        /// The level of power for this affect.  Typically reflects the caster or
        /// creator's level and is a significant factor in dispelling an affect.
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// The length of time that this affect will persist.
        /// </summary>
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        /// <summary>
        /// The number of in-game instances of the Affect class.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return _count;
            }
        }

        // only allows one bitvector per affect data.
        // this prevents us from adding wacky multiple affects.
        // automatically sets all other vector values to 0.
        public void SetBitvector( Bitvector bvect )
        {
            int count;
            for( count = 0; count < Limits.NUM_AFFECT_VECTORS; ++count )
            {
                if( count == bvect.Group )
                {
                    _bitVectors[ count ] = bvect.Vector;
                }
                else
                {
                    _bitVectors[ count ] = 0;
                }
            }

            return;
        }

        // Checks to see if a bitvector is set
        public bool HasBitvector( Bitvector bvect )
        {
            if( ( _bitVectors[ bvect.Group ] & bvect.Vector ) != 0)
            {
                return true;
            }

            return false;
        }

        public string AffectString( bool mortal )
        {
            return BitvectorFlagType.AffectString(_bitVectors, mortal);
        }

        public void AddModifier(Affect.Apply type, int amount)
        {
            AffectApplyType apply = new AffectApplyType();
            apply.Location = type;
            apply.Amount = amount;
            if (_modifiers == null)
            {
                _modifiers = new List<AffectApplyType>();
            }
            _modifiers.Add(apply);
        }

        /// <summary>
        /// Constructor that takes a spell argument to create an affect and set its paramters.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="duration"></param>
        /// <param name="location"></param>
        /// <param name="modifier"></param>
        /// <param name="bitvector"></param>
        public Affect(AffectType type, string name, int duration, Apply location, int modifier, Bitvector bitvector)
        {
            _modifiers = new List<AffectApplyType>();
            _value = name;
            _type = type;
            _duration = duration;
            AffectApplyType apply = new AffectApplyType();
            apply.Amount = modifier;
            apply.Location = location;
            _modifiers.Add(apply);
            SetBitvector(bitvector);
        }

        /// <summary>
        /// Apply types for affects. Used in area files, among other things.
        /// </summary>
        public enum Apply
        {
            none = 0,
            ac,
            age,
            agility,
            charisma,
            constitution,
            curse,
            damroll,
            dexterity,
            fire_protection,
            height,
            hitpoints,
            hitroll,
            immune,
            intelligence,
            luck,
            mana,
            max_agility,
            max_charisma,
            max_constitution,
            max_dexterity,
            max_intelligence,
            max_luck,
            max_power,
            max_strength,
            max_wisdom,
            move,
            power,
            race,
            race_agility,
            race_charisma,
            race_constitution,
            race_dexterity,
            race_intelligence,
            race_luck,
            race_power,
            race_strength,
            race_wisdom,
            resistant,
            save_breath,
            save_paralysis,
            save_petrification,
            save_poison,
            save_spell,
            sex,
            size,
            strength,
            susceptible,
            vulnerable,
            weight,
            wisdom
        };

        /// <summary>
        /// The type of (source of) an affect.
        /// </summary>
        public enum AffectType
        {
            /// <summary>
            /// Unknown or invalid.
            /// </summary>
            unknown = 0,
            /// <summary>
            /// Racial innate ability.
            /// </summary>
            innate,
            /// <summary>
            /// Skill.
            /// </summary>
            skill,
            /// <summary>
            /// Bard song.
            /// </summary>
            song,
            /// <summary>
            /// Magical spell.
            /// </summary>
            spell
        }

        // Affect bitvector definitions.
        public static readonly Bitvector AFFECT_NONE = new Bitvector( 0, 0 );
        public static readonly Bitvector AFFECT_BLIND = new Bitvector(0, Bitvector.BV00);
        public static readonly Bitvector AFFECT_INVISIBLE = new Bitvector(0, Bitvector.BV01);
        public static readonly Bitvector AFFECT_FARSEE = new Bitvector(0, Bitvector.BV02);
        public static readonly Bitvector AFFECT_DETECT_INVIS = new Bitvector(0, Bitvector.BV03);
        public static readonly Bitvector AFFECT_HASTE = new Bitvector(0, Bitvector.BV04);
        public static readonly Bitvector AFFECT_SENSE_LIFE = new Bitvector(0, Bitvector.BV05);
        public static readonly Bitvector AFFECT_MINOR_GLOBE = new Bitvector(0, Bitvector.BV06);
        public static readonly Bitvector AFFECT_STONESKIN = new Bitvector(0, Bitvector.BV07);
        public static readonly Bitvector AFFECT_UNDERDARK_VISION = new Bitvector(0, Bitvector.BV08);
        public static readonly Bitvector AFFECT_SHADOW_FORM = new Bitvector(0, Bitvector.BV09); // Not implemented.
        public static readonly Bitvector AFFECT_WRAITHFORM = new Bitvector(0, Bitvector.BV10);
        public static readonly Bitvector AFFECT_BREATHE_UNDERWATER = new Bitvector(0, Bitvector.BV11);
        public static readonly Bitvector AFFECT_KNOCKED_OUT = new Bitvector(0, Bitvector.BV12); // Not implemented.
        public static readonly Bitvector AFFECT_PROTECT_EVIL = new Bitvector(0, Bitvector.BV13);
        public static readonly Bitvector AFFECT_BOUND = new Bitvector(0, Bitvector.BV14);
        public static readonly Bitvector AFFECT_SLOW_POISON = new Bitvector(0, Bitvector.BV15);
        public static readonly Bitvector AFFECT_PROTECT_GOOD = new Bitvector(0, Bitvector.BV16);
        public static readonly Bitvector AFFECT_SLEEP = new Bitvector(0, Bitvector.BV17);
        public static readonly Bitvector AFFECT_SKL_AWARE = new Bitvector(0, Bitvector.BV18);
        public static readonly Bitvector AFFECT_SNEAK = new Bitvector(0, Bitvector.BV19);
        public static readonly Bitvector AFFECT_HIDE = new Bitvector(0, Bitvector.BV20);
        public static readonly Bitvector AFFECT_FEAR = new Bitvector(0, Bitvector.BV21);
        public static readonly Bitvector AFFECT_CHARM = new Bitvector(0, Bitvector.BV22);
        public static readonly Bitvector AFFECT_MEDITATE = new Bitvector(0, Bitvector.BV23);
        public static readonly Bitvector AFFECT_BARKSKIN = new Bitvector(0, Bitvector.BV24);
        public static readonly Bitvector AFFECT_INFRAVISION = new Bitvector(0, Bitvector.BV25);
        public static readonly Bitvector AFFECT_LEVITATE = new Bitvector(0, Bitvector.BV26);
        public static readonly Bitvector AFFECT_FLYING = new Bitvector(0, Bitvector.BV27);
        public static readonly Bitvector AFFECT_AWARE = new Bitvector(0, Bitvector.BV28);
        public static readonly Bitvector AFFECT_PROTECT_FIRE = new Bitvector(0, Bitvector.BV29);
        public static readonly Bitvector AFFECT_CAMPING = new Bitvector(0, Bitvector.BV30);
        public static readonly Bitvector AFFECT_FIRESHIELD = new Bitvector(1, Bitvector.BV00);
        public static readonly Bitvector AFFECT_ULTRAVISION = new Bitvector(1, Bitvector.BV01);
        public static readonly Bitvector AFFECT_DETECT_EVIL = new Bitvector(1, Bitvector.BV02);
        public static readonly Bitvector AFFECT_DETECT_GOOD = new Bitvector(1, Bitvector.BV03);
        public static readonly Bitvector AFFECT_DETECT_MAGIC = new Bitvector(1, Bitvector.BV04);
        public static readonly Bitvector AFFECT_MAJOR_PHYSICAL = new Bitvector(1, Bitvector.BV05);
        public static readonly Bitvector AFFECT_PROTECT_COLD = new Bitvector(1, Bitvector.BV06);
        public static readonly Bitvector AFFECT_PROTECT_LIGHTNING = new Bitvector(1, Bitvector.BV07);
        public static readonly Bitvector AFFECT_MINOR_PARA = new Bitvector(1, Bitvector.BV08);
        /// <summary>
        /// Major paralysis.
        /// </summary>
        public static readonly Bitvector AFFECT_HOLD = new Bitvector(1, Bitvector.BV09);
        public static readonly Bitvector AFFECT_SLOWNESS = new Bitvector(1, Bitvector.BV10);
        public static readonly Bitvector AFFECT_MAJOR_GLOBE = new Bitvector(1, Bitvector.BV11);
        public static readonly Bitvector AFFECT_PROTECT_GAS = new Bitvector(1, Bitvector.BV12);
        public static readonly Bitvector AFFECT_PROTECT_ACID = new Bitvector(1, Bitvector.BV13);
        public static readonly Bitvector AFFECT_POISON = new Bitvector(1, Bitvector.BV14);
        public static readonly Bitvector AFFECT_SOULSHIELD = new Bitvector(1, Bitvector.BV15);
        public static readonly Bitvector AFFECT_DUERGAR_HIDE = new Bitvector(1, Bitvector.BV16);
        public static readonly Bitvector AFFECT_MINOR_INVIS = new Bitvector(1, Bitvector.BV17);
        public static readonly Bitvector AFFECT_VAMP_TOUCH = new Bitvector(1, Bitvector.BV18);
        public static readonly Bitvector AFFECT_STUNNED = new Bitvector(1, Bitvector.BV19);
        public static readonly Bitvector AFFECT_DROPPED_PRIMARY = new Bitvector(1, Bitvector.BV20);
        public static readonly Bitvector AFFECT_DROPPED_SECOND = new Bitvector(1, Bitvector.BV21);
        public static readonly Bitvector AFFECT_FUMBLED_PRIMARY = new Bitvector(1, Bitvector.BV22);
        public static readonly Bitvector AFFECT_FUMBLED_SECOND = new Bitvector(1, Bitvector.BV23);
        public static readonly Bitvector AFFECT_HOLDING_BREATH = new Bitvector(1, Bitvector.BV24);
        public static readonly Bitvector AFFECT_MEMORIZING = new Bitvector(1, Bitvector.BV25);
        public static readonly Bitvector AFFECT_DROWNING = new Bitvector(1, Bitvector.BV26);
        public static readonly Bitvector AFFECT_PASS_DOOR = new Bitvector(1, Bitvector.BV27);
        public static readonly Bitvector AFFECT_DRAINING = new Bitvector(1, Bitvector.BV28);
        public static readonly Bitvector AFFECT_CASTING = new Bitvector(1, Bitvector.BV29);
        public static readonly Bitvector AFFECT_SCRIBING = new Bitvector(1, Bitvector.BV30);
        public static readonly Bitvector AFFECT_TENSORS_DISC = new Bitvector(2, Bitvector.BV00);
        public static readonly Bitvector AFFECT_TRACKING = new Bitvector(2, Bitvector.BV01);
        public static readonly Bitvector AFFECT_SINGING = new Bitvector(2, Bitvector.BV02);
        public static readonly Bitvector AFFECT_ECTOPLASMIC = new Bitvector(2, Bitvector.BV03);
        public static readonly Bitvector AFFECT_ABSORBING = new Bitvector(2, Bitvector.BV04);
        public static readonly Bitvector AFFECT_VAMP_BITE = new Bitvector(2, Bitvector.BV05);
        public static readonly Bitvector AFFECT_SPIRIT_WARD = new Bitvector(2, Bitvector.BV06);
        public static readonly Bitvector AFFECT_GREATER_SPIRIT_WARD = new Bitvector(2, Bitvector.BV07);
        public static readonly Bitvector AFFECT_NON_DETECTION = new Bitvector(2, Bitvector.BV08);
        public static readonly Bitvector AFFECT_SILVER = new Bitvector(2, Bitvector.BV09);
        public static readonly Bitvector AFFECT_PLUS_ONE = new Bitvector(2, Bitvector.BV10);
        public static readonly Bitvector AFFECT_PLUS_TWO = new Bitvector(2, Bitvector.BV11);
        public static readonly Bitvector AFFECT_PLUS_THREE = new Bitvector(2, Bitvector.BV12);
        public static readonly Bitvector AFFECT_PLUS_FOUR = new Bitvector(2, Bitvector.BV13);
        public static readonly Bitvector AFFECT_PLUS_FIVE = new Bitvector(2, Bitvector.BV14);
        public static readonly Bitvector AFFECT_ENLARGED = new Bitvector(2, Bitvector.BV15);
        public static readonly Bitvector AFFECT_REDUCED = new Bitvector(2, Bitvector.BV16);
        public static readonly Bitvector AFFECT_COVER = new Bitvector(2, Bitvector.BV17);
        public static readonly Bitvector AFFECT_FOUR_ARMS = new Bitvector(2, Bitvector.BV18);
        public static readonly Bitvector AFFECT_INERTIAL_BARRIER = new Bitvector(2, Bitvector.BV19);
        public static readonly Bitvector AFFECT_INTELLECT_FORTRESS = new Bitvector(2, Bitvector.BV20);
        public static readonly Bitvector AFFECT_COLDSHIELD = new Bitvector(2, Bitvector.BV21);
        public static readonly Bitvector AFFECT_CANNIBALIZING = new Bitvector(2, Bitvector.BV22);
        public static readonly Bitvector AFFECT_SWIMMING = new Bitvector(2, Bitvector.BV23);
        public static readonly Bitvector AFFECT_TOWER_OF_IRON_WILL = new Bitvector(2, Bitvector.BV24);
        public static readonly Bitvector AFFECT_UNDERWATER = new Bitvector(2, Bitvector.BV25);
        public static readonly Bitvector AFFECT_BLUR = new Bitvector(2, Bitvector.BV26);
        public static readonly Bitvector AFFECT_NECKBITING = new Bitvector(2, Bitvector.BV27);
        public static readonly Bitvector AFFECT_ELEMENTAL_FORM = new Bitvector(2, Bitvector.BV28);
        public static readonly Bitvector AFFECT_PASS_WITHOUT_TRACE = new Bitvector(2, Bitvector.BV29);
        public static readonly Bitvector AFFECT_PALADIN_AURA = new Bitvector(2, Bitvector.BV30);
        public static readonly Bitvector AFFECT_LOOTER = new Bitvector(3, Bitvector.BV00);
        public static readonly Bitvector AFFECT_DISEASE = new Bitvector(3, Bitvector.BV01);
        public static readonly Bitvector AFFECT_SACKING = new Bitvector(3, Bitvector.BV02);
        public static readonly Bitvector AFFECT_SENSE_FOLLOWER = new Bitvector(3, Bitvector.BV03);
        public static readonly Bitvector AFFECT_STORNOG_SPHERES = new Bitvector(3, Bitvector.BV04);
        public static readonly Bitvector AFFECT_GREATER_SPHERES = new Bitvector(3, Bitvector.BV05);
        public static readonly Bitvector AFFECT_VAMPIRE_FORM = new Bitvector(3, Bitvector.BV06);
        public static readonly Bitvector AFFECT_NO_UNMORPH = new Bitvector(3, Bitvector.BV07);
        public static readonly Bitvector AFFECT_HOLY_SACRIFICE = new Bitvector(3, Bitvector.BV08);
        public static readonly Bitvector AFFECT_BATTLE_ECSTASY = new Bitvector(3, Bitvector.BV09);
        public static readonly Bitvector AFFECT_DAZZLE = new Bitvector(3, Bitvector.BV10);
        public static readonly Bitvector AFFECT_DAZZLED = new Bitvector(3, Bitvector.BV11);
        public static readonly Bitvector AFFECT_THROAT_CRUSH = new Bitvector(3, Bitvector.BV12);
        public static readonly Bitvector AFFECT_REGENERATION = new Bitvector(3, Bitvector.BV13);
        public static readonly Bitvector AFFECT_BEARHUG = new Bitvector(3, Bitvector.BV14);
        public static readonly Bitvector AFFECT_GRAPPLING = new Bitvector(3, Bitvector.BV15);
        public static readonly Bitvector AFFECT_GRAPPLED = new Bitvector(3, Bitvector.BV16);
        public static readonly Bitvector AFFECT_MAGE_FLAME = new Bitvector(3, Bitvector.BV17);
        public static readonly Bitvector AFFECT_NO_IMMOLATE = new Bitvector(3, Bitvector.BV18);
        public static readonly Bitvector AFFECT_MULTICLASS = new Bitvector(3, Bitvector.BV19);
        public static readonly Bitvector AFFECT_DETECT_UNDEAD = new Bitvector(3, Bitvector.BV20);
        // Internal-only values.  Whether intentional or just not yet added to DikuEdit, they are
        // only settable within the game and typically used for spell affects.
        public static readonly Bitvector AFFECT_IS_FLEEING = new Bitvector(4, Bitvector.BV00);
        public static readonly Bitvector AFFECT_HUNTING = new Bitvector(4, Bitvector.BV01);
        public static readonly Bitvector AFFECT_BIOFEEDBACK = new Bitvector(4, Bitvector.BV02);
        public static readonly Bitvector AFFECT_FAMINE = new Bitvector(4, Bitvector.BV03);
        public static readonly Bitvector AFFECT_MUTE = new Bitvector(4, Bitvector.BV04);
        public static readonly Bitvector AFFECT_FAERIE_FIRE = new Bitvector(4, Bitvector.BV05);
        public static readonly Bitvector AFFECT_SANCTUARY = new Bitvector(4, Bitvector.BV06);
        public static readonly Bitvector AFFECT_CHANGE_SEX = new Bitvector(4, Bitvector.BV07);
        public static readonly Bitvector AFFECT_CURSE = new Bitvector(4, Bitvector.BV08);
        public static readonly Bitvector AFFECT_DETECT_HIDDEN = new Bitvector(4, Bitvector.BV09);
        public static readonly Bitvector AFFECT_POLYMORPH = new Bitvector(4, Bitvector.BV10);
        public static readonly Bitvector AFFECT_COMP_LANG = new Bitvector(4, Bitvector.BV11);
        public static readonly Bitvector AFFECT_DENY_EARTH = new Bitvector(4, Bitvector.BV12);
        public static readonly Bitvector AFFECT_DENY_AIR = new Bitvector(4, Bitvector.BV13);
        public static readonly Bitvector AFFECT_DENY_FIRE = new Bitvector(4, Bitvector.BV14);
        public static readonly Bitvector AFFECT_DENY_WATER = new Bitvector(4, Bitvector.BV15);
        public static readonly Bitvector AFFECT_TRACK = new Bitvector(4, Bitvector.BV16);
        public static readonly Bitvector AFFECT_JUSTICE_TRACKER = new Bitvector(4, Bitvector.BV17);
        public static readonly Bitvector AFFECT_LAYHANDS_TIMER = new Bitvector(4, Bitvector.BV18);
        public static readonly Bitvector AFFECT_ELEM_SIGHT = new Bitvector(4, Bitvector.BV19);
        // Bitvector.BV20 unused.
        public static readonly Bitvector AFFECT_MISDIRECTION = new Bitvector(4, Bitvector.BV21);
        public static readonly Bitvector AFFECT_VACANCY = new Bitvector(4, Bitvector.BV22);
        public static readonly Bitvector AFFECT_CHANGE_SELF = new Bitvector(4, Bitvector.BV23);
        public static readonly Bitvector AFFECT_PROWESS = new Bitvector(4, Bitvector.BV24);
        public static readonly Bitvector AFFECT_SUMMON_MOUNT_TIMER = new Bitvector(4, Bitvector.BV25);
        public static readonly Bitvector AFFECT_INCOMPETENCE = new Bitvector(4, Bitvector.BV26);
        public static readonly Bitvector AFFECT_CLIMBING = new Bitvector(4, Bitvector.BV27);
        public static readonly Bitvector AFFECT_RITUAL_OF_PROTECTION = new Bitvector(4, Bitvector.BV28);
        public static readonly Bitvector AFFECT_COORDINATION = new Bitvector(4, Bitvector.BV29);
        public static readonly Bitvector AFFECT_CHARM_OTTER = new Bitvector(4, Bitvector.BV30);
        public static readonly Bitvector AFFECT_ENDURANCE = new Bitvector(5, Bitvector.BV00);
        public static readonly Bitvector AFFECT_FORTITUDE = new Bitvector(5, Bitvector.BV01);
        public static readonly Bitvector AFFECT_INSIGHT = new Bitvector(5, Bitvector.BV02);
        public static readonly Bitvector AFFECT_MIGHT = new Bitvector(5, Bitvector.BV03);
        public static readonly Bitvector AFFECT_SAVVY = new Bitvector(5, Bitvector.BV04);
        public static readonly Bitvector AFFECT_THIRST = new Bitvector(5, Bitvector.BV05);
        public static readonly Bitvector AFFECT_HUNGER = new Bitvector(5, Bitvector.BV06);
        public static readonly Bitvector AFFECT_VITALITY = new Bitvector(5, Bitvector.BV07);
        public static readonly Bitvector AFFECT_PROTECT_FROM_UNDEAD = new Bitvector(5, Bitvector.BV08);
        public static readonly Bitvector AFFECT_BERZERK = new Bitvector(5, Bitvector.BV09);
        public static readonly Bitvector AFFECT_ARMOR = new Bitvector(5, Bitvector.BV10);
        public static readonly Bitvector AFFECT_FEEBLEMIND = new Bitvector(5, Bitvector.BV11);
        public static readonly Bitvector AFFECT_VISION_IMPAIRED = new Bitvector(5, Bitvector.BV12);
        public static readonly Bitvector AFFECT_VISION_ENHANCED = new Bitvector(5, Bitvector.BV13);
        public static readonly Bitvector AFFECT_STRENGTH_INCREASED = new Bitvector(5, Bitvector.BV14);
        public static readonly Bitvector AFFECT_STRENGTH_REDUCED = new Bitvector(5, Bitvector.BV15);
        public static readonly Bitvector AFFECT_BRAVE = new Bitvector(5, Bitvector.BV16);
        public static readonly Bitvector AFFECT_COWARDLY = new Bitvector(5, Bitvector.BV17);
        public static readonly Bitvector AFFECT_MOVEMENT_INCREASED = new Bitvector(5, Bitvector.BV18);
        public static readonly Bitvector AFFECT_MOVEMENT_REDUCED = new Bitvector(5, Bitvector.BV19);
        public static readonly Bitvector AFFECT_SHOCK_SHIELD = new Bitvector(5, Bitvector.BV20);
        public static readonly Bitvector AFFECT_WITHER = new Bitvector(5, Bitvector.BV21);
        public static readonly Bitvector AFFECT_BLESS = new Bitvector(5, Bitvector.BV22);
        public static readonly Bitvector AFFECT_DEXTERITY_INCREASED = new Bitvector(5, Bitvector.BV23);
        public static readonly Bitvector AFFECT_CHARISMA_INCREASED = new Bitvector(5, Bitvector.BV24);
    };


}