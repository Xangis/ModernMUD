using System;
using System.Collections.Generic;
using System.Text;

namespace MUDEngine
{
    /// <summary>
    /// Target types.  Used for determining AI use and target finding for spells.
    /// </summary>
    public enum TargetType
    {
        /// <summary>
        /// Target-less.
        /// </summary>
        none = 0,
        /// <summary>
        /// Can only target yourself.
        /// </summary>
        self,
        /// <summary>
        /// Single-target offensive.
        /// </summary>
        singleCharacterOffensive,
        /// <summary>
        /// Multi-target offensive.
        /// </summary>
        multipleCharacterOffensive,
        /// <summary>
        /// Single-target defensive.
        /// </summary>
        singleCharacterDefensive,
        /// <summary>
        /// Single-target ranged offensive.
        /// </summary>
        singleCharacterRanged,
        /// <summary>
        /// Single-target, infinite range.
        /// </summary>
        singleCharacterWorld,
        /// <summary>
        /// Carried object is target.
        /// </summary>
        objectInInventory,
        /// <summary>
        /// An object in the room is target.
        /// </summary>
        objectInRoom,
        /// <summary>
        /// A corpse is target.
        /// </summary>
        objectCorpse,
        /// <summary>
        /// Can target an object or character.
        /// </summary>
        objectOrCharacter,
        /// <summary>
        /// A trap.
        /// </summary>
        trap,
        /// <summary>
        /// A ritual involving more than one caster.
        /// </summary>
        ritual,
    };
}
