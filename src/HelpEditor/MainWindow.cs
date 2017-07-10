using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using HelpData;

namespace HelpEditor
{
    public partial class MainWindow : Form
    {
        private int _selectedItem = 0;
        List<HelpData.Help> _helpData = new List<HelpData.Help>();

        public MainWindow()
        {
            InitializeComponent();
            for( int count = -1; count < 65; count++ )
            {
                cbLevel.Items.Add( count.ToString() );
            }
            this.SizeChanged += new EventHandler(MainWindow_SizeChanged);
        }

        /// <summary>
        /// Resize text area when window size changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            txtContents.Width = this.Width - 40;
            txtContents.Height = this.Height - 185;
        }

        private void UpdateWindowContents( HelpData.Help help )
        {
            txtContents.Text = help.Text;
            txtKeywords.Text = help.Keyword;
            txtSyntax.Text = help.Syntax;
            cbLevel.Text = help.MinimumLevel.ToString();
            txtSeeAlso.Text = help.SeeAlso;
            lblPosition.Text = (_selectedItem + 1).ToString() + " of " + _helpData.Count.ToString();
        }

        private void ClearWindowContents()
        {
            txtContents.Text = String.Empty;
            txtKeywords.Text = String.Empty;
            txtSyntax.Text = String.Empty;
            cbLevel.Text = "1";
            txtSeeAlso.Text = String.Empty;
            lblPosition.Text = (_selectedItem + 1).ToString() + " of " + _helpData.Count.ToString();
        }

        private void StoreWindowContents()
        {
            if( _selectedItem < _helpData.Count && _helpData.Count > 0 )
            {
                _helpData[_selectedItem].Keyword = txtKeywords.Text;
                _helpData[_selectedItem].Text = txtContents.Text;
                _helpData[_selectedItem].SeeAlso = txtSeeAlso.Text;
                _helpData[_selectedItem].Syntax = txtSyntax.Text;
                if( cbLevel.SelectedIndex != -1 )
                {
                    int level = 0;
                    Int32.TryParse( cbLevel.Items[ cbLevel.SelectedIndex ] as string, out level );
                    // If we fail to parse just set the level to 0 so everyone can read it.
                    _helpData[ _selectedItem ].MinimumLevel = level;
                }
            }
        }

