using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace HelpData
{
    /// <summary>
    /// Individual help entries.
    /// </summary>
    public class Help
    {
        private static int _numHelps;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Help()
        {
            ++_numHelps;
            MinimumLevel = 0;
            SeeAlso = String.Empty;
            Syntax = String.Empty;
            Keyword = String.Empty;
            Text = String.Empty;
        }

        /// <summary>
        /// Destructor.  Decrements count.
        /// </summary>
        ~Help()
        {
            --_numHelps;
        }

        /// <summary>
        /// The minimum level that one must be in order to read this help entry.
        /// </summary>
        public int MinimumLevel { get; set; }

        /// <summary>
        /// Other keywords that may be of interest.
        /// </summary>
        public string SeeAlso { get; set; }

        /// <summary>
        /// Usage information.
        /// </summary>
        public string Syntax { get; set; }

        /// <summary>
        /// Keyword for this help entry.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Actual contents of the help entry.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Generates text to display a help entry to the terminal.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("&+y" + Keyword);
            sb.Append("&n\r\n");

            if (!String.IsNullOrEmpty(Syntax))
            {
                sb.Append("&+rSyntax:&n &+c" + Syntax + "&n\r\n");
            }

            sb.Append(Text + "\r\n");

            if (!String.IsNullOrEmpty(SeeAlso))
            {
                sb.Append("&+rSee Also:&n &+c" + SeeAlso + "&n\r\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Keeps track of the total number of helps in existence.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return _numHelps;
            }
        }

        /// <summary>
        /// Loads a help file from disk and returns the contents as a list of Help.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<Help> Load(string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Help>));
                XmlTextReader xtr = new XmlTextReader(new StreamReader(filename));
                List<Help> helps = serializer.Deserialize(xtr) as List<Help>;
                xtr.Close();
                return helps;
            }
            catch (FileNotFoundException)
            {
                Console.Write("ERROR: Help file " + filename + " not found. Continuing without help data.");
                return new List<Help>();
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Exception in Help.Load(): " + ex);
            }
        }

        /// <summary>
        /// Saves all in-memory help entries to disk.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="helpList"></param>
        public static void Save(string filename, List<Help> helpList)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Help>));
            XmlTextWriter xtw = new XmlTextWriter(new StreamWriter(filename));
            serializer.Serialize(xtw, helpList);
            xtw.Flush();
            xtw.Close();
        }
    }
}