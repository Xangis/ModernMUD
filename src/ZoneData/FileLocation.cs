using System.IO;

namespace ModernMUD
{
    /// <summary>
    /// Constants for file locations -- file names and directory names for data files.
    /// </summary>
    public class FileLocation
    {
        /// <summary>
        /// Player files.
        /// </summary>
        public static string PlayerDirectory = ".." + Path.DirectorySeparatorChar + "player" +
            Path.DirectorySeparatorChar;
        /// <summary>
        /// Player accounts.
        /// </summary>
        public static string AccountDirectory = ".." + Path.DirectorySeparatorChar + "player" +
            Path.DirectorySeparatorChar + "accounts" + Path.DirectorySeparatorChar;
        /// <summary>
        /// Backup copies of player files.
        /// </summary>
        public static string BackupDirectory = ".." + Path.DirectorySeparatorChar + "backup" +
            Path.DirectorySeparatorChar;
        /// <summary>
        /// System files directory. Contains core system data files.
        /// </summary>
        public static string SystemDirectory = ".." + Path.DirectorySeparatorChar + "sys" +
            Path.DirectorySeparatorChar;
        /// <summary>
        /// Contains blank system data files, used if there are none in the system file directory.
        /// </summary>
        public static string BlankSystemFileDirectory = ".." + Path.DirectorySeparatorChar + "sys" +
            Path.DirectorySeparatorChar + "EmptyFiles" + Path.DirectorySeparatorChar;
        /// <summary>
        /// Character class definition files.
        /// </summary>
        public static string ClassDirectory = ".." + Path.DirectorySeparatorChar + "classes" +
            Path.DirectorySeparatorChar;
        /// <summary>
        /// Zone file directory.
        /// </summary>
        public static string AreaDirectory = ".." + Path.DirectorySeparatorChar + "area" +
            Path.DirectorySeparatorChar;
        /// <summary>
        /// Guild files.
        /// </summary>
        public static string GuildDirectory = ".." + Path.DirectorySeparatorChar + "clans" +
            Path.DirectorySeparatorChar;
        /// <summary>
        /// Race definition files. Includes both player and monster races.
        /// </summary>
        public static string RaceDirectory = ".." + Path.DirectorySeparatorChar + "races" +
            Path.DirectorySeparatorChar;
        /// <summary>
        /// Spell files.
        /// </summary>
        public static string SpellDirectory = ".." + Path.DirectorySeparatorChar + "spells" +
            Path.DirectorySeparatorChar;
        /// <summary>
        /// Skill files.
        /// </summary>
        public static string SkillDirectory = ".." + Path.DirectorySeparatorChar + "skills" +
            Path.DirectorySeparatorChar;

        /// <summary>
        /// List of areas to load.
        /// </summary>
        public static string AreaLoadList = "Area.list";
        /// <summary>
        /// List of classes to load.
        /// </summary>
        public static string ClassLoadList = "Class.list";
        /// <summary>
        /// List of clans to load.
        /// </summary>
        public static string GuildLoadList = "Clans.list";
        /// <summary>
        /// List of races to load.
        /// </summary>
        public static string RaceLoadList = "Races.list";
        /// <summary>
        /// List of spells to load.
        /// </summary>
        public static string SpellLoadList = "Spells.list";
        /// <summary>
        /// List of skills to load.
        /// </summary>
        public static string SkillLoadList = "Skills.list";
        /// <summary>
        /// For game shutdown.
        /// </summary>
        public static string ShutdownFile = "SHUTDOWN.TXT";
        /// <summary>
        /// List of banned sites.
        /// </summary>
        public static string BanFile = "Ban.xml";
        /// <summary>
        /// Social action definitions.
        /// </summary>
        public static string SocialFile = "Socials.xml";
        /// <summary>
        /// Help entries.
        /// </summary>
        public static string HelpFile = "Help.xml";
        /// <summary>
        /// MUD system configuration information.
        /// </summary>
        public static string SysdataFile = "Sysdata.xml";
        /// <summary>
        /// Player usage stats.
        /// </summary>
        public static string PlayerCountFile = "Playercounts.csv";
        /// <summary>
        /// Crimes/justice info.
        /// </summary>
        public static string CrimeFile = "Crimes.xml";
        /// <summary>
        /// Player corpse save file.
        /// </summary>
        public static string CorpseFile = "Corpses.xml";
        /// <summary>
        /// Fraglist information.
        /// </summary>
        public static string FragFile = "Fraglist.xml";
        /// <summary>
        /// In-game issue reports - bugs, typos, ideas, etc.
        /// </summary>
        public static string IssueFile = "Issues.xml";
        /// <summary>
        /// Bounties on players and guilds.
        /// </summary>
        public static string BountyFile = "Bounties.xml";
        /// <summary>
        /// Heartbeat file - for keep-alive scripts.
        /// </summary>
        public static string HeartbeatFile = "HEARTBEAT";
        /// <summary>
        /// Conversational chat bot file.
        /// </summary>
        public static string ChatterbotFile = "Chatterbots.xml";
        /// <summary>
        /// ANSI screen file.
        /// </summary>
        public static string ScreenFile = "Screens.xml";
        /// <summary>
        /// List of static room numbers.
        /// </summary>
        public static string StaticRoomFile = "RoomNumbers.xml";
        /// <summary>
        /// Zone-to-zone connections.
        /// </summary>
        public static string ZoneConnectionFile = "ZoneConnections.xml";
    }
}
