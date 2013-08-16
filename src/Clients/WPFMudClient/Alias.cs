using System;

namespace WPFMudClient
{
    /// <summary>
    /// Used to define aliases - shortcut commands, such as "ex" for "examine".
    /// </summary>
    public struct Alias
    {
        public String Keyword;
        public String Expansion;
    }
}
