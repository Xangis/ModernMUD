using System;

namespace MUDEngine
{
    /// <summary>
    /// Drunkenness string replacement class.
    /// </summary>
    class DrunkSpeech
    {
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="minlvl"></param>
        /// <param name="numrep"></param>
        /// <param name="replace"></param>
        DrunkSpeech( int minlvl, int numrep, string[] replace )
        {
            _minDrunkLevel = minlvl;
            _numReplacements = numrep;
            _replacement = replace;
        }

        readonly int _minDrunkLevel;
        readonly int _numReplacements;
        readonly string[] _replacement;

        /// <summary>
        /// Makes a string look drunk.
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string MakeDrunk( string inputString, CharData ch )
        {
            int drunklevel = 0;

            if (!ch.IsNPC())
            {
                drunklevel = ((PC)ch).Drunk;
            }

            // Nothing to do here.
            if (drunklevel == 0)
            {
                return inputString;
            }

            string output = String.Empty;

            // Check how drunk a person is...
            for (int pos = 0; pos < inputString.Length; pos++)
            {
                char temp = inputString[pos];

                if ((char.ToUpper(temp) >= 'A') && (char.ToUpper(temp) <= 'Z'))
                {
                    int drunkpos = char.ToUpper(temp) - 'A';
                    if (drunklevel > _drunkSubstitution[drunkpos]._minDrunkLevel)
                    {
                        int randomnum = MUDMath.NumberRange(0, _drunkSubstitution[drunkpos]._numReplacements);
                        output += _drunkSubstitution[drunkpos]._replacement[randomnum];
                    }
                    else
                    {
                        output += temp;
                    }
                }
                else
                {
                    if (drunklevel < 4)
                    {
                        output += MUDMath.FuzzyNumber(temp);
                    }
                    else if ((temp >= '0') && (temp <= '9'))
                    {
                        output += MUDMath.NumberRange(0, 9).ToString();
                    }
                    else
                    {
                        output += temp;
                    }
                }
            }
            return ( output );
        }

        /// <summary>
        /// Drunkenness string replacements.
        /// </summary>
        static readonly DrunkSpeech[] _drunkSubstitution = new DrunkSpeech[]
        {
            new DrunkSpeech( 3, 10, new[]{ "a", "a", "a", "A", "aa", "ah", "Ah", "ao", "aw", "oa", "ahhhh" } ),
            new DrunkSpeech( 8, 5, new []{ "b", "b", "b", "B", "B", "vb" } ),
            new DrunkSpeech( 3, 5, new []{ "c", "c", "C", "ch", "sj", "zj" } ),
            new DrunkSpeech( 5, 2, new []{ "d", "d", "D" } ),
            new DrunkSpeech( 3, 3, new []{ "e", "e", "eh", "E" } ),
            new DrunkSpeech( 4, 5, new []{ "f", "f", "ff", "fff", "fFf", "F" } ),
            new DrunkSpeech( 8, 4, new []{ "g", "g", "G", "jeh", "guh" } ),
            new DrunkSpeech( 9, 7, new []{ "h", "h", "hh", "hhh", "Hhh", "HhH", "H", "hu" } ),
            new DrunkSpeech( 7, 7, new []{ "i", "i", "Iii", "ii", "iI", "Ii", "I", "eye" } ),
            new DrunkSpeech( 9, 5, new []{ "j", "j", "jj", "Jj", "jJ", "J" } ),
            new DrunkSpeech( 7, 4, new []{ "k", "k", "K", "kah", "kuh" } ),
            new DrunkSpeech( 3, 3, new []{ "l", "l", "L", "ll" } ),
            new DrunkSpeech( 5, 8, new []{ "m", "m", "mm", "mmm", "mmmm", "mmmmm", "MmM", "mM", "M" } ),
            new DrunkSpeech( 6, 8, new []{ "n", "n", "nn", "Nn", "nnn", "nNn", "N", "en", "un" } ),
            new DrunkSpeech( 3, 6, new []{ "o", "o", "ooo", "ao", "aOoo", "Ooo", "ooOo" } ),
            new DrunkSpeech( 3, 3, new []{ "p", "p", "P", "puh" } ),
            new DrunkSpeech( 5, 5, new []{ "q", "q", "Q", "ku", "ququ", "kukeleku" } ),
            new DrunkSpeech( 4, 3, new []{ "r", "r", "R", "rr" } ),
            new DrunkSpeech( 2, 6, new []{ "s", "sh", "ss", "zzZzssZ", "ZSssS", "sSzzsss", "sSss" } ),
            new DrunkSpeech( 5, 5, new []{ "t", "t", "T", "tuh", "tea", "tee" } ),
            new DrunkSpeech( 3, 6, new []{ "u", "u", "uh", "Uh", "Uhuhhuh", "uhU", "uhhu" } ),
            new DrunkSpeech( 4, 4, new []{ "v", "v", "V", "uv", "vv" } ),
            new DrunkSpeech( 4, 5, new []{ "w", "w", "W", "hw", "wuh" } ),
            new DrunkSpeech( 5, 7, new []{ "x", "x", "X", "ks", "iks", "kz", "xz", "ex" } ),
            new DrunkSpeech( 3, 2, new []{ "y", "y", "Y" } ),
            new DrunkSpeech( 2, 8, new []{ "z", "z", "ZzzZz", "Zzz", "Zsszzsz", "szz", "sZZz", "ZSz", "zZ"} )
        };
    }
}
