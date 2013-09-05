using ModernMUD;
using System.Collections.Generic;

namespace MUDEngine
{
    /// <summary>
    /// Equipment wear data.
    /// </summary>
    public class WearData
    {
        /// <summary>
        /// Wear flags.
        /// </summary>
        public Bitvector WearFlag { get; set; }
        /// <summary>
        /// Equipment slots the item can be worn in.
        /// </summary>
        public List<int> WearLocations { get; set; }
        /// <summary>
        /// Message sent to the wearer when worn.
        /// </summary>
        public string WearMessageToWearer { get; set; }
        /// <summary>
        /// Message sent to the room when worn.
        /// </summary>
        public string WearMessageToRoom { get; set; }
        /// <summary>
        /// Body part required to wear the equipment.
        /// </summary>
        public int BodyPartNeeded { get; set; }
        /// <summary>
        /// Races that cannot wear the equipment. Some overlap with BodyPartNeeded.
        /// </summary>
        public int RacesNotAllowed { get; set; }

        public WearData( Bitvector wearflag, int wearloc1, int wearloc2, int wearloc3, string wearmsg1, string wearmsg2, int partneeded, int racenotallowed )
        {
            WearFlag = wearflag;
            WearLocations = new List<int>();
            if (wearloc1 != 0)
            {
                WearLocations.Add(wearloc1);
            }
            if (wearloc2 != 0)
            {
                WearLocations.Add(wearloc2);
            }
            if (wearloc3 != 0)
            {
                WearLocations.Add(wearloc3);
            }
            WearMessageToWearer = wearmsg1;
            WearMessageToRoom = wearmsg2;
            BodyPartNeeded = partneeded;
            RacesNotAllowed = racenotallowed;
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
