using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents a social action in the socials table.
    /// </summary>
    [Serializable]
    public class Social
    {
        private static int _count;
        public string Name { get; set; }
        public string CharNoArgument { get; set; }
        public string OthersNoArgument { get; set; }
        public string CharFound { get; set; }
        public string OthersFound { get; set; }
        public string VictimFound { get; set; }
        public string CharSelf { get; set; }
        public string OthersSelf { get; set; }
        public string AudioFile { get; set; }
        private ActionType _actionType;

        /// <summary>
        /// Action type tells us what type of social it is.  Used so we can have mobs react to
        /// player actions if we want to. 
        /// </summary>
        public enum ActionType
        {
            /// <summary>
            /// Not set or not one of the other types.
            /// </summary>
            none = 0,
            /// <summary>
            /// A friendly social action.
            /// </summary>
            friendly = 1,
            /// <summary>
            /// A neutral action.
            /// </summary>
            neutral = 2,
            /// <summary>
            /// An aggressive action.
            /// </summary>
            aggressive = 3,
            /// <summary>
            /// A greeting.
            /// </summary>
            greeting = 4,
            /// <summary>
            /// A goodbye.
            /// </summary>
            goodbye = 5,
            /// <summary>
            /// An insulting or offensive action.
            /// </summary>
            insulting = 6
        }

        /// <summary>
        /// Constructor.  Initializes things to defaults (blank).
        /// </summary>
        public Social()
        {
            ++_count;
            _actionType = ActionType.none;
            CharNoArgument = String.Empty;
            OthersFound = String.Empty;
            CharSelf = String.Empty;
            OthersNoArgument = String.Empty;
            OthersSelf = String.Empty;
            CharFound = String.Empty;
            Name = String.Empty;
            VictimFound = String.Empty;
            AudioFile = String.Empty;
        }

        /// <summary>
        /// Destructor, decrements counter.
        /// </summary>
        ~Social()
        {
            --_count;
        }

        /// <summary>
        /// Gets the number of socials currently in memory.
        /// </summary>
        public static int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// Gets the type of social action.
        /// </summary>
        public ActionType Type
        {
            get { return _actionType; }
            set { _actionType = value; }
        }

        /// <summary>
        /// Lets us use the not operator on a social to check for null.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static implicit operator bool( Social s )
        {
            if (s == null)
            {
                return false;
            }
            return true;
        }

    }
}