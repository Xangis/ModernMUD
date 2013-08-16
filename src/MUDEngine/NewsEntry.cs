using System;
using System.Collections.Generic;
using System.Text;

namespace MUDEngine
{
    /// <summary>
    /// Represents a news entry, which will be shown when someone types the "news" command.
    /// </summary>
    [Serializable]
    public class NewsEntry
    {
        public string Date { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string ColorCode { get; set; }
    }
}
