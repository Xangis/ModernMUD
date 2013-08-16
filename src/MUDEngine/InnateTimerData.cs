namespace MUDEngine
{
    /// <summary>
    /// Used for keeping track of the re-use timer on an innate ability.
    /// </summary>
    public class InnateTimerData
    {
        public InnateTimerData.Type AbilityType { get; set; }
        public int Timer { get; set; }
        public CharData Who { get; set; }

        /// <summary>
        /// Innate ability types.  Used for innate timers.
        /// </summary>
        public enum Type
        {
            strength,
            invisibility,
            faerie_fire,
            enlarge,
            levitate,
            darkness,
            light,
            remove_poison,
            battle_roar,
            chameleon,
            shift_astral,
            shift_prime
        }

        public static implicit operator bool( InnateTimerData itd )
        {
            if( itd == null )
                return false;
            return true;
        }
    };

}