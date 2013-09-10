using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using ModernMUD;

namespace MUDScreenEditor
{
    /// <summary>
    /// Main MUD Screen Editor form.
    /// </summary>
    public partial class MudScreenEditor : Form
    {
        int _position;
        // TODO: Use this to prompt whether to save a changed file.
        bool _isDirty = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MudScreenEditor()
        {
            InitializeComponent();
            Type types = typeof(ModernMUD.Screen.ScreenType);
            foreach (string s in Enum.GetNames(types))
            {
                cbScreenType.Items.Add(s);
            }
            ModernMUD.Screen.ScreenList.Add(new ModernMUD.Screen());
            UpdatePosition(ModernMUD.Screen.Count - 1);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyCopyrightAttribute copyright =
                (AssemblyCopyrightAttribute)AssemblyCopyrightAttribute.GetCustomAttribute(
                    assembly, typeof( AssemblyCopyrightAttribute ) );
            FileInfo info = new FileInfo( assembly.Location );
            DateTime date = info.LastWriteTime;

            MessageBox.Show("MUD Screen Editor version " + GetType().Assembly.GetName().Version + " (" +
                date.ToShortDateString() + ").\n\n" + copyright.Copyright + "\nWritten by Jason Champion.\n(jchampion@zetacentauri.com)\n" +
                "\nBasternae MUD is at http://basternae.org\n\nThis application is freeware and may be distributed freely.",
                "About The MUD Screen Editor");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "xml files (*.xml)|*.xml";
            fd.InitialDirectory = Directory.GetCurrentDirectory() + "\\sys";
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (!ModernMUD.Screen.Load(fd.FileName))
                {
                    MessageBox.Show("Unable to load screen file.", "Load Error");
                    return;
                }
                UpdatePosition(0);
                _isDirty = false;
            }
        }

        private void UpdatePosition(int position)
        {
            _position = position;
            lblPosition.Text = (_position + 1) + " of " + ModernMUD.Screen.Count;
            if (ModernMUD.Screen.Count > 0 && _position < ModernMUD.Screen.ScreenList.Count)
            {
                UpdateWindowContents(ModernMUD.Screen.ScreenList[_position]);
            }
        }

