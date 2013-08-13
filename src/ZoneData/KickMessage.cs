using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Represents a kick message that can be applied to a particular race.
    /// 
    /// This could probably be generalized for other action or combat messages.
    /// </summary>
    public class KickMessage
    {
        /// <summary>
        /// Message sent to the character performing the action.
        /// </summary>
        public string ToCharMessage { get; set; }

        /// <summary>
        /// Message sent to the people in the room.
        /// </summary>
        public string ToRoomMessage { get; set; }

        /// <summary>
        /// Message sent to the target of the action.
        /// </summary>
        public string ToVictimMessage { get; set; }
    };
}
