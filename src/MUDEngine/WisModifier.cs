namespace MUDEngine
{
    /// <summary>
    /// Bonuses/penalties conferred wisdom score.
    /// </summary>
    public class WisModifier
    {
        public WisModifier(int prac)
        {
            PracticeModifier = prac;
        }

        /// <summary>
        /// Table of wisdom modifiers.
        /// </summary>
        public static WisModifier[] Table = new[]		
        {
            new WisModifier( 0 ),	//  0
            new WisModifier( 0 ),	//  1
            new WisModifier( 0 ),
            new WisModifier( 0 ),	//  3
            new WisModifier( 0 ),
            new WisModifier( 1 ),	//  5
            new WisModifier( 1 ),
            new WisModifier( 1 ),
            new WisModifier( 1 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 10
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 15
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 20
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 25
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 30
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 35
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 40
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 45
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 50
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 55
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 60
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 65
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 70
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 75
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 80
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 85
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 90
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 95
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 100
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 105
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 110
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 115
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 120
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 125
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 130
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 135
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 140
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 145
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 150
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 155
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 160
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 165
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 170
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 175
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 180
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 185
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 190
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 195
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 200
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 205
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 210
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 215
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 220
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 225
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 230
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),	// 235
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 2 ),
            new WisModifier( 3 ),	// 240
            new WisModifier( 3 ),
            new WisModifier( 4 ),
            new WisModifier( 4 ),	// 243
            new WisModifier( 5 ),
            new WisModifier( 5 ),	// 245
            new WisModifier( 6 ),
            new WisModifier( 7 ),
            new WisModifier( 7 ),
            new WisModifier( 7 ),
            new WisModifier( 8 )	// 250
        };

        public int PracticeModifier { get; set; }
    };
}
