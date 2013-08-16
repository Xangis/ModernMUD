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
    public partial class EditCustomActions : Form
    {
        Area _area;

        public EditCustomActions(Area area, List<CustomAction> actions)
        {
            _area = area;
            InitializeComponent();
            Type types = typeof(CustomAction.ActionTriggerType);
            CustomAction.ActionTriggerType[] vals = (CustomAction.ActionTriggerType[])Enum.GetValues(types);
            foreach (CustomAction.ActionTriggerType i in vals)
            {
                this.cbTriggers.Items.Add(CustomAction.WhenString(i));
            }
            cbTriggers.SelectedIndex = 0;
            types = typeof(CustomAction.ActionType);
            CustomAction.ActionType[] vals2 = (CustomAction.ActionType[])Enum.GetValues(types);
            foreach (CustomAction.ActionType i in vals2)
            {
                this.cbActions.Items.Add(CustomAction.ActionString(i));
            }
            cbActions.SelectedIndex = 0;
            if (actions != null)
            {
                foreach (CustomAction action in actions)
                {
                    lstCustomActions.Items.Add(action);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbTriggers_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTriggerData.Enabled = CustomAction.UsesTriggerData((CustomAction.ActionTriggerType)cbTriggers.SelectedIndex);
        }

        private void cbActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtActionData.Enabled = CustomAction.UsesActionData((CustomAction.ActionType)cbActions.SelectedIndex);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CustomAction entry = new CustomAction();
            entry.TriggerData = txtTriggerData.Text;
            entry.Trigger = (CustomAction.ActionTriggerType)cbTriggers.SelectedIndex;
            int value = 0;
            Int32.TryParse(txtPercent.Text, out value);
            entry.Chance = value;
            CustomAction.Action action = new CustomAction.Action();
            action.ActionData = txtActionData.Text;
            action.Type = (CustomAction.ActionType)cbActions.SelectedIndex;
            entry.Actions.Add(action);
            lstCustomActions.Items.Add(entry);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstCustomActions.SelectedIndex != -1)
            {
                lstCustomActions.Items.RemoveAt(lstCustomActions.SelectedIndex);
            }
        }

        private void btnAddSubaction_Click(object sender, EventArgs e)
        {
            if (lstCustomActions.SelectedIndex < 0 || lstCustomActions.SelectedIndex > (lstCustomActions.Items.Count - 1))
            {
                MessageBox.Show("Cannot add a subaction when a custom action is not selected.");
                return;
            }

            CustomAction entry = (CustomAction)lstCustomActions.Items[lstCustomActions.SelectedIndex];
            CustomAction.Action action = new CustomAction.Action();
            action.ActionData = txtActionData.Text;
            action.Type = (CustomAction.ActionType)cbActions.SelectedIndex;
            entry.Actions.Add(action);
            lstCustomActions.Items[lstCustomActions.SelectedIndex] = entry;
            //lstCustomActions.Refresh();
        }

        private void btnFindRoomTrigger_Click(object sender, EventArgs e)
        {
            SelectRoom dlg = new SelectRoom(_area);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTriggerData.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindRoomAction_Click(object sender, EventArgs e)
        {
            SelectRoom dlg = new SelectRoom(_area);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtActionData.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindMobTrigger_Click(object sender, EventArgs e)
        {
            SelectMob dlg = new SelectMob(_area);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTriggerData.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindMobAction_Click(object sender, EventArgs e)
        {
            SelectMob dlg = new SelectMob(_area);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtActionData.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindObjTrigger_Click(object sender, EventArgs e)
        {
            SelectObject dlg = new SelectObject(_area);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTriggerData.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindObjectAction_Click(object sender, EventArgs e)
        {
            SelectObject dlg = new SelectObject(_area);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtActionData.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnFindDirTrigger_Click(object sender, EventArgs e)
        {
            SelectDirection dlg = new SelectDirection();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTriggerData.Text = dlg.GetText();
            }
        }

        private void btnFindDirAction_Click(object sender, EventArgs e)
        {
            SelectDirection dlg = new SelectDirection();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtActionData.Text = dlg.GetText();
            }
        }

        private void btnFindCmdTrigger_Click(object sender, EventArgs e)
        {
            SelectCommand dlg = new SelectCommand();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTriggerData.Text = dlg.GetText();
            }
        }

        private void btnFindCmdAction_Click(object sender, EventArgs e)
        {
            SelectCommand dlg = new SelectCommand();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtActionData.Text = dlg.GetText();
            }
        }

        private void btnFindSpellTrigger_Click(object sender, EventArgs e)
        {
            SelectSpell dlg = new SelectSpell();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTriggerData.Text = dlg.GetText();
            }
        }

        private void btnFindSpellAction_Click(object sender, EventArgs e)
        {
            SelectSpell dlg = new SelectSpell();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtActionData.Text = dlg.GetText();
            }
        }

        public List<CustomAction> GetActions()
        {
            List<CustomAction> actions = new List<CustomAction>();
            for (int i = 0; i < lstCustomActions.Items.Count; i++)
            {
                actions.Add((CustomAction)lstCustomActions.Items[i]);
            }
            return actions;
        }
    }
}
