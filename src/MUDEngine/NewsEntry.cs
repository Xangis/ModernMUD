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
        /// <summary>
        /// Date posted.
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Details of the news entry.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Poster.
        /// </summary>
        public string Author { get; set; }
        public string ColorCode { get; set; }
    }
}
