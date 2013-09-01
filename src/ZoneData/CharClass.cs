using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace ModernMUD
{
    /// <summary>
    /// Represents a player-character class (profession).
    /// 
    /// TODO: Find a way to make class definitions more dynamic, load-time values rather
    /// than having to have hard-coded enums.
    /// </summary>
    public class CharClass
    {
        private string _name;
        private Names _classNumber;
        private string _filename;
        private float[] _saveModifiers = new float[6];
        private List<SkillEntry> _skills = new List<SkillEntry>();
        private List<SpellEntry> _spells = new List<SpellEntry>();
        /// <summary>
        /// List of character classes available in the game.
        /// </summary>
        public static CharClass[] ClassList = new CharClass[Limits.MAX_CLASS];
        List<CustomAction> _customActions;

        /// <summary>
        /// NOTE: This ABSOLUTELY MUST match the Names enum.
        /// </summary>
        [Flags]
        public enum Available
        {
            none = 0,
            warrior = Bitvector.BV00,
            sorcerer = Bitvector.BV01,
            psionicist = Bitvector.BV02,
            cleric = Bitvector.BV03,
            thief = Bitvector.BV04,
            assassin = Bitvector.BV05,
            mercenary = Bitvector.BV06,
            paladin = Bitvector.BV07,
            antipaladin = Bitvector.BV08,
            ranger = Bitvector.BV09,
            elementEarth = Bitvector.BV10,
            elementAir = Bitvector.BV11,
            elementFire = Bitvector.BV12,
            elementWater = Bitvector.BV13,
            shaman = Bitvector.BV14,
            druid = Bitvector.BV15,
            bard = Bitvector.BV16,
            hunter = Bitvector.BV17,
            illusionist = Bitvector.BV18,
            monk = Bitvector.BV19,
            mystic = Bitvector.BV20,
            necromancer = Bitvector.BV21,
            chronomancer = Bitvector.BV22,
            warlock = Bitvector.BV23, // Neogi Class, mana-based
            enslaver = Bitvector.BV24, // Illithid only class
            reaver = Bitvector.BV25, // Neogi Class, hitter type
            enchanter = Bitvector.BV26
        }

        /// <summary>
        /// NOTE: This ABSOLUTELY MUST match the Available enum.
        /// </summary>
        public enum Names
        {
            none = 0,
            warrior = 1,
            sorcerer = 2,
            psionicist = 3,
            cleric = 4,
            thief = 5,
            assassin = 6,
            mercenary = 7,
            paladin = 8,
            antipaladin = 9,
            ranger = 10,
            elementEarth = 11,
            elementAir = 12,
            elementFire = 13,
            elementWater = 14,
            shaman = 15,
            druid = 16,
            bard = 17,
            hunter = 18,
            illusionist = 19,
            monk = 20,
            mystic = 21,
            necromancer = 22,
            chronomancer = 23,
            warlock = 24, // Neogi Class, mana-based
            enslaver = 25, // Illithid only class
            reaver = 26, // Neogi Class, hitter type
            enchanter = 27
        }

        /// <summary>
        /// Does the class memorize spells? If so, are they a lesser caster or a full-fledged caster?
        /// </summary>
        public enum MemorizationType
        {
            None,
            Lesser,
            Full
        }

        /// <summary>
        /// The position of this class in the class table.
        /// </summary>
        public Names ClassNumber
        {
            get { return _classNumber; }
            set { _classNumber = value; }
        }

        /// <summary>
        /// The hitroll for this class at level 0.  This is the percent chance of missing
        /// before any modifiers are applied.
        /// </summary>
        public int HitrollLevel0 { get; set; }

        /// <summary>
        /// The hitroll for this class at level 40.  This is the percent chance of missing
        /// before any modifiers are applied.
        /// </summary>
        public int HitrollLevel40 { get; set; }

        /// <summary>
        /// The starting weapon index number for this class.
        /// </summary>
        public int FirstWeapon { get; set; }

        /// <summary>
        /// Flag that determines whether this class gains and uses mana.
        /// </summary>
        public bool GainsMana { get; set; }

        /// <summary>
        /// The memorization type for this class, either lesser, full, or none.  Determines how many
        /// spells per level can be cast.
        /// </summary>
        public MemorizationType MemType { get; set; }

        /// <summary>
        /// The saving throw modifiers for this class.
        /// </summary>
        public float[] SaveModifiers
        {
            get { return _saveModifiers; }
            set { _saveModifiers = value; }
        }

        /// <summary>
        /// The skills available to this class.
        /// </summary>
        public List<SkillEntry> Skills
        {
            get { return _skills; }
            set { _skills = value; }
        }

        /// <summary>
        /// Custom race-based actions -- AI, etc.
        /// </summary>
        public List<CustomAction> CustomActions
        {
            get { return _customActions; }
            set { _customActions = value; }
        }

        /// <summary>
        /// The minimum number of hitpoints gained per level.
        /// </summary>
        public int MinHpGain { get; set; }

        /// <summary>
        /// The maximum number of hitpoints gained per level.
        /// </summary>
        public int MaxHpGain { get; set; }

        /// <summary>
        /// The spells available to this class.
        /// </summary>
        public List<SpellEntry> Spells
        {
            get { return _spells; }
            set { _spells = value; }
        }

        /// <summary>
        /// The character class's name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The primary (most important) attribute for this class.
        /// </summary>
        public Affect.Apply PrimeAttribute { get; set; }

        /// <summary>
        /// The file that the class definition is stored in.
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        /// <summary>
        /// The three-letter name displayed on the "who list".
        /// </summary>
        public string WholistName { get; set; }

        /// <summary>
        /// The experience modifier for this class.  Higher numbers make it harder to advance.
        /// </summary>
        public double ExperienceModifier { get; set; }

        /// <summary>
        /// Used for paladin and antipaladin summoning of mounts (warhorses). Can also be used
        /// to let the class summon other mounts. Class also needs the "summon mount" skill for
        /// this to have any effect.
        /// </summary>
        public int CanSummonMountNumber { get; set; }

        /// <summary>
        /// The count of in-memory character classes.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return ClassList.Length;
            }
        }

        /// <summary>
        /// Loads the available classes using the class list file.  Links and validates
        /// skills if validateSkills is true.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public bool Load(string filename, bool validateSkills)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof( CharClass ) );
                Stream stream = new FileStream( filename, FileMode.Open,
                    FileAccess.Read, FileShare.None );
                CharClass charclass = (CharClass)serializer.Deserialize( stream );

                stream.Close();
                if( (int)charclass._classNumber < ClassList.Length )
                {
                    if (ClassList[(int)charclass._classNumber] != null)
                    {
                        throw new Exception("Class.Load(): Class number " + (int)charclass._classNumber +
                            " already taken by " + ClassList[(int)charclass._classNumber]._name + ".");
                    }
                    ClassList[ (int)charclass._classNumber ] = charclass;
                    // Set availability in the skills table.
                    if (validateSkills)
                    {
                        foreach (SkillEntry skill in charclass._skills)
                        {
                            Skill skl = Skill.SkillLookup(skill.Name);
                            if (skl != null)
                            {
                                skl.ClassAvailability[(int)charclass._classNumber] = skill.Level;
                            }
                            else
                            {
                                throw new Exception("CharClass.Load(): Skill '" + skill.Name + "' not found in game engine while loading class " + charclass._name + " (" + filename + ").");
                            }
                        }
                    }
                    return true;
                }
                throw new Exception("Failed to load class " + filename);
            }
            catch( Exception ex )
            {
                throw new Exception("Exception loading class file: " + filename + ex);
            }
        }

        /// <summary>
        /// Instance method to save a class to a file.
        /// </summary>
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer( GetType() );
            Stream stream = new FileStream(FileLocation.ClassDirectory + _filename, FileMode.Create,
                FileAccess.Write, FileShare.None );
            serializer.Serialize( stream, this );
            stream.Close();
        }

        /// <summary>
        /// Saves all classes to disk.
        /// </summary>
        public void SaveClasses()
        {
            foreach (CharClass charclass in ClassList)
            {
                charclass.Save();
            }
        }

        /// <summary>
        /// Find a class based on its name.
        /// </summary>
        /// <param name="charclass"></param>
        /// <returns></returns>
        public static CharClass.Names ClassLookup(string charclass)
        {
            for (int index = 0; index < ClassList.Length; index++)
            {
                if (ClassList[index]._name.StartsWith(charclass, StringComparison.CurrentCultureIgnoreCase))
                {
                    return (CharClass.Names)index;
                }
            }

            // Default to none on failed lookup.
            return 0;
        }

        /// <summary>
        /// Loads in all of the class files.  Validates skills if validateSkills is true.
        /// It should only be false when not running the MUD engine, i.e. when using the
        /// zone editor.
        /// </summary>
        public static bool LoadClasses(bool validateSkills)
        {
            string classlist = String.Format("{0}{1}", FileLocation.ClassDirectory, FileLocation.ClassLoadList);
            try
            {
                FileStream fpList = File.OpenRead( classlist );
                StreamReader sr = new StreamReader( fpList );

                while( !sr.EndOfStream )
                {
                    string filename = sr.ReadLine();

                    if( filename[ 0 ] == '$' )
                    {
                        break;
                    }

                    if (!Load(FileLocation.ClassDirectory + filename, validateSkills))
                    {
                        string bugbuf = "Cannot load class file: " + filename;
                        throw new Exception(bugbuf);
                    }
                }
                sr.Close();
            }
            catch( Exception ex )
            {
                throw new Exception("Exception in CharClass.LoadClasses(): " + ex);
            }
            return true;
        }
    }
}