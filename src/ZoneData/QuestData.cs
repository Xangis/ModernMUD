using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// An individual quest within a mob's parent quest data.
    /// </summary>
    [Serializable]
    public class QuestData
    {
        private List<QuestItem> _receive = new List<QuestItem>();
        private List<QuestItem> _give = new List<QuestItem>();
        private string _disappear;
        private string _complete;
        private static int _count;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public QuestData()
        {
            _disappear = String.Empty;
            _complete = String.Empty;
            ++_count;
        }

        /// <summary>
        /// Represents the items that need to be received in order to complete the quest.
        /// </summary>
        public List<QuestItem> Receive
        {
            get { return _receive; }
            set { _receive = value; }
        }

        /// <summary>
        /// Represents the items given to a player when the quest is completed.
        /// </summary>
        public List<QuestItem> Give
        {
            get { return _give; }
            set { _give = value; }
        }

        /// <summary>
        /// Represents the number of in-memory instances of this class.
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
        /// The message displayed when the quest mob disappears.
        /// </summary>
        public string Disappear
        {
            get { return _disappear; }
            set { _disappear = value; }
        }

        /// <summary>
        /// The message displayed when the quest is completed.
        /// </summary>
        public string Complete
        {
            get { return _complete; }
            set { _complete = value; }
        }

        /// <summary>
        /// Lets us use if(QuestData) to check for NULL.
        /// </summary>
        /// <param name="qd"></param>
        /// <returns></returns>
        public static implicit operator bool(QuestData qd)
        {
            if (qd == null)
                return false;
            return true;
        }

        /// <summary>
        /// Exists primarily for showing a summary in a list box in the editor.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "Quest for ";
            foreach (QuestItem item in _give)
            {
                str += " " + item.Type + ":" + item.Value;
            }
            return str;
        }
    };
}
