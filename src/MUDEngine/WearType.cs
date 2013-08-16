using ModernMUD;

namespace MUDEngine
{
    public class WearType
    {
        public WearType(ObjTemplate.WearLocation loc, Bitvector bit)
        {
            _wearLocation = loc;
            _wearBit = bit;
        }
        public ObjTemplate.WearLocation _wearLocation;
        public Bitvector _wearBit;

        public WearType[] WearTable = new WearType[] 
        {
            new WearType( ObjTemplate.WearLocation.none, ObjTemplate.WEARABLE_CARRY ),
            new WearType( ObjTemplate.WearLocation.finger_left, ObjTemplate.WEARABLE_FINGER ),
            new WearType( ObjTemplate.WearLocation.finger_right, ObjTemplate.WEARABLE_FINGER ),
            new WearType( ObjTemplate.WearLocation.neck_one, ObjTemplate.WEARABLE_NECK ),
            new WearType( ObjTemplate.WearLocation.neck_two, ObjTemplate.WEARABLE_NECK ),
            new WearType( ObjTemplate.WearLocation.body, ObjTemplate.WEARABLE_BODY ),
            new WearType( ObjTemplate.WearLocation.head, ObjTemplate.WEARABLE_HEAD ),
            new WearType( ObjTemplate.WearLocation.legs, ObjTemplate.WEARABLE_LEGS ),
            new WearType( ObjTemplate.WearLocation.feet, ObjTemplate.WEARABLE_FEET ),
            new WearType( ObjTemplate.WearLocation.hands, ObjTemplate.WEARABLE_HANDS ),
            new WearType( ObjTemplate.WearLocation.arms, ObjTemplate.WEARABLE_ARMS ),
            new WearType( ObjTemplate.WearLocation.about_body, ObjTemplate.WEARABLE_ABOUT ),
            new WearType( ObjTemplate.WearLocation.waist, ObjTemplate.WEARABLE_WAIST ),
            new WearType( ObjTemplate.WearLocation.wrist_left, ObjTemplate.WEARABLE_WRIST ),
            new WearType( ObjTemplate.WearLocation.wrist_right, ObjTemplate.WEARABLE_WRIST ),
            new WearType( ObjTemplate.WearLocation.hand_one, ObjTemplate.WEARABLE_WIELD ),
            new WearType( ObjTemplate.WearLocation.eyes, ObjTemplate.WEARABLE_EYES ),
            new WearType( ObjTemplate.WearLocation.face, ObjTemplate.WEARABLE_FACE ),
            new WearType( ObjTemplate.WearLocation.badge, ObjTemplate.WEARABLE_BADGE ),
            new WearType( ObjTemplate.WearLocation.quiver, ObjTemplate.WEARABLE_QUIVER ),
            new WearType( ObjTemplate.WearLocation.ear_left, ObjTemplate.WEARABLE_EAR ),
            new WearType( ObjTemplate.WearLocation.ear_right, ObjTemplate.WEARABLE_EAR ),
            new WearType( ObjTemplate.WearLocation.on_back, ObjTemplate.WEARABLE_ONBACK ),
            new WearType( ObjTemplate.WearLocation.belt_attach_one, ObjTemplate.WEARABLE_ATTACH_BELT ),
            new WearType( ObjTemplate.WearLocation.belt_attach_two, ObjTemplate.WEARABLE_ATTACH_BELT ),
            new WearType( ObjTemplate.WearLocation.tail, ObjTemplate.WEARABLE_TAIL ),
            new WearType( ObjTemplate.WearLocation.horns, ObjTemplate.WEARABLE_HORNS )
        };

    };

}
