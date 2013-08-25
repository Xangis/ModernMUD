
namespace MUDEngine
{
    /// <summary>
    /// Command history data structure.
    /// </summary>
    public class HistoryData
    {
        /// <summary>
        /// The command.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Lets us check for null.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public static implicit operator bool( HistoryData h )
        {
            if (h == null)
            {
                return false;
            }
            return true;
        }
    }
}