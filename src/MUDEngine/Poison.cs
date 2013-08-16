namespace MUDEngine
{
    public class Poison
    {
        /// <summary>
        /// Poison types.
        /// </summary>
        public enum Type
        {
            none = 0,
            damage,
            attributes,
            damage_major,
            minor_para,
            minor_para_extended,
            major_para,
            major_para_extended,
            perm_constitution,
            perm_hitpoints,
            near_death,
            reserved,
            generic,
            minor_damage
        }
    }
}