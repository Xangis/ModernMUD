using System;

namespace MUDEngine
{
    /// <summary>
    /// Keeps track of aggression and fear data.
    /// </summary>
    [Serializable]
    public class EnemyData
    {
        /// <summary>
        /// Name of the enemy.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Target enemy.
        /// </summary>
        public CharData Who { get; set; }
        /// <summary>
        /// For future use in keeping track of *how much* a mob hates a particular target.
        /// This could be useful for keeping track of who the mob wants to switch to. 
        /// </summary>
        public int HateLevel { get; set; }
        /// <summary>
        /// For future use in keeping track of how much the enemy is fearing the target.
        /// </summary>
        public int FearLevel { get; set; }

        /// <summary>
        /// Lets us use if( EnemyData ) to check for null.
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public static implicit operator bool( EnemyData enemy )
        {
            if (enemy == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnemyData()
        {
            Name = String.Empty;
            Who = null;
            HateLevel = 0;
        }
    }
}