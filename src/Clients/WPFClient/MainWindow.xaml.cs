using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.Text.RegularExpressions;

namespace WPFMudClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        About _aboutDlg;
        Equipment _equipmentDlg;
        Group _groupDlg;
        Hotkeys _hotkeysDlg;
        Map _mapDlg;
        Settings _settingsDlg;
        Status _statusDlg;
        ProgramSettings _settings;
        SocketConnection _connection;

        public MainWindow()
        {
            InitializeComponent();
            Uri uri = new Uri("pack://application:,,,/B_32x32.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(uri);
            try
            {
                _settings = ProgramSettings.Load("Settings.xml");
            }
            catch (Exception)
            {
                _settings = new ProgramSettings();
            }
            // Show map and status dialog by default.
            if (_mapDlg == null)
            {
                _mapDlg = new Map();
                _mapDlg.Top = 6;
                _mapDlg.Left = 6;
            }
            _mapDlg.Show();
            if (_statusDlg == null)
            {
                _statusDlg = new Status();
                _statusDlg.Top = 420;
                _statusDlg.Left = 6;
            }
            _statusDlg.Show();
            if (_equipmentDlg == null)
            {
                _equipmentDlg = new Equipment();
                _equipmentDlg.Top = 6;
                _equipmentDlg.Left = 924;
                if (_settings.ShowEquipment)
                {
                    _equipmentDlg.Show();
                }
            }
            if (_settings.ShowHotKeys)
            {
                _hotkeysDlg = new Hotkeys(this, _settings);
                _hotkeysDlg.Left = 326;
                _hotkeysDlg.Top = 716;
                _hotkeysDlg.Show();
            }
            if (_settings.ShowGroup)
            {
                _groupDlg = new Group();
                _groupDlg.Top = 606;
                _groupDlg.Left = 924;
                _groupDlg.Show();
            }
            this.Top = 6;
            this.Left = 326;
        }

        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            if (_aboutDlg == null)
            {
                _aboutDlg = new About();
            }
            _aboutDlg.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (_connection != null)
            {
                _connection.Close();
            }
            Application.Current.Shutdown();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (_connection == null)
            {
                _connection = new SocketConnection(ReceiveCallback);
            }
            if (_settings == null)
            {
                _settings = new ProgramSettings();
            }
            AppendColoredText("Connecting to " + _settings.MUDAddress + " port " + _settings.MUDPort.ToString() + "\n", Brushes.White);
            _connection.Connect(_settings.MUDAddress, _settings.MUDPort);
            // Change to the input window because they're going to need to type something.
            Keyboard.Focus(txtInput);
        }


        private void ConnectLocal_Click(object sender, RoutedEventArgs e)
        {
            if (_connection == null)
            {
                _connection = new SocketConnection(ReceiveCallback);
            }
            if (_settings == null)
            {
                _settings = new ProgramSettings();
            }
            AppendColoredText("Connecting to localhost port 4502\n", Brushes.White);
            _connection.Connect("127.0.0.1", 4502);
            // Change to the input window because they're going to need to type something.
            Keyboard.Focus(txtInput);
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (_connection != null)
            {
                _connection.Close();
            }
            AppendColoredText("Closing socket.\n", Brushes.White);
        }
        
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            _settings = ProgramSettings.Load("Settings.xml");
        }
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _settings.Save("Settings.xml");
        }
        
        private void Equipment_Click(object sender, RoutedEventArgs e)
        {
            if (_equipmentDlg == null)
            {
                _equipmentDlg = new Equipment();
                _equipmentDlg.Top = 6;
                _equipmentDlg.Left = 924;
            }
            _equipmentDlg.Show();
        }
        
        private void Hotkeys_Click(object sender, RoutedEventArgs e)
        {
            if (_hotkeysDlg == null)
            {
                _hotkeysDlg = new Hotkeys(this, _settings);
                _hotkeysDlg.Left = 326;
                _hotkeysDlg.Top = 716;
            }
            _hotkeysDlg.Show();
        }
        
        private void Map_Click(object sender, RoutedEventArgs e)
        {
            if (_mapDlg == null)
            {
                _mapDlg = new Map();
                _mapDlg.Top = 6;
                _mapDlg.Left = 6;
            }
            _mapDlg.Show();
        }
        
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (_settings == null)
            {
                _settings = new ProgramSettings();
            }
            if (_settingsDlg == null)
            {
                _settingsDlg = new Settings(_settings);
            }
            _settingsDlg.Show();
        }
        
        private void Status_Click(object sender, RoutedEventArgs e)
        {
            if (_statusDlg == null)
            {
                _statusDlg = new Status();
                _statusDlg.Top = 420;
                _statusDlg.Left = 6;
            }
            _statusDlg.Show();
        }

        private void Group_Click(object sender, RoutedEventArgs e)
        {
            if (_groupDlg == null)
            {
                _groupDlg = new Group();
                _groupDlg.Top = 606;
                _groupDlg.Left = 924;
            }
            _groupDlg.Show();
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtInput.Text.EndsWith("\r\n"))
            {
                string text = txtInput.Text;
                txtInput.Text = String.Empty;
                ProcessInput(text);
            }
        }

        private void PrintAliases()
        {
            foreach (KeyValuePair<String, Alias> entry in _settings.Aliases)
            {
                PrintAlias(entry.Key);
            }
        }

        private void PrintAlias(String name)
        {
            if (_settings.Aliases.ContainsKey(name))
            {
                AppendColoredText("Alias: " + _settings.Aliases[name].Keyword + " = " + _settings.Aliases[name].Expansion + "\n", Brushes.White);
            }
        }

        public void ProcessInput(String input)
        {
            // Process input.  Send to remote machine if necessary.  Handle
            // alias and action creation, etc.
            if (input.StartsWith("#al", StringComparison.CurrentCultureIgnoreCase))
            {
                AppendColoredText(input, Brushes.White);
                String[] pieces = input.Split(new char[] { ' ' });
                if (pieces.Length == 1)
                {
                    PrintAliases();
                }
                else if (pieces.Length == 2)
                {
                    pieces[1] = pieces[1].Replace("\r", "");
                    pieces[1] = pieces[1].Replace("\n", "");
                    PrintAlias(pieces[1]);
                }
                else
                {
                    Alias alias;
                    pieces[pieces.Length - 1] = pieces[pieces.Length - 1].Replace("\r", "");
                    pieces[pieces.Length - 1] = pieces[pieces.Length - 1].Replace("\n", "");
                    if (_settings.Aliases.ContainsKey(pieces[1]))
                    {
                        alias = _settings.Aliases[pieces[1]];
                    }
                    else
                    {
                        alias = new Alias();
                        alias.Keyword = pieces[1];
                    }
                    alias.Expansion = String.Join(" ", pieces, 2, (pieces.Length - 2));
                    _settings.Aliases[pieces[1]] = alias;
                    PrintAlias(pieces[1]);
                }
            }
            else
            {
                String[] pieces = input.Split(new char[] { ' ' });
                if (pieces.Length > 0)
                {
                    pieces[0] = pieces[0].Replace("\n", "");
                    pieces[0] = pieces[0].Replace("\r", "");
                    if (_settings.Aliases.ContainsKey(pieces[0]))
                    {
                        pieces[0] = _settings.Aliases[pieces[0]].Expansion;
                        input = String.Join(" ", pieces, 0, pieces.Length);
                    }
                }
                AppendColoredText(input, Brushes.White);
            }
            if (_connection != null)
            {
                _connection.Send(input);
            }
        }

        /// <summary>
        /// Handles incoming network data.
        /// </summary>
        /// <param name="text"></param>
        public void ReceiveCallback(String text)
        {
            if( Thread.CurrentThread != Dispatcher.Thread )
            {
                try
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { ReceiveCallback(text); });
                }
                catch (System.Reflection.TargetInvocationException ex)
                {
                    string str = ex.ToString();
                }
            }
            else
            {
                try
                {
                    ParseText(text);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
        }

        private void AppendColoredText(String text, Brush color)
        {
            TextRange tr = new TextRange(txtOutput.Document.ContentEnd, txtOutput.Document.ContentEnd);
            tr.Text = text.Replace("\r", "");
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, color);
            tr.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily("Courier New, Courier"));
            txtOutput.ScrollToEnd();
        }

        private void ParseText(String text)
        {
            // Automatic terminal type selection: #5, the hidden value.
            if( text.Contains("Enter your terminal type (0 for ASCII, 1 for ANSI):"))
            {
                if (_connection != null)
                {
                    _connection.Send("5\n\r");
                }
                return;
            }

            ParseXMLTags(text);
        }

        private void ParseXMLTags(String text)
        {
            // HTML Regex from Phil Haack.
            Regex regex = new Regex(
                    @"<"
                    +    @"(?<endTag>/)?"    //Captures the / if this is an end tag.
                    +    @"(?<tagname>\w+)"    //Captures TagName
                    +    @"("                //Groups tag contents
                    +        @"(\s+"            //Groups attributes
                    +            @"(?<attName>\w+)"  //Attribute name
                    +            @"("                //groups =value portion.
                    +                @"\s*=\s*"            // = 
                    +                @"(?:"        //Groups attribute "value" portion.
                    +                    @"""(?<attVal>[^""]*)"""    // attVal='double quoted'
                    +                    @"|'(?<attVal>[^']*)'"        // attVal='single quoted'
                    +                    @"|(?<attVal>[^'"">\s]+)"    // attVal=urlnospaces
                    +                @")"
                    +            @")?"        //end optional att value portion.
                    +        @")+\s*"        //One or more attribute pairs
                    +        @"|\s*"            //Some white space.
                    +    @")"
                    + @"(?<completeTag>/)?>" //Captures the "/" if this is a complete tag.
                    , RegexOptions.IgnoreCase|RegexOptions.Compiled );
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0)
            {
                int lasttagend = 0;
                // TODO: Make this a class variable.
                string previoustag = "";
                for (int i = 0; i < matches.Count; i++ )
                {
                    Match match = matches[i];
                    string value = match.Value;
                    int index = match.Index;
                    int length = match.Length;
                    try
                    {
                        switch (value)
                        {
                            case "</zone>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    _mapDlg.ParseZoneText(usabletext);
                                }
                                break;
                            case "<zone>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    ProcessWindowText(usabletext);
                                }
                                break;
                            case "</room>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    _mapDlg.ParseRoomText(usabletext);
                                }
                                break;
                            case "<room>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    ProcessWindowText(usabletext);
                                }
                                break;
                            case "</equipment>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    _equipmentDlg.ParseText(usabletext);
                                }
                                break;
                            case "<equipment>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    ProcessWindowText(usabletext);
                                }
                                break;
                            case "</prompt>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    _statusDlg.ParseText(usabletext);
                                }
                                break;
                            case "<prompt>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    ProcessWindowText(usabletext);
                                }
                                break;
                            case "</exits>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    _mapDlg.ParseExitText(usabletext);
                                }
                                break;
                            case "<exits>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    ProcessWindowText(usabletext);
                                }
                                break;
                            case "</map>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    _mapDlg.ParseMapText(usabletext);
                                }
                                break;
                            case "<map>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    ProcessWindowText(usabletext);
                                }
                                break;
                            case "</roomDescription>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    _mapDlg.ParseRoomDescriptionText(usabletext);
                                }
                                break;
                            case "<roomDescription>":
                                {
                                    string usabletext = text.Substring(lasttagend, index - lasttagend);
                                    ProcessWindowText(usabletext);
                                }
                                break;
                            default:
                                string unrecognizedtag = value;
                                break;
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        string str = ex.ToString();
                    }
                    lasttagend = index+length;
                    previoustag = value;
                }
                // TODO: If the last tag in the string was NOT a closing tag and this is within an existing tag,
                // save this text until we do get a closing tag.
                if (lasttagend < text.Length)
                {
                    ProcessWindowText(text.Substring(lasttagend));
                }
            }
            else
            {
                ProcessWindowText(text);
            }
        }

        private void ProcessWindowText(String text)
        {
            // See http://regexlib.com/REDetails.aspx?regexp_id=354
            Regex regex = new Regex(@"(?s)(?:\e\[(?:(\d+);?)*([A-Za-z])(.*?))(?=\e\[|\z)", RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    // TODO: Get the combined color from the ANSI bold/unbold codes.
                    bool bold = false;
                    int color = 0;
                    foreach (Capture capture in match.Groups[1].Captures)
                    {
                        int val = -1;
                        if( Int32.TryParse(capture.Value, out val))
                        {
                            if( val == 0 )
                            {
                                bold = false;
                            }
                            else if( val == 1 )
                            {
                                bold = true;
                            }
                            else if( val >= 30 && val <= 37 )
                            {
                                color = val;
                            }
                        }

                    }
                    Brush brush = GetColor(color, bold);
                    AppendColoredText(RemoveANSICodes(match.Groups[0].Value), brush);
                }
            }
            else
            {
                AppendColoredText(text, Brushes.White);
            }
        }

        private Brush GetColor(int colorcode, bool bold)
        {
            switch (colorcode)
            {
                case 30:
                    if (!bold)
                    {
                        return Brushes.DarkGray;
                    }
                    else
                    {
                        return Brushes.Gray;
                    }
                case 31:
                    if (!bold)
                    {
                        return Brushes.DarkRed;
                    }
                    else
                    {
                        return Brushes.Red;
                    }
                case 32:
                    if (!bold)
                    {
                        return Brushes.DarkGreen;
                    }
                    else
                    {
                        return Brushes.Green;
                    }
                case 33:
                    if (!bold)
                    {
                        return Brushes.Orange;
                    }
                    else
                    {
                        return Brushes.Yellow;
                    }
                case 34:
                    if (!bold)
                    {
                        return Brushes.DarkBlue;
                    }
                    else
                    {
                        return Brushes.Blue;
                    }
                case 35:
                    if (!bold)
                    {
                        return Brushes.Purple;
                    }
                    else
                    {
                        return Brushes.Magenta;
                    }
                case 36:
                    if (!bold)
                    {
                        return Brushes.DarkCyan;
                    }
                    else
                    {
                        return Brushes.Cyan;
                    }
                case 37:
                    if (!bold)
                    {
                        return Brushes.LightGray;
                    }
                    else
                    {
                        return Brushes.White;
                    }
            }
            if (!bold)
            {
                return Brushes.LightGray;
            }
            else
            {
                return Brushes.White;
            }
        }

        private String RemoveANSICodes(String text)
        {
            text = Regex.Replace(text, @"\e\[\d*;?\d+m", "");
            return text;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
