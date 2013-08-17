
namespace MUDEngine
{
    /// <summary>
    /// Represents information about a monk tradition.
    /// </summary>
    public class TraditionData
    {
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="trad"></param>
        /// <param name="name"></param>
        /// <param name="cost1"></param>
        /// <param name="cost2"></param>
        /// <param name="cost3"></param>
        /// <param name="cost4"></param>
        /// <param name="cost5"></param>
        public TraditionData( int trad, string name, int cost1, int cost2, int cost3, int cost4, int cost5 )
        {
            Tradition = trad;
            TraditionName = name;
            _cost[ 0 ] = cost1;
            _cost[ 1 ] = cost2;
            _cost[ 2 ] = cost3;
            _cost[ 3 ] = cost4;
            _cost[ 4 ] = cost5;
        }

        public int Tradition { get; set; }
        public string TraditionName { get; set; }
        private int[] _cost = new int[5];

        // Defines for monk orders and Const.MonkTraditions
        public const int TRAD_CELESTIAL_BEAR = 1;
        public const int TRAD_LONG_FANG_VIPERS = 2;
        public const int TRAD_GHOSTWALKERS = 3;
        public const int TRAD_FALCONS_TWILIGHT = 4;
        public const int TRAD_KINGS_THUNDER = 5;
        public const int TRAD_DRUNK_DRAGON = 6;
        public const int TRAD_FISTS_FOUR_FURIES = 7;

        /// <summary>
        /// List of tradition names.
        /// </summary>
        public static string[] Names = new[] 
        {
            "none",
            "Order of the Celestial Bear",
            "Long Fang Vipers",
            "Ghostwalkers",
            "Falcons of the Dying Twilight",
            "Kings of Thunder",
            "Order of The Drunken Green Dragon",
            "Fists of the Four Furies"
        };

        /// <summary>
        /// Tradition table.
        /// </summary>
        public static TraditionData[] Table =
        {
            new TraditionData( TRAD_CELESTIAL_BEAR, "stance_bear",0, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "stance_tiger", 20, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "fortitude", 1, 0, 5, 0, 15  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "might", 1, 0, 5, 0, 15  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "coordination", 5, 0, 20, -1, -1  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "strength", 2, 0, 8, 0, 20  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "constitution", 2, 0, 8, 0, 20  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "dexterity", 6,  0, -1, -1, -1  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "agility", 6, 0, -1, -1, -1  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "stone_palm",10, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "ivory_palm", 20, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "jade_palm", 40, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "lion_strike", 10, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "spider_strike", 15, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "wrist_lock", 25, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "hip_throw", 3, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "might_tiger", 3, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "way_stoic_bear", 10, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "stone_ox", 25, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "kangeiko", 25, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "shochu_geiko", 25, 0, 0, 0, 0  ),
            new TraditionData( TRAD_CELESTIAL_BEAR, "way_of_stone", 25, 25, 25, 25, 25  ),
            new TraditionData( TRAD_LONG_FANG_VIPERS, "stance_snake", 0, 0, 0, 0, 0  )

        };

        /// <summary>
        /// Gets and sets cost data.
        /// </summary>
        public int[] Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        
    };
}
