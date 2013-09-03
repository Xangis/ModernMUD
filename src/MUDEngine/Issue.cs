using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents an in-game issue, such as a bug, typo, idea, or other.
    /// </summary>
    public class Issue
    {
        public enum Priority
        {
            none = 0, // Not classified yet.
            highest, // Crash.
            high, // Cheat or exploit.
            medium, // Functionality hindered.
            low, // Cosmetic, includes typos.
            lowest // Ideas and feature requests.
        }
        public enum Type
        {
            none = 0,
            bug,
            typo,
            idea,
            helpentryrequest
        }

        public IssueEntry IssueDetail { get; set; }
        private List<IssueEntry> _updates;
        public IssueEntry Closure { get; set; }
        /// <summary>
        /// Room in which the issue was entered. May or may not be relevant.
        /// </summary>
        public int RoomIndexNumber { get; set; }
        /// <summary>
        /// Did an immortal create the issue?
        /// </summary>
        public bool OpenedByImmortal { get; set; }
        private Type _type;
        public Priority IssuePriority { get; set; }
        /// <summary>
        /// Has the issue been closed or resolved?
        /// </summary>
        public bool Closed { get; set; }
        public int IssueNumber { get; set; }
        private static int _numIssues;

        public Issue()
        {
            ++_numIssues;
            IssueDetail = null;
            _updates = new List<IssueEntry>();
            Closure = null;
            RoomIndexNumber = 0;
            OpenedByImmortal = false;
            _type = Type.none;
            IssuePriority = Priority.none;
            Closed = false;
            // TODO: Add code to check for and eliminate duplicate issue numbers.
            IssueNumber = _numIssues;
        }

        /// <summary>
        /// Destructor, decrements counter.
        /// </summary>
        ~Issue()
        {
            --_numIssues;
        }

        /// <summary>
        /// Count variable.  Keeps track of the number of Issue objects in memory.
        /// </summary>
        public static int Count
        {
            get
            {
                return _numIssues;
            }
        }

        /// <summary>
        /// Gets or sets the list of updates to an issue.
        /// </summary>
        public List<IssueEntry> Updates
        {
            get { return _updates; }
            set { _updates = value; }
        }

        /// <summary>
        /// Gets or sets the type of issue.
        /// </summary>
        public Type IssueType
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Loads all issues into the game engine.
        /// </summary>
        /// <returns></returns>
        public static bool Load()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.IssueFile;
            string blankFilename = FileLocation.BlankSystemFileDirectory + FileLocation.IssueFile;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Issue>));
            XmlTextReader xtr = null;
            try
            {
                try
                {
                    xtr = new XmlTextReader(new StreamReader(filename));
                }
                catch (FileNotFoundException)
                {
                    Log.Info("Issue file not found, using blank file.");
                    File.Copy(blankFilename, filename);
                    xtr = new XmlTextReader(new StreamReader(filename));
                }
                Database.IssueList = (List<Issue>)serializer.Deserialize(xtr);
                xtr.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception in Issue.Load(): " + ex);
                Database.IssueList = new List<Issue>();
                return false;
            }
        }

        /// <summary>
        /// Saves all of the issues to disk.
        /// </summary>
        /// <returns></returns>
        public static bool Save()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.IssueFile;
            XmlTextWriter xtw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof( List<Issue> ) );
                xtw = new XmlTextWriter(new StreamWriter(filename));
                serializer.Serialize(xtw, Database.IssueList);
                xtw.Flush();
                xtw.Close();
                return true;
            }
            catch( Exception ex )
            {
                Log.Error( "Unable to save issues: " + ex );
                if (xtw != null)
                {
                    xtw.Close();
                }
                return false;
            }
        }

        /// <summary>
        /// Finds an issue in the database and returns the formatted text of the issue.
        /// </summary>
        /// <param name="issueNumber"></param>
        /// <returns>A fully-formatted string record containing information about the issue.</returns>
        public static string Format( int issueNumber )
        {
            foreach( Issue issue in Database.IssueList )
            {
                if( issue.IssueNumber == issueNumber )
                {
                    return issue.Format();
                }
            }
            return String.Format("Issue number {0} was not found.\r\n", issueNumber);
        }

        public string Format()
        {
            string text = String.Empty;

            text += String.Format( "Issue Number: {0}   Priority: {1}   Type: {2}\r\n",
                IssueNumber, IssuePriority, _type );
            text += String.Format( "Is Closed: {0}   Opened by Imm: {1}   In Room: {2}\r\n",
                Closed, OpenedByImmortal, RoomIndexNumber );
            text += String.Format( "Created by: {0}   Time: {1}   Text:\r\n{2}\r\n",
                IssueDetail.Name, IssueDetail.UpdateTime.ToString("g"), IssueDetail.Text);
            foreach( IssueEntry entry in _updates )
            {
                text += String.Format( "Entered by: {0}   Time: {1}   Text:\r\n{2}\r\n",
                    entry.Name, entry.UpdateTime.ToString( "g" ), entry.Text );
            }
            if (Closure != null)
            {
                text += String.Format("Closed by: {0}   Time: {1}   Text:\r\n{2}\r\n",
                    Closure.Name, Closure.UpdateTime.ToString("g"), Closure.Text);
            }

            return text;
        }

        /// <summary>
        /// Finds an issue in the database and returns the formatted short text of the issue.
        /// </summary>
        /// <param name="issueNumber"></param>
        /// <returns></returns>
        public static string ShortFormat( int issueNumber )
        {
            foreach( Issue issue in Database.IssueList )
            {
                if( issue.IssueNumber == issueNumber )
                {
                    return issue.ShortFormat();
                }
            }
            return String.Format( "Issue number {0} was not found.\r\n", issueNumber );
        }

        /// <summary>
        /// Returns the short format of an issue.  Intended for use in list of issues.
        /// </summary>
        /// <returns></returns>
        public string ShortFormat()
        {
            string text = String.Empty;
            if (IssueDetail == null)
            {
                return text;
            }

            string summary;

            if( !String.IsNullOrEmpty(IssueDetail.Text) && IssueDetail.Text.Length > 50 )
            {
                summary = IssueDetail.Text.Substring( 0, 50 );
            }
            else
            {
                summary = IssueDetail.Text;
            }

            text += String.Format( "[{0}] ({1}) {2}\r\n", IssueNumber, IssuePriority, summary );

            return text;
        }

        /// <summary>
        /// Shows a list of the current open issues.
        /// TODO: Add sorting and filtering capabilities.
        /// </summary>
        /// <returns></returns>
        public static string ShowIssues()
        {
            string text = String.Empty;

            text += "Issues:\r\n";
            foreach( Issue issue in Database.IssueList )
            {
                if( !issue.Closed )
                {
                    text += issue.ShortFormat();
                }
            }
            if (String.IsNullOrEmpty(text))
            {
                text = "No issues to display.\r\n";
            }
            return text;
        }
    }
}
