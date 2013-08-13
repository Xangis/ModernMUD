using System;
using System.Xml.Serialization;

namespace ModernMUD
{

    /// <summary>
    /// A piece of 'talk data' - something a mob will say in response to a keyword said by a player.
    /// </summary>
    [Serializable]
    public class TalkData
    {
        private string _keywords;
        private string _message;
        private static int _count;

        /// <summary>
        /// Number of TalkData currently instantianted.
        /// </summary>
        [XmlIgnore]
        static public int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// Keywords used to trigger talk data.
        /// </summary>
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        /// <summary>
        /// Message delivered when keyword is matched.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Allows use of null check in boolean expression.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static implicit operator bool(TalkData t)
        {
            if (t == null)
                return false;
            return true;
        }

        /// <summary>
        /// Reference-counting constructor.
        /// </summary>
        public TalkData()
        {
            ++_count;
        }

        /// <summary>
        /// Exists primarily for display in dialog boxes in the editor.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _keywords + ":" + _message;
        }
    };

}
