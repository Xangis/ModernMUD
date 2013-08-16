namespace MUDEngine
{
    /// <summary>
    /// Bonuses/penalties conferred by strength score.
    /// </summary>
    public class StrengthModifier
    {
        public int HitModifier { get; set; }
        public int DamageModifier { get; set; }
        public int CarryWeight { get; set; }
        public int WieldWeight { get; set; }

        public StrengthModifier(int hit, int dam, int car, int wie)
        {
            HitModifier = hit;
            DamageModifier = dam;
            CarryWeight = car;
            WieldWeight = wie;
        }

        /// <summary>
        /// Strength modifier table
        /// </summary>
        public static StrengthModifier[] Table = new[]		
        {
            new StrengthModifier( -5, -4,   0,  1 ),   // 0  
            new StrengthModifier( -5, -4,   2,  1 ),   // 1  
            new StrengthModifier( -3, -2,   3,  1 ),
            new StrengthModifier( -3, -1,   4,  1 ),   // 3  
            new StrengthModifier( -2, -1,   5,  2 ),
            new StrengthModifier( -2, -1,   7,  2 ),   // 5  
            new StrengthModifier( -1,  0,  10,  2 ),
            new StrengthModifier( -1,  0,  13,  2 ),
            new StrengthModifier(  0,  0,  16,  3 ),
            new StrengthModifier(  0,  0,  20,  3 ),
            new StrengthModifier(  0,  0,  25,  3 ),  // 10 
            new StrengthModifier(  0,  0,  30,  3 ),
            new StrengthModifier(  0,  0,  35,  4 ),  // 12 
            new StrengthModifier(  0,  0,  40,  4 ),
            new StrengthModifier(  0,  0,  45,  4 ),
            new StrengthModifier(  0,  0,  50,  4 ),
            new StrengthModifier(  0,  0,  50,  5 ),
            new StrengthModifier(  0,  0,  50,  5 ),
            new StrengthModifier(  0,  0,  55,  5 ),
            new StrengthModifier(  0,  0,  55,  6 ),
            new StrengthModifier(  0,  0,  55,  6 ),  // 20 
            new StrengthModifier(  0,  0,  60,  6 ),
            new StrengthModifier(  0,  0,  60,  7 ),
            new StrengthModifier(  0,  0,  60,  7 ),
            new StrengthModifier(  0,  0,  65,  7 ),
            new StrengthModifier(  0,  0,  65,  8 ),
            new StrengthModifier(  0,  0,  65,  8 ),
            new StrengthModifier(  0,  0,  70,  8 ),
            new StrengthModifier(  0,  0,  70,  9 ),
            new StrengthModifier(  0,  0,  70,  9 ),
            new StrengthModifier(  0,  0,  75,  9 ),  // 30 
            new StrengthModifier(  0,  0,  75, 10 ),
            new StrengthModifier(  0,  0,  75, 10 ),
            new StrengthModifier(  0,  0,  80, 10 ),
            new StrengthModifier(  0,  0,  80, 11 ),
            new StrengthModifier(  0,  0,  80, 11 ),
            new StrengthModifier(  0,  0,  85, 11 ),
            new StrengthModifier(  0,  0,  85, 12 ),
            new StrengthModifier(  0,  0,  85, 12 ),
            new StrengthModifier(  0,  0,  90, 12 ),
            new StrengthModifier(  0,  0,  90, 13 ),  // 40 
            new StrengthModifier(  0,  0,  90, 13 ),
            new StrengthModifier(  0,  0,  95, 13 ),
            new StrengthModifier(  0,  0,  95, 14 ),
            new StrengthModifier(  0,  0, 100, 14 ),
            new StrengthModifier(  0,  0, 100, 14 ),
            new StrengthModifier(  0,  0, 105, 15 ),
            new StrengthModifier(  0,  0, 105, 15 ),
            new StrengthModifier(  0,  0, 110, 15 ),
            new StrengthModifier(  0,  0, 110, 16 ),
            new StrengthModifier(  0,  0, 115, 16 ),  // 50 
            new StrengthModifier(  0,  0, 115, 16 ),
            new StrengthModifier(  0,  0, 120, 17 ),
            new StrengthModifier(  0,  0, 120, 17 ),
            new StrengthModifier(  0,  0, 125, 17 ),
            new StrengthModifier(  0,  0, 125, 18 ),
            new StrengthModifier(  0,  0, 130, 18 ),
            new StrengthModifier(  0,  0, 130, 18 ),
            new StrengthModifier(  0,  0, 135, 19 ),
            new StrengthModifier(  0,  0, 135, 19 ),
            new StrengthModifier(  0,  0, 140, 19 ),  // 60 
            new StrengthModifier(  0,  0, 140, 20 ),
            new StrengthModifier(  0,  0, 145, 20 ),
            new StrengthModifier(  0,  0, 145, 20 ),
            new StrengthModifier(  0,  0, 150, 21 ),
            new StrengthModifier(  0,  0, 150, 21 ),
            new StrengthModifier(  0,  0, 155, 21 ),
            new StrengthModifier(  0,  0, 155, 22 ),
            new StrengthModifier(  0,  0, 160, 22 ),
            new StrengthModifier(  0,  0, 160, 22 ),
            new StrengthModifier(  0,  0, 165, 23 ),  // 70 
            new StrengthModifier(  0,  0, 165, 23 ),
            new StrengthModifier(  0,  0, 170, 23 ),
            new StrengthModifier(  0,  0, 170, 24 ),
            new StrengthModifier(  0,  0, 175, 24 ),
            new StrengthModifier(  0,  0, 175, 24 ),
            new StrengthModifier(  0,  0, 180, 25 ),
            new StrengthModifier(  0,  0, 185, 25 ),
            new StrengthModifier(  0,  0, 190, 25 ),
            new StrengthModifier(  0,  0, 195, 26 ),
            new StrengthModifier(  0,  1, 200, 26 ),  // 80 
            new StrengthModifier(  0,  1, 205, 26 ),
            new StrengthModifier(  0,  1, 210, 27 ),
            new StrengthModifier(  0,  1, 215, 27 ),
            new StrengthModifier(  0,  1, 220, 27 ),
            new StrengthModifier(  0,  1, 225, 28 ),
            new StrengthModifier(  0,  1, 230, 28 ),
            new StrengthModifier(  1,  1, 235, 28 ),
            new StrengthModifier(  1,  1, 240, 29 ),
            new StrengthModifier(  1,  1, 245, 29 ),
            new StrengthModifier(  1,  1, 250, 29 ),  // 90 
            new StrengthModifier(  1,  1, 260, 30 ),
            new StrengthModifier(  1,  1, 270, 30 ),
            new StrengthModifier(  1,  2, 280, 30 ),
            new StrengthModifier(  1,  2, 290, 31 ),
            new StrengthModifier(  1,  2, 300, 31 ),
            new StrengthModifier(  1,  2, 310, 31 ),
            new StrengthModifier(  2,  2, 320, 32 ),
            new StrengthModifier(  2,  2, 330, 32 ),
            new StrengthModifier(  2,  2, 340, 32 ),
            new StrengthModifier(  2,  3, 350, 33 ),  // 100 
            new StrengthModifier(  2,  3, 360, 33 ),
            new StrengthModifier(  2,  3, 370, 33 ),
            new StrengthModifier(  2,  3, 380, 34 ),
            new StrengthModifier(  2,  3, 390, 34 ),
            new StrengthModifier(  2,  3, 400, 34 ),
            new StrengthModifier(  2,  3, 410, 35 ),
            new StrengthModifier(  2,  3, 420, 35 ),
            new StrengthModifier(  2,  3, 430, 35 ),
            new StrengthModifier(  2,  3, 440, 36 ),
            new StrengthModifier(  2,  4, 450, 36 ),  // 110 
            new StrengthModifier(  2,  4, 460, 36 ),
            new StrengthModifier(  2,  4, 470, 37 ),
            new StrengthModifier(  2,  4, 480, 37 ),
            new StrengthModifier(  2,  4, 490, 37 ),
            new StrengthModifier(  2,  4, 500, 38 ),
            new StrengthModifier(  2,  4, 510, 38 ),
            new StrengthModifier(  2,  4, 520, 38 ),
            new StrengthModifier(  2,  4, 530, 39 ),
            new StrengthModifier(  2,  4, 540, 39 ),
            new StrengthModifier(  3,  4, 550, 39 ),  // 120 
            new StrengthModifier(  3,  4, 560, 40 ),
            new StrengthModifier(  3,  4, 570, 40 ),
            new StrengthModifier(  3,  4, 580, 40 ),
            new StrengthModifier(  3,  4, 590, 41 ),
            new StrengthModifier(  3,  4, 600, 41 ),
            new StrengthModifier(  3,  4, 610, 41 ),
            new StrengthModifier(  3,  4, 620, 42 ),
            new StrengthModifier(  3,  4, 630, 42 ),
            new StrengthModifier(  3,  4, 640, 42 ),
            new StrengthModifier(  3,  5, 650, 43 ),  // 130 
            new StrengthModifier(  3,  5, 660, 43 ),
            new StrengthModifier(  3,  5, 670, 43 ),
            new StrengthModifier(  3,  5, 680, 44 ),
            new StrengthModifier(  3,  5, 690, 44 ),
            new StrengthModifier(  3,  5, 700, 44 ),
            new StrengthModifier(  3,  5, 710, 44 ),
            new StrengthModifier(  3,  5, 720, 45 ),
            new StrengthModifier(  3,  5, 730, 45 ),
            new StrengthModifier(  3,  5, 740, 45 ),
            new StrengthModifier(  3,  6, 750, 45 ),  // 140 
            new StrengthModifier(  3,  6, 760, 46 ),
            new StrengthModifier(  3,  6, 770, 46 ),
            new StrengthModifier(  3,  6, 780, 46 ),
            new StrengthModifier(  3,  6, 790, 46 ),
            new StrengthModifier(  3,  6, 800, 47 ),
            new StrengthModifier(  3,  6, 810, 47 ),
            new StrengthModifier(  3,  6, 820, 47 ),
            new StrengthModifier(  3,  6, 830, 47 ),
            new StrengthModifier(  3,  6, 840, 48 ),
            new StrengthModifier(  3,  6, 850, 48 ),  // 150 
            new StrengthModifier(  3,  7, 860, 48 ),
            new StrengthModifier(  3,  7, 870, 48 ),
            new StrengthModifier(  3,  7, 880, 49 ),
            new StrengthModifier(  3,  7, 890, 49 ),
            new StrengthModifier(  3,  7, 900, 49 ),
            new StrengthModifier(  3,  7, 910, 49 ),
            new StrengthModifier(  3,  7, 920, 50 ),
            new StrengthModifier(  3,  7, 930, 50 ),
            new StrengthModifier(  3,  7, 940, 50 ),
            new StrengthModifier(  4,  7, 950, 50 ),  // 160 
            new StrengthModifier(  4,  7, 960, 51 ),
            new StrengthModifier(  4,  7, 970, 51 ),
            new StrengthModifier(  4,  7, 980, 51 ),
            new StrengthModifier(  4,  7, 990, 51 ),
            new StrengthModifier(  4,  7, 1000, 52 ),
            new StrengthModifier(  4,  7, 1010, 52 ),
            new StrengthModifier(  4,  7, 1020, 52 ),
            new StrengthModifier(  4,  7, 1030, 52 ),
            new StrengthModifier(  4,  7, 1040, 53 ),
            new StrengthModifier(  4,  8, 1050, 53 ),  // 170 
            new StrengthModifier(  4,  8, 1060, 53 ),
            new StrengthModifier(  4,  8, 1070, 53 ),
            new StrengthModifier(  4,  8, 1080, 54 ),
            new StrengthModifier(  4,  8, 1090, 54 ),
            new StrengthModifier(  4,  8, 1100, 54 ),
            new StrengthModifier(  4,  8, 1110, 54 ),
            new StrengthModifier(  4,  8, 1120, 55 ),
            new StrengthModifier(  4,  8, 1130, 55 ),
            new StrengthModifier(  4,  8, 1140, 55 ),
            new StrengthModifier(  4,  9, 1140, 55 ),  // 180 
            new StrengthModifier(  4,  9, 1140, 56 ),
            new StrengthModifier(  4,  9, 1140, 56 ),
            new StrengthModifier(  4,  9, 1140, 56 ),
            new StrengthModifier(  4,  9, 1140, 56 ),
            new StrengthModifier(  4,  9, 1140, 57 ),
            new StrengthModifier(  4,  9, 1140, 57 ),
            new StrengthModifier(  4,  9, 1140, 57 ),
            new StrengthModifier(  4,  9, 1140, 57 ),
            new StrengthModifier(  4,  9, 1140, 58 ),
            new StrengthModifier(  5,  9, 1140, 58 ),  // 190 
            new StrengthModifier(  5,  9, 1140, 58 ),
            new StrengthModifier(  5,  9, 1140, 58 ),
            new StrengthModifier(  5,  9, 1140, 59 ),
            new StrengthModifier(  5,  9, 1140, 59 ),
            new StrengthModifier(  5,  9, 1140, 59 ),
            new StrengthModifier(  5,  9, 11140, 59 ),
            new StrengthModifier(  5,  9, 1140, 60 ),
            new StrengthModifier(  5,  9, 1140, 60 ),
            new StrengthModifier(  5,  9, 1140, 60 ),
            new StrengthModifier(  5,  10, 1400, 60 ),  // 200 
            new StrengthModifier(  5,  10, 1400, 61 ),
            new StrengthModifier(  5,  10, 1400, 61 ),
            new StrengthModifier(  5,  10, 1400, 61 ),
            new StrengthModifier(  5,  10, 1400, 61 ),
            new StrengthModifier(  5,  10, 1400, 102 ),
            new StrengthModifier(  5,  10, 1400, 102 ),
            new StrengthModifier(  5,  10, 1400, 102 ),
            new StrengthModifier(  5,  10, 1400, 102 ),
            new StrengthModifier(  5,  10, 1400, 102 ),
            new StrengthModifier(  5,  11, 1400, 102 ),  // 210 
            new StrengthModifier(  5,  11, 1400, 110 ),
            new StrengthModifier(  5,  11, 1400, 110 ),
            new StrengthModifier(  5,  11, 1400, 110 ),
            new StrengthModifier(  5,  11, 1400, 110 ),
            new StrengthModifier(  5,  11, 1400, 112 ),
            new StrengthModifier(  5,  11, 1400, 112 ),
            new StrengthModifier(  5,  11, 1400, 112 ),
            new StrengthModifier(  5,  11, 1400, 112 ),
            new StrengthModifier(  5,  11, 1400, 112 ),
            new StrengthModifier(  5,  11, 1400, 112 ),  // 220 
            new StrengthModifier(  5,  11, 1400, 112 ),
            new StrengthModifier(  6,  11, 1400, 112 ),
            new StrengthModifier(  6,  11, 1400, 112 ),
            new StrengthModifier(  6,  11, 1400, 112 ),
            new StrengthModifier(  6,  11, 1400, 112 ),
            new StrengthModifier(  6,  11, 1400, 112 ),
            new StrengthModifier(  6,  11, 1400, 112 ),
            new StrengthModifier(  6,  11, 1400, 112 ),
            new StrengthModifier(  6,  11, 1400, 112 ),
            new StrengthModifier(  6,  11, 1400, 112 ),  // 230 
            new StrengthModifier(  6,  12, 1400, 112 ),
            new StrengthModifier(  6,  12, 1400, 112 ),
            new StrengthModifier(  6,  12, 1400, 112 ),
            new StrengthModifier(  6,  12, 1400, 112 ),
            new StrengthModifier(  7,  12, 1400, 112 ),
            new StrengthModifier(  7,  12, 1400, 112 ),
            new StrengthModifier(  7,  12, 1400, 112 ),
            new StrengthModifier(  7,  12, 1400, 112 ),
            new StrengthModifier(  7,  12, 1400, 113 ),
            new StrengthModifier(  7,  13, 1700, 114 ),  // 240 
            new StrengthModifier(  7,  13, 1700, 115 ),
            new StrengthModifier(  7,  13, 1950, 116 ),
            new StrengthModifier(  7,  13, 2200, 122 ),
            new StrengthModifier(  7,  13, 2500, 125 ),
            new StrengthModifier(  8,  13, 4000, 130 ),
            new StrengthModifier(  8,  13, 5000, 135 ),
            new StrengthModifier(  8,  13, 6000, 140 ),
            new StrengthModifier(  8,  14, 7000, 145 ),
            new StrengthModifier(  8,  15, 8000, 150 ),
            new StrengthModifier(  9,  16, 8000, 150 ),  // 250 
        };
    };

}
