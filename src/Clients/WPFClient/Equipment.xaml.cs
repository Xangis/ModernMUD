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
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class Equipment : Window
    {
        public Equipment()
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
            char[] colon = { ':' };
            char[] comma = { ',' };
            string[] pieces = text.Split(colon, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in pieces)
            {
                string[] itempair = str.Split(comma);
                if (itempair.Length == 2)
                {
                    int wearloc = 0;
                    if (Int32.TryParse(itempair[0], out wearloc))
                    {
                        itempair[1] = SocketConnection.RemoveANSICodes(itempair[1]);
                        switch (wearloc)
                        {
                            case 1:
                                lblFinger1.Content = itempair[1];
                                break;
                            case 2:
                                lblFinger2.Content = itempair[1];
                                break;
                            case 3:
                                lblNeck1.Content = itempair[1];
                                break;
                            case 4:
                                lblNeck2.Content = itempair[1];
                                break;
                            case 5:
                                lblBody.Content = itempair[1];
                                break;
                            case 6:
                                lblHead.Content = itempair[1];
                                break;
                            case 7:
                                lblLegs.Content = itempair[1];
                                break;
                            case 8:
                                lblFeet.Content = itempair[1];
                                break;
                            case 9:
                                lblHands.Content = itempair[1];
                                break;
                            case 10:
                                lblArms.Content = itempair[1];
                                break;
                            case 11:
                                lblAboutBody.Content = itempair[1];
                                break;
                            case 12:
                                lblWaist.Content = itempair[1];
                                break;
                            case 13:
                                lblWrist1.Content = itempair[1];
                                break;
                            case 14:
                                lblWrist2.Content = itempair[1];
                                break;
                            case 15:
                                lblHand1.Content = itempair[1];
                                break;
                            case 16:
                                lblHand2.Content = itempair[1];
                                break;
                            case 17:
                                lblEyes.Content = itempair[1];
                                break;
                            case 18:
                                lblFace.Content = itempair[1];
                                break;
                            case 19:
                                lblEar1.Content = itempair[1];
                                break;
                            case 20:
                                lblEar2.Content = itempair[1];
                                break;
                            case 21:
                                lblBadge.Content = itempair[1];
                                break;
                            /*
                             * Still Need:
                             * 22 - on back
                             * 23 - belt attach 1
                             * 24 - belt attach 2
                             * 25 - belt attach 3
                             * 26 - quiver
                             * 27 - tail
                             * 28 - horse body
                             * 29 - horns
                             * 30 - nose
                             * 31 - hand3
                             * 32 - hand4
                             * 33 - lower arms
                             * 34 - lower left wrist
                             * 35 - lower right wrist
                             */
                        }
                    }
                }
            }
        }
    }
}
