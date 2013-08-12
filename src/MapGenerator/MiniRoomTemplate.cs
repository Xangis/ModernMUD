using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ModernMUD;

namespace MapGenerator
{
    /// <summary>
    /// A template used for generating basic rooms from a map image.
    /// </summary>
    [Serializable]
    public class MiniRoomTemplate
    {
        /// <summary>
        /// The title used for the generated room, i.e. "The Trackless Swamps of Dread".
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description used for the generated room.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The terrain type used for the generated room.
        /// </summary>
        public TerrainType Terrain { get; set; }
        /// <summary>
        /// The image color that corresponds to this room template.
        /// </summary>
        public Color ImageColor { get; set; }
        /// <summary>
        /// Gets/sets the image color using a integer rather than a Color class.
        /// </summary>
        public int ColorInt
        {
            get
            {
                return ImageColor.ToArgb();
            }
            set
            {
                ImageColor = Color.FromArgb(value);
            }
        }
    }
}
