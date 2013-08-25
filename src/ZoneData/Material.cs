using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Represents a material that objects can be made of.  Defines physical properties
    /// of the material.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Material()
        {
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="durable"></param>
        /// <param name="typ"></param>
        /// <param name="val"></param>
        /// <param name="nam"></param>
        /// <param name="dmg"></param>
        public Material(string desc, int durable, MaterialType typ, int val, string nam, int dmg)
        {
            ShortDescription = desc;
            Durability = durable;
            Type = typ;
            Value = val;
            Name = nam;
            ExplosionDamage = dmg;
        }

        /// <summary>
        /// The short description of the material.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// The specific type of the material.
        /// </summary>
        public MaterialType Type { get; set; }

        /// <summary>
        /// The durability level of the material.
        /// </summary>
        public int Durability { get; set; }

        /// <summary>
        /// The base value of the material.  Used in calculating item value based
        /// on its constituent parts.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// The name of the material.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The explosion damage ratio used when the material detonates, for
        /// use with shatter, overcharge, etc.
        /// 
        /// Used with shatter spell, or when an item otherwise explodes for whatever
        /// reason, such as a failed enchant.  Should take into account the shard-factor
        /// and hardness of the item.
        ///
        /// Valid values are 1-5.
        /// </summary>
        public int ExplosionDamage { get; set; }

        /// <summary>
        /// Material types for objects.  Must be kept in sync with the material table.
        /// </summary>
        public enum MaterialType
        {
            undefined = 0,
            nonsubstantial,
            flesh,
            cloth,
            bark,
            softwood,
            hardwood,
            glass,
            crystal,
            ceramic,
            bone,
            stone,
            hide,
            leather,
            cured_leather,
            iron,
            steel,
            brass,
            mithril,
            adamantium,
            bronze,
            copper,
            silver,
            electrum,
            gold,
            platinum,
            gem,
            diamond,
            paper,
            parchment,
            leaves,
            ruby,
            emerald,
            sapphire,
            ivory,
            dragonscale,
            obsidian,
            granite,
            marble,
            limestone,
            liquid,
            bamboo,
            reeds,
            hemp,
            glassteel,
            eggshell,
            chitinous,
            reptilescale,
            generic_food,
            rubber,
            feather,
            wax,
            pearl,
            silk,
            spidersilk,
            jade,
            lead
        }
                
        /// <summary>
        /// Material Table: Material string, type, durability, value.
        /// 
        /// Used for looking up strings, durability for eq damage, and values for
        /// automatic price setting on objects.
        /// 
        /// Durability is on a scale of 1 to 5.
        /// 1 = Fragile, like eggshell or paper
        /// 2 = Fairly frail, like glass or thin wood 
        /// 3 = Average, like wood
        /// 4 = Fairly durable, like bronze or stone
        /// 5 = Very durable, like steel or adamantium
        /// 
        /// This table must be kept in sync with the Type enumeration.
        /// </summary>
        public static Material[] Table = new[]  
        {
            // Name          Durable Material           Cost  String
            new Material(  "undefined",      3,  MaterialType.undefined,      100, "&+wundefined&n", 1     ),  //  0
            new Material(  "nonsubstantial", 3,  MaterialType.nonsubstantial, 100, "&+wnonsubstantial&n", 0),  //  1
            new Material(  "flesh",          2,  MaterialType.flesh,           30, "&+Rflesh&n", 2         ),
            new Material(  "cloth",          2,  MaterialType.cloth,           60, "&+wcloth&n", 1         ),
            new Material(  "bark",           2,  MaterialType.bark,            20, "&+ybark&n", 3          ),
            new Material(  "hard wood",      2,  MaterialType.softwood,        50, "&+yhard wood&n", 2     ),  //  5
            new Material(  "soft wood",      3,  MaterialType.hardwood,        70, "&+ysoft wood&n", 3     ),
            new Material(  "glass",          2,  MaterialType.glass,          130, "&+wglass&n" , 5        ),
            new Material(  "crystal",        2,  MaterialType.crystal,        140, "&+wcrystal&n", 4       ),
            new Material(  "ceramic",        2,  MaterialType.ceramic,         65, "&+wceramic&n", 3       ),
            new Material(  "bone",           3,  MaterialType.bone,            35, "&+Wbone&n", 3          ),  //  10
            new Material(  "stone",          4,  MaterialType.stone,           80, "&+wstone&n", 4         ),
            new Material(  "hide",           3,  MaterialType.hide,            60, "&+yhide&n", 2          ),
            new Material(  "leather",        2,  MaterialType.leather,         80, "&+yleather&n", 2       ),
            new Material(  "cured leather",  3,  MaterialType.cured_leather,   85, "&+ycured leather&n", 2 ),
            new Material(  "iron",           4,  MaterialType.iron,           100, "&+wiron&n", 4          ),  //  15
            new Material(  "steel",          5,  MaterialType.steel,          120, "&+wsteel&n", 4         ),
            new Material(  "brass",          4,  MaterialType.brass,           95, "&+Ybrass&n", 4         ),
            new Material(  "mithril",        5,  MaterialType.mithril,        150, "&+wmithril&n", 5       ),
            new Material(  "adamantium",     5,  MaterialType.adamantium,     200, "&+wadamantium&n", 5    ),
            new Material(  "bronze",         4,  MaterialType.bronze,          90, "&+ybronze&n", 4        ),  //  20
            new Material(  "copper",         4,  MaterialType.copper,          50, "&+ycopper&n", 4        ),
            new Material(  "silver",         4,  MaterialType.silver,         110, "&+wsilver&n", 4        ),
            new Material(  "electrum",       4,  MaterialType.electrum,       150, "&+welectrum&n", 4      ),
            new Material(  "gold",           4,  MaterialType.gold,           300, "&+Ygold&n", 4          ),
            new Material(  "platinum",       4,  MaterialType.platinum,       700, "&+Wplatinum&n", 4      ),  //  25
            new Material(  "gem",            5,  MaterialType.gem,            750, "&+wgem&n", 4           ),
            new Material(  "diamond",        5,  MaterialType.diamond,       1000, "&+Wdiamond&n", 4       ),
            new Material(  "paper",          1,  MaterialType.paper,           35, "&+wpaper&n", 1         ),
            new Material(  "parchment",      1,  MaterialType.parchment,       25, "&+wparchment&n", 1     ),
            new Material(  "leaves",         1,  MaterialType.leaves,          20, "&+gleaves&n", 1        ),  //  30
            new Material(  "ruby",           5,  MaterialType.ruby,           800, "&+rruby&n", 4          ),
            new Material(  "emerald",        5,  MaterialType.emerald,        900, "&+Gemerald&n", 4       ),
            new Material(  "sapphire",       5,  MaterialType.sapphire,       950, "&+Bsapphire&n", 4      ),
            new Material(  "ivory",          4,  MaterialType.ivory,          300, "&+wivory&n", 3         ),
            new Material(  "dragonscale",    4,  MaterialType.dragonscale,    220, "&+gdragonscale&n", 2   ),  //  35
            new Material(  "obsidian",       4,  MaterialType.obsidian,       130, "&+Lobsidian&n", 4      ),
            new Material(  "granite",        4,  MaterialType.granite,         88, "&+wgranite&n", 4       ),
            new Material(  "marble",         4,  MaterialType.marble,          83, "&+wmarble&n", 4        ),
            new Material(  "limestone",      4,  MaterialType.limestone,       68, "&+wlimestone&n", 3     ),
            new Material(  "liquid",         3,  MaterialType.liquid,         100, "&+wliquid&n", 1        ),  //  40
            new Material(  "bamboo",         2,  MaterialType.bamboo,          30, "&+wbamboo&n", 2        ),
            new Material(  "reeds",          2,  MaterialType.reeds,           28, "&+wreeds&n", 2         ),
            new Material(  "hemp",           2,  MaterialType.hemp,            42, "&+ghemp&n", 1          ),
            new Material(  "glassteel",      5,  MaterialType.glassteel,      125, "&+wglassteel&n", 5     ),
            new Material(  "eggshell",       1,  MaterialType.eggshell,        15, "&+weggshell&n", 1      ),  //  45
            new Material(  "chitinous",      3,  MaterialType.chitinous,       22, "&+wchitinous&n", 2     ),
            new Material(  "reptile scale",  3,  MaterialType.reptilescale,    82, "&+greptilescale&n", 2  ),
            new Material(  "generic food",   2,  MaterialType.generic_food,   100, "&+wfood&n", 2          ),
            new Material(  "rubber",         3,  MaterialType.rubber,          80, "&+wrubber&n", 2        ),
            new Material(  "feather",        2,  MaterialType.feather,         45, "&+Yfeather&n", 1       ),  //  50
            new Material(  "wax",            2,  MaterialType.wax,             38, "&+wwax&n", 2           ),
            new Material(  "pearl",          4,  MaterialType.pearl,          650, "&+Wpearl&n", 4         ),
            new Material(  "silk",           2,  MaterialType.silk,           185, "&+wsilk&n", 1          ),
            new Material(  "spider silk",    3,  MaterialType.spidersilk,     235, "&+wspider silk&n", 1   ),
            new Material(  "jade",           4,  MaterialType.jade,           920, "&+Gjade&n", 4          ),  //  55
            new Material(  "lead",           4,  MaterialType.lead,            60, "&+wlead&n", 4          ),
        };
    }
}
