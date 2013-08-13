using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// This class is for storing spawn points based on race and class.
    /// </summary>
    [Serializable]
    public class RepopulationPoint
    {
        private int _roomIndexNumber;
        private string _raceString;
        private string _classString;
        private Race _race;
        private CharClass _class;
        /// <summary>
        /// List of repopulation points available in the game.
        /// </summary>
        public static List<RepopulationPoint> RepopList = new List<RepopulationPoint>();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RepopulationPoint()
        {
            _roomIndexNumber = 0;
            _raceString = String.Empty;
            _classString = String.Empty;
            _race = null;
            _class = null;
        }

        /// <summary>
        /// The room location for this repopulation point.
        /// </summary>
        public int RoomIndexNumber
        {
            get { return _roomIndexNumber; }
            set { _roomIndexNumber = value; }
        }

        /// <summary>
        /// The race that this repop point applies to.
        /// </summary>
        public string RaceString
        {
            get { return _raceString; }
            set { _raceString = value; }
        }

        /// <summary>
        /// Race associated with this repopulation point.  Not saved, populated at runtime.
        /// </summary>
        [XmlIgnore]
        public Race Race
        {
            get { return _race; }
            set { _race = value; }
        }

        /// <summary>
        /// Class associated with this repopulation point.  Not saved, populated at runtime.
        /// </summary>
        [XmlIgnore]
        public CharClass CharacterClass
        {
            get { return _class; }
            set { _class = value; }
        }

        /// <summary>
        /// The class that this repop point applies to.
        /// </summary>
        public string ClassString
        {
            get { return _classString; }
            set { _classString = value; }
        }

        /// <summary>
        /// Sets race and class on a repop by looking up the race and class.
        /// </summary>
        public void Setup()
        {
            _race = Race.RaceList[Race.RaceLookup(_raceString)];
            _class = CharClass.ClassList[(int)CharClass.ClassLookup(_classString)];
        }

        /// <summary>
        /// Room index of the respawn point.
        /// </summary>
        public int Room
        {
            get
            {
                return _roomIndexNumber;
            }
        }

        /// <summary>
        /// Race number that the respawn point applies to.
        /// </summary>
        public int RaceNumber
        {
            get
            {
                if (_race != null)
                    return _race.Number;
                return 0;
            }
        }

        /// <summary>
        /// Class number that the respawn point applies to.
        /// </summary>
        public int ClassNumber
        {
            get
            {
                if (_class != null)
                    return (int)_class.ClassNumber;
                return 0;
            }
        }

        /// <summary>
        /// String conversion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _raceString + " " + _classString + " repop in room " + _roomIndexNumber;
        }

    }
}
