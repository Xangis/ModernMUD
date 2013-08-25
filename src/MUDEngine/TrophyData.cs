using System;
namespace MUDEngine
{
    /// <summary>
    /// Used to keep track of the number of times a player has killed a particular mob type.
    /// </summary>
    [Serializable]
    public class TrophyData
    {
        /// <summary>
        /// The index number of the mob being counted.
        /// </summary>
        public int MobIndexNumber { get; set; }
        
        /// <summary>
        /// Number killed, in hundredths.  100 = one kill.
        /// </summary>
        public int NumberKilled { get; set; }
    }
}