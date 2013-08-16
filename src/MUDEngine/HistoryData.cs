
namespace MUDEngine
{
    /// <summary>
    /// Command history data structure.
    /// </summary>
    public class HistoryData
    {
        public string Command { get; set; }

        public static implicit operator bool( HistoryData h )
        {
            if (h == null)
            {
                return false;
            }
            return true;
        }
    };
}