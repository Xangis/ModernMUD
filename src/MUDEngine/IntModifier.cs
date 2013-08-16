namespace MUDEngine
{
    /// <summary>
    /// Bonuses/penalties conferred by intelligence score.
    /// </summary>
    public class IntModifier
    {
        public int LearnModifier { get; set; }

        public IntModifier(int lrn)
        {
            LearnModifier = lrn;
        }

        public static IntModifier[] Table = new[]		
        {
            new IntModifier(  3 ),	//  0 
            new IntModifier(  5 ),	//  1 
            new IntModifier(  7 ),
            new IntModifier(  8 ),	//  3 
            new IntModifier(  9 ),
            new IntModifier( 10 ),	//  5 
            new IntModifier( 11 ),
            new IntModifier( 12 ),
            new IntModifier( 13 ),
            new IntModifier( 15 ),
            new IntModifier( 17 ),	// 10 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 15 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 20 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 25 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 30 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 35 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 40 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 45 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 50 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 55 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 60 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 65 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 70 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 75 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 80 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 85 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 90 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 95 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 100 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 105 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 110 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 115 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 120 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 125 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 130 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 135 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 140 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 145 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 150 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 155 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 160 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 165 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 170 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 175 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 180 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 185 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 190 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 195 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 200 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 205 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 210 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 215 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 220 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 225 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),	// 230 
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),
            new IntModifier( 17 ),     // 235 
            new IntModifier( 19 ),     // 236 
            new IntModifier( 22 ),
            new IntModifier( 25 ),
            new IntModifier( 28 ),
            new IntModifier( 31 ),	// 240 
            new IntModifier( 34 ),
            new IntModifier( 37 ),
            new IntModifier( 40 ),	// 243 
            new IntModifier( 44 ),
            new IntModifier( 49 ),	// 245 
            new IntModifier( 55 ),
            new IntModifier( 60 ),
            new IntModifier( 70 ),
            new IntModifier( 85 ),
            new IntModifier( 99 )	// 250 
        };
    };
}
