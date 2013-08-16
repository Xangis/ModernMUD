
namespace MUDEngine
{
    /// <summary>
    /// Data representing a spell that is currently being cast.
    /// </summary>
    public class CastData
    {
        public Event Eventdata { get; set; }
        public CharData Who { get; set; }
    };
}
