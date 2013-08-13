using System;

namespace ZoneData
{
    /// <summary>
    /// Represents a spell ability.  Intended for use on objects
    /// </summary>
    public class SpellAbility
    {
        /// <summary>
        /// The way in which a spell ability is activated.
        /// </summary>
        public enum ActivationType
        {
            /// <summary>
            /// Happens at non-regular time intervals.
            /// </summary>
            RandomInterval,
            /// <summary>
            /// Invoked via a specific command.
            /// </summary>
            CommandInvoked,
            /// <summary>
            /// Happens randomly during normal use.
            /// </summary>
            RandomUse,
            /// <summary>
            /// Always active.
            /// </summary>
            AlwaysActive, 
            /// <summary>
            /// Can be used at will, no command/action necessary.
            /// </summary>
            AtWill,
            /// <summary>
            /// Invoked via a specific action.
            /// </summary>
            ActionInvoked
        }

        /// <summary>
        /// Level of power.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Number of charges, -1 for infinite.
        /// </summary>
        public int NumCharges { get; set; }

        /// <summary>
        /// Method of activating this spell ability.
        /// </summary>
        public ActivationType Type { get; set; }

        /// <summary>
        /// Percent chance of activating, where applicable.
        /// </summary>
        public int PercentChance { get; set; }

        /// <summary>
        /// Minimum amount of time between activations, if any.
        /// </summary>
        public TimeSpan MinIntervalBetweenUses { get; set; }
    }
}
