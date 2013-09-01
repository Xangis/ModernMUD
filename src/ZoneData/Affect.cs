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
            if (af == null)
            {
                return false;
            }
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

        /// <summary>
        /// Sets which bitvector is active on an affect. Only allows one bitvector
        /// per affect data and sets everything else to 0.
        /// </summary>
        /// <param name="bvect"></param>
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

        /// <summary>
        /// Checks to see whether a bitvector is set.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
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

        // TODO: Verify that all of these are implemented.

        // TODO: Look at the affect types that seem to overlap and see if anything can be
        // consolidated.

        /// <summary>
        /// No affect.
        /// </summary>
        public static readonly Bitvector AFFECT_NONE = new Bitvector( 0, 0 );
        /// <summary>
        /// Can't see.
        /// </summary>
        public static readonly Bitvector AFFECT_BLIND = new Bitvector(0, Bitvector.BV00);
        /// <summary>
        /// Can't be seen.
        /// </summary>
        public static readonly Bitvector AFFECT_INVISIBLE = new Bitvector(0, Bitvector.BV01);
        /// <summary>
        /// Can see into adjacent rooms.
        /// </summary>
        public static readonly Bitvector AFFECT_FARSEE = new Bitvector(0, Bitvector.BV02);
        /// <summary>
        /// Can see invisible.
        /// </summary>
        public static readonly Bitvector AFFECT_DETECT_INVIS = new Bitvector(0, Bitvector.BV03);
        /// <summary>
        /// Attacks at double speed.
        /// </summary>
        public static readonly Bitvector AFFECT_HASTE = new Bitvector(0, Bitvector.BV04);
        /// <summary>
        /// Can sense lifeforms.
        /// </summary>
        public static readonly Bitvector AFFECT_SENSE_LIFE = new Bitvector(0, Bitvector.BV05);
        /// <summary>
        /// Protected from low-level spells.
        /// </summary>
        public static readonly Bitvector AFFECT_MINOR_GLOBE = new Bitvector(0, Bitvector.BV06);
        /// <summary>
        /// Skin has a protective coating.
        /// </summary>
        public static readonly Bitvector AFFECT_STONESKIN = new Bitvector(0, Bitvector.BV07);
        /// <summary>
        /// Can see in the underdark (ultravision).
        /// </summary>
        public static readonly Bitvector AFFECT_UNDERDARK_VISION = new Bitvector(0, Bitvector.BV08);
        /// <summary>
        /// Shadow form.
        /// </summary>
        public static readonly Bitvector AFFECT_SHADOW_FORM = new Bitvector(0, Bitvector.BV09);
        /// <summary>
        /// Wraith form.
        /// </summary>
        public static readonly Bitvector AFFECT_WRAITHFORM = new Bitvector(0, Bitvector.BV10);
        /// <summary>
        /// Can breathe underwater.
        /// </summary>
        public static readonly Bitvector AFFECT_BREATHE_UNDERWATER = new Bitvector(0, Bitvector.BV11);
        /// <summary>
        /// Unconscious.
        /// </summary>
        public static readonly Bitvector AFFECT_KNOCKED_OUT = new Bitvector(0, Bitvector.BV12);
        /// <summary>
        /// Protected from evil.
        /// </summary>
        public static readonly Bitvector AFFECT_PROTECT_EVIL = new Bitvector(0, Bitvector.BV13);
        /// <summary>
        /// Captured/bound.
        /// </summary>
        public static readonly Bitvector AFFECT_BOUND = new Bitvector(0, Bitvector.BV14);
        /// <summary>
        /// Effects of poison are slowed.
        /// </summary>
        public static readonly Bitvector AFFECT_SLOW_POISON = new Bitvector(0, Bitvector.BV15);
        /// <summary>
        /// Protection from good.
        /// </summary>
        public static readonly Bitvector AFFECT_PROTECT_GOOD = new Bitvector(0, Bitvector.BV16);
        /// <summary>
        /// Asleep (magically).
        /// </summary>
        public static readonly Bitvector AFFECT_SLEEP = new Bitvector(0, Bitvector.BV17);
        /// <summary>
        /// Awareness skill is active.
        /// </summary>
        public static readonly Bitvector AFFECT_SKL_AWARE = new Bitvector(0, Bitvector.BV18);
        /// <summary>
        /// Moving silently.
        /// </summary>
        public static readonly Bitvector AFFECT_SNEAK = new Bitvector(0, Bitvector.BV19);
        /// <summary>
        /// Hiding.
        /// </summary>
        public static readonly Bitvector AFFECT_HIDE = new Bitvector(0, Bitvector.BV20);
        /// <summary>
        /// Afraid.
        /// </summary>
        public static readonly Bitvector AFFECT_FEAR = new Bitvector(0, Bitvector.BV21);
        /// <summary>
        /// Charmed.
        /// </summary>
        public static readonly Bitvector AFFECT_CHARM = new Bitvector(0, Bitvector.BV22);
        /// <summary>
        /// Meditating.
        /// </summary>
        public static readonly Bitvector AFFECT_MEDITATE = new Bitvector(0, Bitvector.BV23);
        /// <summary>
        /// Protective skin coating.
        /// </summary>
        public static readonly Bitvector AFFECT_BARKSKIN = new Bitvector(0, Bitvector.BV24);
        /// <summary>
        /// Can see lifeforms (infrared).
        /// </summary>
        public static readonly Bitvector AFFECT_INFRAVISION = new Bitvector(0, Bitvector.BV25);
        /// <summary>
        /// Levitating.
        /// </summary>
        public static readonly Bitvector AFFECT_LEVITATE = new Bitvector(0, Bitvector.BV26);
        /// <summary>
        /// Flying.
        /// </summary>
        public static readonly Bitvector AFFECT_FLYING = new Bitvector(0, Bitvector.BV27);
        /// <summary>
        /// Aware.
        /// </summary>
        public static readonly Bitvector AFFECT_AWARE = new Bitvector(0, Bitvector.BV28);
        /// <summary>
        /// Protection from fire.
        /// </summary>
        public static readonly Bitvector AFFECT_PROTECT_FIRE = new Bitvector(0, Bitvector.BV29);
        /// <summary>
        /// Camping out (leaving the game).
        /// </summary>
        public static readonly Bitvector AFFECT_CAMPING = new Bitvector(0, Bitvector.BV30);
        /// <summary>
        /// Surrounded by flames.
        /// </summary>
        public static readonly Bitvector AFFECT_FIRESHIELD = new Bitvector(1, Bitvector.BV00);
        /// <summary>
        /// Ultraviolet vision (see in the dark).
        /// </summary>
        public static readonly Bitvector AFFECT_ULTRAVISION = new Bitvector(1, Bitvector.BV01);
        /// <summary>
        /// Can detect evil.
        /// </summary>
        public static readonly Bitvector AFFECT_DETECT_EVIL = new Bitvector(1, Bitvector.BV02);
        /// <summary>
        /// Can detect good.
        /// </summary>
        public static readonly Bitvector AFFECT_DETECT_GOOD = new Bitvector(1, Bitvector.BV03);
        /// <summary>
        /// Can detect magic.
        /// </summary>
        public static readonly Bitvector AFFECT_DETECT_MAGIC = new Bitvector(1, Bitvector.BV04);
        /// <summary>
        /// ???
        /// </summary>
        public static readonly Bitvector AFFECT_MAJOR_PHYSICAL = new Bitvector(1, Bitvector.BV05);
        /// <summary>
        /// Protection from cold.
        /// </summary>
        public static readonly Bitvector AFFECT_PROTECT_COLD = new Bitvector(1, Bitvector.BV06);
        /// <summary>
        /// Protection from lightning.
        /// </summary>
        public static readonly Bitvector AFFECT_PROTECT_LIGHTNING = new Bitvector(1, Bitvector.BV07);
        /// <summary>
        /// Minor paralysis.
        /// </summary>
        public static readonly Bitvector AFFECT_MINOR_PARA = new Bitvector(1, Bitvector.BV08);
        /// <summary>
        /// Major paralysis.
        /// </summary>
        public static readonly Bitvector AFFECT_HOLD = new Bitvector(1, Bitvector.BV09);
        /// <summary>
        /// Attacks at half speed.
        /// </summary>
        public static readonly Bitvector AFFECT_SLOWNESS = new Bitvector(1, Bitvector.BV10);
        /// <summary>
        /// Immune to low and mid level spells.
        /// </summary>
        public static readonly Bitvector AFFECT_MAJOR_GLOBE = new Bitvector(1, Bitvector.BV11);
        /// <summary>
        /// Protection from gas.
        /// </summary>
        public static readonly Bitvector AFFECT_PROTECT_GAS = new Bitvector(1, Bitvector.BV12);
        /// <summary>
        /// Protection from acid.
        /// </summary>
        public static readonly Bitvector AFFECT_PROTECT_ACID = new Bitvector(1, Bitvector.BV13);
        /// <summary>
        /// Poisoned.
        /// </summary>
        public static readonly Bitvector AFFECT_POISON = new Bitvector(1, Bitvector.BV14);
        /// <summary>
        /// Alignment-based shield.
        /// </summary>
        public static readonly Bitvector AFFECT_SOULSHIELD = new Bitvector(1, Bitvector.BV15);
        /// <summary>
        /// Improved hide.
        /// </summary>
        public static readonly Bitvector AFFECT_DUERGAR_HIDE = new Bitvector(1, Bitvector.BV16);
        /// <summary>
        /// Concealed (invisible).
        /// </summary>
        public static readonly Bitvector AFFECT_MINOR_INVIS = new Bitvector(1, Bitvector.BV17);
        /// <summary>
        /// Vampiric touch, drains hitpoints from enemies during combat.
        /// </summary>
        public static readonly Bitvector AFFECT_VAMP_TOUCH = new Bitvector(1, Bitvector.BV18);
        /// <summary>
        /// Stunned.
        /// </summary>
        public static readonly Bitvector AFFECT_STUNNED = new Bitvector(1, Bitvector.BV19);
        /// <summary>
        /// Dropped primary weapon.
        /// </summary>
        public static readonly Bitvector AFFECT_DROPPED_PRIMARY = new Bitvector(1, Bitvector.BV20);
        /// <summary>
        /// Dropped secondary weapon.
        /// </summary>
        public static readonly Bitvector AFFECT_DROPPED_SECOND = new Bitvector(1, Bitvector.BV21);
        /// <summary>
        /// Fumbled primary weapon.
        /// </summary>
        public static readonly Bitvector AFFECT_FUMBLED_PRIMARY = new Bitvector(1, Bitvector.BV22);
        /// <summary>
        /// Fumbled secondary weapon.
        /// </summary>
        public static readonly Bitvector AFFECT_FUMBLED_SECOND = new Bitvector(1, Bitvector.BV23);
        /// <summary>
        /// Holding breath.
        /// </summary>
        public static readonly Bitvector AFFECT_HOLDING_BREATH = new Bitvector(1, Bitvector.BV24);
        /// <summary>
        /// Memorizing a spell.
        /// </summary>
        public static readonly Bitvector AFFECT_MEMORIZING = new Bitvector(1, Bitvector.BV25);
        /// <summary>
        /// Drowning.
        /// </summary>
        public static readonly Bitvector AFFECT_DROWNING = new Bitvector(1, Bitvector.BV26);
        /// <summary>
        /// Can walk through doors.
        /// </summary>
        public static readonly Bitvector AFFECT_PASS_DOOR = new Bitvector(1, Bitvector.BV27);
        /// <summary>
        /// Draining something from someone (blood, mana, etc.)
        /// </summary>
        public static readonly Bitvector AFFECT_DRAINING = new Bitvector(1, Bitvector.BV28);
        /// <summary>
        /// Casting a spell.
        /// </summary>
        public static readonly Bitvector AFFECT_CASTING = new Bitvector(1, Bitvector.BV29);
        /// <summary>
        /// Writing down a spell.
        /// </summary>
        public static readonly Bitvector AFFECT_SCRIBING = new Bitvector(1, Bitvector.BV30);
        /// <summary>
        /// Has an object for carrying heavy items.
        /// </summary>
        public static readonly Bitvector AFFECT_TENSORS_DISC = new Bitvector(2, Bitvector.BV00);
        /// <summary>
        /// Searching for a foe.
        /// </summary>
        public static readonly Bitvector AFFECT_TRACKING = new Bitvector(2, Bitvector.BV01);
        /// <summary>
        /// Singing a song.
        /// </summary>
        public static readonly Bitvector AFFECT_SINGING = new Bitvector(2, Bitvector.BV02);
        /// <summary>
        /// Ectoplasmic form.
        /// </summary>
        public static readonly Bitvector AFFECT_ECTOPLASMIC = new Bitvector(2, Bitvector.BV03);
        /// <summary>
        /// Absorbing (mana, etc.)
        /// </summary>
        public static readonly Bitvector AFFECT_ABSORBING = new Bitvector(2, Bitvector.BV04);
        /// <summary>
        /// Bitten by a vampire.
        /// </summary>
        public static readonly Bitvector AFFECT_VAMP_BITE = new Bitvector(2, Bitvector.BV05);
        /// <summary>
        /// Shaman protection.
        /// </summary>
        public static readonly Bitvector AFFECT_SPIRIT_WARD = new Bitvector(2, Bitvector.BV06);
        /// <summary>
        /// High level shaman protection.
        /// </summary>
        public static readonly Bitvector AFFECT_GREATER_SPIRIT_WARD = new Bitvector(2, Bitvector.BV07);
        /// <summary>
        /// Cannot be scryed or detected from afar.
        /// </summary>
        public static readonly Bitvector AFFECT_NON_DETECTION = new Bitvector(2, Bitvector.BV08);
        /// <summary>
        /// ???
        /// </summary>
        public static readonly Bitvector AFFECT_SILVER = new Bitvector(2, Bitvector.BV09);
        /// <summary>
        /// ???
        /// </summary>
        public static readonly Bitvector AFFECT_PLUS_ONE = new Bitvector(2, Bitvector.BV10);
        /// <summary>
        /// ???
        /// </summary>
        public static readonly Bitvector AFFECT_PLUS_TWO = new Bitvector(2, Bitvector.BV11);
        /// <summary>
        /// ???
        /// </summary>
        public static readonly Bitvector AFFECT_PLUS_THREE = new Bitvector(2, Bitvector.BV12);
        /// <summary>
        /// ???
        /// </summary>
        public static readonly Bitvector AFFECT_PLUS_FOUR = new Bitvector(2, Bitvector.BV13);
        /// <summary>
        /// ???
        /// </summary>
        public static readonly Bitvector AFFECT_PLUS_FIVE = new Bitvector(2, Bitvector.BV14);
        /// <summary>
        /// Larger than normal size.
        /// </summary>
        public static readonly Bitvector AFFECT_ENLARGED = new Bitvector(2, Bitvector.BV15);
        /// <summary>
        /// Smaller than normal size.
        /// </summary>
        public static readonly Bitvector AFFECT_REDUCED = new Bitvector(2, Bitvector.BV16);
        /// <summary>
        /// Harder to hit with projectiles.
        /// </summary>
        public static readonly Bitvector AFFECT_COVER = new Bitvector(2, Bitvector.BV17);
        /// <summary>
        /// Has four arms.
        /// </summary>
        public static readonly Bitvector AFFECT_FOUR_ARMS = new Bitvector(2, Bitvector.BV18);
        /// <summary>
        /// Inertial barrier psionic ability.
        /// </summary>
        public static readonly Bitvector AFFECT_INERTIAL_BARRIER = new Bitvector(2, Bitvector.BV19);
        /// <summary>
        /// Intellect fortress psionic ability.
        /// </summary>
        public static readonly Bitvector AFFECT_INTELLECT_FORTRESS = new Bitvector(2, Bitvector.BV20);
        /// <summary>
        /// Covered in a barrier of cold.
        /// </summary>
        public static readonly Bitvector AFFECT_COLDSHIELD = new Bitvector(2, Bitvector.BV21);
        /// <summary>
        /// Another kind of draining.
        /// </summary>
        public static readonly Bitvector AFFECT_CANNIBALIZING = new Bitvector(2, Bitvector.BV22);
        /// <summary>
        /// Swimming.
        /// </summary>
        public static readonly Bitvector AFFECT_SWIMMING = new Bitvector(2, Bitvector.BV23);
        /// <summary>
        /// Psionic tower of iron will ability.
        /// </summary>
        public static readonly Bitvector AFFECT_TOWER_OF_IRON_WILL = new Bitvector(2, Bitvector.BV24);
        /// <summary>
        /// Is underwater.
        /// </summary>
        public static readonly Bitvector AFFECT_UNDERWATER = new Bitvector(2, Bitvector.BV25);
        /// <summary>
        /// Ranger "blur" ability.
        /// </summary>
        public static readonly Bitvector AFFECT_BLUR = new Bitvector(2, Bitvector.BV26);
        /// <summary>
        /// Vampiric bite.
        /// </summary>
        public static readonly Bitvector AFFECT_NECKBITING = new Bitvector(2, Bitvector.BV27);
        /// <summary>
        /// In elemental form.
        /// </summary>
        public static readonly Bitvector AFFECT_ELEMENTAL_FORM = new Bitvector(2, Bitvector.BV28);
        /// <summary>
        /// Does not leave tracks.
        /// </summary>
        public static readonly Bitvector AFFECT_PASS_WITHOUT_TRACE = new Bitvector(2, Bitvector.BV29);
        /// <summary>
        /// Paladin aura of goodness.
        /// </summary>
        public static readonly Bitvector AFFECT_PALADIN_AURA = new Bitvector(2, Bitvector.BV30);
        /// <summary>
        /// Looted someone recently.
        /// </summary>
        public static readonly Bitvector AFFECT_LOOTER = new Bitvector(3, Bitvector.BV00);
        /// <summary>
        /// Diseased.
        /// </summary>
        public static readonly Bitvector AFFECT_DISEASE = new Bitvector(3, Bitvector.BV01);
        /// <summary>
        /// Looting an enemy guildhall.
        /// </summary>
        public static readonly Bitvector AFFECT_SACKING = new Bitvector(3, Bitvector.BV02);
        /// <summary>
        /// Can sense own followers.
        /// </summary>
        public static readonly Bitvector AFFECT_SENSE_FOLLOWER = new Bitvector(3, Bitvector.BV03);
        /// <summary>
        /// Absorptive protection spheres.
        /// </summary>
        public static readonly Bitvector AFFECT_STORNOG_SPHERES = new Bitvector(3, Bitvector.BV04);
        /// <summary>
        /// Greater absorptive protection spheres.
        /// </summary>
        public static readonly Bitvector AFFECT_GREATER_SPHERES = new Bitvector(3, Bitvector.BV05);
        /// <summary>
        /// Vampiric form.
        /// </summary>
        public static readonly Bitvector AFFECT_VAMPIRE_FORM = new Bitvector(3, Bitvector.BV06);
        /// <summary>
        /// Cannot un-polymorph.
        /// </summary>
        public static readonly Bitvector AFFECT_NO_UNMORPH = new Bitvector(3, Bitvector.BV07);
        /// <summary>
        /// Holy sacrifice ability.
        /// </summary>
        public static readonly Bitvector AFFECT_HOLY_SACRIFICE = new Bitvector(3, Bitvector.BV08);
        /// <summary>
        /// Battle ecstasy ability.
        /// </summary>
        public static readonly Bitvector AFFECT_BATTLE_ECSTASY = new Bitvector(3, Bitvector.BV09);
        /// <summary>
        /// Dazzle ability.
        /// </summary>
        public static readonly Bitvector AFFECT_DAZZLE = new Bitvector(3, Bitvector.BV10);
        /// <summary>
        /// Stun-blinded by dazzle.
        /// </summary>
        public static readonly Bitvector AFFECT_DAZZLED = new Bitvector(3, Bitvector.BV11);
        /// <summary>
        /// Throat has been crushed, cannot speak.
        /// </summary>
        public static readonly Bitvector AFFECT_THROAT_CRUSH = new Bitvector(3, Bitvector.BV12);
        /// <summary>
        /// Healing quickly.
        /// </summary>
        public static readonly Bitvector AFFECT_REGENERATION = new Bitvector(3, Bitvector.BV13);
        /// <summary>
        /// Being squeezed.
        /// </summary>
        public static readonly Bitvector AFFECT_BEARHUG = new Bitvector(3, Bitvector.BV14);
        /// <summary>
        /// Winner in a grapple, holding opponent.
        /// </summary>
        public static readonly Bitvector AFFECT_GRAPPLING = new Bitvector(3, Bitvector.BV15);
        /// <summary>
        /// Loser in a grapple, being held by opponent.
        /// </summary>
        public static readonly Bitvector AFFECT_GRAPPLED = new Bitvector(3, Bitvector.BV16);
        /// <summary>
        /// Has a magical torch.
        /// </summary>
        public static readonly Bitvector AFFECT_MAGE_FLAME = new Bitvector(3, Bitvector.BV17);
        /// <summary>
        /// Cannot be immolated.
        /// </summary>
        public static readonly Bitvector AFFECT_NO_IMMOLATE = new Bitvector(3, Bitvector.BV18);
        /// <summary>
        /// Is multi-classed.
        /// </summary>
        public static readonly Bitvector AFFECT_MULTICLASS = new Bitvector(3, Bitvector.BV19);
        /// <summary>
        /// Can detect undead.
        /// </summary>
        public static readonly Bitvector AFFECT_DETECT_UNDEAD = new Bitvector(3, Bitvector.BV20);
        // Internal-only values.  Whether intentional or just not yet added to DikuEdit, they are
        // only settable within the game and typically used for spell affects.
        /// <summary>
        /// Is actively fleeing.
        /// </summary>
        public static readonly Bitvector AFFECT_IS_FLEEING = new Bitvector(4, Bitvector.BV00);
        /// <summary>
        /// Hunting a foe.
        /// </summary>
        public static readonly Bitvector AFFECT_HUNTING = new Bitvector(4, Bitvector.BV01);
        /// <summary>
        /// Has psionicist biofeedback active.
        /// </summary>
        public static readonly Bitvector AFFECT_BIOFEEDBACK = new Bitvector(4, Bitvector.BV02);
        /// <summary>
        /// Famine spell (increased hunger curse) active.
        /// </summary>
        public static readonly Bitvector AFFECT_FAMINE = new Bitvector(4, Bitvector.BV03);
        /// <summary>
        /// Cannot speak.
        /// </summary>
        public static readonly Bitvector AFFECT_MUTE = new Bitvector(4, Bitvector.BV04);
        /// <summary>
        /// Covered in faerie fire and easier to hit.
        /// </summary>
        public static readonly Bitvector AFFECT_FAERIE_FIRE = new Bitvector(4, Bitvector.BV05);
        /// <summary>
        /// Sanctuary spell active.
        /// </summary>
        public static readonly Bitvector AFFECT_SANCTUARY = new Bitvector(4, Bitvector.BV06);
        /// <summary>
        /// Sex has been changed.
        /// </summary>
        public static readonly Bitvector AFFECT_CHANGE_SEX = new Bitvector(4, Bitvector.BV07);
        /// <summary>
        /// Cursed.
        /// </summary>
        public static readonly Bitvector AFFECT_CURSE = new Bitvector(4, Bitvector.BV08);
        /// <summary>
        /// Can see hidden.
        /// </summary>
        public static readonly Bitvector AFFECT_DETECT_HIDDEN = new Bitvector(4, Bitvector.BV09);
        /// <summary>
        /// Polymorphed.
        /// </summary>
        public static readonly Bitvector AFFECT_POLYMORPH = new Bitvector(4, Bitvector.BV10);
        /// <summary>
        /// Understands all languages.
        /// </summary>
        public static readonly Bitvector AFFECT_COMP_LANG = new Bitvector(4, Bitvector.BV11);
        /// <summary>
        /// Immune to elemental earth attacks.
        /// </summary>
        public static readonly Bitvector AFFECT_DENY_EARTH = new Bitvector(4, Bitvector.BV12);
        /// <summary>
        /// Immune to elemental air attacks.
        /// </summary>
        public static readonly Bitvector AFFECT_DENY_AIR = new Bitvector(4, Bitvector.BV13);
        /// <summary>
        /// Immune to elemental fire attacks.
        /// </summary>
        public static readonly Bitvector AFFECT_DENY_FIRE = new Bitvector(4, Bitvector.BV14);
        /// <summary>
        /// Immune to elemental water attacks.
        /// </summary>
        public static readonly Bitvector AFFECT_DENY_WATER = new Bitvector(4, Bitvector.BV15);
        /// <summary>
        /// Tracking.
        /// </summary>
        public static readonly Bitvector AFFECT_TRACK = new Bitvector(4, Bitvector.BV16);
        /// <summary>
        /// Is a "justice tracker" - can track perfectly in a justice area.
        /// </summary>
        public static readonly Bitvector AFFECT_JUSTICE_TRACKER = new Bitvector(4, Bitvector.BV17);
        /// <summary>
        /// Has Paladin "lay on hands" timer.
        /// </summary>
        public static readonly Bitvector AFFECT_LAYHANDS_TIMER = new Bitvector(4, Bitvector.BV18);
        /// <summary>
        /// Has elemental sight.
        /// </summary>
        public static readonly Bitvector AFFECT_ELEM_SIGHT = new Bitvector(4, Bitvector.BV19);
        // Bitvector.BV20 unused.
        /// <summary>
        /// Misdirection - directionally challenged.
        /// </summary>
        public static readonly Bitvector AFFECT_MISDIRECTION = new Bitvector(4, Bitvector.BV21);
        /// <summary>
        /// Affected by the vacancy spell.
        /// </summary>
        public static readonly Bitvector AFFECT_VACANCY = new Bitvector(4, Bitvector.BV22);
        /// <summary>
        /// Appearance altered.
        /// </summary>
        public static readonly Bitvector AFFECT_CHANGE_SELF = new Bitvector(4, Bitvector.BV23);
        /// <summary>
        /// Looks tougher than they are.
        /// </summary>
        public static readonly Bitvector AFFECT_PROWESS = new Bitvector(4, Bitvector.BV24);
        /// <summary>
        /// Can't summon a new mount for a while.
        /// </summary>
        public static readonly Bitvector AFFECT_SUMMON_MOUNT_TIMER = new Bitvector(4, Bitvector.BV25);
        /// <summary>
        /// Looks more inept than they are.
        /// </summary>
        public static readonly Bitvector AFFECT_INCOMPETENCE = new Bitvector(4, Bitvector.BV26);
        /// <summary>
        /// Is busy climbing.
        /// </summary>
        public static readonly Bitvector AFFECT_CLIMBING = new Bitvector(4, Bitvector.BV27);
        /// <summary>
        /// Affected by a protection ritual.
        /// </summary>
        public static readonly Bitvector AFFECT_RITUAL_OF_PROTECTION = new Bitvector(4, Bitvector.BV28);
        /// <summary>
        /// Enhanced dexterity.
        /// </summary>
        public static readonly Bitvector AFFECT_COORDINATION = new Bitvector(4, Bitvector.BV29);
        /// <summary>
        /// Enhanced charisma.
        /// </summary>
        public static readonly Bitvector AFFECT_CHARM_OTTER = new Bitvector(4, Bitvector.BV30);
        /// <summary>
        /// Enhanced movement points.
        /// </summary>
        public static readonly Bitvector AFFECT_ENDURANCE = new Bitvector(5, Bitvector.BV00);
        /// <summary>
        /// Enhanced constitution.
        /// </summary>
        public static readonly Bitvector AFFECT_FORTITUDE = new Bitvector(5, Bitvector.BV01);
        /// <summary>
        /// Enhanced wisdom.
        /// </summary>
        public static readonly Bitvector AFFECT_INSIGHT = new Bitvector(5, Bitvector.BV02);
        /// <summary>
        /// Enhanced strength.
        /// </summary>
        public static readonly Bitvector AFFECT_MIGHT = new Bitvector(5, Bitvector.BV03);
        /// <summary>
        /// Enhanced intelligence.
        /// </summary>
        public static readonly Bitvector AFFECT_SAVVY = new Bitvector(5, Bitvector.BV04);
        /// <summary>
        /// Thirsty.
        /// </summary>
        public static readonly Bitvector AFFECT_THIRST = new Bitvector(5, Bitvector.BV05);
        /// <summary>
        /// Hungry.
        /// </summary>
        public static readonly Bitvector AFFECT_HUNGER = new Bitvector(5, Bitvector.BV06);
        /// <summary>
        /// Extra hitpoints.
        /// </summary>
        public static readonly Bitvector AFFECT_VITALITY = new Bitvector(5, Bitvector.BV07);
        /// <summary>
        /// Protection from undead.
        /// </summary>
        public static readonly Bitvector AFFECT_PROTECT_FROM_UNDEAD = new Bitvector(5, Bitvector.BV08);
        /// <summary>
        /// Berzerking.
        /// </summary>
        public static readonly Bitvector AFFECT_BERZERK = new Bitvector(5, Bitvector.BV09);
        /// <summary>
        /// Armor spell effect.
        /// </summary>
        public static readonly Bitvector AFFECT_ARMOR = new Bitvector(5, Bitvector.BV10);
        /// <summary>
        /// Brain-dead.
        /// </summary>
        public static readonly Bitvector AFFECT_FEEBLEMIND = new Bitvector(5, Bitvector.BV11);
        /// <summary>
        /// Can't see very well.
        /// </summary>
        public static readonly Bitvector AFFECT_VISION_IMPAIRED = new Bitvector(5, Bitvector.BV12);
        /// <summary>
        /// Enhanced vision.
        /// </summary>
        public static readonly Bitvector AFFECT_VISION_ENHANCED = new Bitvector(5, Bitvector.BV13);
        /// <summary>
        /// Increased strength.
        /// </summary>
        public static readonly Bitvector AFFECT_STRENGTH_INCREASED = new Bitvector(5, Bitvector.BV14);
        /// <summary>
        /// Decreased strength.
        /// </summary>
        public static readonly Bitvector AFFECT_STRENGTH_REDUCED = new Bitvector(5, Bitvector.BV15);
        /// <summary>
        /// Immune to fear.
        /// </summary>
        public static readonly Bitvector AFFECT_BRAVE = new Bitvector(5, Bitvector.BV16);
        /// <summary>
        /// Afraid.
        /// </summary>
        public static readonly Bitvector AFFECT_COWARDLY = new Bitvector(5, Bitvector.BV17);
        /// <summary>
        /// Increased movement points.
        /// </summary>
        public static readonly Bitvector AFFECT_MOVEMENT_INCREASED = new Bitvector(5, Bitvector.BV18);
        /// <summary>
        /// Decreased movement points.
        /// </summary>
        public static readonly Bitvector AFFECT_MOVEMENT_REDUCED = new Bitvector(5, Bitvector.BV19);
        /// <summary>
        /// Electrical shield.
        /// </summary>
        public static readonly Bitvector AFFECT_SHOCK_SHIELD = new Bitvector(5, Bitvector.BV20);
        /// <summary>
        /// Aged.
        /// </summary>
        public static readonly Bitvector AFFECT_WITHER = new Bitvector(5, Bitvector.BV21);
        /// <summary>
        /// Blessed.
        /// </summary>
        public static readonly Bitvector AFFECT_BLESS = new Bitvector(5, Bitvector.BV22);
        /// <summary>
        /// Increased dexterity.
        /// </summary>
        public static readonly Bitvector AFFECT_DEXTERITY_INCREASED = new Bitvector(5, Bitvector.BV23);
        /// <summary>
        /// Increased charisma.
        /// </summary>
        public static readonly Bitvector AFFECT_CHARISMA_INCREASED = new Bitvector(5, Bitvector.BV24);
    }
}