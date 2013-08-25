
namespace ModernMUD
{
    /// <summary>
    /// Liquid types - beverages, etc.
    /// </summary>
    public class Liquid
    {
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="liqname"></param>
        /// <param name="liqcolor"></param>
        /// <param name="drunk"></param>
        /// <param name="hunger"></param>
        /// <param name="thirst"></param>
        public Liquid(string liqname, string liqcolor, int drunk, int hunger, int thirst)
        {
            Name = liqname;
            Color = liqcolor;
            DrunkValue = drunk;
            HungerValue = hunger;
            ThirstValue = thirst;
        }

        /// <summary>
        /// Name of the liquid.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Color of the liquid.
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Food value of this drink.
        /// </summary>
        public int HungerValue { get; set; }
        /// <summary>
        /// Alcohol strength of this drink, zero for non-alcoholic.
        /// </summary>
        public int DrunkValue { get; set; }
        /// <summary>
        /// Thirst value of this drink.
        /// </summary>
        public int ThirstValue { get; set; }

        /// <summary>
        /// Liquid types and their properties.
        /// 
        /// Values are: name, color, drunk points, food points, thirst points.
        /// 
        /// Some of these types exist for compatibility with vintage areas.
        /// </summary>
        public static Liquid[] Table =
        {
            new Liquid( "water", "clear", 0, 0, 10 ), /* 0 */
            new Liquid( "beer", "amber", 3, 2, 5 ),
            new Liquid( "wine", "rose", 5, 2, 5 ),
            new Liquid( "ale", "brown", 2, 2, 5 ),
            new Liquid( "dark ale", "dark", 1, 2, 5 ),
            new Liquid( "whiskey", "golden", 6, 1, 4 ), /* 5 */
            new Liquid( "lemonade", "yellow", 0, 1, 8 ),
            new Liquid( "firewater", "red", 10, 0, 0 ),
            new Liquid( "scotch", "amber", 3, 3, 3 ),
            new Liquid( "slime", "green", 0, 4, -8 ),
            new Liquid( "milk", "white", 0, 3, 6 ), /* 10 */
            new Liquid( "tea", "tan", 0, 1, 6 ),
            new Liquid( "coffee", "black", 0, 1, 6 ),
            new Liquid( "blood", "red", 0, 2, -1 ),
            new Liquid( "salt water", "clear", 0, 1, -2 ),
            new Liquid( "cola", "brown", 0, 1, 5 ), /* 15 */
            new Liquid( "white wine", "golden", 5, 2, 5 ),
            new Liquid( "root beer", "brown", 0, 3, 6 ),
            new Liquid( "champagne", "golden", 5, 2, 5 ),
            new Liquid( "vodka", "clear", 7, 1, 4 ),
            new Liquid( "absinthe", "green", 10, 0, 0 ), /* 20 */
            new Liquid( "brandy", "golden", 5, 1, 4 ),
            new Liquid( "schnapps", "clear", 6, 1, 4 ),
            new Liquid( "orange juice", "orange", 0, 2, 8 ),
            new Liquid( "sherry", "red", 3, 2, 4 ),
            new Liquid( "rum", "amber", 8, 1, 4 ), /* 25 */
            new Liquid( "port", "red", 3, 3, 4 ),
            new Liquid( "holy water", "clear", 0, 0, 12 ),
            new Liquid( "unholy water", "clear", 0, 0, 12 ),
            new Liquid( "stout beer", "dark brown", 4, 3, 5 ),
            new Liquid( "sour milk", "chunky white", 0, 2, -1),
            new Liquid( "apple juice", "yellow", 0, 1, 6),
            new Liquid( "grape juice", "purple", 0, 1, 6),
            new Liquid( "urine", "yellow", 0, 0, -2),
        };
    }
}
