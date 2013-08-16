using System;

namespace MUDEngine
{
    /// <summary>
    /// Represents a sum of money.
    /// </summary>
    [Serializable]
    public class Coins
    {
        public int Copper { get; set; }
        public int Silver { get; set; }
        public int Gold { get; set; }
        public int Platinum { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Coins()
        {
            Copper = 0;
            Silver = 0;
            Gold = 0;
            Platinum = 0;
        }

        /// <summary>
        /// Fills a coin structure based on some input text.  Valid inputs include:
        /// 3 platinum
        /// all.coins
        /// 1 p
        /// 2 p 3 copper all.gold
        /// all.silver
        /// all.g
        /// a
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool FillFromString( string[] str, CharData ch )
        {
            Copper = 0;
            Silver = 0;
            Gold = 0;
            Platinum = 0;

            if( ch == null || str.Length < 1 || String.IsNullOrEmpty(str[0] ))
            {
                return false;
            }

            int currentNumber = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ("all.coins".StartsWith(str[i]))
                {
                    Copper = ch.GetCopper();
                    Silver = ch.GetSilver();
                    Gold = ch.GetGold();
                    Platinum = ch.GetPlatinum();
                    return true;
                }
                else if ("all.copper".StartsWith(str[i]))
                {
                    Copper = ch.GetCopper();
                }
                else if ("all.silver".StartsWith(str[i]))
                {
                    Silver = ch.GetSilver();
                }
                else if ("all.gold".StartsWith(str[i]))
                {
                    Gold = ch.GetGold();
                }
                else if ("all.platinum".StartsWith(str[i]))
                {
                    Platinum = ch.GetPlatinum();
                }
                else if ("copper".StartsWith(str[i]))
                {
                    Copper = currentNumber;
                }
                else if ("silver".StartsWith(str[i]))
                {
                    Silver = currentNumber;
                }
                else if ("gold".StartsWith(str[i]))
                {
                    Gold = currentNumber;
                }
                else if ("platinum".StartsWith(str[i]))
                {
                    Platinum = currentNumber;
                }
                // Treat it as a number.
                else
                {
                    Int32.TryParse(str[i], out currentNumber);
                }
            }
            if (Copper > 0 || Silver > 0 || Gold > 0 || Platinum > 0)
            {
                return true;
            }
            return false;
        }
    }
}