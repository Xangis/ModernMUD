using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Used for keeping track of total items created and destroyed.
    /// </summary>
    public class ItemStatus
    {
        private int _quantity;
        private int _totalCost;
        private int _maxCost;

        public int Quantity
        {
            get
            {
                return _quantity;
            }
        }

        public int TotalCost
        {
            get
            {
                return _totalCost;
            }
        }

        public int MaxCost
        {
            get
            {
                return _maxCost;
            }
        }

        public ItemStatus()
        {
            _quantity = 0;
            _totalCost = 0;
            _maxCost = 0;
        }

        public void AddItem( Object obj )
        {
            // For now we're not giving credit for food and trash items.
            if( obj.ItemType == ObjTemplate.ObjectType.food || obj.ItemType == ObjTemplate.ObjectType.trash )
            {
                return;
            }

            ++_quantity;
            int cost = obj.Cost;
            _totalCost += cost;
            if( cost > _maxCost )
            {
                _maxCost = cost;
            }
        }
    }
}
