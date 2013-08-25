
namespace MUDEngine
{
    /// <summary>
    /// Data representing a spell that is currently being cast.
    /// </summary>
    public class CastData
    {
        /// <summary>
        /// The event that will fire when the spell casting finishes.
        /// </summary>
        public Event Eventdata { get; set; }

        /// <summary>
        /// The person casting the spell.
        /// </summary>
        public CharData Who { get; set; }
    };
}
