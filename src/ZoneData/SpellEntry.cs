using System;

namespace ModernMUD
{
    /// <summary>
    /// Represents a spell entry in a player's spellbook or in the definition of
    /// the available spells for a character class.
    /// </summary>
    [Serializable]
    public class SpellEntry
    {
        /// <summary>
        /// Represents the name of the spell.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents the spell circle of a spell entry.
        /// </summary>
        public int Circle { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SpellEntry()
        {
            Name = String.Empty;
            Circle = Limits.MAX_CIRCLE;
        }

        /// <summary>
        /// Value constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="circle"></param>
        public SpellEntry( string name, int circle )
        {
            Name = name;
            Circle = circle;
        }
    }
}
