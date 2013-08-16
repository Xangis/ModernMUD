using System;

namespace MUDEngine
{
    /// <summary>
    /// Defines a site ban.
    /// </summary>
    public class BanData
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public static int NumBanData { get; set; }
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the number of in-memory site bans.
        /// </summary>
        public static int Count
        {
            get
            {
                return NumBanData;
            }
        }

        public BanData()
        {
            Name = String.Empty;
            Created = DateTime.Now;
            Reason = String.Empty;
            ++NumBanData;
        }

        ~BanData()
        {
            --NumBanData;
        }
    };
}
