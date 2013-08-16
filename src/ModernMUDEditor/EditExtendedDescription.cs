using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModernMUD;

namespace ModernMUDEditor
{
    public partial class EditExtendedDescription : Form
    {

        public EditExtendedDescription(ExtendedDescription desc)
        {
            InitializeComponent();
            lblDescription.Text = "Description";
            txtDescription.Text = desc.Description;
            txtKeywords.Text = desc.Keyword;
        }

        public EditExtendedDescription(TalkData data)
        {
            InitializeComponent();
            lblDescription.Text = "Response";
            txtDescription.Text = data.Message;
            txtKeywords.Text = data.Keywords;
        }

        public ExtendedDescription GetExtendedDescription()
        {
            ExtendedDescription desc = new ExtendedDescription();
            desc.Keyword = txtKeywords.Text;
            desc.Description = txtDescription.Text;
            return desc;
        }

        public TalkData GetTalkData()
        {
            TalkData data = new TalkData();
            data.Message = txtDescription.Text;
            data.Keywords = txtKeywords.Text;
            return data;
        }

        /// <summary>
        /// Resets the window by telling it to refresh with the currently selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void txtDescription_TextChanged( object sender, EventArgs e )
        {
            MainForm.BuildRTFString( txtDescription.Text, rtbDescription );
        }

    }
}