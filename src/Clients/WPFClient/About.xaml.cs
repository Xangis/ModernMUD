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
using System.Reflection;
using System.IO;

namespace WPFMudClient
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            Uri uri = new Uri("pack://application:,,,/B_32x32.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(uri);
            // Populate name and version info.
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyTitleAttribute title =
                (AssemblyTitleAttribute)AssemblyTitleAttribute.GetCustomAttribute(
                    assembly, typeof(AssemblyTitleAttribute));
            int minVer = typeof(About).Assembly.GetName().Version.Minor;
            int majVer = typeof(About).Assembly.GetName().Version.Major;
            double ver = majVer + (minVer / 100.0);
            FileInfo info = new FileInfo(assembly.Location);
            DateTime date = info.LastWriteTime;
            lblAppNameAndVersion.Content = title.Title + " version " + ver + " (" + date.ToShortDateString() + ")";

            // Populate copyright info
            AssemblyCopyrightAttribute copyright =
                (AssemblyCopyrightAttribute)AssemblyCopyrightAttribute.GetCustomAttribute(
                    assembly, typeof(AssemblyCopyrightAttribute));
            lblCopyright.Content = copyright.Copyright;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink source = sender as Hyperlink;
            if( source != null )
            {
                System.Diagnostics.Process.Start(source.NavigateUri.ToString());
            }
        }
    }
}
