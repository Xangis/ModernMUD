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
    public partial class EditQuests : Form
    {
        Area _area = null;
        private MainForm _parent = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent"></param>
        public EditQuests(MainForm parent)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler( EditQuests_FormClosing );
            _parent = parent;
            SetControlAvailability();
        }

        public void UpdateData(ModernMUD.Area area)
        {
            questList.Items.Clear();
            _area = area;
            if (area != null)
            {
                UpdateQuestList();
            }
            SetControlAvailability();
        }

        private void UpdateQuestList()
        {
            int index = questList.SelectedIndex;
            bool empty = (questList.Items.Count == 0);

            questList.Items.Clear();
            foreach (QuestTemplate quest in _area.Quests)
            {
                questList.Items.Add(quest.IndexNumber.ToString());
            }
            if (questList.Items.Count > 0)
            {
                questList.SelectedIndex = 0;
                UpdateWindowContents(_area.Quests[questList.SelectedIndex]);
            }
            else
            {
                questList.SelectedIndex = index;
            }
        }

        public void NavigateTo(int index)
        {
            if (index >= questList.Items.Count)
            {
                return;
            }
            questList.SelectedIndex = index;
            UpdateWindowContents(_area.Quests[index]);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ApplyWindowContents();
            QuestTemplate quest = new QuestTemplate();
            _area.Quests.Add( quest );
            UpdateQuestList();
            SetControlAvailability();
            questList.SelectedIndex = questList.Items.Count - 1;
            UpdateWindowContents(quest);
            _parent.UpdateStatusBar();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (questList.Items.Count > 0 && questList.SelectedIndex > 0)
            {
                ApplyWindowContents();
                --questList.SelectedIndex;
                UpdateWindowContents(_area.Quests[questList.SelectedIndex]);
            }
        }

        private void btnFwd_Click(object sender, EventArgs e)
        {
            if ((questList.SelectedIndex + 1 ) < questList.Items.Count)
            {
                ApplyWindowContents();
                ++questList.SelectedIndex;
                UpdateWindowContents(_area.Quests[questList.SelectedIndex]);
            }
        }

        private void ApplyWindowContents()
        {
            if (questList.SelectedIndex != -1)
            {
                if (!String.IsNullOrEmpty(txtIndexNumber.Text) && (string)questList.Items[questList.SelectedIndex] !=
                    ModernMUD.Color.RemoveColorCodes(txtIndexNumber.Text))
                {
                    questList.SelectedIndexChanged -= questList_SelectedIndexChanged;
                    questList.Items[questList.SelectedIndex] = ModernMUD.Color.RemoveColorCodes(txtIndexNumber.Text);
                    questList.SelectedIndexChanged += questList_SelectedIndexChanged;
                }
                int indexNumber = 0;
                if (Int32.TryParse(txtIndexNumber.Text, out indexNumber))
                {
                    _area.Quests[questList.SelectedIndex].IndexNumber = indexNumber;
                }
                _area.Quests[questList.SelectedIndex].Messages.Clear();
                foreach (object obj in lstMessages.Items)
                {
                    if (obj is TalkData)
                    {
                        _area.Quests[questList.SelectedIndex].Messages.Add((TalkData)obj);
                    }
                }
                foreach (object obj in lstQuests.Items)
                {
                    if (obj is QuestData)
                    {
                        _area.Quests[questList.SelectedIndex].Quests.Add((QuestData)obj);
                    }
                }
            }
        }

        void UpdateWindowContents(QuestTemplate quest)
        {
            txtIndexNumber.Text = quest.IndexNumber.ToString();
            lstMessages.Items.Clear();
            lstQuests.Items.Clear();
            foreach (TalkData msg in quest.Messages)
            {
                lstMessages.Items.Add(msg);
            }
            foreach (QuestData qst in quest.Quests)
            {
                lstQuests.Items.Add(qst);
            }
        }

        private void questList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateWindowContents(_area.Quests[questList.SelectedIndex]);
        }

        /// <summary>
        /// Resets the window by telling it to refresh with the currently selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyWindowContents();
            Close();
        }

        private void EditQuests_FormClosing( object sender, FormClosingEventArgs e )
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void btnAddMessage_Click(object sender, EventArgs e)
        {
            EditExtendedDescription dialog = new EditExtendedDescription(new TalkData());
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                lstMessages.Items.Add(dialog.GetTalkData());
            }
        }

        private void btnRemoveMessage_Click(object sender, EventArgs e)
        {
            if (lstMessages.SelectedIndex != -1)
            {
                lstMessages.Items.RemoveAt(lstMessages.SelectedIndex);
            }
        }

        private void btnAddQuest_Click(object sender, EventArgs e)
        {
            EditQuestData dialog = new EditQuestData(new QuestData());
            if( dialog.ShowDialog() == DialogResult.OK)
            {
                lstQuests.Items.Add( dialog.GetQuestData() );
            }
        }

        private void btnRemoveQuest_Click(object sender, EventArgs e)
        {
            if (lstQuests.SelectedIndex != -1)
            {
                lstQuests.Items.RemoveAt(lstQuests.SelectedIndex);
            }
        }

        private void btnEditMessage_Click(object sender, EventArgs e)
        {
            int index = lstMessages.SelectedIndex;
            if (index != -1)
            {
                TalkData desc = lstMessages.SelectedItem as TalkData;
                EditExtendedDescription dialog = new EditExtendedDescription(desc);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    lstMessages.Items.RemoveAt(index);
                    lstMessages.Items.Insert(index, dialog.GetTalkData());
                }
            }
        }

        private void btnEditQuest_Click(object sender, EventArgs e)
        {
            int index = lstQuests.SelectedIndex;
            if (index != -1)
            {
                QuestData data = lstQuests.SelectedItem as QuestData;
                EditQuestData dialog = new EditQuestData(data);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    lstQuests.Items.RemoveAt(index);
                    lstQuests.Items.Insert(index, dialog.GetQuestData());
                }
            }
        }

        private void txtIndexNumber_TextChanged(object sender, EventArgs e)
        {
            int indexNumber = 0;
            Int32.TryParse(txtIndexNumber.Text, out indexNumber);
            foreach (MobTemplate mob in _area.Mobs)
            {
                if (mob.IndexNumber == indexNumber)
                {
                    MainForm.BuildRTFString(mob.ShortDescription, rtbMobName);
                    return;
                }
            }
            rtbMobName.Text = String.Empty;
        }

        private void btnFindIndexNumber_Click( object sender, EventArgs e )
        {
            SelectMob dlg = new SelectMob( _area );
            if( dlg.ShowDialog() == DialogResult.OK )
            {
                txtIndexNumber.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = questList.SelectedIndex;
            if (index == -1) return;
            if (index < questList.Items.Count)
            {
                _area.Quests.RemoveAt(index);
                questList.Items.RemoveAt(index);
                SetControlAvailability();
            }
            if (index > 0)
            {
                --index;
            }
            if (index >= 0 && index < questList.Items.Count)
            {
                questList.SelectedIndex = index;
                UpdateWindowContents(_area.Quests[questList.SelectedIndex]);
            }
        }

        private void SetControlAvailability()
        {
            bool enabled = true;
            if (_area == null || _area.Quests.Count < 1)
            {
                enabled = false;
            }
            txtIndexNumber.Enabled = enabled;
            btnAddMessage.Enabled = enabled;
            btnAddQuest.Enabled = enabled;
            btnBack.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btnEditMessage.Enabled = enabled;
            btnEditQuest.Enabled = enabled;
            btnFindIndexNumber.Enabled = enabled;
            btnFwd.Enabled = enabled;
            btnRemoveMessage.Enabled = enabled;
            btnRemoveQuest.Enabled = enabled;
        }
    }
}