
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// An in-game object or item. Unfortunate naming given that we're in .NET.
    /// </summary>
    [Serializable]
    public class Object
    {
        private static int _numObjects;
        private List<Object> _contains = new List<Object>();
        private Object _inObject;
        private List<ObjSpecial> _specFun = new List<ObjSpecial>();
        private string _specFunName;
        private CharData _carriedBy;
        private CharData _createdBy;
        private List<ExtendedDescription> _extraDescription = new List<ExtendedDescription>();
        private List<Affect> _affected = new List<Affect>();
        private ObjTemplate _objIndexData;
        private int _objIndexNumber;
        private Room _inRoom;
        private Trap _trap;
        private string _name;
        private string _shortDescription;
        private string _fullDescription;
        private ObjTemplate.ObjectType _itemType;
        private int[] _extraFlags = new int[ Limits.NUM_ITEM_EXTRA_VECTORS ];
        private int[] _affectedBy = new int[ Limits.NUM_AFFECT_VECTORS ];
        private int[] _wearFlags = new int[ Limits.NUM_WEAR_FLAGS_VECTORS ];
        private int[] _antiFlags = new int[ Limits.NUM_USE_FLAGS_VECTORS ];
        private Material.MaterialType _material;
        private Race.Size _size;
        private int _volume;
        private ObjTemplate.Craftsmanship _craftsmanship;
        private ObjTemplate.WearLocation _wearLocation;
        private int _weight;
        private int _cost;
        private int _level;
        private int _timer;
        private int[] _values = new int[ 8 ];
        private int _condition;
        private CharData.FlyLevel _flyLevel;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Object()
        {
            ++_numObjects;
            _createdBy = null;
            _itemType = ObjTemplate.ObjectType.trash;
            _material = 0;
            _size = 0;
            _volume = 0;
            _craftsmanship = 0;
            _wearLocation = 0;
            _weight = 0;
            _cost = 0;
            _level = 1;
            _timer = -1;
            _condition = 100;
            _flyLevel = CharData.FlyLevel.ground;
            int count;
            for( count = 0; count < Limits.NUM_ITEM_EXTRA_VECTORS; ++count )
                _extraFlags[ count ] = 0;
            for (count = 0; count < Limits.NUM_USE_FLAGS_VECTORS; ++count)
                _antiFlags[count] = 0;
            for (count = 0; count < Limits.NUM_WEAR_FLAGS_VECTORS; ++count)
                _wearFlags[count] = 0;
            for (count = 0; count < Limits.NUM_AFFECT_VECTORS; ++count)
                _affectedBy[ count ] = 0;
            for( count = 0; count < 8; ++count )
                _values[ count ] = 0;
        }

        /// <summary>
        /// Creates an object and initializes it to the parent object values.
        /// </summary>
        /// <param name="indexData"></param>
        public Object( ObjTemplate indexData )
        {
            if( indexData == null )
            {
                Log.Error( "Object.Object(ObjIndex *) called with null ObjIndex.", 0 );
                return;
            }

            ++_numObjects;
            _objIndexNumber = indexData.IndexNumber;
            _objIndexData = indexData;
            _wearLocation = ObjTemplate.WearLocation.none;
            _flyLevel = 0;
            _level = 1;
            _timer = -1;
            _createdBy = null;
            _extraDescription = indexData.ExtraDescriptions;
            _name = indexData.Name;
            _shortDescription = indexData.ShortDescription;
            _fullDescription = indexData.FullDescription;
            _specFun = indexData.SpecFun;
            _itemType = indexData.ItemType;
            int count;
            for( count = 0; count < Limits.NUM_ITEM_EXTRA_VECTORS; ++count )
                _extraFlags[ count ] = indexData.ExtraFlags[ count ];
            for( count = 0; count < Limits.NUM_AFFECT_VECTORS; ++count )
            {
                _affectedBy[ count ] = indexData.AffectedBy[ count ];
            }
            _wearFlags = indexData.WearFlags;
            _antiFlags = indexData.UseFlags;
            _material = indexData.Material;
            _size = indexData.Size;
            _volume = indexData.Volume;
            _craftsmanship = indexData.CraftsmanshipLevel;
            for( count = 0; count < 8; ++count )
                _values[ count ] = indexData.Values[ count ];
            _weight = indexData.Weight;
            _cost = indexData.Cost;
            _condition = indexData.Condition;
            _trap = indexData.Trap;

            // Create vehicle data for vehicles that are created.  The
            // bulk of the data is stored in the object - Xangis
            if( _itemType == ObjTemplate.ObjectType.vehicle || _itemType == ObjTemplate.ObjectType.ship )
            {
                Vehicle vehicle = new Vehicle();
                if( _itemType == ObjTemplate.ObjectType.ship )
                    vehicle.Type = Vehicle.VehicleType.ship_any_water;
                else
                    vehicle.Type = Vehicle.VehicleType.flat_land;
                // need to create virtual rooms for the rest of the data
                vehicle.HullPoints = _values[ 5 ];
                vehicle.FlyLevel = 0;
                vehicle.Direction = 0;
                vehicle.Speed = 0;
                vehicle.Occupants = 0;
                vehicle.MovementTimer = 0;
                vehicle.MovementDelay = 0;
                vehicle.MovementPointer = 0;
                vehicle.MovementScript = String.Empty;
                vehicle.ParentObject = this;
                vehicle.EntryRoomTemplateNumber = _values[ 1 ];
                vehicle.ControlPanelRoomTemplateNumber = _values[ 2 ];
            }

            ++indexData.QuantityLoaded;
            --indexData.Scarcity;

            Database.ObjectList.Add( this );

            return;
        }

        /// <summary>
        /// Destructor.  Decrements counters and clears data members.
        /// </summary>
        ~Object()
        {
            if( _objIndexData != null )
            {
                --_objIndexData.QuantityLoaded;
                ++_objIndexData.Scarcity;
            }
            _name = String.Empty;
            _shortDescription = String.Empty;
            _fullDescription = String.Empty;
            // Remove from object list.
            for(int i = Database.ObjectList.Count - 1; i >= 0; i-- )
            {
                if( Database.ObjectList[i] == this )
                {
                    Database.ObjectList.RemoveAt( i );
                }
            }
            // Delete affects and extra descriptions.
            _extraDescription.Clear();
            _affected.Clear();
            --_numObjects;
        }

        /// <summary>
        /// Gets the number of objects currently in game.
        /// </summary>
        public static int Count
        {
            get
            {
                return _numObjects;
            }
        }

        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }

        public int Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public ObjTemplate.WearLocation WearLocation
        {
            get { return _wearLocation; }
            set { _wearLocation = value; }
        }

        public ObjTemplate.Craftsmanship Craftsmanship
        {
            get { return _craftsmanship; }
            set { _craftsmanship = value; }
        }

        public int[] Values
        {
            get { return _values; }
            set { _values = value; }
        }

        public CharData.FlyLevel FlyLevel
        {
            get { return _flyLevel; }
            set { _flyLevel = value; }
        }

        public int Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        public Race.Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public Material.MaterialType Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public int[] UseFlags
        {
            get { return _antiFlags; }
            set { _antiFlags = value; }
        }

        public int[] WearFlags
        {
            get { return _wearFlags; }
            set { _wearFlags = value; }
        }

        public int[] AffectedBy
        {
            get { return _affectedBy; }
            set { _affectedBy = value; }
        }

        public int[] ExtraFlags
        {
            get { return _extraFlags; }
            set { _extraFlags = value; }
        }

        public ObjTemplate.ObjectType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        public string FullDescription
        {
            get { return _fullDescription; }
            set { _fullDescription = value; }
        }

        public string ShortDescription
        {
            get { return _shortDescription; }
            set { _shortDescription = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [XmlIgnore]
        public Room InRoom
        {
            get { return _inRoom; }
            set { _inRoom = value; }
        }

        public int ObjIndexNumber
        {
            get { return _objIndexNumber; }
            set { _objIndexNumber = value; }
        }

        [XmlIgnore]
        public ObjTemplate ObjIndexData
        {
            get { return _objIndexData; }
            set { _objIndexData = value; }
        }

        public List<Affect> Affected
        {
            get { return _affected; }
            set { _affected = value; }
        }

        public List<ExtendedDescription> ExtraDescription
        {
            get { return _extraDescription; }
            set { _extraDescription = value; }
        }

        public string SpecFunName
        {
            get { return _specFunName; }
            set { _specFunName = value; }
        }

        public List<Object> Contains
        {
            get { return _contains; }
            set { _contains = value; }
        }

        [XmlIgnore]
        public Object InObject
        {
            get { return _inObject; }
            set { _inObject = value; }
        }

        [XmlIgnore]
        public CharData CarriedBy
        {
            get { return _carriedBy; }
            set { _carriedBy = value; }
        }

        [XmlIgnore]
        public CharData CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        [XmlIgnore]
        public List<ObjSpecial> SpecFun
        {
            get { return _specFun; }
            set { _specFun = value; }
        }

        public Trap Trap
        {
            get { return _trap; }
            set { _trap = value; }
        }

        /// <summary>
        /// Displays the object name as string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _shortDescription;
        }

        /// <summary>
        /// Checks an object for a specific extra flag.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool HasFlag( Bitvector bvect )
        {
            if( Macros.IsSet( _extraFlags[ bvect.Group ], bvect.Vector ) )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks an object for a specific wear flag.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool HasWearFlag(Bitvector bvect)
        {
            if ((_wearFlags.Length - 1) < bvect.Group)
            {
                Log.Error("HasWearFlag: Called on object with too few wear flags. BV: " + bvect + ", Object: " +
                    this, ", NumFlags: " + _wearFlags.Length);
                return false;
            }
            if (Macros.IsSet(_wearFlags[bvect.Group], bvect.Vector))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks an object for a specific anti flag.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool HasAntiFlag(Bitvector bvect)
        {
            if ((_antiFlags.Length - 1) < bvect.Group)
            {
                Log.Error("HasAntiFlag: Called on object with too few anti flags. BV: " + bvect + ", Object: " +
                    this, ", NumFlags: " + _antiFlags.Length);
                return false;
            }
            if (Macros.IsSet(_antiFlags[bvect.Group], bvect.Vector))
            {
                return true;
            }
            return false;
        }

        public void AddFlag( Bitvector bvect )
        {
            Macros.SetBit( ref _extraFlags[ bvect.Group ], bvect.Vector );
            return;
        }

        /// <summary>
        /// Removes a flag from an item.
        /// </summary>
        /// <param name="bvect"></param>
        public void RemoveFlag( Bitvector bvect )
        {
            Macros.RemoveBit( ref _extraFlags[ bvect.Group ], bvect.Vector );
            return;
        }

        /// <summary>
        /// Gets the text representation for all of the flags on an item.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ItemString(Object obj)
        {
            string text = String.Empty;
            int count;

            for (count = 0; count < BitvectorFlagType.ItemFlags.Length; count++)
            {
                if (obj.HasFlag(BitvectorFlagType.ItemFlags[count].BitvectorData))
                {
                    text += " ";
                    text += BitvectorFlagType.ItemFlags[count].Name;
                }
            }

            return (!String.IsNullOrEmpty(text)) ? text.Substring(1) : "none";
        }

        /// <summary>
        /// Gets a string representing the "use" flags on an object.  Also known as "anti" flags.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string UseFlagString(Object obj)
        {
            string text = String.Empty;
            int count;

            for (count = 0; count < BitvectorFlagType.UseFlags.Length; count++)
            {
                if (obj.HasAntiFlag(BitvectorFlagType.UseFlags[count].BitvectorData))
                {
                    text += " ";
                    text += BitvectorFlagType.UseFlags[count].Name;
                }
            }

            return (!String.IsNullOrEmpty(text)) ? text.Substring(1) : "none";
        }

        public static string WearString(Object obj)
        {
            string text = String.Empty;
            int count;

            for (count = 0; BitvectorFlagType.WearFlags[count].BitvectorData; count++)
            {
                if (obj.HasWearFlag(BitvectorFlagType.WearFlags[count].BitvectorData))
                {
                    text += " ";
                    text += BitvectorFlagType.WearFlags[count].Name;
                }
            }

            return (!String.IsNullOrEmpty(text)) ? text + 1 : "none";
        }

        public static implicit operator bool( Object o )
        {
            if( o == null )
                return false;
            return true;
        }

        public static bool operator !( Object o )
        {
            if( o == null )
                return true;
            return false;
        }

        /// <summary>
        /// Checks for an affect on an object based on its bitvector.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool HasAffect( Bitvector bvect )
        {
            if( Macros.IsSet( _affectedBy[ bvect.Group ], bvect.Vector ) )
                return true;

            return false;
        }

        /// <summary>
        /// Adds an affect to an object based on its bitvector.
        /// </summary>
        /// <param name="bvect"></param>
        public void AddAffect( Bitvector bvect )
        {
            Macros.SetBit( ref _affectedBy[ bvect.Group ], bvect.Vector );
            return;
        }

        /// <summary>
        /// Removes an affect from an object based on its bitvector.
        /// </summary>
        /// <param name="bvect"></param>
        public void RemoveAffect( Bitvector bvect )
        {
            Macros.RemoveBit( ref _affectedBy[ bvect.Group ], bvect.Vector );
            return;
        }

        /// <summary>
        /// Takes an object out of the room it's currently in.
        /// </summary>
        public void RemoveFromRoom()
        {
            if( _inRoom == null )
            {
                Log.Error( "Object.RemoveFromRoom(): Object " + _shortDescription + " not in any room.");
                return;
            }

            if( !_inRoom.Contents.Remove( this ) )
            {
                Log.Error( "Object.RemoveFromRoom(): obj " + _shortDescription + " not found in room " +
                    _inRoom.IndexNumber + "." );
                // If the room doesn't recognize this object as being present we might as well reciprocate.
                _inRoom = null;
                return;
            }
            _inRoom = null;

            if( HasFlag( ObjTemplate.ITEM_LIT ) )
            {
                _inRoom.Light--;
            }
            return;
        }

        /// <summary>
        /// Places an object into a room.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool AddToRoom( Room room )
        {
            if( room == null )
            {
                Log.Error( "Object.AddToRoom(): null room.", 0 );
                return false;
            }
            if( _objIndexData == null )
            {
                Log.Error( "Object.AddToRoom(): Object has null pIndexData.", 0 );
                return false;
            }
            if( _objIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_MONEY_ONE || _objIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_MONEY_SOME )
            {
                for( int i = (room.Contents.Count-1); i >= 0; i-- )
                {
                    Object obj2 = room.Contents[i];
                    switch( obj2._objIndexData.IndexNumber )
                    {
                        case StaticObjects.OBJECT_NUMBER_MONEY_ONE:
                        case StaticObjects.OBJECT_NUMBER_MONEY_SOME:
                            _objIndexData = Database.GetObjTemplate( StaticObjects.OBJECT_NUMBER_MONEY_SOME );
                            _name = _objIndexData.Name;
                            _shortDescription = _objIndexData.ShortDescription;
                            _fullDescription = _objIndexData.FullDescription;
                            _values[ 0 ] += obj2._values[ 0 ];
                            _values[ 1 ] += obj2._values[ 1 ];
                            _values[ 2 ] += obj2._values[ 2 ];
                            _values[ 3 ] += obj2._values[ 3 ];
                            obj2.RemoveFromWorld();
                            break;
                    }
                }
            }

            room.Contents.Insert(0, this );
            _inRoom = room;

            if( HasFlag( ObjTemplate.ITEM_LIT ) )
            {
                _inRoom.Light++;
            }

            return true;
        }

        /// <summary>
        /// Move an object into another object, such as putting an item in a bag.
        /// </summary>
        /// <param name="objToAdd">The object to be placed into this object.</param>
        /// <returns>True for success, False for failure.</returns>
        public bool AddToObject( Object objToAdd )
        {
            if (objToAdd == null)
            {
                Log.Error( "Object.AddToObject(): Called with null objToAdd.", 0 );
                return false;
            }

            _contains.Add( objToAdd );
            objToAdd._inObject = this;
            objToAdd._flyLevel = _flyLevel;

            if( _carriedBy != null )
            {
                _carriedBy._carryWeight += objToAdd.Weight;
                foreach (Object obj in objToAdd.Contains)
                {
                    _carriedBy._carryWeight += obj.Weight;
                }
            }

            return true;
        }

        /// <summary>
        /// Used to move an object out of another object, such as taking an item from a backpack.
        /// </summary>
        public void RemoveFromObject()
        {
            Object objFrom = _inObject;
            if( objFrom == null )
            {
                Log.Error("Object.RemoveFromObject(): null objFrom for " + _shortDescription + ".", 0);
                return;
            }

            if( !objFrom._contains.Remove( this ) )
            {
                Log.Error("Object.RemoveFromObject(): obj: " + _shortDescription + "not found in objFrom: " + 
                    objFrom._shortDescription + ".", 0);
                _inObject = null;
                return;
            }

            for( ; objFrom != null; objFrom = objFrom._inObject )
            {
                if( objFrom._carriedBy != null )
                {
                    objFrom._carriedBy._carryWeight -= GetWeight();
                }
            }

            _inObject = null;
            return;
        }

        /// <summary>
        /// Checks whether the object can be worn by the character and provides a message
        /// to the character if it can't be.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool IsWearableBy(CharData ch)
        {
            if (ch.IsImmortal() && ch.HasActionBit(PC.PLAYER_GODMODE))
                return true;

            if (ch.IsClass(CharClass.Names.monk))
            {
                if (_weight > (1 + (ch._level / 15)))
                {
                    SocketConnection.Act("$p&n is too heavy for you to use.", ch, this, null, SocketConnection.MessageTarget.character);
                    return false;
                }
            }

            if (HasWearFlag(ObjTemplate.USE_ANYONE))
                return true;

            switch (ch._sex)
            {
                case MobTemplate.Sex.male:
                    if (HasAntiFlag(ObjTemplate.USE_ANTIMALE))
                    {
                        SocketConnection.Act("$p&n is too dainty for one of such as yourself.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case MobTemplate.Sex.female:
                    if (HasAntiFlag(ObjTemplate.USE_ANTIFEMALE))
                    {
                        SocketConnection.Act("$p&n is too bulky and poorly designed for one as skilled as you.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case MobTemplate.Sex.neutral:
                    if (HasAntiFlag(ObjTemplate.USE_ANTINEUTER))
                    {
                        SocketConnection.Act("$p&n was not designed for your form.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                default:
                    SocketConnection.Act("$p&n cannot be used by you because your sex is buggy.", ch, this, null, SocketConnection.MessageTarget.character);
                    return false;
            }

            switch (ch._charClass.ClassNumber)
            {
                default:
                    break;
                case CharClass.Names.warrior:
                    if (!HasAntiFlag(ObjTemplate.USE_WARRIOR))
                    {
                        SocketConnection.Act("You won't abide such inferiority as found in $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.ranger:
                case CharClass.Names.hunter:
                    if (!HasAntiFlag(ObjTemplate.USE_RANGER))
                    {
                        SocketConnection.Act("Your professionalism prevents you from using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.paladin:
                    if (!HasAntiFlag(ObjTemplate.USE_PALADIN))
                    {
                        SocketConnection.Act("Your convictions prevents you from using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.antipaladin:
                    if (!HasAntiFlag(ObjTemplate.USE_ANTI))
                    {
                        SocketConnection.Act("Your skill and pride prevents you from using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.cleric:
                    if (!HasAntiFlag(ObjTemplate.USE_CLERIC))
                    {
                        SocketConnection.Act("Your faith prevents you from using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.druid:
                    if (!HasAntiFlag(ObjTemplate.USE_DRUID))
                    {
                        SocketConnection.Act("Your path prevents you from using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.shaman:
                    if (!HasAntiFlag(ObjTemplate.USE_SHAMAN))
                    {
                        SocketConnection.Act("You lack the proper know-how to use $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.sorcerer:
                case CharClass.Names.illusionist:
                    if (!HasAntiFlag(ObjTemplate.USE_SORCERER))
                    {
                        SocketConnection.Act("You neglected use of $p&n in deference to your magics.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.elementAir:
                case CharClass.Names.elementEarth:
                case CharClass.Names.elementFire:
                case CharClass.Names.elementWater:
                    if (!HasAntiFlag(ObjTemplate.USE_ELEMENTAL))
                    {
                        SocketConnection.Act("You have better things to waste time on than $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.thief:
                    if (!HasAntiFlag( ObjTemplate.USE_THIEF))
                    {
                        SocketConnection.Act("Using $p&n would only hinder your abilities.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.psionicist:
                    if (!HasAntiFlag(ObjTemplate.USE_PSI))
                    {
                        SocketConnection.Act("The thought of using $p&n makes your brain hurt.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.assassin:
                    if (!HasAntiFlag(ObjTemplate.USE_ASSASSIN))
                    {
                        SocketConnection.Act("You never studied the art behind $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.mercenary:
                    if (!HasAntiFlag( ObjTemplate.USE_MERCENARY))
                    {
                        SocketConnection.Act("Your training never addressed using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case CharClass.Names.mystic:
                case CharClass.Names.monk:
                    if (!HasAntiFlag( ObjTemplate.USE_MONK))
                    {
                        SocketConnection.Act("You cannot use $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
            }

            switch (ch.GetRace())
            {
                default:
                    break;
                case Race.RACE_HUMAN:
                    if (HasAntiFlag( ObjTemplate.USE_NOHUMAN))
                    {
                        SocketConnection.Act("Your race is ill fit to use $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_GREYELF:
                    if (HasAntiFlag(ObjTemplate.USE_NOGREYELF))
                    {
                        SocketConnection.Act("Your race is too weak to handle $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_HALFELF:
                    if (HasAntiFlag( ObjTemplate.USE_NOHALFELF))
                    {
                        SocketConnection.Act("Your bastardized nature prevents you from using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_DWARF:
                    if (HasAntiFlag( ObjTemplate.USE_NODWARF))
                    {
                        SocketConnection.Act("Your body wasn't designed to use $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_HALFLING:
                    if (HasAntiFlag( ObjTemplate.USE_NOGNOME))
                    {
                        SocketConnection.Act("Just contemplating $p&n makes you long for food.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_BARBARIAN:
                    if (HasAntiFlag( ObjTemplate.USE_NOBARBARIAN))
                    {
                        SocketConnection.Act("Your size and mass prevents you from using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_DUERGAR:
                    if (HasAntiFlag( ObjTemplate.USE_NODUERGAR))
                    {
                        SocketConnection.Act("Of what use is $p&n to a &n&+rduergar&n?", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_DROW:
                    if (HasAntiFlag( ObjTemplate.USE_NODROW))
                    {
                        SocketConnection.Act("The design of $p&n is too inferior for use by a &n&+mdrow&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_TROLL:
                    if (HasAntiFlag( ObjTemplate.USE_NOTROLL))
                    {
                        SocketConnection.Act("You try to use $p&n, but become quickly frustrated.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_OGRE:
                    if (HasAntiFlag( ObjTemplate.USE_NOOGRE))
                    {
                        SocketConnection.Act("You can't quite figure out how to use $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_ORC:
                    if (HasAntiFlag( ObjTemplate.USE_NOORC))
                    {
                        SocketConnection.Act("Obviously, $p&n was not crafted for an &+Lorc&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_CENTAUR:
                    if (HasAntiFlag( ObjTemplate.USE_NOCENTAUR))
                    {
                        SocketConnection.Act("Your find $p&n's design unsuitable for a &n&+gcen&+Ltaur&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_GITHYANKI:
                    if (HasAntiFlag( ObjTemplate.USE_NOGITHYANKI))
                    {
                        SocketConnection.Act("You find $p&n to be unfitting for a &+Wgith&+Gyanki&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_GITHZERAI:
                    if (HasAntiFlag( ObjTemplate.USE_NOGITHZERAI))
                    {
                        SocketConnection.Act("Your nature prevents you from using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_MINOTAUR:
                    if (HasAntiFlag( ObjTemplate.USE_NOMINOTAUR))
                    {
                        SocketConnection.Act("Your distinctive build prevents your use of $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_GOBLIN:
                    if (HasAntiFlag( ObjTemplate.USE_NOGOBLIN))
                    {
                        SocketConnection.Act("Your stature and build make it impossible for you to use $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_RAKSHASA:
                    if (HasAntiFlag( ObjTemplate.USE_NORAKSHASA))
                    {
                        SocketConnection.Act("Your feline structure inhibits you from using $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RACE_GNOLL:
                    if (HasAntiFlag( ObjTemplate.USE_NOGNOLL))
                    {
                        SocketConnection.Act("Your canine features hinder the use of $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
            }

            switch (ch.GetRacewarSide())
            {
                case Race.RacewarSide.good:
                    if (HasAntiFlag( ObjTemplate.USE_ANTIGOODRACE))
                    {
                        SocketConnection.Act("Your &n&+cs&+Co&n&+cu&+Cl&n is too &+Wpure&n to use $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                case Race.RacewarSide.evil:
                    if (HasAntiFlag( ObjTemplate.USE_ANTIEVILRACE))
                    {
                        SocketConnection.Act("Your &+Lblack &n&+rhe&+Ra&n&+rr&+Rt&n prevents use of $p&n.", ch, this, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// Takes an object from the character who is carrying it.
        /// </summary>
        public void RemoveFromChar()
        {
            CharData ch = _carriedBy;
            if( ch == null )
            {
                Log.Error( "Object.RemoveFromChar: null ch.", 0 );
                return;
            }

            if( _wearLocation != ObjTemplate.WearLocation.none )
            {
                ch.UnequipObject( this );
            }

            if (!ch._carrying.Remove(this))
            {
                Log.Error("Object.RemoveFromChar(): obj not in list.", 0);
            }
            else
            {
                ch._carryNumber -= 1;
                ch._carryWeight -= GetWeight();
                if (HasFlag(ObjTemplate.ITEM_LIT))
                {
                    ch._inRoom.Light--;
                }
            }
            _carriedBy = null;

            return;
        }

        /// <summary>
        /// Gets the weight of an object, including the weight of its contents.
        /// </summary>
        /// <returns></returns>
        public int GetWeight()
        {
            int oweight = _weight;

            foreach( Object iter in _contains )
            {
                oweight += iter.GetWeight();
            }

            return oweight;
        }

        public const int EQUIP_HOLD = 1;
        public const int EQUIP_WIELD = 2;
        public const int EQUIP_SHIELD = 3;
        public const int EQUIP_LIGHT = 4;

        public static void GetObject(CharData ch, Object obj, Object container)
        {
            if (!obj.HasWearFlag(ObjTemplate.WEARABLE_CARRY))
            {
                ch.SendText("You can't pick that up.\r\n");
                return;
            }

            if (obj._itemType != ObjTemplate.ObjectType.money)
            {
                if (ch._carryWeight + obj.GetWeight() > ch.MaxCarryWeight())
                {
                    SocketConnection.Act("$p&n is quite literally the &+Ystraw&n that would break the &n&+ycamel&n's back.", ch, obj, null, SocketConnection.MessageTarget.character);
                    return;
                }
            }

            if (container != null)
            {
                SocketConnection.Act("You get $p&n from $P&n.", ch, obj, container, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n retrieves $p&n from $P&n.", ch, obj, container, SocketConnection.MessageTarget.room);
                obj.RemoveFromObject();
                // Fix for corpse EQ dupe on crash
                if (container._itemType == ObjTemplate.ObjectType.pc_corpse)
                {
                    Database.CorpseList.Save();
                }
            }
            else
            {
                SocketConnection.Act("You get $p&n.", ch, obj, container, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n picks up $p&n.", ch, obj, container, SocketConnection.MessageTarget.room);
                obj.RemoveFromRoom();
            }


            if (obj.HasFlag(ObjTemplate.ITEM_ANTI_EVIL) && ch.IsEvil())
            {
                SocketConnection.Act("&+LYou are &n&+rburned&+L by holy &+Rfire&+L from $p&+L.  Ouch!&n", ch, obj, null,
                     SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&+L is &n&+rburned&+L by holy &+Rfire&+L from &n$p&+L!&n", ch, obj, null, SocketConnection.MessageTarget.room);
                Combat.InflictSpellDamage(ch, ch, 20, "burning hands", AttackType.DamageType.white_magic);
                obj.AddToRoom(ch._inRoom);
                return;
            }

            if (obj.HasFlag(ObjTemplate.ITEM_ANTI_EVIL) && ch.IsEvil())
            {
                SocketConnection.Act("&+LYou are &n&+rburned&+L by holy &+Rfire&+L from $p&+L.  Ouch!&n", ch, obj, null,
                     SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&+L is &n&+rburned&+L by holy &+Rfire&+L from &n$p&+L!&n", ch, obj, null, SocketConnection.MessageTarget.room);
                Combat.InflictSpellDamage(ch, ch, 20, "burning hands", AttackType.DamageType.white_magic);
                obj.AddToRoom(ch._inRoom);
                return;
            }

            if (obj.HasFlag(ObjTemplate.ITEM_ANTI_GOOD) && ch.IsGood())
            {
                SocketConnection.Act("&+LYou are &n&+rconsumed&+L by &+Rfire&+L and &+Ldespair&n from $p&+L!&n", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&+L is &n&+rengulfed&+L by an abundancy of &+Rflames&+L from &n$p&+L!&n", ch, obj, null, SocketConnection.MessageTarget.room);
                Combat.InflictSpellDamage(ch, ch, 20, "burning hands", AttackType.DamageType.white_magic);
                obj.AddToRoom(ch._inRoom);
                return;
            }

            if (obj._itemType == ObjTemplate.ObjectType.money)
            {
                int amount = obj._values[0] + obj._values[1] + obj._values[2] + obj._values[3];
                ch.ReceiveCopper(obj._values[0]);
                ch.ReceiveSilver(obj._values[1]);
                ch.ReceiveGold(obj._values[2]);
                ch.ReceivePlatinum(obj._values[3]);

                if (amount > 1)
                {
                    string text = String.Format("You pick up");
                    string text2;
                    if (obj._values[3] > 0)
                    {
                        text2 = String.Format(" {0} &+Wplatinum&n", obj._values[3]);
                        if (obj._values[0] > 0 || obj._values[1] > 0 || obj._values[2] > 0)
                        {
                            text2 += ",";
                        }
                        text += text2;
                    }
                    if (obj._values[2] > 0)
                    {
                        text2 = String.Format(" {0} &+Ygold&n", obj._values[2]);
                        if (obj._values[0] > 0 || obj._values[1] > 0)
                        {
                            text2 += ",";
                        }
                        text += text2;
                    }
                    if (obj._values[1] > 0)
                    {
                        text2 = String.Format(" {0} &n&+wsilver&n", obj._values[1]);
                        if (obj._values[0] > 0)
                        {
                            text2 += ",";
                        }
                        text += text2;
                    }
                    if (obj._values[0] > 0)
                    {
                        text2 = String.Format(" {0} &n&+ycopper&n", obj._values[0]);
                        text += text2;
                    }
                    text += " coins.\r\n";
                    ch.SendText(text);
                }

                obj.RemoveFromWorld();
            }
            else
            {
                obj.ObjToChar(ch);
                // Prevent item duplication.
                CharData.SavePlayer(ch);
            }

            return;
        }

        /// <summary>
        /// Wear an object with optional replacement of existing worn object in
        /// the same location.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="obj"></param>
        /// <param name="replaceExisting"></param>
        public static void WearObject(CharData ch, Object obj, bool replaceExisting)
        {
            if (!obj.IsWearableBy(ch))
                return;

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_TAIL)
                    && Macros.IsSet((int)Race.RaceList[ch.GetRace()].BodyParts, (int)Race.Parts.tail))
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.tail, replaceExisting))
                    return;
                SocketConnection.Act("You wear $p&n on your &+Ltail&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n wears $p&n on $s &+Ltail&n.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.tail);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_HORNS)
                    && Macros.IsSet((int)Race.RaceList[ch.GetRace()].BodyParts, (int)Race.Parts.horns))
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.horns, replaceExisting))
                    return;
                SocketConnection.Act("You wear $p&n on your &+Whorns&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n wears $p&n on $s &+Whorns&n.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.horns);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_EYES))
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.eyes, replaceExisting))
                    return;
                SocketConnection.Act("You place $p&n over your eyes.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n places $p&n over $s eyes.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.eyes);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_FINGER) && ch.GetRace() != Race.RACE_THRIKREEN)
            {
                if (GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.finger_left)
                        && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.finger_right)
                        && !ch.RemoveObject(ObjTemplate.WearLocation.finger_left, replaceExisting)
                        && !ch.RemoveObject(ObjTemplate.WearLocation.finger_right, replaceExisting))
                    return;

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.finger_left))
                {
                    SocketConnection.Act("You slip $p&n onto a finger of your left hand.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n slips $p&n onto a finger of $s left hand.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.finger_left);
                    return;
                }

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.finger_right))
                {
                    SocketConnection.Act("You slide $p&n onto a finger of your right hand.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n slides $p&n onto a finger of $s right hand.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.finger_right);
                    return;
                }

                Log.Error("Object.WearObject: no free finger.", 0);
                ch.SendText("Anymore rings and you would severly hinder finger movement.\r\n");
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_NECK))
            {
                if (GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.neck_one)
                        && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.neck_two)
                        && !ch.RemoveObject(ObjTemplate.WearLocation.neck_one, replaceExisting)
                        && !ch.RemoveObject(ObjTemplate.WearLocation.neck_two, replaceExisting))
                    return;

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.neck_one))
                {
                    SocketConnection.Act("You place $p&n around your neck.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n wears $p&n around $s neck.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.neck_one);
                    return;
                }

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.neck_two))
                {
                    SocketConnection.Act("You wear $p&n around your neck.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n places $p&n around $s neck.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.neck_two);
                    return;
                }

                Log.Error("Object.WearObject: no free neck.", 0);
                ch.SendText("You can't fit anything else around your neck.\r\n");
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_BODY) && ch.GetRace() != Race.RACE_THRIKREEN)
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.body, replaceExisting))
                    return;
                SocketConnection.Act("You slide your body into $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n wears $p&n on $s body.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.body);
                return;
            }
            // Added check for mino and head gear prevention due to horns
            if (obj.HasWearFlag(ObjTemplate.WEARABLE_HEAD))
            {
                if (ch.GetRace() != Race.RACE_MINOTAUR)
                {
                    if (!ch.RemoveObject(ObjTemplate.WearLocation.head, replaceExisting))
                        return;
                    SocketConnection.Act("You don $p&n on your head.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n places $p&n on $s head.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.head);
                    return;
                }
                SocketConnection.Act("Your &+Lhorns&n prevent you from wearing $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n foolishly attempts to place $p&n on over $s &+Lhorns&n.", ch, obj, null, SocketConnection.MessageTarget.room);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_LEGS)
                    && ch.GetRace() != Race.RACE_CENTAUR)
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.legs, replaceExisting))
                    return;
                SocketConnection.Act("You pull $p&n onto your legs.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n slides $s legs into $p&n.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.legs);
                return;
            }

            // Added Minotaur to the Following ObjIndex.WEARABLE_FEET check
            // Check some over the syntax, Vey, if you wouldn't mind.
            if (obj.HasWearFlag(ObjTemplate.WEARABLE_FEET))
            {
                if (ch.GetRace() != Race.RACE_CENTAUR && ch.GetRace() != Race.RACE_MINOTAUR && ch.GetRace() != Race.RACE_THRIKREEN)
                {
                    if (!ch.RemoveObject(ObjTemplate.WearLocation.feet, replaceExisting))
                        return;
                    SocketConnection.Act("You wear $p&n on your feet.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n wears $p&n on $s feet.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.feet);
                    return;
                }
                if (ch.GetRace() != Race.RACE_THRIKREEN)
                {
                    SocketConnection.Act("Your &+Lhooves&n prevent you from wearing $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n foolishly attempts to place $p&n on $s &+Lhooves&n.", ch, obj, null, SocketConnection.MessageTarget.room);
                    return;
                }
                SocketConnection.Act("Your underdeveloped feet prevent you from wearing $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n foolishly attempts to place $p&n on $s feet.", ch, obj, null, SocketConnection.MessageTarget.room);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_HANDS))
            {
                if (ch.GetRace() != Race.RACE_THRIKREEN)
                {
                    if (!ch.RemoveObject(ObjTemplate.WearLocation.hands, replaceExisting))
                        return;
                    SocketConnection.Act("You wear $p&n on your hands.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n pulls $p&n onto $s hands.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.hands);
                    return;
                }
                if (GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hands)
                    && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hands_lower)
                    && !ch.RemoveObject(ObjTemplate.WearLocation.hands, replaceExisting)
                    && !ch.RemoveObject(ObjTemplate.WearLocation.hands_lower, replaceExisting))
                    return;

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hands))
                {
                    SocketConnection.Act("You wear $p&n on your hands.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n pulls $p&n onto $s hands.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.hands);
                    return;
                }

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hands_lower))
                {
                    SocketConnection.Act("You wear $p&n on your hands.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n pulls $p&n onto $s hands.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.hands_lower);
                    return;
                }
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_ARMS))
            {
                if (ch.GetRace() != Race.RACE_THRIKREEN)
                {
                    if (!ch.RemoveObject(ObjTemplate.WearLocation.arms, replaceExisting))
                        return;
                    SocketConnection.Act("You slip your arms into $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n slides $p&n over $s arms.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.arms);
                    return;
                }
                if (GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.arms)
                    && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.arms_lower)
                    && !ch.RemoveObject(ObjTemplate.WearLocation.arms, replaceExisting)
                    && !ch.RemoveObject(ObjTemplate.WearLocation.arms_lower, replaceExisting))
                    return;

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.arms))
                {
                    SocketConnection.Act("You slip your arms into $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n slides $p&n over $s arms.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.arms);
                    return;
                }

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.arms_lower))
                {
                    SocketConnection.Act("You slip your arms into $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n slides $p&n over $s arms.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.arms_lower);
                    return;
                }

                Log.Error("WearObject: no free ear.", 0);
                ch.SendText("You already wear an earring in each ear.\r\n");
                return;
            }


            if (obj.HasWearFlag(ObjTemplate.WEARABLE_ONBACK))
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.on_back, replaceExisting))
                    return;
                SocketConnection.Act("You strap $p&n to your back.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n straps $p&n to $s back.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.on_back);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_QUIVER))
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.quiver, replaceExisting))
                    return;
                SocketConnection.Act("You adjust $p&n across your shoulders.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n straps $p&n across $s shoulders.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.quiver);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_ABOUT))
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.about_body, replaceExisting))
                    return;
                SocketConnection.Act("You drape $p&n about your body.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n wraps $p&n about $s body.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.about_body);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_WAIST))
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.waist, replaceExisting))
                    return;
                SocketConnection.Act("You secure $p&n about your waist.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n secures $p&n about $s waist.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.waist);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_FACE))
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.face, replaceExisting))
                    return;
                SocketConnection.Act("You place $p&n over your face.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n covers $s his face with $p&n.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.face);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_BADGE))
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.badge, replaceExisting))
                    return;
                SocketConnection.Act("You pin $p&n over your right breast.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n pins $p&n over $s right breast.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.badge);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_WRIST))
            {
                if (ch.GetRace() != Race.RACE_THRIKREEN)
                {
                    if (GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_left)
                            && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_right)
                            && !ch.RemoveObject(ObjTemplate.WearLocation.wrist_left, replaceExisting)
                            && !ch.RemoveObject(ObjTemplate.WearLocation.wrist_right, replaceExisting))
                        return;

                    if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_left))
                    {
                        SocketConnection.Act("You wear $p&n around your left wrist.",
                             ch, obj, null, SocketConnection.MessageTarget.character);
                        SocketConnection.Act("$n&n wears $p&n around $s left wrist.",
                             ch, obj, null, SocketConnection.MessageTarget.room);
                        ch.EquipObject(ref obj, ObjTemplate.WearLocation.wrist_left);
                        return;
                    }

                    if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_right))
                    {
                        SocketConnection.Act("You wear $p&n about your right wrist.",
                             ch, obj, null, SocketConnection.MessageTarget.character);
                        SocketConnection.Act("$n&n wears $p&n about $s right wrist.",
                             ch, obj, null, SocketConnection.MessageTarget.room);
                        ch.EquipObject(ref obj, ObjTemplate.WearLocation.wrist_right);
                        return;
                    }

                    Log.Error("Object.WearObject: no free wrist.", 0);
                    ch.SendText("You already wear something upon both wrists.\r\n");
                    return;
                }
                if (GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_left)
                    && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_right)
                    && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_lower_left)
                    && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_lower_right)
                    && !ch.RemoveObject(ObjTemplate.WearLocation.wrist_left, replaceExisting)
                    && !ch.RemoveObject(ObjTemplate.WearLocation.wrist_right, replaceExisting)
                    && !ch.RemoveObject(ObjTemplate.WearLocation.wrist_lower_left, replaceExisting)
                    && !ch.RemoveObject(ObjTemplate.WearLocation.wrist_lower_right, replaceExisting))
                    return;

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_left))
                {
                    SocketConnection.Act("You wear $p&n around your left wrist.",
                                         ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n wears $p&n around $s left wrist.",
                                         ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.wrist_left);
                    return;
                }

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_right))
                {
                    SocketConnection.Act("You wear $p&n about your right wrist.",
                                         ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n wears $p&n about $s right wrist.",
                                         ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.wrist_right);
                    return;
                }

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_lower_left))
                {
                    SocketConnection.Act("You wear $p&n around your lower left wrist.",
                                         ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n wears $p&n around $s lower left wrist.",
                                         ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.wrist_lower_left);
                    return;
                }

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.wrist_lower_right))
                {
                    SocketConnection.Act("You wear $p&n about your lower right wrist.",
                                         ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n wears $p&n about $s lower right wrist.",
                                         ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.wrist_lower_right);
                    return;
                }

                Log.Error("Object.WearObject: no free wrist.", 0);
                ch.SendText("You already wear something upon all four wrists.\r\n");
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_EAR) && ch.GetRace() != Race.RACE_THRIKREEN)
            {
                if (GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.ear_left)
                        && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.ear_right)
                        && !ch.RemoveObject(ObjTemplate.WearLocation.ear_left, replaceExisting)
                        && !ch.RemoveObject(ObjTemplate.WearLocation.ear_right, replaceExisting))
                    return;

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.ear_left))
                {
                    SocketConnection.Act("You wear $p&n in your left ear.",
                         ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n hangs $p&n from $s left ear.",
                         ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.ear_left);
                    return;
                }

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.ear_right))
                {
                    SocketConnection.Act("You hang $p&n from your right ear.",
                         ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n wears $p&n in $s right ear.",
                         ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.ear_right);
                    return;
                }

                Log.Error("Object.WearObject: no free ear.", 0);
                ch.SendText("You already wear an earring in each ear.\r\n");
                return;
            }

            // Moved these before belt check so as to avoid weapons flagged both
            // wieldable and wear_belt from defaulting to belt position

            // The following code looks way to repetitive, leading one
            // to believe it can be highly optimized.
            if (obj._itemType == ObjTemplate.ObjectType.light)
            {
                EquipInHand(ch, obj, EQUIP_LIGHT);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_WIELD) && obj._itemType == ObjTemplate.ObjectType.weapon)
            {
                EquipInHand(ch, obj, EQUIP_WIELD);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_SHIELD))
            {
                EquipInHand(ch, obj, EQUIP_SHIELD);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_HOLD))
            {
                EquipInHand(ch, obj, EQUIP_HOLD);
                return;
            }

            // Only two belt slots for now, should be three.
            if (obj.HasWearFlag(ObjTemplate.WEARABLE_ATTACH_BELT))
            {
                Object obj2;
                string text;

                if (!(obj2 = GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.waist)))
                {
                    ch.SendText("You don't have a &n&+ybelt&n to attach it to.\r\n");
                    return;
                }

                if (GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.belt_attach_one)
                        && GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.belt_attach_two)
                        && !ch.RemoveObject(ObjTemplate.WearLocation.belt_attach_one, replaceExisting)
                        && !ch.RemoveObject(ObjTemplate.WearLocation.belt_attach_two, replaceExisting))
                    return;

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.belt_attach_one))
                {
                    text = String.Format("You attach $p&n to {0}&n.", obj2._shortDescription);
                    SocketConnection.Act(text, ch, obj, null, SocketConnection.MessageTarget.character);
                    text = String.Format("$n&n attaches $p&n to {0}&n.", obj2._shortDescription);
                    SocketConnection.Act(text, ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.belt_attach_one);
                    return;
                }

                if (!GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.belt_attach_two))
                {
                    text = String.Format("You attach $p&n to {0}&n.", obj2._shortDescription);
                    SocketConnection.Act(text, ch, obj, null, SocketConnection.MessageTarget.character);
                    text = String.Format("$n&n attaches $p&n to {0}&n.", obj2._shortDescription);
                    SocketConnection.Act(text, ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, ObjTemplate.WearLocation.belt_attach_two);
                    return;
                }

                Log.Error("Wear_obj: no free belt slot.", 0);
                ch.SendText("Your &n&+ybelt&n is already full.\r\n");
                return;
            }

            /* This will be fixed when we have something better to check against. */
            if (obj.HasWearFlag(ObjTemplate.WEARABLE_HORSE_BODY)
                    && ch.GetRace() == Race.RACE_CENTAUR)
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.horse_body, replaceExisting))
                    return;
                SocketConnection.Act("You strap $p&n onto your body.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n straps $p&n onto $s body.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.horse_body);
                return;
            }

            if (obj.HasWearFlag(ObjTemplate.WEARABLE_NOSE) && ch.GetRace() == Race.RACE_MINOTAUR)
            {
                if (!ch.RemoveObject(ObjTemplate.WearLocation.nose, replaceExisting))
                    return;
                SocketConnection.Act("You attach $p&n to your nose.", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n&n wears $p&n in $s nose.", ch, obj, null, SocketConnection.MessageTarget.room);
                ch.EquipObject(ref obj, ObjTemplate.WearLocation.nose);
                return;
            }

            if (replaceExisting)
                ch.SendText("You can't wear, wield, or hold that.\r\n");

            return;
        }

        /// <summary>
        /// Gets the cost (cash value) of an object.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="keeper"></param>
        /// <param name="obj"></param>
        /// <param name="isBuyTransaction"></param>
        /// <returns></returns>
        public static int GetCost(CharData customer, CharData keeper, Object obj, bool isBuyTransaction)
        {
            Shop shop;
            int cost;

            if (!obj)
                return 0;

            if (!(shop = keeper._mobTemplate.ShopData))
            {
                cost = obj._cost;
            }
            else
            {
                if (isBuyTransaction)
                {
                    cost = obj._cost * shop.PercentBuy / 100;
                }
                else
                {
                    cost = 0;
                    foreach (ObjTemplate.ObjectType itemtype in shop.BuyTypes)
                    {
                        if (obj._itemType == itemtype)
                        {
                            cost = obj._cost * shop.PercentSell / 100;
                            break;
                        }
                    }
                }
            }

            if (obj._itemType == ObjTemplate.ObjectType.staff || obj._itemType == ObjTemplate.ObjectType.wand)
                cost = cost * obj._values[2] / obj._values[1];

            if (!isBuyTransaction)
                cost = (cost * (80 + customer.GetCurrCha() / 5)) / 100;
            else
                cost = (cost * (120 - customer.GetCurrCha() / 5)) / 100;

            return cost;
        }

        /// <summary>
        /// Recoding of the WearObject function into a table-based system which is fairly
        /// complex, but easy to maintain via the table. Bitshifting is used to check
        /// each wear location for a piece of equipment.
        /// 
        /// (not enabled)
        /// 
        /// TODO: Enable/finish this or remove it.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="obj"></param>
        /// <param name="replaceExisting"></param>
        static void NewWearObject(CharData ch, ref Object obj, bool replaceExisting)
        {
            int count;

            for (count = 0; count < 30; ++count)
            {
                if (obj.HasWearFlag(new Bitvector(0, (1 << count))))
                {
                    if (WearData.Table[(1 << count)]._bodyPartNeeded == 0 ||
                            Macros.IsSet((int)Race.RaceList[ch.GetRace()].BodyParts, WearData.Table[(1 << count)]._bodyPartNeeded))
                    {
                        if (WearData.Table[(1 << count)]._racesNotAllowed == -1 ||
                                ch.GetRace() != WearData.Table[(1 << count)]._racesNotAllowed)
                        {
                            if ((WearData.Table[(1 << count)]._wearLocation != 0 && !ch.RemoveObject((ObjTemplate.WearLocation)WearData.Table[(1 << count)]._wearLocation, replaceExisting)) &&
                                    (WearData.Table[(1 << count)]._wearLocation2 != 0 && !ch.RemoveObject((ObjTemplate.WearLocation)WearData.Table[(1 << count)]._wearLocation2, replaceExisting)) &&
                                    (WearData.Table[(1 << count)]._wearLocation3 != 0 && !ch.RemoveObject((ObjTemplate.WearLocation)WearData.Table[(1 << count)]._wearLocation3, replaceExisting)))
                                return;
                            SocketConnection.Act(WearData.Table[(1 << count)]._wearMessage, ch, obj, null, SocketConnection.MessageTarget.character);
                            SocketConnection.Act(WearData.Table[(1 << count)]._wearMessage2, ch, obj, null, SocketConnection.MessageTarget.room);
                            // need to allow for multiple wear locations
                            ch.EquipObject(ref obj, (ObjTemplate.WearLocation)WearData.Table[(1 << count)]._wearLocation);
                            return;

                        }
                        ch.SendText("Your race cannot wear that type of equipment.\r\n");
                        return;
                    }
                    ch.SendText("The design of your body prevents you from using that.\r\n");
                    return;
                }
            }

        }

        /// <summary>
        /// Utility function to create a wall.  Called by create wall spells.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        public static Object MakeWall(CharData ch, Spell spell, int level, Target target, int indexNumber)
        {
            if (ch == null || spell == null) return null;

            Object wall2;
            Exit exit;
            string targetName = (string)target;
            // If we were really smooth we would just create another _targetType called
            // tar_exit
            if (String.IsNullOrEmpty(targetName))
            {
                ch.SendText("Specify a direction in which to cast the spell!\r\n");
                return null;
            }

            int door = Movement.FindExit(ch, targetName);

            if (door == -1)
            {
                ch.SendText("You failed!\r\n");
                return null;
            }

            // The exit of the same direction should be flagged Exit.ExitFlags.walled
            if (!(exit = ch._inRoom.ExitData[door]))
            {
                ch.SendText("You failed!\r\n");
                return null;
            }

            if (exit.HasFlag(Exit.ExitFlag.walled))
            {
                ch.SendText("There's already a wall there!\r\n");
                return null;
            }
            if (exit.HasFlag(Exit.ExitFlag.is_door))
            {
                return null;
            }

            ObjTemplate wallTemplate = Database.GetObjTemplate(indexNumber);
            if (!wallTemplate)
            {
                string output = String.Format("MakeWall: null wall pointer from Database.GetObjTemplate( {0} ).", indexNumber);
                ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_SPAM, 0, output);
                Log.Error(output, 0);
                ch.SendText("Uh oh, no template found for that wall.\r\n");
                return null;
            }

            Object wall = Database.CreateObject(Database.GetObjTemplate(indexNumber), 0);
            if (!wall)
            {
                Log.Error("MakeWall: null wall pointer from Database.CreateObject", 0);
                ch.SendText("Whoops, failed to create wall.  Report this as a bug.\r\n");
                return null;
            }

            // Value[0] should be the direction that is blocked by the wall.
            wall.Values[0] = door;
            // Set the wall's level
            wall.Values[2] = level;

            string text = String.Format("{0} {1} exit.&n", wall.FullDescription, Exit.DirectionName[door]);
            wall.FullDescription = text;

            exit.AddFlag(Exit.ExitFlag.walled);
            if (indexNumber == StaticObjects.OBJECT_NUMBER_WALL_ILLUSION)
                exit.AddFlag(Exit.ExitFlag.illusion);

            wall.Timer = ch._level / 3;
            wall.Level = ch._level;
            wall.AddToRoom(ch._inRoom);

            // Create the wall on the other side
            if (exit.TargetRoom)
            {
                if (exit.TargetRoom.ExitData[Exit.ReverseDirection[wall.Values[0]]])
                {
                    //we actually have a matching exit
                    wall2 = Database.CreateObject(Database.GetObjTemplate(indexNumber), 0);

                    // Value[0] should be the direction that is blocked by the wall.
                    wall2.Values[0] = Exit.ReverseDirection[door];
                    // Set the wall's level
                    wall2.Values[2] = level;

                    text = String.Format("{0} {1} exit.&n", wall2.FullDescription, Exit.DirectionName[Exit.ReverseDirection[door]]);
                    wall2.FullDescription = text;

                    if (exit.TargetRoom.ExitData[Exit.ReverseDirection[door]])
                        exit.TargetRoom.ExitData[Exit.ReverseDirection[door]].AddFlag(Exit.ExitFlag.walled);

                    wall2.Timer = ch._level / 2;
                    wall2.Level = ch._level;
                    wall2.AddToRoom(Room.GetRoom(exit.IndexNumber));
                    if (Room.GetRoom(exit.IndexNumber).People.Count > 0)
                    {
                        text = String.Format("$p&n appears to the {0}.", Exit.DirectionName[wall2.Values[0]]);
                        SocketConnection.Act(text, Room.GetRoom(exit.IndexNumber).People[0], wall2, null, SocketConnection.MessageTarget.all);
                    }
                } //end if matching exit
            }

            return wall;
        }

        /// <summary>
        /// Invoke a magic item.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="obj"></param>
        public static void Invoke(CharData ch, Object obj)
        {
            if (obj.HasAffect(Affect.AFFECT_STONESKIN) && !ch.IsAffected(Affect.AFFECT_STONESKIN))
            {
                Spell spl = Spell.SpellList["stoneskin"];
                if (spl != null)
                {
                    spl.Invoke(ch, Math.Max(obj._level, ch._level), ch);
                }
            }
            if (obj.HasAffect(Affect.AFFECT_FLYING) && !ch.IsAffected(Affect.AFFECT_FLYING))
            {
                ch.SetAffectBit(Affect.AFFECT_FLYING);
                ch.SendText("Your feet rise off the ground.\r\n");
                SocketConnection.Act("$n's feet rise off the ground.", ch, null, null, SocketConnection.MessageTarget.room);
            }
            return;
        }

        /// <summary>
        /// This function's main access commands are wear, wield, and hold, which have the
        /// following flow:
        /// wear:  Command.Wear, wear_obj, equip_hand
        /// wield: Command.Wield, equip_hand
        /// hold:  Command.Hold, equip_hand 
        /// 
        /// We assume by this point that the character is physically able to use the item and will be able
        /// to equip it however specified.  Those checks are performed by WearObject(), Equip(), and Hold().
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool EquipInHand(CharData ch, Object obj, int type)
        {
            int weight = 0;
            ObjTemplate.WearLocation firstAvail = ObjTemplate.WearLocation.none;
            ObjTemplate.WearLocation secondAvail = ObjTemplate.WearLocation.none;
            ObjTemplate.WearLocation lastAvail = ObjTemplate.WearLocation.none;

            Object hand1 = GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_one);
            Object hand2 = GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_two);
            Object hand3 = GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_three);
            Object hand4 = GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_four);
            if (hand1 && (hand1._itemType == ObjTemplate.ObjectType.weapon || hand1._itemType == ObjTemplate.ObjectType.ranged_weapon))
                weight += hand1.GetWeight();
            if (hand2 && (hand2._itemType == ObjTemplate.ObjectType.weapon || hand2._itemType == ObjTemplate.ObjectType.ranged_weapon))
                weight += hand2.GetWeight();
            if (hand3 && (hand3._itemType == ObjTemplate.ObjectType.weapon || hand3._itemType == ObjTemplate.ObjectType.ranged_weapon))
                weight += hand3.GetWeight();
            if (hand4 && (hand4._itemType == ObjTemplate.ObjectType.weapon || hand4._itemType == ObjTemplate.ObjectType.ranged_weapon))
                weight += hand4.GetWeight();

            if (ch.GetRace() != Race.RACE_THRIKREEN)
            {
                if (hand3)
                    Log.Error("non-thrikreen wielding item in hand3", 0);
                if (hand4)
                    Log.Error("non-thrikreen wielding item in hand4", 0);
            }

            // Find number of hand slots used and first available hand.
            // Be sure to handle twohanded stuff.
            if (hand4 && hand4.HasFlag(ObjTemplate.ITEM_TWOHANDED))
            {
                Log.Error("Twohanded weapon in fourth hand -- this is not possible.", 0);
            }
            if (hand3 && hand3.HasFlag(ObjTemplate.ITEM_TWOHANDED))
            {
                if (hand4)
                {
                    Log.Error("Twohanded weapon in third hand with fourth hand holding twohanded weapon -- this is not possible, all twohanded must have a blank hand after it.", 0);
                }
                hand4 = hand3;
            }
            if (hand2 && hand2.HasFlag(ObjTemplate.ITEM_TWOHANDED))
            {
                if (hand3)
                {
                    Log.Error("Twohanded weapon in second hand with third hand holding twohanded weapon -- this is not possible, all twohanded must have a blank hand after it.", 0);
                }
                hand2 = hand3;
            }
            if (!ch.HasInnate(Race.RACE_EXTRA_STRONG_WIELD))
            {
                if (hand1 && hand1.HasFlag(ObjTemplate.ITEM_TWOHANDED))
                {
                    if (hand2)
                    {
                        Log.Error("Twohanded weapon in second hand with first hand holding twohanded weapon -- this is not possible, all twohanded must have a blank hand after it.", 0);
                    }
                    hand2 = hand1;
                }
            }

            if (obj.HasFlag(ObjTemplate.ITEM_TWOHANDED)
                    && !ch.HasInnate(Race.RACE_EXTRA_STRONG_WIELD))
            {
                if (ch.GetRace() == Race.RACE_THRIKREEN && !hand4)
                {
                    firstAvail = ObjTemplate.WearLocation.hand_four;
                    lastAvail = ObjTemplate.WearLocation.hand_four;
                }
                if (ch.GetRace() == Race.RACE_THRIKREEN && !hand3)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_three;
                    secondAvail = firstAvail;
                    firstAvail = ObjTemplate.WearLocation.hand_three;
                }
                if (!hand2)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_two;
                    secondAvail = firstAvail;
                    firstAvail = ObjTemplate.WearLocation.hand_two;
                }
                if (!hand1)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_one;
                    secondAvail = firstAvail;
                    firstAvail = ObjTemplate.WearLocation.hand_one;
                }

                if (firstAvail == 0)
                {
                    ch.SendText("Your hands are full!\r\n");
                    return false;
                }
                if (secondAvail == 0)
                {
                    ch.SendText("You need two hands free to wield that!\r\n");
                    return false;
                }
            }
            else if (obj.HasFlag(ObjTemplate.ITEM_TWOHANDED))
            {
                if (ch.GetRace() == Race.RACE_THRIKREEN && !hand4)
                {
                    firstAvail = ObjTemplate.WearLocation.hand_four;
                }
                if (ch.GetRace() == Race.RACE_THRIKREEN && !hand3)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_three;
                    secondAvail = firstAvail;
                    firstAvail = ObjTemplate.WearLocation.hand_three;
                }
                if (!hand2)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_two;
                    secondAvail = firstAvail;
                    firstAvail = ObjTemplate.WearLocation.hand_two;
                }
                if (!hand1)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_one;
                    secondAvail = ObjTemplate.WearLocation.hand_one;
                    firstAvail = ObjTemplate.WearLocation.hand_one;
                }

                if (firstAvail == 0)
                {
                    ch.SendText("Your hands are full!\r\n");
                    return false;
                }
                if (secondAvail == 0)
                {
                    ch.SendText("You need two hands free to wield that!\r\n");
                    return false;
                }
            }
            else
            {
                if (ch.GetRace() == Race.RACE_THRIKREEN && !hand4)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_four;
                    firstAvail = ObjTemplate.WearLocation.hand_four;
                }
                if (ch.GetRace() == Race.RACE_THRIKREEN && !hand3)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_three;
                    firstAvail = ObjTemplate.WearLocation.hand_three;
                }
                if (!hand2)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_two;
                    firstAvail = ObjTemplate.WearLocation.hand_two;
                }
                if (hand1 == null)
                {
                    if (lastAvail == 0)
                        lastAvail = ObjTemplate.WearLocation.hand_one;
                    firstAvail = ObjTemplate.WearLocation.hand_one;
                }

                if (firstAvail == 0)
                {
                    ch.SendText("Your hands are full!\r\n");
                    return false;
                }
            }

            // Successful hand availability, send message and ready the item.
            // Twohanded shields, held items, and lights are equipped primary.
            // This could annoy ogres/thris but twohanded versions of these items
            // are so rare it's not likely to be an issue.
            switch (type)
            {
                case EQUIP_HOLD:
                    if (!obj.HasFlag(ObjTemplate.ITEM_TWOHANDED))
                        ch.EquipObject(ref obj, lastAvail);
                    else
                        ch.EquipObject(ref obj, firstAvail);
                    break;
                case EQUIP_SHIELD:
                    SocketConnection.Act("You strap $p&n to your arm.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n straps $p&n to $s arm.", ch, obj, null, SocketConnection.MessageTarget.room);
                    if (!obj.HasFlag(ObjTemplate.ITEM_TWOHANDED))
                        ch.EquipObject(ref obj, lastAvail);
                    else
                        ch.EquipObject(ref obj, firstAvail);
                    break;
                case EQUIP_LIGHT:
                    if (obj._itemType == ObjTemplate.ObjectType.light && obj._values[2] != 0)
                    {
                        SocketConnection.Act("You &n&+rli&+Rght&n $p&n and hold it before you.", ch, obj, null, SocketConnection.MessageTarget.character);
                        SocketConnection.Act("$n&n &+Rlig&n&+rhts&n $p&n and holds it before $m.", ch, obj, null, SocketConnection.MessageTarget.room);
                    }
                    else
                    {
                        SocketConnection.Act("You hold the &+Lspent&n remains of $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                        SocketConnection.Act("$n&n holds the spent husk of $p&n.", ch, obj, null, SocketConnection.MessageTarget.room);
                    }
                    if (!obj.HasFlag(ObjTemplate.ITEM_TWOHANDED))
                        ch.EquipObject(ref obj, lastAvail);
                    else
                        ch.EquipObject(ref obj, firstAvail);
                    break;
                case EQUIP_WIELD:
                    // Have to check for dual wield skill, and for total weight of weapons.
                    if (firstAvail != ObjTemplate.WearLocation.hand_one)
                    {
                        // Those without dual wield cannot wield anything in their second hand.  Include thrikreen
                        if (!ch.IsNPC() && !ch.HasSkill("dual wield"))
                        {
                            ch.SendText("You lack the skills to wield a weapon in anything but your primary hand.\r\n");
                            return false;
                        }
                    }
                    if ((weight + obj.GetWeight()) > StrengthModifier.Table[ch.GetCurrStr()].WieldWeight)
                    {
                        SocketConnection.Act("Your meager strength is overwhelmed by $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                        return false;
                    }
                    SocketConnection.Act("You wield $p&n.", ch, obj, null, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n brandishes $p&n.", ch, obj, null, SocketConnection.MessageTarget.room);
                    ch.EquipObject(ref obj, firstAvail);
                    break;
            }

            // Objects with a trap activated on wear.
            if (obj._trap != null && obj._trap.CheckTrigger( Trap.TriggerType.wear))
            {
                ch.SetOffTrap(obj);
                if (ch._position == Position.dead)
                    return false;
            }

            return true;

        }

        /// <summary>
        /// Clones an object recursively.  Ensures containers are properly cloned.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="obj"></param>
        /// <param name="clone"></param>
        public static void RecursiveClone( CharData ch, Object obj, Object clone )
        {
            Object tObj;

            foreach( Object cObj in obj._contains )
            {
                tObj = Database.CreateObject( cObj._objIndexData, 0 );
                Database.CloneObject( cObj, ref tObj );
                clone.AddToObject( tObj );
                RecursiveClone( ch, cObj, tObj );
            }
        }

        /// <summary>
        /// Give an obj to a char.
        /// </summary>
        /// <param name="ch"></param>
        public void ObjToChar( CharData ch )
        {
            ch._carrying.Add(this);
            _carriedBy = ch;
            ch._carryNumber += 1;
            ch._carryWeight += GetWeight();

            if( HasFlag( ObjTemplate.ITEM_LIT ) )
                ch._inRoom.Light++;
            _flyLevel = ch._flyLevel;
        }

        /// <summary>
        /// Find the armor class value of an object, including position effect.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="iWear"></param>
        /// <returns></returns>
        static public int GetArmorClassModifer( Object obj, ObjTemplate.WearLocation iWear )
        {
            if( obj._itemType != ObjTemplate.ObjectType.armor )
                return 0;

            if( obj._itemType == ObjTemplate.ObjectType.shield && ( ( iWear == ObjTemplate.WearLocation.hand_one )
                                                  || ( iWear == ObjTemplate.WearLocation.hand_two ) ) )
            {
                return obj._values[ 0 ];
            }

            switch( iWear )
            {
                case ObjTemplate.WearLocation.body:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.head:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.legs:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.feet:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.hands:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.arms:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.finger_left:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.finger_right:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.neck_one:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.neck_two:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.about_body:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.waist:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.wrist_left:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.wrist_right:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.eyes:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.face:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.horns:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.tail:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.ear_left:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.ear_right:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.badge:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.quiver:
                    return obj._values[ 0 ];
                case ObjTemplate.WearLocation.on_back:
                    return obj._values[ 0 ];
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Find a piece of eq on a character.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="iWear"></param>
        /// <returns></returns>
        public static Object GetEquipmentOnCharacter( CharData ch, ObjTemplate.WearLocation iWear )
        {
            if( ch == null )
                return null;

            foreach( Object obj in ch._carrying )
            {
                if( obj._wearLocation == iWear )
                    return obj;
            }

            return null;
        }

        /// <summary>
        /// Find some object with a given index data. Used by area-reset 'P' command.
        /// </summary>
        /// <param name="objTemplate"></param>
        /// <returns></returns>
        public static Object GetFirstObjectOfTemplateType( ObjTemplate objTemplate )
        {
            foreach( Object obj in Database.ObjectList )
            {
                if (obj._objIndexData == objTemplate)
                {
                    return obj;
                }
            }

            return null;
        }

        /// <summary>
        /// Find an obj in a list.  Uses ch as point-of-view (for can-see checks) and searches
        /// A list starting with object being used to call the method.
        /// </summary>
        /// <param name="list">The list of objects to search.</param>
        /// <param name="ch">The person or mobile that is looking for the object.</param>
        /// <param name="argument">the keyword of the object we are looking for.</param>
        /// <returns>Object if found, null if not.</returns>
        public static Object GetObjFromList( List<Object> list, CharData ch, string argument )
        {
            string arg = String.Empty;

            int number = MUDString.NumberArgument( argument, ref arg );
            int count = 0;
            foreach( Object obj in list )
            {
                // fly_level added
                if( obj._flyLevel != ch._flyLevel )
                    continue;

                if( CharData.CanSeeObj( ch, obj ) && MUDString.NameContainedIn( arg, obj._name ) )
                {
                    if( ++count == number )
                        return obj;
                }
            }

            count = 0;
            foreach( Object obj in list )
            {
                if( ch._flyLevel != obj._flyLevel
                        && obj._itemType != ObjTemplate.ObjectType.wall )
                    continue;
                if( CharData.CanSeeObj( ch, obj ) && MUDString.NameIsPrefixOfContents( arg, obj._name ) )
                {
                    if( ++count == number )
                        return obj;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds an object in the room, but not in a character's inventory.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static Object GetObjectInRoom( CharData ch, string argument )
        {
            Object obj = GetObjFromList( ch._inRoom.Contents, ch, argument );
            if( obj != null )
                return obj;

            return null;
        }

        /// <summary>
        /// Finds an object in the world based on a keyword.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static Object GetObjectInWorld( CharData ch, string argument )
        {
            string arg = String.Empty;

            Object obj = ch.GetObjHere( argument );
            if( obj != null )
            {
                return obj;
            }

            int number = MUDString.NumberArgument( argument, ref arg );
            int count = 0;
            foreach( Object it in Database.ObjectList )
            {
                obj = it;
                if( CharData.CanSeeObj( ch, obj ) && MUDString.NameContainedIn( arg, obj._name ) )
                {
                    if( ++count == number )
                        return obj;
                }
            }

            count = 0;
            foreach( Object it in Database.ObjectList )
            {
                obj = it;
                if( CharData.CanSeeObj( ch, obj ) && MUDString.NameIsPrefixOfContents( arg, obj._name ) )
                {
                    if( ++count == number )
                        return obj;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks whether an artifact has been insulted.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool InsultArtifact(CharData ch)
        {
            Object obj = this;
            if (obj.HasFlag(ObjTemplate.ITEM_ARTIFACT) && !ch.IsImmortal())
            {
                SocketConnection.Act("A &+Bbolt &+Wof &+Cenergy&n from $p zaps you!", ch, obj, null, SocketConnection.MessageTarget.character);
                SocketConnection.Act("A &+Bbolt &+Wof &+Cenergy&n streaks from $p&n and hits $n&n!", ch, obj, null, SocketConnection.MessageTarget.room);
                if (obj._carriedBy == ch)
                {
                    SocketConnection.Act("$p&n falls to the ground.", ch, obj, null, SocketConnection.MessageTarget.room);
                    SocketConnection.Act("$p&n falls to the ground.", ch, obj, null, SocketConnection.MessageTarget.character);
                    obj.RemoveFromChar();
                    obj.AddToRoom(ch._inRoom);
                    obj._flyLevel = ch._flyLevel;
                    Combat.InflictSpellDamage(ch, ch, MUDMath.Dice(2, 6), "none", AttackType.DamageType.magic_other);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Create and fill a money object.
        /// </summary>
        /// <param name="copper"></param>
        /// <param name="silver"></param>
        /// <param name="gold"></param>
        /// <param name="platinum"></param>
        /// <returns></returns>
        static public Object CreateMoney( int copper, int silver, int gold, int platinum )
        {
            Object obj;

            int amount = copper + silver + gold + platinum;

            if( copper < 0 || silver < 0 || gold < 0 || platinum < 0 || amount == 0 )
            {
                Log.Error( "CreateMoney: zero or negative money {0}.", amount );
                copper = 1;
                silver = 0;
                gold = 0;
                platinum = 0;
                amount = 1;
            }

            if( amount == 1 )
            {
                obj = Database.CreateObject( Database.GetObjTemplate( StaticObjects.OBJECT_NUMBER_MONEY_ONE ), 0 );
            }
            else
            {
                obj = Database.CreateObject( Database.GetObjTemplate( StaticObjects.OBJECT_NUMBER_MONEY_SOME ), 0 );
            }

            obj._values[ 0 ] = copper;
            obj._values[ 1 ] = silver;
            obj._values[ 2 ] = gold;
            obj._values[ 3 ] = platinum;
            return obj;
        }

        /// <summary>
        /// Return the number of objects which an object counts as.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetObjectQuantity( Object obj )
        {
            int number = 0;
            if( obj._itemType == ObjTemplate.ObjectType.container )
                foreach( Object obj2 in obj._contains )
                {
                    number += GetObjectQuantity( obj2 );
                }
            else
                number = 1;

            return number;
        }

        /// <summary>
        /// Gives an affect to an object.  Doesn't set any bits on the object.
        /// </summary>
        /// <param name="affect"></param>
        public void AddAffect( Affect affect )
        {
            int i;

            Affect newAffect = new Affect();
            newAffect.Type = affect.Type;
            newAffect.Value = affect.Value;
            newAffect.Modifiers = affect.Modifiers;
            newAffect.Level = affect.Level;
            newAffect.Duration = affect.Duration;
            for( i = 0; i < Limits.NUM_AFFECT_VECTORS; i++ )
            {
                newAffect.BitVectors[ i ] = affect.BitVectors[ i ];
            }
            _affected.Add(newAffect);

            return;
        }

        public bool HasAffect( Affect.AffectType type, string value )
        {
            foreach (Affect affect in _affected)
            {
                if( affect.Type == type && affect.Value == value )
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a character based on an object. Used for animate-object-type spells.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CharData CreateCharacterFromObject( ref Object obj )
        {
            int count;

            MobTemplate mobTemplate = Database.GetMobTemplate( StaticMobs.MOB_NUMBER_OBJECT );
            if( !mobTemplate )
            {
                Log.Error( "CreateCharacterFromObject: null object template.", 0 );
                return null;
            }

            CharData mob = new CharData();

            mob._mobTemplate = mobTemplate;
            mob._name = obj._name;
            mob._shortDescription = obj._shortDescription;
            mob._fullDescription = obj._fullDescription;
            mob._description = obj._fullDescription;
            mob._charClass = mobTemplate.CharacterClass;
            mob._level = Math.Max( obj._level, 1 );
            mob._actionFlags = mobTemplate.ActionFlags;
            mob._position = mobTemplate.DefaultPosition;
            for( count = 0; count < Limits.NUM_AFFECT_VECTORS; ++count )
            {
                mob._affectedBy[ count ] = mobTemplate.AffectedBy[ count ];
            }
            mob._alignment = mobTemplate.Alignment;
            mob._sex = mobTemplate.Gender;
            mob.SetPermRace( mobTemplate.Race );
            mob._size = Race.RaceList[ mob.GetRace() ].DefaultSize;
            if (mob.HasActionBit(MobTemplate.ACT_SIZEMINUS))
                mob._size--;
            if (mob.HasActionBit(MobTemplate.ACT_SIZEPLUS))
                mob._size++;

            mob._castingSpell = 0;
            mob._castingTime = 0;
            mob._permStrength = 55;
            mob._permIntelligence = 55;
            mob._permWisdom = 55;
            mob._permDexterity = 55;
            mob._permConstitution = 55;
            mob._permAgility = 55;
            mob._permCharisma = 55;
            mob._permPower = 55;
            mob._permLuck = 55;
            mob._modifiedStrength = 0;
            mob._modifiedIntelligence = 0;
            mob._modifiedWisdom = 0;
            mob._modifiedDexterity = 0;
            mob._modifiedConstitution = 0;
            mob._modifiedAgility = 0;
            mob._modifiedCharisma = 0;
            mob._modifiedPower = 0;
            mob._modifiedLuck = 0;
            mob._resistant = mobTemplate.Resistant;
            mob._immune = mobTemplate.Immune;
            mob._susceptible = mobTemplate.Susceptible;
            mob._vulnerable = mobTemplate.Vulnerable;
            mob.SetCoins( 0, 0, 0, 0 );
            mob._armorPoints = MUDMath.Interpolate( mob._level, 100, -100 );

            // * MOB HITPOINTS *
            //
            // Was level d 8, upped it to level d 13
            // considering mobs *still* won't have as many hitpoints as some players until
            // at least lvl 10, this shouldn't be too big an upgrade.
            //
            // Mob hitpoints are not based on constitution *unless* they have a
            // constitution modifier from an item, spell, or other affect

            mob._maxHitpoints = mob._level * 100;
            mob._hitpoints = mob.GetMaxHit();

            /*
            * Insert in list.
            */
            Database.CharList.Add( mob );
            // Increment in-game count of mob.
            mobTemplate.NumActive++;
            mob.AddToRoom( obj._inRoom );
            return mob;
        }

        /// <summary>
        /// Strips all affects for a given type/value pair (i.e. all "bless" spells).
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public static void StripAffect( Object obj, Affect.AffectType type, string value )
        {
            if( value == null )
            {
                Log.Error("Invalid call to Object.StripAffect! Must pass non-null value!", 0);
                return;
            }
            foreach (Affect aff in obj._affected.ToArray())
            {
                if( aff.Type == type && aff.Value == value )
                {
                    obj._affected.Remove(aff);
                }
            }

            return;
        }

        /// <summary>
        /// Counts the number of occurrences of an object inside an object list.
        /// </summary>
        /// <param name="objTemplate"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        static public int CountObjectInList( ObjTemplate objTemplate, List<Object> list )
        {
            int nMatch = 0;

            if( objTemplate == null )
            {
                Log.Error("Object.CountObjectInList: called with null ObjIndex", 0);
                return 0;
            }
            if( list == null )
            {
                Log.Error("Object.CountObjectInList: called with null Object list.", 0);
                return 0;
            }

            foreach( Object obj in list )
            {
                if( obj._objIndexData == null )
                {
                    Log.Error("Object.CountObjectInList: Object has no pIndexData", 0);
                    continue;
                }
                if( obj._objIndexData == objTemplate )
                {
                    nMatch++;
                }
            }

            return nMatch;
        }

        /// <summary>
        /// Remove an object from the world and then delete it.
        /// </summary>
        public void RemoveFromWorld()
        {
            // Decrement global object count is handled by destructor.

            if( _inRoom != null )
            {
                // Remove wall flags from exits if we're removing a wall.
                if( _itemType == ObjTemplate.ObjectType.wall )
                {
                    if (InRoom && InRoom.ExitData[Values[0]])
                    {
                        InRoom.ExitData[Values[0]].RemoveFlag(Exit.ExitFlag.walled);
                        InRoom.ExitData[Values[0]].RemoveFlag(Exit.ExitFlag.illusion);
                    }
                }
                RemoveFromRoom();
            }
            if( _carriedBy != null )
            {
                RemoveFromChar();
            }
            if( _inObject != null )
            {
                RemoveFromObject();
            }

            for( int i = (_contains.Count - 1); i >= 0; i-- )
            {
                if (_contains[i] != null)
                {
                    _contains[i].RemoveFromWorld();
                }
            }

            Database.ObjectList.Remove(this);

            return;
        }

        /// <summary>
        /// Checks for object special function activation.
        /// 
        /// TODO: Make sure these are chosen in random order rather than list-first order.
        /// </summary>
        /// <param name="hitFunction"></param>
        /// <returns></returns>
        public bool CheckSpecialFunction(bool hitFunction)
        {
            foreach( ObjSpecial spec in _specFun )
            {
                if( spec.Function( this, _carriedBy, hitFunction ) )
                {
                    return true;
                }
            }
            return false;
        }
    };

}