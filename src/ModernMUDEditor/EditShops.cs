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
    public partial class EditShops : Form
    {
        Area _area = null;
        private MainForm _parent = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent"></param>
        public EditShops(MainForm parent)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler( EditShops_FormClosing );
            _parent = parent;
            SetControlAvailability();
        }

        public void UpdateData(Area area)
        {
            shopList.Items.Clear();
            _area = area;
            if (area != null)
            {
                UpdateShopList();
            }
            SetControlAvailability();
        }

        private void UpdateShopList()
        {
            int index = shopList.SelectedIndex;
            bool empty = (shopList.Items.Count == 0);

            shopList.Items.Clear();
            foreach (Shop shop in _area.Shops)
            {
                shopList.Items.Add(shop.Keeper.ToString());
            }
            if (shopList.Items.Count > 0)
            {
                shopList.SelectedIndex = 0;
                UpdateWindowContents(_area.Shops[shopList.SelectedIndex]);
            }
            else
            {
                shopList.SelectedIndex = index;
            }
        }

        public void NavigateTo(int index)
        {
            if (index >= shopList.Items.Count)
            {
                return;
            }
            shopList.SelectedIndex = index;
            UpdateWindowContents(_area.Shops[index]);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ApplyWindowContents();
            Shop shop = new Shop();
            _area.Shops.Add( shop );
            UpdateShopList();
            SetControlAvailability();
            shopList.SelectedIndex = shopList.Items.Count - 1;
            UpdateWindowContents(shop);
            _parent.UpdateStatusBar();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (shopList.Items.Count > 0 && shopList.SelectedIndex > 0)
            {
                ApplyWindowContents();
                --shopList.SelectedIndex;
                UpdateWindowContents(_area.Shops[shopList.SelectedIndex]);
            }
        }

        private void btnFwd_Click(object sender, EventArgs e)
        {
            if ((shopList.SelectedIndex + 1) < shopList.Items.Count)
            {
                ApplyWindowContents();
                ++shopList.SelectedIndex;
                UpdateWindowContents(_area.Shops[shopList.SelectedIndex]);
            }
        }

        void UpdateWindowContents(Shop shop)
        {
            txtKeeperIndexNumber.Text = shop.Keeper.ToString();
            txtBuyPercent.Text = shop.PercentBuy.ToString();
            txtSellPercent.Text = shop.PercentSell.ToString();
            txtOpenHour.Text = shop.OpenHour.ToString();
            txtCloseHour.Text = shop.CloseHour.ToString();
            lstBuyTypes.Items.Clear();
            foreach (int item in shop.BuyTypes)
            {
                lstBuyTypes.Items.Add(ObjTemplate.ItemTypeString((ObjTemplate.ObjectType)item));
            }
            lstItemsForSale.Items.Clear();
            foreach (int item in shop.ItemsForSale)
            {
                lstItemsForSale.Items.Add(item.ToString());
            }
        }


        private void ApplyWindowContents()
        {
            if (shopList.SelectedIndex != -1)
            {
                if (!String.IsNullOrEmpty(txtKeeperIndexNumber.Text) && (string)shopList.Items[shopList.SelectedIndex] !=
                    txtKeeperIndexNumber.Text)
                {
                    shopList.SelectedIndexChanged -= shopList_SelectedIndexChanged;
                    shopList.Items[shopList.SelectedIndex] = ModernMUD.Color.RemoveColorCodes(txtKeeperIndexNumber.Text);
                    shopList.SelectedIndexChanged += shopList_SelectedIndexChanged;
                }
                int value = 0;
                if (Int32.TryParse(txtKeeperIndexNumber.Text, out value))
                {
                    _area.Shops[shopList.SelectedIndex].Keeper = value;
                }
                if (Int32.TryParse(txtBuyPercent.Text, out value))
                {
                    _area.Shops[shopList.SelectedIndex].PercentBuy = value;
                }
                if (Int32.TryParse(txtSellPercent.Text, out value))
                {
                    _area.Shops[shopList.SelectedIndex].PercentSell = value;
                }
                if (Int32.TryParse(txtOpenHour.Text, out value))
                {
                    _area.Shops[shopList.SelectedIndex].OpenHour = value;
                }
                if (Int32.TryParse(txtCloseHour.Text, out value))
                {
                    _area.Shops[shopList.SelectedIndex].CloseHour = value;
                }
                List<ObjTemplate.ObjectType> buyTypes = new List<ObjTemplate.ObjectType>();
                foreach (object obj in lstBuyTypes.Items)
                {
                    buyTypes.Add(ObjTemplate.ItemTypeLookup((string)obj));
                }
                _area.Shops[shopList.SelectedIndex].BuyTypes = buyTypes;
                List<int> itemsForSale = new List<int>();
                foreach (object obj in lstItemsForSale.Items)
                {
                    if( Int32.TryParse((string)obj, out value))
                        itemsForSale.Add(value);
                }
                _area.Shops[shopList.SelectedIndex].ItemsForSale = itemsForSale;
            }
        }

        private void shopList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateWindowContents(_area.Shops[shopList.SelectedIndex]);
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

        private void EditShops_FormClosing( object sender, FormClosingEventArgs e )
        {
            Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void txtKeeperIndexNumber_TextChanged(object sender, EventArgs e)
        {
            int indexNumber = 0;
            Int32.TryParse(txtKeeperIndexNumber.Text, out indexNumber);
            foreach (MobTemplate mob in _area.Mobs)
            {
                if (mob.IndexNumber == indexNumber)
                {
                    MainForm.BuildRTFString(mob.ShortDescription,rtbKeeperName);
                    return;
                }
            }
            rtbKeeperName.Text = String.Empty;
        }

        private void btnAddSale_Click(object sender, EventArgs e)
        {
            NumberInput input = new NumberInput();
            if (input.ShowDialog() == DialogResult.OK)
            {
                lstItemsForSale.Items.Add(input.Value.ToString());
            }
        }

        private void btnRemoveSale_Click(object sender, EventArgs e)
        {
            if (lstItemsForSale.SelectedIndex != -1)
            {
                lstItemsForSale.Items.RemoveAt(lstItemsForSale.SelectedIndex);
            }
        }

        private void btnAddBuy_Click(object sender, EventArgs e)
        {
            ItemTypeSelector input = new ItemTypeSelector();
            if (input.ShowDialog() == DialogResult.OK)
            {
                lstBuyTypes.Items.Add(input.Value);
            }
        }

        private void btnRemoveBuy_Click(object sender, EventArgs e)
        {
            if (lstBuyTypes.SelectedIndex != -1)
            {
                lstBuyTypes.Items.RemoveAt(lstBuyTypes.SelectedIndex);
            }
        }

        private void btnFindKeeper_Click( object sender, EventArgs e )
        {
            SelectMob dlg = new SelectMob( _area );
            if( dlg.ShowDialog() == DialogResult.OK )
            {
                txtKeeperIndexNumber.Text = dlg.GetIndexNumberString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = shopList.SelectedIndex;
            if (index == -1) return;
            if (index < shopList.Items.Count)
            {
                _area.Shops.RemoveAt(index);
                shopList.Items.RemoveAt(index);
                SetControlAvailability();
            }
            if (index > 0)
            {
                --index;
            }
            if (index >= 0 && index < shopList.Items.Count)
            {
                shopList.SelectedIndex = index;
                UpdateWindowContents(_area.Shops[shopList.SelectedIndex]);
            }
        }

        private void SetControlAvailability()
        {
            bool enabled = true;
            if (_area == null || _area.Shops.Count < 1)
            {
                enabled = false;
            }
            txtBuyPercent.Enabled = enabled;
            txtCloseHour.Enabled = enabled;
            txtKeeperIndexNumber.Enabled = enabled;
            txtOpenHour.Enabled = enabled;
            txtSellPercent.Enabled = enabled;
            btnAddBuy.Enabled = enabled;
            btnAddSale.Enabled = enabled;
            btnBack.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btnFindKeeper.Enabled = enabled;
            btnFwd.Enabled = enabled;
            btnRemoveBuy.Enabled = enabled;
            btnRemoveSale.Enabled = enabled;
        }
    }
}