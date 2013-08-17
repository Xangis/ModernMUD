using System;

namespace MUDEngine
{
    [Serializable]
    public class ExtendedTerrain
    {
        public ExtendedTerrain( int cextendedSector, string croomTitle, string croomDescription, int cmainSector,
                          string cmapChar, int cvisibility, char cforeground, char cbackground, char csymbol )
        {
            ExtendedSectorType = cextendedSector;
            RoomTitle = croomTitle;
            RoomDescription = croomDescription;
            MainSector = cmainSector;
            MapCharacter = cmapChar;
            SightRange = cvisibility;
            ForegroundColor = cforeground;
            BackgroundColor = cbackground;
            Symbol = csymbol;
        }

        public int ExtendedSectorType { get; set; }
        public string RoomTitle { get; set; }
        public string RoomDescription { get; set; }
        public int MainSector { get; set; } // Actual base terrain type-
        public string MapCharacter { get; set; } // Ansi displayed for that sector
        public int SightRange { get; set; }

        /// <summary>
        /// ANSI code for foreground color.
        /// </summary>
        public char ForegroundColor { get; set; }

        /// <summary>
        /// ANSI code for background color.
        /// </summary>
        public char BackgroundColor { get; set; }

        /// <summary>
        /// ASCII character code for this terrain square.
        /// </summary>
        public char Symbol { get; set; }

        /// <summary>
        /// Special case for zone marker, which isn't in the core terrain types.
        /// 
        /// TODO: Make this a little less awkward - possibly put it in the main terrain type list.
        /// </summary>
        public const int EXT_ZONEMARKER = 45;
    };

}
