
namespace MUDEngine
{
    /// <summary>
    /// Experience point advancement table.
    /// </summary>
    public class ExperienceTable
    {
        public ExperienceTable( int mobexp, int lvlexp )
        {
            MobExperience = mobexp;
            LevelExperience = lvlexp;
        }

        /// <summary>
        /// The typical experience reward for killing a mob.
        /// </summary>
        public int MobExperience { get; set; }

        /// <summary>
        /// The experience required to advance to the next level.
        /// </summary>
        public int LevelExperience { get; set; }

        /// <summary>
        /// Experience table, used for determining level advancement.
        /// </summary>
        static public ExperienceTable[] Table = 
        {
            new ExperienceTable( 1, 20 ),     // 0
            new ExperienceTable( 10, 200 ),     // 1
            new ExperienceTable( 20, 420 ),     // 2
            new ExperienceTable( 30, 660 ),     // 3
            new ExperienceTable( 40, 900 ),     // 4
            new ExperienceTable( 50, 1150 ),     // 5
            new ExperienceTable( 60, 1500 ),     // 6
            new ExperienceTable( 75, 1950 ),     // 7
            new ExperienceTable( 95, 2500 ),    // 8
            new ExperienceTable( 120, 3100 ),   // 9
            new ExperienceTable( 135, 4000 ),   // 10
            new ExperienceTable( 160, 5000 ),   // 11
            new ExperienceTable( 180, 6200 ),   // 12
            new ExperienceTable( 210, 7600 ),   // 13
            new ExperienceTable( 280, 9200 ),   // 14
            new ExperienceTable( 340, 11000 ),   // 15
            new ExperienceTable( 400, 13000 ),   // 16
            new ExperienceTable( 450, 15200 ),   // 17
            new ExperienceTable( 500, 17400 ),   // 18
            new ExperienceTable( 550, 19800 ),   // 19
            new ExperienceTable( 625, 23400 ),   // 20
            new ExperienceTable( 675, 27200 ),  // 21 // For 21 through 26, the jumps are a little steeper (intentional)
            new ExperienceTable( 750, 31200 ),  // 22
            new ExperienceTable( 800, 35400 ),  // 23
            new ExperienceTable( 850, 39800 ),  // 24
            new ExperienceTable( 900, 44400 ),  // 25
            new ExperienceTable( 950, 48200 ),  // 26
            new ExperienceTable( 1000, 54200 ), // 27
            new ExperienceTable( 1075, 60400 ), // 28
            new ExperienceTable( 1150, 66800 ), // 29
            new ExperienceTable( 1225, 73400 ), // 30
            new ExperienceTable( 1300, 80200 ), // 31 // 31 has a steeper jump
            new ExperienceTable( 1375, 83200 ), // 32
            new ExperienceTable( 1500, 86400 ), // 33
            new ExperienceTable( 1550, 89800 ), // 34
            new ExperienceTable( 1600, 95400 ), // 35
            new ExperienceTable( 1650, 101200 ), // 36
            new ExperienceTable( 1725, 107200 ), // 37
            new ExperienceTable( 1775, 113400 ), // 38
            new ExperienceTable( 1900, 119800 ), // 39
            new ExperienceTable( 1950, 126400 ), // 40
            new ExperienceTable( 2025, 163200 ), // 41
            new ExperienceTable( 2150, 166600 ), // 42
            new ExperienceTable( 2250, 170100 ), // 43
            new ExperienceTable( 2550, 173700 ), // 44
            new ExperienceTable( 2900, 177400 ), // 45
            new ExperienceTable( 3250, 181200 ), // 46
            new ExperienceTable( 3600, 185100 ), // 47
            new ExperienceTable( 4050, 189100 ), // 48
            new ExperienceTable( 4400, 193200 ), // 49
            new ExperienceTable( 4750, 197400 ), // 50
            new ExperienceTable( 5100, 1105000 ), // 51
            new ExperienceTable( 5450, 1115000 ), // 52
            new ExperienceTable( 5800, 1130000 ), // 53
            new ExperienceTable( 6150, 1150000 ), // 54
            new ExperienceTable( 6500, 1175000 ), // 55
            new ExperienceTable( 6850, 1200000 ), // 56
            new ExperienceTable( 7200, 1250000 ), // 57
            new ExperienceTable( 7550, 1325000 ), // 58
            new ExperienceTable( 7900, 1425000 ), // 59
            new ExperienceTable( 8250, 1550000 ), // 60
            new ExperienceTable( 8600, 1700000 ), // 61
            new ExperienceTable( 8950, 11000000 ), // 62
            new ExperienceTable( 9300, 12000000 ), // 63
            new ExperienceTable( 9650, 13000000 ), // 64
            new ExperienceTable( 10000, 14000000 ) // 65
        };
    }
}
