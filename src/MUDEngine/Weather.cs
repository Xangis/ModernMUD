using System;

namespace MUDEngine
{
    /// <summary>
    /// Represents the current weather state for the MUD.
    /// </summary>
    [Serializable]
    public class Weather
    {
        public int BarometricPressure { get; set; }
        public int Change { get; set; }
        public Sysdata.SkyType Sky { get; set; }
        public SunType Sunlight { get; set; }
        public Sysdata.MoonPhase MoonPhase { get; set; }
        public int Moonday { get; set; }
        public int WindDirection { get; set; }
        public int Temperature { get; set; }
        public int WindSpeed{ get; set; }

        public static bool operator !(Weather w)
        {
            if (w == null)
            {
                return true;
            }
            return false;
        }

        public static implicit operator bool(Weather w)
        {
            if (w == null)
            {
                return false;
            }
            return true;
        }
    }
}