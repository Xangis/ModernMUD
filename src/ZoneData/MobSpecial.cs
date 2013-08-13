using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ModernMUD
{
    /// <summary>
    /// This class is used to cross refrence mob special functions to their names.
    /// </summary>
    [Serializable]
    public class MobSpecial
    {
        private string _specName;
        private MobTemplate.MobFun _specFunction;
        /// <summary>
        /// Dictionary of mob specials referenced by string.
        /// </summary>
        public static Dictionary<string, MobSpecial> MobSpecialTable = new Dictionary<string, MobSpecial>();

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="specname"></param>
        /// <param name="specfun"></param>
        public MobSpecial( string specname, MobTemplate.MobFun specfun )
        {
            _specName = specname;
            _specFunction = specfun;
        }

        /// <summary>
        /// Deafult constructor.
        /// </summary>
        public MobSpecial()
        {
        }

        /// <summary>
        /// The name of the mob special.
        /// </summary>
        public string SpecName
        {
            get { return _specName; }
            set { _specName = value; }
        }

        /// <summary>
        /// The delegate that executes the special function.
        /// </summary>
        [XmlIgnore]
        public MobTemplate.MobFun SpecFunction
        {
            get { return _specFunction; }
            set { _specFunction = value; }
        }

        /// <summary>
        /// Lets us check for null by using if(MobSpecial)
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static implicit operator bool(MobSpecial ms)
        {
            if( ms == null )
                return false;
            return true;
        }

        /// <summary>
        /// String conversion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _specName;
        }

        /// <summary>
        /// Gets a list of MobSpecial from a space-separated string.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<MobSpecial> SpecMobLookup(string name)
        {
            List<MobSpecial> functions = new List<MobSpecial>();

            String[] names = name.Split(' ');

            foreach (String str in names)
            {
                if( MobSpecialTable.ContainsKey(str))
                    functions.Add(MobSpecialTable[str]);
            }
            return functions;
        }

    };

}