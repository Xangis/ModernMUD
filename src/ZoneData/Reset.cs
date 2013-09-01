using System;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Area Reset definition - used for loading mobs, objects, and performing other load/Reset-time tasks.
    /// 
    /// The declaration of these is derived from the Basternae 2 area format. Commands are:
    ///    '*': Comment
    ///    'M': Load a mobile 
    ///    'O': Load an object
    ///    'P': Put object in object
    ///    'G': Give object to mobile
    ///    'E': Equip object to mobile
    ///    'D': Set state of door
    ///    'R': Randomize room exits
    ///    'S': Stop (end of reset definitions)
    ///    
    /// A reset also contains 8 value arguments that are used to define different things based on
    /// the type of reset. Values 6 through 8 have never been used for anything and can probably be
    /// removed safely, but that would change the area format. We don't like changing area formats.
    /// </summary>
    [Serializable]
    public class Reset
    {
        private char _command;
        private int _arg0;
        private int _arg1;
        private int _arg2;
        private int _arg3;
        private int _arg4;
        private int _arg5;
        private int _arg6;
        private int _arg7;
        private static int _numResets;

        /// <summary>
        /// The command key letter for this reset.
        /// </summary>
        public char Command
        {
            get { return _command; }
            set { _command = value; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Reset()
        {
            ++_numResets;
            _command = '*';
            _arg0 = 0;
            _arg1 = 0;
            _arg2 = 0;
            _arg3 = 0;
            _arg4 = 0;
            _arg5 = 0;
            _arg6 = 0;
            _arg7 = 0;
        }

        /// <summary>
        /// Destructor -- decrements the in-memory object count.
        /// </summary>
        ~Reset()
        {
             ++_numResets;
        }

        /// <summary>
        /// The number of in-memory objects of this type.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return _numResets;
            }
        }

        /// <summary>
        /// Argument 0.
        /// </summary>
        public int Arg0
        {
            get { return _arg0; }
            set { _arg0 = value; }
        }

        /// <summary>
        /// Argument 1.
        /// </summary>
        public int Arg1
        {
            get { return _arg1; }
            set { _arg1 = value; }
        }

        /// <summary>
        /// Argument 2.
        /// </summary>
        public int Arg2
        {
            get { return _arg2; }
            set { _arg2 = value; }
        }

        /// <summary>
        /// Argument 3.
        /// </summary>
        public int Arg3
        {
            get { return _arg3; }
            set { _arg3 = value; }
        }

        /// <summary>
        /// Argument 4.
        /// </summary>
        public int Arg4
        {
            get { return _arg4; }
            set { _arg4 = value; }
        }

        /// <summary>
        /// Argument 5.
        /// </summary>
        public int Arg5
        {
            get { return _arg5; }
            set { _arg5 = value; }
        }

        /// <summary>
        /// Argument 6.
        /// </summary>
        public int Arg6
        {
            get { return _arg6; }
            set { _arg6 = value; }
        }

        /// <summary>
        /// Argument 7.
        /// </summary>
        public int Arg7
        {
            get { return _arg7; }
            set { _arg7 = value; }
        } 

        /// <summary>
        /// Gets a terminal-friendly description of the reset.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            switch (_command)
            {
                default:
                    return "Invalid Reset - does nothing.";
                case '*':
                    return "Comment - does nothing.";
                case 'M':
                    return "Load mobile " + _arg1 + " in room " + _arg3 + ".";
                case 'O':
                    return "Load object " + _arg1 + " in room " + _arg3 + ".";
                case 'P':
                    return "Put object " + _arg1 + " in object " + _arg3 + " in room " + _arg5 + ".";
                case 'G':
                    return "Give object " + _arg1 + " to mobile " + _arg5 + " in room " + _arg3 + ".";
                case 'E':
                    return "Equip object " + _arg1 + " on mobile " + _arg5 + " in room " + _arg4 + " on eq slot " + _arg3 + " (" + ((ObjTemplate.WearLocation)_arg3).ToString() +").";
                case 'D':
                    return "Set the state of the " + ((Exit.Direction)_arg2).ToString() + " door in room " + _arg1 + " to " + _arg3 + " (" + ((Exit.ExitFlag)_arg3).ToString() + ").";
                case 'R':
                    return "Randomize exits in room " + _arg3 + ".";
                case 'S':
                    return "Stop - end of Reset list.";
                case 'F':
                    return "Set mob " + _arg1 + " to follow previous mob in room " + _arg3 + ".";
            }
        }

        /// <summary>
        /// Gets whether this particular Reset applies to the given room.  Used mainly for room-only resets.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool IsRoomReset(RoomTemplate room)
        {
            switch (_command)
            {
                default:
                    return false;
                case 'S':
                case '*':
                    return false;
                case 'M':
                case 'O':
                case 'G':
                case 'R':
                case 'F':
                    if (_arg3 == room.IndexNumber)
                    {
                        return true;
                    }
                    return false;
                case 'E':
                    if (_arg4 == room.IndexNumber)
                    {
                        return true;
                    }
                    return false;
                case 'P':
                    if (_arg5 == room.IndexNumber)
                    {
                        return true;
                    }
                    return false;
                case 'D':
                    if (_arg1 == room.IndexNumber)
                    {
                        return true;
                    }
                    return false;
            }
        }
    }
}