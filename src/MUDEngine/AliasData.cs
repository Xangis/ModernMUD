using System;

namespace MUDEngine
{
    /// <summary>
    /// For alias output.
    /// </summary>
    [Serializable]
    public class AliasData
    {
        public string Command { get; set; }
        public string Substitution { get; set; }
    };
}
