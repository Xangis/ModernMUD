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
            /// <summary>
            /// Cannot be triggered.
            /// </summary>
            none = 0,
            /// <summary>
            /// Movement in any direction
            /// </summary>
            move = Bitvector.BV00,
            /// <summary>
            /// Get, put, or drop.
            /// </summary>
            get_put = Bitvector.BV01,
            /// <summary>
            /// When the room is full.
            /// </summary>
            full_room = Bitvector.BV02,
            /// <summary>
            /// Movement north.
            /// </summary>
            north = Bitvector.BV03,
            /// <summary>
            /// Movement east.
            /// </summary>
            east = Bitvector.BV04,
            /// <summary>
            /// Movement south.
            /// </summary>
            south = Bitvector.BV05,
            /// <summary>
            /// Movement west.
            /// </summary>
            west = Bitvector.BV06,
            /// <summary>
            /// Movement up.
            /// </summary>
            up = Bitvector.BV07,
            /// <summary>
            /// Movement down.
            /// </summary>
            down = Bitvector.BV08,
            /// <summary>
            /// Opening it.
            /// </summary>
            open = Bitvector.BV09,
            /// <summary>
            /// Everything triggers this trap.
            /// </summary>
            unlimited = Bitvector.BV10,
            glyph = Bitvector.BV11,
            /// <summary>
            /// Wearing or equipping it.
            /// </summary>
            wear = Bitvector.BV12,
            /// <summary>
            /// Unwearing or unequipping it.
            /// </summary>
            unequip = Bitvector.BV13,
            /// <summary>
            /// Only when stolen.
            /// </summary>
            steal = Bitvector.BV14,
            /// <summary>
            /// When someone tries to enchant it.
            /// </summary>
            enchant = Bitvector.BV15,
            /// <summary>
            /// When someone tries to cast remove curse on it.
            /// </summary>
            uncurse = Bitvector.BV16,
            /// <summary>
            /// When someone searches.
            /// </summary>
            search = Bitvector.BV17,
            /// <summary>
            /// When closed.
            /// </summary>
            close = Bitvector.BV18,
            /// <summary>
            /// When a spell is cast.
            /// </summary>
            spell_cast = Bitvector.BV19,
            /// <summary>
            /// Triggered by speech or songs.
            /// </summary>
            voice = Bitvector.BV20,
            /// <summary>
            /// When it's climbed.
            /// </summary>
            climb = Bitvector.BV21,
            /// <summary>
            /// When an open flame is present (flammable).
            /// </summary>
            open_flame = Bitvector.BV22,
            /// <summary>
            /// Movement northwest.
            /// </summary>
            northwest = Bitvector.BV23,
            /// <summary>
            /// Movement southwest.
            /// </summary>
            southwest = Bitvector.BV24,
            /// <summary>
            /// Movement northeast.
            /// </summary>
            northeast = Bitvector.BV25,
            /// <summary>
            /// Movement southeast.
            /// </summary>
            southeast = Bitvector.BV26,
            /// <summary>
            /// Triggered by good racewar presence.
            /// </summary>
            goodie_in_room = Bitvector.BV27,
            /// <summary>
            /// Triggered by evil racewar presence.
            /// </summary>
            evil_in_room = Bitvector.BV28,
            /// <summary>
            /// When it is thrown or shot.
            /// </summary>
            throw_shoot = Bitvector.BV29,
            /// <summary>
            /// When the item is disintegrated, burned, shattered, or otherwise destroyed.
            /// </summary>
            destroy = Bitvector.BV30
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