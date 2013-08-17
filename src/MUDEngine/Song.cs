using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents a bard song.
    /// </summary>
    public class Song
    {
        public static int _numSongs;
        public string _fullName;		   /* Name of skill              */
        public int[] _spellCircle = new int[ Limits.MAX_CLASS ]; /* Circle needed by class     */
        public string _name;			   /* Name of skill		 */
        public delegate bool spell_fun( CharData ch, Spell spell, int level, Target target );  /* Spell pointer (for songs) */
        public spell_fun _function;
        public TargetType _targetType;			   /* Legal targets		 */
        public bool _canCastInCombat;		   /* Can you cast it in a fight?*/
        public int _minimumMana;		   /* Minimum mana used		 */
        public int _delay;			   /* Waiting time after use	 */
        public string _messageDamage;		   /* Damage message		 */
        public string _messageDamageToVictim;	   /* Damage message to victim	 */
        public string _messageDamageToRoom;	   /* Damage message to room	 */
        public string _messageDamageToSelf;	   /* Damage message _targetType self */
        public string _messageDamageSelfToRoom;	   /* Room dmg msg, self = _targetType*/
        public string _messageKill;                  /* Kill Message               */
        public string _messageWearOff;		   /* Wear off message		 */
        public int _instrument;			   /* Skill realm requirements
                                                 (Instrument required for bards) */
        public int _performanceType;		   /* Mana type (for spells)
                                                  Instrument type (for bards) */
        public bool _canScribe;                /* Can the spell be scribed?  */

        public Song( string fnam, string nam, spell_fun func, TargetType targ, bool combat,int minmana, int time, string dmg, string dmgvict, string dmgroom, string dmgself, string dmgselfrm, string kill, string wearoff, int schools, int manatype, bool scribe )
        {
            _fullName = fnam;
            _name = nam;
            _function = func;
            _targetType = targ;
            _canCastInCombat = combat;
            _minimumMana = minmana;
            _delay = time;
            _messageDamage = dmg;
            _messageDamageToVictim = dmgvict;
            _messageDamageToRoom = dmgroom;
            _messageDamageToSelf = dmgself;
            _messageDamageSelfToRoom = dmgselfrm;
            _messageKill = kill;
            _messageWearOff = wearoff;
            _instrument = schools;
            _performanceType = manatype;
            _canScribe = scribe;
            ++_numSongs;
        }
    }
}