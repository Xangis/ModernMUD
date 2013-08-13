using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Main parent quest class for a mob's quest data.
    /// </summary>
    [Serializable]
    public class QuestTemplate
    {
        private int _indexNumber;
        private List<QuestData> _quests = new List<QuestData>();
        private List<TalkData> _messages = new List<TalkData>();
        /// <summary>
        /// List of quest templates.
        /// </summary>
        public static List<QuestTemplate> QuestList = new List<QuestTemplate>();
        private static int _count;

        /// <summary>
        /// Default constructor.  Automatically adds item to the quest list.
        /// </summary>
        public QuestTemplate()
        {
            _indexNumber = 0;
            QuestList.Add(this);
            ++_count;
        }

        /// <summary>
        /// Represents the individual quests in this quest template.
        /// </summary>
        public List<QuestData> Quests
        {
            get { return _quests; }
            set { _quests = value; }
        }

        /// <summary>
        /// Represents the mob virtual number that this quest template is associated with.
        /// </summary>
        public int IndexNumber
        {
            get { return _indexNumber; }
            set { _indexNumber = value; }
        }

        /// <summary>
        /// Represents the quest-related messages in this quest template.
        /// </summary>
        public List<TalkData> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        /// <summary>
        /// The total number of in-memory quest templates.
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
        /// Lets us check for null by calling if(QuestTemplate).
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static implicit operator bool(QuestTemplate q)
        {
            if (q == null)
                return false;
            return true;
        }
    }
}
