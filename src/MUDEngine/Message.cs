using System;

namespace MUDEngine
{
    /// <summary>
    /// Represents a player-to-player message (mail message, note, etc.)
    /// </summary>
    [Serializable]
    public class Message
    {
        public static int NumMessages { get; set; }

        public Message()
        {
            ++NumMessages;
            Sender = String.Empty;
            RecipientList = String.Empty;
            Subject = String.Empty;
            TextBody = String.Empty;
            TimeStamp = DateTime.Now;
        }
        ~Message()
        {
            --NumMessages;
        }

        public string Sender { get; set; }
        public string RecipientList { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public DateTime TimeStamp { get; set; }
    };

}