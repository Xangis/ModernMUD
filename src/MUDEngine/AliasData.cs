using System;

namespace MUDEngine
{
    /// <summary>
    /// For alias output.
    /// </summary>
    [Serializable]
    public class AliasData
    {
        /// <summary>
        /// The command typed for the alias to fire.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// What is sent instead of the command text.
        /// </summary>
        public string Substitution { get; set; }
    }
}
