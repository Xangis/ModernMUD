using System;

namespace MUDEngine
{
    /// <summary>
    /// Represents an issue, whether it be a typo, bug, suggestion, etc.
    /// </summary>
    public class IssueEntry
    {
        public string Name { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Text { get; set; }

        public IssueEntry( string playerName, string text )
        {
            Name = playerName;
            Text = text;
            UpdateTime = DateTime.Now;
        }

        public IssueEntry()
        {
        }
    }
}
