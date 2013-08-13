using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Represents a skill entry in a player's skill list.
    /// </summary>
    public class SkillEntry
    {
        /// <summary>
        /// Level of ability with the skill.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Skill's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SkillEntry()
        {
            Level = 1;
            Name = string.Empty;
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        public SkillEntry( string name, int level )
        {
            Name = name;
            Level = level;
        }
    }
}
