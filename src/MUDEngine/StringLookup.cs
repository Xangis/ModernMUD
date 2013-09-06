using System;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Text-to-value conversion utility functions.
    /// </summary>
    public class StringLookup
    {
        /// <summary>
        /// Convert parts string to a value
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Race.Parts PartsLookup( string text )
        {
            int part = 0;

            if (!MUDString.StringsNotEqual(text, "none"))
            {
                return Race.Parts.none;
            }

            if( MUDString.NameContainedIn( "skull", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.skull );
            if( MUDString.NameContainedIn( "arms", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.arms );
            if( MUDString.NameContainedIn( "legs", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.legs );
            if( MUDString.NameContainedIn( "heart", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.heart );
            if( MUDString.NameContainedIn( "brains", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.brains );
            if( MUDString.NameContainedIn( "guts", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.guts );
            if( MUDString.NameContainedIn( "hands", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.hands );
            if( MUDString.NameContainedIn( "feet", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.feet );
            if( MUDString.NameContainedIn( "fingers", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.fingers );
            if( MUDString.NameContainedIn( "ears", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.ears );
            if( MUDString.NameContainedIn( "eyes", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.eyes );
            if( MUDString.NameContainedIn( "hooves", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.hooves );
            if( MUDString.NameContainedIn( "tongue", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.tongue );
            if( MUDString.NameContainedIn( "eyestalks", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.eyestalks );
            if( MUDString.NameContainedIn( "tentacles", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.tentacles );
            if( MUDString.NameContainedIn( "fins", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.fins );
            if( MUDString.NameContainedIn( "wings", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.wings );
            if( MUDString.NameContainedIn( "tail", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.tail );
            if( MUDString.NameContainedIn( "claws", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.claws );
            if( MUDString.NameContainedIn( "fangs", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.fangs );
            if( MUDString.NameContainedIn( "horns", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.horns );
            if( MUDString.NameContainedIn( "scales", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.scales );
            if( MUDString.NameContainedIn( "tusks", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.tusks );
            if( MUDString.NameContainedIn( "bark", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.bark );
            if( MUDString.NameContainedIn( "leaves", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.leaves );
            if( MUDString.NameContainedIn( "branches", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.branches );
            if( MUDString.NameContainedIn( "trunk", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.trunk );
            if( MUDString.NameContainedIn( "scalp", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.scalp );
            if( MUDString.NameContainedIn( "cranial_chitin", text ) )
                Macros.SetBit( ref part, (int)Race.Parts.cranial_chitin );

            return (Race.Parts)part;
        }

        /// <summary>
        /// Converts the _name of a language to a Race.Language value.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static Race.Language LanguageLookup( string lang )
        {
            if( !MUDString.StringsNotEqual( lang, "unknown" ) )
                return Race.Language.unknown;
            if( !MUDString.StringsNotEqual( lang, "common" ) )
                return Race.Language.common;
            if( !MUDString.StringsNotEqual( lang, "elven" ) )
                return Race.Language.elven;
            if( !MUDString.StringsNotEqual( lang, "dwarven" ) )
                return Race.Language.dwarven;
            if( !MUDString.StringsNotEqual( lang, "centaur" ) )
                return Race.Language.centaur;
            if( !MUDString.StringsNotEqual( lang, "ogre" ) )
                return Race.Language.ogre;
            if( !MUDString.StringsNotEqual( lang, "orc" ) || !MUDString.StringsNotEqual( lang, "orcish" ) )
                return Race.Language.orcish;
            if( !MUDString.StringsNotEqual( lang, "troll" ) )
                return Race.Language.troll;
            if( !MUDString.StringsNotEqual( lang, "aquaticelf" ) )
                return Race.Language.aquaticelf;
            if( !MUDString.StringsNotEqual( lang, "neogi" ) )
                return Race.Language.neogi;
            if( !MUDString.StringsNotEqual( lang, "thri-kreen" ) )
                return Race.Language.thri;
            if( !MUDString.StringsNotEqual( lang, "dragon" ) )
                return Race.Language.dragon;
            if( !MUDString.StringsNotEqual( lang, "magic" ) || !MUDString.StringsNotEqual( lang, "magical" ) )
                return Race.Language.magical;
            if( !MUDString.StringsNotEqual( lang, "goblin" ) )
                return Race.Language.goblin;
            if( !MUDString.StringsNotEqual( lang, "god" ) )
                return Race.Language.god;
            if( !MUDString.StringsNotEqual( lang, "halfling" ) )
                return Race.Language.halfling;
            if( !MUDString.StringsNotEqual( lang, "githyanki" ) )
                return Race.Language.githyanki;
            if( !MUDString.StringsNotEqual( lang, "drow" ) )
                return Race.Language.drow;
            if( !MUDString.StringsNotEqual( lang, "kobold" ) )
                return Race.Language.kobold;
            if( !MUDString.StringsNotEqual( lang, "gnome" ) )
                return Race.Language.gnome;
            if( !MUDString.StringsNotEqual( lang, "animal" ) )
                return Race.Language.animal;
            if( !MUDString.StringsNotEqual( lang, "duergar" ) )
                return Race.Language.duergar;
            if( !MUDString.StringsNotEqual( lang, "githzerai" ) )
                return Race.Language.githzerai;
            if( !MUDString.StringsNotEqual( lang, "gnoll" ) )
                return Race.Language.gnoll;
            if( !MUDString.StringsNotEqual( lang, "rakshasa" ) )
                return Race.Language.rakshasa;
            if( !MUDString.StringsNotEqual( lang, "minotaur" ) )
                return Race.Language.minotaur;
            if( !MUDString.StringsNotEqual( lang, "illithid" ) )
                return Race.Language.illithid;
            if( !MUDString.StringsNotEqual( lang, "barbarian" ) )
                return Race.Language.barbarian;

            Log.Error( "StringLookup.LanguageLookup: Language string not found.", 0 );

            return 0;
        }

        /// <summary>
        /// Converts the _name of a racewar side to a Race.RacewarSide value.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Race.RacewarSide RacewarLookup( string text )
        {
            if( !MUDString.StringsNotEqual( text, "none" ) )
                return Race.RacewarSide.neutral;
            if( !MUDString.StringsNotEqual( text, "good" ) )
                return Race.RacewarSide.good;
            if( !MUDString.StringsNotEqual( text, "evil" ) )
                return Race.RacewarSide.evil;
            if( !MUDString.StringsNotEqual( text, "enslaver" ) )
                return Race.RacewarSide.enslaver;
            if( !MUDString.StringsNotEqual( text, "neutral" ) )
                return Race.RacewarSide.neutral;

            Log.Error( "StringLookup.RacewarLookup: Racewar string not none/good/neutral/evil/enslaver.", 0 );

            return Race.RacewarSide.neutral;
        }

        /// <summary>
        /// Converts Yes No True False On Off to a bool string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool TrueFalseLookup( string text )
        {
            if( !MUDString.StringsNotEqual( text, "yes" ) )
                return true;
            if( !MUDString.StringsNotEqual( text, "true" ) )
                return true;
            if( !MUDString.StringsNotEqual( text, "on" ) )
                return true;
            if( !MUDString.StringsNotEqual( text, "no" ) )
                return false;
            if( !MUDString.StringsNotEqual( text, "false" ) )
                return false;
            if( !MUDString.StringsNotEqual( text, "off" ) )
                return false;

            Log.Error( "StringLookup.TrueFalseLookup: True false string not true/false yes/no on/off.", 0 );

            return false;
        }

        /// <summary>
        /// Look a damage type based on its name.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Race.DamageType DamageTypeLookup( string text )
        {
            int value = 0;

            if (!MUDString.StringsNotEqual(text, "none"))
            {
                return 0;
            }

            if( MUDString.NameContainedIn( "fire", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.fire );
            if( MUDString.NameContainedIn( "cold", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.cold );
            if( MUDString.NameContainedIn( "electricity", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.electricity );
            if( MUDString.NameContainedIn( "energy", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.energy );
            if( MUDString.NameContainedIn( "acid", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.acid );
            if( MUDString.NameContainedIn( "poison", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.poison );
            if( MUDString.NameContainedIn( "charm", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.charm );
            if( MUDString.NameContainedIn( "mental", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.mental );
            if( MUDString.NameContainedIn( "white_mana", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.whiteMana );
            if( MUDString.NameContainedIn( "black_mana", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.blackMana );
            if( MUDString.NameContainedIn( "disease", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.disease );
            if( MUDString.NameContainedIn( "drowning", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.drowning );
            if( MUDString.NameContainedIn( "light", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.light );
            if( MUDString.NameContainedIn( "sound", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.sound );
            if( MUDString.NameContainedIn( "magic", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.magic );
            if( MUDString.NameContainedIn( "nonmagic", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.nonmagic );
            if( MUDString.NameContainedIn( "silver", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.silver );
            if( MUDString.NameContainedIn( "iron", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.iron );
            if( MUDString.NameContainedIn( "wood", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.wood );
            if( MUDString.NameContainedIn( "weapon", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.weapon );
            if( MUDString.NameContainedIn( "bash", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.bash );
            if( MUDString.NameContainedIn( "pierce", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.pierce );
            if( MUDString.NameContainedIn( "slash", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.slash );
            if( MUDString.NameContainedIn( "gas", text ) )
                Macros.SetBit( ref value, (int)Race.DamageType.gas );

            return (Race.DamageType)value;
        }

        /// <summary>
        /// Converts mob innate string to a value.
        /// </summary>
        /// <param name="innate"></param>
        /// <param name="text"></param>
        public static void InnateLookup( int[] innate, string text )
        {
            int index;

            if (!MUDString.StringsNotEqual(text, "none"))
            {
                return;
            }

            for( index = 0; BitvectorFlagType.InnateFlags[ index ].BitvectorData; index++ )
            {
                if( MUDString.NameContainedIn( BitvectorFlagType.InnateFlags[ index ].Name, text ) )
                {
                    Macros.SetBit( ref innate[ BitvectorFlagType.InnateFlags[ index ].BitvectorData.Group ], BitvectorFlagType.InnateFlags[ index ].BitvectorData.Vector );
                }
            }
        }

        /// <summary>
        /// Converts spell school string to a value.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int SchoolLookup( string text )
        {
            int school = 0;

            if (!MUDString.StringsNotEqual(text, "none"))
            {
                return 0;
            }

            if( MUDString.NameContainedIn( "abjuration", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_ABJURATION );
            if( MUDString.NameContainedIn( "alteration", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_ALTERATION );
            if( MUDString.NameContainedIn( "conjuration", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_CONJURATION );
            if( MUDString.NameContainedIn( "summoning", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_SUMMONING );
            if( MUDString.NameContainedIn( "illusion", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_ILLUSION );
            if( MUDString.NameContainedIn( "phantasm", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_PHANTASM );
            if( MUDString.NameContainedIn( "invocation", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_INVOCATION );
            if( MUDString.NameContainedIn( "evocation", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_EVOCATION );
            if( MUDString.NameContainedIn( "enchantment", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_ENCHANTMENT );
            if( MUDString.NameContainedIn( "charm", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_CHARM );
            if( MUDString.NameContainedIn( "divination", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_DIVINATION );
            if( MUDString.NameContainedIn( "necromancy", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_NECROMANCY );
            if( MUDString.NameContainedIn( "martial_offensive", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_OFFENSIVE );
            if( MUDString.NameContainedIn( "martial_defensive", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_DEFENSIVE );
            if( MUDString.NameContainedIn( "skill_stealth", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_STEALTH );
            if( MUDString.NameContainedIn( "skill_survival", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_SURVIVAL );
            if( MUDString.NameContainedIn( "sham_elemental", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_ELEMENTAL );
            if( MUDString.NameContainedIn( "sham_spiritual", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_SPIRITUAL );
            if( MUDString.NameContainedIn( "sham_animal", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_ANIMAL );
            if( MUDString.NameContainedIn( "instr_horn", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_HORN );
            if( MUDString.NameContainedIn( "instr_flute", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_FLUTE );
            if( MUDString.NameContainedIn( "instr_mandolin", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_MANDOLIN );
            if( MUDString.NameContainedIn( "instr_lyre", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_LYRE );
            if( MUDString.NameContainedIn( "instr_drums", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_DRUMS );
            if( MUDString.NameContainedIn( "instr_harp", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_HARP );
            if( MUDString.NameContainedIn( "instr_pipes", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_PIPES );
            if( MUDString.NameContainedIn( "instr_fiddle", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_FIDDLE );
            if( MUDString.NameContainedIn( "instr_dulcimer", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_DULCIMER );
            if( MUDString.NameContainedIn( "chronomancy", text ) )
                Macros.SetBit( ref school, Magic.SCHOOL_CHRONOMANCY );

            if( school == 0 )
            {
                Log.Error( "School/realm keyword not found:", 0 );
                Log.Trace( text );
            }

            return school;
        }

        /// <summary>
        /// Converts spell mana string to a value.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ManaLookup( string text )
        {
            int mana = 0;

            if (!MUDString.StringsNotEqual(text, "none"))
            {
                return 0;
            }

            if (!MUDString.StringsNotEqual(text, "any"))
            {
                return -1;
            }

            if( MUDString.NameContainedIn( "earth", text ) )
                Macros.SetBit( ref mana, Magic.MANA_EARTH );
            if( MUDString.NameContainedIn( "air", text ) )
                Macros.SetBit( ref mana, Magic.MANA_AIR );
            if( MUDString.NameContainedIn( "fire", text ) )
                Macros.SetBit( ref mana, Magic.MANA_FIRE );
            if( MUDString.NameContainedIn( "water", text ) )
                Macros.SetBit( ref mana, Magic.MANA_WATER );
            if( MUDString.NameContainedIn( "time", text ) )
                Macros.SetBit( ref mana, Magic.MANA_TIME );
            if( MUDString.NameContainedIn( "horn", text ) )
                Macros.SetBit( ref mana, Magic.MANA_HORN );
            if( MUDString.NameContainedIn( "flute", text ) )
                Macros.SetBit( ref mana, Magic.MANA_FLUTE );
            if( MUDString.NameContainedIn( "mandolin", text ) )
                Macros.SetBit( ref mana, Magic.MANA_MANDOLIN );
            if( MUDString.NameContainedIn( "lyre", text ) )
                Macros.SetBit( ref mana, Magic.MANA_LYRE );
            if( MUDString.NameContainedIn( "drums", text ) )
                Macros.SetBit( ref mana, Magic.MANA_DRUMS );
            if( MUDString.NameContainedIn( "harp", text ) )
                Macros.SetBit( ref mana, Magic.MANA_HARP );
            if( MUDString.NameContainedIn( "pipes", text ) )
                Macros.SetBit( ref mana, Magic.MANA_PIPES );
            if( MUDString.NameContainedIn( "fiddle", text ) )
                Macros.SetBit( ref mana, Magic.MANA_FIDDLE );
            if( MUDString.NameContainedIn( "dulcimer", text ) )
                Macros.SetBit( ref mana, Magic.MANA_DULCIMER );
            if( MUDString.NameContainedIn( "instrument", text ) )
                Macros.SetBit( ref mana, Magic.MANA_INSTRUMENT );
            if( MUDString.NameContainedIn( "voice", text ) )
                Macros.SetBit( ref mana, Magic.MANA_VOICE );
            if( MUDString.NameContainedIn( "dance", text ) )
                Macros.SetBit( ref mana, Magic.MANA_DANCE );
            if( MUDString.NameContainedIn( "instr_augment", text ) )
                Macros.SetBit( ref mana, Magic.MANA_INSTR_AUGMENT );
            if( MUDString.NameContainedIn( "dance_augment", text ) )
                Macros.SetBit( ref mana, Magic.MANA_DANCE_AUGMENT );
            if( MUDString.NameContainedIn( "voice_augment", text ) )
                Macros.SetBit( ref mana, Magic.MANA_VOICE_AUGMENT );

            if( mana == 0 )
            {
                Log.Error( "Mana keyword not found:", 0 );
                Log.Trace( text );
            }

            return mana;

        }

        /// <summary>
        /// Converts spell school string to a value.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TargetType TargetLookup( string text )
        {
            if( !MUDString.StringsNotEqual( text, "none" ) )
            {
                Log.Error( "Spell _targetType type set to 'none'.  Please correct this.", 0 );
                return 0;
            }

            if( !MUDString.StringsNotEqual( "TargetType.none", text ) )
                return TargetType.none;
            if( !MUDString.StringsNotEqual( "TargetType.singleCharacterOffensive", text ) )
                return TargetType.singleCharacterOffensive;
            if( !MUDString.StringsNotEqual( "TargetType.singleCharacterDefensive", text ) )
                return TargetType.singleCharacterDefensive;
            if( !MUDString.StringsNotEqual( "TargetType.self", text ) )
                return TargetType.self;
            if( !MUDString.StringsNotEqual( "TargetType.objectInInventory", text ) )
                return TargetType.objectInInventory;
            if( !MUDString.StringsNotEqual( "TargetType.objectInRoom", text ) )
                return TargetType.objectInRoom;
            if( !MUDString.StringsNotEqual( "TargetType.trap", text ) )
                return TargetType.trap;
            if( !MUDString.StringsNotEqual( "TargetType.objectCorpse", text ) )
                return TargetType.objectCorpse;
            if( !MUDString.StringsNotEqual( "TargetType.objectOrCharacter", text ) )
                return TargetType.objectOrCharacter;
            if( !MUDString.StringsNotEqual( "TargetType.singleCharacterRanged", text ) )
                return TargetType.singleCharacterRanged;
            if( !MUDString.StringsNotEqual( "TargetType.singleCharacterWorld", text ) )
                return TargetType.singleCharacterWorld;
            if( !MUDString.StringsNotEqual( "TargetType.ritual", text ) )
                return TargetType.ritual;
            if( !MUDString.StringsNotEqual( "TargetType.multipleCharacterOffensive", text ) )
                return TargetType.multipleCharacterOffensive;

            Log.Error( "StringLookup.TargetLookup: Target type not found:", 0 );
            Log.Trace( text );

            return 0;
        }

        /// <summary>
        /// Get an object special based on its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<ObjSpecial> SpecObjLookup( string name )
        {
            List<ObjSpecial> functions = new List<ObjSpecial>();

            foreach( ObjSpecial spec in ObjFun.ObjectSpecialTable )
            {
                if (name.Contains(spec.Name))
                {
                    functions.Add(spec);
                }
            }
            return functions;
        }

        /// <summary>
        /// Find a monk skill based on its _name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MonkSkill MonkSkillLookup( string name )
        {
            foreach (KeyValuePair<String, MonkSkill> kvp in Database.MonkSkillList)
            {
                if (kvp.Value.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return kvp.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Find a spell based on its _name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Spell SpellLookup( string name )
        {
            foreach (KeyValuePair<String, Spell> kvp in Spell.SpellList)
            {
                if (kvp.Value.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return kvp.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Find a song based on its _name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Song SongLookup( string name )
        {
            foreach (KeyValuePair<String, Song> kvp in Database.SongList)
            {
                if (kvp.Value.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return kvp.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a size value based on its name.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Race.Size SizeLookup( string size )
        {
            int index;
            for (index = 0; index < Race.MAX_SIZE; index++)
            {
                if (!MUDString.IsPrefixOf(size, Race.SizeString((Race.Size)index)))
                {
                    return (Race.Size)index;
                }
            }

            Log.Error( "SizeLookup: Unable to match size string " + size );
            return Race.Size.medium;
        }

        /// <summary>
        /// Get the language ID from a language name.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static Race.Language LanguageLookup(CharData ch, string lang)
        {
            if (ch == null) return 0;

            if (ch.IsImmortal() && MUDString.IsNumber(lang))
            {
                int ilang;
                Int32.TryParse(lang, out ilang);
                if (ilang >= 0 && ilang < Race.MAX_LANG)
                    return (Race.Language)ilang;
                string buf = String.Format("{0} is not within valid language range(0 to {1})!\r\n", ilang, (Race.MAX_LANG - 1));
                ch.SendText(buf);
                return Race.Language.unknown;
            }

            if (!MUDString.IsPrefixOf(lang, "unknown"))
                return Race.Language.unknown;
            if (!MUDString.IsPrefixOf(lang, "common"))
                return Race.Language.common;
            if (!MUDString.IsPrefixOf(lang, "elven"))
                return Race.Language.elven;
            if (!MUDString.IsPrefixOf(lang, "dwarven"))
                return Race.Language.dwarven;
            if (!MUDString.IsPrefixOf(lang, "centaur"))
                return Race.Language.centaur;
            if (!MUDString.IsPrefixOf(lang, "ogre"))
                return Race.Language.ogre;
            if (!MUDString.IsPrefixOf(lang, "orc"))
                return Race.Language.orcish;
            if (!MUDString.IsPrefixOf(lang, "troll"))
                return Race.Language.troll;
            if (!MUDString.IsPrefixOf(lang, "aquatic elf"))
                return Race.Language.aquaticelf;
            if (!MUDString.IsPrefixOf(lang, "neogi"))
                return Race.Language.neogi;
            if (!MUDString.IsPrefixOf(lang, "thri-kreen"))
                return Race.Language.thri;
            if (!MUDString.IsPrefixOf(lang, "dragon"))
                return Race.Language.dragon;
            if (!MUDString.IsPrefixOf(lang, "magic"))
                return Race.Language.magical;
            if (!MUDString.IsPrefixOf(lang, "goblin"))
                return Race.Language.goblin;
            if (!MUDString.IsPrefixOf(lang, "god"))
                return Race.Language.god;
            if (!MUDString.IsPrefixOf(lang, "halfling"))
                return Race.Language.halfling;
            if (!MUDString.IsPrefixOf(lang, "githyanki"))
                return Race.Language.githyanki;
            if (!MUDString.IsPrefixOf(lang, "drow"))
                return Race.Language.drow;
            if (!MUDString.IsPrefixOf(lang, "kobold"))
                return Race.Language.kobold;
            if (!MUDString.IsPrefixOf(lang, "gnome"))
                return Race.Language.gnome;
            if (!MUDString.IsPrefixOf(lang, "animal"))
                return Race.Language.animal;
            if (!MUDString.IsPrefixOf(lang, "duergar"))
                return Race.Language.duergar;
            if (!MUDString.IsPrefixOf(lang, "githzerai"))
                return Race.Language.githzerai;
            if (!MUDString.IsPrefixOf(lang, "gnoll"))
                return Race.Language.gnoll;
            if (!MUDString.IsPrefixOf(lang, "rakshasa"))
                return Race.Language.rakshasa;
            if (!MUDString.IsPrefixOf(lang, "minotaur"))
                return Race.Language.minotaur;
            if (!MUDString.IsPrefixOf(lang, "illithid"))
                return Race.Language.illithid;
            if (!MUDString.IsPrefixOf(lang, "barbarian"))
                return Race.Language.barbarian;

            return Race.Language.unknown;
        }

    }
}