using System;

namespace MUDEngine
{
    /// <summary>
    /// Represents spell memorization data.
    /// </summary>
    public class MemorizeData
    {
        public string Name { get; set; }
        public int Memtime { get; set; } // time in pulses
        public int FullMemtime { get; set; } // time in pulses
        public int Circle { get; set; }
        public bool Memmed { get; set; }
        private static int _numMemorizeData;

        public MemorizeData()
        {
            Name = String.Empty;
            Memtime = 0;
            FullMemtime = 0;
            Circle = 1;
            Memmed = false;
            ++_numMemorizeData;
        }

        ~MemorizeData()
        {
            --_numMemorizeData;
        }

        public static implicit operator bool( MemorizeData md )
        {
            if( md == null )
                return false;
            return true;
        }

        public static int Count
        {
            get
            {
                return _numMemorizeData;
            }
        }
    };
}
