namespace MUDEngine
{
    /// <summary>
    /// Bonuses/penalties conferred by agility score.
    /// </summary>
    public class AgiModifier
    {
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="def"></param>
        public AgiModifier(int def)
        {
            ACModifier = def;
        }

        /// <summary>
        /// Table of agility modifiers based on the score.
        /// </summary>
        public static AgiModifier[] Table = new[]
        {
            new AgiModifier(   60 ),   // 0 
            new AgiModifier(   57 ),   // 1 
            new AgiModifier(   54 ),
            new AgiModifier(   52 ),
            new AgiModifier(   50 ),
            new AgiModifier(   48 ),   // 5
            new AgiModifier(   46 ),
            new AgiModifier(   44 ),
            new AgiModifier(   42 ),
            new AgiModifier(   40 ),
            new AgiModifier(   38 ),   // 10
            new AgiModifier(   37 ),
            new AgiModifier(   36 ),
            new AgiModifier(   35 ),
            new AgiModifier(   34 ),
            new AgiModifier(   33 ),   // 15 */
            new AgiModifier(   32 ),
            new AgiModifier(   31 ),
            new AgiModifier(   30 ),
            new AgiModifier(   29 ),
            new AgiModifier(   28 ),   // 20
            new AgiModifier(   27 ),
            new AgiModifier(   26 ),
            new AgiModifier(   25 ),
            new AgiModifier(   24 ),
            new AgiModifier(   23 ),   // 25
            new AgiModifier(   22 ),
            new AgiModifier(   21 ),
            new AgiModifier(   20 ),
            new AgiModifier(   19 ),
            new AgiModifier(   18 ),   // 30
            new AgiModifier(   17 ),
            new AgiModifier(   16 ),
            new AgiModifier(   15 ),
            new AgiModifier(   14 ),
            new AgiModifier(   13 ),   // 35
            new AgiModifier(   12 ),
            new AgiModifier(   11 ),
            new AgiModifier(   10 ),
            new AgiModifier(    9 ),
            new AgiModifier(    8 ),   // 40
            new AgiModifier(    7 ),
            new AgiModifier(    6 ),
            new AgiModifier(    5 ),
            new AgiModifier(    4 ),
            new AgiModifier(    3 ),   // 45
            new AgiModifier(    2 ),
            new AgiModifier(    2 ),
            new AgiModifier(    1 ),
            new AgiModifier(    1 ),
            new AgiModifier(    0 ),   // 50
            new AgiModifier(    0 ),
            new AgiModifier(    0 ),
            new AgiModifier(    0 ),
            new AgiModifier(    0 ),
            new AgiModifier(    0 ),   // 55
            new AgiModifier(    0 ),
            new AgiModifier(    0 ),
            new AgiModifier(    0 ),
            new AgiModifier(    0 ),
            new AgiModifier( -  1 ),   // 60
            new AgiModifier( -  1 ),
            new AgiModifier( -  1 ),
            new AgiModifier( -  2 ),
            new AgiModifier( -  2 ),
            new AgiModifier( -  3 ),   // 65
            new AgiModifier( -  3 ),
            new AgiModifier( -  4 ),
            new AgiModifier( -  4 ),
            new AgiModifier( -  5 ),
            new AgiModifier( -  5 ),   // 70
            new AgiModifier( -  6 ),
            new AgiModifier( -  6 ),
            new AgiModifier( -  7 ),
            new AgiModifier( -  7 ),
            new AgiModifier( -  8 ),   // 75
            new AgiModifier( -  8 ),
            new AgiModifier( -  9 ),
            new AgiModifier( -  9 ),
            new AgiModifier( - 10 ),
            new AgiModifier( - 11 ),   // 80
            new AgiModifier( - 12 ),
            new AgiModifier( - 13 ),
            new AgiModifier( - 14 ),
            new AgiModifier( - 15 ),
            new AgiModifier( - 16 ),   // 85
            new AgiModifier( - 17 ),
            new AgiModifier( - 18 ),
            new AgiModifier( - 19 ),
            new AgiModifier( - 20 ),
            new AgiModifier( - 21 ),   // 90
            new AgiModifier( - 22 ),
            new AgiModifier( - 23 ),
            new AgiModifier( - 24 ),
            new AgiModifier( - 25 ),
            new AgiModifier( - 26 ),   // 95
            new AgiModifier( - 27 ),
            new AgiModifier( - 28 ),
            new AgiModifier( - 29 ),
            new AgiModifier( - 30 ),
            new AgiModifier( - 31 ),   // 100
            new AgiModifier( - 32 ),
            new AgiModifier( - 32 ),
            new AgiModifier( - 33 ),
            new AgiModifier( - 33 ),
            new AgiModifier( - 34 ),   // 105
            new AgiModifier( - 34 ),
            new AgiModifier( - 35 ),
            new AgiModifier( - 35 ),
            new AgiModifier( - 36 ),
            new AgiModifier( - 36 ),   // 110
            new AgiModifier( - 37 ),
            new AgiModifier( - 37 ),
            new AgiModifier( - 38 ),
            new AgiModifier( - 38 ),
            new AgiModifier( - 39 ),   // 115
            new AgiModifier( - 39 ),
            new AgiModifier( - 40 ),
            new AgiModifier( - 40 ),
            new AgiModifier( - 41 ),
            new AgiModifier( - 41 ),   // 120
            new AgiModifier( - 42 ),
            new AgiModifier( - 42 ),
            new AgiModifier( - 43 ),
            new AgiModifier( - 43 ),
            new AgiModifier( - 44 ),   // 125
            new AgiModifier( - 44 ),
            new AgiModifier( - 45 ),
            new AgiModifier( - 45 ),
            new AgiModifier( - 46 ),
            new AgiModifier( - 46 ),   // 130
            new AgiModifier( - 47 ),
            new AgiModifier( - 47 ),
            new AgiModifier( - 48 ),
            new AgiModifier( - 48 ),
            new AgiModifier( - 49 ),   // 135
            new AgiModifier( - 49 ),
            new AgiModifier( - 50 ),
            new AgiModifier( - 50 ),
            new AgiModifier( - 51 ),
            new AgiModifier( - 51 ),   // 140
            new AgiModifier( - 52 ),
            new AgiModifier( - 52 ),
            new AgiModifier( - 53 ),
            new AgiModifier( - 53 ),
            new AgiModifier( - 54 ),   // 145
            new AgiModifier( - 54 ),
            new AgiModifier( - 55 ),
            new AgiModifier( - 55 ),
            new AgiModifier( - 56 ),
            new AgiModifier( - 56 ),   // 150
            new AgiModifier( - 57 ),
            new AgiModifier( - 57 ),
            new AgiModifier( - 58 ),
            new AgiModifier( - 58 ),
            new AgiModifier( - 59 ),   // 155
            new AgiModifier( - 59 ),
            new AgiModifier( - 60 ),
            new AgiModifier( - 60 ),
            new AgiModifier( - 61 ),
            new AgiModifier( - 61 ),   // 160
            new AgiModifier( - 62 ),
            new AgiModifier( - 62 ),
            new AgiModifier( - 63 ),
            new AgiModifier( - 63 ),
            new AgiModifier( - 64 ),   // 165
            new AgiModifier( - 64 ),
            new AgiModifier( - 65 ),
            new AgiModifier( - 65 ),
            new AgiModifier( - 66 ),
            new AgiModifier( - 66 ),   // 170
            new AgiModifier( - 67 ),
            new AgiModifier( - 67 ),
            new AgiModifier( - 68 ),
            new AgiModifier( - 68 ),
            new AgiModifier( - 69 ),   // 175
            new AgiModifier( - 69 ),
            new AgiModifier( - 70 ),
            new AgiModifier( - 70 ),
            new AgiModifier( - 71 ),
            new AgiModifier( - 71 ),   // 180
            new AgiModifier( - 72 ),
            new AgiModifier( - 72 ),
            new AgiModifier( - 73 ),
            new AgiModifier( - 73 ),
            new AgiModifier( - 74 ),   // 185
            new AgiModifier( - 74 ),
            new AgiModifier( - 75 ),
            new AgiModifier( - 75 ),
            new AgiModifier( - 76 ),
            new AgiModifier( - 76 ),   // 190
            new AgiModifier( - 76 ),
            new AgiModifier( - 77 ),
            new AgiModifier( - 77 ),
            new AgiModifier( - 77 ),
            new AgiModifier( - 78 ),   // 195
            new AgiModifier( - 78 ),
            new AgiModifier( - 78 ),
            new AgiModifier( - 79 ),
            new AgiModifier( - 79 ),
            new AgiModifier( - 79 ),   // 200
            new AgiModifier( - 80 ),
            new AgiModifier( - 80 ),
            new AgiModifier( - 81 ),
            new AgiModifier( - 82 ),
            new AgiModifier( - 83 ),   // 205
            new AgiModifier( - 84 ),
            new AgiModifier( - 85 ),
            new AgiModifier( - 86 ),
            new AgiModifier( - 87 ),
            new AgiModifier( - 88 ),   // 210
            new AgiModifier( - 89 ),
            new AgiModifier( - 90 ),
            new AgiModifier( - 91 ),
            new AgiModifier( - 92 ),
            new AgiModifier( - 93 ),   // 215
            new AgiModifier( - 94 ),
            new AgiModifier( - 95 ),
            new AgiModifier( - 96 ),
            new AgiModifier( - 97 ),
            new AgiModifier( - 98 ),   // 220
            new AgiModifier( - 99 ),
            new AgiModifier( - 100 ),
            new AgiModifier( - 101 ),
            new AgiModifier( - 102 ),
            new AgiModifier( - 103 ),   // 225
            new AgiModifier( - 104 ),
            new AgiModifier( - 105 ),
            new AgiModifier( - 106 ),
            new AgiModifier( - 107 ),
            new AgiModifier( - 108 ),   // 230
            new AgiModifier( - 109 ),
            new AgiModifier( - 110 ),
            new AgiModifier( - 111 ),
            new AgiModifier( - 112 ),
            new AgiModifier( - 113 ),   // 235
            new AgiModifier( - 114 ),
            new AgiModifier( - 115 ),
            new AgiModifier( - 116 ),
            new AgiModifier( - 117 ),
            new AgiModifier( - 118 ),   // 240
            new AgiModifier( - 119 ),
            new AgiModifier( - 120 ),
            new AgiModifier( - 121 ),
            new AgiModifier( - 122 ),
            new AgiModifier( - 123 ),   // 245
            new AgiModifier( - 124 ),
            new AgiModifier( - 125 ),
            new AgiModifier( - 126 ),
            new AgiModifier( - 128 ),
            new AgiModifier( - 130 )    // 250
        };

        /// <summary>
        /// The armor class modifier.
        /// </summary>
        public int ACModifier { get; set; }
    }
}
