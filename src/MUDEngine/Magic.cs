using System;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    public class Magic
    {
        /// <summary>
        /// Spell school flags, used to determine mana type.
        /// </summary>
        [Flags]
        public enum SchoolType
        {
            none = 0,
            abjuration = Bitvector.BV00,
            alteration = Bitvector.BV01,
            conjuration = Bitvector.BV02
        }
        public const int SCHOOL_NONE = 0;		/* no school realm required  */
        public const int SCHOOL_ABJURATION = Bitvector.BV00;		/* abjuration magics	     */
        public const int SCHOOL_ALTERATION = Bitvector.BV01;		/* alteration magics	     */
        public const int SCHOOL_CONJURATION = Bitvector.BV02;		/* conjuration magics	     */
        public const int SCHOOL_SUMMONING = Bitvector.BV03;		/* summoning magics	     */
        public const int SCHOOL_ILLUSION = Bitvector.BV04;		/* illusionist magics	     */
        public const int SCHOOL_PHANTASM = Bitvector.BV05;		/* phantasm projection realm */
        public const int SCHOOL_INVOCATION = Bitvector.BV06;		/* invocation magics         */
        public const int SCHOOL_EVOCATION = Bitvector.BV07;		/* evocative magics	     */
        public const int SCHOOL_ENCHANTMENT = Bitvector.BV08;		/* enchantment magics	     */
        public const int SCHOOL_CHARM = Bitvector.BV09;		/* charm skills		     */
        public const int SCHOOL_DIVINATION = Bitvector.BV10;		/* divinatory arts	     */
        public const int SCHOOL_NECROMANCY = Bitvector.BV11;		/* necromantic skills	     */
        public const int SCHOOL_OFFENSIVE = Bitvector.BV13;		/* offensive martial arts    */
        public const int SCHOOL_DEFENSIVE = Bitvector.BV14;		/* defensive martial arts    */
        public const int SCHOOL_STEALTH = Bitvector.BV15;		/* stealth related skills    */
        public const int SCHOOL_SURVIVAL = Bitvector.BV16;		/* wilderness suvival skills */
        /*
         * Shaman school flags added.
         * These will be used to determine the type of totem needed to cast them.
         */
        public const int SCHOOL_ELEMENTAL = Bitvector.BV17;
        public const int SCHOOL_SPIRITUAL = Bitvector.BV18;
        public const int SCHOOL_ANIMAL = Bitvector.BV19;
        /*
         * Bard flags added.  These will determine whether a spell
         * is primarily of a particular instrument.  Those that can take more than
         * one instrument type will have them specified here.  The mana type will
         * be the primary (most powerful) instrument.  All others will be about
         * 50% as effective
         */
        public const int SCHOOL_HORN = Bitvector.BV20;
        public const int SCHOOL_FLUTE = Bitvector.BV21;
        public const int SCHOOL_MANDOLIN = Bitvector.BV22;
        public const int SCHOOL_LYRE = Bitvector.BV23;
        public const int SCHOOL_DRUMS = Bitvector.BV24;
        public const int SCHOOL_HARP = Bitvector.BV25;
        public const int SCHOOL_PIPES = Bitvector.BV26;
        public const int SCHOOL_FIDDLE = Bitvector.BV27;
        public const int SCHOOL_DULCIMER = Bitvector.BV28;
        public const int SCHOOL_CHRONOMANCY = Bitvector.BV29;
        /*
         * Mana types.
         */
        public const int MANA_ANY = -1;
        public const int MANA_NONE = 0;
        public const int MANA_EARTH = Bitvector.BV00;
        public const int MANA_AIR = Bitvector.BV01;
        public const int MANA_FIRE = Bitvector.BV02;
        public const int MANA_WATER = Bitvector.BV03;
        public const int MANA_TIME = Bitvector.BV04;
        // Bard mana types
        public const int MANA_HORN = Bitvector.BV11;
        public const int MANA_FLUTE = Bitvector.BV12;
        public const int MANA_MANDOLIN = Bitvector.BV13;
        public const int MANA_LYRE = Bitvector.BV14;
        public const int MANA_DRUMS = Bitvector.BV15;
        public const int MANA_HARP = Bitvector.BV16;
        public const int MANA_PIPES = Bitvector.BV17;
        public const int MANA_FIDDLE = Bitvector.BV18;
        public const int MANA_DULCIMER = Bitvector.BV19;
        public const int MANA_INSTRUMENT = Bitvector.BV20; // Instrument required (voice not necessary unless flagged)
        public const int MANA_VOICE = Bitvector.BV21; // Voice requireed
        public const int MANA_DANCE = Bitvector.BV22; // Dance required
        public const int MANA_INSTR_AUGMENT = Bitvector.BV23; // Augmented by instrument
        public const int MANA_DANCE_AUGMENT = Bitvector.BV24; // Augmented by dance
        public const int MANA_VOICE_AUGMENT = Bitvector.BV25; // Augmented by voice

        static void SaySong( CharData ch, Spell spell )
        {
            return;
        }

        static public int[,] LesserMemchart = new int[Limits.MAX_LEVEL, 12]
        // memorization chart for partial spell casters:
        // rangers, paladins, antipaladins
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 10
            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 2, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 2, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0 }, // 20
            { 0, 0, 3, 3, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 3, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 4, 3, 2, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 4, 3, 2, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 4, 3, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 4, 3, 3, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 4, 3, 3, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 4, 4, 3, 2, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 4, 4, 3, 2, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 4, 4, 3, 3, 0, 0, 0, 0, 0, 0 }, // 30
            { 0, 0, 5, 4, 3, 3, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 5, 4, 3, 3, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 5, 4, 4, 3, 2, 0, 0, 0, 0, 0 },
            { 0, 0, 5, 4, 4, 3, 2, 0, 0, 0, 0, 0 },
            { 0, 0, 5, 4, 4, 3, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 5, 5, 4, 3, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 5, 5, 4, 3, 3, 1, 0, 0, 0, 0 },
            { 0, 0, 5, 5, 4, 4, 3, 1, 0, 0, 0, 0 },
            { 0, 0, 5, 5, 4, 4, 3, 2, 0, 0, 0, 0 },
            { 0, 0, 5, 5, 4, 4, 3, 2, 0, 0, 0, 0 }, // 40
            { 0, 0, 5, 5, 5, 4, 3, 3, 1, 0, 0, 0 },
            { 0, 0, 5, 5, 5, 4, 3, 3, 1, 0, 0, 0 },
            { 0, 0, 5, 5, 5, 4, 4, 3, 2, 0, 0, 0 },
            { 0, 0, 5, 5, 5, 4, 4, 3, 2, 0, 0, 0 },
            { 0, 0, 5, 5, 5, 4, 4, 3, 3, 0, 0, 0 },
            { 0, 0, 5, 5, 5, 5, 4, 3, 3, 0, 0, 0 },
            { 0, 0, 5, 5, 5, 5, 4, 3, 3, 1, 0, 0 },
            { 0, 0, 5, 5, 5, 5, 4, 3, 3, 1, 0, 0 },
            { 0, 0, 5, 5, 5, 5, 4, 4, 3, 2, 0, 0 },
            { 0, 0, 5, 5, 5, 5, 4, 4, 3, 2, 1, 0 }, // 50
            { 0, 0, 6, 6, 6, 5, 5, 4, 3, 3, 1, 0 },
            { 0, 0, 6, 6, 6, 5, 5, 4, 3, 3, 2, 0 },
            { 0, 0, 6, 6, 6, 5, 5, 4, 4, 3, 2, 0 },
            { 0, 0, 6, 6, 6, 6, 5, 4, 4, 3, 3, 0 },
            { 0, 0, 6, 6, 6, 6, 5, 4, 4, 3, 3, 0 },
            { 0, 0, 6, 6, 6, 6, 5, 4, 4, 3, 3, 1 },
            { 0, 0, 6, 6, 6, 6, 5, 5, 4, 3, 3, 1 },
            { 0, 0, 6, 6, 6, 6, 5, 5, 4, 3, 3, 2 },
            { 0, 0, 6, 6, 6, 6, 6, 5, 4, 4, 3, 2 },
            { 0, 0, 6, 6, 6, 6, 6, 5, 4, 4, 3, 3 }, // 60
            { 0, 0, 6, 6, 6, 6, 6, 5, 5, 4, 3, 3 },
            { 0, 0, 6, 6, 6, 6, 6, 5, 5, 4, 4, 3 },
            { 0, 0, 6, 6, 6, 6, 6, 5, 5, 4, 4, 3 },
            { 0, 0, 6, 6, 6, 6, 6, 5, 5, 4, 4, 3 }, // 64
            { 0, 0, 6, 6, 6, 6, 6, 5, 5, 4, 4, 3 }, // 65
        };

        static public int[,] Memchart = new int[Limits.MAX_LEVEL, 12] 
        // memorization chart for full spellcasting classes:
        // cleric, shaman, druid, conjurer, necromancer, sorcerer
        {
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 5, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 5, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 5, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 6, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 10
            { 6, 6, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 7, 6, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 7, 6, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 7, 6, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 7, 6, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 7, 6, 6, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 7, 6, 6, 2, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 8, 7, 6, 3, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 8, 7, 6, 4, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 8, 7, 6, 5, 0, 0, 0, 0, 0, 0, 0, 0 }, // 20
            { 8, 7, 6, 6, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 8, 7, 6, 6, 2, 0, 0, 0, 0, 0, 0, 0 },
            { 8, 7, 6, 6, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 8, 7, 7, 6, 4, 0, 0, 0, 0, 0, 0, 0 },
            { 9, 8, 7, 6, 5, 0, 0, 0, 0, 0, 0, 0 },
            { 9, 8, 7, 6, 6, 1, 0, 0, 0, 0, 0, 0 },
            { 9, 8, 7, 6, 6, 2, 0, 0, 0, 0, 0, 0 },
            { 9, 8, 7, 6, 6, 3, 0, 0, 0, 0, 0, 0 },
            { 9, 8, 7, 7, 6, 4, 0, 0, 0, 0, 0, 0 },
            { 9, 8, 7, 7, 6, 5, 0, 0, 0, 0, 0, 0 }, // 30
            { 9, 8, 7, 7, 6, 6, 1, 0, 0, 0, 0, 0 },
            {10, 9, 8, 7, 6, 6, 2, 0, 0, 0, 0, 0 },
            {10, 9, 8, 7, 6, 6, 3, 0, 0, 0, 0, 0 },
            {10, 9, 8, 7, 7, 6, 4, 0, 0, 0, 0, 0 },
            {10, 9, 8, 7, 7, 6, 5, 0, 0, 0, 0, 0 },
            {10, 9, 8, 7, 7, 6, 6, 1, 0, 0, 0, 0 },
            {10, 9, 8, 8, 7, 6, 6, 2, 0, 0, 0, 0 },
            {10, 9, 8, 8, 7, 6, 6, 3, 0, 0, 0, 0 },
            {11,10, 8, 8, 7, 7, 6, 4, 0, 0, 0, 0 },
            {11,10, 9, 8, 7, 7, 6, 5, 0, 0, 0, 0 }, // 40
            {11,10, 9, 8, 7, 7, 6, 6, 1, 0, 0, 0 },
            {11,10, 9, 8, 8, 7, 6, 6, 2, 0, 0, 0 },
            {11,10, 9, 8, 8, 7, 6, 6, 3, 0, 0, 0 },
            {11,10, 9, 8, 8, 7, 7, 6, 4, 0, 0, 0 },
            {11,10, 9, 9, 8, 7, 7, 6, 5, 0, 0, 0 },
            {12,11, 9, 9, 8, 7, 7, 6, 6, 1, 0, 0 },
            {12,11, 9, 9, 8, 8, 7, 6, 6, 2, 0, 0 },
            {12,11,10, 9, 8, 8, 7, 7, 6, 3, 0, 0 },
            {12,11,10, 9, 8, 8, 7, 7, 6, 4, 0, 0 },
            {12,11,10, 9, 9, 8, 7, 7, 6, 5, 0, 0 }, // 50
            {12,11,10, 9, 9, 8, 7, 7, 6, 6, 1, 0 },
            {12,11,10,10, 9, 8, 8, 7, 6, 6, 2, 0 },
            {13,12,10,10, 9, 8, 8, 7, 6, 6, 3, 0 },
            {13,12,10,10, 9, 8, 8, 7, 7, 6, 4, 0 },
            {13,12,11,10, 9, 8, 8, 7, 7, 6, 5, 0 },
            {13,12,11,10, 9, 9, 8, 8, 7, 6, 6, 1 }, // 56 -- end of mortal levels
            {13,12,11,10,10, 9, 8, 8, 7, 6, 6, 2 },
            {13,12,11,10,10, 9, 8, 8, 7, 6, 6, 3 },
            {13,12,11,11,10, 9, 8, 8, 7, 7, 6, 4 },
            {14,13,11,11,10, 9, 9, 8, 7, 7, 6, 5 }, // 60
            {14,13,11,11,10, 9, 9, 8, 7, 7, 6, 6 },
            {14,13,12,11,10, 9, 9, 8, 8, 7, 6, 6 },
            {14,13,12,11,10,10, 9, 8, 8, 7, 7, 6 },
            {14,13,12,11,10,10, 9, 8, 8, 7, 7, 6 },
            {16,16,16,16,16,16, 9, 8, 8, 7, 7, 6 }, // 65
        };

        /// <summary>
        /// Utter the magical-sounding words for an spell.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        static void SaySpell( CharData ch, Spell spell )
        {
            string buf = String.Empty;

            SyllableType[] sylTable = 
            {
                new SyllableType( " ",      " "         ),
                new SyllableType( "ar",     "abra"      ),
                new SyllableType( "au",     "kada"      ),
                new SyllableType( "bless",  "fido"      ),
                new SyllableType( "blind",  "nose"      ),
                new SyllableType( "bur",    "mosa"      ),
                new SyllableType( "cu",     "judi"      ),
                new SyllableType( "de",     "oculo"     ),
                new SyllableType( "en",     "unso"      ),
                new SyllableType( "light",  "dies"      ),
                new SyllableType( "lo",     "hi"        ),
                new SyllableType( "mor",    "zak"       ),
                new SyllableType( "move",   "sido"      ),
                new SyllableType( "ness",   "lacri"     ),
                new SyllableType( "ning",   "illa"      ),
                new SyllableType( "per",    "duda"      ),
                new SyllableType( "ra",     "gru"       ),
                new SyllableType( "re",     "candus"    ),
                new SyllableType( "son",    "sabru"     ),
                new SyllableType( "tect",   "infra"     ),
                new SyllableType( "tri",    "cula"      ),
                new SyllableType( "ven",    "nofo"      ),
                new SyllableType( "a", "a" ), new SyllableType( "b", "b" ), new SyllableType( "c", "q" ), new SyllableType( "d", "e" ),
                new SyllableType( "e", "z" ), new SyllableType( "f", "y" ), new SyllableType( "g", "o" ), new SyllableType( "h", "p" ),
                new SyllableType( "i", "u" ), new SyllableType( "j", "y" ), new SyllableType( "k", "t" ), new SyllableType( "l", "r" ),
                new SyllableType( "m", "w" ), new SyllableType( "n", "i" ), new SyllableType( "o", "a" ), new SyllableType( "p", "s" ),
                new SyllableType( "q", "d" ), new SyllableType( "r", "f" ), new SyllableType( "s", "g" ), new SyllableType( "t", "h" ),
                new SyllableType( "u", "j" ), new SyllableType( "v", "z" ), new SyllableType( "w", "x" ), new SyllableType( "x", "n" ),
                new SyllableType( "y", "l" ), new SyllableType( "z", "k" ),
                new SyllableType( String.Empty, String.Empty )
            };

            // TODO: FIXME: Fix the replacement algorithm.
            //for( pName = 0; pName < buf.Length; pName += length )
            //{
            //    int iSyl;
            //    for( iSyl = 0; ( length = ( syl_table[ iSyl ]._old.Length ) ) != 0; iSyl++ )
            //    {
            //        if( !MUDString.IsPrefixOf( syl_table[ iSyl ]._old, pName ) )
            //        {
            //            buf += syl_table[ iSyl ]._cnew;
            //            break;
            //        }
            //    }

            //    if( length == 0 )
            //        length = 1;
            //}

            string buf2 = "$n&n utters the words, '" + buf + "'.";
            buf = "$n&n utters the words, '" + spell.Name + "'.";

            foreach( CharData roomChar in ch._inRoom.People )
            {
                if( roomChar._flyLevel != ch._flyLevel )
                    continue;
                if( roomChar != ch && ( ( roomChar._charClass == ch._charClass ) || ch.IsImmortal() || ch.IsAffected( Affect.AFFECT_COMP_LANG ) ) )
                {
                    SocketConnection.Act( buf, ch, null, roomChar, SocketConnection.MessageTarget.victim );
                }
                else if( roomChar != ch )
                {
                    SocketConnection.Act( buf2, ch, null, roomChar, SocketConnection.MessageTarget.victim );
                }
            }

            return;
        }

        /// <summary>
        /// Compute a saving throw.  Negative apply's make saving throw better.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="victim"></param>
        /// <param name="damType"></param>
        /// <returns></returns>
        public static bool SpellSavingThrow( int level, CharData victim, AttackType.DamageType damType )
        {
            int ibase = 50;

            if( victim == null )
            {
                Log.Error("SpellSavingThrow called without CharData argument for victim.", 0);
                return false;
            }

            if (victim.IsAffected(Affect.AFFECT_DENY_WATER) && damType == AttackType.DamageType.water)
                return true;
            if (victim.IsAffected(Affect.AFFECT_DENY_FIRE) && damType == AttackType.DamageType.fire)
                return true;
            if (victim.IsAffected(Affect.AFFECT_DENY_EARTH) && damType == AttackType.DamageType.earth)
                return true;
            if (victim.IsAffected(Affect.AFFECT_DENY_AIR) && damType == AttackType.DamageType.wind)
                return true;

            // Keep in mind that *negative* saving throw improves the chance.
            // positive saving throw is a bad thing
            /* Thus, we want a - save to increase the save chance, not decrease
            * it.  So, we subtract the saving throw.
            */
            int save = ibase + ( victim._level - level - victim._savingThrows[ 4 ] ) * 2;

            if( victim.IsNPC() && victim._level > 55 )
                ibase += 20;

            // We aren't too harsh on our save penalties because the victim is already
            // automatically taking augmented damage.
            switch( victim.CheckRIS( damType ) )
            {
                case Race.ResistanceType.resistant:
                    save += 12;
                    break;
                case Race.ResistanceType.immune:
                    return true;
                case Race.ResistanceType.susceptible:
                    save -= 12;
                    break;
                case Race.ResistanceType.vulnerable:
                    save -= 25;
                    break;
            }

            /* Note that protection spells aren't quite as good as a natural resistance
            * ( +10% save -25% damage as opposed to +12% save -33% damage), but they
            * are cumulative, so a natural resistance and a protection spell will give
            * +22% save and -50% damage overall.
            */

            // TODO: This is duplicated in SavesBreath. Don't do that.
            if (damType == AttackType.DamageType.fire && victim.IsAffected(Affect.AFFECT_PROTECT_FIRE))
                save += 10;
            else if (damType == AttackType.DamageType.cold && victim.IsAffected(Affect.AFFECT_PROTECT_COLD))
                save += 10;
            else if (damType == AttackType.DamageType.gas && victim.IsAffected(Affect.AFFECT_PROTECT_GAS))
                save += 10;
            else if (damType == AttackType.DamageType.acid && victim.IsAffected(Affect.AFFECT_PROTECT_ACID))
                save += 10;
            else if (damType == AttackType.DamageType.electricity && victim.IsAffected(Affect.AFFECT_PROTECT_LIGHTNING))
                save += 10;

            save = Macros.Range( 5, save, 95 );
            if( MUDMath.NumberPercent() < save )
                return true;
            return false;
        }

        /// <summary>
        /// Compute a saving throw versus breath weapon (saving_throw[3]).
        /// Negative apply's make saving throw better.
        ///
        /// This is basically identical to Magic.SavesSpell, except that it uses
        /// a different saving throw as its base.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="victim"></param>
        /// <param name="damType"></param>
        /// <returns></returns>
        public static bool SavesBreath( int level, CharData victim, AttackType.DamageType damType )
        {
            int ibase = 50;

            string lfbuf = String.Format( "Magic.SavesBreath: dt: {0}, level: {1}", damType, level );
            Log.Trace( lfbuf );

            if( !victim )
            {
                Log.Error( "Magic.SavesBreath called without CharData argument for victim.", 0 );
                return false;
            }

            int save = ibase + ( victim._level - level - victim._savingThrows[ 3 ] ) * 2;

            switch( victim.CheckRIS( damType ) )
            {
                case Race.ResistanceType.resistant:
                    save += 12;
                    break;
                case Race.ResistanceType.immune:
                    return true;
                case Race.ResistanceType.susceptible:
                    save -= 12;
                    break;
                case Race.ResistanceType.vulnerable:
                    save -= 25;
                    break;
            }

            /* Note that protection spells aren't quite as good as a natural resistance
            * ( +10% save -25% damage as opposed to +12% save -33% damage), but they
            * are cumulative, so a natural resistance and a protection spell will give
            * +22% save and -50% damage overall.
            */

            // TODO: This is duplicated in SpellSavingThrow. Don't do that.
            if (damType == AttackType.DamageType.fire && victim.IsAffected(Affect.AFFECT_PROTECT_FIRE))
                save += 10;
            else if (damType == AttackType.DamageType.cold && victim.IsAffected(Affect.AFFECT_PROTECT_COLD))
                save += 10;
            else if (damType == AttackType.DamageType.gas && victim.IsAffected(Affect.AFFECT_PROTECT_GAS))
                save += 10;
            else if (damType == AttackType.DamageType.acid && victim.IsAffected(Affect.AFFECT_PROTECT_ACID))
                save += 10;
            else if (damType == AttackType.DamageType.electricity && victim.IsAffected(Affect.AFFECT_PROTECT_LIGHTNING))
                save += 10;

            save = Macros.Range( 5, save, 95 );
            if( MUDMath.NumberPercent() < save )
                return true;
            else
                return false;
        }

        /// <summary>
        /// Save against illusionist spells, intelligence and level-based.  Base save 50% with a
        /// bonus/penalty of 2% per level of difference between spell power (typically caster level
        /// but capped on most spells) and victim level.  Also has a 1% bonus/penalty per 5 int
        /// points of difference between caster and victim.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="victim"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        static public bool Disbelieve( int level, CharData victim, CharData ch )
        {
            int ibase = 50;

            if( !victim )
            {
                Log.Error( "Magic.Disbelieve called without CharData argument for victim.", 0 );
                return false;
            }

            // Level has an effect on illusions.
            int save = ibase + ( victim._level - level ) * 2;

            // Will cause a 100 int player to get a 5% bonus against a 50 int mob.
            save += ( victim.GetCurrInt() / 5 );
            save -= ( ch.GetCurrInt() / 5 );

            // Figure in spell saving throw adjustment.
            save -= victim._savingThrows[4];

            // Even though we already figured in intelligence, also take into account mental
            // damage type resistance and susceptibility.
            switch (victim.CheckRIS(AttackType.DamageType.mental))
            {
                case Race.ResistanceType.resistant:
                    save += 10;
                    break;
                case Race.ResistanceType.immune:
                    return true;
                case Race.ResistanceType.susceptible:
                    save -= 10;
                    break;
                case Race.ResistanceType.vulnerable:
                    save -= 20;
                    break;
            }

            save = Macros.Range( 5, save, 95 );
            return MUDMath.NumberPercent() < save;
        }

        /// <summary>
        /// Checks whether someone is a valid target for an area-effect spell.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        static bool IsValidAreaTarget( CharData ch, CharData target )
        {
            if (target.IsSameGroup(ch) || ch == target)
            {
                return false;
            }
            if (target._flyLevel != ch._flyLevel)
            {
                return false;
            }
            return true;
        }

        static CharData FindFirstAreaTarget( CharData ch, int numTargets )
        {
            int numInRoom = 0;
            int temp = 0;
            int firstTarget;

            foreach( CharData targetChar in ch._inRoom.People )
            {
                if( IsValidAreaTarget( ch, targetChar ) )
                    numInRoom++;
            }
            if( numTargets < numInRoom )
                firstTarget = MUDMath.Dice( 1, numInRoom - numTargets );
            else
                firstTarget = MUDMath.Dice( 1, numInRoom );
            foreach( CharData targetChar in ch._inRoom.People )
            {
                if( IsValidAreaTarget( ch, targetChar ) )
                {
                    temp++;
                    if (temp == firstTarget)
                    {
                        return targetChar;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Inflict the damage from an area-effect spell.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="numTargets"></param>
        /// <param name="damage"></param>
        /// <param name="damtype"></param>
        public static void AreaSpellDamage( CharData ch, Spell spell, int numTargets, int damage, AttackType.DamageType damtype )
        {
            int count = 0;

            // TODO: Start with either a random or a targeted character instead of picking the first.
            foreach( CharData targetChar in ch._inRoom.People )
            {
                if (++count > numTargets)
                    break;

                if( !ch.IsSameGroup( targetChar ) && ch != targetChar && ch._flyLevel == targetChar._flyLevel )
                {
                    int tmpDam = damage;
                    /* handle special damage types (chain lightning, quake, etc) here */
                    /* by modifying tmpDam, or setting stuff on chars */
                    if (tmpDam > 0)
                    {
                        Combat.InflictSpellDamage(ch, targetChar, damage, spell, damtype);
                    }
                } //end if
            }
        }

        /// <summary>
        /// Casts a spell using a magical object.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="victim"></param>
        /// <param name="obj"></param>
        public static void ObjectCastSpell( CharData ch, Spell spell, int level, CharData victim, Object obj )
        {
            Target target;

            if( spell == null || obj == null )
            {
                return;
            }

            switch( spell.ValidTargets )
            {
                default:
                    Log.Error( "ObjectCastSpell: bad TargetType for spell {0}.", spell.Name );
                    return;

                case TargetType.none:
                    target = null;
                    break;

                case TargetType.trap:
                    return;

                case TargetType.singleCharacterOffensive:
                    if( !victim )
                        victim = ch._fighting;
                    if( !victim )
                    {
                        ch.SendText( "You can't do that.\r\n" );
                        return;
                    }

                    if( Combat.IsSafe( ch, victim ) )
                        return;
                    // Combat.IsSafe could wipe out victim, as it calls procs if a boss
                    // check and see that victim is still valid
                    if(  !victim )
                        return;

                    Crime.CheckAttemptedMurder( ch, victim );

                    target = new Target( victim );
                    break;

                case TargetType.singleCharacterDefensive:
                    if( !victim )
                        victim = ch;
                    target = new Target( victim );
                    break;

                case TargetType.self:
                    target = new Target( ch );
                    break;

                case TargetType.objectInInventory:
                    if( !obj )
                    {
                        ch.SendText( "You can't do that.\r\n" );
                        return;
                    }
                    target = new Target( obj );
                    break;

                case TargetType.objectInRoom:
                    if( !obj )
                    {
                        ch.SendText( "You can't do that.\r\n" );
                        return;
                    }
                    target = new Target( obj );
                    break;

                case TargetType.objectCorpse:
                    target = new Target( obj );
                    break;
            }

            spell.Invoke(ch, level, target);

            if( spell.ValidTargets == TargetType.singleCharacterOffensive
                    && victim._master != ch && ch != victim )
            {
                foreach( CharData vch in ch._inRoom.People )
                {
                    if( victim == vch && !victim._fighting )
                    {
                        victim.AttackCharacter( ch );
                        break;
                    }
                }
            }

            return;
        }

        /// <summary>
        /// This is called by Command.Memorize and Commandpray.  This was originally
        /// attached to each function, but it's silly to have two large blocks
        /// of the same code
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="argument"></param>
        /// <param name="pray"></param>
        public static void Memorize( CharData ch, string argument, bool pray )
        {
            string text;
            Dictionary<String, Int32> memmed = new Dictionary<String, Int32>();
            int[] circle = new int[ Limits.MAX_CIRCLE ];
            int[] circfree = new int[ Limits.MAX_CIRCLE ];

            // with an argument they want to start memorizing a new spell.
            if( !String.IsNullOrEmpty(argument) )
            {
                // Must be in the proper position
                if( ch._position != Position.resting )
                {
                    if( pray )
                        ch.SendText( "You can only pray for spells while resting.\r\n" );
                    else
                        ch.SendText( "You can memorize spells only when resting.\r\n" );
                    return;
                }

                // Find the spell they want
                Spell spell = StringLookup.SpellLookup( argument );
                if( spell == null )
                {
                    ch.SendText( "Never heard of that spell...\r\n" );
                    return;
                }

                // Check to see that they can memorize another spell
                // Immortals have no limits
                if( !ch.IsImmortal() )
                {
                    if( ( (PC)ch ).Hunger <= 0 || ( (PC)ch ).Thirst <= 0 )
                    {
                        ch.SendText( "You can't seem to concentrate on anything but your appetite.\r\n" );
                    }

                    if( !ch.HasSpell( argument ) )
                    {
                        ch.SendText( "That spell is beyond you.\r\n" );
                        return;
                    }

                    if (!pray && ((PC)ch).SpellAptitude[spell.Name] < 1)
                    {
                        ch.SendText( "You have not yet learned that spell.  Find a place to scribe it.\r\n" );
                        return;
                    }

                    int lvltotal = 0;
                    foreach( MemorizeData mem in ((PC)ch).Memorized )
                    {
                        if (mem.Circle == spell.SpellCircle[(int)ch._charClass.ClassNumber])
                            lvltotal += 1;
                    }
                    int numMemmable = 0;
                    if (ch._charClass.MemType == CharClass.MemorizationType.Lesser)
                    {
                        numMemmable = LesserMemchart[(ch._level - 1), (spell.SpellCircle[(int)ch._charClass.ClassNumber] - 1)];
                    }
                    else
                    {
                        numMemmable = Memchart[(ch._level - 1), (spell.SpellCircle[(int)ch._charClass.ClassNumber] - 1)];
                    }
                    if (lvltotal >= numMemmable )
                    {
                        if( pray )
                            ch.SendText( "You can pray for no more spells of that level.\r\n" );
                        else
                            ch.SendText( "You can memorize no more spells of that circle.\r\n" );
                        return;
                    }
                }

                // If we know what they want and they can have it, let's create it.
                MemorizeData memm = CreateMemorizeData( ch, spell );
                if( !memm )
                {
                    Log.Error( "Unable to create memorization (spell {0})", spell );
                    return;
                }

                // If they're not already memorizing, they are now.
                ch.SetActBit(PC.PLAYER_MEMORIZING );

                if( pray )
                    text = String.Format( "You start praying for {0} which will take about {1} seconds.\r\n",
                              spell.Name, ( memm.Memtime / Event.TICK_PER_SECOND ) );
                else
                    text = String.Format( "You start memorizing {0} which will take about {1} seconds.\r\n",
                              spell.Name, ( memm.Memtime / Event.TICK_PER_SECOND ) );
                ch.SendText( text );

                return;
            }
            bool found = false;
            // If they didn't give us an argument, that means that they must
            // want to either see their spell list or continue memorizing
            // Either way we show their spell list.
            // make sure they have some mem data first...
            int count;
            if( ( (PC)ch ).Memorized.Count > 0 )
            {
                // Figure out what spells they have memorized
                foreach( MemorizeData mem in ((PC)ch).Memorized )
                {
                    if( mem.Memmed )
                    {
                        if (memmed.ContainsKey(mem.Name))
                        {
                            memmed[mem.Name] = memmed[mem.Name] + 1;
                        }
                        else
                        {
                            memmed[mem.Name] = 1;
                        }
                    }
                }

                // Show memorized spells
                if( pray )
                    ch.SendText( "You have prayed the following spells:\r\n" );
                else
                    ch.SendText( "You have memorized the following spells:\r\n" );
                int circleIndex;
                for( circleIndex = 12; circleIndex > 0; circleIndex-- )
                {
                    foreach (KeyValuePair<String, Spell> kvp in Spell.SpellList)
                    {
                        if (kvp.Value.SpellCircle[(int)ch._charClass.ClassNumber] != circleIndex)
                            continue;
                        if( memmed.ContainsKey(kvp.Key) && memmed[ kvp.Key ] > 0 && kvp.Value.Name != null )
                        {
                            text = String.Format( "({0,2}{1} circle)  {2} - {3}\r\n",
                                                 kvp.Value.SpellCircle[(int)ch._charClass.ClassNumber],
                                                 MUDString.NumberSuffix(kvp.Value.SpellCircle[(int)ch._charClass.ClassNumber]),
                                                 memmed[ kvp.Key ],
                                                 kvp.Value.Name );
                            ch.SendText( text );
                        }
                    }
                } //end for(circle)

                // Figure out what spells they are working on
                if( pray )
                    ch.SendText( "You are praying for the following spells:\r\n" );
                else
                    ch.SendText( "You are memorizing the following spells:\r\n" );
                int totalMem = 0;
                // TODO: Make sure we cycle through this in the right order.
                // [ might need to do _memorized.Reverse() ]
                foreach( MemorizeData mem in ((PC)ch).Memorized )
                {
                    if( mem.Name == null )
                        break;
                    if( mem.Memmed )
                        continue;
                    found = true;
                    text = String.Format( "    {0} seconds:  ({1}{2}) {3}\r\n",
                                         ( ( totalMem + mem.Memtime ) / Event.TICK_PER_SECOND ),
                                         Spell.SpellList[mem.Name].SpellCircle[(int)ch._charClass.ClassNumber],
                                         MUDString.NumberSuffix(Spell.SpellList[mem.Name].SpellCircle[(int)ch._charClass.ClassNumber]),
                                         mem.Name );
                    ch.SendText( text );
                    totalMem += mem.Memtime;
                }
            }

            //
            // Tell them what they still have slots open for...
            for( count = 0; count < Limits.MAX_CIRCLE; ++count )
                circle[ count ] = 0;
            foreach( MemorizeData mem in ((PC)ch).Memorized )
            {
                circle[ ( mem.Circle - 1 ) ] += 1;
            }
            bool left = false;
            for( count = 0; count < Limits.MAX_CIRCLE; ++count )
            {
                int numMemmable = 0;
                if (ch._charClass.MemType == CharClass.MemorizationType.Lesser)
                {
                    numMemmable = LesserMemchart[(ch._level - 1), count];
                }
                else
                {
                    numMemmable = Memchart[(ch._level - 1), count];
                }
                circfree[ count ] = numMemmable - circle[ count ];
                if( circfree[ count ] > 0 )
                    left = true;
            }
            if( !left )
            {
                if( pray )
                    ch.SendText( "\r\nYou can pray for no more spells.\r\n" );
                else
                    ch.SendText( "\r\nYou can memorize no more spells.\r\n" );
            }
            else
            {
                if( pray )
                    text = String.Format( "\r\nYou can pray for" );
                else
                    text = String.Format( "\r\nYou can memorize" );
                for( count = 0; count < Limits.MAX_CIRCLE; ++count )
                {
                    if( circfree[ count ] > 0 )
                    {
                        string buf2 = String.Format( " {0}x{1}{2}", circfree[ count ], ( count + 1 ), MUDString.NumberSuffix( count + 1 ) );
                        text += buf2;
                    }
                }
                text += " level spells.\r\n";
                ch.SendText( text );
            }

            // If they aren't memming and they should be, start 'em up.
            if( found && !ch.HasActBit(PC.PLAYER_MEMORIZING )
                && ch._position == Position.resting )
            {
                ch.SetActBit(PC.PLAYER_MEMORIZING );
                if( ( (PC)ch ).Hunger > 0
                    && ( (PC)ch ).Thirst > 0 )
                {
                    if( pray )
                        ch.SendText( "You continue your prayers.\r\n" );
                    else
                        ch.SendText( "You continue your studies.\r\n" );
                }
            }
            return;
        }

        // Should hard code level checks to see if it is okay to mem a certain
        // spell.
        static MemorizeData CreateMemorizeData( CharData ch, Spell spell )
        {
            if( ch.IsNPC() )
                return null;

            MemorizeData mem = new MemorizeData();

            mem.Name = spell.Name;
            mem.Memtime = CalculateMemorizationTime( ch, spell );
            mem.FullMemtime = CalculateMemorizationTime( ch, spell );
            mem.Circle = spell.SpellCircle[(int)ch._charClass.ClassNumber];
            mem.Memmed = false;

            // TODO: Make sure that this is added in the right order.
            ( (PC)ch ).Memorized.Add( mem );

            return mem;
        }

        static int CalculateMemorizationTime( CharData ch, Spell spell )
        {
            int attribute;

            if( ch.IsClass(CharClass.Names.cleric) || ch.IsClass(CharClass.Names.druid)
                    || ch.IsClass(CharClass.Names.paladin) || ch.IsClass(CharClass.Names.antipaladin ))
                attribute = ch.GetCurrWis();
            else if( ch.IsClass(CharClass.Names.shaman) )
                attribute = ( ch.GetCurrInt() + ch.GetCurrWis() ) / 2;
            else
                attribute = ch.GetCurrInt();

            int memtime = 220 - attribute - ( ch._level * 3 )
                          + (spell.SpellCircle[(int)ch._charClass.ClassNumber] * 8);
            if( memtime < 4 )
                memtime = 4;
            return memtime;
        }

        /// <summary>
        /// Update memorization data for any who are memorizing spells.
        /// </summary>
        public static void MemorizeUpdate()
        {
            MemorizeData chkspl = null;
            CharData ch;

            foreach( CharData it in Database.CharList )
            {
                ch = it;

                if( ch.IsNPC() )
                    continue;

                if( !( ch.HasActBit(PC.PLAYER_MEMORIZING ) ) )
                    continue;

                if( ( (PC)ch ).Hunger <= 0 )
                {
                    ch.SendText( "&+rYou are too hungry to concentrate.&n\r\n" );
                    continue;
                }
                if( ( (PC)ch ).Thirst <= 0 )
                {
                    ch.SendText( "&+rYou are too thirsty to concentrate.&n\r\n" );
                    continue;
                }

                if( ( (PC)ch ).Memorized.Count == 0 )
                {
                    Log.Error( "Memorizing character with no mem_data", 0 );
                    ch.RemoveActBit(PC.PLAYER_MEMORIZING);
                }

                // Find the oldest unmemmed piece of spell data
                // TODO: Make sure this happens in the right order.
                foreach( MemorizeData memor in ((PC)ch).Memorized )
                {
                    if( !memor.Memmed )
                    {
                        chkspl = memor;
                        break;
                    }
                }

                if( !chkspl )
                {
                    ch.RemoveActBit(PC.PLAYER_MEDITATING);
                    ch.RemoveActBit(PC.PLAYER_MEMORIZING);
                    ch.SendText( "Your studies are complete..." );
                    continue;
                }
                if( chkspl.Memtime < 1 )
                {
                    chkspl.Memmed = true;
                    string buf = String.Format( "You have finished memorizing {0}.\r\n",
                                                chkspl.Name );
                    ch.SendText( buf );
                }
                else
                {
                    if (ch.HasActBit(PC.PLAYER_MEDITATING))
                    {
                        if ((chkspl.Memtime >= (chkspl.FullMemtime / 2)) && ((chkspl.Memtime - Event.TICK_MEMORIZE)
                                 < (chkspl.FullMemtime / 2)))
                        {
                            if (ch.CheckSkill("meditate", PracticeType.difficult))
                            {
                                chkspl.Memmed = true;
                                string buf = String.Format("You have finished memorizing {0}.\r\n", chkspl.Name);
                                ch.SendText(buf);
                            }
                        }
                    }
                    chkspl.Memtime -= Event.TICK_MEMORIZE;
                }

                bool done = true;
                foreach( MemorizeData memor in ((PC)ch).Memorized )
                {
                    if( !memor.Memmed )
                    {
                        done = false;
                        break;
                    }
                }

                if( ( done ) && ( ch.HasActBit(PC.PLAYER_MEMORIZING ) ) )
                {
                    ch.RemoveActBit(PC.PLAYER_MEDITATING);
                    ch.RemoveActBit(PC.PLAYER_MEMORIZING);
                    ch.SendText( "Your studies are complete.\r\n" );
                    SocketConnection.Act( "$n&n is finished memorizing.", ch, null, null, SocketConnection.MessageTarget.room );
                }
            }

            return;
        }

        /// <summary>
        /// Clears spell memorization list.  Used for both death and for 'forget all' command.
        /// </summary>
        /// <param name="ch"></param>
        public static void ForgetAllSpells( CharData ch )
        {
            if( ch.IsNPC() )
                return;

            if( ( (PC)ch ).Memorized.Count == 0 )
                return;

            ( (PC)ch ).Memorized.Clear();

            return;
        }

        /// <summary>
        /// TODO: Put Bard song code here.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="song"></param>
        /// <param name="target"></param>
        public static void SongVerse(CharData ch, Song song, Target target)
        {
        }

        /// <summary>
        /// When a spell event terminates, we need something to happen.
        /// 
        /// By this point we should have terminated the spell/song event data
        /// and should only need the info about the character and the spell
        /// and the argument(s).
        /// 
        /// Passing of the correct function parameters should be handled by the
        /// event system.
        /// </summary>
        /// <param name="ch">The caster</param>
        /// <param name="spell">The spell number</param>
        /// <param name="target">The _targetType</param>
        public static void FinishSpell( CharData ch, Spell spell, Target target )
        {
            Object obj;
            int chance = 0;
            bool found = false;

            string lbuf = String.Format("Magic.FinishSpell: {0} by {1}", spell.Name, ch._name);
            Log.Trace( lbuf );

            for( int i = Database.CastList.Count - 1; i >= 0; i--)
            {
                if (Database.CastList[i].Who && Database.CastList[i].Who == ch)
                {
                    Database.CastList.RemoveAt( i );
                }
            }

            // If they're not casting at the end of the song or spell
            // they certainly can't finish it.
            if( ch.IsAffected( Affect.AFFECT_CASTING ) )
                ch.RemoveAffect( Affect.AFFECT_CASTING );
            else
            {
                return;
            }

            if( !ch.CheckConcentration(spell) )
                return;

            if( ( ch.IsAffected( Affect.AFFECT_MUTE ) || ch.HasInnate( Race.RACE_MUTE ) )
                    && !ch.IsClass( CharClass.Names.psionicist ) )
            {
                ch.SendText( "Your lips move but no sound comes out.\r\n" );
                return;
            }

            // Make sure the room is still castable.
            if( !ch._inRoom.CheckCastable( ch, false, false ) )
                return;

            if( ch._inRoom.CheckStarshell( ch ) )
                return;

            MemorizeData memorized = null;
            if (!ch.IsNPC() && !ch.IsImmortal() && !ch.IsClass(CharClass.Names.psionicist))
            {
                foreach( MemorizeData mem in ((PC)ch).Memorized )
                {
                    if( !mem.Memmed )
                        continue;
                    if( mem.Name == spell.Name )
                    {
                        found = true;
                        memorized = mem;
                        break;
                    }
                }

                if (!found && !ch.IsNPC() && !ch.IsClass(CharClass.Names.psionicist))
                {
                    ch.SendText( "You do not have that spell memorized!\r\n" );
                    if( spell.ValidTargets == TargetType.objectOrCharacter )
                        target = null;
                    else if( spell.ValidTargets == TargetType.none )
                        target = null;
                    return;
                }
            }

            if( ch.IsAffected( Affect.AFFECT_FEEBLEMIND ) )
            {
                ch.SendText( "You are just too stupid to cast that spell!\r\n" );
                SocketConnection.Act( "$n&n screws up $s face in concentration.", ch, null, null, SocketConnection.MessageTarget.room );
                SocketConnection.Act( "$n&n tries really, really hard to complete a spell, but fails.", ch, null, null, SocketConnection.MessageTarget.room );
                return;
            }

            // Locate targets.
            CharData victim = null;
            switch( spell.ValidTargets )
            {
                default:
                    Log.Trace( "FinishSpell: bad TargetType for spell {1}.", spell );
                    return;
                case TargetType.objectOrCharacter:
                    if( ch.IsAffected( Affect.AFFECT_BLIND ) )
                    {
                        ch.SendText( "You cannot see to cast that spell.\r\n" );
                        return;
                    }
                    break;
                case TargetType.none:
                    break;
                case TargetType.trap:
                    ch.SendText( "You cannot cast a trap!\r\n" );
                    return;
                case TargetType.singleCharacterOffensive:
                    victim = (CharData)target;

                    if( ch.IsAffected( Affect.AFFECT_BLIND ) )
                    {
                        //allow casting if in combat and no _targetType specified
                        if( !( ch._fighting && victim == ch._fighting ) )
                        {
                            ch.SendText( "You cannot see to cast that spell.\r\n" );
                            return;
                        }
                    }
                    if( !victim )
                    {
                        ch.SendText( "They aren't here.\r\n" );
                        return;
                    }
                    if( !victim._inRoom || victim._inRoom != ch._inRoom )
                    {
                        ch.SendText( "They are not here.\r\n" );
                        return;
                    }
                    if( Combat.IsSafe( ch, victim ) )
                        return;
                    // Command.is_safe could wipe out victim, as it calls procs if a boss
                    // check and see that victim is still valid
                    if( !victim )
                        return;

                    Crime.CheckAttemptedMurder( ch, victim );

                    /* Check for globes.  This will stop any spells of type TargetType.singleCharacterOffensive
                    * but area effect spells with type TargetType.none will get through, since we
                    * don't know whether they will be offensive or not.  The only thing we can
                    * really do is add this same thing in the Command.SpellDamage function to prevent
                    * those from getting through.  However, we must treat cases of things like
                    * an area effect sleep spell as a special case within the SpellWhatever
                    * function in Spells.cs.  However, by the nature of the spell, anything
                    * that is not either offensive and not direct damage, it should get through
                    * just so that these spells have some weaknesses for the strategic to get
                    * around.
                    */
                    /*
                    *  TODO: Find out why this globe code was commented out and either uncomment or delete.
                    if( CharData.IsAffected( victim, Affect.AFFECT_MAJOR_GLOBE ) && Spell.Table[spell].spell_circle[ch.cclass] <= 6 )
                    {
                    Descriptor._actFlags( "&+RThe globe around $N&n's body bears the brunt of your assault!&n", ch, null, victim, Descriptor.MessageTarget.character );
                    Descriptor._actFlags( "&+RYour globe deflects $n&+R's attack!&n", ch, null, victim, Descriptor.MessageTarget.victim );
                    Descriptor._actFlags( "&+R$N&+R's globe deflects $n&+R's attack!&n", ch, null, victim, Descriptor.MessageTarget.room );
                    return;
                    }
                    if( CharData.IsAffected( victim, Affect.AFFECT_GREATER_SPIRIT_WARD ) && Spell.Table[spell].spell_circle[ch.cclass] <= 5 )
                    {
                    Descriptor._actFlags( "&+WThe aura around $N&n's body bears the brunt of your assault!&n", ch, null, victim, Descriptor.MessageTarget.character );
                    Descriptor._actFlags( "&+WYour globe absorbs $n&+W's attack!&n", ch, null, victim, Descriptor.MessageTarget.victim );
                    Descriptor._actFlags( "&+W$N&+W's aura absorbs $n&+W's attack!&n", ch, null, victim, Descriptor.MessageTarget.room );
                    return;
                    }
                    if( CharData.IsAffected( victim, Affect.AFFECT_MINOR_GLOBE ) && Spell.Table[spell].spell_circle[ch.cclass] <= 4 )
                    {
                    Descriptor._actFlags( "&+RThe globe around $N&n's body bears the brunt of your assault!&n", ch, null, victim, Descriptor.MessageTarget.character );
                    Descriptor._actFlags( "&+RYour globe deflects $n&+R's attack!&n", ch, null, victim, Descriptor.MessageTarget.victim );
                    Descriptor._actFlags( "&+R$N&+R's globe deflects $n&+R's attack!&n", ch, null, victim, Descriptor.MessageTarget.room );
                    return;
                    }
                    if( CharData.IsAffected( victim, Affect.AFFECT_SPIRIT_WARD ) && Spell.Table[spell].spell_circle[ch.cclass] <= 3 )
                    {
                    Descriptor._actFlags( "&+WThe aura around $N&n's body bears the brunt of your assault!&n", ch, null, victim, Descriptor.MessageTarget.character );
                    Descriptor._actFlags( "&+WYour globe absorbs $n&+W's attack!&n", ch, null, victim, Descriptor.MessageTarget.victim );
                    Descriptor._actFlags( "&+W$N&+W's aura absorbs $n&+W's attack!&n", ch, null, victim, Descriptor.MessageTarget.room );
                    return;
                    }
                    */
                    break;
                case TargetType.singleCharacterWorld:
                    victim = (CharData)target;

                    if( ch.IsAffected( Affect.AFFECT_BLIND ) && victim != ch )
                    {
                        ch.SendText( "You cannot see to cast that spell.\r\n" );
                        return;
                    }
                    break;
                case TargetType.singleCharacterDefensive:
                    victim = (CharData)target;

                    if( ch.IsAffected( Affect.AFFECT_BLIND ) && victim != ch )
                    {
                        ch.SendText( "You cannot see to cast that spell.\r\n" );
                        return;
                    }
                    if( !victim || victim._inRoom != ch._inRoom )
                    {
                        ch.SendText( "They aren't here.\r\n" );
                        return;
                    }
                    break;
                case TargetType.self:
                    break;
                case TargetType.objectInInventory:
                    obj = (Object)target;

                    if( ch.IsAffected( Affect.AFFECT_BLIND ) )
                    {
                        ch.SendText( "You cannot see to cast that spell.\r\n" );
                        return;
                    }
                    if( !obj || obj.CarriedBy != ch )
                    {
                        ch.SendText( "You are not carrying that.\r\n" );
                        return;
                    }
                    break;
                case TargetType.objectInRoom:
                    obj = (Object)target;

                    if( ch.IsAffected( Affect.AFFECT_BLIND ) )
                    {
                        ch.SendText( "You cannot see to cast that spell.\r\n" );
                        return;
                    }
                    if( !obj || ( obj.CarriedBy != ch && obj.InRoom != ch._inRoom ) )
                    {
                        ch.SendText( "You do not see that here.\r\n" );
                        return;
                    }
                    break;
                case TargetType.objectCorpse:
                    break;
                case TargetType.singleCharacterRanged:
                    victim = (CharData)target;
                    if( ch.IsAffected( Affect.AFFECT_BLIND ) )
                    {
                        ch.SendText( "You cannot see to cast that spell.\r\n" );
                        return;
                    }
                    if( !victim
                            || victim._flyLevel != ch._flyLevel
                            || !CharData.CanSee( ch, victim ) )
                    {
                        ch.SendText( "Your prey has disappeared.\r\n" );
                        return;
                    }
                    //check that _targetType is still within the spell range
                    if( ch._inRoom == victim._inRoom )
                    {
                        break;
                    }
                    bool targetInRange = false;
                    int dir;
                    for( dir = 0; dir < Limits.MAX_DIRECTION; dir++ )
                    {
                        if( !ch._inRoom.ExitData[ dir ]
                                || ch._inRoom.ExitData[ dir ].HasFlag( Exit.ExitFlag.secret )
                                || ch._inRoom.ExitData[ dir ].HasFlag( Exit.ExitFlag.closed )
                                || ch._inRoom.ExitData[ dir ].HasFlag( Exit.ExitFlag.blocked )
                                || ch._inRoom.ExitData[dir].HasFlag(Exit.ExitFlag.walled))
                            continue;
                        if( ch._inRoom.ExitData[ dir ].TargetRoom == victim._inRoom )
                        {
                            targetInRange = true;
                            break;
                        }
                        // for fireball we check two rooms away
                        if( ch._inRoom.ExitData[ dir ].TargetRoom &&
                                ch._inRoom.ExitData[ dir ].TargetRoom.ExitData[ dir ]
                                && ch._inRoom.ExitData[ dir ].TargetRoom.ExitData[ dir ].TargetRoom == victim._inRoom )
                        {
                            targetInRange = true;
                            break;
                        }
                    }
                    if( !targetInRange )
                    {
                        ch.SendText( "They are no longer in range!\r\n" );
                        return;
                    }
                    break;
                }

            // No wait state - we already made them wait.
            ch.PracticeSpell( spell );

            if (ch.IsNPC())
            {
                chance = 85;
            }
            else if (ch.HasSpell(spell.Name))
            {
                chance = ((PC)ch).SpellAptitude[spell.Name];
            }

            if( !ch.IsImmortal() && ( MUDMath.NumberPercent() > chance ) )
            {
                ch.SendText( "You lost your concentration.\r\n" );
                SocketConnection.Act( "&+r$n&n&+r stops chanting abruptly.&n", ch, null, null, SocketConnection.MessageTarget.room );
            }
            else
            {
                // TODO: Figure out whether this should be re-enabled.
                //if( song )
                //{
                //    ch.SendText( "You complete a verse of the song...\r\n" );
                //    ch.GainExperience( 1 );
                //    SaySong( ch, spell );
                //}
                //else
                {
                    if (!ch.IsClass(CharClass.Names.psionicist))
                    {
                        ch.SendText( "You complete your spell...\r\n" );
                        if( MUDString.StringsNotEqual( spell.Name, "ventriloquate" ) )
                            SaySpell( ch, spell );
                    }
                    ch.GainExperience( 1 );
                }
                if( !ch.IsNPC() )
                {
                    string buf = String.Format( "Spell ({0}) being cast by {1}", spell.Name, ch._name );
                    Log.Trace( buf );
                }

                int level = Macros.Range(1, ch._level, Limits.LEVEL_HERO);
                spell.Invoke(ch, level, target);

                if( memorized && !ch.IsNPC() && !ch.IsImmortal() )
                {
                    memorized.Memmed = false;
                    memorized.Memtime = memorized.FullMemtime;
                }
            }

            if( ( spell.ValidTargets == TargetType.singleCharacterOffensive
                    || spell.ValidTargets == TargetType.singleCharacterRanged )
                    && victim && victim._master != ch && victim != ch && victim.IsAwake() )
            {
                if( ch._inRoom == victim._inRoom )
                {
                    if( !victim._fighting && CharData.CanSee( victim, ch ) )
                        victim.AttackCharacter( ch );
                }
                else
                {
                    // range spell presumably, since different rooms
                    Combat.StartGrudge( victim, ch, true );
                    foreach( CharData vch in ch._inRoom.People )
                    {
                        if( vch == victim )
                            continue;
                        if( vch._flyLevel != ch._flyLevel )
                            continue;
                        //protectors will be aggro'd
                        if (vch.HasActBit(MobTemplate.ACT_PROTECTOR) && (vch.GetRace() == victim.GetRace()))
                        {
                            Combat.StartGrudge( vch, ch, true );
                        }
                        // all aggro mobs will hunt down caster
                        if (vch.HasActBit(MobTemplate.ACT_AGGRESSIVE))
                            Combat.StartGrudge( vch, ch, true );
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Handles all spellcasting, whether it be willing, singing, or casting
        /// If they got here as a bard, they're using the SING command word,
        /// if they got here as a psionicist, they're using the WILL command word,
        /// and if they got here as anything else, they're using CAST.
        ///
        /// These are just cheesy details handled by CommandType.cs... we don't care.
        /// What we do care about is that we *know* it's safe to base all our
        /// messages/decisions on the character's class.
        ///
        /// This function is also *mob-safe*, meaning that mobs can cast spells
        /// too.  However, this is not the preferred method (as far as I can tell)
        ///
        /// Shaman totems are checked for in this function.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="argument"></param>
        public static void Cast( CharData ch, string argument )
        {
            // No casting while berzerked... Nor singing! Hah!
            if (ch.IsAffected(Affect.AFFECT_BERZERK))
            {
                ch.SendText( "Not while you're in a &+RBl&n&+ro&+Ro&n&+rd&+L Rage&n!\r\n" );
                return;
            }

            // No casting while thirsty... Nor singing! Hah!
            if (ch.IsAffected(Affect.AFFECT_THIRST) && ( ch.IsNPC() || ( (PC)ch ).Thirst < 10 ) )
            {
                ch.SendText( "&+BNo&+Ct w&+chi&+ble &+cyo&+Bu'&+cre &+Bso p&+carc&+Bhed&n!\r\n" );
                return;
            }

            String[] pieces = argument.Split( new Char[] {'\''}, StringSplitOptions.RemoveEmptyEntries);
            if (pieces.Length < 1)
            {
                ch.SendText("Spell names must always be in single quotes, such as:  cast 'magic missile' troll.\r\n");
                return;
            }
            if (pieces.Length > 1)
            {
                pieces[1] = pieces[1].Trim();
            }

            Spell spell;
            if (((spell = StringLookup.SpellLookup(pieces[0])) == null) || ((!ch.HasSpell(pieces[0])) && !ch.IsImmortal()))
            {
                ch.SendText( "You can't do that.\r\n" );
                return;
            }

            if( !CheckTotem( ch, spell ))
                return;

            if( !ch.CheckConcentration( spell ) )
                return;

            if (!ch.CheckMemorized(spell))
                return;

            if( ( !ch.CanSpeak() || ch.HasInnate(Race.RACE_MUTE)) && !ch.IsClass(CharClass.Names.psionicist))
            {
                ch.SendText( "Your lips move but no sound comes out.\r\n" );
                return;
            }

            if( !ch._inRoom.CheckCastable( ch, ch.IsClass( CharClass.Names.bard ), true) )
                return;

            if( ch._inRoom.CheckStarshell( ch ) )
                return;

            int manaUsed = 0;
            // TODO: Rather than hard-code psionicist as a mana class, let that be set in the class files.
            if (ch.IsClass(CharClass.Names.psionicist))
            {
                manaUsed = Macros.ManaCost(ch, spell);
            }
            else if (ch.IsClass(CharClass.Names.bard))
            {
                manaUsed = spell.MinimumMana;
            }

            // Locate targets.
            if( ch.IsNPC() )
            {
                ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, "Magic.Cast: Attempting to find _targetType for " + ch._shortDescription + "&n." );
            }

            if (pieces.Length > 1)
            {
                ProcessSpellTargets(ch, spell, pieces[1]);
            }
            else
            {
                ProcessSpellTargets(ch, spell, null);
            }
        }

        /// <summary>
        /// Called by the cast function to process a spell's targets and react accordingly.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="argument"></param>
        private static void ProcessSpellTargets(CharData ch, Spell spell, string argument)
        {
            Object obj = null;
            Target target = null;
            CharData victim = null;

            if (!ch.IsClass(CharClass.Names.bard))
                switch (spell.ValidTargets)
                {
                    default:
                        Log.Error("Magic.Cast: bad target type for spell {0}.  Type is: {1}", spell.Name, spell.ValidTargets.ToString());
                        return;
                    case TargetType.none:
                        target = new Target(argument);
                        break;
                    case TargetType.singleCharacterWorld:
                        victim = ch.GetCharWorld(argument);
                        if (victim == null)
                        {
                            ch.SendText("Cast the spell on whom?\r\n");
                            return;
                        }
                        target = new Target(victim);
                        break;
                    case TargetType.trap:
                        ch.SendText("You cannot cast a trap!\r\n");
                        return;
                    case TargetType.singleCharacterOffensive:
                        if (String.IsNullOrEmpty(argument))
                        {
                            victim = ch._fighting;
                            if (victim == null)
                            {
                                if (ch.IsClass(CharClass.Names.psionicist))
                                    ch.SendText("Will the spell upon whom?\r\n");
                                else
                                    ch.SendText("Cast the spell upon whom?\r\n");
                                return;
                            }
                        }
                        else
                        {
                            victim = ch.GetCharRoom(argument);
                            if (!victim)
                            {
                                ch.SendText("They aren't here.\r\n");
                                return;
                            }
                        }

                        if (ch.IsAffected( Affect.AFFECT_CHARM) && ch._master == victim)
                        {
                            ch.SendText("You can't do that to your master!.\r\n");
                            return;
                        }

                        if (Combat.IsSafe(ch, victim))
                            return;
                        // Combat.IsSafe could wipe out victim, as it calls procs if a boss
                        // check and see that victim is still valid
                        if (!victim)
                            return;

                        Crime.CheckAttemptedMurder(ch, victim);
                        target = new Target(victim);
                        ch.BreakInvis();
                        break;

                    case TargetType.singleCharacterDefensive:
                        if (String.IsNullOrEmpty(argument))
                        {
                            victim = ch;
                        }
                        else
                        {
                            victim = ch.GetCharRoom(argument);
                            if (victim == null)
                            {
                                ch.SendText("They aren't here.\r\n");
                                return;
                            }
                        }

                        target = new Target(victim);
                        break;

                    case TargetType.self:
                        if (!String.IsNullOrEmpty(argument) && !MUDString.NameContainedIn(argument, ch._name) &&
                                "me".Equals(argument, StringComparison.CurrentCultureIgnoreCase) &&
                                "self".Equals(argument, StringComparison.CurrentCultureIgnoreCase))
                        {
                            ch.SendText("You cannot cast this spell on another.\r\n");
                            return;
                        }

                        target = new Target(ch);
                        break;

                    case TargetType.objectInInventory:
                        if (String.IsNullOrEmpty(argument))
                        {
                            ch.SendText("What item should the spell be cast upon?\r\n");
                            return;
                        }

                        obj = ch.GetObjCarrying(argument);
                        if (obj == null)
                        {
                            ch.SendText("You are not carrying that.\r\n");
                            return;
                        }

                        target = new Target(obj);
                        break;

                    case TargetType.objectInRoom:
                        if (String.IsNullOrEmpty(argument))
                        {
                            ch.SendText("What should the spell be cast upon?\r\n");
                            return;
                        }

                        obj = ch.GetObjHere(argument);
                        if (obj == null)
                        {
                            ch.SendText("You do not see that here.\r\n");
                            return;
                        }

                        target = new Target(obj);
                        break;

                    case TargetType.objectCorpse:
                        target = new Target(argument);
                        break;
                    case TargetType.objectOrCharacter:
                        if (String.IsNullOrEmpty(argument))
                        {
                            if (ch._fighting != null)
                                victim = ch._fighting;
                            else
                            {
                                ch.SendText("Cast upon what?\r\n");
                                return;
                            }
                        }
                        else if (!(victim = ch.GetCharRoom(argument)))
                        {
                            obj = ch.GetObjHere(argument);
                        }

                        if (victim != null)
                        {
                            target = new Target(victim);
                        }
                        else if (obj != null)
                        {
                            target = new Target(obj);
                        }
                        else
                        {
                            ch.SendText("You do not see that here.\r\n");
                            return;
                        }

                        break;
                    case TargetType.singleCharacterRanged:
                        if (String.IsNullOrEmpty(argument))
                        {
                            victim = ch._fighting;
                            if (victim == null)
                            {
                                ch.SendText("Cast the spell on whom?\r\n");
                                return;
                            }
                        }
                        else
                        {
                            victim = ch.GetCharRoom(argument);
                            if (!victim)
                            {
                                ch.SendText("They aren't here.\r\n");
                                return;
                            }
                            // Ranged combat.
                            //
                            // TODO: FIXME: The next line does not successfully get the required argument.
                            //int dir = Movement.FindExit(ch, arg3);
                            //if (ch._level >= Limits.LEVEL_IMMORTAL)
                            //{
                            //    buf = String.Format("Looking for {0} to the {1}.\r\n", arg2, arg3);
                            //    ch.SendText(buf);
                            //}
                            //if (ch._inRoom._exitData[dir].HasFlag(Exit.ExitFlags.walled)
                            //        || ch._inRoom._exitData[dir].HasFlag(Exit.ExitFlags.blocked)
                            //        || ch._inRoom._exitData[dir].HasFlag(Exit.ExitFlags.secret)
                            //        || ch._inRoom._exitData[dir].HasFlag(Exit.ExitFlags.closed)
                            //        || !ch._inRoom._exitData[dir]._targetRoom
                            //        || ch._inRoom._area != ch._inRoom._exitData[dir]._targetRoom._area)
                            //{
                            //    ch.SendText("You see nothing in that direction.\r\n");
                            //    return;
                            //}
                            //room2 = Movement.FindRoom(ch, arg3);
                            //if (room2 == null)
                            //{
                            //    ch.SendText("You see nothing in that direction.\r\n");
                            //    return;
                            //}
                            //victim = CharData.GetCharAtRoom(room2, ch, arg2);
                            //if (victim == null)
                            //{
                            //    Room room3;
                            //    if (room2._exitData[dir] && ((room3 = Room.GetRoom(room2._exitData[dir]._vnum))) &&
                            //        spell == Spell.SpellList["fireball"])
                            //    {
                            //        victim = CharData.GetCharAtRoom(room3, ch, arg2);
                            //    }
                            //}
                            //if (victim == null)
                            //{
                            //    ch.SendText("They aren't here.\r\n");
                            //    return;
                            //}
                            //} //end else
                        } //end else
                        if (ch.IsAffected(Affect.AFFECT_CHARM) && ch._master == victim)
                        {
                            ch.SendText("You can't do that to your master!.\r\n");
                            return;
                        }

                        if (Combat.IsSafe(ch, victim))
                            return;
                        // Combat.IsSafe could wipe out victim, as it calls procs if a boss
                        // check and see that victim is still valid
                        if (!victim)
                            return;

                        Crime.CheckAttemptedMurder(ch, victim);

                        target = new Target(victim);
                        ch.BreakInvis();
                        break;
                }

            int beats = 0;  // For quick chant            
            if (!ch.IsClass(CharClass.Names.bard) && !ch.IsClass(CharClass.Names.psionicist))
            {
                ch.SendText( "You begin casting...\r\n" );
                if( "ventriloquate".Equals(spell.Name, StringComparison.CurrentCultureIgnoreCase ))
                {
                    if( spell.ValidTargets == TargetType.singleCharacterOffensive
                            || spell.ValidTargets == TargetType.singleCharacterRanged )
                        SocketConnection.Act( "$n&n begins casting an offensive spell...", ch, null, null, SocketConnection.MessageTarget.room );
                    else
                        SocketConnection.Act( "$n&n begins casting...", ch, null, null, SocketConnection.MessageTarget.room );
                }
                beats = spell.CastingTime;
            }
            else if (ch.IsClass(CharClass.Names.bard))
            {
                ch.SendText( "You begin singing...\r\n" );
                SocketConnection.Act( "$n&n starts singing...", ch, null, null, SocketConnection.MessageTarget.room );
                beats = 0;
            }

            if (!ch.IsClass(CharClass.Names.psionicist) && !ch.IsClass(CharClass.Names.bard))
            {
                // Characters with int of 110 have normal memtimes.
                // int of 100 worsens casting times by 10%
                // with an int of 55 casting times are doubled.
                // This may seem a bit harsh, but keep in mind any
                // casters are very likely to have an int above 100, as it
                // is their prime requisite.  120 is the max int for Grey Elves
                // to start.
                if (ch.IsClass(CharClass.Names.cleric) || ch.IsClass(CharClass.Names.druid)
                        || ch.IsClass(CharClass.Names.paladin) || ch.IsClass(CharClass.Names.antipaladin))
                {
                    beats = ( beats * 110 ) / ch.GetCurrWis();
                }
                else if (ch.IsClass( CharClass.Names.shaman))
                {
                    beats = ( beats * 330 ) / ( ch.GetCurrInt() + ( ch.GetCurrWis() * 2 ) );
                }
                else
                {
                    beats = ( beats * 110 ) / ch.GetCurrInt();
                }

                if( ch.CheckSkill("quick chant", PracticeType.only_on_success) )
                {
                    beats = beats / 2;
                }

                /*
                * A check for impossibly long cast times...came about from a player
                * trying to cast when feebleminded.  100 casting time is arbitrary.
                */
                if( beats > 100 )
                {
                    ch.SendText( "Forget it!  In your present state you haven't a dream of success.\r\n" );
                    return;
                }
                ch.WaitState( beats );

                if( CheckHypnoticPattern( ch ) )
                {
                    return;
                }

                CastData caster = new CastData();
                caster.Who = ch;
                caster.Eventdata = Event.CreateEvent( Event.EventType.spell_cast, beats, ch, target, spell );
                Database.CastList.Add( caster );
                ch.SetAffBit( Affect.AFFECT_CASTING );
            }
            else if (ch.IsClass( CharClass.Names.psionicist))
            {
                ch.WaitState( beats );

                if( CheckHypnoticPattern( ch ) )
                {
                    return;
                }

                ch.PracticeSpell( spell );

                int mana = 0;
                if( !ch.IsImmortal() && !ch.IsNPC()
                        && ch._level < ( spell.SpellCircle[ (int)ch._charClass.ClassNumber ] * 4 + 1 )
                        && MUDMath.NumberPercent() > ((PC)ch).SpellAptitude[spell.Name])
                {
                    ch.SendText( "You lost your concentration.\r\n" );
                    SocketConnection.Act( "&+r$n&n&+r's face flushes white for a moment.&n", ch, null, null, SocketConnection.MessageTarget.room );
                    ch._currentMana -= mana / 2;
                }
                else
                {
                    ch._currentMana -= mana;
                    string buf = String.Format( "Spell {0} ({1}) being willed by {2}", spell,
                                                spell.Name, ch._name );
                    Log.Trace( buf );
                    ch.SetAffBit( Affect.AFFECT_CASTING );
                    FinishSpell( ch, spell, target );
                }
                if( ch._position > Position.sleeping && ch._currentMana < 0 )
                {
                    ch.WaitState( 2 * Event.TICK_PER_SECOND );
                    ch.SendText( "&+WThat last spe&+wll w&+Las a _bitvector&+l much...&n\r\n" );
                    ch._position = Position.standing;
                    ch._fighting = null;
                    SocketConnection.Act( "$n&n collapses from exhaustion&n.",
                         ch, null, null, SocketConnection.MessageTarget.room );
                    ch._position = Position.sleeping;
                }

                if( spell.ValidTargets == TargetType.singleCharacterOffensive
                        && victim && victim._master != ch && victim != ch && victim.IsAwake() )
                {
                    foreach( CharData vch in ch._inRoom.People )
                    {
                        if( victim == vch && !victim._fighting )
                        {
                            victim.AttackCharacter( ch );
                            break;
                        }
                    }
                }
            }
            else if( ch.IsClass(CharClass.Names.bard ))
            {
                ch.WaitState( 0 );

                // Create an event to handle the spell
                CastData caster = new CastData();
                caster.Who = ch;
                caster.Eventdata = Event.CreateEvent( Event.EventType.bard_song, Event.TICK_SONG, ch, target, spell );
                caster.Eventdata = Event.CreateEvent( Event.EventType.bard_song, Event.TICK_SONG * 2, ch, target, spell);
                caster.Eventdata = Event.CreateEvent( Event.EventType.bard_song, Event.TICK_SONG * 3, ch, target, spell);
                caster.Eventdata = Event.CreateEvent( Event.EventType.bard_song, Event.TICK_SONG * 4, ch, target, spell);
                caster.Eventdata = Event.CreateEvent( Event.EventType.bard_song, Event.TICK_SONG * 5, ch, target, spell);
                Database.CastList.Add( caster );
                ch.SetAffBit( Affect.AFFECT_SINGING );
            }
            return;
        }

        /// <summary>
        /// Function for use with portal spells.  Can be reused for other spells if applicable.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static bool HasSpellConsent( CharData ch, CharData victim )
        {
            if( victim.IsNPC() || ch.IsNPC() )
                return true;

            if( ch.IsSameGuild( victim ) )
                return true;

            if( ch.IsSameGroup( victim ) )
                return true;

            if( ch.IsImmortal() )
                return true;

            if( !victim.IsConsenting(ch) )
            {
                ch.SendText( "You do not have their consent.\r\n" );
                return false;
            }

            return true;
        }

        static bool CheckHypnoticPattern( CharData ch )
        {
            CharData owner;

            if( !ch || ch.IsClass(CharClass.Names.psionicist) || ch.IsNPC() )
                return false;

            foreach( Object obj in ch._inRoom.Contents)
            {
                if( obj.ObjIndexData.IndexNumber == StaticObjects.OBJECT_NUMBER_HYPNOTIC_PATTERN )
                {
                    //we have a pattern
                    owner = obj.CreatedBy;
                    //doublecheck that we have the right ch
                    if( owner != null && !owner.IsNPC() )
                        //we can't figure who owns it
                        return false;
                    if( owner != null && owner.IsSameGroup( ch ) )
                        continue;
                    if( MUDMath.NumberPercent() < ch._level / 3 + ch.GetCurrInt() / 5 )
                        continue;
                    SocketConnection.Act( "Your concentration is disrupted by $p&n!", ch, obj, null, SocketConnection.MessageTarget.character );
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether a shaman has the proper totem to cast a spell.
        /// </summary>
        /// <param name="ch">The caster</param>
        /// <param name="spell">The spell number</param>
        /// <returns>true if the character is not a shaman, true if the character is a shaman and has the correct
        /// totem, and false if the character is a shaman without the correct totem.</returns>
        public static bool CheckTotem( CharData ch, Spell spell )
        {
            // Non-Shamans always succeed a totem check.
            if( !ch.IsClass( CharClass.Names.shaman ))
                return true;

            // Immortals can cast without totems in god mode.
            if (ch.IsImmortal() && ch.HasActBit(PC.PLAYER_GODMODE))
            {
                return true;
            }

            // Find totems
            Object totem = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_one );
            if( totem && totem.ItemType != ObjTemplate.ObjectType.totem )
                totem = null;
            Object totem2 = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_two );
            if( totem2 && totem2.ItemType != ObjTemplate.ObjectType.totem )
                totem2 = null;

            if( !totem && !totem2 )
            {
                ch.SendText( "You must be holding a totem in order to cast a spell.\r\n" );
                return false;
            }

            switch( spell.School )
            {
                default:
                    ch.SendText( "This spell has an invalid sphere setting for a shaman.  Please report this as a bug.\r\n" );
                    return false;
                case SCHOOL_SPIRITUAL:
                    if( spell.SpellCircle[ (int)ch._charClass.ClassNumber ] <= 5 )
                    {
                        if (!totem || !Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_L_SPIRIT.Vector))
                        {
                            if (!totem2 || !Macros.IsSet(totem2.Values[0], ObjTemplate.TOTEM_L_SPIRIT.Vector))
                            {
                                ch.SendText( "You are not holding the proper totem to cast this spell.\r\n" );
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (!totem || !Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_G_SPIRIT.Vector))
                        {
                            if (!totem2 || !Macros.IsSet(totem2.Values[0], ObjTemplate.TOTEM_G_SPIRIT.Vector))
                            {
                                ch.SendText( "You are not holding the proper totem to cast this spell.\r\n" );
                                return false;
                            }
                        }
                    }
                    break;
                case SCHOOL_ANIMAL:
                    if( spell.SpellCircle[ (int)ch._charClass.ClassNumber ] <= 5 )
                    {
                        if (!totem || !Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_L_ANIMAL.Vector))
                        {
                            if (!totem2 || !Macros.IsSet(totem2.Values[0], ObjTemplate.TOTEM_L_ANIMAL.Vector))
                            {
                                ch.SendText( "You are not holding the proper totem to cast this spell.\r\n" );
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (!totem || !Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_G_ANIMAL.Vector))
                        {
                            if (!totem2 || !Macros.IsSet(totem2.Values[0], ObjTemplate.TOTEM_G_ANIMAL.Vector))
                            {
                                ch.SendText( "You are not holding the proper totem to cast this spell.\r\n" );
                                return false;
                            }
                        }
                    }
                    break;
                case SCHOOL_ELEMENTAL:
                    if( spell.SpellCircle[ (int)ch._charClass.ClassNumber ] <= 5 )
                    {
                        if (!totem || !Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_L_ELEMENTAL.Vector))
                        {
                            if (!totem2 || !Macros.IsSet(totem2.Values[0], ObjTemplate.TOTEM_L_ELEMENTAL.Vector))
                            {
                                ch.SendText( "You are not holding the proper totem to cast this spell.\r\n" );
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (!totem || !Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_G_ELEMENTAL.Vector))
                        {
                            if (!totem2 || !Macros.IsSet(totem2.Values[0], ObjTemplate.TOTEM_G_ELEMENTAL.Vector))
                            {
                                ch.SendText( "You are not holding the proper totem to cast this spell.\r\n" );
                                return false;
                            }
                        }
                    }
                    break;
            }
            // No totem problems found. Must mean they have the right one.
            return true;
        }
    }
}
