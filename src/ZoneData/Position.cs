
namespace ModernMUD
{
    /// <summary>
    /// Represents positions for a living being.
    /// </summary>
    public class Position
    {
        // TODO: Convert all integer values to enum values.
        /// <summary>
        /// Corpse.
        /// </summary>
        public const int dead = 0;
        /// <summary>
        /// Mortally wounded, and will bleed to death soon.
        /// </summary>
        public const int mortally_wounded = 1;
        /// <summary>
        /// Mortally wounded, may or may not bleed to death.
        /// </summary>
        public const int incapacitated = 2;
        /// <summary>
        /// Knocked out.
        /// </summary>
        public const int unconscious = 3;
        /// <summary>
        /// Stunned, almost unconscious but able to observe.
        /// </summary>
        public const int stunned = 4;
        /// <summary>
        /// Asleep.
        /// </summary>
        public const int sleeping = 5;
        /// <summary>
        /// On your back.
        /// </summary>
        public const int reclining = 6;
        /// <summary>
        /// Sitting, relaxed.
        /// </summary>
        public const int resting = 7;
        /// <summary>
        /// Sitting down, alert.
        /// </summary>
        public const int sitting = 8;
        /// <summary>
        /// On your knees.
        /// </summary>
        public const int kneeling = 9;
        /// <summary>
        /// Upright, busy with a fight.
        /// </summary>
        public const int fighting = 10;
        /// <summary>
        /// Upright and alert.
        /// </summary>
        public const int standing = 11;

        /// <summary>
        /// Gets a position value based on the supplied string.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int PositionLookup(string position)
        {
            switch (position)
            {
                case "standing":
                    return standing;
                case "fighting":
                    return fighting;
                case "kneeling":
                    return kneeling;
                case "sitting":
                    return sitting;
                case "resting":
                    return resting;
                case "reclining":
                    return reclining;
                case "sleeping":
                    return sleeping;
                case "lying stunned":
                    return stunned;
                case "lying unconscious":
                    return unconscious;
                case "incapacitated":
                    return incapacitated;
                case "mortally wounded":
                    return mortally_wounded;
                case "dead":
                    return dead;
            }
            return standing;
        }

        /// <summary>
        /// Converts a position value to a string.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string PositionString( int position )
        {
            switch (position)
            {
                default:
                    return "unknown";
                case standing:
                    return "standing";
                case fighting:
                    return "standing";
                case kneeling:
                    return "kneeling";
                case sitting:
                    return "sitting";
                case resting:
                    return "resting";
                case reclining:
                    return "reclining";
                case sleeping:
                    return "sleeping";
                case stunned:
                    return "lying stunned";
                case unconscious:
                    return "lying unconscious";
                case incapacitated:
                    return "incapacitated";
                case mortally_wounded:
                    return "mortally wounded";
                case dead:
                    return "dead";
            }
        }
    }
}
