namespace MUDEngine
{
    /// <summary>
    /// Bonuses/penalties conferred by constitution score.
    /// </summary>
    public class ConModifier
    {
        public int Hitpoints { get; set; }
        public int Shock { get; set; }

        public ConModifier(int hp, int shk)
        {
            Hitpoints = hp;
            Shock = shk;
        }

        public static ConModifier[] Table = new ConModifier[]		
        {
            new ConModifier( 35, 20 ),   //  0 
            new ConModifier(  40, 25 ),   //  1 
            new ConModifier(  42, 30 ),
            new ConModifier(  44, 35 ),	  //  3 
            new ConModifier(  46, 40 ),
            new ConModifier(  48, 45 ),   //  5 
            new ConModifier(  50, 50 ),
            new ConModifier(  52, 55 ),
            new ConModifier(  54, 60 ),
            new ConModifier(  56, 65 ),
            new ConModifier(  58, 70 ),   // 10 
            new ConModifier(  60, 70 ),
            new ConModifier(  62, 70 ),
            new ConModifier(  64, 70 ),
            new ConModifier(  66, 70 ),
            new ConModifier(  68, 70 ),   // 15 
            new ConModifier(  70, 70 ),
            new ConModifier(  71, 70 ),
            new ConModifier(  72, 70 ),
            new ConModifier(  73, 70 ),
            new ConModifier(  74, 70 ),   // 20 
            new ConModifier(  75, 70 ),
            new ConModifier(  76, 70 ),
            new ConModifier(  77, 70 ),
            new ConModifier(  78, 70 ),
            new ConModifier(  79, 70 ),   // 25 
            new ConModifier(  80, 70 ),
            new ConModifier(  81, 70 ),
            new ConModifier(  82, 70 ),
            new ConModifier(  83, 70 ),
            new ConModifier(  84, 70 ),   // 30 
            new ConModifier(  85, 70 ),
            new ConModifier(  86, 70 ),
            new ConModifier(  87, 70 ),
            new ConModifier(  88, 70 ),
            new ConModifier(  89, 70 ),   // 35 
            new ConModifier(  90, 70 ),
            new ConModifier(  91, 70 ),
            new ConModifier(  92, 70 ),
            new ConModifier(  93, 70 ),
            new ConModifier(  94, 70 ),   // 40 
            new ConModifier(  95, 70 ),
            new ConModifier(  96, 70 ),
            new ConModifier(  96, 70 ),
            new ConModifier(  97, 70 ),
            new ConModifier(  97, 70 ),   // 45 
            new ConModifier(  98, 70 ),
            new ConModifier(  98, 70 ),
            new ConModifier(  99, 70 ),
            new ConModifier(  99, 70 ),
            new ConModifier(  100, 70 ),   // 50 
            new ConModifier(  100, 70 ),
            new ConModifier(  100, 70 ),
            new ConModifier(  100, 70 ),
            new ConModifier(  100, 70 ),
            new ConModifier(  100, 70 ),   // 55 
            new ConModifier(  100, 70 ),
            new ConModifier(  100, 70 ),
            new ConModifier(  100, 70 ),
            new ConModifier(  100, 70 ),
            new ConModifier(  101, 70 ),   // 60 
            new ConModifier(  101, 70 ),
            new ConModifier(  101, 70 ),
            new ConModifier(  102, 70 ),
            new ConModifier(  102, 70 ),
            new ConModifier(  102, 70 ),   // 65 
            new ConModifier(  103, 70 ),
            new ConModifier(  103, 70 ),
            new ConModifier(  103, 70 ),
            new ConModifier(  104, 70 ),
            new ConModifier(  104, 70 ),   // 70 
            new ConModifier(  104, 70 ),
            new ConModifier(  105, 70 ),
            new ConModifier(  105, 70 ),
            new ConModifier(  105, 70 ),
            new ConModifier(  106, 70 ),   // 75 
            new ConModifier(  106, 70 ),
            new ConModifier(  106, 70 ),
            new ConModifier(  107, 70 ),
            new ConModifier(  107, 70 ),
            new ConModifier(  107, 70 ),   // 80 
            new ConModifier(  108, 70 ),
            new ConModifier(  108, 70 ),
            new ConModifier(  109, 70 ),
            new ConModifier(  109, 70 ),
            new ConModifier(  110, 70 ),   // 85 
            new ConModifier(  110, 70 ),
            new ConModifier(  111, 70 ),
            new ConModifier(  111, 70 ),
            new ConModifier(  112, 70 ),
            new ConModifier(  112, 70 ),   // 90 
            new ConModifier(  113, 70 ),
            new ConModifier(  113, 70 ),
            new ConModifier(  114, 70 ),
            new ConModifier(  114, 70 ),
            new ConModifier(  115, 70 ),   // 95 
            new ConModifier(  115, 70 ),
            new ConModifier(  116, 70 ),
            new ConModifier(  116, 70 ),
            new ConModifier(  117, 70 ),
            new ConModifier(  117, 70 ),   // 100 
            new ConModifier(  118, 70 ),
            new ConModifier(  118, 70 ),
            new ConModifier(  119, 70 ),
            new ConModifier(  119, 70 ),
            new ConModifier(  120, 70 ),   // 105 
            new ConModifier(  120, 70 ),
            new ConModifier(  121, 70 ),
            new ConModifier(  121, 70 ),
            new ConModifier(  122, 70 ),
            new ConModifier(  122, 70 ),   // 110 
            new ConModifier(  123, 70 ),
            new ConModifier(  123, 70 ),
            new ConModifier(  124, 70 ),
            new ConModifier(  124, 70 ),
            new ConModifier(  125, 70 ),   // 115 
            new ConModifier(  125, 70 ),
            new ConModifier(  126, 70 ),
            new ConModifier(  126, 70 ),
            new ConModifier(  127, 70 ),
            new ConModifier(  127, 70 ),   // 120 
            new ConModifier(  128, 70 ),
            new ConModifier(  128, 70 ),
            new ConModifier(  128, 70 ),
            new ConModifier(  129, 70 ),
            new ConModifier(  129, 70 ),   // 125 
            new ConModifier(  129, 70 ),
            new ConModifier(  130, 70 ),
            new ConModifier(  130, 70 ),
            new ConModifier(  130, 70 ),
            new ConModifier(  131, 70 ),   // 130 
            new ConModifier(  131, 70 ),
            new ConModifier(  131, 70 ),
            new ConModifier(  132, 70 ),
            new ConModifier(  132, 70 ),
            new ConModifier(  132, 70 ),   // 135 
            new ConModifier(  133, 70 ),
            new ConModifier(  133, 70 ),
            new ConModifier(  133, 70 ),
            new ConModifier(  134, 70 ),
            new ConModifier(  134, 70 ),   // 140 
            new ConModifier(  134, 70 ),
            new ConModifier(  135, 70 ),
            new ConModifier(  135, 70 ),
            new ConModifier(  135, 70 ),
            new ConModifier(  136, 70 ),   // 145 
            new ConModifier(  136, 70 ),
            new ConModifier(  136, 70 ),
            new ConModifier(  137, 70 ),
            new ConModifier(  137, 70 ),
            new ConModifier(  137, 70 ),   // 150 
            new ConModifier(  138, 70 ),
            new ConModifier(  138, 70 ),
            new ConModifier(  138, 70 ),
            new ConModifier(  139, 70 ),
            new ConModifier(  139, 70 ),   // 155 
            new ConModifier(  139, 70 ),
            new ConModifier(  140, 70 ),
            new ConModifier(  140, 70 ),
            new ConModifier(  140, 70 ),
            new ConModifier(  141, 70 ),   // 160 
            new ConModifier(  141, 70 ),
            new ConModifier(  141, 70 ),
            new ConModifier(  142, 70 ),
            new ConModifier(  142, 70 ),
            new ConModifier(  142, 70 ),   // 165 
            new ConModifier(  143, 70 ),
            new ConModifier(  143, 70 ),
            new ConModifier(  143, 70 ),
            new ConModifier(  144, 70 ),
            new ConModifier(  144, 70 ),   // 170 
            new ConModifier(  144, 70 ),
            new ConModifier(  145, 70 ),
            new ConModifier(  145, 70 ),
            new ConModifier(  145, 70 ),
            new ConModifier(  146, 70 ),   // 175 
            new ConModifier(  146, 70 ),
            new ConModifier(  146, 70 ),
            new ConModifier(  147, 70 ),
            new ConModifier(  147, 70 ),
            new ConModifier(  147, 70 ),   // 180 
            new ConModifier(  148, 70 ),
            new ConModifier(  148, 70 ),
            new ConModifier(  148, 70 ),
            new ConModifier(  149, 70 ),
            new ConModifier(  149, 70 ),   // 185 
            new ConModifier(  149, 70 ),
            new ConModifier(  150, 70 ),
            new ConModifier(  150, 70 ),
            new ConModifier(  150, 70 ),
            new ConModifier(  151, 70 ),   // 190 
            new ConModifier(  151, 70 ),
            new ConModifier(  151, 70 ),
            new ConModifier(  152, 70 ),
            new ConModifier(  152, 70 ),
            new ConModifier(  152, 70 ),   // 195 
            new ConModifier(  153, 70 ),
            new ConModifier(  153, 70 ),
            new ConModifier(  153, 70 ),
            new ConModifier(  154, 70 ),
            new ConModifier(  154, 70 ),   // 200 
            new ConModifier(  154, 70 ),
            new ConModifier(  155, 70 ),
            new ConModifier(  155, 70 ),
            new ConModifier(  155, 70 ),
            new ConModifier(  156, 70 ),   // 205 
            new ConModifier(  156, 70 ),
            new ConModifier(  156, 70 ),
            new ConModifier(  157, 70 ),
            new ConModifier(  157, 70 ),
            new ConModifier(  157, 70 ),   // 210 
            new ConModifier(  158, 70 ),
            new ConModifier(  158, 70 ),
            new ConModifier(  158, 70 ),
            new ConModifier(  159, 70 ),
            new ConModifier(  159, 70 ),   // 215 
            new ConModifier(  160, 70 ),
            new ConModifier(  160, 70 ),
            new ConModifier(  160, 70 ),
            new ConModifier(  161, 70 ),
            new ConModifier(  161, 70 ),   // 220 
            new ConModifier(  161, 70 ),
            new ConModifier(  162, 70 ),
            new ConModifier(  162, 70 ),
            new ConModifier(  162, 70 ),
            new ConModifier(  163, 70 ),   // 225 
            new ConModifier(  163, 70 ),   // 226 
            new ConModifier(  163, 70 ),
            new ConModifier(  164, 70 ),
            new ConModifier(  164, 70 ),
            new ConModifier(  164, 70 ),
            new ConModifier(  165, 70 ),   // 231 
            new ConModifier(  165, 70 ),
            new ConModifier(  165, 70 ),
            new ConModifier(  166, 70 ),
            new ConModifier(  166, 70 ),
            new ConModifier(  166, 75 ),
            new ConModifier(  167, 80 ),
            new ConModifier(  167, 85 ),
            new ConModifier(  167, 88 ),
            new ConModifier(  168, 90 ),   // 240 
            new ConModifier(  169, 95 ),
            new ConModifier(  170, 97 ),
            new ConModifier(  171, 99 ),   // 243 
            new ConModifier(  172, 99 ),
            new ConModifier(  173, 99 ),   // 245 
            new ConModifier(  174, 99 ),
            new ConModifier(  175, 99 ),
            new ConModifier(  166, 99 ),
            new ConModifier(  167, 99 ),
            new ConModifier(  168, 99 )    // 250 
        };
    }
}
