using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Equipment wear data.
    /// </summary>
    public class WearData
    {
        public Bitvector _wearFlag;
        public int _wearLocation;
        public int _wearLocation2;
        public int _wearLocation3;
        public string _wearMessage;
        public string _wearMessage2;
        public int _bodyPartNeeded;
        public int _racesNotAllowed;

        public WearData( Bitvector wearflag, int wearloc1, int wearloc2, int wearloc3, string wearmsg1, string wearmsg2, int partneeded, int racenotallowed )
        {
            _wearFlag = wearflag;
            _wearLocation = wearloc1;
            _wearLocation2 = wearloc2;
            _wearLocation3 = wearloc3;
            _wearMessage = wearmsg1;
            _wearMessage2 = wearmsg2;
            _bodyPartNeeded = partneeded;
            _racesNotAllowed = racenotallowed;
        }

        public static WearData[] Table = 
        {
            new WearData( ObjTemplate.WEARABLE_CARRY, 0, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_FINGER, 2, 1, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_NECK, 3, 4, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_BODY, 5, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_HEAD, 6, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_LEGS, 7, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_FEET, 8, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_HANDS, 9, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_ARMS, 10, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_SHIELD, 16, 18, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_ABOUT, 12, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_WAIST, 13, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_WRIST, 15, 14, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_WIELD, 16, 18, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_HOLD, 16, 18, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_EYES, 19, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_FACE, 20, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_EAR, 22, 21, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_QUIVER, 24, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_BADGE, 23, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_ONBACK, 24, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_ATTACH_BELT, 25, 26, 27, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_HORSE_BODY, 31, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_TAIL, 30, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_NOSE, 33, 0, 0, null, null, 0, -1 ),
            new WearData( ObjTemplate.WEARABLE_HORNS, 32, 0, 0, null, null, 0, -1 )
        };
    }
}