        private void openToolStripMenuItem_Click( object sender, EventArgs e )
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "xml files (*.xml)|*.xml";
            fd.InitialDirectory = Directory.GetCurrentDirectory() + "\\sys";
            DialogResult dr = fd.ShowDialog();
            if( dr == System.Windows.Forms.DialogResult.OK )
            {
               _helpData = HelpData.Help.Load( fd.FileName );
            }
            toolStripFilename.Text = "[" + fd.FileName + "]";
            if( _helpData.Count > 0 )
            {
                _selectedItem = 0;
                UpdateWindowContents(_helpData[_selectedItem]);
            }
        }

        private void saveAsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if( _helpData.Count < 1 )
            {
                MessageBox.Show( "Nothing to save: no file loaded or created.", "Save Error" );
                return;
            }
            StoreWindowContents();
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "xml files (*.xml)|*.xml";
            fd.InitialDirectory = Directory.GetCurrentDirectory() + "\\sys";
            DialogResult dr = fd.ShowDialog();
            if( dr == System.Windows.Forms.DialogResult.OK )
            {
                HelpData.Help.Save( fd.FileName, _helpData );
            }
            toolStripFilename.Text = "[" + fd.FileName + "]";
        }

        private void aboutToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyCopyrightAttribute copyright =
                (AssemblyCopyrightAttribute)AssemblyCopyrightAttribute.GetCustomAttribute(
                    assembly, typeof( AssemblyCopyrightAttribute ) );
            AssemblyTitleAttribute title =
                (AssemblyTitleAttribute)AssemblyTitleAttribute.GetCustomAttribute(
                    assembly, typeof( AssemblyTitleAttribute ) );
            System.IO.FileInfo info = new System.IO.FileInfo( assembly.Location );
            DateTime date = info.LastWriteTime;

            // Create an area class just so we can get info about the base assembly.
            HelpData.Help area = new HelpData.Help();
            string BaseName = area.GetType().Assembly.GetName().Name;
            string BaseVersion = area.GetType().Assembly.GetName().Version.ToString();
            System.IO.FileInfo BaseInfo = new System.IO.FileInfo( area.GetType().Assembly.Location );
            DateTime BaseDate = info.LastWriteTime;

            MessageBox.Show(
                title.Title +
                " version " +
                assembly.GetName().Version.ToString() +
                " built on " +
                date.ToShortDateString() +
                ".\nBased on version " +
                BaseVersion +
                " of " +
                BaseName +
                " built on " +
                BaseDate.ToShortDateString() +
                ".\nThis application is " +
                copyright.Copyright +
                "\nWritten by Jason Champion (Xangis).\nFor the latest version, visit http://basternae.org.",
                "About " + title.Title );
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void btnRewind_Click( object sender, EventArgs e )
        {
            if( _helpData.Count > 0 && _selectedItem > 0 )
            {
                StoreWindowContents();
                _selectedItem = 0;
                UpdateWindowContents( _helpData[ _selectedItem ] );
            }
        }

        private void btnLeft_Click( object sender, EventArgs e )
        {
            if( _helpData.Count > 0 && _selectedItem > 0 )
            {
                StoreWindowContents();
                --_selectedItem;
                UpdateWindowContents( _helpData[_selectedItem] );
            }
        }

        private void btnRight_Click( object sender, EventArgs e )
        {
            if( ( _selectedItem + 1 ) < _helpData.Count )
            {
                StoreWindowContents();
                ++_selectedItem;
                UpdateWindowContents( _helpData[ _selectedItem ] );
            }
        }

        private void btnEnd_Click( object sender, EventArgs e )
        {
            if( ( _selectedItem + 1 ) < _helpData.Count )
            {
                StoreWindowContents();
                _selectedItem = _helpData.Count - 1;
                UpdateWindowContents( _helpData[ _selectedItem ] );
            }
        }

        private void btnNew_Click( object sender, EventArgs e )
        {
            StoreWindowContents();
            HelpData.Help help = new HelpData.Help();
            _helpData.Add( help );
            _selectedItem = _helpData.Count - 1;
            UpdateWindowContents( help );
        }

        private void btnFind_Click( object sender, EventArgs e )
        {
            if (_helpData.Count < 1)
            {
                return;
            }

            // We start at the current item + 1 to give ourselves an
            // automatic "find next" search.
            int start = _selectedItem;
            if( _selectedItem + 1 < _helpData.Count )
            {
                for( int x = (_selectedItem + 1); x < _helpData.Count; x++ )
                {
                    if( _helpData[ x ].Keyword.ToLower().Contains( txtFind.Text.ToLower() ) )
                    {
                        StoreWindowContents();
                        _selectedItem = x;
                        UpdateWindowContents( _helpData[ _selectedItem ] );
                        return;
                    }
                }
            }
            for( int x = 0; x <= _selectedItem; x++ )
            {
                if( _helpData[ x ].Keyword.ToLower().Contains( txtFind.Text.ToLower() ) )
                {
                    StoreWindowContents();
                    _selectedItem = x;
                    UpdateWindowContents( _helpData[ _selectedItem ] );
                    return;
                }
            }
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (_helpData.Count > 0 && _selectedItem > -1)
            {
                _helpData.RemoveAt(_selectedItem);
                if (_helpData.Count == 0)
                {
                    _selectedItem = -1;
                    ClearWindowContents();
                }
                else if (_selectedItem > 0)
                {
                    --_selectedItem;
                }
                else if( _selectedItem < (_helpData.Count - 1))
                {
                    ++_selectedItem;
                }
                if (_selectedItem >= 0)
                {
                    UpdateWindowContents(_helpData[_selectedItem]);
                }
            }
        }
    }
}