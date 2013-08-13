using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Cross references a mob special function to its name.
    /// </summary>
    [Serializable]
    public class ObjSpecial
    {
        /// <summary>
        /// Collection of object specials referenced by string.
        /// </summary>
        [XmlIgnore]
        public static Dictionary<string, ObjSpecial> ObjectSpecialTable = new Dictionary<string, ObjSpecial>();
        private string _name;
        private ObjTemplate.ObjFun _function;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ObjSpecial()
        {
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sfun"></param>
        public ObjSpecial(string name, ObjTemplate.ObjFun sfun)
        {
            _name = name;
            _function = sfun;
        }

        /// <summary>
        /// The delegate that executes the object special function.
        /// </summary>
        [XmlIgnore]
        public ObjTemplate.ObjFun Function
        {
            get { return _function; }
            set { _function = value; }
        }

        /// <summary>
        /// The name of the special function.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// String conversion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name;
        }
    };
}