        private void UpdateWindowContents(ModernMUD.Screen screen)
        {
            textBox1.Text = screen.Contents;
            try
            {
                cbScreenType.SelectedIndex = (int)screen.Type;
            }
            catch
            {
                // whatever, who cares?
            }
            txtName.Text = screen.Name;
            chkEnabled.Checked = screen.Active;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StoreCurrentScreen();
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "xml files (*.xml)|*.xml";
            fd.InitialDirectory = Directory.GetCurrentDirectory() + "\\sys";
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (!ModernMUD.Screen.Save(fd.FileName))
                {
                    MessageBox.Show("Unable to save screen file.", "Save Error");
                }
                else
                {
                    _isDirty = false;
                }
            }
        }

        private void exportScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "all files (*.*)|*.*";
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult dr = fd.ShowDialog();
            // Convert text box text into a byte stream before saving.
            string basestr = textBox1.Text;
            byte[] outbytes = new byte[basestr.Length];
            for(int x = 0; x < basestr.Length; x++ )
            {
                outbytes[x] = (byte)basestr[x];
            }
            if (dr == DialogResult.OK)
            {
                FileStream fs = new FileStream(fd.FileName, FileMode.Create);
                fs.Write(outbytes, 0, textBox1.Text.Length);
                fs.Close();
            }
        }

        private void importScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "all files (*.*)|*.*";
            fd.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                FileStream fs = new FileStream(fd.FileName, FileMode.Open);
                int length = (int)fs.Length;
                byte[] bytes = new byte[length];
                fs.Read(bytes, 0, length);
                string readStr = String.Empty;
                foreach( byte b in bytes )
                {
                    readStr += (char)b;
                }
                textBox1.Text = readStr;
            }
        }

        private void BuildRTFStringFromANSIText(string text, RichTextBox target)
        {
            // Add header and build color table.
            const string rtfHeader = "{\\rtf\\ansi{\\colortbl\\red192\\green192\\blue192;\\red0\\green0\\blue0;\\red0\\green0\\blue255;\\red0\\green255\\blue255;\\red0\\green255\\blue0;\\red255\\green0\\blue255;\\red255\\green0\\blue0;\\red255\\green255\\blue0;\\red255\\green255\\blue255;\\red0\\green0\\blue128;\\red0\\green128\\blue128;\\red0\\green128\\blue0;\\red128\\green0\\blue128;\\red128\\green0\blue0;\\red128\\green128\\blue0;\\red128\\green128\\blue128;\\red192\\green192\\blue192;}\\cf0 ";
            // Replace each color one by one.
            string parsedText = text.Replace(ModernMUD.Color.FG_BLACK, "\\cf1 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_B_BLUE, "\\cf2 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_B_CYAN, "\\cf3 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_B_GREEN, "\\cf4 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_B_MAGENTA, "\\cf5 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_B_RED, "\\cf6 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_B_YELLOW, "\\cf7 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_B_WHITE, "\\cf8 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_BLUE, "\\cf9 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_CYAN, "\\cf10 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_GREEN, "\\cf11 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_MAGENTA, "\\cf12 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_RED, "\\cf13 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_YELLOW, "\\cf14 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_B_BLACK, "\\cf15 ");
            parsedText = parsedText.Replace(ModernMUD.Color.FG_WHITE, "\\cf16 ");
            // Replace newlines with paragraph breaks.
            parsedText = parsedText.Replace("\n", "\\par ");
            target.Rtf = rtfHeader + parsedText + "}";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BuildRTFStringFromANSIText(textBox1.Text, richTextBox1);
            _isDirty = true;
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_RED);
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_GREEN);
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_BLUE);
        }

        private void btnCyan_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_CYAN);
        }

        private void btnMagenta_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_MAGENTA);
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_YELLOW);
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_BLACK);
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_WHITE);
        }

        private void btnBrtRed_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_B_RED);
        }

        private void btnBrtGreen_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_B_GREEN);
        }

        private void btnBrtBlue_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_B_BLUE);
        }

        private void btnBrtCyan_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_B_CYAN);
        }

        private void btnBrtMagenta_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_B_MAGENTA);
        }

        private void btnBrtYellow_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_B_YELLOW);
        }

        private void btnBrtBlack_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_B_BLACK);
        }

        private void btnBrtWhite_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = (ModernMUD.Color.FG_B_WHITE);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (_position > 0)
            {
                StoreCurrentScreen();
                UpdatePosition(_position - 1);
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (_position < (ModernMUD.Screen.Count - 1))
            {
                StoreCurrentScreen();
                UpdatePosition(_position + 1);
            }
        }

        private void StoreCurrentScreen()
        {
            if (_position < ModernMUD.Screen.Count)
            {
                if (ModernMUD.Screen.ScreenList[_position].Active != chkEnabled.Checked)
                {
                    _isDirty = true;
                    ModernMUD.Screen.ScreenList[_position].Active = chkEnabled.Checked;
                }
                if (ModernMUD.Screen.ScreenList[_position].Contents != textBox1.Text)
                {
                    _isDirty = true;
                    ModernMUD.Screen.ScreenList[_position].Contents = textBox1.Text;
                }
                if (ModernMUD.Screen.ScreenList[_position].Name != txtName.Text)
                {
                    _isDirty = true;
                    ModernMUD.Screen.ScreenList[_position].Name = txtName.Text;
                }
                if (ModernMUD.Screen.ScreenList[_position].Type != (ModernMUD.Screen.ScreenType)cbScreenType.SelectedIndex)
                {
                    _isDirty = true;
                    ModernMUD.Screen.ScreenList[_position].Type = (ModernMUD.Screen.ScreenType)cbScreenType.SelectedIndex;
                }
            }
        }

        private void btnNewScreen_Click(object sender, EventArgs e)
        {
            StoreCurrentScreen();
            ModernMUD.Screen.ScreenList.Add(new ModernMUD.Screen());
            UpdatePosition(ModernMUD.Screen.Count - 1);
            _isDirty = true;
        }

        private void helpToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            MessageBox.Show("File->Open: Opens a ModernMUD-format screen file.\n" +
                            "File->Save: Saves the current screens as a ModernMUD-format screen file.\n" +
                            "File->Import: Loads any text file for editing.\n" +
                            "File->Export: Saves the current edit window as any text file.  Does not save\n" + 
                            "              screens other than the one currently being edited.\n" +
                            "File->Exit: Closes the program.  Make sure you've saved your work first.\n"+
                            "Help->Help: Shows this screen.  Duh.\n" +
                            "Help->About: Shows the credits.\n\n" +
                            "The first button in the row across the top creates a new screen.  The others\n" +
                            "insert ANSI color control codes at the end of the text in the edit window.\n" +
                            "While these will be convenient when creating a new screen, you may have to\n" +
                            "use a bit of cut-and-paste when you want to edit the control codes in an existing\n" +
                            "block of text.\n\n" +
                            "Even though this application is specifically meant for editing screens on\n" +
                            "ModernMUD, it should function well enough as a simple ANSI screen editor\n" +
                            "for use with other MUDS, bulletin board systems, or other ANSI-screen-enabled\n" +
                            "applications.  ANSI screen files have traditionally had the .ANS file extension\n" +
                            "but you can name them anything you like.\n\n" +
                            "This program does not support the full set of ANSI control codes.  This (first)\n" +
                            "version only supports foreground color changes.  Background color code support\n" +
                            "will be added in the future if there's a demand for it.\n\n" +
                            "For support, comments, compliments, or to request additional features, email\n" +
                            "jchampion@zetacentauri.com.  Because this is free software we can't offer the\n" +
                            "highest level of support, but if there's something we can add that would make\n" +
                            "this application useful for you (or your MUD), it couldn't hurt to ask.", "Help Screen" );
        }
    }
}