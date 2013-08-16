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
using System.Drawing;

namespace WPFMudClient
{
    /// <summary>
    /// Interaction logic for Status.xaml
    /// </summary>
    public partial class Status : Window
    {
        int _statusWidth = 140;
        int _playerHits;
        int _playerMaxHits;
        int _playerCondition;
        int _tankCondition;
        int _enemyCondition;
        int _playerMana;
        int _playerMaxMana;
        int _playerMoves;
        int _playerMaxMoves;
        public Status()
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
            char[] space = { ' ' };
            char[] colon = { ':' };
            String[] pieces = text.Split(space, StringSplitOptions.RemoveEmptyEntries);
            foreach (String str in pieces)
            {
                String[] subpieces = str.Split(colon);
                int value = 0;
                if (subpieces.Length > 1)
                {
                    switch (subpieces[0])
                    {
                        case "A": // Mana
                            if( Int32.TryParse(subpieces[1], out value ))
                            {
                                _playerMana = value;
                                if (_playerMaxMana > 0)
                                {
                                    pnlMana.Width = Math.Max(0, (_playerMana * _statusWidth) / _playerMaxMana);
                                }
                                else
                                {
                                    pnlMana.Width = 1;
                                }
                            }
                            break;
                        case "B": // Max Mana
                            if( Int32.TryParse(subpieces[1], out value ))
                            {
                                _playerMaxMana = value;
                                if (_playerMaxMana > 0)
                                {
                                    pnlMana.Width = (Math.Max(0, (_playerMana * _statusWidth) / _playerMaxMana));
                                }
                                else
                                {
                                    pnlMana.Width = 1;
                                }
                            }
                            break;
                        case "E": // Enemy Name
                            if (subpieces[1] == "0")
                            {
                                lblEnemyName.Content = "";
                            }
                            else
                            {
                                lblEnemyName.Content = subpieces[1];
                            }
                            break;
                        case "F": // Enemy Condition
                            if (Int32.TryParse(subpieces[1], out value))
                            {
                                _enemyCondition = value;
                                pnlEnemyCond.Width = Math.Max(0, (value * _statusWidth) / 100);
                            }
                            break;
                        case "G": // Enemy Position
                            if (subpieces[1] == "0")
                            {
                                lblEnemyPos.Content = "";
                            }
                            else
                            {
                                lblEnemyPos.Content = subpieces[1];
                            }
                            break;
                        case "H": // Hits
                            if( Int32.TryParse(subpieces[1], out value ))
                            {
                                _playerHits = value;
                            }
                            break;
                        case "I": // MaxHits
                            if( Int32.TryParse(subpieces[1], out value ))
                            {
                                _playerMaxHits = value;
                            }
                            break;
                        case "M": // Moves
                            if( Int32.TryParse(subpieces[1], out value ))
                            {
                                _playerMoves = value;
                                if (_playerMaxMoves > 0)
                                {
                                    pnlMoves.Width = Math.Max(0, (_playerMoves * _statusWidth) / _playerMaxMoves);
                                }
                                else
                                {
                                    pnlMoves.Width = 1;
                                }
                            }
                            break;
                        case "N": // Max Moves
                            if( Int32.TryParse(subpieces[1], out value ))
                            {
                                _playerMaxMoves = value;
                                if (_playerMaxMoves > 0)
                                {
                                    pnlMoves.Width = Math.Max(0, (_playerMoves * _statusWidth) / _playerMaxMoves);
                                }
                                else
                                {
                                    pnlMoves.Width = 1;
                                }
                            }
                            break;
                        case "P": // Player Name
                            if (this.Title != subpieces[1])
                            {
                                this.Title = subpieces[1];
                            }
                            break;
                        case "Q": // Player Condition
                            if (Int32.TryParse(subpieces[1], out value))
                            {
                                _playerCondition = value;
                            }
                            pnlHits.Width = Math.Max(0, (_playerHits * _statusWidth) / _playerMaxHits);
                            break;
                        case "R": // Player Position
                            lblPlayerPos.Content = subpieces[1];
                            break;
                        case "T": // Tank Name
                            if (subpieces[1] == "0")
                            {
                                lblTankName.Content = "";
                            }
                            else
                            {
                                lblTankName.Content = subpieces[1];
                            }
                            break;
                        case "U": // Tank Condition
                            if( Int32.TryParse(subpieces[1], out value ))
                            {
                                _tankCondition = value;
                                pnlTankCond.Width = Math.Max(0, (value * _statusWidth) / 100);
                            }
                            break;
                        case "V": // Tank Position
                            if (subpieces[1] == "0")
                            {
                                lblTankPos.Content = "";
                            }
                            else
                            {
                                lblTankPos.Content = subpieces[1];
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
