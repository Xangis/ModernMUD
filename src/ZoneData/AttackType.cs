
namespace ModernMUD
{
    /// <summary>
    /// Represents a physical attack.
    /// </summary>
    public class AttackType
    {
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="skill"></param>
        /// <param name="damtype"></param>
        AttackType(string nam, string skill, DamageType damtype)
        {
            Name = nam;
            SkillName = skill;
            DamageInflicted = damtype;
        }

        /// <summary>
        /// Name of the attack type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the applicable skill.
        /// </summary>
        public string SkillName { get; set; }

        /// <summary>
        /// Damage type for the attack.
        /// </summary>
        public DamageType DamageInflicted { get; set; }

        /// <summary>
        /// Damage types for spells and combat.
        /// </summary>
        public enum DamageType
        {
            none = 0,
            bludgeon,
            pierce,
            slash,
            magic_other,
            fire,
            cold,
            electricity,
            acid,
            poison,
            charm,
            mental,
            energy,
            white_magic,
            black_magic,
            disease,
            drowning,
            light,
            sound,
            harm,
            disintegration,
            gas,
            asphyxiation,
            wind,
            crushing,
            earth, // When it absolutely *has* to be elemental earth damage
            water // When it absolutely *has* to be elemental water damage
        }

        /// <summary>
        /// Attack types: damage string, damage type, and related skill
        /// </summary>
        public static AttackType[] Table = new[]
        {
            new AttackType(  "hit",      "barehanded fighting", DamageType.bludgeon  ),  //  0
            new AttackType(  "slice",    "1h slashing",         DamageType.slash     ),  //  1
            new AttackType(  "stab",     "1h piercing",         DamageType.pierce    ),
            new AttackType(  "slash",    "1h slashing",         DamageType.slash     ),
            new AttackType(  "whip",     "1h whip",             DamageType.slash     ),
            new AttackType(  "claw",     "1h slashing",	        DamageType.slash     ),  //  5
            new AttackType(  "blast",    "1h bludgeoning",      DamageType.bludgeon  ),
            new AttackType(  "pound",    "1h bludgeoning",      DamageType.bludgeon  ),
            new AttackType(  "crush",    "1h bludgeoning",      DamageType.bludgeon  ),
            new AttackType(  "puncture", "1h piercing",         DamageType.pierce    ),
            new AttackType(  "bite",     "1h piercing",         DamageType.pierce    ),  //  10
            new AttackType(  "pierce",   "1h piercing",         DamageType.pierce    ),
            new AttackType(  "smash",    "1h bludgeoning",      DamageType.bludgeon  ),
            new AttackType(  "chop",     "1h slashing",         DamageType.slash     ),
            new AttackType(  "cut",      "1h slashing",         DamageType.slash     ),
            new AttackType(  "cleave",   "1h slashing",         DamageType.slash     ),  //  15
            new AttackType(  "wail",     "barehanded fighting", DamageType.bludgeon  ),
            new AttackType(  "punch",    "barehanded fighting", DamageType.bludgeon  )
        };

    };
}
