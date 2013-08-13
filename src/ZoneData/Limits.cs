namespace ModernMUD
{
    /// <summary>
    /// Constants for the game engine.
    /// 
    /// Increase the maximums if you add another bitvector, adjust the time numbers if you want
    /// a different game year, and modify other things if you think you should.
    /// 
    /// Some of these could be refactored into their individual class definitions or otherwise
    /// factored out in a way that makes them unnecessary.
    /// </summary>
    public class Limits
    {
        /// <summary>
        /// Maximum number of items a character can carry in their inventory.
        /// </summary>
        public const int MAX_CARRY = 10;
        /// <summary>
        /// Total number of classes in the game.
        /// </summary>
        public const int MAX_CLASS = 28;
        /// <summary>
        /// Total number of races in the game.
        /// </summary>
        public const int MAX_RACE = 82;
        /// <summary>
        /// Last race on the race list to carry coins.
        /// </summary>
        public const int MAX_COIN_RACE = 40;
        /// <summary>
        /// Number of player races available.
        /// </summary>
        public const int MAX_PC_RACE = 21;
        /// <summary>
        /// Highest level a PC can ever be.
        /// </summary>
        public const int MAX_LEVEL = 65;
        /// <summary>
        /// Number of spell circles
        /// </summary>
        public const int MAX_CIRCLE = 12;
        /// <summary>
        /// Maximum length of a player's title.
        /// </summary>
        public const int MAX_TITLE_LENGTH = 60;
        /// <summary>
        /// Maximum number of items in a player's command history.
        /// </summary>
        public const int MAX_HISTORY = 10;
        /// <summary>
        /// Maximum members per clan/guild.
        /// </summary>
        public const int MAX_CLAN_MEMBERS = 30;
        /// <summary>
        /// Highest map visibility distance.
        /// </summary>
        public const int MAX_MAP_VISIBILITY = 4;
        /// <summary>
        /// Exit direction max
        /// </summary>
        public const int MAX_DIRECTION = 10;
        /// <summary>
        /// Starting skill proficiency value.
        /// </summary>
        public const int BASE_SKILL_ADEPT = 25;
        /// <summary>
        /// Highest a skill proficiency can ever be.
        /// </summary>
        public const int MAX_SKILL_ADEPT = 95;
        /// <summary>
        /// Base level of proficiency with a spell.
        /// </summary>
        public const int BASE_SPELL_ADEPT = 80;
        /// <summary>
        /// Highest a spell proficiency can ever be.
        /// </summary>
        public const int MAX_SPELL_ADEPT = 99;
        /// <summary>
        /// Maximum unmodified base score for a character attribute.
        /// </summary>
        public const int MAX_BASE_ATTRIBUTE = 100;
        /// <summary>
        /// Overlord. Supreme ruler of the game. Can do anything.
        /// </summary>
        public const int LEVEL_OVERLORD = MAX_LEVEL;
        /// <summary>
        /// Second-highest immortal level, can do just about anything.
        /// </summary>
        public const int LEVEL_GREATER_GOD = (LEVEL_OVERLORD - 1);
        /// <summary>
        /// A mid-level immortal with access to most immortal commands, with a few exceptions.
        /// </summary>
        public const int LEVEL_LESSER_GOD = (LEVEL_GREATER_GOD - 1);
        /// <summary>
        /// A lower level immortal with access to some commands, but nothing sensitive or dangerous.
        /// </summary>
        public const int LEVEL_DEMIGOD = (LEVEL_LESSER_GOD - 1);
        /// <summary>
        /// A limited low-level immortal with only basic command access.
        /// </summary>
        public const int LEVEL_AVATAR = (LEVEL_DEMIGOD - 1);
        /// <summary>
        /// Above Human, but not a god. Typically used for "immortal emmeritus" types to get access to immortal chat.
        /// </summary>
        public const int LEVEL_HERO = (LEVEL_AVATAR - 1); // Builders are basically Heros
        /// <summary>
        /// Highest level a mortal could ever be.
        /// </summary>
        public const int MAX_MORTAL_LEVEL = 45;
        /// <summary>
        /// Max level a mortal can obtain through normal experience point accumulation.
        /// </summary>
        public const int MAX_ADVANCE_LEVEL = 40;
        /// <summary>
        /// Number of affect flag bitvectors in the game.
        /// </summary>
        public const int NUM_AFFECT_VECTORS = 6;
        /// <summary>
        /// Number of action flag bitvectors in the game.
        /// </summary>
        public const int NUM_ACTION_VECTORS = 2;
        /// <summary>
        /// Number of item extra bitvectors in the game.
        /// </summary>
        public const int NUM_ITEM_EXTRA_VECTORS = 2;
        /// <summary>
        /// Number of item use flag bitvectors in the game.
        /// </summary>
        public const int NUM_USE_FLAGS_VECTORS = 2;
        /// <summary>
        /// Number of item wear flag bitvectors in the game.
        /// </summary>
        public const int NUM_WEAR_FLAGS_VECTORS = 1;
        /// <summary>
        /// Number of room flag bitvectors in the game.
        /// </summary>
        public const int NUM_ROOM_FLAGS = 2;
        /// <summary>
        /// Number of area flag bitvectors in the game.
        /// </summary>
        public const int NUM_AREA_FLAGS = 1;
        /// <summary>
        /// Number of innate bitvectors in the game.
        /// </summary>
        public const int NUM_INNATE_VECTORS = 2;
        /// <summary>
        /// Number of wear locations for items.
        /// </summary>
        public const int NUM_WEARABLE_VECTORS = 28;
        /// <summary>
        /// Minimum value for faction standing.
        /// </summary>
        public const double MIN_FACTION = -10000.0;
        /// <summary>
        /// Maximum value for faction standing.
        /// </summary>
        public const double MAX_FACTION = 10000.0;
        /// <summary>
        /// Number of days in a week.
        /// </summary>
        public const int DAYS_PER_WEEK = 7;
        /// <summary>
        /// Number of real-time seconds per hour.
        /// </summary>
        public const int SECONDS_PER_HOUR = 30;
        /// <summary>
        /// Number of game hours per day.
        /// </summary>
        public const int HOURS_PER_DAY = 24;
        /// <summary>
        /// Number of game days per month.
        /// </summary>
        public const int DAYS_PER_MONTH = 30;
        /// <summary>
        /// Number of game months per year.
        /// </summary>
        public const int MONTHS_PER_YEAR = 12;
        /// <summary>
        /// Length of a game year in real-time as a timespan.
        /// </summary>
        public static readonly System.TimeSpan TIMESPAN_GAME_YEAR =
            new System.TimeSpan(0, 0, (SECONDS_PER_HOUR * HOURS_PER_DAY * DAYS_PER_MONTH * MONTHS_PER_YEAR));
        /// <summary>
        /// Length of a game hour in real-time as a timespan.
        /// </summary>
        public static readonly System.TimeSpan TIMESPAN_GAME_HOUR =
            new System.TimeSpan(0, 0, SECONDS_PER_HOUR);
        /// <summary>
        /// Length of a game day in real-time as a timespan.
        /// </summary>
        public static readonly System.TimeSpan TIMESPAN_GAME_DAY =
            new System.TimeSpan(0, 0, (SECONDS_PER_HOUR * HOURS_PER_DAY));
    }
}