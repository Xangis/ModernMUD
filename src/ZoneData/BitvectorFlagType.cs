using System;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Used for bitvector-based flag tables.
    /// </summary>
    public class BitvectorFlagType
    {
        private string _name;
        private Bitvector _bitvector;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BitvectorFlagType()
        {
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="bitv"></param>
        /// <param name="set"></param>
        public BitvectorFlagType(string nam, Bitvector bitv, bool set)
        {
            _name = nam;
            _bitvector = bitv;
            Settable = set;
            VisibleToMortals = true;
        }

        /// <summary>
        /// Parameterized constructor that lets us set the visible to mortals flag.
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="bitv"></param>
        /// <param name="set"></param>
        /// <param name="visible"></param>
        public BitvectorFlagType(string nam, Bitvector bitv, bool set, bool visible)
        {
            _name = nam;
            _bitvector = bitv;
            Settable = set;
            VisibleToMortals = visible;
        }

        /// <summary>
        /// Zone-wide flags.
        /// </summary>
        public static BitvectorFlagType[] AreaFlags = 
        {
            new BitvectorFlagType( "none", Area.AREA_NONE, false ),
            new BitvectorFlagType( "no_dimdoor",Area.AREA_NO_DIMDOOR, true ),
            new BitvectorFlagType( "no_gate", Area.AREA_NO_GATE, true ),
            new BitvectorFlagType( "worldmap", Area.AREA_WORLDMAP, true ),
            new BitvectorFlagType( "underground", Area.AREA_UNDERGROUND, true )
        };

        /// <summary>
        /// Flags for containers (bags, chests, etc.)
        /// </summary>
        public static BitvectorFlagType[] ContainerFlags = 
        {
            new BitvectorFlagType( "none", ObjTemplate.CONTAINER_NONE, false ),
            new BitvectorFlagType( "closeable", ObjTemplate.CONTAINER_CLOSEABLE, true ),
            new BitvectorFlagType( "hard_to_pick", ObjTemplate.CONTAINER_HARDTOPICK, true ),
            new BitvectorFlagType( "closed", ObjTemplate.CONTAINER_CLOSED, true ),
            new BitvectorFlagType( "locked", ObjTemplate.CONTAINER_LOCKED, true ),
            new BitvectorFlagType( "pickproof", ObjTemplate.CONTAINER_PICKPROOF, true ),
        };

        /// <summary>
        /// Flags for exits (doors).
        /// </summary>
        public static BitvectorFlagType[] ExitFlags =
        {
            new BitvectorFlagType( "none", new Bitvector(0, (int)Exit.ExitFlag.none), false ),
            new BitvectorFlagType( "bashed", new Bitvector(0, (int)Exit.ExitFlag.bashed), false ),
            new BitvectorFlagType( "bashproof", new Bitvector(0, (int)Exit.ExitFlag.bashproof), true ),
            new BitvectorFlagType( "blocked", new Bitvector(0, (int)Exit.ExitFlag.blocked), true ),
            new BitvectorFlagType( "closed", new Bitvector(0, (int)Exit.ExitFlag.closed), true ),
            new BitvectorFlagType( "destroys_key", new Bitvector(0, (int)Exit.ExitFlag.destroys_key), true ),
            new BitvectorFlagType( "illusion", new Bitvector(0, (int)Exit.ExitFlag.illusion), true ),
            new BitvectorFlagType( "is_door", new Bitvector(0, (int)Exit.ExitFlag.is_door), true ),
            new BitvectorFlagType( "jammed", new Bitvector(0, (int)Exit.ExitFlag.jammed), true ),
            new BitvectorFlagType( "locked", new Bitvector(0, (int)Exit.ExitFlag.locked), true ),
            new BitvectorFlagType( "passproof", new Bitvector(0, (int)Exit.ExitFlag.passproof), true ),
            new BitvectorFlagType( "pickproof", new Bitvector(0, (int)Exit.ExitFlag.pickproof), true ),
            new BitvectorFlagType( "secret", new Bitvector(0, (int)Exit.ExitFlag.secret), true ),
            new BitvectorFlagType( "spiked", new Bitvector(0, (int)Exit.ExitFlag.spiked), true ),
            new BitvectorFlagType( "trapped", new Bitvector(0, (int)Exit.ExitFlag.trapped), true ),
            new BitvectorFlagType( "walled", new Bitvector(0, (int)Exit.ExitFlag.walled), false ),
        };

        /// <summary>
        /// Flags for totems.
        /// </summary>
        public static BitvectorFlagType[] TotemFlags =
        {
            new BitvectorFlagType( "lesser_animal", ObjTemplate.TOTEM_L_ANIMAL, true ),
            new BitvectorFlagType( "lesser_elemental", ObjTemplate.TOTEM_L_ELEMENTAL, true ),
            new BitvectorFlagType( "lesser_spirit", ObjTemplate.TOTEM_L_SPIRIT, true ),
            new BitvectorFlagType( "greater_animal", ObjTemplate.TOTEM_G_ANIMAL, true ),
            new BitvectorFlagType( "greater_elemental", ObjTemplate.TOTEM_G_ELEMENTAL, true ),
            new BitvectorFlagType( "greater_spirit", ObjTemplate.TOTEM_G_SPIRIT, true ),
        };

        /// <summary>
        /// Mob behavior flags.
        /// </summary>
        public static BitvectorFlagType[] MobActFlags = 
        {
            new BitvectorFlagType( "no_telekinesis", MobTemplate.ACT_CANT_TK, true ),
            new BitvectorFlagType( "guardian", MobTemplate.ACT_GUARDIAN, true ),
            new BitvectorFlagType( "outlaw", MobTemplate.ACT_OUTLAW, true ),
            new BitvectorFlagType( "witness", MobTemplate.ACT_WITNESS, true ),
            new BitvectorFlagType( "can_fly", MobTemplate.ACT_CANFLY, true ),
            new BitvectorFlagType( "can_swim", MobTemplate.ACT_CANSWIM, true ),
            new BitvectorFlagType( "npc", MobTemplate.ACT_IS_NPC, false ),
            new BitvectorFlagType( "moved", MobTemplate.ACT_MOVED, false ),
            new BitvectorFlagType( "sizeplusone", MobTemplate.ACT_SIZEPLUS, true ),
            new BitvectorFlagType( "sizeminusone", MobTemplate.ACT_SIZEMINUS, true ),
            new BitvectorFlagType( "no_exp", MobTemplate.ACT_NOEXP, true ),
            new BitvectorFlagType( "no_bash", MobTemplate.ACT_NOBASH, true ),
            new BitvectorFlagType( "no_charm", MobTemplate.ACT_NOCHARM, true ),
            new BitvectorFlagType( "no_para", MobTemplate.ACT_NOPARA, true ),
            new BitvectorFlagType( "no_summon", MobTemplate.ACT_NOSUMMON, true ),
            new BitvectorFlagType( "memory", MobTemplate.ACT_MEMORY, true ),
            new BitvectorFlagType( "hunter", MobTemplate.ACT_HUNTER, true ),
            new BitvectorFlagType( "sentinel", MobTemplate.ACT_SENTINEL, true ),
            new BitvectorFlagType( "scavenger", MobTemplate.ACT_SCAVENGER, true ),
            new BitvectorFlagType( "aggressive", MobTemplate.ACT_AGGRESSIVE, true ),
            new BitvectorFlagType( "aggr_good", MobTemplate.ACT_AGGROGOOD, true ),
            new BitvectorFlagType( "aggr_evil", MobTemplate.ACT_AGGROEVIL, true ),
            new BitvectorFlagType( "aggr_neut", MobTemplate.ACT_AGGRONEUT, true ),
            new BitvectorFlagType( "aggr_goodrace", MobTemplate.ACT_AGGROGOODRACE, true ),
            new BitvectorFlagType( "aggr_evilrace", MobTemplate.ACT_AGGROEVILRACE, true ),
            new BitvectorFlagType( "stay_area", MobTemplate.ACT_STAY_AREA, true ),
            new BitvectorFlagType( "wimpy", MobTemplate.ACT_WIMPY, true ),
            new BitvectorFlagType( "pet", MobTemplate.ACT_PET, true ),
            new BitvectorFlagType( "teacher", MobTemplate.ACT_TEACHER, true ),
            new BitvectorFlagType( "protector", MobTemplate.ACT_PROTECTOR, true ),
            new BitvectorFlagType( "mountable", MobTemplate.ACT_MOUNT, true ),
            new BitvectorFlagType( "dispel_wall", MobTemplate.ACT_DISPEL_WALL, true ),
        };

        /// <summary>
        /// Wear location flags.
        /// </summary>
        public static BitvectorFlagType[] WearFlags =
        {
            new BitvectorFlagType( "carryable", ObjTemplate.WEARABLE_CARRY, true ),
            new BitvectorFlagType( "wear_finger", ObjTemplate.WEARABLE_FINGER, true ),
            new BitvectorFlagType( "wear_neck", ObjTemplate.WEARABLE_NECK, true ),
            new BitvectorFlagType( "wear_body", ObjTemplate.WEARABLE_BODY, true ),
            new BitvectorFlagType( "wear_head", ObjTemplate.WEARABLE_HEAD, true ),
            new BitvectorFlagType( "wear_legs", ObjTemplate.WEARABLE_LEGS, true ),
            new BitvectorFlagType( "wear_feet", ObjTemplate.WEARABLE_FEET, true ),
            new BitvectorFlagType( "wear_hands", ObjTemplate.WEARABLE_HANDS, true ),
            new BitvectorFlagType( "wear_arms", ObjTemplate.WEARABLE_ARMS, true ),
            new BitvectorFlagType( "wear_shield", ObjTemplate.WEARABLE_SHIELD, true ),
            new BitvectorFlagType( "wear_aboutbody", ObjTemplate.WEARABLE_ABOUT, true ),
            new BitvectorFlagType( "wear_waist", ObjTemplate.WEARABLE_WAIST, true ),
            new BitvectorFlagType( "wear_wrist", ObjTemplate.WEARABLE_WRIST, true ),
            new BitvectorFlagType( "wear_wield", ObjTemplate.WEARABLE_WIELD, true ),
            new BitvectorFlagType( "wear_hold", ObjTemplate.WEARABLE_HOLD, true ),
            // 15 and 16 not used
            new BitvectorFlagType( "wear_eyes", ObjTemplate.WEARABLE_EYES, true ),
            new BitvectorFlagType( "wear_face", ObjTemplate.WEARABLE_FACE, true ),
            new BitvectorFlagType( "wear_ear", ObjTemplate.WEARABLE_EAR, true ),
            new BitvectorFlagType( "wear_quiver", ObjTemplate.WEARABLE_QUIVER, true ),
            new BitvectorFlagType( "wear_badge", ObjTemplate.WEARABLE_BADGE, true ),
            new BitvectorFlagType( "wear_onback", ObjTemplate.WEARABLE_ONBACK, true ),
            new BitvectorFlagType( "wear_attachbelt", ObjTemplate.WEARABLE_ATTACH_BELT, true ),
            new BitvectorFlagType( "wear_horsebody", ObjTemplate.WEARABLE_HORSE_BODY, true ),
            new BitvectorFlagType( "wear_tail", ObjTemplate.WEARABLE_TAIL, true ),
            new BitvectorFlagType( "wear_nose", ObjTemplate.WEARABLE_NOSE, true ),
            new BitvectorFlagType( "wear_horns", ObjTemplate.WEARABLE_HORNS, true ),
        };

        /// <summary>
        /// Racial innate abilities.
        /// </summary>
        public static BitvectorFlagType[] InnateFlags =
        {
            new BitvectorFlagType( "PC", Race.RACE_PC_AVAIL, true ),
            new BitvectorFlagType( "PC_NEUTRAL", Race.RACE_PC_NEUTRAL, true),
            new BitvectorFlagType( "waterbreather", Race.RACE_WATERBREATH, true),
            new BitvectorFlagType( "fly", Race.RACE_FLY, true),
            new BitvectorFlagType( "swim", Race.RACE_SWIM, true),
            new BitvectorFlagType( "waterwalk", Race.RACE_WATERWALK, true),
            new BitvectorFlagType( "passdoor", Race.RACE_PASSDOOR, true),
            new BitvectorFlagType( "infravision", Race.RACE_INFRAVISION, true),
            new BitvectorFlagType( "detectalign", Race.RACE_DETECT_ALIGN, true),
            new BitvectorFlagType( "detectivis", Race.RACE_DETECT_INVIS, true),
            new BitvectorFlagType( "detecthidden", Race.RACE_DETECT_HIDDEN, true),
            new BitvectorFlagType( "extraarms", Race.RACE_EXTRA_ARMS, true),
            new BitvectorFlagType( "faeriefire", Race.RACE_FAERIE_FIRE, true),
            new BitvectorFlagType( "weaponwield", Race.RACE_WEAPON_WIELD, true),
            new BitvectorFlagType( "mute", Race.RACE_MUTE, true),
            new BitvectorFlagType( "bodyslam", Race.RACE_BODYSLAM, true),
            new BitvectorFlagType( "charge", Race.RACE_CHARGE, true),
            new BitvectorFlagType( "ultravision", Race.RACE_ULTRAVISION, true),
            new BitvectorFlagType( "doorbash", Race.RACE_DOORBASH, true),
            new BitvectorFlagType( "shrug", Race.RACE_SHRUG, true),
            new BitvectorFlagType( "outdoorsneak", Race.RACE_ODSNEAK, true),
            new BitvectorFlagType( "underdarksneak", Race.RACE_UDSNEAK, true),
            new BitvectorFlagType( "strength", Race.RACE_STRENGTH, true),
            new BitvectorFlagType( "underdark_vision", Race.RACE_UNDERDARK_VIS, true),
            new BitvectorFlagType( "enlarge", Race.RACE_ENLARGE, true),
            new BitvectorFlagType( "invis", Race.RACE_INVIS, true),
            new BitvectorFlagType( "trample", Race.RACE_TRAMPLE, true),
            new BitvectorFlagType( "shiftprime", Race.RACE_SHIFT_PRIME, true),
            new BitvectorFlagType( "shiftastral", Race.RACE_SHIFT_ASTRAL, true),
            new BitvectorFlagType( "levitate", Race.RACE_LEVITATE, true),
            new BitvectorFlagType( "bite", Race.RACE_BITE, true),
            new BitvectorFlagType( "strongwield", Race.RACE_EXTRA_STRONG_WIELD, true),
            new BitvectorFlagType( "negatespell", Race.RACE_NEGATE_SPELL, true),
            new BitvectorFlagType( "channel_berzerk", Race.RACE_CHANNEL_BERZERK, true),
            new BitvectorFlagType( "nobash", Race.RACE_NOBASH, true),
            new BitvectorFlagType( "nospringleap", Race.RACE_NOSPRING, true),
            new BitvectorFlagType( "nobodyslam", Race.RACE_NOSLAM, true),
            new BitvectorFlagType( "notrip", Race.RACE_NOTRIP, true),
            new BitvectorFlagType( "aware", Race.RACE_AWARE, true),
            new BitvectorFlagType( "bloodrage", Race.RACE_BLOODRAGE, true),
            new BitvectorFlagType( "darkness", Race.RACE_DARKNESS, true),
            new BitvectorFlagType( "climb", Race.RACE_CLIMB, true),
            new BitvectorFlagType( "confuse", Race.RACE_CONFUSE, true),
            new BitvectorFlagType( "dig", Race.RACE_DIG, true),
            new BitvectorFlagType( "bad-dodge", Race.RACE_BAD_DODGE, true),
            new BitvectorFlagType( "good-dodge", Race.RACE_GOOD_DODGE, true),
            new BitvectorFlagType( "regenerate", Race.RACE_REGENERATE, true),
            new BitvectorFlagType( "slam-larger", Race.RACE_SLAM_LARGER, true),
            new BitvectorFlagType( String.Empty, null, false )
        };

        /// <summary>
        /// Room settings flags.
        /// </summary>
        public static BitvectorFlagType[] RoomFlags =
        {
            new BitvectorFlagType( "airystarshell", RoomTemplate.ROOM_AIRY_STARSHELL, true ),
            new BitvectorFlagType( "bank", RoomTemplate.ROOM_BANK, true ),
            new BitvectorFlagType( "dockable", RoomTemplate.ROOM_DOCKABLE, true ),
            new BitvectorFlagType( "dark", RoomTemplate.ROOM_DARK, true ),
            new BitvectorFlagType( "earthenstarshell", RoomTemplate.ROOM_EARTHEN_STARSHELL,true ),
            new BitvectorFlagType( "fierystarshell", RoomTemplate.ROOM_FIERY_STARSHELL, true ),
            new BitvectorFlagType( "guildroom", RoomTemplate.ROOM_GUILDROOM, true ),
            new BitvectorFlagType( "heal", RoomTemplate.ROOM_HEAL, true ),
            new BitvectorFlagType( "hypnoticpattern", RoomTemplate.ROOM_HYPNOTIC_PATTERN, true ),
            new BitvectorFlagType( "indoors", RoomTemplate.ROOM_INDOORS, true ),
            new BitvectorFlagType( "inn", RoomTemplate.ROOM_INN, true ),
            new BitvectorFlagType( "jail", RoomTemplate.ROOM_JAIL, true ),
            new BitvectorFlagType( "magicdark", RoomTemplate.ROOM_MAGICDARK, true ),
            new BitvectorFlagType( "magiclight", RoomTemplate.ROOM_MAGICLIGHT, true ),
            new BitvectorFlagType( "nomob", RoomTemplate.ROOM_NO_MOB, true ),
            new BitvectorFlagType( "noheal", RoomTemplate.ROOM_NO_HEAL, true ),
            new BitvectorFlagType( "norecall", RoomTemplate.ROOM_NO_RECALL, true ),
            new BitvectorFlagType( "noscan", RoomTemplate.ROOM_NO_SCAN, true ),
            new BitvectorFlagType( "nomagic", RoomTemplate.ROOM_NO_MAGIC, true ),
            new BitvectorFlagType( "nogate", RoomTemplate.ROOM_NO_GATE, true ),
            new BitvectorFlagType( "nosummon", RoomTemplate.ROOM_NO_SUMMON, true ),
            new BitvectorFlagType( "noteleport", RoomTemplate.ROOM_NO_TELEPORT, true ),
            new BitvectorFlagType( "noprecipitation", RoomTemplate.ROOM_NO_PRECIP, true ),
            new BitvectorFlagType( "private", RoomTemplate.ROOM_PRIVATE, true ),
            new BitvectorFlagType( "pet_shop", RoomTemplate.ROOM_PET_SHOP, true ),
            new BitvectorFlagType( "safe", RoomTemplate.ROOM_SAFE, true ),
            new BitvectorFlagType( "solitary", RoomTemplate.ROOM_SOLITARY, true ),
            new BitvectorFlagType( "silence", RoomTemplate.ROOM_SILENT, true ),
            new BitvectorFlagType( "singlefile", RoomTemplate.ROOM_SINGLE_FILE, true ),
            new BitvectorFlagType( "tunnel", RoomTemplate.ROOM_TUNNEL, true ),
            new BitvectorFlagType( "twilight", RoomTemplate.ROOM_TWILIGHT, true ),
            new BitvectorFlagType( "underwater", RoomTemplate.ROOM_UNDERWATER, true ),
            new BitvectorFlagType( "waterystarshell", RoomTemplate.ROOM_WATERY_STARSHELL, true )
        };

        /// <summary>
        /// Creature affect flags.
        /// </summary>
        public static BitvectorFlagType[] AffectFlags = 
        {
            new BitvectorFlagType( "absorbing", Affect.AFFECT_ABSORBING, true),
            new BitvectorFlagType( "armor", Affect.AFFECT_ARMOR, true),
            new BitvectorFlagType( "awareness", Affect.AFFECT_AWARE, true),
            new BitvectorFlagType( "awareskill", Affect.AFFECT_SKL_AWARE, true, false),
            new BitvectorFlagType( "barkskin", Affect.AFFECT_BARKSKIN, true),
            new BitvectorFlagType( "battle ecstasy", Affect.AFFECT_BATTLE_ECSTASY, true),
            new BitvectorFlagType( "bearhug", Affect.AFFECT_BEARHUG, true),
            new BitvectorFlagType( "berzerk", Affect.AFFECT_BERZERK, true),
            new BitvectorFlagType( "biofeedback", Affect.AFFECT_BIOFEEDBACK, true),
            new BitvectorFlagType( "bless", Affect.AFFECT_BLESS, true),
            new BitvectorFlagType( "blind", Affect.AFFECT_BLIND, true),
            new BitvectorFlagType( "blur", Affect.AFFECT_BLUR, true),
            new BitvectorFlagType( "bound", Affect.AFFECT_BOUND, true),
            new BitvectorFlagType( "brave", Affect.AFFECT_BRAVE, false),
            new BitvectorFlagType( "camping", Affect.AFFECT_CAMPING, true),
            new BitvectorFlagType( "cannibalizing", Affect.AFFECT_CANNIBALIZING, true),
            new BitvectorFlagType( "casting", Affect.AFFECT_CASTING, true),
            new BitvectorFlagType( "changeself", Affect.AFFECT_CHANGE_SELF, true),
            new BitvectorFlagType( "changesex", Affect.AFFECT_CHANGE_SEX, true),
            new BitvectorFlagType( "charm", Affect.AFFECT_CHARM, true),
            new BitvectorFlagType( "charm of the otter", Affect.AFFECT_CHARM_OTTER, true),
            new BitvectorFlagType( "climbing", Affect.AFFECT_CLIMBING, true),
            new BitvectorFlagType( "coldshield", Affect.AFFECT_COLDSHIELD, true),
            new BitvectorFlagType( "complang", Affect.AFFECT_COMP_LANG, true),
            new BitvectorFlagType( "concealed", Affect.AFFECT_MINOR_INVIS, true),
            new BitvectorFlagType( "coordination", Affect.AFFECT_COORDINATION, true),
            new BitvectorFlagType( "cowardly", Affect.AFFECT_COWARDLY, false),
            new BitvectorFlagType( "cover", Affect.AFFECT_COVER, true),
            new BitvectorFlagType( "curse", Affect.AFFECT_CURSE, true),
            new BitvectorFlagType( "dazzle", Affect.AFFECT_DAZZLE, true),
            new BitvectorFlagType( "dazzled", Affect.AFFECT_DAZZLED, true),
            new BitvectorFlagType( "denyair", Affect.AFFECT_DENY_AIR, true),
            new BitvectorFlagType( "denyearth", Affect.AFFECT_DENY_EARTH, true),
            new BitvectorFlagType( "denyfire", Affect.AFFECT_DENY_FIRE, true),
            new BitvectorFlagType( "denywater", Affect.AFFECT_DENY_WATER, true),
            new BitvectorFlagType( "detectevil", Affect.AFFECT_DETECT_EVIL, true),
            new BitvectorFlagType( "detecthidden", Affect.AFFECT_DETECT_HIDDEN, true),
            new BitvectorFlagType( "detectinvis", Affect.AFFECT_DETECT_INVIS, true),
            new BitvectorFlagType( "detectmagic", Affect.AFFECT_DETECT_MAGIC, true),
            new BitvectorFlagType( "detectundead", Affect.AFFECT_DETECT_UNDEAD, true),
            new BitvectorFlagType( "detectgood", Affect.AFFECT_DETECT_GOOD, true),
            new BitvectorFlagType( "dex increased", Affect.AFFECT_DEXTERITY_INCREASED, false),
            new BitvectorFlagType( "draining", Affect.AFFECT_DRAINING, true),
            new BitvectorFlagType( "droppedprimary", Affect.AFFECT_DROPPED_PRIMARY, true),
            new BitvectorFlagType( "droppedsecondary", Affect.AFFECT_DROPPED_SECOND, true),
            new BitvectorFlagType( "drowning", Affect.AFFECT_DROWNING, true),
            new BitvectorFlagType( "duergarhide", Affect.AFFECT_DUERGAR_HIDE, true, false),
            new BitvectorFlagType( "ectoplasmic", Affect.AFFECT_ECTOPLASMIC, true),
            new BitvectorFlagType( "elementalsight", Affect.AFFECT_ELEM_SIGHT, true),
            new BitvectorFlagType( "elementalform", Affect.AFFECT_ELEMENTAL_FORM, true),
            new BitvectorFlagType( "endurance", Affect.AFFECT_ENDURANCE, true),
            new BitvectorFlagType( "enlarge", Affect.AFFECT_ENLARGED, true),
            new BitvectorFlagType( "faeriefire", Affect.AFFECT_FAERIE_FIRE, true),
            new BitvectorFlagType( "famine", Affect.AFFECT_FAMINE, true),
            new BitvectorFlagType( "farsee", Affect.AFFECT_FARSEE, true),
            new BitvectorFlagType( "fear", Affect.AFFECT_FEAR, true ),
            new BitvectorFlagType( "feeblemind", Affect.AFFECT_FEEBLEMIND, true),
            new BitvectorFlagType( "flaming", Affect.AFFECT_FIRESHIELD, true),
            new BitvectorFlagType( "flying", Affect.AFFECT_FLYING, true),
            new BitvectorFlagType( "fortitude", Affect.AFFECT_FORTITUDE, true),
            new BitvectorFlagType( "fourarms", Affect.AFFECT_FOUR_ARMS, true),
            new BitvectorFlagType( "fumbledprimary", Affect.AFFECT_FUMBLED_PRIMARY, true),
            new BitvectorFlagType( "fumbledsecondary", Affect.AFFECT_FUMBLED_SECOND, true),
            new BitvectorFlagType( "ghoul", Affect.AFFECT_WRAITHFORM, true),
            new BitvectorFlagType( "gills", Affect.AFFECT_BREATHE_UNDERWATER, true),
            new BitvectorFlagType( "grappling", Affect.AFFECT_GRAPPLING, true),
            new BitvectorFlagType( "grappled", Affect.AFFECT_GRAPPLED, true),
            new BitvectorFlagType( "grspiritward", Affect.AFFECT_GREATER_SPIRIT_WARD,true),
            new BitvectorFlagType( "grstornogs", Affect.AFFECT_GREATER_SPHERES, true),
            new BitvectorFlagType( "haste", Affect.AFFECT_HASTE, true),
            new BitvectorFlagType( "hide", Affect.AFFECT_HIDE, true, false),
            new BitvectorFlagType( "hold", Affect.AFFECT_HOLD, true),
            new BitvectorFlagType( "holy sacrifice", Affect.AFFECT_HOLY_SACRIFICE, true),
            new BitvectorFlagType( "hunting", Affect.AFFECT_HUNTING, true),
            new BitvectorFlagType( "holdingbreath", Affect.AFFECT_HOLDING_BREATH, true),
            new BitvectorFlagType( "incompetence", Affect.AFFECT_INCOMPETENCE, true),
            new BitvectorFlagType( "inertial", Affect.AFFECT_INFRAVISION, true),
            new BitvectorFlagType( "infrared", Affect.AFFECT_INERTIAL_BARRIER, true),
            new BitvectorFlagType( "infrared", Affect.AFFECT_INFRAVISION, true),
            new BitvectorFlagType( "insight", Affect.AFFECT_INSIGHT, true),
            // new BitvectorFlagType( "wristlocking", Affect.AFFECT_WRISTLOCKING, true),
            // new BitvectorFlagType( "wristlocked primary", Affect.AFFECT_WRISTLOCKED1, true),
            // new BitvectorFlagType( "wristlocked secondary",Affect.AFFECT_WRISTLOCKED2, true),
            new BitvectorFlagType( "intellectfort", Affect.AFFECT_INTELLECT_FORTRESS,true),
            new BitvectorFlagType( "invisible", Affect.AFFECT_INVISIBLE, true),
            new BitvectorFlagType( "isfleeing", Affect.AFFECT_IS_FLEEING, true),
            new BitvectorFlagType( "justicetracker", Affect.AFFECT_JUSTICE_TRACKER, true, false),
            new BitvectorFlagType( "ko", Affect.AFFECT_KNOCKED_OUT, true),
            new BitvectorFlagType( "layhandstimer", Affect.AFFECT_LAYHANDS_TIMER, true, false),
            new BitvectorFlagType( "levitate", Affect.AFFECT_LEVITATE, true),
            new BitvectorFlagType( "looter", Affect.AFFECT_LOOTER, true),
            new BitvectorFlagType( "mageflame", Affect.AFFECT_MAGE_FLAME, true),
            new BitvectorFlagType( "meditate", Affect.AFFECT_MEDITATE, true, false),
            new BitvectorFlagType( "might", Affect.AFFECT_MIGHT, true),
            new BitvectorFlagType( "misdirection", Affect.AFFECT_MISDIRECTION, true),
            new BitvectorFlagType( "majorglobe", Affect.AFFECT_MAJOR_GLOBE, true),
            new BitvectorFlagType( "majorphysical", Affect.AFFECT_MAJOR_PHYSICAL, true),
            new BitvectorFlagType( "memorizing", Affect.AFFECT_MEMORIZING, true),
            new BitvectorFlagType( "minorglobe", Affect.AFFECT_MINOR_GLOBE, true),
            new BitvectorFlagType( "minorpara", Affect.AFFECT_MINOR_PARA, true),
            new BitvectorFlagType( "moves increased", Affect.AFFECT_MOVEMENT_INCREASED, false),
            new BitvectorFlagType( "moves reduced", Affect.AFFECT_MOVEMENT_REDUCED, false),
            new BitvectorFlagType( "multiclass", Affect.AFFECT_MULTICLASS, true),
            new BitvectorFlagType( "mute", Affect.AFFECT_MUTE, true),
            new BitvectorFlagType( "neckbiting", Affect.AFFECT_NECKBITING, true),
            new BitvectorFlagType( "noimmolate", Affect.AFFECT_NO_IMMOLATE, true),
            new BitvectorFlagType( "nounmorph", Affect.AFFECT_NO_UNMORPH, true),
            new BitvectorFlagType( "nondetection", Affect.AFFECT_NON_DETECTION, true),
            new BitvectorFlagType( "paladinaura", Affect.AFFECT_PALADIN_AURA, true),
            new BitvectorFlagType( "passdoor", Affect.AFFECT_PASS_DOOR, true),
            new BitvectorFlagType( "passwithouttrace", Affect.AFFECT_PASS_WITHOUT_TRACE,true),
            new BitvectorFlagType( "protacid", Affect.AFFECT_PROTECT_ACID, true),
            new BitvectorFlagType( "protcold", Affect.AFFECT_PROTECT_COLD, true),
            new BitvectorFlagType( "protevil", Affect.AFFECT_PROTECT_EVIL, true),
            new BitvectorFlagType( "protfire", Affect.AFFECT_PROTECT_FIRE, true),
            new BitvectorFlagType( "protgas", Affect.AFFECT_PROTECT_GAS, true),
            new BitvectorFlagType( "protgood", Affect.AFFECT_PROTECT_GOOD, true),
            new BitvectorFlagType( "protlightning", Affect.AFFECT_PROTECT_LIGHTNING, true),
            new BitvectorFlagType( "prowess", Affect.AFFECT_PROWESS, true),
            new BitvectorFlagType( "plague", Affect.AFFECT_DISEASE, true),
            new BitvectorFlagType( "plusone", Affect.AFFECT_PLUS_ONE, true),
            new BitvectorFlagType( "plustwo", Affect.AFFECT_PLUS_TWO, true),
            new BitvectorFlagType( "plusthree", Affect.AFFECT_PLUS_THREE, true),
            new BitvectorFlagType( "plusfour", Affect.AFFECT_PLUS_FOUR, true),
            new BitvectorFlagType( "plusfive", Affect.AFFECT_PLUS_FIVE, true),
            new BitvectorFlagType( "poison", Affect.AFFECT_POISON, true),
            new BitvectorFlagType( "polymorph", Affect.AFFECT_POLYMORPH, true),
            new BitvectorFlagType( "protection from undead", Affect.AFFECT_PROTECT_FROM_UNDEAD, true),
            new BitvectorFlagType( "reduce", Affect.AFFECT_REDUCED, true),
            new BitvectorFlagType( "regeneration", Affect.AFFECT_REGENERATION, true),
            new BitvectorFlagType( "ritual of protection", Affect.AFFECT_RITUAL_OF_PROTECTION, true),
            new BitvectorFlagType( "sacking", Affect.AFFECT_SACKING, true),
            new BitvectorFlagType( "savvy", Affect.AFFECT_SAVVY, true),
            new BitvectorFlagType( "silver", Affect.AFFECT_SILVER, true),
            new BitvectorFlagType( "scribing", Affect.AFFECT_SCRIBING, true),
            new BitvectorFlagType( "sensefollower", Affect.AFFECT_SENSE_FOLLOWER, true),
            new BitvectorFlagType( "sanctuary", Affect.AFFECT_SANCTUARY, true),
            new BitvectorFlagType( "senselife", Affect.AFFECT_SENSE_LIFE, true),
            new BitvectorFlagType( "shadow form", Affect.AFFECT_SHADOW_FORM, true),
            new BitvectorFlagType( "shockshield", Affect.AFFECT_SHOCK_SHIELD, true),
            new BitvectorFlagType( "singing", Affect.AFFECT_SINGING, true),
            new BitvectorFlagType( "sleep", Affect.AFFECT_SLEEP, true),
            new BitvectorFlagType( "slowness", Affect.AFFECT_SLOWNESS, true),
            new BitvectorFlagType( "slowpoison", Affect.AFFECT_SLOW_POISON, true),
            new BitvectorFlagType( "sneak", Affect.AFFECT_SNEAK, true),
            new BitvectorFlagType( "soulshield", Affect.AFFECT_SOULSHIELD, true),
            new BitvectorFlagType( "spiritward", Affect.AFFECT_SPIRIT_WARD, true),
            new BitvectorFlagType( "stoneskin", Affect.AFFECT_STONESKIN, true),
            new BitvectorFlagType( "stornogspheres", Affect.AFFECT_STORNOG_SPHERES, true),
            new BitvectorFlagType( "str reduced", Affect.AFFECT_STRENGTH_REDUCED, false),
            new BitvectorFlagType( "str increased", Affect.AFFECT_STRENGTH_INCREASED, false),
            new BitvectorFlagType( "stunned", Affect.AFFECT_STUNNED, true),
            new BitvectorFlagType( "summonmounttimer", Affect.AFFECT_SUMMON_MOUNT_TIMER, true, false),
            new BitvectorFlagType( "swimming", Affect.AFFECT_SWIMMING, true),
            new BitvectorFlagType( "tensersdisc", Affect.AFFECT_TENSORS_DISC, true),
            new BitvectorFlagType( "throatcrush", Affect.AFFECT_THROAT_CRUSH, true),
            new BitvectorFlagType( "towerironwill", Affect.AFFECT_TOWER_OF_IRON_WILL, true),
            new BitvectorFlagType( "track", Affect.AFFECT_TRACK, true),
            new BitvectorFlagType( "tracking", Affect.AFFECT_TRACKING, true),
            new BitvectorFlagType( "ultravision", Affect.AFFECT_ULTRAVISION, true),
            new BitvectorFlagType( "underdarkvis", Affect.AFFECT_UNDERDARK_VISION, true),
            new BitvectorFlagType( "underwater", Affect.AFFECT_UNDERWATER, true),
            new BitvectorFlagType( "vacancy", Affect.AFFECT_VACANCY, true),
            new BitvectorFlagType( "vampbite", Affect.AFFECT_VAMP_BITE, true),
            new BitvectorFlagType( "vampform", Affect.AFFECT_VAMPIRE_FORM, true),
            new BitvectorFlagType( "vamptouch", Affect.AFFECT_VAMP_TOUCH, true),
            new BitvectorFlagType( "vision enhanced", Affect.AFFECT_VISION_ENHANCED, false),
            new BitvectorFlagType( "vision impaired", Affect.AFFECT_VISION_IMPAIRED, false),
            new BitvectorFlagType( "vitality", Affect.AFFECT_VITALITY, true),
            new BitvectorFlagType( "wither", Affect.AFFECT_WITHER, true),
        };

        /// <summary>
        /// Use flags for items.
        /// </summary>
        public static BitvectorFlagType[] UseFlags =
        {
            new BitvectorFlagType( "use_anyone", ObjTemplate.USE_ANYONE, true ),
            new BitvectorFlagType( "use_warrior", ObjTemplate.USE_WARRIOR, true),
            new BitvectorFlagType( "use_ranger", ObjTemplate.USE_RANGER, true),
            new BitvectorFlagType( "use_paladin", ObjTemplate.USE_PALADIN, true),
            new BitvectorFlagType( "use_anti", ObjTemplate.USE_ANTI, true),
            new BitvectorFlagType( "use_cleric", ObjTemplate.USE_CLERIC, true),
            new BitvectorFlagType( "use_monk", ObjTemplate.USE_MONK, true), // For later use
            new BitvectorFlagType( "use_druid", ObjTemplate.USE_DRUID, true),
            new BitvectorFlagType( "use_shaman", ObjTemplate.USE_SHAMAN, true),
            new BitvectorFlagType( "use_sorcerer", ObjTemplate.USE_SORCERER, true),
            new BitvectorFlagType( "use_necro", ObjTemplate.USE_NECRO, true),// For later use
            new BitvectorFlagType( "use_elemental", ObjTemplate.USE_ELEMENTAL, true),
            new BitvectorFlagType( "use_psi", ObjTemplate.USE_PSI, true),
            new BitvectorFlagType( "use_thief", ObjTemplate.USE_THIEF, true), // For later use
            new BitvectorFlagType( "use_assassin", ObjTemplate.USE_ASSASSIN, true),
            new BitvectorFlagType( "use_mercenary", ObjTemplate.USE_MERCENARY, true),
            new BitvectorFlagType( "use_bard", ObjTemplate.USE_BARD, true), // For later use
            new BitvectorFlagType( "use_nohuman", ObjTemplate.USE_NOHUMAN, true),
            new BitvectorFlagType( "use_nogreyelf", ObjTemplate.USE_NOGREYELF, true),
            new BitvectorFlagType( "use_nohalfelf", ObjTemplate.USE_NOHALFELF, true),
            new BitvectorFlagType( "use_nodwarf", ObjTemplate.USE_NODWARF, true),
            new BitvectorFlagType( "use_nohalfling", ObjTemplate.USE_NOHALFLING, true),
            new BitvectorFlagType( "use_nognome", ObjTemplate.USE_NOGNOME, true),
            new BitvectorFlagType( "use_nobarbarian", ObjTemplate.USE_NOBARBARIAN, true),
            new BitvectorFlagType( "use_noduergar", ObjTemplate.USE_NODUERGAR, true),
            new BitvectorFlagType( "use_nodrow", ObjTemplate.USE_NODROW, true),
            new BitvectorFlagType( "use_notroll", ObjTemplate.USE_NOTROLL, true),
            new BitvectorFlagType( "use_noogre", ObjTemplate.USE_NOOGRE, true),
            new BitvectorFlagType( "use_nogoodrace", ObjTemplate.USE_ANTIGOODRACE, true),
            new BitvectorFlagType( "use_noorc", ObjTemplate.USE_NOORC, true),
            new BitvectorFlagType( "use_noevilrace", ObjTemplate.USE_ANTIEVILRACE, true),
            // Anti2 Flags
            new BitvectorFlagType( "use_nothri", ObjTemplate.USE_NOTHRIKREEN, true),// For later use
            new BitvectorFlagType( "use_nocentaur", ObjTemplate.USE_NOCENTAUR, true),
            new BitvectorFlagType( "use_nogithyanki", ObjTemplate.USE_NOGITHYANKI, true),
            new BitvectorFlagType( "use_nominotaur", ObjTemplate.USE_NOMINOTAUR, true),
            new BitvectorFlagType( "use_nomale", ObjTemplate.USE_ANTIMALE, true),
            new BitvectorFlagType( "use_nofemale", ObjTemplate.USE_ANTIFEMALE, true),
            new BitvectorFlagType( "use_noneuter", ObjTemplate.USE_ANTINEUTER, true),
            new BitvectorFlagType( "use_noaquaelf", ObjTemplate.USE_NOAQUAELF, true), // For later use
            new BitvectorFlagType( "use_nosahaugin", ObjTemplate.USE_NOSAHAUGIN, true), // For later use
            new BitvectorFlagType( "use_nogoblin", ObjTemplate.USE_NOGOBLIN, true),
            new BitvectorFlagType( "use_norakshasa", ObjTemplate.USE_NORAKSHASA, true),
            new BitvectorFlagType( "use_nognoll", ObjTemplate.USE_NOGNOLL, true),
            new BitvectorFlagType( "use_nogithzerai", ObjTemplate.USE_NOGITHZERAI, true),
            new BitvectorFlagType( "use_nodual", ObjTemplate.USE_NODUAL, true)
        };

        /// <summary>
        /// Item flags.
        /// </summary>
        public static BitvectorFlagType[] ItemFlags = 
        {
            new BitvectorFlagType( "antigood", ObjTemplate.ITEM_ANTI_GOOD, true ),
            new BitvectorFlagType( "antievil", ObjTemplate.ITEM_ANTI_EVIL, true),
            new BitvectorFlagType( "antineutral", ObjTemplate.ITEM_ANTI_NEUTRAL, true),
            new BitvectorFlagType( "artifact", ObjTemplate.ITEM_ARTIFACT, true),
            new BitvectorFlagType( "bless", ObjTemplate.ITEM_BLESS, true),
            new BitvectorFlagType( "buried", ObjTemplate.ITEM_BURIED, true),
            new BitvectorFlagType( "disarmed", ObjTemplate.ITEM_WAS_DISARMED, true),
            new BitvectorFlagType( "evil", ObjTemplate.ITEM_EVIL, true),
            new BitvectorFlagType( "enlarged", ObjTemplate.ITEM_ENLARGED, true),
            new BitvectorFlagType( "float", ObjTemplate.ITEM_FLOAT, true),
            new BitvectorFlagType( "fly", ObjTemplate.ITEM_FLY, true),
            new BitvectorFlagType( "glow", ObjTemplate.ITEM_GLOW, true),
            new BitvectorFlagType( "goodonly", ObjTemplate.ITEM_GOODONLY, true),
            new BitvectorFlagType( "hum", ObjTemplate.ITEM_HUM, true),
            new BitvectorFlagType( "inventory", ObjTemplate.ITEM_INVENTORY, true),
            new BitvectorFlagType( "invis", ObjTemplate.ITEM_INVIS, true),
            new BitvectorFlagType( "levitates", ObjTemplate.ITEM_LEVITATES, true),
            new BitvectorFlagType( "lit", ObjTemplate.ITEM_LIT, true),
            new BitvectorFlagType( "magic", ObjTemplate.ITEM_MAGIC, true),
            new BitvectorFlagType( "nodrop", ObjTemplate.ITEM_NODROP, true),
            new BitvectorFlagType( "noshow", ObjTemplate.ITEM_NOSHOW, true),
            new BitvectorFlagType( "nosell", ObjTemplate.ITEM_NOSELL, true),
            new BitvectorFlagType( "noburn", ObjTemplate.ITEM_NOBURN, true),
            new BitvectorFlagType( "nolocate", ObjTemplate.ITEM_NOLOCATE, true),
            new BitvectorFlagType( "noid", ObjTemplate.ITEM_NOID, true),
            new BitvectorFlagType( "norent", ObjTemplate.ITEM_NORENT, true),
            new BitvectorFlagType( "rerepair", ObjTemplate.ITEM_NO_REPAIR, true),
            new BitvectorFlagType( "nosummon", ObjTemplate.ITEM_NOSUMMON, true),
            new BitvectorFlagType( "nosleep", ObjTemplate.ITEM_NOSLEEP, true),
            new BitvectorFlagType( "nocharm", ObjTemplate.ITEM_NOCHARM, true),
            new BitvectorFlagType( "poisoned", ObjTemplate.ITEM_POISONED, true),
            new BitvectorFlagType( "reduced", ObjTemplate.ITEM_REDUCED, true),
            new BitvectorFlagType( "secret", ObjTemplate.ITEM_SECRET, true),
            new BitvectorFlagType( "silver", ObjTemplate.ITEM_SILVER, true),
            new BitvectorFlagType( "poisoned", ObjTemplate.ITEM_POISONED, true),
            new BitvectorFlagType( "throwreturn", ObjTemplate.ITEM_THROW_RETURN, true),
            new BitvectorFlagType( "throwoneroom", ObjTemplate.ITEM_THROW_ONEROOM,true),
            new BitvectorFlagType( "throwtworooms",ObjTemplate.ITEM_THROW_TWOROOMS,true),
            new BitvectorFlagType( "transient", ObjTemplate.ITEM_TRANSIENT, true),
            new BitvectorFlagType( "unique", ObjTemplate.ITEM_UNIQUE, true),
            new BitvectorFlagType( "twohanded", ObjTemplate.ITEM_TWOHANDED, true),
            new BitvectorFlagType( "wholebody", ObjTemplate.ITEM_WHOLEBODY, true),
            new BitvectorFlagType( "wholehead", ObjTemplate.ITEM_WHOLEHEAD, true)
        };

        /// <summary>
        /// The bit vector data associated with this bitvector flag type.
        /// </summary>
        public Bitvector BitvectorData
        {
            get { return _bitvector; }
            set { _bitvector = value; }
        }

        /// <summary>
        /// Controls whether this bitvector is usable from an editor.
        /// </summary>
        public bool Settable { get; set; }

        /// <summary>
        /// Indicates whether this bitvector should be shown to mortals.
        /// </summary>
        public bool VisibleToMortals { get; set; }

        /// <summary>
        /// The name of this bitvector.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Print an area's flags as a string.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static string AreaString(Area area)
        {
            string text = String.Empty;
            int count;

            for (count = 0; count < AreaFlags.Length; count++)
            {
                if (area.HasFlag(AreaFlags[count]._bitvector))
                {
                    text += " ";
                    text += AreaFlags[count]._name;
                }
            }

            return (!String.IsNullOrEmpty(text)) ? text.Substring(1) : "none";
        }

        /// <summary>
        /// Prints affect flags as a string.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="mortal"></param>
        /// <returns></returns>
        public static string AffectString(int[] vector, bool mortal)
        {
            string text = String.Empty;

            foreach(BitvectorFlagType bvt in BitvectorFlagType.AffectFlags )
            {
                if ((vector[bvt.BitvectorData.Group] & bvt.BitvectorData.Vector) != 0)
                {
                    if (mortal && bvt.VisibleToMortals == false)
                        continue;
                    text += " ";
                    text += bvt.Name;
                }
            }

            return (!String.IsNullOrEmpty(text)) ? text.Substring(1) : "none";
        }
    }
}
