using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace ModernMUD
{
    /// <summary>
    /// Represents a "screen", whether it be an intro screen, race selection screen,
    /// class selection screen, or menu. Screens are stored in binary form in order
    /// to avoid the ugliness that can come with loading/saving characters that XML
    /// does not like.
    /// </summary>
    [Serializable]
    public class Screen
    {
        /// <summary>
        /// ANSI and ASCII greeting screen variables.  Filled with a simple string in case
        /// we're unable to load the greetings files.
        /// </summary>
        public static List<Screen> ScreenList = new List<Screen>();
        private static Screen _introScreenMonochrome;
        private static Screen _introScreenColor;
        private static Screen _raceSelectionScreenColor;
        private static Screen _raceSelectionScreenMonochrome;
        private static Screen _mainMenuScreen;
        private string _name;
        private string _contents;
        private bool _active;
        private ScreenType _type;

        /// <summary>
        /// Defines the different types of screens.
        /// </summary>
        public enum ScreenType
        {
            /// <summary>
            /// An uncategorized screen.
            /// </summary>
            none = 0,
            /// <summary>
            /// Intro screen (color)
            /// </summary>
            intro = 1,
            /// <summary>
            /// Intro screen (black and white)
            /// </summary>
            monochrome_intro = 2,
            /// <summary>
            /// Race selection screen (color)
            /// </summary>
            races = 3,
            /// <summary>
            /// Race selection screen (black and white)
            /// </summary>
            monochrome_races = 4,
            /// <summary>
            /// Menu screen
            /// </summary>
            menu = 5,
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Screen()
        {
            _name = String.Empty;
            _contents = String.Empty;
            _type = ScreenType.none;
            _active = false;
        }

        /// <summary>
        /// Parameterized contstructor.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public Screen( string text, string name, ScreenType type )
        {
            _name = name;
            _contents = text;
            _type = type;
            _active = false;
        }

        /// <summary>
        /// The race selection screen, color version.
        /// </summary>
        [XmlIgnore]
        public static Screen RaceSelectionScreenColor
        {
            get { return _raceSelectionScreenColor; }
            set { _raceSelectionScreenColor = value; }
        }

        /// <summary>
        /// The race selection screen, plain text version.
        /// </summary>
        [XmlIgnore]
        public static Screen RaceSelectionScreenMonochrome
        {
            get { return _raceSelectionScreenMonochrome; }
            set { _raceSelectionScreenMonochrome = value; }
        }

        /// <summary>
        /// The greeting screen, monochrome version.
        /// </summary>
        [XmlIgnore]
        public static Screen IntroScreenMonochrome
        {
            get { return _introScreenMonochrome; }
            set { _introScreenMonochrome = value; }
        }

        /// <summary>
        /// The main game menu screen.
        /// </summary>
        [XmlIgnore]
        public static Screen MainMenuScreen
        {
            get { return _mainMenuScreen; }
            set { _mainMenuScreen = value; }
        }

        /// <summary>
        /// The greeting screen, colorized version.
        /// </summary>
        [XmlIgnore]
        public static Screen IntroScreenColor
        {
            get { return _introScreenColor; }
            set { _introScreenColor = value; }
        }
        
        /// <summary>
        /// The type of screen, see ScreenType.
        /// </summary>
        public ScreenType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// The name/title of the screen.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The character data for this screen.
        /// </summary>
        public string Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }

        /// <summary>
        /// Is this screen activated?
        /// </summary>
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        /// <summary>
        /// The total number of screens in game.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return ScreenList.Count;
            }
        }

        /// <summary>
        /// Shown when an immortal enters "stat screens".  Shows status of screen settings.
        /// </summary>
        static public string GetStatus()
        {
            string text = "-- Screens In Game --\r\n";
            foreach( Screen screen in ScreenList )
            {
                text += String.Format( "Screen type: {0}.  Screen Name: '{1}'.\r\n", screen._type, screen._name );
            }
            text += "-- Screens In Use --\r\n";
            text += String.Format( "Current Intro (Color): {0}\r\n", _introScreenColor._name );
            text += String.Format( "Current Intro (B/W): {0}\r\n", _introScreenMonochrome._name );
            text += String.Format( "Current Race List (Color): {0}\r\n", _raceSelectionScreenColor._name );
            text += String.Format( "Current Race List (B/W): {0}\r\n", _raceSelectionScreenMonochrome._name );
            text += String.Format( "Current Menu: {0}\r\n", _mainMenuScreen._name );
            return text;
        }

        /// <summary>
        /// Loads a screen file from disk.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool Load(string filename, string blankFilename = null)
        {
            ScreenList.Clear();
            _introScreenColor = null;
            _introScreenMonochrome = null;
            _mainMenuScreen = null;
            _raceSelectionScreenColor = null;
            _raceSelectionScreenMonochrome = null;
            XmlTextReader xtr = null;
            try
            {
                // This method of serialization will preserve newline characters in the text.
                // This is different from most of what we have done in other parts of Basternae.
                XmlSerializer serializer = new XmlSerializer(typeof(List<Screen>));
                try
                {
                    xtr = new XmlTextReader(new StreamReader(filename));
                }
                catch (FileNotFoundException)
                {
                    if (!String.IsNullOrEmpty(blankFilename))
                    {
                        File.Copy(blankFilename, filename);
                        xtr = new XmlTextReader(new StreamReader(filename));
                    }
                    else
                    {
                        throw;
                    }
                }
                ScreenList = serializer.Deserialize(xtr) as List<Screen>;
                xtr.Close();
                if( ScreenList == null )
                {
                    throw new Exception("Unable to load intro, menu, and race selection screens.  Does file exist and contain valid data?");
                }
                foreach( Screen scrn in ScreenList )
                {
                    if (scrn._active)
                    {
                        switch (scrn._type)
                        {
                            case ScreenType.intro:
                                _introScreenColor = scrn;
                                break;
                            case ScreenType.menu:
                                _mainMenuScreen = scrn;
                                break;
                            case ScreenType.races:
                                _raceSelectionScreenColor = scrn;
                                break;
                            case ScreenType.monochrome_intro:
                                _introScreenMonochrome = scrn;
                                break;
                            case ScreenType.monochrome_races:
                                _raceSelectionScreenMonochrome = scrn;
                                break;
                        }
                    }
                }
                if( _introScreenColor == null )
                    Console.WriteLine("Color intro screen not found.");
                if( _mainMenuScreen == null )
                    Console.WriteLine("Main menu not found.");
                if( _raceSelectionScreenColor == null )
                    Console.WriteLine("Color race selection screen not found.");
                if( _introScreenMonochrome == null )
                    Console.WriteLine("Monochrome intro screen not found.");
                if( _raceSelectionScreenMonochrome == null )
                    Console.WriteLine("Monochrome race selection screen not found.");
                return true;
            }
            catch( Exception ex )
            {
                Console.WriteLine("Exception in Screen.Load(): " + ex);
                ScreenList = new List<Screen>();
                return false;
            }
        }

        /// <summary>
        /// Saves the screens to disk.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool Save(string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Screen>));
                XmlTextWriter xtw = new XmlTextWriter(new StreamWriter(filename));
                serializer.Serialize(xtw, ScreenList);
                xtw.Flush();
                xtw.Close();
                return true;

            }
            catch( Exception ex )
            {
                Console.WriteLine( "Unable to save screens: " + ex );
                return false;
            }
        }
    }
}
