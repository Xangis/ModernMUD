using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// In-game shop information.
    /// </summary>
    [Serializable]
    public class Shop
    {
        private static int _numShops;
        private int _keeper;
        private List<ObjTemplate.ObjectType> _buyTypes = new List<ObjTemplate.ObjectType>();
        private List<int> _itemsForSale = new List<int>();
        private int _percentBuy;
        private int _percentSell;
        private int _openHour;
        private int _closeHour;
        [XmlIgnore]
        private Area _area;

        /// <summary>
        /// Deafault constructor.
        /// </summary>
        public Shop()
        {
            ++_numShops;

            _keeper = 0;
            _percentBuy = 100;
            _percentSell = 100;
            _openHour = 0;
            _closeHour = 23;
        }

        /// <summary>
        /// Default destructor, decrements the number of in-memory Shops.
        /// </summary>
        ~Shop()
        {
            --_numShops;
            return;
        }

        /// <summary>
        /// The index number of the shopkeeper associated with this shop.
        /// </summary>
        public int Keeper
        {
            get { return _keeper; }
            set { _keeper = value; }
        }

        /// <summary>
        /// A list of the virtual numbers of objects this shop always has for sale.
        /// </summary>
        public List<int> ItemsForSale
        {
            get { return _itemsForSale; }
            set { _itemsForSale = value; }
        }

        /// <summary>
        /// The percentage multiplier for buy transactions.
        /// </summary>
        public int PercentBuy
        {
            get { return _percentBuy; }
            set { _percentBuy = value; }
        }

        /// <summary>
        /// The percentage multiplier for sell transactions.
        /// </summary>
        public int PercentSell
        {
            get { return _percentSell; }
            set { _percentSell = value; }
        }

        /// <summary>
        /// The hour of the day at which this shop opens.
        /// </summary>
        public int OpenHour
        {
            get { return _openHour; }
            set { _openHour = value; }
        }

        /// <summary>
        /// The hour of the day at which this shop closes.
        /// </summary>
        public int CloseHour
        {
            get { return _closeHour; }
            set { _closeHour = value; }
        }

        /// <summary>
        /// The area that this shop is associated with.
        /// </summary>
        [XmlIgnore]
        public Area Area
        {
            get { return _area; }
            set { _area = value; }
        }

        /// <summary>
        /// The item types that the shopkeeper is willing to buy.
        /// </summary>
        public List<ObjTemplate.ObjectType> BuyTypes
        {
            get { return _buyTypes; }
            set { _buyTypes = value; }
        }

        /// <summary>
        /// The number of in-memory representations of the Shop class.
        /// </summary>
        [XmlIgnore]
        public static int Count
        {
            get
            {
                return _numShops;
            }
        }

        /// <summary>
        /// Allows us to use if(Shop) to check for null.
        /// </summary>
        /// <param name="sh"></param>
        /// <returns></returns>
        public static implicit operator bool( Shop sh )
        {
            if (sh == null)
            {
                return false;
            }
            return true;
        }

    };
}