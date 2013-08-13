using System;
using System.Collections.Generic;
using System.Text;

namespace ModernMUD
{
    /// <summary>
    /// Represents justice types available for an area.
    /// </summary>
    public enum JusticeType
    {
        /// <summary>
        /// Crimes legal, all are welcome.
        /// </summary>
        none = 0,
        /// <summary>
        /// Crimes illegal, evils banned.
        /// </summary>
        good,
        /// <summary>
        /// Crimes illegal, goods banned.
        /// </summary>
        evil,
        /// <summary>
        /// Crimes illegal, all are welcome.
        /// </summary>
        neutral,
        /// <summary>
        /// Crimes illegal, only neutrals are welcome.
        /// </summary>
        neutral_only,
        /// <summary>
        /// Crimes legal, evils banned.
        /// </summary>
        chaotic_good,
        /// <summary>
        /// Crimes legal, goods banned.
        /// </summary>
        chaotic_evil,
        /// <summary>
        /// Crimes legal, only goods welcome.
        /// </summary>
        chaotic_good_only,
        /// <summary>
        /// Crimes legal, only evils welcome.
        /// </summary>
        chaotic_evil_only,
        /// <summary>
        /// Crimes legal, only neutrals are welcome.
        /// </summary>
        chaotic_neutral_only,
        /// <summary>
        /// Crimes illegal, evils and neutrals banned.
        /// </summary>
        good_only,
        /// <summary>
        /// Crimes illegal, goods and neutrals banned.
        /// </summary>
        evil_only
    }

}
