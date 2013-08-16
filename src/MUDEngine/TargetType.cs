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
        none = 0,
        self,
        singleCharacterOffensive,
        multipleCharacterOffensive,
        singleCharacterDefensive,
        singleCharacterRanged,
        singleCharacterWorld,
        objectInInventory,
        objectInRoom,
        objectCorpse,
        objectOrCharacter,
        trap,
        ritual,
    };
}
