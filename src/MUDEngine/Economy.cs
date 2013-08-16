using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Data used to track the scarcity/abundance of materials and adjust prices accordingly.
    /// </summary>
    public class Economy
    {
        private int[] _materialPriceModifiers = new int[Material.Table.Length];

        /// <summary>
        /// Default constructor.
        /// </summary>
        Economy()
        {
            // Just like scarcity, prices are in hundredths of a percent.
            int count;
            for( count = 0; count < Material.Table.Length; count++ )
            {
                _materialPriceModifiers[ count ] = 10000;
            }
        }

        /// <summary>
        /// Adjustments to prices based upon construction materials.
        /// </summary>
        public int[] MaterialPriceModifiers
        {
            get { return _materialPriceModifiers; }
            set { _materialPriceModifiers = value; }
        }
    };

}