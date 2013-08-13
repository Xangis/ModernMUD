using System;

namespace ModernMUD
{
    /// <summary>
    /// Represents a trap.
    ///
    /// Trap functions are actually spell functions, but with the prefix
    /// "trap" before them.  They are always objects nailing the character
    /// with nasty stuff, but they are exactly like spell functions in every
    /// other respect.
    /// </summary>
    public class Trap
    {
        private static int _numTraps;
        private TriggerType _trigger;
        private TrapType _damage;
        private int _charges;
        private int _level;
        private int _percent;

        // Common traps
        // TODO: Make these non-constant.  A trap's variables should contain
        // all of the information needed for it to deliver its damage, spell
        // payload, or any other effects.
        //
        // I'm not even sure it's actually necessary to define these anymore.
        public static int TrpAcid;
        public static int TrpBash;
        public static int TrpCold;
        public static int TrpDisease;
        public static int TrpDispel;
        public static int TrpEnergy;
        public static int TrpFire;
        public static int TrpGate;
        public static int TrpHarm;
        public static int TrpPara;
        public static int TrpPierce;
        public static int TrpPoison;
        public static int TrpSlash;
        public static int TrpSleep;
        public static int TrpStun;
        public static int TrpSummon;
        public static int TrpTeleport;
        public static int TrpWither;

        /// <summary>
        /// Trap types.
        /// </summary>
        public enum TrapType
        {
            sleep = 0,
            teleport,
            fire,
            cold,
            acid,
            energy,
            blunt,
            piercing,
            slashing,
            dispel,
            gate,
            summon,
            wither,
            harm,
            poison,
            paralysis,
            stun,
            disease
        }
        public static readonly int MAX_TRAP = Enum.GetValues(typeof(TrapType)).Length;

        /// <summary>
        /// Represnts the various ways that a trap can be triggered.  Traps can have multiple
        /// trigger types.
        /// </summary>
        [Flags]
        public enum TriggerType
        {
            none = 0,
            move = Bitvector.BV00, // Movement in any direction
            get_put = Bitvector.BV01, // Get, put, or drop.
            full_room = Bitvector.BV02,
            north = Bitvector.BV03,
            east = Bitvector.BV04,
            south = Bitvector.BV05,
            west = Bitvector.BV06,
            up = Bitvector.BV07,
            down = Bitvector.BV08,
            open = Bitvector.BV09,
            unlimited = Bitvector.BV10,
            glyph = Bitvector.BV11,
            wear = Bitvector.BV12,
            unequip = Bitvector.BV13,
            steal = Bitvector.BV14,
            enchant = Bitvector.BV15,
            uncurse = Bitvector.BV16,
            search = Bitvector.BV17,
            close = Bitvector.BV18,
            spell_cast = Bitvector.BV19,
            voice = Bitvector.BV20, // Speech or songs.
            climb = Bitvector.BV21,
            open_flame = Bitvector.BV22, // Flammable
            northwest = Bitvector.BV23,
            southwest = Bitvector.BV24,
            northeast = Bitvector.BV25,
            southeast = Bitvector.BV26,
            goodie_in_room = Bitvector.BV27, // Good racewar presence
            evil_in_room = Bitvector.BV28, // Evil racewar presence
            throw_shoot = Bitvector.BV29,
            destroy = Bitvector.BV30 // Item is disintegrated, burned, shattered, or otherwise destroyed.
        }

        /// <summary>
        /// Constructor.  Increments the total number of in-game traps.
        /// </summary>
        public Trap()
        {
            ++_numTraps;
        }

        /// <summary>
        /// Destructor.  Decrements the total number of in-game traps on destruction.
        /// </summary>
        ~Trap()
        {
            --_numTraps;
        }

        /// <summary>
        /// Gets the number of traps currently in-game.
        /// </summary>
        public static int Count
        {
            get
            {
                return _numTraps;
            }
        }

        /// <summary>
        /// How the trap is triggered.
        /// </summary>
        public TriggerType Trigger
        {
            get { return _trigger; }
            set { _trigger = value; }
        }

        /// <summary>
        /// Type of damage inflicted.
        /// </summary>
        public TrapType Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        /// <summary>
        /// Number of times the trap can go off.
        /// </summary>
        public int Charges
        {
            get { return _charges; }
            set { _charges = value; }
        }

        /// <summary>
        /// Level of the trap. Used for calculating damage and avoidance.
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// Percent change that the trap will trigger when its activation criteria is met.
        /// </summary>
        public int Percent
        {
            get { return _percent; }
            set { _percent = value; }
        }

        /// <summary>
        /// Checks whether the trap still has charges and whether it can be activated by
        /// the specified trigger type.
        /// </summary>
        /// <param name="triggerType"></param>
        /// <returns></returns>
        public bool CheckTrigger(TriggerType triggerType)
        {
            if (_charges == 0)
            {
                return false;
            }
            if ((_trigger & triggerType) != 0)
            {
                return true;
            }
            return false;
        }

    }
}