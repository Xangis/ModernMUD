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
    public partial class EditValues : Form
    {
        ObjTemplate.ObjectType _type;
        int[] _values;
        Area _area;

        public EditValues(ObjTemplate.ObjectType type, int[] values, String text, Area area)
        {
            _area = area;
            InitializeComponent();
            _values = values;
            _type = type;
            lblObjectName.Text = text;
            RefreshWindowContents();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int val;
            if (Int32.TryParse(txtValue1.Text, out val))
            {
                _values[0] = val;
            }
            if (Int32.TryParse(txtValue2.Text, out val))
            {
                _values[1] = val;
            }
            if (Int32.TryParse(txtValue3.Text, out val))
            {
                _values[2] = val;
            }
            if (Int32.TryParse(txtValue4.Text, out val))
            {
                _values[3] = val;
            }
            if (Int32.TryParse(txtValue5.Text, out val))
            {
                _values[4] = val;
            }
            if (Int32.TryParse(txtValue6.Text, out val))
            {
                _values[5] = val;
            }
            if (Int32.TryParse(txtValue7.Text, out val))
            {
                _values[6] = val;
            }
            if (Int32.TryParse(txtValue8.Text, out val))
            {
                _values[7] = val;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public Int32[] GetValues()
        {
            return _values;
        }

        private void RefreshWindowContents()
        {
            txtValue1.Text = _values[0].ToString();
            txtValue2.Text = _values[1].ToString();
            txtValue3.Text = _values[2].ToString();
            txtValue4.Text = _values[3].ToString();
            txtValue5.Text = _values[4].ToString();
            txtValue6.Text = _values[5].ToString();
            txtValue7.Text = _values[6].ToString();
            txtValue8.Text = _values[7].ToString();
            switch (_type)
            {
                case ObjTemplate.ObjectType.weapon:
                    label1.Text = "Weapon Type";
                    label2.Text = "# of Dmg Dice";
                    label3.Text = "Dmg Dice Range";
                    label4.Text = "Damage Type";
                    label5.Text = "Poison Type";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Load Limit";
                    btnFindValue1.Enabled = true;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = true;
                    break;
                case ObjTemplate.ObjectType.armor:
                    label1.Text = "Armor Class";
                    label2.Text = "Not Used";
                    label3.Text = "Not Used";
                    label4.Text = "Armor Thickness";
                    label5.Text = "Armor Flags";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Load Limit";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.food:
                    label1.Text = "Hours Filled";
                    label2.Text = "Not Used";
                    label3.Text = "Not Used";
                    label4.Text = "Poisoned";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Load Limit";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.container:
                    label1.Text = "Capacity";
                    label2.Text = "Flags";
                    label3.Text = "Key";
                    label4.Text = "Volume";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Load Limit";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = true;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.light:
                    label1.Text = "Not Used";
                    label2.Text = "Not Used";
                    label3.Text = "Hours Lit";
                    label4.Text = "Not Used";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.totem:
                    label1.Text = "Sphere(s)";
                    label2.Text = "Not Used";
                    label3.Text = "Not Used";
                    label4.Text = "Not Used";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = true;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.treasure:
                case ObjTemplate.ObjectType.trash:
                case ObjTemplate.ObjectType.wall:
                case ObjTemplate.ObjectType.pen:
                case ObjTemplate.ObjectType.timer:
                case ObjTemplate.ObjectType.scabbard:
                    label1.Text = "Not Used";
                    label2.Text = "Not Used";
                    label3.Text = "Not Used";
                    label4.Text = "Not Used";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.teleport:
                    label1.Text = "Target Room";
                    label2.Text = "Command";
                    label3.Text = "Num Charges";
                    label4.Text = "Not Used";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = true;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.shield:
                    label1.Text = "Shield Type";
                    label2.Text = "Shield Shape";
                    label3.Text = "Shield Size";
                    label4.Text = "Armor Class";
                    label5.Text = "Thickness";
                    label6.Text = "Shield Flags";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.spellbook:
                    label1.Text = "Not Used";
                    label2.Text = "Not Used";
                    label3.Text = "Num Pages";
                    label4.Text = "Not Used";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.instrument:
                    label1.Text = "Instr Type";
                    label2.Text = "Level";
                    label3.Text = "Break Chance";
                    label4.Text = "Min Level Use";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = true;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.quiver:
                    label1.Text = "Max Capacity";
                    label2.Text = "Container Flags";
                    label3.Text = "Missile Type";
                    label4.Text = "Num Missiles";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = true;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.lockpick:
                    label1.Text = "Pick Bonus";
                    label2.Text = "Break Chance";
                    label3.Text = "Not Used";
                    label4.Text = "Not Used";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.switch_trigger:
                    label1.Text = "Trigger Cmd";
                    label2.Text = "Room";
                    label3.Text = "Exit Dir";
                    label4.Text = "Move Type";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = true;
                    btnFindValue3.Enabled = true;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.key:
                    label1.Text = "Not Used";
                    label2.Text = "Break Chance";
                    label3.Text = "Not Used";
                    label4.Text = "Not Used";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.drink_container:
                    label1.Text = "Max Drinks";
                    label2.Text = "Curr Drinks";
                    label3.Text = "Liquid Type";
                    label4.Text = "Poisoned";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = true;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.note:
                    label1.Text = "Language";
                    label2.Text = "Not Used";
                    label3.Text = "Not Used";
                    label4.Text = "Not Used";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = true;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                case ObjTemplate.ObjectType.money:
                    label1.Text = "Copper";
                    label2.Text = "Silver";
                    label3.Text = "Gold";
                    label4.Text = "Platinum";
                    label5.Text = "Not Used";
                    label6.Text = "Not Used";
                    label7.Text = "Not Used";
                    label8.Text = "Not Used";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
                default:
                    label1.Text = "Value 1";
                    label2.Text = "Value 2";
                    label3.Text = "Value 3";
                    label4.Text = "Value 4";
                    label5.Text = "Value 5";
                    label6.Text = "Value 6";
                    label7.Text = "Value 7";
                    label8.Text = "Load Limit";
                    btnFindValue1.Enabled = false;
                    btnFindValue2.Enabled = false;
                    btnFindValue3.Enabled = false;
                    btnFindValue4.Enabled = false;
                    break;
            }
        }

        private void btnFindValue1_Click(object sender, EventArgs e)
        {
            switch (_type)
            {
                case ObjTemplate.ObjectType.weapon:
                    {
                        SelectWeaponType dlg = new SelectWeaponType();
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtValue1.Text = dlg.GetNumber().ToString();
                        }
                    }
                    return;
                case ObjTemplate.ObjectType.totem:
                    {
                        int value = 0;
                        Int32.TryParse(txtValue1.Text, out value);
                        FlagEditor dlg = new FlagEditor(BitvectorFlagType.TotemFlags, value, 0);
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtValue1.Text = dlg.Value.ToString();
                        }
                    }
                    return;
                case ObjTemplate.ObjectType.teleport:
                    {
                        SelectRoom dlg = new SelectRoom(_area);
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            txtValue1.Text = dlg.GetIndexNumberString();
                        }
                    }
                    return;
                case ObjTemplate.ObjectType.note:
                    {
                        SelectLanguage dlg = new SelectLanguage();
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            txtValue1.Text = ((int)dlg.GetLanguage()).ToString();
                        }
                    }
                    return;
                case ObjTemplate.ObjectType.instrument:
                    {
                        SelectInstrumentType dlg = new SelectInstrumentType();
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            txtValue1.Text = dlg.GetInstrumentType().ToString();
                        }
                    }
                    return;
                default:
                    return;
            }
        }

        private void btnFindValue4_Click(object sender, EventArgs e)
        {
            switch (_type)
            {
                case ObjTemplate.ObjectType.weapon:
                    SelectWeaponDamage dlg = new SelectWeaponDamage();
                    dlg.ShowDialog();
                    if (dlg.DialogResult == DialogResult.OK)
                    {
                        txtValue4.Text = dlg.GetNumber().ToString();
                    }
                    return;
                default:
                    return;
            }
        }

        private void btnFindValue2_Click(object sender, EventArgs e)
        {
            switch (_type)
            {
                case ObjTemplate.ObjectType.switch_trigger:
                    {
                        SelectRoom dlg = new SelectRoom(_area);
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            txtValue2.Text = dlg.GetIndexNumberString();
                        }
                    }
                    return;
                case ObjTemplate.ObjectType.container:
                case ObjTemplate.ObjectType.quiver:
                    {
                        int value = 0;
                        Int32.TryParse(txtValue1.Text, out value);
                        FlagEditor dlg = new FlagEditor(BitvectorFlagType.ContainerFlags, value, 0);
                        dlg.ShowDialog();
                        if (dlg.DialogResult == DialogResult.OK)
                        {
                            txtValue2.Text = dlg.Value.ToString();
                        }
                    }
                    return;
                default:
                    return;
            }
        }

        private void btnFindValue3_Click(object sender, EventArgs e)
        {
            switch (_type)
            {
                case ObjTemplate.ObjectType.switch_trigger:
                    {
                        SelectDirection dlg = new SelectDirection();
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            txtValue3.Text = dlg.GetDirectionNumber().ToString();
                        }
                    }
                    return;
                case ObjTemplate.ObjectType.drink_container:
                    {
                        SelectLiquid dlg = new SelectLiquid();
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            txtValue3.Text = dlg.GetText();
                        }
                    }
                    return;
                default:
                    return;
            }
        }
    }
}
