using System;
using System.Collections.Generic;
using System.Text;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Communication Channels
    /// </summary>
    [Flags]
    public enum TalkChannel
    {
        immortal = Bitvector.BV00,
        shout = Bitvector.BV01,
        yell = Bitvector.BV02,
        guild = Bitvector.BV03
    }

}
