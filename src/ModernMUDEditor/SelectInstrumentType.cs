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
    public partial class SelectInstrumentType : Form
    {
        public SelectInstrumentType()
        {
            InitializeComponent();
        }

        public int GetInstrumentType()
        {
            switch (cbInstrumentType.Text)
            {
                default:
                    return 0;
                case "flute":
                    return 184;
                case "lyre":
                    return 185;
                case "mandolin":
                    return 186;
                case "harp":
                    return 187;
                case "drums":
                    return 188;
                case "horn":
                    return 189;
                case "pipes":
                    return 190;
                case "fiddle":
                    return 191;
                case "dulcimer":
                    return 192;
            }
        }

        public void SetInstrumentType(int type)
        {
            switch (type)
            {
                default:
                    return;
                case 184:
                    cbInstrumentType.Text = "flute";
                    return;
                case 185:
                    cbInstrumentType.Text = "lyre";
                    return;
                case 186:
                    cbInstrumentType.Text = "mandolin";
                    return;
                case 187:
                    cbInstrumentType.Text = "harp";
                    return;
                case 188:
                    cbInstrumentType.Text = "drums";
                    return;
                case 189:
                    cbInstrumentType.Text = "horn";
                    return;
                case 190:
                    cbInstrumentType.Text = "pipes";
                    return;
                case 191:
                    cbInstrumentType.Text = "fiddle";
                    return;
                case 192:
                    cbInstrumentType.Text = "dulcimer";
                    return;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
