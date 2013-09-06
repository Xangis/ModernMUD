using System;
namespace MUDEngine
{

    public class Macros
    {
        /// <summary>
        /// Bounds a value between low and high values (inclusive).
        /// </summary>
        /// <param name="low"></param>
        /// <param name="value"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static int Range( int low, int value, int high )
        {
            return ((value) < (low) ? (low) : ((value) > (high) ? (high) : (value)));
        }

        /// <summary>
        /// Checks whether a bit is set on an int.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool IsSet( int flag, int bit )
        {
            return ((( flag ) & ( bit )) > 0);
        }

        /// <summary>
        /// Sets a bit flag on an int value.
        /// </summary>
        /// <param name="var"></param>
        /// <param name="bit"></param>
        public static void SetBit( ref int var, int bit )
        {
            var = ( var |= bit );
        }

        /// <summary>
        /// Removes a bit flag from an int value.
        /// </summary>
        /// <param name="var"></param>
        /// <param name="bit"></param>
        public static void RemoveBit( ref int var, int bit )
        {
            var = (var &= ( ~bit ));
        }

        /// <summary>
        /// Calculates the base mana for a character.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <returns></returns>
        public static int ManaBase( CharData ch, Spell spell )
        {
            return (30 + 15 * (spell.SpellCircle[(int)ch.CharacterClass.ClassNumber]));
        }

        /// <summary>
        /// Calculates the mana scale for a spell.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <returns></returns>
        public static int ManaScale( CharData ch, Spell spell )
        {
            return (spell.SpellCircle[(int)ch.CharacterClass.ClassNumber] + 9);
        }

        /// <summary>
        /// Calculates the mana level for a spell.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <returns></returns>
        public static int ManaLevel( CharData ch, Spell spell )
        {
            return ( ( ch.Level - 1 ) - 5 * (
                                    spell.SpellCircle[(int)ch.CharacterClass.ClassNumber] - 1));
        }

        /// <summary>
        /// Calculates the mana cost for a spell.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <returns></returns>
        public static int ManaCost( CharData ch, Spell spell )
        {
            return ( ch.IsNPC() ? 0 : Math.Max( spell.MinimumMana,
                                    ManaBase( ch, spell ) - ManaScale( ch, spell ) *
                                    ManaLevel( ch, spell ) ) );
        }

    }
}