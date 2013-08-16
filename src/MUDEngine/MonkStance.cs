namespace MUDEngine
{
    /// <summary>
    /// A fighting stance for monk and martial arts classes.
    /// </summary>
    public class MonkStance
    {
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="stancename"></param>
        /// <param name="hitmod"></param>
        /// <param name="dammod"></param>
        /// <param name="acmod"></param>
        /// <param name="basedmg"></param>
        /// <param name="dblatk"></param>
        /// <param name="hitpls"></param>
        /// <param name="dampls"></param>
        public MonkStance(string stancename, int hitmod, int dammod, int acmod, int basedmg, int dblatk, int[] hitpls, int[] dampls)
        {
            Name = stancename;
            HitrollModifier = hitmod;
            DamrollModifier = dammod;
            ArmorModifier = acmod;
            BaseDamage = basedmg;
            DoubleAttack = dblatk;
            HitPlus = hitpls;
            DamPlus = dampls;
        }

        public string Name { get; set; }
        public int HitrollModifier { get; set; }
        public int DamrollModifier { get; set; }
        public int ArmorModifier { get; set; }
        public int BaseDamage { get; set; }
        public int DoubleAttack { get; set; }
        private int[] _hitPlus = new int[5];
        private int[] _damPlus = new int[5];

        public int[] HitPlus
        {
            get { return _hitPlus; }
            set { _hitPlus = value; }
        }

        public int[] DamPlus
        {
            get { return _damPlus; }
            set { _damPlus = value; }
        }

        /// <summary>
        /// These stances correspond to the monk skill numbers.
        /// </summary>
        public enum Type
        {
            none = 0,
            bear,
            cat,
            cobra,
            crane,
            dragon,
            dragonfly,
            hawk,
            leopard,
            mantis,
            monkey,
            snake,
            tiger
        }

        public static MonkStance GetMonkStance(string stanceName)
        {
            switch (stanceName)
            {
                case "Bear Stance":
                    return Table[1];
                case "Cat Stance":
                    return Table[2];
                case "Cobra Stance":
                    return Table[3];
                case "Crane Stance":
                    return Table[4];
                case "Dragon Stance":
                    return Table[5];
                case "Dragonfly Stance":
                    return Table[6];
                case "Hawk Stance":
                    return Table[7];
                case "Leopard Stance":
                    return Table[8];
                case "Mantis Stance":
                    return Table[9];
                case "Monkey Stance":
                    return Table[10];
                case "Snake Stance":
                    return Table[11];
                case "Tiger Stance":
                    return Table[12];
                default:
                    return Table[0];
            }
        }

        static public MonkStance[] Table = 
        {
            new MonkStance(
                "none",
                0,
                0,
                100,
                2,
                0,
                new int[] { 61, 61, 61, 61, 61 },
                new int[] { 61, 61, 61, 61, 61 }
            ),
            new MonkStance(
                "bear",
                1,
                2,
                80,
                6,
                30,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            ),
            new MonkStance(
                "cat",
                1,
                0,
                60,
                4,
                60,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 31, 41, 51, 61 }
            ),
            new MonkStance(
                "cobra",
                2,
                2,
                80,
                8,
                0,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            ),
            new MonkStance(
                "crane",
                2,
                1,
                50,
                6,
                0,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            ),
            new MonkStance(
                "dragon",
                0,
                2,
                80,
                8,
                0,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            ),
            new MonkStance(
                "dragonfly",
                0,
                0,
                50,
                4,
                0,
                new int[] { 11, 31, 51, 61, 61 },
                new int[] { 11, 31, 51, 61, 61 }
            ),
            new MonkStance(
                "hawk",
                2,
                2,
                80,
                6,
                60,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            ),
            new MonkStance(
                "leopard",
                2,
                1,
                70,
                6,
                30,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            ),
            new MonkStance(
                "mantis",
                2,
                1,
                70,
                6,
                30,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            ),
            new MonkStance(
                "monkey",
                1,
                1,
                60,
                6,
                30,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            ),
            new MonkStance(
                "snake",
                1,
                1,
                60,
                4,
                30,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            ),
            new MonkStance(
                "tiger",
                1,
                2,
                80,
                8,
                30,
                new int[] { 11, 21, 31, 41, 51 },
                new int[] { 11, 21, 31, 41, 51 }
            )
        };

    }
}
