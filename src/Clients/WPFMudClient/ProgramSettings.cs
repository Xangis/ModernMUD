using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;

namespace WPFMudClient
{
    /// <summary>
    /// Stores the client's application settings.
    /// </summary>
    [Serializable]
    public class ProgramSettings
    {
        private String _displayFont;
        private String _inputFont;
        private String _mudAddress;
        private Int32 _mudPort;
        private Color _statusTextColor;
        private Color _statusTitleColor;
        private Color _inputTextColor;
        private Color _inputBackgroundColor;
        private Color _backgroundColor;
        private Color _defaultTextColor;
        private SerializableDictionary<String, Alias> _aliases;
        private SerializableDictionary<String, Action> _actions;
        private List<Alias> _hotkeyCommands = new List<Alias>();
        private int _numHotKeys;
        private bool _showHotKeys;
        private bool _showGroup;
        private bool _showEquipment;

        public ProgramSettings()
        {
            _displayFont = "Courier";
            _inputFont = "Courier";
            _mudAddress = "basternae.org";
            _mudPort = 4502;
            _statusTextColor = Color.WhiteSmoke;
            _statusTitleColor = Color.DarkBlue;
            _inputTextColor = Color.Black;
            _backgroundColor = Color.Black;
            _defaultTextColor = Color.White;
            _inputBackgroundColor = Color.White;
            _aliases = new SerializableDictionary<String, Alias>();
            _actions = new SerializableDictionary<String, Action>();
            _numHotKeys = 16;
            _showHotKeys = false;
        }

        public List<Alias> HotkeyCommands
        {
            get { return _hotkeyCommands; }
            set { _hotkeyCommands = value; }
        }

        public SerializableDictionary<String, Action> Actions
        {
            get { return _actions; }
            set { _actions = value; }
        }

        public SerializableDictionary<String, Alias> Aliases
        {
            get { return _aliases; }
            set { _aliases = value; }
        }

        public Color DefaultTextColor
        {
            get { return _defaultTextColor; }
            set { _defaultTextColor = value; }
        }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public Color InputBackgroundColor
        {
            get { return _inputBackgroundColor; }
            set { _inputBackgroundColor = value; }
        }

        public Color InputTextColor
        {
            get { return _inputTextColor; }
            set { _inputTextColor = value; }
        }

        public Color StatusTitleColor
        {
            get { return _statusTitleColor; }
            set { _statusTitleColor = value; }
        }

        public Color StatusTextColor
        {
            get { return _statusTextColor; }
            set { _statusTextColor = value; }
        }

        public int MUDPort
        {
            get { return _mudPort; }
            set { _mudPort = value; }
        }

        public string MUDAddress
        {
            get { return _mudAddress; }
            set { _mudAddress = value; }
        }

        public string InputFont
        {
            get { return _inputFont; }
            set { _inputFont = value; }
        }

        public string DisplayFont
        {
            get { return _displayFont; }
            set { _displayFont = value; }
        }

        public int NumHotKeys
        {
            get { return _numHotKeys; }
            set { _numHotKeys = value; }
        }

        public bool ShowHotKeys
        {
            get { return _showHotKeys; }
            set { _showHotKeys = value; }
        }

        public bool ShowEquipment
        {
            get { return _showEquipment; }
            set { _showEquipment = value; }
        }


        public bool ShowGroup
        {
            get { return _showGroup; }
            set { _showGroup = value; }
        }

        /// <summary>
        /// Saves the program settings to disk.
        /// </summary>
        /// <param name="filename"></param>
        public void Save(String filename)
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, this);
            stream.Close();
        }

        /// <summary>
        /// Loads the program settings from disk.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ProgramSettings Load(String filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProgramSettings));
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
            ProgramSettings settings = (ProgramSettings)serializer.Deserialize(stream);
            stream.Close();
            return settings;
        }
    }
}
