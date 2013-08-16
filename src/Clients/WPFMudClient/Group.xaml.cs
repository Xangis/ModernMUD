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
    /// Interaction logic for Group.xaml
    /// </summary>
    public partial class Group : Window
    {
        public Group()
        {
            InitializeComponent();
            Uri uri = new Uri("pack://application:,,,/B_32x32.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(uri);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void ParseText(string text)
        {
        }
    }
}
