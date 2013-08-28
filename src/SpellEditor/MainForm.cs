using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using ModernMUD;
using MUDEngine;

namespace SpellEditor
{
    public partial class MainForm : Form
    {
        int _selectedIndex;

        public MainForm()
        {
            // Set up controls.
            InitializeComponent();
            Type types = typeof(Spell.AICategory);
            foreach (string s in Enum.GetNames(types))
            {
                cbAIType.Items.Add(s);
            }
            types = typeof(Race.DamageType);
            foreach (string s in Enum.GetNames(types))
            {
                cbDamageType.Items.Add(s);
            }
            types = typeof(SpellDurationType);
            foreach (string s in Enum.GetNames(types))
            {
                cbSpellDuration.Items.Add(s);
            }
            types = typeof(Spell.SavingThrowResult);
            foreach (string s in Enum.GetNames(types))
            {
                cbSavingThrow.Items.Add(s);
            }
            types = typeof(TargetType);
            foreach (string s in Enum.GetNames(types))
            {
                cbTargetType.Items.Add(s);
            }
            types = typeof(Spell.StackType);
            foreach (string s in Enum.GetNames(types))
            {
                cbStackType.Items.Add(s);
            }

            // Load and set up data.
            Spell.LoadSpells();
            for (int i = 0; i < Spell.Count; i++)
            {
                cbSelectSpell.Items.Add(Spell.SpellList.ElementAt(i).Value.Name);
            }
            statusStrip1.Items.Add("Number of Spells: " + Spell.Count);
            if (Spell.Count > 0)
            {
                _selectedIndex = 0;
                RefreshWindowContents();
            }
        }

        /// <summary>
        /// Show the about box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(BuildVersionString());
        }

        /// <summary>
        /// For the about box - gather info on build date and DLLs being used.
        /// </summary>
        /// <returns></returns>
        private string BuildVersionString()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyCopyrightAttribute copyright =
                (AssemblyCopyrightAttribute)AssemblyCopyrightAttribute.GetCustomAttribute(
                    assembly, typeof(AssemblyCopyrightAttribute));
            AssemblyTitleAttribute title =
                (AssemblyTitleAttribute)AssemblyTitleAttribute.GetCustomAttribute(
                    assembly, typeof(AssemblyTitleAttribute));
            FileInfo info = new FileInfo(assembly.Location);
            DateTime date = info.LastWriteTime;

            // Create an area class just so we can get info about the base assembly.
            Area area = new Area();
            string baseName = area.GetType().Assembly.GetName().Name;
            string baseVersion = area.GetType().Assembly.GetName().Version.ToString();
            FileInfo baseInfo = new FileInfo(area.GetType().Assembly.Location);
            DateTime baseDate = baseInfo.LastWriteTime;

            Spell spell = new Spell();
            string baseName2 = spell.GetType().Assembly.GetName().Name;
            string baseVersion2 = spell.GetType().Assembly.GetName().Version.ToString();
            FileInfo baseInfo2 = new FileInfo(spell.GetType().Assembly.Location);
            DateTime baseDate2 = baseInfo2.LastWriteTime;

