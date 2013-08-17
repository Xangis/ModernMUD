using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents a command that can be issued by a player or mob.
    /// </summary>
    public class CommandType
    {
        CommandType(string nam, CommandFunction fun, int pos, int lvl, LogType logged, bool shown, bool removeinvis, bool removehide, bool removemed)
        {
            Name = nam;
            Function = fun;
            MinLevel = lvl;
            _logType = logged;
            Show = shown;
            BreakInvisibility = removeinvis;
            BreakHide = removehide;
            BreakMeditate = removemed;
            MinPosition = pos;
            CanUseWhenParalyzed = false;
            MustSpellOut = false;
        }

        CommandType(string nam, CommandFunction fun, int pos, int lvl, LogType logged, bool shown, bool removeinvis, bool removehide, bool removemed, bool usePara)
        {
            Name = nam;
            Function = fun;
            MinLevel = lvl;
            _logType = logged;
            Show = shown;
            BreakInvisibility = removeinvis;
            BreakHide = removehide;
            BreakMeditate = removemed;
            MinPosition = pos;
            CanUseWhenParalyzed = usePara;
            MustSpellOut = false;
        }

        CommandType(string nam, CommandFunction fun, int pos, int lvl, LogType logged, bool shown, bool removeinvis, bool removehide, bool removemed, bool usePara, bool mustSpellOut)
        {
            Name = nam;
            Function = fun;
            MinLevel = lvl;
            _logType = logged;
            Show = shown;
            BreakInvisibility = removeinvis;
            BreakHide = removehide;
            BreakMeditate = removemed;
            MinPosition = pos;
            CanUseWhenParalyzed = usePara;
            MustSpellOut = mustSpellOut;
        }

        /// <summary>
        /// Name of the command.
        /// </summary>
        public string Name { get; set; }

        public delegate void CommandFunction(CharData ch, string[] str);

        public CommandFunction Function { get; set; }
        public int MinPosition { get; set; }
        public int MinLevel { get; set; }
        public LogType _logType;
        public bool Show { get; set; }
        /// <summary>
        /// Does the command cause the person to snap vis?
        /// </summary>
        public bool BreakInvisibility { get; set; }
        /// <summary>
        /// Does the command cause the person to break hide?
        /// </summary>
        public bool BreakHide { get; set; }
        /// <summary>
        /// Does the command interrupt meditation?
        /// </summary>
        public bool BreakMeditate { get; set; }
        /// <summary>
        /// Can the command be used when you're paralyzed?
        /// </summary>
        public bool CanUseWhenParalyzed { get; set; }
        /// <summary>
        /// Do you have to type the whole command to avoid doing it accidentally (i.e. suicide).
        /// </summary>
        public bool MustSpellOut { get; set; }

        /// <summary>
        /// Command log level.  Used for determining command's log output.
        /// </summary>
        public enum LogType
        {
            normal = 0,
            always,
            never
        }

        public static bool fLogAll; // Log-all switch

        /// <summary>
        /// Command table
        /// 
        /// Items in this table are matched in the order in which they appear, so a command
        /// needs to be near the top of the list for a short 1-2 letter version of it to match.
        /// </summary>
        public static readonly CommandType[] CommandTable = new[]
        {
            // Common movement commands.
            new CommandType( "east", Command.East, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "north", Command.North, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "south", Command.South, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "west", Command.West, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "up", Command.Up, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "down", Command.Down, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "northwest", Command.NorthWest, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "nw", Command.NorthWest, Position.standing, 0, LogType.normal, false, false, true, true ),
            new CommandType( "northeast", Command.NorthEast, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "ne", Command.NorthEast, Position.standing, 0, LogType.normal, false, false, true, true ),
            new CommandType( "southeast", Command.SouthEast, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "se", Command.SouthEast, Position.standing, 0, LogType.normal, false, false, true, true ),
            new CommandType( "southwest", Command.SouthWest, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "sw", Command.SouthWest, Position.standing, 0, LogType.normal, false, false, true, true ),
            new CommandType( "fly", Command.Fly, Position.standing, 0, LogType.normal, true, false, true, true ),
            // Common other commands.
            // Placed here so one and two letter abbreviations work.
            new CommandType( "at", Command.At, Position.dead, Limits.LEVEL_LESSER_GOD, LogType.normal, true, false, false, true ),
            new CommandType( "buy", Command.Buy, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "cast", Command.Cast, Position.fighting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "exits", Command.Exits, Position.resting, 0, LogType.normal, true, false, false, true ),
            new CommandType( "get", Command.Get, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "inventory", Command.Inventory, Position.dead, 0, LogType.normal, true, false, false, true ),
            new CommandType( "kill", Command.Kill, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "look", Command.LookCommand, Position.reclining, 0, LogType.normal, true, false, false, true, true ),
            new CommandType( "order", Command.Order, Position.resting, 0, LogType.always, true, false, true, true ),
            new CommandType( "quaff", Command.Quaff, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "shoot", Command.Shoot, Position.standing, 0, LogType.normal, true, true, true, true ),
            new CommandType( "search", Command.Search, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "sing", Command.Sing, Position.standing, 0, LogType.normal, true, true, true, true ),
            new CommandType( "play", Command.Play, Position.standing, 0, LogType.normal, true, true, true, true ),
            new CommandType( "tell", Command.Tell, Position.reclining, 0, LogType.normal, true, false, true, true ),
            new CommandType( "wield", Command.Wield, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "will", Command.Will, Position.fighting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "imm", Command.ImmortalTalk, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false ),
            new CommandType( "immhelp", Command.ImmortalHelp, Position.dead, Limits.LEVEL_HERO, LogType.normal, true, false, false, true ),
            new CommandType( "killproc", Command.KillProcess, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, true ),
            // Position commands.
            new CommandType( "kneel", Command.Kneel, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "rest", Command.Rest, Position.sleeping, 0, LogType.normal, true, false, false, false ),
            new CommandType( "recline", Command.Recline, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "sit", Command.Sit, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "sleep", Command.Sleep, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "stand", Command.Stand, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "wake", Command.Wake, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            // Informational commands.
            // Most informational commands should not break invisibility or hide.
            // These are also about the only commands that should not break meditate.
            new CommandType( "areas", Command.Areas, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "attributes", Command.Attributes, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "bug", Command.Bug, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "commands", Command.Commands, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "compare", Command.Compare, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "consider", Command.Consider, Position.resting, 0, LogType.normal, true, false, false, true, true ),
            new CommandType( "consent", Command.Consent, Position.dead, 0, LogType.normal, true, false, false, true, true ),
            new CommandType( "crashbug", Command.CrashBug, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "credits", Command.Credits, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "equipment", Command.Equipment, Position.dead, 0, LogType.normal, true, false, false, true ),
            new CommandType( "examine", Command.Examine, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "experience", Command.Experience, Position.dead, 0, LogType.normal, true, false, false, false ),
            new CommandType( "faction", Command.Faction, Position.dead, 0, LogType.normal, true, false, false, false ),
            new CommandType( "fraglist", Command.Fraglist, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "glance", Command.Glance, Position.reclining, 0, LogType.normal, true, false, false, true, true ),
            new CommandType( "help", Command.HelpCommand, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "idea", Command.Idea, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "ignore", Command.Ignore, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "innate", Command.Innate, Position.dead, 0, LogType.normal, true, false, true, true ),
            new CommandType( "invoke", Command.Invoke, Position.resting, 0, LogType.normal, true, false, false, false ),
            new CommandType( "justice", Command.JusticeCommand, Position.dead, 0, LogType.normal, true, false, false, true, true ),
            new CommandType( "news", Command.News, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "report", Command.Report, Position.reclining, 0, LogType.normal, true, false, true, true, true ),
            new CommandType( "requesthelp", Command.RequestHelp, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "pagelength", Command.PageLength, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "read", Command.LookCommand, Position.resting, 0, LogType.normal, true, false, false, true ),
            new CommandType( "score", Command.Score, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "guild", Command.Society, Position.dead, 0, LogType.normal, true, false, false, true ),
            new CommandType( "socials", Command.Socials, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "summon", Command.SummonMount,Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "scan", Command.Scan, Position.standing, 0, LogType.normal, true, false, false, true ),
            new CommandType( "skills", Command.Skills, Position.dead, 0, LogType.normal, true, false, false, false ),
            new CommandType( "songs", Command.Songs, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "spells", Command.Spells, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "speak", Command.Speak, Position.dead, 0, LogType.normal, true, false, false, true ),
            new CommandType( "time", Command.Time, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "trophy", Command.Trophy, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "typo", Command.Typo, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "who", Command.Who, Position.reclining, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "wizlist", Command.Wizlist, Position.dead, 0, LogType.normal, true, false, false, false ),
            new CommandType( "zones", Command.Areas, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            // Combat commands.
            //
            // Most of these should break invisibility or hide.
            new CommandType( "assist", Command.Assist, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "backstab", Command.Backstab, Position.standing, 0, LogType.normal, true, true, true, true ),
            new CommandType( "bash", Command.Bash, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "bs", Command.Backstab, Position.standing, 0, LogType.normal, false, true, true, true ),
            new CommandType( "berzerk", Command.Berzerk, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "berserk", Command.Berzerk, Position.fighting, 0, LogType.normal, false, true, true, true ),
            new CommandType( "bodyslam", Command.Bodyslam, Position.standing, 0, LogType.normal, true, true, true, true ),
            new CommandType( "charge", Command.Charge, Position.standing, 0, LogType.normal, true, true, true, true ),
            new CommandType( "circle", Command.Circle, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "disarm", Command.Disarm, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "disengage", Command.Disengage, Position.fighting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "guard", Command.Guard, Position.fighting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "flee", Command.Flee, Position.reclining, 0, LogType.normal, true, false, true, true ),
            new CommandType( "headbutt", Command.Headbutt, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "kick", Command.Kick, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "dirt", Command.DirtToss, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "rescue", Command.Rescue, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "springleap", Command.Springleap, Position.resting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "stance", Command.Stance, Position.fighting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "throw", Command.Throw, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "trip", Command.Trip, Position.fighting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "whirlwind", Command.Whirlwind, Position.standing, 0, LogType.normal, true, true, true, true ),
            new CommandType( "chill", Command.Chill, Position.fighting, Limits.LEVEL_AVATAR, LogType.normal, true, true, true, true ),
            // Configuration commands.
            //
            // Most of these should not break invis or hide, or meditate
            new CommandType( "auto", Command.Auto, Position.dead, 0, LogType.normal, false, false, false, false, true ),
            new CommandType( "blank", Command.Blank, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "brief", Command.Brief, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "cant", Command.Cant, Position.reclining,0, LogType.normal, true, false, true, true ),
            new CommandType( "channels", Command.Channels, Position.dead, 0, LogType.normal, true, false, false, true ),
            new CommandType( "color", Command.ColorCommand,Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "colour", Command.ColorCommand,Position.dead, 0, LogType.normal, false, false, false, false, true ),
            new CommandType( "combine", Command.Combine, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "compact", Command.Blank, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "config", Command.Toggle, Position.dead, 0, LogType.normal, false, false, false, false, true ),
            new CommandType( "toggle", Command.Toggle, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "description",Command.Description, Position.dead, 0, LogType.normal, true, false, false, true ),
            new CommandType( "prompt", Command.Prompt, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "display", Command.Prompt, Position.dead, 0, LogType.normal, true, false, false, false, true ),
            new CommandType( "title", Command.Title, Position.dead, Limits.LEVEL_HERO, LogType.normal, true, false, false, true ),
            new CommandType( "wimpy", Command.Wimpy, Position.dead, 0, LogType.normal, true, false, false, true, true ),
            new CommandType( "history", Command.History, Position.dead, 0, LogType.normal, true, false, false, false ),
            // Communication commands.
            //
            // Most of these should break hide but not invisibility.
            new CommandType( "ask", Command.Ask, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "beep", Command.Beep, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "emote", Command.Emote, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( ":", Command.Emote, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "gsay", Command.GroupChat, Position.reclining, 0, LogType.normal, true, false, true, true ),
            new CommandType( ";", Command.GroupChat, Position.reclining, 0, LogType.normal, true, false, true, true ),
            new CommandType( "ptell", Command.PlayerTell, Position.dead, Limits.LEVEL_AVATAR, LogType.always, true, false, false, true ),
            new CommandType( "reply", Command.Reply, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "say", Command.Say, Position.reclining, 0, LogType.normal, true, false, true, true ),
            new CommandType( "random", Command.RandomSentence, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, true ),
            new CommandType( "sign", Command.Sign, Position.reclining, 0, LogType.normal, true, false, true, true ),
            new CommandType( "'", Command.Say, Position.reclining, 0, LogType.normal, true, false, true, true ),
            new CommandType( "shout", Command.Shout, Position.resting, 1, LogType.normal, true, false, true, true ),
            new CommandType( "whisper", Command.Whisper, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "yell", Command.Yell, Position.resting, 1, LogType.normal, true, false, true, true ),
            // Object manipulation commands.
            //
            // Most of these should break hide but not invisibility.
            new CommandType( "auction", Command.Auction, Position.sitting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "brandish", Command.Brandish, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "carve", Command.Carve, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "climb", Command.Climb, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "close", Command.Close, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "deposit", Command.Deposit, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "drag", Command.Drag, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "drink", Command.Drink, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "drop", Command.Drop, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "eat", Command.Eat, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "fill", Command.Fill, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "pour", Command.Pour, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "give", Command.Give, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "grab", Command.Hold, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "hold", Command.Hold, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "list", Command.ListCommand, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "lock", Command.Lock, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "open", Command.Open, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "pick", Command.Pick, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "put", Command.Put, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "quaff", Command.Quaff, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "recite", Command.Recite, Position.resting, 0, LogType.normal, true, true, true, true ),
            new CommandType( "remove", Command.Remove, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "reload", Command.Reload, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "sell", Command.Sell, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "take", Command.Get, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "unlock", Command.Unlock, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "use", Command.Use, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "value", Command.Value, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "wear", Command.Wear, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "weather", Command.Weather, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "withdraw", Command.Withdraw, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "zap", Command.Zap, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "brew", Command.Brew, Position.standing, Limits.LEVEL_AVATAR, LogType.normal, true, false, true, true ),
            new CommandType( "scribe", Command.Scribe, Position.resting, 0, LogType.normal, true, false, true, true ),
            // Miscellaneous commands.
            new CommandType( "afk", Command.AFK, Position.sleeping, 0, LogType.normal, true, false, true, true, true ),
            new CommandType( "aggressive", Command.Aggressive, Position.sleeping, 0, LogType.normal, true, false, false, true ),
            new CommandType( "awareness", Command.Aware, Position.standing, 0, LogType.normal, true, false, false, true ),
            new CommandType( "bandage", Command.Bandage, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "bot", Command.Bot, Position.dead, 0, LogType.normal, true, false, true, true ),
            new CommandType( "bounty", Command.BountyCommand, Position.resting, 0, LogType.normal, true, false, false, true ),
            new CommandType( "camp", Command.Camp, Position.sitting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "chameleon power",Command.Chameleon, Position.standing, 0, LogType.normal, true, false, false, true ),
            new CommandType( "dice", Command.Dice, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "disembark", Command.Disembark, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "doorbash", Command.Doorbash, Position.standing, 0, LogType.normal, true, true, true, true ),
            new CommandType( "disband", Command.Disband, Position.resting, 0, LogType.normal, true, false, false, true ),
            new CommandType( "enter", Command.Enter, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "firstaid", Command.FirstAid, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "follow", Command.Follow, Position.resting, 0, LogType.normal, true, false, false, true ),
            new CommandType( "forage", Command.Forage, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "forget", Command.Forget, Position.dead, 0, LogType.normal, true, false, false, true ),
            new CommandType( "group", Command.Group, Position.sleeping, 0, LogType.normal, true, false, false, true ),
            new CommandType( "heighten senses",Command.Heighten, Position.standing, 0, LogType.normal, true, false, false, true ),
            new CommandType( "hide", Command.Hide, Position.resting, 0, LogType.normal, true, false, false, true ),
            new CommandType( "layhands", Command.LayHands, Position.fighting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "meditate", Command.Meditate, Position.resting, 0, LogType.normal, true, false, false, false ),
            new CommandType( "memorize", Command.Memorize, Position.resting, 0, LogType.normal, true, false, false, false ),
            new CommandType( "poison weapon", Command.PoisonWeapon,Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "pray", Command.Pray, Position.resting, 0, LogType.normal, true, false, true, false ),
            new CommandType( "practice", Command.Practice, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "quit", Command.Quit, Position.dead, 0, LogType.normal, true, false, false, true, false, true ),
            new CommandType( "rent", Command.Rent, Position.dead, 0, LogType.normal, true, false, true, true ),
            new CommandType( "save", Command.Save, Position.dead, 0, LogType.normal, true, false, false, false ),
            new CommandType( "shadow form", Command.Shadow, Position.standing, 0, LogType.normal, true, false, false, true ),
            new CommandType( "shift", Command.Shift, Position.standing, 0, LogType.normal, true, false, true, false ),
            new CommandType( "capture", Command.Capture, Position.fighting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "sneak", Command.Sneak, Position.standing, 0, LogType.normal, true, false, false, true ),
            new CommandType( "split", Command.Split, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "steal", Command.Steal, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "suicide", Command.Suicide, Position.dead, 0, LogType.always, true, false, true, true ),
            new CommandType( "train", Command.Train, Position.resting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "unbind", Command.Untangle, Position.fighting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "untangle", Command.Untangle, Position.fighting, 0, LogType.normal, true, false, true, true ),
            new CommandType( "ungroup", Command.Ungroup, Position.sleeping, 0, LogType.normal, true, false, false, true ),
            new CommandType( "visible", Command.Visible, Position.sleeping, 0, LogType.normal, true, false, true, true ),
            new CommandType( "world", Command.MudConfig, Position.dead, Limits.LEVEL_OVERLORD, LogType.always, true, false, false, false ),
            new CommandType( "worldmap", Command.Worldmap, Position.dead, 0, LogType.always, true, false, false, false ),
            new CommandType( "track", Command.TrackCommand,Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "mount", Command.Mount, Position.standing, 0, LogType.normal, true, false, true, true ),
            new CommandType( "dismount", Command.Dismount, Position.resting, 0, LogType.normal, true, false, true, true ),
            // Immortal commands.
            // These should not cause you to go visible or lose hide
            new CommandType( "finger", Command.Finger, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false ),
            new CommandType( "fog", Command.Fog, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false ),
            new CommandType( "advance", Command.Advance, Position.dead, Limits.LEVEL_OVERLORD, LogType.always, true, false, false, false ),
            new CommandType( "reset", Command.ResetCommand, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "trust", Command.Trust, Position.dead, Limits.LEVEL_OVERLORD, LogType.always, true, false, false, false ),
            new CommandType( "delet", Command.Terminat, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "alist", Command.AreaList, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false ),
            new CommandType( "allow", Command.Allow, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "siteban", Command.Ban, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "bitflag", Command.BitFlag, Position.dead, Limits.LEVEL_AVATAR, LogType.always, true, false, false, false ),
            new CommandType( "deny", Command.Deny, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "disconnect", Command.Disconnect, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "economy", Command.Economy, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false ),
            new CommandType( "force", Command.Force, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "freeze", Command.Freeze, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "find", Command.Find, Position.dead, Limits.LEVEL_LESSER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "immcolor", Command.SetImmortalColor, Position.dead, Limits.LEVEL_AVATAR, LogType.always, true, false, false, false ),
            new CommandType( "imtlset", Command.ImmortalSkillSet, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "issue", Command.IssueCommand, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false ),
            new CommandType( "language", Command.ResetLanguages, Position.fighting, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false ),
            new CommandType( "load", Command.Load, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "log", Command.LogCommand, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "loopmud", Command.LoopMUD, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "newsentry", Command.NewsEntry, Position.dead, Limits.LEVEL_AVATAR, LogType.always, true, false, false, false ),
            new CommandType( "noemote", Command.NoEmote, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "numlock", Command.Numlock, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "pardon", Command.Pardon, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "purge", Command.Purge, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "reboot", Command.Reboot, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false, false, true ),
            new CommandType( "rename", Command.Rename, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "namesucks", Command.NameSucks, Position.dead, Limits.LEVEL_LESSER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "restore", Command.Restore, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "setbit", Command.Set, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "shutdow", Command.Shutdow, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "shutdown", Command.Shutdown, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "silence", Command.Silence, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "slay", Command.Slay, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false, false, true ),
            new CommandType( "sset", Command.SetSkill, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "sstime", Command.Sstime, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "stat", Command.Stat, Position.dead, Limits.LEVEL_AVATAR, LogType.always, true, false, false, false ),
            new CommandType( "statdump", Command.StatDump, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "terminate", Command.Terminate, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "transfer", Command.Transfer, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "users", Command.Users, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "where", Command.Where, Position.dead, Limits.LEVEL_AVATAR, LogType.always, true, false, false, false ),
            new CommandType( "wizify", Command.Wizify, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "wizlock", Command.Wizlock, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "makeclan", Command.MakeClan, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "killclan", Command.Killclan, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "echo", Command.Echo, Position.dead, Limits.LEVEL_LESSER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "memory", Command.Memory, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "peace", Command.Peace, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "recho", Command.Recho, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "return", Command.Return, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "sober", Command.Sober, Position.dead, Limits.LEVEL_LESSER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "snoop", Command.Snoop, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "switch", Command.Switch, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "wizinvis", Command.Invis, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "appearmsg", Command.AppearanceMessage, Position.dead, Limits.LEVEL_LESSER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "disappmsg", Command.DisappearMessage, Position.dead, Limits.LEVEL_LESSER_GOD, LogType.normal, true, false, false, false ),
            new CommandType( "goto", Command.Goto, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false ),
            new CommandType( "godmode", Command.GodMode, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false ),
            new CommandType( "immchannels", Command.ImmortalChannels, Position.dead, Limits.LEVEL_AVATAR, LogType.normal, true, false, false, false, true, false ),
            new CommandType( "show", Command.Show, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "clone", Command.Clone, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            new CommandType( "zreset", Command.ZoneReset, Position.dead, Limits.LEVEL_GREATER_GOD, LogType.always, true, false, false, false ),
            // Guild commands.
            new CommandType( "guilds", Command.Clans, Position.dead, 0, LogType.normal, true, false, false, false ),
            new CommandType( "orders", Command.Clans, Position.dead, 0, LogType.normal, true, false, false, false ),
            new CommandType( "clans", Command.Clans, Position.dead, 0, LogType.normal, true, false, false, false ),
            new CommandType( "gcc", Command.ClanChat, Position.sleeping, 0, LogType.normal, true, false, false, false )
        };

        /// <summary>
        /// The main entry point for executing commands.
        /// Can be recursively called from 'at', 'order', 'force'.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="argument"></param>
        public static void Interpret(CharData ch, string argument)
        {
            string command;
            Object obj;
            Room room;
            int cmd;

            // Get rid of leading and trailing spaces.
            argument = argument.Trim();

            // Strip leading spaces.
            argument.Trim();
            if (argument.Length == 0)
            {
                return;
            }

            // Remove AFK
            if (!ch.IsNPC())
            {
                ch.RemoveActBit(PC.PLAYER_AFK);
            }

            // Implement freeze command.
            if (!ch.IsNPC() && ch.HasActBit(PC.PLAYER_FREEZE))
            {
                ch.SendText("You're totally frozen!\r\n");
                return;
            }

            // Grab the command word.  Special parsing so ' can be a command,
            // also no spaces needed after punctuation.
            string logline = argument;
            int argptr = 0;
            if (!Char.IsLetter(argument[0]) && !Char.IsDigit(argument[0]))
            {
                command = argument.Substring(0, 1);
                argptr++;
                while (argument.Length > argptr && Char.IsWhiteSpace(argument[argptr]))
                {
                    argument = argument.Remove(0, 1);
                }
            }
            else
            {
                command = MUDString.OneArgument(argument, ref argument);
                argument.Trim(); // Clean up the remainder of the command.
            }

            // Nothing to do if command is empty.  Just send them a newline and bail.
            if (string.IsNullOrEmpty(command) && string.IsNullOrEmpty(argument))
            {
                ch.SendText("\r\n");
                return;
            }

            // Look for an item with a teleport trigger in the room.
            // and check to see if the command is a teleport trigger
            if (ch._inRoom && (obj = ch.GetObjHere(argument)))
            {
                if (obj.ItemType == ObjTemplate.ObjectType.teleport)
                {
                    if (CheckCommandTrigger(command, obj.Values[1]) && obj.Values[2] != 0)
                    {
                        if (obj.Values[2] != -1)
                        {
                            obj.Values[2]--;
                        }
                        room = Room.GetRoom(obj.Values[0]);
                        if (room)
                        {
                            SocketConnection.Act("$n&n vanishes suddenly.", ch, null, null, SocketConnection.MessageTarget.room);
                            string buf = String.Format("You {0} $p&n.\r\n", command);
                            SocketConnection.Act(buf, ch, obj, null, SocketConnection.MessageTarget.character);
                            Log.Trace(String.Format("{0} activated keyword and was teleported by object.", ch._name));
                            ch.RemoveFromRoom();
                            ch.AddToRoom(room);
                            Interpret(ch, "look auto");
                            SocketConnection.Act("$n&n arrives suddenly.", ch, null, null, SocketConnection.MessageTarget.room);
                        }
                        else
                        {
                            ch.SendText("BUG: The target room for this teleporter does not exist.\r\n");
                            Log.Error("Target room for object {0} does not exist.", obj.ObjIndexData.IndexNumber);
                        }
                        return;
                    }
                }
                else if (obj.ItemType == ObjTemplate.ObjectType.switch_trigger)
                {
                    Exit exit;
                    string cbuf = String.Format("Checking {0} against command no. {1} for {2}.", command, obj.Values[0], obj.Name);
                    ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_SPAM, 0, cbuf);
                    if (CheckCommandTrigger(command, obj.Values[0]))
                    {
                        ch.SendText("Click.\r\n");
                        room = Room.GetRoom(obj.Values[1]);
                        if (!room)
                        {
                            Log.Error("Target room for switch object {0} does not exist.", obj.ObjIndexData.IndexNumber);
                            return;
                        }
                        exit = room.ExitData[obj.Values[2]];
                        if (exit == null)
                        {
                            Log.Error("Target exit for switch object {0} does not exist.", obj.ObjIndexData.IndexNumber);
                            return;
                        }
                        if (exit.HasFlag(Exit.ExitFlag.blocked))
                        {
                            exit.RemoveFlag(Exit.ExitFlag.blocked);
                        }
                        return;
                    }
                }
            }

            // Look for command in command table.
            bool found = false;
            int trust = ch.GetTrust();
            for (cmd = 0; cmd < CommandTable.Length; cmd++)
            {
                if (CommandTable[cmd].Name.StartsWith(command, StringComparison.CurrentCultureIgnoreCase)
                        && (CommandTable[cmd].MinLevel <= trust))
                {
                    found = true;
                    break;
                }
            }

            // Command was found, respond accordingly.            
            if (found)
            {
                // Logging and snooping.
                if (CommandTable[cmd]._logType == LogType.never)
                {
                    logline = "---- Nothing to see here ----";
                }

                if ((!ch.IsNPC() && ch.HasActBit(PC.PLAYER_LOG)) || fLogAll
                        || CommandTable[cmd]._logType == LogType.always)
                {
                    string logBuf = String.Format("Log {0}: {1}", ch._name, logline);
                    Log.Trace(logBuf);
                    ImmortalChat.SendImmortalChat(ch, ImmortalChat.IMMTALK_SECURE, ch.GetTrust(), logBuf);
                }

                if (ch._desc && ch._desc.SnoopBy)
                {
                    ch._desc.SnoopBy.WriteToBuffer("% ");
                    ch._desc.SnoopBy.WriteToBuffer(logline);
                    ch._desc.SnoopBy.WriteToBuffer("\r\n");
                }

                // Break meditate
                if (CommandTable[cmd].BreakMeditate)
                {
                    if (!ch.IsNPC() && ch.HasActBit(PC.PLAYER_MEDITATING))
                    {
                        ch.RemoveActBit(PC.PLAYER_MEDITATING);
                        ch.SendText("You stop meditating.\r\n");
                    }
                }

                // Break sneak, hide, and invis
                // Anything that will break hide OR invis will break concealment
                // This is DUMB!  Breaks invis w/backstab on a target that's not
                //   there: i.e. "backstab trolll"/"backstab humann" . vis and no
                //   attack! - (Should be handled with make_vis function).
                if (CommandTable[cmd].BreakInvisibility)
                {
                    if (ch.IsAffected(Affect.AFFECT_INVISIBLE))
                    {
                        ch.RemoveAffect(Affect.AFFECT_INVISIBLE);
                        ch.RemoveAffect(Affect.AFFECT_HIDE);
                        ch.RemoveAffect(Affect.AFFECT_MINOR_INVIS);

                        SocketConnection.Act("$n&n snaps into visibility.", ch, null, null, SocketConnection.MessageTarget.room);
                        ch.SendText("You snap into visibility.\r\n");
                    }
                    else if (ch.IsAffected(Affect.AFFECT_MINOR_INVIS))
                    {
                        ch.RemoveAffect(Affect.AFFECT_INVISIBLE);
                        ch.RemoveAffect(Affect.AFFECT_HIDE);
                        ch.RemoveAffect(Affect.AFFECT_MINOR_INVIS);

                        ch.SendText("You appear.\r\n");
                    }
                }

                if (CommandTable[cmd].BreakHide)
                {
                    if (ch.IsAffected(Affect.AFFECT_MINOR_INVIS))
                    {
                        ch.SendText("You appear.\r\n");
                    }
                    ch.AffectStrip( Affect.AffectType.skill, "shadow form");
                    ch.RemoveAffect( Affect.AFFECT_HIDE );
                    ch.RemoveAffect(Affect.AFFECT_MINOR_INVIS);
                }
            }
            // Command was not found, respond accordingly.
            else
            {
                // Look for command in socials table.
                if (!Database.SocialList.CheckSocial(ch, command, argument))
                {
                    if (!ch.IsNPC() && !MUDString.IsPrefixOf(command, "petition"))
                    {
                        string logBuf = String.Format("Log {0}: {1}", ch._name, logline);
                        Log.Trace(logBuf);
                        ImmortalChat.SendImmortalChat(ch, ImmortalChat.IMMTALK_SECURE, ch.GetTrust(), logBuf);
                        Command.Petition(ch, argument.Split(' '));
                        return;
                    }
                    Log.Trace("Failed to match command.");
                    ch.SendText("Huh?\r\n");
                }
                return;
            }

            // Character not in position for command?
            if (ch._position < CommandTable[cmd].MinPosition)
            {
                switch (ch._position)
                {
                    case Position.dead:
                        ch.SendText("Lie still; you are &+rDEAD&n!\r\n");
                        break;

                    case Position.mortally_wounded:
                    case Position.incapacitated:
                        ch.SendText("You are hurt far too bad for that.\r\n");
                        break;

                    case Position.stunned:
                        ch.SendText("You are too stunned to do that.\r\n");
                        break;

                    case Position.sleeping:
                        ch.SendText("In your dreams, or what?\r\n");
                        break;

                    case Position.reclining:
                        ch.SendText("You can't do that while lying around.\r\n");
                        break;

                    case Position.sitting:
                        ch.SendText("You can't do this sitting!\r\n");
                        break;

                    case Position.kneeling:
                        ch.SendText("Get off your knees!\r\n");
                        break;

                    case Position.resting:
                        ch.SendText("Nah... You feel too relaxed...\r\n");
                        break;

                    case Position.fighting:
                        ch.SendText("No way! You are still fighting!\r\n");
                        break;

                }
                if (!ch.IsImmortal())
                {
                    return;
                }
                if (ch._position == Position.dead)
                    ch._position = Position.sleeping;
                ch.SendText("You're not in the right position, but..\r\n");
            }
            if (ch.IsAffected(Affect.AFFECT_MINOR_PARA) &&
                CommandTable[cmd].Function != Command.LookCommand &&
                CommandTable[cmd].Function != Command.Score &&
                CommandTable[cmd].Function != Command.Attributes)
            {
                if (!ch.IsImmortal())
                {
                    ch.SendText("&+YYour mind moves, but your body doesn't.&n\r\n");
                    return;
                }
                ch.SendText("&+YYour immortality allows you to move!&n\r\n");
            }

            string[] str = argument.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            // Dispatch the command.  Catch any exceptions, since most exceptions will probably happen
            // based on user commands.
            try
            {
                (CommandTable[cmd].Function)(ch, str);
            }
            catch (Exception ex)
            {
                Log.Error("Exception in CommandType.Interpret: " + ex);
            }
            return;
        }

        /// <summary>
        /// This function is used to compare an integer to the list of commands in DE
        /// to see if a teleport or switch trigger will work.
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool CheckCommandTrigger(string argument, int number)
        {
            switch (number)
            {
                default:
                    Log.Error("Command {0} is not a valid teleport/switch trigger command!\r\n", number);
                    break;
                case 1:
                    if (!MUDString.StringsNotEqual(argument, "north"))
                        return true;
                    return false;

                case 2:
                    if (!MUDString.StringsNotEqual(argument, "east"))
                        return true;
                    return false;

                case 3:
                    if (!MUDString.StringsNotEqual(argument, "south"))
                        return true;
                    return false;

                case 4:
                    if (!MUDString.StringsNotEqual(argument, "west"))
                        return true;
                    return false;
                case 5:
                    if (!MUDString.StringsNotEqual(argument, "up"))
                        return true;
                    return false;
                case 6:
                    if (!MUDString.StringsNotEqual(argument, "down"))
                        return true;
                    return false;
                case 7:
                    if (!MUDString.StringsNotEqual(argument, "enter"))
                        return true;
                    return false;
                case 8:
                    if (!MUDString.StringsNotEqual(argument, "turn"))
                        return true;
                    return false;
                case 9:
                    if (!MUDString.StringsNotEqual(argument, "kiss"))
                        return true;
                    return false;
                case 10:
                    if (!MUDString.StringsNotEqual(argument, "get"))
                        return true;
                    return false;
                case 11:
                    if (!MUDString.StringsNotEqual(argument, "drink"))
                        return true;
                    return false;
                case 12:
                    if (!MUDString.StringsNotEqual(argument, "eat"))
                        return true;
                    return false;
                case 13:
                    if (!MUDString.StringsNotEqual(argument, "wear"))
                        return true;
                    return false;

                case 14:
                    if (!MUDString.StringsNotEqual(argument, "wield"))
                        return true;
                    return false;
                case 15:
                    if (!MUDString.StringsNotEqual(argument, "look"))
                        return true;
                    return false;

                case 17:
                    if (!MUDString.StringsNotEqual(argument, "say"))
                        return true;
                    return false;

                case 18:
                    if (!MUDString.StringsNotEqual(argument, "shout"))
                        return true;
                    return false;

                case 19:
                    if (!MUDString.StringsNotEqual(argument, "tell"))
                        return true;
                    return false;

                case 20:
                    if (!MUDString.StringsNotEqual(argument, "climb"))
                        return true;
                    return false;

                case 21:
                    if (!MUDString.StringsNotEqual(argument, "swing"))
                        return true;
                    return false;

                case 23:
                    if (!MUDString.StringsNotEqual(argument, "smile"))
                        return true;
                    return false;

                case 24:
                    if (!MUDString.StringsNotEqual(argument, "dance"))
                        return true;
                    return false;

                case 25:
                    if (!MUDString.StringsNotEqual(argument, "kill"))
                        return true;
                    return false;

                case 26:
                    if (!MUDString.StringsNotEqual(argument, "cackle"))
                        return true;
                    return false;

                case 27:
                    if (!MUDString.StringsNotEqual(argument, "laugh"))
                        return true;
                    return false;

                case 28:
                    if (!MUDString.StringsNotEqual(argument, "giggle"))
                        return true;
                    return false;

                case 29:
                    if (!MUDString.StringsNotEqual(argument, "shake"))
                        return true;
                    return false;

                case 30:
                    if (!MUDString.StringsNotEqual(argument, "puke"))
                        return true;
                    return false;

                case 31:
                    if (!MUDString.StringsNotEqual(argument, "growl"))
                        return true;
                    return false;

                case 32:
                    if (!MUDString.StringsNotEqual(argument, "scream"))
                        return true;
                    return false;

                case 33:
                    if (!MUDString.StringsNotEqual(argument, "insult"))
                        return true;
                    return false;

                case 34:
                    if (!MUDString.StringsNotEqual(argument, "comfort"))
                        return true;
                    return false;

                case 35:
                    if (!MUDString.StringsNotEqual(argument, "nod"))
                        return true;
                    return false;

                case 36:
                    if (!MUDString.StringsNotEqual(argument, "sigh"))
                        return true;
                    return false;

                case 37:
                    if (!MUDString.StringsNotEqual(argument, "sulk"))
                        return true;
                    return false;

                case 42:
                    if (!MUDString.StringsNotEqual(argument, "stand"))
                        return true;
                    return false;

                case 43:
                    if (!MUDString.StringsNotEqual(argument, "sit"))
                        return true;
                    return false;

                case 44:
                    if (!MUDString.StringsNotEqual(argument, "rest"))
                        return true;
                    return false;

                case 45:
                    if (!MUDString.StringsNotEqual(argument, "sleep"))
                        return true;
                    return false;

                case 46:
                    if (!MUDString.StringsNotEqual(argument, "wake"))
                        return true;
                    return false;

                case 49:
                    if (!MUDString.StringsNotEqual(argument, "hug"))
                        return true;
                    return false;

                case 50:
                    if (!MUDString.StringsNotEqual(argument, "snuggle"))
                        return true;
                    return false;

                case 51:
                    if (!MUDString.StringsNotEqual(argument, "cuddle"))
                        return true;
                    return false;

                case 52:
                    if (!MUDString.StringsNotEqual(argument, "nuzzle"))
                        return true;
                    return false;

                case 53:
                    if (!MUDString.StringsNotEqual(argument, "cry"))
                        return true;
                    return false;

                case 60:
                    if (!MUDString.StringsNotEqual(argument, "drop"))
                        return true;
                    return false;

                case 62:
                    if (!MUDString.StringsNotEqual(argument, "_weather"))
                        return true;
                    return false;

                case 63:
                    if (!MUDString.StringsNotEqual(argument, "read"))
                        return true;
                    return false;

                case 64:
                    if (!MUDString.StringsNotEqual(argument, "pour"))
                        return true;
                    return false;

                case 65:
                    if (!MUDString.StringsNotEqual(argument, "grab"))
                        return true;
                    return false;

                case 67:
                    if (!MUDString.StringsNotEqual(argument, "put"))
                        return true;
                    return false;

                case 70:
                    if (!MUDString.StringsNotEqual(argument, "hit"))
                        return true;
                    return false;

                case 72:
                    if (!MUDString.StringsNotEqual(argument, "give"))
                        return true;
                    return false;

                case 83:
                    if (!MUDString.StringsNotEqual(argument, "whisper"))
                        return true;
                    return false;

                case 86:
                    if (!MUDString.StringsNotEqual(argument, "ask"))
                        return true;
                    return false;

                case 87:
                    if (!MUDString.StringsNotEqual(argument, "order"))
                        return true;
                    return false;

                case 88:
                    if (!MUDString.StringsNotEqual(argument, "sip"))
                        return true;
                    return false;

                case 89:
                    if (!MUDString.StringsNotEqual(argument, "taste"))
                        return true;
                    return false;

                case 91:
                    if (!MUDString.StringsNotEqual(argument, "follow"))
                        return true;
                    return false;

                case 93:
                    if (!MUDString.StringsNotEqual(argument, "offer"))
                        return true;
                    return false;

                case 94:
                    if (!MUDString.StringsNotEqual(argument, "poke"))
                        return true;
                    return false;

                case 96:
                    if (!MUDString.StringsNotEqual(argument, "accuse"))
                        return true;
                    return false;

                case 97:
                    if (!MUDString.StringsNotEqual(argument, "grin"))
                        return true;
                    return false;

                case 98:
                    if (!MUDString.StringsNotEqual(argument, "bow"))
                        return true;
                    return false;

                case 99:
                    if (!MUDString.StringsNotEqual(argument, "open"))
                        return true;
                    return false;

                case 100:
                    if (!MUDString.StringsNotEqual(argument, "close"))
                        return true;
                    return false;

                case 101:
                    if (!MUDString.StringsNotEqual(argument, "lock"))
                        return true;
                    return false;

                case 102:
                    if (!MUDString.StringsNotEqual(argument, "unlock"))
                        return true;
                    return false;

                case 103:
                    if (!MUDString.StringsNotEqual(argument, "leave"))
                        return true;
                    return false;

                case 104:
                    if (!MUDString.StringsNotEqual(argument, "applaud"))
                        return true;
                    return false;

                case 105:
                    if (!MUDString.StringsNotEqual(argument, "blush"))
                        return true;
                    return false;

                case 106:
                    if (!MUDString.StringsNotEqual(argument, "burp"))
                        return true;
                    return false;

                case 107:
                    if (!MUDString.StringsNotEqual(argument, "chuckle"))
                        return true;
                    return false;

                case 108:
                    if (!MUDString.StringsNotEqual(argument, "clap"))
                        return true;
                    return false;

                case 109:
                    if (!MUDString.StringsNotEqual(argument, "cough"))
                        return true;
                    return false;

                case 110:
                    if (!MUDString.StringsNotEqual(argument, "curtsey"))
                        return true;
                    return false;

                case 111:
                    if (!MUDString.StringsNotEqual(argument, "fart"))
                        return true;
                    return false;

                case 112:
                    if (!MUDString.StringsNotEqual(argument, "flip"))
                        return true;
                    return false;

                case 113:
                    if (!MUDString.StringsNotEqual(argument, "fondle"))
                        return true;
                    return false;

                case 114:
                    if (!MUDString.StringsNotEqual(argument, "frown"))
                        return true;
                    return false;

                case 115:
                    if (!MUDString.StringsNotEqual(argument, "gasp"))
                        return true;
                    return false;

                case 116:
                    if (!MUDString.StringsNotEqual(argument, "glare"))
                        return true;
                    return false;

                case 117:
                    if (!MUDString.StringsNotEqual(argument, "groan"))
                        return true;
                    return false;

                case 118:
                    if (!MUDString.StringsNotEqual(argument, "grope"))
                        return true;
                    return false;

                case 119:
                    if (!MUDString.StringsNotEqual(argument, "hiccup"))
                        return true;
                    return false;

                case 120:
                    if (!MUDString.StringsNotEqual(argument, "lick"))
                        return true;
                    return false;

                case 121:
                    if (!MUDString.StringsNotEqual(argument, "love"))
                        return true;
                    return false;

                case 122:
                    if (!MUDString.StringsNotEqual(argument, "moan"))
                        return true;
                    return false;

                case 123:
                    if (!MUDString.StringsNotEqual(argument, "nibble"))
                        return true;
                    return false;

                case 124:
                    if (!MUDString.StringsNotEqual(argument, "pout"))
                        return true;
                    return false;

                case 125:
                    if (!MUDString.StringsNotEqual(argument, "purr"))
                        return true;
                    return false;

                case 126:
                    if (!MUDString.StringsNotEqual(argument, "ruffle"))
                        return true;
                    return false;

                case 127:
                    if (!MUDString.StringsNotEqual(argument, "shiver"))
                        return true;
                    return false;

                case 128:
                    if (!MUDString.StringsNotEqual(argument, "shrug"))
                        return true;
                    return false;

                case 129:
                    if (!MUDString.StringsNotEqual(argument, "Sing"))
                        return true;
                    return false;

                case 130:
                    if (!MUDString.StringsNotEqual(argument, "slap"))
                        return true;
                    return false;

                case 131:
                    if (!MUDString.StringsNotEqual(argument, "smirk"))
                        return true;
                    return false;

                case 132:
                    if (!MUDString.StringsNotEqual(argument, "snap"))
                        return true;
                    return false;

                case 133:
                    if (!MUDString.StringsNotEqual(argument, "sneeze"))
                        return true;
                    return false;

                case 134:
                    if (!MUDString.StringsNotEqual(argument, "snicker"))
                        return true;
                    return false;

                case 135:
                    if (!MUDString.StringsNotEqual(argument, "sniff"))
                        return true;
                    return false;

                case 136:
                    if (!MUDString.StringsNotEqual(argument, "snore"))
                        return true;
                    return false;

                case 137:
                    if (!MUDString.StringsNotEqual(argument, "spit"))
                        return true;
                    return false;

                case 138:
                    if (!MUDString.StringsNotEqual(argument, "squeeze"))
                        return true;
                    return false;

                case 139:
                    if (!MUDString.StringsNotEqual(argument, "stare"))
                        return true;
                    return false;

                case 140:
                    if (!MUDString.StringsNotEqual(argument, "strut"))
                        return true;
                    return false;

                case 141:
                    if (!MUDString.StringsNotEqual(argument, "thank"))
                        return true;
                    return false;

                case 142:
                    if (!MUDString.StringsNotEqual(argument, "twiddle"))
                        return true;
                    return false;

                case 143:
                    if (!MUDString.StringsNotEqual(argument, "wave"))
                        return true;
                    return false;

                case 144:
                    if (!MUDString.StringsNotEqual(argument, "whistle"))
                        return true;
                    return false;

                case 145:
                    if (!MUDString.StringsNotEqual(argument, "wiggle"))
                        return true;
                    return false;

                case 146:
                    if (!MUDString.StringsNotEqual(argument, "wink"))
                        return true;
                    return false;

                case 147:
                    if (!MUDString.StringsNotEqual(argument, "yawn"))
                        return true;
                    return false;

                case 150:
                    if (!MUDString.StringsNotEqual(argument, "hold"))
                        return true;
                    return false;

                case 155:
                    if (!MUDString.StringsNotEqual(argument, "pick"))
                        return true;
                    return false;

                case 156:
                    if (!MUDString.StringsNotEqual(argument, "steal"))
                        return true;
                    return false;

                case 160:
                    if (!MUDString.StringsNotEqual(argument, "french"))
                        return true;
                    return false;

                case 161:
                    if (!MUDString.StringsNotEqual(argument, "comb"))
                        return true;
                    return false;

                case 162:
                    if (!MUDString.StringsNotEqual(argument, "massage"))
                        return true;
                    return false;

                case 163:
                    if (!MUDString.StringsNotEqual(argument, "tickle"))
                        return true;
                    return false;

                case 165:
                    if (!MUDString.StringsNotEqual(argument, "pat"))
                        return true;
                    return false;

                case 166:
                    if (!MUDString.StringsNotEqual(argument, "examine"))
                        return true;
                    return false;

                case 167:
                    if (!MUDString.StringsNotEqual(argument, "take"))
                        return true;
                    return false;

                case 169:
                    if (!MUDString.StringsNotEqual(argument, "spells"))
                        return true;
                    return false;

                case 171:
                    if (!MUDString.StringsNotEqual(argument, "curse"))
                        return true;
                    return false;

                case 172:
                    if (!MUDString.StringsNotEqual(argument, "use"))
                        return true;
                    return false;

                case 178:
                    if (!MUDString.StringsNotEqual(argument, "beg"))
                        return true;
                    return false;

                case 179:
                    if (!MUDString.StringsNotEqual(argument, "bleed"))
                        return true;
                    return false;

                case 180:
                    if (!MUDString.StringsNotEqual(argument, "cringe"))
                        return true;
                    return false;

                case 181:
                    if (!MUDString.StringsNotEqual(argument, "dream"))
                        return true;
                    return false;

                case 182:
                    if (!MUDString.StringsNotEqual(argument, "fume"))
                        return true;
                    return false;

                case 183:
                    if (!MUDString.StringsNotEqual(argument, "grovel"))
                        return true;
                    return false;

                case 184:
                    if (!MUDString.StringsNotEqual(argument, "hop"))
                        return true;
                    return false;

                case 185:
                    if (!MUDString.StringsNotEqual(argument, "nudge"))
                        return true;
                    return false;

                case 186:
                    if (!MUDString.StringsNotEqual(argument, "peer"))
                        return true;
                    return false;

                case 187:
                    if (!MUDString.StringsNotEqual(argument, "point"))
                        return true;
                    return false;

                case 188:
                    if (!MUDString.StringsNotEqual(argument, "ponder"))
                        return true;
                    return false;

                case 189:
                    if (!MUDString.StringsNotEqual(argument, "punch"))
                        return true;
                    return false;

                case 190:
                    if (!MUDString.StringsNotEqual(argument, "snarl"))
                        return true;
                    return false;

                case 191:
                    if (!MUDString.StringsNotEqual(argument, "spank"))
                        return true;
                    return false;

                case 192:
                    if (!MUDString.StringsNotEqual(argument, "steam"))
                        return true;
                    return false;

                case 193:
                    if (!MUDString.StringsNotEqual(argument, "tackle"))
                        return true;
                    return false;

                case 194:
                    if (!MUDString.StringsNotEqual(argument, "taunt"))
                        return true;
                    return false;

                case 195:
                    if (!MUDString.StringsNotEqual(argument, "think"))
                        return true;
                    return false;

                case 196:
                    if (!MUDString.StringsNotEqual(argument, "whine"))
                        return true;
                    return false;

                case 197:
                    if (!MUDString.StringsNotEqual(argument, "worship"))
                        return true;
                    return false;

                case 198:
                    if (!MUDString.StringsNotEqual(argument, "yodel"))
                        return true;
                    return false;

                case 206:
                    if (!MUDString.StringsNotEqual(argument, "quaff"))
                        return true;
                    return false;

                case 207:
                    if (!MUDString.StringsNotEqual(argument, "recite"))
                        return true;
                    return false;

                case 240:
                    if (!MUDString.StringsNotEqual(argument, "fill"))
                        return true;
                    return false;

                case 248:
                    if (!MUDString.StringsNotEqual(argument, "listen"))
                        return true;
                    return false;

                case 256:
                    if (!MUDString.StringsNotEqual(argument, "bribe"))
                        return true;
                    return false;

                case 257:
                    if (!MUDString.StringsNotEqual(argument, "bonk"))
                        return true;
                    return false;

                case 258:
                    if (!MUDString.StringsNotEqual(argument, "calm"))
                        return true;
                    return false;

                case 259:
                    if (!MUDString.StringsNotEqual(argument, "rub"))
                        return true;
                    return false;

                case 260:
                    if (!MUDString.StringsNotEqual(argument, "censor"))
                        return true;
                    return false;

                case 261:
                    if (!MUDString.StringsNotEqual(argument, "choke"))
                        return true;
                    return false;

                case 262:
                    if (!MUDString.StringsNotEqual(argument, "drool"))
                        return true;
                    return false;

                case 263:
                    if (!MUDString.StringsNotEqual(argument, "flex"))
                        return true;
                    return false;

                case 264:
                    if (!MUDString.StringsNotEqual(argument, "jump"))
                        return true;
                    return false;

                case 265:
                    if (!MUDString.StringsNotEqual(argument, "lean"))
                        return true;
                    return false;

                case 266:
                    if (!MUDString.StringsNotEqual(argument, "moon"))
                        return true;
                    return false;

                case 267:
                    if (!MUDString.StringsNotEqual(argument, "ogle"))
                        return true;
                    return false;

                case 268:
                    if (!MUDString.StringsNotEqual(argument, "pant"))
                        return true;
                    return false;

                case 269:
                    if (!MUDString.StringsNotEqual(argument, "pinch"))
                        return true;
                    return false;

                case 270:
                    if (!MUDString.StringsNotEqual(argument, "push"))
                        return true;
                    return false;

                case 271:
                    if (!MUDString.StringsNotEqual(argument, "scare"))
                        return true;
                    return false;

                case 272:
                    if (!MUDString.StringsNotEqual(argument, "scold"))
                        return true;
                    return false;

                case 273:
                    if (!MUDString.StringsNotEqual(argument, "seduce"))
                        return true;
                    return false;

                case 274:
                    if (!MUDString.StringsNotEqual(argument, "shove"))
                        return true;
                    return false;

                case 275:
                    if (!MUDString.StringsNotEqual(argument, "shudder"))
                        return true;
                    return false;

                case 276:
                    if (!MUDString.StringsNotEqual(argument, "shush"))
                        return true;
                    return false;

                case 277:
                    if (!MUDString.StringsNotEqual(argument, "slobber"))
                        return true;
                    return false;

                case 278:
                    if (!MUDString.StringsNotEqual(argument, "smell"))
                        return true;
                    return false;

                case 279:
                    if (!MUDString.StringsNotEqual(argument, "sneer"))
                        return true;
                    return false;

                case 280:
                    if (!MUDString.StringsNotEqual(argument, "spin"))
                        return true;
                    return false;

                case 281:
                    if (!MUDString.StringsNotEqual(argument, "squirm"))
                        return true;
                    return false;

                case 282:
                    if (!MUDString.StringsNotEqual(argument, "stomp"))
                        return true;
                    return false;

                case 283:
                    if (!MUDString.StringsNotEqual(argument, "strangle"))
                        return true;
                    return false;

                case 284:
                    if (!MUDString.StringsNotEqual(argument, "stretch"))
                        return true;
                    return false;

                case 285:
                    if (!MUDString.StringsNotEqual(argument, "tap"))
                        return true;
                    return false;

                case 286:
                    if (!MUDString.StringsNotEqual(argument, "tease"))
                        return true;
                    return false;

                case 287:
                    if (!MUDString.StringsNotEqual(argument, "tiptoe"))
                        return true;
                    return false;

                case 288:
                    if (!MUDString.StringsNotEqual(argument, "tweak"))
                        return true;
                    return false;

                case 289:
                    if (!MUDString.StringsNotEqual(argument, "twirl"))
                        return true;
                    return false;

                case 290:
                    if (!MUDString.StringsNotEqual(argument, "undress"))
                        return true;
                    return false;

                case 291:
                    if (!MUDString.StringsNotEqual(argument, "whimper"))
                        return true;
                    return false;

                case 292:
                    if (!MUDString.StringsNotEqual(argument, "exchange"))
                        return true;
                    return false;

                case 293:
                    if (!MUDString.StringsNotEqual(argument, "release"))
                        return true;
                    return false;

                case 294:
                    if (!MUDString.StringsNotEqual(argument, "search"))
                        return true;
                    return false;

                case 306:
                    if (!MUDString.StringsNotEqual(argument, "caress"))
                        return true;
                    return false;

                case 314:
                    if (!MUDString.StringsNotEqual(argument, "yell"))
                        return true;
                    return false;

                case 320:
                    if (!MUDString.StringsNotEqual(argument, "touch"))
                        return true;
                    return false;

                case 321:
                    if (!MUDString.StringsNotEqual(argument, "scratch"))
                        return true;
                    return false;

                case 322:
                    if (!MUDString.StringsNotEqual(argument, "wince"))
                        return true;
                    return false;

                case 323:
                    if (!MUDString.StringsNotEqual(argument, "toss"))
                        return true;
                    return false;

                case 324:
                    if (!MUDString.StringsNotEqual(argument, "flame"))
                        return true;
                    return false;

                case 325:
                    if (!MUDString.StringsNotEqual(argument, "arch"))
                        return true;
                    return false;

                case 326:
                    if (!MUDString.StringsNotEqual(argument, "amaze"))
                        return true;
                    return false;

                case 327:
                    if (!MUDString.StringsNotEqual(argument, "bathe"))
                        return true;
                    return false;

                case 328:
                    if (!MUDString.StringsNotEqual(argument, "embrace"))
                        return true;
                    return false;

                case 329:
                    if (!MUDString.StringsNotEqual(argument, "brb"))
                        return true;
                    return false;

                case 330:
                    if (!MUDString.StringsNotEqual(argument, "ack"))
                        return true;
                    return false;

                case 331:
                    if (!MUDString.StringsNotEqual(argument, "cheer"))
                        return true;
                    return false;

                case 332:
                    if (!MUDString.StringsNotEqual(argument, "snort"))
                        return true;
                    return false;

                case 333:
                    if (!MUDString.StringsNotEqual(argument, "eyebrow"))
                        return true;
                    return false;

                case 334:
                    if (!MUDString.StringsNotEqual(argument, "bang"))
                        return true;
                    return false;

                case 335:
                    if (!MUDString.StringsNotEqual(argument, "pillow"))
                        return true;
                    return false;

                case 336:
                    if (!MUDString.StringsNotEqual(argument, "nap"))
                        return true;
                    return false;

                case 337:
                    if (!MUDString.StringsNotEqual(argument, "nose"))
                        return true;
                    return false;

                case 338:
                    if (!MUDString.StringsNotEqual(argument, "raise"))
                        return true;
                    return false;

                case 339:
                    if (!MUDString.StringsNotEqual(argument, "hand"))
                        return true;
                    return false;

                case 340:
                    if (!MUDString.StringsNotEqual(argument, "pull"))
                        return true;
                    return false;

                case 341:
                    if (!MUDString.StringsNotEqual(argument, "tug"))
                        return true;
                    return false;

                case 342:
                    if (!MUDString.StringsNotEqual(argument, "wet"))
                        return true;
                    return false;

                case 343:
                    if (!MUDString.StringsNotEqual(argument, "mosh"))
                        return true;
                    return false;

                case 344:
                    if (!MUDString.StringsNotEqual(argument, "wait"))
                        return true;
                    return false;

                case 345:
                    if (!MUDString.StringsNotEqual(argument, "hi5"))
                        return true;
                    return false;

                case 346:
                    if (!MUDString.StringsNotEqual(argument, "envy"))
                        return true;
                    return false;

                case 347:
                    if (!MUDString.StringsNotEqual(argument, "flirt"))
                        return true;
                    return false;

                case 348:
                    if (!MUDString.StringsNotEqual(argument, "bark"))
                        return true;
                    return false;

                case 349:
                    if (!MUDString.StringsNotEqual(argument, "whap"))
                        return true;
                    return false;
                case 350:
                    if (!MUDString.StringsNotEqual(argument, "roll"))
                        return true;
                    return false;
                case 351:
                    if (!MUDString.StringsNotEqual(argument, "blink"))
                        return true;
                    return false;
                case 352:
                    if (!MUDString.StringsNotEqual(argument, "duh"))
                        return true;
                    return false;
                case 353:
                    if (!MUDString.StringsNotEqual(argument, "gag"))
                        return true;
                    return false;
                case 354:
                    if (!MUDString.StringsNotEqual(argument, "grumble"))
                        return true;
                    return false;
                case 355:
                    if (!MUDString.StringsNotEqual(argument, "dropkick"))
                        return true;
                    return false;
                case 356:
                    if (!MUDString.StringsNotEqual(argument, "whatever"))
                        return true;
                    return false;
                case 357:
                    if (!MUDString.StringsNotEqual(argument, "fool"))
                        return true;
                    return false;
                case 358:
                    if (!MUDString.StringsNotEqual(argument, "noogie"))
                        return true;
                    return false;
                case 359:
                    if (!MUDString.StringsNotEqual(argument, "melt"))
                        return true;
                    return false;
                case 360:
                    if (!MUDString.StringsNotEqual(argument, "smoke"))
                        return true;
                    return false;
                case 361:
                    if (!MUDString.StringsNotEqual(argument, "wheeze"))
                        return true;
                    return false;
                case 362:
                    if (!MUDString.StringsNotEqual(argument, "bird"))
                        return true;
                    return false;
                case 363:
                    if (!MUDString.StringsNotEqual(argument, "boggle"))
                        return true;
                    return false;
                case 364:
                    if (!MUDString.StringsNotEqual(argument, "hiss"))
                        return true;
                    return false;
                case 365:
                    if (!MUDString.StringsNotEqual(argument, "bite"))
                        return true;
                    return false;
            }

            return false;
        }
    }
}
