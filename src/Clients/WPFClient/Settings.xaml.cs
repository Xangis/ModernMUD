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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        ProgramSettings _settings;

        public Settings(ProgramSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            Uri uri = new Uri("pack://application:,,,/B_32x32.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(uri);
            txtAddress.Text = settings.MUDAddress;
            txtPort.Text = settings.MUDPort.ToString();
            txtNumHotKeys.Text = _settings.NumHotKeys.ToString();
            chkShowHotkeys.IsChecked = _settings.ShowHotKeys;
            chkShowEquipment.IsChecked = _settings.ShowEquipment;
            chkShowGroup.IsChecked = _settings.ShowGroup;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            _settings.MUDAddress = txtAddress.Text;
            int value = 0;
            if( Int32.TryParse(txtPort.Text, out value ))
            {
                _settings.MUDPort = value;
            }
            if (Int32.TryParse(txtNumHotKeys.Text, out value))
            {
                _settings.NumHotKeys = value;
            }
            _settings.ShowHotKeys = (bool)chkShowHotkeys.IsChecked;
            _settings.ShowGroup = (bool)chkShowGroup.IsChecked;
            _settings.ShowEquipment = (bool)chkShowEquipment.IsChecked;
            Hide();
        }
    }
}
