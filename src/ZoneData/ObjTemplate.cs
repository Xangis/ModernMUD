using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ModernMUD
{
    /// <summary>
    /// Prototype for an object.  Object indexes are never created in-game themselves,
    /// but are used as templates for the creation of objects.  They are parent data,
    /// but the data of an object can vary from its template, typically due to enchantments
    /// and various modifiers.  That is why it is not a parent class.
    /// 
    /// Remember that if you change anything here you will have to modify the
    /// Database.CreateObject function to give a value to the object instance.
    /// 
    /// Many of the constants and flags for an object template are derived from the Basternae
    /// 2 area format.
    /// </summary>
    [Serializable]
    public class ObjTemplate
    {
        private static int _numObjIndex;
        [XmlIgnore]
        private Area _area;
        private List<ExtendedDescription> _extraDescriptions = new List<ExtendedDescription>();
        private List<Affect> _affected = new List<Affect>();
        private Trap _trap;
        public delegate bool ObjFun(System.Object obj, System.Object keeper, bool hit);
        private List<ObjSpecial> _specFun = new List<ObjSpecial>();
        private List<SpellEntry> _spellEffects = new List<SpellEntry>();
        private string _specFunName;
        private string _name;
        private string _shortDescription;
        private string _fullDescription;
        private int _indexNumber;
        private ObjectType _itemType;
        private int[] _extraFlags = new int[Limits.NUM_ITEM_EXTRA_VECTORS];
        private int[] _affectedBy = new int[Limits.NUM_AFFECT_VECTORS];
        private int[] _wearFlags = new int[Limits.NUM_WEAR_FLAGS_VECTORS];
        private int[] _antiFlags = new int[Limits.NUM_USE_FLAGS_VECTORS];
        private Material.MaterialType _material;
        private Race.Size _size;
        private int _volume;
        private Craftsmanship _craftsmanshipLevel;
        private int _scarcity; // Common-ness of the item in hundredths of a percent.  10,000 is "normal".
        private int _count;
        private int _weight;
        private int _cost;			/* Unused */
        private int _level;
        private int _condition;
        private int[] _values = new int[8];
        private int _maxNumber;
        private List<CustomAction> _customActions;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ObjTemplate()
        {
            ++_numObjIndex;
            _name = "no name";
            _shortDescription = "(no short description)";
            _fullDescription = "(no description)";
            _indexNumber = 0;
            _count = 0;
            _weight = 1;
            _cost = 0;
            _material = 0;
            _size = Race.Size.medium;
            _volume = 1;
            _craftsmanshipLevel = Craftsmanship.average;
            _level = 1;
            _condition = 100;
            _scarcity = 10000;
            _maxNumber = 0;
            _itemType = ObjectType.trash;
            int count;
            for (count = 0; count < Limits.NUM_ITEM_EXTRA_VECTORS; ++count)
            {
                _extraFlags[count] = 0;
            }
            for (count = 0; count < Limits.NUM_USE_FLAGS_VECTORS; ++count)
            {
                _antiFlags[count] = 0;
            }
            _antiFlags[0] = 1; // Usable by all by default.
            for (count = 0; count < Limits.NUM_WEAR_FLAGS_VECTORS; ++count)
            {
                _wearFlags[count] = 0;
            }
            _wearFlags[0] = 1; // Carryable by default.
            for (count = 0; count < Limits.NUM_AFFECT_VECTORS; ++count)
            {
                _affectedBy[count] = 0;
            }
            for (count = 0; count < 8; ++count)
            {
                _values[count] = 0;
            }
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="obj"></param>
        public ObjTemplate(ObjTemplate obj)
        {
            this._affected = new List<Affect>(obj._affected);
            this._affectedBy = new int[obj._affectedBy.Length];
            obj._affectedBy.CopyTo(_affectedBy, 0);
            this._antiFlags = new int[obj._antiFlags.Length];
            obj._antiFlags.CopyTo(_antiFlags, 0);
            this._area = obj._area;
            this._condition = obj._condition;
            this._cost = obj._cost;
            this._count = 0;
            this._craftsmanshipLevel = obj._craftsmanshipLevel;
            this._customActions = new List<CustomAction>(obj._customActions);
            this._extraDescriptions = new List<ExtendedDescription>(obj._extraDescriptions);
            this._extraFlags = new int[obj._extraFlags.Length];
            obj._extraFlags.CopyTo(_extraFlags, 0);
            this._fullDescription = obj._fullDescription;
            this._indexNumber = obj._area.HighObjIndexNumber + 1;
            this._itemType = obj._itemType;
            this._level = obj._level;
            this._material = obj._material;
            this._maxNumber = obj._maxNumber;
            this._name = obj._name;
            this._scarcity = obj._scarcity;
            this._shortDescription = obj._shortDescription;
            this._size = obj._size;
            this._specFun = new List<ObjSpecial>(obj._specFun);
            this._specFunName = obj._specFunName;
            this._spellEffects = new List<SpellEntry>(obj._spellEffects);
            this._trap = obj._trap;
            this._values = new int[obj._values.Length];
            obj._values.CopyTo(_values, 0);
            this._volume = obj._volume;
            this._wearFlags = new int[obj._wearFlags.Length];
            obj._wearFlags.CopyTo(_wearFlags, 0);
            this._weight = obj._weight;
            ++_numObjIndex;
        }

        /// <summary>
        /// Destructor.  Decrements the in-memory object count.
        /// </summary>
        ~ObjTemplate()
        {
            _shortDescription = String.Empty;
            _fullDescription = String.Empty;
            --_numObjIndex;
            return;
        }

        /// <summary>
        /// Allows us to check for null by using if(obj).
        /// </summary>
        /// <param name="oi"></param>
        /// <returns></returns>
        public static implicit operator bool( ObjTemplate oi )
        {
            if (oi == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Allows us to check for null by using the NOT operator.
        /// </summary>
        /// <param name="oi"></param>
        /// <returns></returns>
        public static bool operator !( ObjTemplate oi )
        {
            if (oi == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Extra descriptions attached to the object.
        /// </summary>
        public List<ExtendedDescription> ExtraDescriptions
        {
            get { return _extraDescriptions; }
            set { _extraDescriptions = value; }
        }

        /// <summary>
        /// Custom actions.  Used for special functions.
        /// </summary>
        public List<CustomAction> CustomActions
        {
            get { return _customActions; }
            set { _customActions = value; }
        }

        /// <summary>
        /// Spell effect entries.  Use for potions, wands, scrolls, rods, and other similar items.
        /// </summary>
        public List<SpellEntry> SpellEffects
        {
            get { return _spellEffects; }
            set { _spellEffects = value; }
        }

        /// <summary>
        /// A trap set on the object.
        /// </summary>
        public Trap Trap
        {
            get { return _trap; }
            set { _trap = value; }
        }

        /// <summary>
        /// The special functions associated wit this object.  Not saved -- generated at runtime from SpecFunName
        /// </summary>
        [XmlIgnore]
        public List<ObjSpecial> SpecFun
        {
            get { return _specFun; }
            set { _specFun = value; }
        }

        /// <summary>
        /// The build quality of the item.
        /// </summary>
        public Craftsmanship CraftsmanshipLevel
        {
            get { return _craftsmanshipLevel; }
            set { _craftsmanshipLevel = value; }
        }

        /// <summary>
        /// The area that this object is associated with.  Not saved -- set at runtime.
        /// </summary>
        [XmlIgnore]
        public Area Area
        {
            get { return _area; }
            set { _area = value; }
        }

        /// <summary>
        /// The virtual number of the item.
        /// </summary>
        public int IndexNumber
        {
            get { return _indexNumber; }
            set { _indexNumber = value; }
        }

        /// <summary>
        /// Extra flags set on the item.
        /// </summary>
        public int[] ExtraFlags
        {
            get { return _extraFlags; }
            set { _extraFlags = value; }
        }

        /// <summary>
        /// The size of the item.
        /// </summary>
        public Race.Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Affect flags set on the object.
        /// </summary>
        public int[] AffectedBy
        {
            get { return _affectedBy; }
            set { _affectedBy = value; }
        }

        /// <summary>
        /// The wear flags set on the object.
        /// </summary>
        public int[] WearFlags
        {
            get { return _wearFlags; }
            set { _wearFlags = value; }
        }

        /// <summary>
        /// The anti flags set on the object.
        /// </summary>
        public int[] UseFlags
        {
            get { return _antiFlags; }
            set { _antiFlags = value; }
        }

        /// <summary>
        /// Number of in-game objects from this template (index number).
        /// </summary>
        [XmlIgnore]
        public int QuantityLoaded
        {
            get { return _count; }
            set { _count = value; }
        }

        /// <summary>
        /// The volume of the object, in cubic feet.  If set at zero, volume is determined based on weight.
        /// </summary>
        public int Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        /// <summary>
        /// The scarcity of the object, with 10,000 being average (100.00% as common as the average item).
        /// </summary>
        public int Scarcity
        {
            get { return _scarcity; }
            set { _scarcity = value; }
        }

        /// <summary>
        /// The weight of the item in pounds.
        /// </summary>
        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        /// <summary>
        /// The cost of the object in copper pieces.
        /// </summary>
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        /// <summary>
        /// The level of the object.
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// The condition of the object as a percent value.
        /// </summary>
        public int Condition
        {
            get { return _condition; }
            set
            {
                _condition = value;
                if (_condition < 0)
                {
                    _condition = 0;
                }
                if (_condition > 100)
                {
                    _condition = 100;
                }
            }
        }

        /// <summary>
        /// The extended attribute value information for the object.
        /// </summary>
        public int[] Values
        {
            get { return _values; }
            set { _values = value; }
        }

        /// <summary>
        /// The maximum number of objects of this type that can be loaded.
        /// </summary>
        public int MaxNumber
        {
            get { return _maxNumber; }
            set { _maxNumber = value; }
        }

        /// <summary>
        /// The affects on this object.
        /// </summary>
        public List<Affect> Affected
        {
            get { return _affected; }
            set { _affected = value; }
        }

        /// <summary>
        /// The name of any special functions attached to the object.  Used to look up and attach special functions.
        /// </summary>
        public string SpecFunName
        {
            get { return _specFunName; }
            set { _specFunName = value; }
        }

        /// <summary>
        /// The name of the object.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The short description of the object.
        /// </summary>
        public string ShortDescription
        {
            get { return _shortDescription; }
            set { _shortDescription = value; }
        }

        /// <summary>
        /// The full description of the object.
        /// </summary>
        public string FullDescription
        {
            get { return _fullDescription; }
            set { _fullDescription = value; }
        }

        /// <summary>
        /// The type of the object.
        /// </summary>
        public ObjectType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        /// <summary>
        /// The material the object is made from.
        /// </summary>
        public Material.MaterialType Material
        {
            get { return _material; }
            set { _material = value; }
        }

        /// <summary>
        /// The number of in-memory instances of this class.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return _numObjIndex;
            }
        }

        /// <summary>
        /// Automatically adds another affect vector if the object does not have enough values in the array.
        /// </summary>
        public void ExtendAffects()
        {
            if (_affectedBy.Length < Limits.NUM_AFFECT_VECTORS)
            {
                int[] oldData = _affectedBy;
                _affectedBy = new int[Limits.NUM_AFFECT_VECTORS];
                for (int i = 0; i < oldData.Length; i++)
                {
                    _affectedBy[i] = oldData[i];
                }
            }
        }

        /// <summary>
        /// Essentially a ToString for craftsmanship.
        /// </summary>
        /// <param name="craft"></param>
        /// <returns></returns>
        public static string CraftsmanshipString(Craftsmanship craft)
        {
            if (craft == Craftsmanship.one_of_a_kind)
                return "one of a kind";
            if (craft == Craftsmanship.master_artisan)
                return "master artisan";
            if (craft == Craftsmanship.gifted_artisan)
                return "gifted artisan";
            if (craft == Craftsmanship.skilled_artisan)
                return "skilled artisan";
            if (craft == Craftsmanship.excellent)
                return "excellent";
            if (craft == Craftsmanship.well_above_average)
                return "well above average";
            if (craft == Craftsmanship.above_average)
                return "above average";
            if (craft == Craftsmanship.slightly_above_average)
                return "slightly above average";
            if (craft == Craftsmanship.average)
                return "average";
            if (craft == Craftsmanship.slightly_below_average)
                return "slightly below average";
            if (craft == Craftsmanship.below_average)
                return "below average";
            if (craft == Craftsmanship.well_below_average)
                return "well below average";
            if (craft == Craftsmanship.fairly_poor)
                return "fairly poor";
            if (craft == Craftsmanship.very_poor)
                return "very poor";
            if (craft == Craftsmanship.extremely_poor)
                return "extremely poor";
            return "terrible";
        }

        /// <summary>
        /// Displays the object as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Routine to automatically price objects based on material type, quality,
        /// and values set on the object.
        /// </summary>
        public void SetCost()
        {
            int ivalue;

            // find the value multiplier based on what the object is made of
            // expensive materials yield expensive objects
            int multiplier = ModernMUD.Material.Table[(int)_material].Value;

            if( _craftsmanshipLevel != 0 )
            {
                multiplier = ( (int)_craftsmanshipLevel * multiplier ) / (int)Craftsmanship.average;
            }

            // Value is in copper pieces for base value
            switch( _itemType )
            {
                default:
                    ivalue = 10;
                    break;
                case ObjectType.shield:
                    ivalue = _values[3] * _values[3] * 6 + 15;
                    break;
                case ObjectType.armor:
                    ivalue = _values[ 0 ] * _values[ 0 ] * 6 + 10;
                    break;
                case ObjectType.weapon:
                    ivalue = ( _values[ 1 ] * _values[ 2 ] * 2 ) * 50;
                    break;
                case ObjectType.staff:
                case ObjectType.scroll:
                case ObjectType.wand:
                case ObjectType.potion:
                case ObjectType.herb:
                    ivalue = 10000;
                    break;
                case ObjectType.lockpick:
                    ivalue = 85 + (_values[0] * 10);
                    break;
                case ObjectType.food:
                    ivalue =  _values[0] + 15;
                    break;
                case ObjectType.light:
                    ivalue = _values[2] + 30;
                    break;
                case ObjectType.rope:
                    ivalue = 100;
                    break;
                case ObjectType.drink_container:
                    ivalue = _values[0] + 25;
                    break;
                case ObjectType.pen:
                    ivalue = 80;
                    break;
                case ObjectType.container:
                    ivalue = _values[0] + 10;
                    break;
                case ObjectType.scabbard:
                    ivalue = 40;
                    break;
                case ObjectType.totem:
                    ivalue = 200;
                    break;
                case ObjectType.spellbook:
                    ivalue = ( 10 * _values[ 2 ] ) + 50;
                    break;
                case ObjectType.quiver:
                    ivalue = 10 + _values[0];
                    break;
                case ObjectType.treasure:
                    ivalue = 100;
                    break;
                case ObjectType.clothing:
                    ivalue = 15;
                    break;
                case ObjectType.trash:
                    ivalue = 1;
                    break;
                case ObjectType.boat:
                    ivalue = 100;
                    break;
                case ObjectType.ship:
                    ivalue = 1000;
                    break;
                case ObjectType.instrument:
                    ivalue = 100;
                    break;
            }

            // 10% bonus for magic items.
            if (((_extraFlags[ObjTemplate.ITEM_MAGIC.Group] & ObjTemplate.ITEM_MAGIC.Vector) != 0))
            {
                ivalue += (ivalue / 10);
            }

            // 2% bonus for blessed items.
            if (((_extraFlags[ObjTemplate.ITEM_BLESS.Group] & ObjTemplate.ITEM_BLESS.Vector) != 0))
            {
                ivalue += (ivalue / 50);
            }

            // 3% bonus for float items.
            if (((_extraFlags[ObjTemplate.ITEM_FLOAT.Group] & ObjTemplate.ITEM_FLOAT.Vector) != 0))
            {
                ivalue += (ivalue / 33);
            }
            
            // 5% bonus for noburn items.
            if (((_extraFlags[ObjTemplate.ITEM_NOBURN.Group] & ObjTemplate.ITEM_NOBURN.Vector) != 0))
            {
                ivalue += (ivalue / 20);
            }

            // 6% bonus for lit items.
            if (((_extraFlags[ObjTemplate.ITEM_LIT.Group] & ObjTemplate.ITEM_LIT.Vector) != 0))
            {
                ivalue += (ivalue / 16);
            }
            
            // 33% penalty for norepair items.
            if (((_extraFlags[ObjTemplate.ITEM_NO_REPAIR.Group] & ObjTemplate.ITEM_NO_REPAIR.Vector) != 0))
            {
                ivalue += (ivalue / 3);
            }

            // 1% bonus for glowing items.
            if (((_extraFlags[ObjTemplate.ITEM_GLOW.Group] & ObjTemplate.ITEM_GLOW.Vector) != 0))
            {
                ivalue += (ivalue / 100);
            }

            // Just to make it _slightly_ less monotonous
            multiplier = (new Random()).Next((multiplier -1), (multiplier+1));

            _cost = ( ( ivalue * multiplier ) / 100 );

            if (_condition != 0)
            {
                _cost = (_cost * _condition) / 100;
            }

            // Last stage, multiply it by the scarcity (which is denoted in hundredths of a percent).
            //
            // Items that are super-common, such as a scarcity of 5000, will be worth 50% as much as
            // they normally would, while rarer items, such as a scarcity of 15000, will be worth
            // 150% of normal.
            //
            _cost = ( _cost * _scarcity ) / 10000;

            if( _cost == 0 )
            {
                _cost = 1;
            }
        }

        // Item flags.
        public static readonly Bitvector ITEM_NONE = new Bitvector(0, 0);
        public static readonly Bitvector ITEM_GLOW = new Bitvector(0, Bitvector.BV00);
        public static readonly Bitvector ITEM_NOSHOW = new Bitvector(0, Bitvector.BV01);
        public static readonly Bitvector ITEM_BURIED = new Bitvector(0, Bitvector.BV02);
        public static readonly Bitvector ITEM_NOSELL = new Bitvector(0, Bitvector.BV03);
        public static readonly Bitvector ITEM_EVIL = new Bitvector(0, Bitvector.BV04);
        public static readonly Bitvector ITEM_INVIS = new Bitvector(0, Bitvector.BV05);
        public static readonly Bitvector ITEM_MAGIC = new Bitvector(0, Bitvector.BV06);
        public static readonly Bitvector ITEM_NODROP = new Bitvector(0, Bitvector.BV07);
        public static readonly Bitvector ITEM_BLESS = new Bitvector(0, Bitvector.BV08);
        public static readonly Bitvector ITEM_ANTI_GOOD = new Bitvector(0, Bitvector.BV09);
        public static readonly Bitvector ITEM_ANTI_EVIL = new Bitvector(0, Bitvector.BV10);
        public static readonly Bitvector ITEM_ANTI_NEUTRAL = new Bitvector(0, Bitvector.BV11);
        public static readonly Bitvector ITEM_SECRET = new Bitvector(0, Bitvector.BV12);
        public static readonly Bitvector ITEM_FLOAT = new Bitvector(0, Bitvector.BV13);
        public static readonly Bitvector ITEM_NOBURN = new Bitvector(0, Bitvector.BV14);
        public static readonly Bitvector ITEM_NOLOCATE = new Bitvector(0, Bitvector.BV15);
        public static readonly Bitvector ITEM_NOID = new Bitvector(0, Bitvector.BV16);
        public static readonly Bitvector ITEM_NOSUMMON = new Bitvector(0, Bitvector.BV17);
        public static readonly Bitvector ITEM_LIT = new Bitvector(0, Bitvector.BV18);
        public static readonly Bitvector ITEM_TRANSIENT = new Bitvector(0, Bitvector.BV19);
        public static readonly Bitvector ITEM_NOSLEEP = new Bitvector(0, Bitvector.BV20);
        public static readonly Bitvector ITEM_NOCHARM = new Bitvector(0, Bitvector.BV21);
        public static readonly Bitvector ITEM_TWOHANDED = new Bitvector(0, Bitvector.BV22);
        public static readonly Bitvector ITEM_NORENT = new Bitvector(0, Bitvector.BV23);
        public static readonly Bitvector ITEM_GOODONLY = new Bitvector(0, Bitvector.BV24);
        public static readonly Bitvector ITEM_HUM = new Bitvector(0, Bitvector.BV25);
        public static readonly Bitvector ITEM_LEVITATES = new Bitvector(0, Bitvector.BV26);
        public static readonly Bitvector ITEM_INVENTORY = new Bitvector(0, Bitvector.BV27);
        public static readonly Bitvector ITEM_WAS_DISARMED = new Bitvector(0, Bitvector.BV28);
        public static readonly Bitvector ITEM_WHOLEBODY = new Bitvector(0, Bitvector.BV29);
        public static readonly Bitvector ITEM_WHOLEHEAD = new Bitvector(0, Bitvector.BV30);
        public static readonly Bitvector ITEM_SILVER = new Bitvector(1, Bitvector.BV00);
        public static readonly Bitvector ITEM_THROW_RETURN = new Bitvector(1, Bitvector.BV06);
        public static readonly Bitvector ITEM_THROW_ONEROOM = new Bitvector(1, Bitvector.BV07);
        public static readonly Bitvector ITEM_THROW_TWOROOMS = new Bitvector(1, Bitvector.BV08);
        public static readonly Bitvector ITEM_ARTIFACT = new Bitvector(1, Bitvector.BV09);
        public static readonly Bitvector ITEM_ENLARGED = new Bitvector(1, Bitvector.BV16);
        public static readonly Bitvector ITEM_REDUCED = new Bitvector(1, Bitvector.BV17);
        public static readonly Bitvector ITEM_POISONED = new Bitvector(1, Bitvector.BV18);
        public static readonly Bitvector ITEM_NO_REPAIR = new Bitvector(1, Bitvector.BV19);
        public static readonly Bitvector ITEM_UNIQUE = new Bitvector(1, Bitvector.BV20);
        public static readonly Bitvector ITEM_FLY = new Bitvector(1, Bitvector.BV21);

        /// <summary>
        /// Translates wear locations into top-down viewing order.
        /// </summary>
        public static readonly WearLocation[] TopDownEquipment = new WearLocation[]
        {
            WearLocation.horns, WearLocation.badge, WearLocation.nose,
            WearLocation.head, WearLocation.eyes, WearLocation.ear_left,
            WearLocation.ear_right, WearLocation.face, WearLocation.neck_one,
            WearLocation.neck_two, WearLocation.body, WearLocation.on_back,
            WearLocation.quiver, WearLocation.about_body, WearLocation.waist,
            WearLocation.tail, WearLocation.belt_attach_one, WearLocation.belt_attach_two,
            WearLocation.belt_attach_three, WearLocation.horse_body, WearLocation.arms,
            WearLocation.arms_lower, WearLocation.wrist_left, WearLocation.wrist_right,
            WearLocation.wrist_lower_left, WearLocation.wrist_lower_right, WearLocation.hands,
            WearLocation.hands_lower, WearLocation.finger_left, WearLocation.finger_right,
            WearLocation.hand_one, WearLocation.hand_two, WearLocation.hand_three,
            WearLocation.hand_four, WearLocation.legs, WearLocation.feet,
            WearLocation.none
        };     
   
        // Wear flags.
        public static readonly Bitvector WEARABLE_CARRY = new Bitvector(0, Bitvector.BV00);
        public static readonly Bitvector WEARABLE_FINGER = new Bitvector(0, Bitvector.BV01);
        public static readonly Bitvector WEARABLE_NECK = new Bitvector(0, Bitvector.BV02);
        public static readonly Bitvector WEARABLE_BODY = new Bitvector(0, Bitvector.BV03);
        public static readonly Bitvector WEARABLE_HEAD = new Bitvector(0, Bitvector.BV04);
        public static readonly Bitvector WEARABLE_LEGS = new Bitvector(0, Bitvector.BV05);
        public static readonly Bitvector WEARABLE_FEET = new Bitvector(0, Bitvector.BV06);
        public static readonly Bitvector WEARABLE_HANDS = new Bitvector(0, Bitvector.BV07);
        public static readonly Bitvector WEARABLE_ARMS = new Bitvector(0, Bitvector.BV08);
        public static readonly Bitvector WEARABLE_SHIELD = new Bitvector(0, Bitvector.BV09);
        public static readonly Bitvector WEARABLE_ABOUT = new Bitvector(0, Bitvector.BV10);
        public static readonly Bitvector WEARABLE_WAIST = new Bitvector(0, Bitvector.BV11);
        public static readonly Bitvector WEARABLE_WRIST = new Bitvector(0, Bitvector.BV12);
        public static readonly Bitvector WEARABLE_WIELD = new Bitvector(0, Bitvector.BV13);
        public static readonly Bitvector WEARABLE_HOLD = new Bitvector(0, Bitvector.BV14);
        // 15 and 16 not used
        public static readonly Bitvector WEARABLE_EYES = new Bitvector(0, Bitvector.BV17);
        public static readonly Bitvector WEARABLE_FACE = new Bitvector(0, Bitvector.BV18);
        public static readonly Bitvector WEARABLE_EAR = new Bitvector(0, Bitvector.BV19);
        public static readonly Bitvector WEARABLE_QUIVER = new Bitvector(0, Bitvector.BV20);
        public static readonly Bitvector WEARABLE_BADGE = new Bitvector(0, Bitvector.BV21);
        public static readonly Bitvector WEARABLE_ONBACK = new Bitvector(0, Bitvector.BV22);
        public static readonly Bitvector WEARABLE_ATTACH_BELT = new Bitvector(0, Bitvector.BV23);
        public static readonly Bitvector WEARABLE_HORSE_BODY = new Bitvector(0, Bitvector.BV24);
        public static readonly Bitvector WEARABLE_TAIL = new Bitvector(0, Bitvector.BV25);
        public static readonly Bitvector WEARABLE_NOSE = new Bitvector(0, Bitvector.BV26);
        public static readonly Bitvector WEARABLE_HORNS = new Bitvector(0, Bitvector.BV27);
        
        // Anti flags.
        public static readonly Bitvector USE_ANYONE = new Bitvector(0, Bitvector.BV00);
        public static readonly Bitvector USE_WARRIOR = new Bitvector(0, Bitvector.BV01);
        public static readonly Bitvector USE_RANGER = new Bitvector(0, Bitvector.BV02);
        public static readonly Bitvector USE_PALADIN = new Bitvector(0, Bitvector.BV03);
        public static readonly Bitvector USE_ANTI = new Bitvector(0, Bitvector.BV04);
        public static readonly Bitvector USE_CLERIC = new Bitvector(0, Bitvector.BV05);
        public static readonly Bitvector USE_MONK = new Bitvector(0, Bitvector.BV06); // For later use
        public static readonly Bitvector USE_DRUID = new Bitvector(0, Bitvector.BV07);
        public static readonly Bitvector USE_SHAMAN = new Bitvector(0, Bitvector.BV08);
        public static readonly Bitvector USE_SORCERER = new Bitvector(0, Bitvector.BV09);
        public static readonly Bitvector USE_NECRO = new Bitvector(0, Bitvector.BV10);// For later use
        public static readonly Bitvector USE_ELEMENTAL = new Bitvector(0, Bitvector.BV11);
        public static readonly Bitvector USE_PSI = new Bitvector(0, Bitvector.BV12);
        public static readonly Bitvector USE_THIEF = new Bitvector(0, Bitvector.BV13); // For later use
        public static readonly Bitvector USE_ASSASSIN = new Bitvector(0, Bitvector.BV14);
        public static readonly Bitvector USE_MERCENARY = new Bitvector(0, Bitvector.BV15);
        public static readonly Bitvector USE_BARD = new Bitvector(0, Bitvector.BV16); // For later use
        public static readonly Bitvector USE_NOHUMAN = new Bitvector(0, Bitvector.BV17);
        public static readonly Bitvector USE_NOGREYELF = new Bitvector(0, Bitvector.BV18);
        public static readonly Bitvector USE_NOHALFELF = new Bitvector(0, Bitvector.BV19);
        public static readonly Bitvector USE_NODWARF = new Bitvector(0, Bitvector.BV20);
        public static readonly Bitvector USE_NOHALFLING = new Bitvector(0, Bitvector.BV21);
        public static readonly Bitvector USE_NOGNOME = new Bitvector(0, Bitvector.BV22);
        public static readonly Bitvector USE_NOBARBARIAN = new Bitvector(0, Bitvector.BV23);
        public static readonly Bitvector USE_NODUERGAR = new Bitvector(0, Bitvector.BV24);
        public static readonly Bitvector USE_NODROW = new Bitvector(0, Bitvector.BV25);
        public static readonly Bitvector USE_NOTROLL = new Bitvector(0, Bitvector.BV26);
        public static readonly Bitvector USE_NOOGRE = new Bitvector(0, Bitvector.BV27);
        public static readonly Bitvector USE_ANTIGOODRACE = new Bitvector(0, Bitvector.BV28);
        public static readonly Bitvector USE_NOORC = new Bitvector(0, Bitvector.BV29);
        public static readonly Bitvector USE_ANTIEVILRACE = new Bitvector(0, Bitvector.BV30);
        // Anti2 Flags
        public static readonly Bitvector USE_NOTHRIKREEN = new Bitvector(1, Bitvector.BV00);
        public static readonly Bitvector USE_NOCENTAUR = new Bitvector(1, Bitvector.BV01);
        public static readonly Bitvector USE_NOGITHYANKI = new Bitvector(1, Bitvector.BV02);
        public static readonly Bitvector USE_NOMINOTAUR = new Bitvector(1, Bitvector.BV03);
        public static readonly Bitvector USE_ANTIMALE = new Bitvector(1, Bitvector.BV04);
        public static readonly Bitvector USE_ANTIFEMALE = new Bitvector(1, Bitvector.BV05);
        public static readonly Bitvector USE_ANTINEUTER = new Bitvector(1, Bitvector.BV06);
        public static readonly Bitvector USE_NOAQUAELF = new Bitvector(1, Bitvector.BV07);
        public static readonly Bitvector USE_NOSAHAUGIN = new Bitvector(1, Bitvector.BV08);
        public static readonly Bitvector USE_NOGOBLIN = new Bitvector(1, Bitvector.BV09);
        public static readonly Bitvector USE_NORAKSHASA = new Bitvector(1, Bitvector.BV10);
        public static readonly Bitvector USE_NOGNOLL = new Bitvector(1, Bitvector.BV11);
        public static readonly Bitvector USE_NOGITHZERAI = new Bitvector(1, Bitvector.BV12);
        public static readonly Bitvector USE_NODUAL = new Bitvector(1, Bitvector.BV13);

        /// <summary>
        /// Craftsmanship values.
        /// </summary>
        public enum Craftsmanship
        {
            terrible = 0,
            extremely_poor,
            very_poor,
            fairly_poor,
            well_below_average,
            below_average,
            slightly_below_average,
            average,
            slightly_above_average,
            above_average,
            well_above_average,
            excellent,
            skilled_artisan,
            gifted_artisan,
            master_artisan,
            one_of_a_kind
        }
        public static readonly int MAX_CRAFT = Enum.GetValues(typeof(Craftsmanship)).Length;

        /// <summary>
        /// Melee weapon types.
        /// </summary>
        public enum WeaponType
        {
            unknown = 0,
            axe,
            dagger,
            flail,
            hammer,
            longsword,
            mace,
            spiked_mace,
            polearm,
            shortsword,
            club, // 10
            spiked_club,
            staff,
            two_handed_sword,
            whip,
            pick,
            lance,
            sickle,
            fork,
            horn,
            nunchaku, // 20
            spear,
            battle_axe,
            katana,
            bastard_sword,
            morningstar,
            rapier,
            scimitar,
            sabre,
            cutlass,
            warhammer, // 30
            machete,
            claymore,
            khopesh,
            flamberge,
            great_axe,
            halberd,
            glaive,
            falchion,
            naginata,
            pike, // 40
            wakizashi,
            scythe,
            parrying_dagger,
            knife,
            dirk,
            kris,
            claw,
            fang,
            gythka,
            chatkcha, // 50
            scepter,
            quarterstaff,
            stiletto,
            trident
        }

        /// <summary>
        /// Available object types.
        /// </summary>
        public enum ObjectType
        {
            none = 0,
            ammunition,
            armor,
            battery,
            boat,
            clothing,
            container,
            drink_container,
            food,
            herb,
            instrument,
            key,
            light,
            lockpick,
            message_board,
            missile_weapon,
            money,
            note,
            npc_corpse,
            other,
            pc_corpse,
            pen,
            pill,
            pipe,
            portal,
            potion,
            quiver,
            ranged_weapon,
            rope,
            scabbard,
            scroll,
            shield,
            ship,
            spellbook,
            staff,
            storage_chest,
            switch_trigger,
            teleport,
            timer,
            totem,
            tradition_icon,
            trap,
            trash,
            treasure,
            vehicle,
            wall,
            wand,
            wanted_list,
            weapon
        }
        public static readonly int MAX_ITEM_TYPE = Enum.GetValues(typeof(ObjectType)).Length;


        /// <summary>
        /// Returns ASCII _name of an item type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ItemTypeString(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.none:
                    return "none";
                case ObjectType.light:
                    return "light";
                case ObjectType.scroll:
                    return "scroll";
                case ObjectType.wand:
                    return "wand";
                case ObjectType.staff:
                    return "staff";
                case ObjectType.weapon:
                    return "weapon";
                case ObjectType.ranged_weapon:
                    return "ranged weapon";
                case ObjectType.missile_weapon:
                    return "missile weapon";
                case ObjectType.treasure:
                    return "treasure";
                case ObjectType.armor:
                    return "armor";
                case ObjectType.potion:
                    return "potion";
                case ObjectType.clothing:
                    return "clothing";
                case ObjectType.other:
                    return "other";
                case ObjectType.trash:
                    return "trash";
                case ObjectType.trap:
                    return "trap";
                case ObjectType.container:
                    return "container";
                case ObjectType.note:
                    return "note";
                case ObjectType.drink_container:
                    return "drink container";
                case ObjectType.key:
                    return "key";
                case ObjectType.food:
                    return "food";
                case ObjectType.money:
                    return "money";
                case ObjectType.pen:
                    return "pen";
                case ObjectType.boat:
                    return "boat";
                case ObjectType.battery:
                    return "battery";
                case ObjectType.portal:
                    return "portal";
                case ObjectType.timer:
                    return "timer";
                case ObjectType.vehicle:
                    return "vehicle";
                case ObjectType.ship:
                    return "ship";
                case ObjectType.switch_trigger:
                    return "switch";
                case ObjectType.quiver:
                    return "quiver";
                case ObjectType.lockpick:
                    return "lockpick";
                case ObjectType.instrument:
                    return "instrument";
                case ObjectType.spellbook:
                    return "spellbook";
                case ObjectType.totem:
                    return "totem";
                case ObjectType.storage_chest:
                    return "storage chest";
                case ObjectType.scabbard:
                    return "scabbard";
                case ObjectType.shield:
                    return "shield";
                case ObjectType.npc_corpse:
                    return "npc corpse";
                case ObjectType.pc_corpse:
                    return "pc corpse";
                case ObjectType.pill:
                    return "pill";
                case ObjectType.message_board:
                    return "message board";
                case ObjectType.wanted_list:
                    return "wanted list";
                case ObjectType.wall:
                    return "wall";
                case ObjectType.ammunition:
                    return "ammo";
            }
            return String.Empty;
        }


        /// <summary>
        /// Gets an item type based on a string.
        /// </summary>
        /// <param name="text">The _name of the type to match.</param>
        /// <returns>The item type, or ObjIndex.ObjectType.none (0) if not matched.</returns>
        public static ObjectType ItemTypeLookup(string text)
        {
            switch (text)
            {
                case "light":
                    return ObjectType.light;
                case "scroll":
                    return ObjectType.scroll;
                case "wand":
                    return ObjectType.wand;
                case "staff":
                    return ObjectType.staff;
                case "weapon":
                    return ObjectType.weapon;
                case "ranged weapon":
                    return ObjectType.ranged_weapon;
                case "missile weapon":
                    return ObjectType.missile_weapon;
                case "treasure":
                    return ObjectType.treasure;
                case "armor":
                    return ObjectType.armor;
                case "potion":
                    return ObjectType.potion;
                case "clothing":
                    return ObjectType.clothing;
                case "other":
                    return ObjectType.other;
                case "trash":
                    return ObjectType.trash;
                case "trap":
                    return ObjectType.trap;
                case "container":
                    return ObjectType.container;
                case "note":
                    return ObjectType.note;
                case "drink container":
                    return ObjectType.drink_container;
                case "key":
                    return ObjectType.key;
                case "food":
                    return ObjectType.food;
                case "money":
                    return ObjectType.money;
                case "pen":
                    return ObjectType.pen;
                case "boat":
                    return ObjectType.boat;
                case "battery":
                    return ObjectType.battery;
                case "portal":
                    return ObjectType.portal;
                case "timer":
                    return ObjectType.timer;
                case "vehicle":
                    return ObjectType.vehicle;
                case "ship":
                    return ObjectType.ship;
                case "switch":
                    return ObjectType.switch_trigger;
                case "quiver":
                    return ObjectType.quiver;
                case "lockpick":
                    return ObjectType.lockpick;
                case "instrument":
                    return ObjectType.instrument;
                case "spellbook":
                    return ObjectType.spellbook;
                case "totem":
                    return ObjectType.totem;
                case "storage chest":
                    return ObjectType.storage_chest;
                case "scabbard":
                    return ObjectType.scabbard;
                case "shield":
                    return ObjectType.shield;
                case "npc corpse":
                    return ObjectType.npc_corpse;
                case "pc corpse":
                    return ObjectType.pc_corpse;
                case "pill":
                    return ObjectType.pill;
                case "message board":
                    return ObjectType.message_board;
                case "wanted list":
                    return ObjectType.wanted_list;
                case "wall":
                    return ObjectType.wall;
                case "ammo":
                    return ObjectType.ammunition;
                case "none":
                    return ObjectType.none;
            }
            return ObjectType.none;
        }

        // Container flags.
        public static readonly Bitvector CONTAINER_NONE = new Bitvector(0, 0);
        public static readonly Bitvector CONTAINER_CLOSEABLE = new Bitvector(0, Bitvector.BV00);
        public static readonly Bitvector CONTAINER_HARDTOPICK = new Bitvector(0, Bitvector.BV01);
        public static readonly Bitvector CONTAINER_CLOSED = new Bitvector(0, Bitvector.BV02);
        public static readonly Bitvector CONTAINER_LOCKED = new Bitvector(0, Bitvector.BV03);
        public static readonly Bitvector CONTAINER_PICKPROOF = new Bitvector(0, Bitvector.BV04);

        // Portal flags.
        public const int PORTAL_NO_CURSED = Bitvector.BV00;
        public const int PORTAL_GO_WITH = Bitvector.BV01;
        public const int PORTAL_RANDOM = Bitvector.BV02;
        public const int PORTAL_BUGGY = Bitvector.BV03;
        public const int PORTAL_CLOSEABLE = Bitvector.BV04;
        public const int PORTAL_PICKPROOF = Bitvector.BV05;
        public const int PORTAL_CLOSED = Bitvector.BV06;
        public const int PORTAL_LOCKED = Bitvector.BV07;
        // Portal Values:
        // [0] - Destination room
        // [1] - Trigger command
        // [2] - Number of charges
        // [3] - Portal Flags
        // [4] - Key #, 0 for none

        // Ranged weapon types.
        public const int RNG_BOW = 0;
        public const int RNG_CROSSBOW = 1;
        public const int RNG_CATAPULT = 2;

        // Totem types
        public static readonly Bitvector TOTEM_L_ANIMAL = new Bitvector(0, Bitvector.BV00);
        public static readonly Bitvector TOTEM_G_ANIMAL = new Bitvector(0, Bitvector.BV01);
        public static readonly Bitvector TOTEM_L_ELEMENTAL = new Bitvector(0, Bitvector.BV02);
        public static readonly Bitvector TOTEM_G_ELEMENTAL = new Bitvector(0, Bitvector.BV03);
        public static readonly Bitvector TOTEM_L_SPIRIT = new Bitvector(0, Bitvector.BV04);
        public static readonly Bitvector TOTEM_G_SPIRIT = new Bitvector(0, Bitvector.BV05);

        /// <summary>
        /// Wear locations for items.  Only one item can be equipped in each location.
        /// </summary>
        public enum WearLocation
        {
            none,
            finger_left,
            finger_right,
            neck_one,
            neck_two,
            body,
            head,
            legs,
            feet,
            hands,
            arms,
            about_body,
            waist,
            wrist_left,
            wrist_right,
            hand_one,
            hand_two,
            eyes,
            face,
            ear_left,
            ear_right,
            badge,
            on_back,
            belt_attach_one,
            belt_attach_two,
            belt_attach_three,
            quiver,
            tail,
            horse_body,
            horns,
            nose,
            hand_three,
            hand_four,
            arms_lower,
            hands_lower,
            wrist_lower_left,
            wrist_lower_right
        };
    }
}