namespace MUDEngine
{
    /// <summary>
    /// Well-known object index numbers, defined in the required.are.xml area file.
    /// 
    /// Any objects used by code should be defined in the "required" area file.
    /// </summary>
    public class StaticObjects
    {
        /// <summary>
        /// This definition is used for corpse saving.  They are saved with index numbers
        /// so that they can be loaded back up over crashes and be allowed to
        /// decay after a while.  If there are enough zones to make this number a
        /// problem simply up the number.  The mud will start saving the corpses
        /// at the new number and after a reboot they will load at the higer index number.
        /// </summary>
        public const int CORPSE_INDEX_NUMBER = 1000000;

        // TODO: Verify that all of these numbers exist, and match the corresponding object
        // in required.are.xml.
        public const int OBJECT_NUMBER_OBJECT = 1;
        public const int OBJECT_NUMBER_MONEY_ONE = 2;
        public const int OBJECT_NUMBER_MONEY_SOME = 3;
        public const int OBJECT_NUMBER_51_POTION = 5;
        public const int OBJECT_NUMBER_52_POTION = 6;
        public const int OBJECT_NUMBER_53_POTION = 7;
        public const int OBJECT_NUMBER_54_POTION = 8;
        public const int OBJECT_NUMBER_55_POTION = 9;
        public const int OBJECT_NUMBER_CORPSE_NPC = 10;
        public const int OBJECT_NUMBER_CORPSE_PC = 11;
        public const int OBJECT_NUMBER_SEVERED_SCALP = 12;
        public const int OBJECT_NUMBER_TORN_HEART = 13;
        public const int OBJECT_NUMBER_SLICED_ARM = 14;
        public const int OBJECT_NUMBER_SLICED_LEG = 15;
        public const int OBJECT_NUMBER_SEVERED_SKULL = 16;
        public const int OBJECT_NUMBER_IRON_RATION = 20;
        public const int OBJECT_NUMBER_LIGHT_BALL = 21;
        public const int OBJECT_NUMBER_WATERSKIN = 22;
        public const int OBJECT_NUMBER_PORTAL = 23;
        public const int OBJECT_NUMBER_SPRING = 25;
        public const int OBJECT_NUMBER_WINDSABER = 26;
        public const int OBJECT_NUMBER_CELESTIAL_SWORD = 27;
        public const int OBJECT_NUMBER_STONES = 28;
        public const int OBJECT_NUMBER_GATE = 29;
        public const int OBJECT_NUMBER_MOONWELL = 30;
        public const int OBJECT_NUMBER_WORMHOLE = 31;
        public const int OBJECT_NUMBER_HYPNOTIC_PATTERN = 47;
        public const int OBJECT_NUMBER_WALL_IRON = 50;
        public const int OBJECT_NUMBER_WALL_STONE = 51;
        public const int OBJECT_NUMBER_WALL_FIRE = 52;
        public const int OBJECT_NUMBER_WALL_ILLUSION = 53;
        public const int OBJECT_NUMBER_WALL_ICE = 54;
        public const int OBJECT_NUMBER_WALL_FORCE = 55;
        public const int OBJECT_NUMBER_LIGHTNING_CURTAIN = 56;
        public const int OBJECT_NUMBER_WALL_SPARKS = 57;
        public const int OBJECT_NUMBER_WALL_MIST = 58;
        public const int OBJECT_NUMBER_WALL_WAVES = 59;
        public const int OBJECT_NUMBER_WALL_EARTH = 289;
        public const int OBJECT_NUMBER_WALL_BONES = 290;
        // Newbie items
        // New design allows a start index number and a range for random newbie equipment.
        public const int OBJECT_NUMBER_TORCH = 19;
        public const int OBJECT_NUMBER_QUILL = 24;
        public const int OBJECT_NUMBER_NEWBIE_VEST = 100;
        public const int NUM_NEWBIE_VEST = 10;
        public const int OBJECT_NUMBER_NEWBIE_HELM = 110;
        public const int NUM_NEWBIE_HELM = 5;
        public const int OBJECT_NUMBER_NEWBIE_SLEEVES = 120;
        public const int NUM_NEWBIE_SLEEVES = 3;
        public const int OBJECT_NUMBER_NEWBIE_PANTS = 130;
        public const int NUM_NEWBIE_PANTS = 3;
        public const int OBJECT_NUMBER_NEWBIE_BOOTS = 140;
        public const int NUM_NEWBIE_BOOTS = 2;
        public const int OBJECT_NUMBER_NEWBIE_CLOAK = 150;
        public const int NUM_NEWBIE_CLOAK = 2;
        public const int OBJECT_NUMBER_NEWBIE_SWORD = 200;
        public const int NUM_NEWBIE_SWORD = 10;
        public const int OBJECT_NUMBER_NEWBIE_DAGGER = 210;
        public const int NUM_NEWBIE_DAGGER = 13;
        public const int OBJECT_NUMBER_NEWBIE_2HSWORD = 223;
        public const int NUM_NEWBIE_2HSWORD = 16;
        public const int OBJECT_NUMBER_NEWBIE_KNIFE = 239;
        public const int NUM_NEWBIE_KNIFE = 4;
        public const int OBJECT_NUMBER_NEWBIE_SHORTSWORD = 243;
        public const int NUM_NEWBIE_SHORTSWORD = 11;
        public const int OBJECT_NUMBER_NEWBIE_MACE = 254;
        public const int NUM_NEWBIE_MACE = 10;
        public const int OBJECT_NUMBER_NEWBIE_STAFF = 264;
        public const int NUM_NEWBIE_STAFF = 10;
        public const int OBJECT_NUMBER_NEWBIE_TOTEM_A = 41;
        public const int OBJECT_NUMBER_NEWBIE_TOTEM_E = 42;
        public const int OBJECT_NUMBER_NEWBIE_TOTEM_S = 43;
        public const int OBJECT_NUMBER_NEWBIE_BACKPACK = 286;
    }
}