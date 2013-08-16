using System;
using System.Collections.Generic;
using System.Text;
using ModernMUD;

namespace ModernMUDEditor
{
    /// <summary>
    /// Represents a room location on the graphical room map.  Used for drawing and for determining click
    /// locations so we can show the proper edit window.
    /// </summary>
    public class RoomLocation
    {
        public System.Drawing.Point Location { get; set; }
        public int Level { get; set; }
        private bool[] _exits = new bool[Limits.MAX_DIRECTION];
        public System.Drawing.Pen RoomColor { get; set; }
        public System.Drawing.Brush SecondaryColor { get; set; }

        public RoomLocation()
        {
        }

        public RoomLocation(System.Drawing.Point point, int level, System.Drawing.Pen pen, System.Drawing.Brush brush)
        {
            Location = point;
            RoomColor = pen;
            SecondaryColor = brush;
            Level = level;
        }

        public bool[] Exits
        {
            get { return _exits; }
            set { _exits = value; }
        }

        /// <summary>
        /// Checks whether a point is within the bounding box of this room location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Contains(int x, int y)
        {
            if (x >= Location.X && x <= Location.X + 10 && y >= Location.Y && y <= Location.Y + 10)
            {
                return true;
            }
            return false;
        }
    }
}
