using System;
namespace MUDEngine
{
    [Serializable]
    public class TrophyData
    {
        public int MobIndexNumber { get; set; }
        /// <summary>
        /// Number killed, in hundredths.  100 = one kill.
        /// </summary>
        public int NumberKilled { get; set; }
    };
}