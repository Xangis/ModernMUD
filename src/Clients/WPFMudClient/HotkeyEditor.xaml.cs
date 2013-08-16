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

namespace WPFMudClient
{
    /// <summary>
    /// Interaction logic for HotkeyEditor.xaml
    /// </summary>
    public partial class HotkeyEditor : Window
    {
        public HotkeyEditor()
        {
            InitializeComponent();
        }

        public void SetValues(string title, string text)
        {
            txtName.Text = title;
            txtSend.Text = text;
        }

        public void GetValues(out string title, out string text)
        {
            title = txtName.Text;
            text = txtSend.Text;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
