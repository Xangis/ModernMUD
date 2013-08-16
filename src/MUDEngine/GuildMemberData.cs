using System;

namespace MUDEngine
{
    /// <summary>
    /// Represents a single guild member.
    /// </summary>
    [Serializable]
    public class GuildMemberData
    {
        public string Name { get; set; }
        public Guild.Rank Rank { get; set; }
        public int Fine { get; set; }
        public DateTime JoinTime { get; set; }
        public bool Filled { get; set; }
    };
}
