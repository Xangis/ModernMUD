using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents a bard song, or more generally, magic that can be invoked
    /// by singing or playing an instrument.
    /// </summary>
    public class Song
    {
        /// <summary>
        /// Number of songs in memory.
        /// </summary>
        public static int NumSongs { get; set;  }
        /// <summary>
        /// Full name of songs.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Circle needed by class. Akin to spell circles.
        /// </summary>
        public int[] SongCircle = new int[ Limits.MAX_CLASS ];
        /// <summary>
        /// Short name of song.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Defines the function executed for a song.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="song"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public delegate bool SongFunction( CharData ch, Song song, int level, Target target );
        /// <summary>
        /// The function executed for the song.
        /// </summary>
        public SongFunction Function { get; set; }
        /// <summary>
        /// Legal targets for the song.
        /// </summary>
        public TargetType ValidTargets { get; set; }
        /// <summary>
        /// Can you sing the song while fighting?
        /// </summary>
        public bool CanSingInCombat { get; set; }
        /// <summary>
        /// Minimum mana used.
        /// </summary>
        public int MinimumMana { get; set; }
        /// <summary>
        /// Wait time after using.
        /// </summary>
        public int Delay { get; set; }
        /// <summary>
        /// Damage message sent to the singer.
        /// </summary>
        public string MessageDamage { get; set; }
        /// <summary>
        /// Damage message sent to the target.
        /// </summary>
        public string MessageDamageToVictim { get; set; }
        /// <summary>
        /// Damage message sent to the room.
        /// </summary>
        public string MessageDamageToRoom { get; set; }
        /// <summary>
        /// Message sent to self when damage is done to self.
        /// </summary>
        public string MessageDamageToSelf { get; set; }
        /// <summary>
        /// Message sent to room when damage is done to self.
        /// </summary>
        public string MessageDamageSelfToRoom { get; set; }
        /// <summary>
        /// Kill message.
        /// </summary>
        public string MessageKill { get; set; }
        /// <summary>
        /// Wear off message.
        /// </summary>
        public string MessageWearOff { get; set; }
        /// <summary>
        /// Instrument reuquired for the song.
        /// </summary>
        public int Instrument { get; set; }
        /// <summary>
        /// Performance type - how the song is performed.
        /// </summary>
        public int PerformanceType { get; set; }
        /// <summary>
        /// Can this song be written down?
        /// </summary>
        public bool CanScribe { get; set; }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="fnam"></param>
        /// <param name="nam"></param>
        /// <param name="func"></param>
        /// <param name="targ"></param>
        /// <param name="combat"></param>
        /// <param name="minmana"></param>
        /// <param name="time"></param>
        /// <param name="dmg"></param>
        /// <param name="dmgvict"></param>
        /// <param name="dmgroom"></param>
        /// <param name="dmgself"></param>
        /// <param name="dmgselfrm"></param>
        /// <param name="kill"></param>
        /// <param name="wearoff"></param>
        /// <param name="schools"></param>
        /// <param name="manatype"></param>
        /// <param name="scribe"></param>
        public Song( string fnam, string nam, SongFunction func, TargetType targ, bool combat,int minmana, int time, string dmg, string dmgvict, string dmgroom, string dmgself, string dmgselfrm, string kill, string wearoff, int schools, int manatype, bool scribe )
        {
            FullName = fnam;
            Name = nam;
            Function = func;
            ValidTargets = targ;
            CanSingInCombat = combat;
            MinimumMana = minmana;
            Delay = time;
            MessageDamage = dmg;
            MessageDamageToVictim = dmgvict;
            MessageDamageToRoom = dmgroom;
            MessageDamageToSelf = dmgself;
            MessageDamageSelfToRoom = dmgselfrm;
            MessageKill = kill;
            MessageWearOff = wearoff;
            Instrument = schools;
            PerformanceType = manatype;
            CanScribe = scribe;
            ++NumSongs;
        }
    }
}