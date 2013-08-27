using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// MUD-specific numerical methods.
    /// </summary>
    public class MUDMath
    {
        /// <summary>
        /// A standard die-rolling method.
        /// </summary>
        /// <param name="number">The number of dice being rolled.</param>
        /// <param name="size">The number of sides on each die.</param>
        /// <returns></returns>
        public static int Dice( int number, int size )
        {
            int idice;
            int sum;

            switch( size )
            {
                case 0:
                    return 0;
                case 1:
                    return number;
            }

            for( idice = 0, sum = 0; idice < number; idice++ )
            {
                sum += NumberRange( 1, size );
            }

            return sum;
        }

        /// <summary>
        /// Makes a number "fuzzy" by giving it a 50% change of being either 1 higher (25%) or
        /// 1 lower than the original number (25%).
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int FuzzyNumber( int number )
        {
            switch( NumberBits( 2 ) )
            {
                case 0:
                    number -= 1;
                    break;
                case 3:
                    number += 1;
                    break;
            }

            return Math.Max( 1, number );
        }

        /// <summary>
        /// Generate a random number.
        /// </summary>
        /// <param name="from">Minimum value.</param>
        /// <param name="to">Maximum value.</param>
        /// <returns></returns>
        public static int NumberRange( int from, int to )
        {
            int power;
            int number;

            if ((to = to - from + 1) <= 1)
            {
                return from;
            }

            for (power = 2; power < to; power <<= 1)
            {
            }

            while ((number = RandomNumber.Get() & (power - 1)) >= to)
            {
            }

            return from + number;
        }

        /// <summary>
        /// Generates a percentile roll.
        /// </summary>
        /// <returns></returns>
        public static int NumberPercent()
        {
            return NumberRange(0, 100);
        }

        /// <summary>
        /// Gets a number from 0 to 2^bits.
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public static int NumberBits( int width )
        {
            return RandomNumber.Get() & ( ( 1 << width ) - 1 );
        }

        /// <summary>
        /// Linear interpolation for hit roll and other level-based items.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="lowLevelValue"></param>
        /// <param name="maxLevelValue"></param>
        /// <returns></returns>
        public static int Interpolate( int level, int lowLevelValue, int maxLevelValue )
        {
            return lowLevelValue + level * ( maxLevelValue - lowLevelValue ) / Limits.MAX_ADVANCE_LEVEL;
        }

    };
}