            string version = title.Title +
                " version " +
                assembly.GetName().Version +
                " built on " +
                date.ToShortDateString() +
                ".\nBased on version " +
                baseVersion +
                " of " +
                baseName +
                " built on " +
                baseDate.ToShortDateString() +
                ".\nBased on version " +
                baseVersion2 +
                " of " +
                baseName2 +
                " built on " +
                baseDate2.ToShortDateString() +
                ".\nThis application is " +
                copyright.Copyright +
                "\nWritten by Jason Champion (Xangis).\nFor the latest version, visit http://www.basternae.org.";
            return version;
        }

        /// <summary>
        /// Handle the exit command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handle the save command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spell.SaveSpells();
            MessageBox.Show("Spells saved.");
        }

        /// <summary>
        /// Update the window contents with the currently selected spell's info.
        /// </summary>
        private void RefreshWindowContents()
        {
            if( _selectedIndex < 0 || _selectedIndex >= Spell.Count )
            {
                return;
            }
            lblIndex.Text = (_selectedIndex + 1) + " of " + Spell.Count;
            Spell spell = Spell.SpellList.ElementAt(_selectedIndex).Value;
            txtName.Text = spell.Name;
            chkCastCombat.Checked = spell.CanCastInCombat;
            txtAIPower.Text = spell.AIPower.ToString();
            txtAIChance.Text = spell.AIPower.ToString();
            chkCanBeScribed.Checked = spell.CanBeScribed;
            txtCastingTime.Text = spell.CastingTime.ToString();
            txtCode.Text = spell.Code;
            txtDamageMessage.Text = spell.MessageDamage;
            txtDamageMessageSelfToRoom.Text = spell.MessageDamageSelfToRoom;
            txtDamageMessageToRoom.Text = spell.MessageDamageToRoom;
            txtDamageMessageToSelf.Text = spell.MessageDamageToSelf;
            txtDamageMessageToVictim.Text = spell.MessageDamageToVictim;
            txtKillMessage.Text = spell.MessageKill;
            txtWearOffMessage.Text = spell.MessageWearOff;
            txtMinMana.Text = spell.MinimumMana.ToString();
            txtBaseDamage.Text = spell.BaseDamage.ToString();
            txtLevelCap.Text = spell.LevelCap.ToString();
            txtDamageDicePerLevel.Text = spell.DamageDicePerLevel.ToString();
            chkDetrimental.Checked = spell.Detrimental;
            txtManaType.Text = spell.ManaType.ToString();
            txtSchool.Text = spell.School.ToString();
            cbAIType.Text = spell.AICategoryType.ToString();
            cbDamageType.Text = spell.DamageInflicted.ToString();
            cbSpellDuration.Text = spell.Duration.ToString();
            cbSavingThrow.Text = spell.SavingThrowEffect.ToString();
            cbTargetType.Text = spell.ValidTargets.ToString();
            cbStackType.Text = spell.StackingType.ToString();
            txtCompletedMessage.Text = spell.MessageCompleted;
            txtCompletedMessageToRoom.Text = spell.MessageCompletedToRoom;
            txtCompletedMessageToTarget.Text = spell.MessageCompletedToTarget;
            cbStackType.Text = spell.StackingType.ToString();
        }

        /// <summary>
        /// Navigate to the previous spell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_selectedIndex > 0 && Spell.SpellList.Count > 0)
            {
                StoreWindowContents();
                _selectedIndex--;
                RefreshWindowContents();
            }
        }

        /// <summary>
        /// Navigate to the next spell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if( _selectedIndex < (Spell.SpellList.Count - 1))
            {
                StoreWindowContents();
                _selectedIndex++;
                RefreshWindowContents();
            }
        }

        /// <summary>
        /// Display the different tokens that can be used in spell text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showTextTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show( "$t\ttext argument 1.\n" +
                "$T\ttext argument 2.\n" +
                "$n\tname of the source.\n" +
                "$N\tname of the target.\n" +
                "$e\the or she, based on source's sex.\n" +
                "$E\the or she, based on victim's sex.\n" +
                "$m\thim or her, based on source's sex.\n" +
                "$M\thim or her, based on target's sex.\n" +
                "$s\this or her, based on source's sex.\n" +
                "$S\this or her, based on target's sex.\n" +
                "$p\tsource object name.\n" +
                "$P\ttarget object name.\n" +
                "$d\tdoor name.\n");
        }

        private void btnProvides_Click(object sender, EventArgs e)
        {
            MultiFlagEditor editor = new MultiFlagEditor(BitvectorFlagType.AffectFlags, Spell.SpellList.ElementAt(_selectedIndex).Value.Provides);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                Spell.SpellList.ElementAt(_selectedIndex).Value.Provides = editor.GetValues();
            }
        }

        private void btnNegates_Click(object sender, EventArgs e)
        {
            MultiFlagEditor editor = new MultiFlagEditor(BitvectorFlagType.AffectFlags, Spell.SpellList.ElementAt(_selectedIndex).Value.Negates);
            DialogResult result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                Spell.SpellList.ElementAt(_selectedIndex).Value.Negates = editor.GetValues();
            }
        }

        private void StoreWindowContents()
        {
            // TODO: Actually store all contents.
            string spell = txtName.Text;
            // Don't store this -- it's read-only.
            //Spell.SpellList[spell].Name = spell;
            Spell.SpellList[spell].CanCastInCombat = chkCastCombat.Checked;
            Spell.SpellList[spell].AIPower = Int32.Parse(txtAIPower.Text);
            Spell.SpellList[spell].AIChance = Int32.Parse(txtAIChance.Text);
            Spell.SpellList[spell].CanBeScribed = chkCanBeScribed.Checked;
            Spell.SpellList[spell].CastingTime = Int32.Parse(txtCastingTime.Text);
            Spell.SpellList[spell].Code = txtCode.Text;
            Spell.SpellList[spell].MessageDamage = txtDamageMessage.Text;
            Spell.SpellList[spell].MessageDamageSelfToRoom = txtDamageMessageSelfToRoom.Text;
            Spell.SpellList[spell].MessageDamageToRoom = txtDamageMessageToRoom.Text;
            Spell.SpellList[spell].MessageDamageToSelf = txtDamageMessageToSelf.Text;
            Spell.SpellList[spell].MessageDamageToVictim = txtDamageMessageToVictim.Text;
            Spell.SpellList[spell].MessageKill = txtKillMessage.Text;
            Spell.SpellList[spell].MessageWearOff = txtWearOffMessage.Text;
            Spell.SpellList[spell].MinimumMana = Int32.Parse(txtMinMana.Text);
            Spell.SpellList[spell].BaseDamage = Int32.Parse(txtBaseDamage.Text);
            Spell.SpellList[spell].LevelCap = Int32.Parse(txtLevelCap.Text);
            Spell.SpellList[spell].DamageDicePerLevel = Int32.Parse(txtDamageDicePerLevel.Text);
            Spell.SpellList[spell].Detrimental = chkDetrimental.Checked;
            Spell.SpellList[spell].ManaType = Int32.Parse(txtManaType.Text);
            Spell.SpellList[spell].School = Int32.Parse(txtSchool.Text);
            Spell.SpellList[spell].AICategoryType = (Spell.AICategory)Enum.Parse(typeof(Spell.AICategory), cbAIType.Text );
            Spell.SpellList[spell].DamageInflicted = (AttackType.DamageType)Enum.Parse(typeof(AttackType.DamageType), cbDamageType.Text);
            Spell.SpellList[spell].Duration = (SpellDurationType)Enum.Parse(typeof(SpellDurationType), cbSpellDuration.Text);
            Spell.SpellList[spell].SavingThrowEffect = (Spell.SavingThrowResult)Enum.Parse(typeof(Spell.SavingThrowResult), cbSavingThrow.Text);
            Spell.SpellList[spell].ValidTargets = (TargetType)Enum.Parse(typeof(TargetType), cbTargetType.Text);
            Spell.SpellList[spell].StackingType = (Spell.StackType)Enum.Parse(typeof(Spell.StackType), cbStackType.Text);
            Spell.SpellList[spell].MessageCompleted = txtCompletedMessage.Text;
            Spell.SpellList[spell].MessageCompletedToRoom = txtCompletedMessageToRoom.Text;
            Spell.SpellList[spell].MessageCompletedToTarget = txtCompletedMessageToTarget.Text;
        }

        private void cbSelectSpell_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = cbSelectSpell.Text;
            for (int i = 0; i < Spell.SpellList.Count; i++)
            {
                if (Spell.SpellList.ElementAt(i).Value.Name == text)
                {
                    StoreWindowContents();
                    _selectedIndex = i;
                    RefreshWindowContents();
                }
            }
        }

        private void btnModifies_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO: Work out how the 'modifies' button is going to work.");
        }
    }
}
