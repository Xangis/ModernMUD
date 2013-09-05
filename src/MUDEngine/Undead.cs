using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents information and utility functions for undead, mainly
    /// for use with necromancers, but useful for other things, like
    /// turning/dispelling.
    /// </summary>
    public class Undead
    {
        public string Name { get; set; }
        public int MobIndexNumber { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        /// <summary>
        /// The number of control slots taken up by the type of undead.
        /// </summary>
        public int Slots { get; set; }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="indexNumber"></param>
        /// <param name="minlev"></param>
        /// <param name="maxlev"></param>
        /// <param name="numslots"></param>
        Undead( string nam, int indexNumber, int minlev, int maxlev, int numslots )
        {
            Name = nam;
            MobIndexNumber = indexNumber;
            MinLevel = minlev;
            MaxLevel = maxlev;
            Slots = numslots;
        }

        /// <summary>
        /// Table of the different summonable undead types.
        /// </summary>
        public static Undead[] Table =
        {
            new Undead( "skeleton",  StaticMobs.MOB_NUMBER_SKELETON,    5,  15,  1 ),
            new Undead( "zombie",    StaticMobs.MOB_NUMBER_ZOMBIE,      5,  20,  1 ),
            new Undead( "spectre",   StaticMobs.MOB_NUMBER_SPECTRE,     20, 41,  3 ),
            new Undead( "wraith",    StaticMobs.MOB_NUMBER_WRAITH,      30, 40,  5 ),
            new Undead( "vampire",   StaticMobs.MOB_NUMBER_VAMPIRE,     35, 41,  8 ),
            new Undead( "lich",      StaticMobs.MOB_NUMBER_LICH,        41, 50, 15 ),
            new Undead( "dracolich", StaticMobs.MOB_NUMBER_RED_DRACO,   41, 53, 26 ),
            new Undead( "dracolich", StaticMobs.MOB_NUMBER_BLUE_DRACO,  41, 53, 26 ),
            new Undead( "dracolich", StaticMobs.MOB_NUMBER_BLACK_DRACO, 41, 53, 26 ),
            new Undead( "dracolich", StaticMobs.MOB_NUMBER_GREEN_DRACO, 41, 53, 26 ),
            new Undead( "dracolich", StaticMobs.MOB_NUMBER_WHITE_DRACO, 41, 53, 26 )

        };

        public const int UNDEAD_SKELETON = 0;
        public const int UNDEAD_ZOMBIE = 1;
        public const int UNDEAD_SPECTRE = 2;
        public const int UNDEAD_WRAITH = 3;
        public const int UNDEAD_VAMPIRE = 4;
        public const int UNDEAD_LICH = 5;
        public const int UNDEAD_RED_DRACO = 6;
        public const int UNDEAD_BLUE_DRACO = 7;
        public const int UNDEAD_BLACK_DRACO = 8;
        public const int UNDEAD_GREEN_DRACO = 9;
        public const int UNDEAD_WHITE_DRACO = 10;
        public const int UNDEAD_MAX = 11;

        /// <summary>
        /// Sets the level of an undead creature based on info from the character,
        /// the corpse, and the type of undead being raised.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="corpseLevel"></param>
        /// <param name="petType"></param>
        /// <returns></returns>
        public static int FuzzyLevel( CharData ch, int corpseLevel, int petType )
        {
            int temp = corpseLevel;

            if( ( corpseLevel > Table[ petType ].MaxLevel ) && ( corpseLevel <= ch.Level ) )
            {
                return Table[ petType ].MaxLevel;
            }
            if( ( corpseLevel >= ch.Level ) &&
                ( ch.Level <= Table[ petType ].MaxLevel ) )
            {
                temp = ( ch.Level + ( ( MUDMath.Dice( 1, ( ( corpseLevel + 1 ) - ch.Level ) ) ) - 1 ) );
                return Math.Min( temp, Table[ petType ].MaxLevel );
            }
            if( ch.Level >= corpseLevel )
            {
                return Math.Min( ch.Level, Table[ petType ].MaxLevel );
            }
            return temp;
        }

        /// <summary>
        /// Checks whether undead type is a dracolich.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDracolichType( int type )
        {
            if (type != UNDEAD_RED_DRACO && type != UNDEAD_BLUE_DRACO
                    && type != UNDEAD_BLACK_DRACO && type != UNDEAD_GREEN_DRACO
                    && type != UNDEAD_WHITE_DRACO)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Counts the number of control points used by a necromancer's pets.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int CountUndeadPets( CharData ch )
        {
            CharData pet;
            int count = 0;

            foreach( CharData flist in ch.Followers )
            {
                pet = flist;
                if( pet && pet.IsNPC() )
                {
                    if( "skeleton undead".StartsWith(pet.Name, StringComparison.CurrentCultureIgnoreCase))
                        count += Table[ UNDEAD_SKELETON ].Slots;
                    if ("zombie undead".StartsWith(pet.Name, StringComparison.CurrentCultureIgnoreCase))
                        count += Table[ UNDEAD_ZOMBIE ].Slots;
                    if ("spectre undead".StartsWith(pet.Name, StringComparison.CurrentCultureIgnoreCase))
                        count += Table[ UNDEAD_SPECTRE ].Slots;
                    if ("wraith undead".StartsWith(pet.Name, StringComparison.CurrentCultureIgnoreCase))
                        count += Table[ UNDEAD_WRAITH ].Slots;
                    if ("vampire undead".StartsWith(pet.Name, StringComparison.CurrentCultureIgnoreCase))
                        count += Table[ UNDEAD_VAMPIRE ].Slots;
                    if ("lich undead".StartsWith(pet.Name, StringComparison.CurrentCultureIgnoreCase))
                        count += Table[ UNDEAD_LICH ].Slots;
                    if( !MUDString.IsSuffixOf( "dracolich undead", pet.Name ) )
                        count += Table[ UNDEAD_RED_DRACO ].Slots;
                    if( !MUDString.IsSuffixOf( "dracolich undead", pet.Name ) )
                        count += Table[ UNDEAD_BLUE_DRACO ].Slots;
                    if( !MUDString.IsSuffixOf( "dracolich undead", pet.Name ) )
                        count += Table[ UNDEAD_BLACK_DRACO ].Slots;
                    if( !MUDString.IsSuffixOf( "dracolich undead", pet.Name ) )
                        count += Table[ UNDEAD_GREEN_DRACO ].Slots;
                    if( !MUDString.IsSuffixOf( "dracolich undead", pet.Name ) )
                        count += Table[ UNDEAD_WHITE_DRACO ].Slots;

                }
            }
            return count;
        }
    }
}