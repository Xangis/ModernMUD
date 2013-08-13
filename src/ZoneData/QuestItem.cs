using System;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// An individual piece of data within a quest.  Corresponds to giving/receiving items, experience, etc.
    /// </summary>
    [Serializable]
    public class QuestItem
    {
        private bool _completed;
        private QuestType _type;
        private int _value;
        private static int _count;

        /// <summary>
        /// Type of items gives/received for quests.  Some items, such as skills, will only make sense
        /// when given from the mob to the player.
        /// </summary>
        public enum QuestType
        {
            item,
            money,
            skill,
            experience,
            spell,
            spellcast, // Mob casts spell on player when quest is finished (or player has to cast on mob).
            transfermobs,
            createmob,
            createpet,
            transferplayer,
            transfergroup,
            song,
            faction,
        }

        /// <summary>
        /// The type of quest item this is.
        /// </summary>
        public QuestType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Has this portion of the quest been completed?  This is not saved because
        /// quest completion is only tracked at runtime.
        /// </summary>
        [XmlIgnore]
        public bool Completed
        {
            get { return _completed; }
            set { _completed = value; }
        }

        /// <summary>
        /// The value of the item requested to complete this quest item or given by
        /// the quest item when a quest is completed.  This is a quantity, index #, or
        /// some other value based on the type.
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Gets the number of in-memory representations of a QuestItem.
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
        /// Lets us use if(QuestItem) to check for null.
        /// </summary>
        /// <param name="qi"></param>
        /// <returns></returns>
        public static implicit operator bool(QuestItem qi)
        {
            if (qi == null)
                return false;
            return true;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public QuestItem()
        {
            _completed = false;
            _type = QuestType.item;
            _value = 0;
            ++_count;
        }

        /// <summary>
        /// String conversion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _type.ToString() + ": " + _value.ToString();
        }
    };
}
