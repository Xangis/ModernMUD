
namespace MUDEngine
{
    /// <summary>
    /// Represents a syllable replacement for language translations.
    /// </summary>
    public class SyllableType
    {
        public SyllableType( string oldsyl, string newsyl )
        {
            Old = oldsyl;
            New = newsyl;
        }

        public string New { get; set; }

        public string Old { get; set; }
    };
}
