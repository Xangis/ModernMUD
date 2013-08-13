using System;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Extra description data for a room or object.
    /// </summary>
    [Serializable]
    public class ExtendedDescription
    {
        private static int _numExtendedDescriptions;
        private string _keyword;     // Keyword in look/examine.
        private string _description; // What is seen.

        /// <summary>
        /// the keyword(s) used to trigger display of the description.
        /// </summary>
        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }

        /// <summary>
        /// The text to be displayed when a keyword is triggered.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ExtendedDescription()
        {
            ++_numExtendedDescriptions;
            _keyword = String.Empty;
            _description = String.Empty;
        }

        /// <summary>
        /// Destructor.  Decrements the count of in-memory instances.
        /// </summary>
        ~ExtendedDescription()
        {
            --_numExtendedDescriptions;

            return;
        }
        
        /// <summary>
        /// ToString exists mainly for dialog box visualization in the editor.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _keyword + ":" + _description;
        }

        /// <summary>
        /// Gets the number of in-memory instances of this type.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return _numExtendedDescriptions;
            }
        }

        /// <summary>
        /// Allows if(ExtendedDescription) to check for NULL.
        /// </summary>
        /// <param name="ed"></param>
        /// <returns></returns>
        public static implicit operator bool( ExtendedDescription ed )
        {
            if( ed == null )
                return false;
            return true;
        }
    };

}