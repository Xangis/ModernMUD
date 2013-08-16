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
using System.Windows.Shapes;

namespace WPFMudClient
{
    /// <summary>
    /// Interaction logic for Hotkeys.xaml
    /// </summary>
    public partial class Hotkeys : Window
    {
        MouseGesture rightClickGesture = new MouseGesture();
        List<Button> buttons = new List<Button>();
        MainWindow parent;
        ProgramSettings programSettings;

        public Hotkeys(MainWindow parentwindow, ProgramSettings settings)
        {
            programSettings = settings;
            parent = parentwindow;
            InitializeComponent();
            rightClickGesture.MouseAction = MouseAction.RightClick;
            MouseBinding rightClickBinding = new MouseBinding();
            RoutedCommand rightClickCmd = new RoutedCommand();
            rightClickBinding.Gesture = rightClickGesture;
            rightClickBinding.Command = rightClickCmd;
            CommandBinding rightClickCmdBinding = new CommandBinding();
            rightClickCmdBinding.Command = rightClickCmd;
            rightClickCmdBinding.Executed += Button_RightClick;
            Uri uri = new Uri("pack://application:,,,/B_32x32.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(uri);
            for (int i = 0; i < programSettings.NumHotKeys; i++)
            {
                Button btn = new Button();
                btn.Name = "btn" + i.ToString();
                btn.Click += Button_Click;
                btn.Width = 56;
                btn.InputBindings.Add(rightClickBinding);
                btn.CommandBindings.Add(rightClickCmdBinding);
                pnlButtons.Children.Add(btn);
                if (programSettings.HotkeyCommands.Count <= i)
                {
                    Alias alias = new Alias();
                    alias.Keyword = "(None)";
                    btn.Content = "(none)";
                    alias.Expansion = String.Empty;
                    programSettings.HotkeyCommands.Add(alias);
                }
                else
                {
                    btn.Content = programSettings.HotkeyCommands[i].Keyword;
                }
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button_RightClick(object sender, ExecutedRoutedEventArgs e)
        {
            Object o = e.Source;
            Button btn = (Button)sender;
            HotkeyEditor editor = new HotkeyEditor();
            Int32 num = Int32.Parse(btn.Name.Substring(3));
            editor.SetValues(programSettings.HotkeyCommands[num].Keyword, programSettings.HotkeyCommands[num].Expansion);
            if( (bool)editor.ShowDialog() )
            {
                String label;
                String command;
                editor.GetValues(out label, out command);
                btn.Content = label;
                Alias alias = programSettings.HotkeyCommands[num];
                alias.Keyword = label;
                alias.Expansion = command;
                programSettings.HotkeyCommands[num] = alias;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Int32 num = Int32.Parse(btn.Name.Substring(3));
            parent.ProcessInput(programSettings.HotkeyCommands[num].Expansion + "\r\n");
        }
    }
}
