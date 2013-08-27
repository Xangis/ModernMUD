using System;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Exit data. Joins rooms - used for getting from one room to another.  Includes both doors and open spaces.
    /// </summary>
    [Serializable]
    public class Exit
    {
        [XmlIgnore]
        private RoomTemplate _targetRoom;
        private int _indexNumber;
        private ExitFlag _exitFlags;
        private int _key;
        private string _keyword;
        private string _description;
        private static int _numExits;

        /// <summary>
        /// Movement directions.
        /// </summary>
        public enum Direction
        {
            invalid = -1,
            north = 0,
            east = 1,
            south = 2,
            west = 3,
            up = 4,
            down = 5,
            northwest = 6,
            southwest = 7,
            northeast = 8,
            southeast = 9
        }

        /// <summary>
        /// Exit flags.  Used to determine whether the exit has a door, is secret,
        /// locked, et cetera.
        /// </summary>
        [Flags]
        public enum ExitFlag
        {
            /// <summary>
            /// Plain exit, no door.
            /// </summary>
            none = 0,
            /// <summary>
            /// Is a door. Required for many of the other flags to have meaning.
            /// </summary>
            is_door = Bitvector.BV00,
            /// <summary>
            /// Exit is closed.
            /// </summary>
            closed = Bitvector.BV01,
            /// <summary>
            /// Exit is locked.
            /// </summary>
            locked = Bitvector.BV02,
            /// <summary>
            /// Exit is secret (hidden).
            /// </summary>
            secret = Bitvector.BV03,
            /// <summary>
            /// Exit is blocked.
            /// </summary>
            blocked = Bitvector.BV04,
            /// <summary>
            /// Lock cannot be picked.
            /// </summary>
            pickproof = Bitvector.BV05,
            /// <summary>
            /// Exit is covered by a wall.
            /// </summary>
            walled = Bitvector.BV06,
            /// <summary>
            /// Exit is covered in spikes.
            /// </summary>
            spiked = Bitvector.BV07,
            /// <summary>
            /// Exit is illusionary.
            /// </summary>
            illusion = Bitvector.BV08,
            /// <summary>
            /// Exit is a door that has been bashed from its hinges.
            /// </summary>
            bashed = Bitvector.BV09,
            /// <summary>
            /// Exit cannot be bashed.
            /// </summary>
            bashproof = Bitvector.BV10,
            /// <summary>
            /// Exit cannot be passed through.
            /// </summary>
            passproof = Bitvector.BV11,
            /// <summary>
            /// Exit is trapped.
            /// </summary>
            trapped = Bitvector.BV12,
            /// <summary>
            /// Exit destroys the key when unlocked.
            /// </summary>
            destroys_key = Bitvector.BV13,
            /// <summary>
            /// Exit is jammed.
            /// </summary>
            jammed = Bitvector.BV14
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Exit()
        {
            ++_numExits;
            _description = String.Empty;
            _exitFlags = 0;
            _key = 0;
            _keyword = String.Empty;
            _targetRoom = null;
            _indexNumber = 0;
        }

        /// <summary>
        /// Destructor, decrements memory usage counter.
        /// </summary>
        ~Exit()
        {
            --_numExits;
        }

        /// <summary>
        /// Points to the room one will end up in after passing through the exit.
        /// </summary>
        public RoomTemplate TargetRoom
        {
            get { return _targetRoom; }
            set { _targetRoom = value; }
        }

        /// <summary>
        /// The virtual number of the target room.  Used for linking exits to
        /// rooms after the exits have been loaded.
        /// </summary>
        public int IndexNumber
        {
            get { return _indexNumber; }
            set { _indexNumber = value; }
        }

        /// <summary>
        /// Represents the object index number of the key required to unlock this
        /// exit, if any.
        /// </summary>
        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// the keyword for the exit.  Used when looking, walling, or using a key.
        /// </summary>
        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }

        /// <summary>
        /// The text description to be shown when one looks at the exit.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Represents the attributes (flags) for an exit.
        /// </summary>
        public ExitFlag ExitFlags
        {
            get { return _exitFlags; }
            set { _exitFlags = value; }
        }

        /// <summary>
        /// Gets the number of in-memory objects of this type.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return _numExits;
            }
        }

        /// <summary>
        /// Allows checking for null using the NOT operator.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool operator !( Exit e )
        {
            if( e == null )
                return true;
            return false;
        }

        /// <summary>
        /// Allows if(Exit) to return false for NULL values and true for non-NULL.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static implicit operator bool( Exit e )
        {
            if( e == null )
                return false;
            return true;
        }

        /// <summary>
        /// Checks for the existence of a particular flag on an exit.
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool HasFlag(ExitFlag flag)
        {
            return ((int)(_exitFlags & flag) != 0);
        }

        /// <summary>
        /// Adds a flag to an exit.
        /// </summary>
        /// <param name="flag"></param>
        public void AddFlag(ExitFlag flag)
        {
            _exitFlags = _exitFlags | flag;
        }

        /// <summary>
        /// Removes a flag from an exit.
        /// </summary>
        /// <param name="flag"></param>
        public void RemoveFlag(ExitFlag flag)
        {
            _exitFlags = _exitFlags & (~(flag));
        }

        /// <summary>
        /// Gets the name of the direction opposite the supplied direction.
        /// Used primarily for direction someone enters a room from.
        /// </summary>
        public static string[] ReverseDirectionName = new[]     
        {
            "the south", "the west", "the north", "the east", "below", "above",
            "the southeast", "the northeast", "the southwest", "the southwest"
        };

        /// <summary>
        /// Gets the opposite direction number based on an exit direction.
        /// </summary>
        private static Direction[] _reverseDirection = new[]     
        {
            Direction.south, Direction.west, Direction.north, Direction.east,
            Direction.down, Direction.up, Direction.southeast, Direction.northeast,
            Direction.southwest, Direction.northwest
        };

        /// <summary>
        /// Does a reverse direction lookup.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static Direction ReverseDirection(Direction dir)
        {
            return _reverseDirection[(int)dir];
        }

        /// <summary>
        /// Returns the door number based on the provided string
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>The direction number, or -1 if no match was found.</returns>
        public static Exit.Direction DoorLookup(string arg)
        {
            if (String.IsNullOrEmpty(arg))
            {
                return Exit.Direction.invalid;
            }
            if ("north".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.north;
            }
            if ("east".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.east;
            }
            if ("south".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.south;
            }
            if ("west".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.west;
            }
            if ("up".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.up;
            }
            if ("down".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.down;
            }
            if ("northwest".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.northwest;
            }
            if ("southwest".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.southwest;
            }
            if ("northeast".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.northeast;
            }
            if ("southeast".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.southeast;
            }
            if ("nw".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.northwest;
            }
            if ("sw".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.southwest;
            }
            if ("ne".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.northeast;
            }
            if ("se".StartsWith(arg, StringComparison.CurrentCultureIgnoreCase))
            {
                return Exit.Direction.southeast;
            }
            return Exit.Direction.invalid;
        }

        /// <summary>
        /// Check whether an exit flag is set.  Currently only handles a single set
        /// of bitvectors, which is fine since we haven't filled it even halfway yet.
        /// </summary>
        /// <param name="bvect"></param>
        /// <returns></returns>
        public bool HasFlag(Bitvector bvect)
        {
            return HasFlag(bvect.Vector);
        }

        /// <summary>
        /// Check whether an exit flag is set.
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool HasFlag(int flag)
        {
            if (((int)ExitFlags & flag) != 0)
            {
                return true;
            }
            return false;
        }
    }
}