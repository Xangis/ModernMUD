using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Maps wear slots to on-body wear locations.
    /// </summary>
    public class WearInfo
    {
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="bit"></param>
        public WearInfo(ObjTemplate.WearLocation loc, Bitvector bit)
        {
            WornLocation = loc;
            WearBit = bit;
        }

        /// <summary>
        /// Where the object is worn.
        /// </summary>
        public ObjTemplate.WearLocation WornLocation { get; set; }

        /// <summary>
        /// The wear flag for the object.
        /// </summary>
        public Bitvector WearBit { get; set; }

        /// <summary>
        /// The mapping table for WearInfo.
        /// </summary>
        public WearInfo[] WearTable = new WearInfo[] 
        {
            new WearInfo( ObjTemplate.WearLocation.none, ObjTemplate.WEARABLE_CARRY ),
            new WearInfo( ObjTemplate.WearLocation.finger_left, ObjTemplate.WEARABLE_FINGER ),
            new WearInfo( ObjTemplate.WearLocation.finger_right, ObjTemplate.WEARABLE_FINGER ),
            new WearInfo( ObjTemplate.WearLocation.neck_one, ObjTemplate.WEARABLE_NECK ),
            new WearInfo( ObjTemplate.WearLocation.neck_two, ObjTemplate.WEARABLE_NECK ),
            new WearInfo( ObjTemplate.WearLocation.body, ObjTemplate.WEARABLE_BODY ),
            new WearInfo( ObjTemplate.WearLocation.head, ObjTemplate.WEARABLE_HEAD ),
            new WearInfo( ObjTemplate.WearLocation.legs, ObjTemplate.WEARABLE_LEGS ),
            new WearInfo( ObjTemplate.WearLocation.feet, ObjTemplate.WEARABLE_FEET ),
            new WearInfo( ObjTemplate.WearLocation.hands, ObjTemplate.WEARABLE_HANDS ),
            new WearInfo( ObjTemplate.WearLocation.arms, ObjTemplate.WEARABLE_ARMS ),
            new WearInfo( ObjTemplate.WearLocation.about_body, ObjTemplate.WEARABLE_ABOUT ),
            new WearInfo( ObjTemplate.WearLocation.waist, ObjTemplate.WEARABLE_WAIST ),
            new WearInfo( ObjTemplate.WearLocation.wrist_left, ObjTemplate.WEARABLE_WRIST ),
            new WearInfo( ObjTemplate.WearLocation.wrist_right, ObjTemplate.WEARABLE_WRIST ),
            new WearInfo( ObjTemplate.WearLocation.hand_one, ObjTemplate.WEARABLE_WIELD ),
            new WearInfo( ObjTemplate.WearLocation.eyes, ObjTemplate.WEARABLE_EYES ),
            new WearInfo( ObjTemplate.WearLocation.face, ObjTemplate.WEARABLE_FACE ),
            new WearInfo( ObjTemplate.WearLocation.badge, ObjTemplate.WEARABLE_BADGE ),
            new WearInfo( ObjTemplate.WearLocation.quiver, ObjTemplate.WEARABLE_QUIVER ),
            new WearInfo( ObjTemplate.WearLocation.ear_left, ObjTemplate.WEARABLE_EAR ),
            new WearInfo( ObjTemplate.WearLocation.ear_right, ObjTemplate.WEARABLE_EAR ),
            new WearInfo( ObjTemplate.WearLocation.on_back, ObjTemplate.WEARABLE_ONBACK ),
            new WearInfo( ObjTemplate.WearLocation.belt_attach_one, ObjTemplate.WEARABLE_ATTACH_BELT ),
            new WearInfo( ObjTemplate.WearLocation.belt_attach_two, ObjTemplate.WEARABLE_ATTACH_BELT ),
            new WearInfo( ObjTemplate.WearLocation.tail, ObjTemplate.WEARABLE_TAIL ),
            new WearInfo( ObjTemplate.WearLocation.horns, ObjTemplate.WEARABLE_HORNS )
        };
    }
}
