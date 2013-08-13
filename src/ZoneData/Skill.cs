using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ModernMUD
{
    /// <summary>
    /// Represents a player skill.
    /// </summary>
    [Serializable]
    public class Skill
    {
        private string _name;
        private int[] _classAvailability = new int[Limits.MAX_CLASS];
        private int _delay;
        private string _damageText;
        private string _wearOffMessage;
        private int _aiChance;
        private int _aiPower;
        private string _fileName;
        private string _code;
        private static Dictionary<String, Skill> _skillList = new Dictionary<String, Skill>();

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Skill()
        {
        }
        
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="lag"></param>
        /// <param name="dmg"></param>
        /// <param name="wearoff"></param>
        public Skill(string nam, int lag, string dmg, string wearoff)
        {
            _name = nam;
            // Using the count is an easy way to keep track because the only skill instantiation is
            // done from the const array.
            _delay = lag;
            _damageText = dmg;
            _wearOffMessage = wearoff;
            _classAvailability = new int[Limits.MAX_CLASS];
            _aiChance = 30;
            _aiPower = 50;
            _code = String.Empty;
            if (!String.IsNullOrEmpty(wearoff) && wearoff.StartsWith("!"))
            {
                _wearOffMessage = String.Empty;
            }
            int count;
            for (count = 0; count < CharClass.ClassList.Length; count++)
            {
                _classAvailability[count] = Limits.LEVEL_LESSER_GOD;
            }
        }

        /// <summary>
        /// The skill dictionary.  Used for looking up skills based on their name.
        /// </summary>
        [XmlIgnore]
        public static Dictionary<String, Skill> SkillList
        {
            get { return _skillList; }
            set { _skillList = value; }
        }

        /// <summary>
        /// The source code associated with this skill.
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// The per-class availability of the skill, stored as the level at which the
        /// class gets the skill.  This is built dynamically post-boot based on the
        /// information contained in the loaded class files.
        /// </summary>
        [XmlIgnore]
        public int[] ClassAvailability
        {
            get { return _classAvailability; }
            set { _classAvailability = value; }
        }

        /// <summary>
        /// The file name of this skill.
        /// </summary>
        [XmlIgnore]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        /// <summary>
        /// The amount of time that this skill takes to perform and recover from.
        /// </summary>
        public int Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        /// <summary>
        /// The text used when damaging a target with this skill.
        /// </summary>
        public string DamageText
        {
            get { return _damageText; }
            set { _damageText = value; }
        }

        /// <summary>
        /// The message displayed when this skill wears off.
        /// </summary>
        public string WearOffMessage
        {
            get { return _wearOffMessage; }
            set { _wearOffMessage = value; }
        }

        /// <summary>
        /// The power level of this skill, used when calculating actions for the
        /// artificial intelligence engine.  More powerful skills will tend to be
        /// preferred over those that are less powerful.
        /// </summary>
        public int AIPower
        {
            get { return _aiPower; }
            set { _aiPower = value; }
        }

        /// <summary>
        /// The chance of an artificial intelligence using this skill when other
        /// factors have determined it to be appropriate to use.
        /// </summary>
        public int AIChance
        {
            get { return _aiChance; }
            set { _aiChance = value; }
        }

        /// <summary>
        /// The string representation of this skill.  Displays terminal-friendly
        /// information.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append( String.Format("Skill: '{0}' Lag: {1}\r\n", _name, _delay));
            output.Append("\r\nClass Availability:\r\n");
            for (int count = 0; count < CharClass.ClassList.Length; ++count)
            {
                output.Append(String.Format("  {0,-16}: {1}", CharClass.ClassList[count].Name,
                    _classAvailability[count]));
                if (count % 3 == 2)
                    output.Append("\r\n");
            }
            output.Append("\r\n");
            return output.ToString();
        }

        /// <summary>
        /// Gets the number of skills in memory.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get { return SkillList.Count; }
        }

        /// <summary>
        /// The name of the skill.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Loads all skills from disk using the "skill load list" file.
        /// </summary>
        /// <returns></returns>
        public static bool LoadSkills()
        {
            string skillList = String.Format("{0}{1}", FileLocation.SkillDirectory, FileLocation.SkillLoadList);
            try
            {
                FileStream fpList = File.OpenRead(skillList);
                StreamReader sr = new StreamReader(fpList);

                while (!sr.EndOfStream)
                {
                    string filename = sr.ReadLine();

                    if (filename[0] == '$')
                    {
                        break;
                    }

                    if (!Load(FileLocation.SkillDirectory + filename))
                    {
                        string bugbuf = "Cannot load skill file: " + filename;
                        throw new Exception(bugbuf);
                    }
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Saves in-memory skills to disk.
        /// </summary>
        /// <returns></returns>
        public static bool SaveSkills()
        {
            string skillList = String.Empty;
            foreach (KeyValuePair<String, Skill> kvp in _skillList)
            {
                kvp.Value.Save();
                skillList += kvp.Value._fileName + "\n";
            }
            skillList += "$";
            FileStream fpList = File.OpenWrite(FileLocation.SkillDirectory + FileLocation.SkillLoadList);
            StreamWriter sw = new StreamWriter(fpList);
            sw.Write(skillList);
            sw.Flush();
            sw.Close();
            return true;
        }

        /// <summary>
        /// Loads an individual skill from a file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public bool Load(string filename)
        {
            Stream stream = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Skill));
                stream = new FileStream(filename, FileMode.Open,
                    FileAccess.Read, FileShare.None);
                Skill skill = (Skill)serializer.Deserialize(stream);
                skill._fileName = filename;
                stream.Close();
                _skillList.Add(skill._name, skill);
                return true;
            }
            catch (ArgumentException)
            {
                if (stream != null)
                {
                    stream.Close();
                }
                throw new Exception("Attempted to load a skill with a duplicate name.  Please check " + FileLocation.SkillLoadList + " to make sure that the same skill file isn't listed twice.");
            }
            catch (Exception ex)
            {
                if (stream != null)
                {
                    stream.Close();
                }
                throw new Exception("Exception loading skill file: " + filename + " Details: " + ex);
            }
        }

        /// <summary>
        /// Instance method that saves an individual skill file to disk.
        /// </summary>
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            Stream stream = new FileStream(FileLocation.SkillDirectory + _fileName, FileMode.Create,
                FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, this);
            stream.Close();
        }


        /// <summary>
        /// Lookup a skill by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Skill SkillLookup(string name)
        {
            foreach (KeyValuePair<String, Skill> kvp in _skillList)
            {
                if (kvp.Value._name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase))
                    return kvp.Value;
            }

            return null;
        }


    };

}