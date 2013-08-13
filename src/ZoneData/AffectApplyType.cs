using System;
using System.Collections.Generic;
using System.Text;

namespace ModernMUD
{
    /// <summary>
    /// Represents an affect applied to something in a particular amount.
    /// </summary>
    [Serializable]
    public class AffectApplyType
    {
        /// <summary>
        /// Thing that the affect has been applied to (strength, etc.)
        /// </summary>
        public Affect.Apply Location { get; set; }
        /// <summary>
        /// Affect modifier, i.e. +3.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// String conversion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Modifies " + Location.ToString() + " by " + Amount.ToString() + ".";
        }
    }
}
