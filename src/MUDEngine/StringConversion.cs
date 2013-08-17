using System;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Utility functions for converting values into strings.
    /// </summary>
    public class StringConversion
    {
        /// <summary>
        /// Get the string representation of a language.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string LanguageString( Race.Language value )
        {
            if( value == Race.Language.unknown )
                return "unknown";
            if( value == Race.Language.common )
                return "common";
            if( value == Race.Language.elven )
                return "elven";
            if( value == Race.Language.dwarven )
                return "dwarven";
            if( value == Race.Language.centaur )
                return "centaur";
            if( value == Race.Language.ogre )
                return "ogre";
            if( value == Race.Language.orcish )
                return "orcish";
            if( value == Race.Language.troll )
                return "troll";
            if( value == Race.Language.aquaticelf )
                return "aquaticelf";
            if( value == Race.Language.neogi )
                return "neogi";
            if( value == Race.Language.thri )
                return "thri-kreen";
            if( value == Race.Language.dragon )
                return "dragon";
            if( value == Race.Language.magical )
                return "magic";
            if( value == Race.Language.goblin )
                return "goblin";
            if( value == Race.Language.god )
                return "god";
            if( value == Race.Language.halfling )
                return "halfling";
            if( value == Race.Language.githyanki )
                return "githyanki";
            if( value == Race.Language.drow )
                return "drow";
            if( value == Race.Language.kobold )
                return "kobold";
            if( value == Race.Language.gnome )
                return "gnome";
            if( value == Race.Language.animal )
                return "animal";
            if( value == Race.Language.duergar )
                return "duergar";
            if( value == Race.Language.githzerai )
                return "githzerai";
            if( value == Race.Language.gnoll )
                return "gnoll";
            if( value == Race.Language.rakshasa )
                return "rakshasa";
            if( value == Race.Language.minotaur )
                return "minotaur";
            if( value == Race.Language.illithid )
                return "illithid";
            if( value == Race.Language.barbarian )
                return "barbarian";

            return "invalid_language";
        }

        /// <summary>
        /// Gets the display name from the monk stance name.
        /// </summary>
        /// <param name="stance"></param>
        /// <returns></returns>
        public static string StanceString( string stance )
        {
            if( String.IsNullOrEmpty(stance) )
                return "none";
            if( stance == "Bear Stance" )
                return "bear";
            if (stance == "Cat Stance")
                return "cat";
            if (stance == "Cobra Stance")
                return "cobra";
            if (stance == "Crane Stance")
                return "crane";
            if (stance == "Dragon Stance")
                return "dragon";
            if (stance == "Dragonfly Stance")
                return "dragonfly";
            if (stance == "Hawk Stance")
                return "hawk";
            if (stance == "Leopard Stance")
                return "leopard";
            if (stance == "Mantis Stance")
                return "mantis";
            if (stance == "Monkey Stance")
                return "monkey";
            if (stance == "Snake Stance")
                return "snake";
            if (stance == "Tiger Stance")
                return "tiger";

            return "none";
        }

        /// <summary>
        /// Gets faction standing as a string.
        /// </summary>
        /// <param name="faction"></param>
        /// <returns></returns>
        public static string FactionString(double faction)
        {
            if (faction < (Limits.MIN_FACTION * 0.9))
                return "abject hatred";
            if (faction < (Limits.MIN_FACTION * 0.7))
                return "hatred";
            if (faction < (Limits.MIN_FACTION * 0.5))
                return "strong dislike";
            if (faction < (Limits.MIN_FACTION * 0.3))
                return "dislike";
            if (faction < (Limits.MIN_FACTION * 0.1))
                return "slight dislike";
            if (faction < (Limits.MAX_FACTION * 0.1))
                return "indifferent";
            if (faction < (Limits.MAX_FACTION * 0.3))
                return "warm";
            if (faction < (Limits.MAX_FACTION * 0.5))
                return "friendly";
            if (faction < (Limits.MAX_FACTION * 0.7))
                return "very friendly";
            if (faction < (Limits.MAX_FACTION * 0.9))
                return "alliance";
            return "blood alliance";
        }

        // TODO: Make spell schools a type-safe bitvector and make this method
        // the ToString() override for spell schools.
        /// <summary>
        /// Converts an integer value for a spell's schools into a printable string.
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public static string SpellSchoolString(int school)
        {
            String str = String.Empty;

            if ((school & Magic.SCHOOL_ABJURATION) != 0)
                str += "abjuration, ";
            if ((school & Magic.SCHOOL_ALTERATION) != 0)
                str += "alteration, ";
            if ((school & Magic.SCHOOL_ANIMAL) != 0)
                str += "animal, ";
            if ((school & Magic.SCHOOL_CHARM) != 0)
                str += "charm, ";
            if ((school & Magic.SCHOOL_CHRONOMANCY) != 0)
                str += "chronomancy, ";
            if ((school & Magic.SCHOOL_CONJURATION) != 0)
                str += "conjuration, ";
            if ((school & Magic.SCHOOL_DEFENSIVE) != 0)
                str += "defensive, ";
            if ((school & Magic.SCHOOL_DIVINATION) != 0)
                str += "divination, ";
            if ((school & Magic.SCHOOL_DRUMS) != 0)
                str += "drums, ";
            if ((school & Magic.SCHOOL_DULCIMER) != 0)
                str += "dulcimer, ";
            if ((school & Magic.SCHOOL_ELEMENTAL) != 0)
                str += "elemental, ";
            if ((school & Magic.SCHOOL_ENCHANTMENT) != 0)
                str += "enchantment, ";
            if ((school & Magic.SCHOOL_EVOCATION) != 0)
                str += "evocation, ";
            if ((school & Magic.SCHOOL_FIDDLE) != 0)
                str += "fiddle, ";
            if ((school & Magic.SCHOOL_FLUTE) != 0)
                str += "flute, ";
            if ((school & Magic.SCHOOL_HARP) != 0)
                str += "harp, ";
            if ((school & Magic.SCHOOL_HORN) != 0)
                str += "horn, ";
            if ((school & Magic.SCHOOL_ILLUSION) != 0)
                str += "illusion, ";
            if ((school & Magic.SCHOOL_INVOCATION) != 0)
                str += "invocation, ";
            if ((school & Magic.SCHOOL_LYRE) != 0)
                str += "lyre, ";
            if ((school & Magic.SCHOOL_MANDOLIN) != 0)
                str += "mandolin, ";
            if ((school & Magic.SCHOOL_NECROMANCY) != 0)
                str += "necromancy, ";
            if ((school & Magic.SCHOOL_OFFENSIVE) != 0)
                str += "offensive, ";
            if ((school & Magic.SCHOOL_PHANTASM) != 0)
                str += "phantasm, ";
            if ((school & Magic.SCHOOL_PIPES) != 0)
                str += "pipes, ";
            if ((school & Magic.SCHOOL_SPIRITUAL) != 0)
                str += "spiritual, ";
            if ((school & Magic.SCHOOL_STEALTH) != 0)
                str += "stealth, ";
            if ((school & Magic.SCHOOL_SUMMONING) != 0)
                str += "summoning, ";
            if ((school & Magic.SCHOOL_SURVIVAL) != 0)
                str += "survival, ";

            if (String.IsNullOrEmpty(str))
            {
                return "none";
            }

            if (str.EndsWith(", "))
            {
                str = str.Substring(0, str.Length - 2);
            }

            return str;
        }

        /// <summary>
        /// Wear location names.
        /// </summary>
        public static readonly string[] EquipmentLocationDisplay = new[]
        {
            "&+y(none)                &n",
            "&+y(worn on left finger) &n",
            "&+y(worn on right finger)&n",
            "&+y(worn around neck1)   &n",
            "&+y(worn around neck2)   &n",
            "&+y(worn on body)        &n",
            "&+y(worn on head)        &n",
            "&+y(worn on legs)        &n",
            "&+y(worn on feet)        &n",
            "&+y(worn on hands)       &n",
            "&+y(worn on arms)        &n",
            "&+y(worn about body)     &n",
            "&+y(worn about waist)    &n",
            "&+y(worn on left wrist)  &n",
            "&+y(worn on right wrist) &n",
            "&+y(primary hand)        &n",
            "&+y(secondary hand)      &n",
            "&+y(worn on eyes)        &n",
            "&+y(worn on face)        &n",
            "&+y(worn in left ear)    &n",
            "&+y(worn in right ear)   &n",
            "&+y(worn as badge)       &n",
            "&+y(worn on back)        &n",
            "&+y(attached to belt1)   &n",
            "&+y(attached to belt2)   &n",
            "&+y(attached to belt3)   &n",
            "&+y(worn as quiver)      &n",
            "&+y(worn on tail)        &n",
            "&+y(worn on horse body)  &n",
            "&+y(worn on horns)       &n",
            "&+y(worn in nose)        &n",
            "&+y(third hand)          &n",
            "&+y(fourth hand)         &n",
            "&+y(lower arms)          &n",
            "&+y(lower hands)         &n",
            "&+y(lower left wrist)    &n",
            "&+y(lower right wrist)   &n",
        };

        /// <summary>
        /// Gets an alignment string from an alignment value.
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static string AlignmentString(int alignment)
        {
            if (alignment > 1000)
                return "buggy - too high";
            if (alignment == 1000)
                return "pure and holy";
            if (alignment > 900)
                return "extremely good";
            if (alignment > 600)
                return "very good";
            if (alignment > 350)
                return "good";
            if (alignment > 100)
                return "neutral leaning towards good";
            if (alignment > -100)
                return "neutral";
            if (alignment > -350)
                return "neutral leaning towards evil";
            if (alignment > -600)
                return "evil";
            if (alignment > -900)
                return "very evil";
            if (alignment > -1000)
                return "extremely evil";
            if (alignment == -1000)
                return "pure evil";
            return "buggy - too low";
        }

        /// <summary>
        /// Gets a character's alignment string.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string AlignmentString( CharData ch )
        {
            return AlignmentString(ch._alignment);
        }

        /// <summary>
        /// Gets the wear location text for an object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string WearString( Object obj )
        {
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_FINGER.Vector ) )
            {
                return "finger";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_NECK.Vector ) )
            {
                return "neck";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_BODY.Vector ) )
            {
                return "body";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_HEAD.Vector ) )
            {
                return "head";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_LEGS.Vector ) )
            {
                return "legs";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_FEET.Vector ) )
            {
                return "feet";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_HANDS.Vector ) )
            {
                return "hands";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_ARMS.Vector ) )
            {
                return "arms";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_SHIELD.Vector ) )
            {
                return "arms as a shield";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_ABOUT.Vector ) )
            {
                return "shoulders (about the body)";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_WAIST.Vector ) )
            {
                return "waist";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_WRIST.Vector ) )
            {
                return "wrist";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_EYES.Vector ) )
            {
                return "eyes";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_FACE.Vector ) )
            {
                return "face";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_EAR.Vector ) )
            {
                return "ears";
            }
            if( Macros.IsSet( obj.WearFlags[0], ObjTemplate.WEARABLE_ONBACK.Vector ) )
            {
                return "back";
            }
            return "nowhere";
        }

        /// <summary>
        /// Converts a bool to "yes" or "no".
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string YesNoString( bool value )
        {
            if( value )
            {
                return "yes";
            }
            return "no";
        }

        /// <summary>
        /// Gets the string for a skill proficiency.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SkillString( int value )
        {
            if( value == 0 )
                return "unlearned";
            if( value < 30 )
                return "just learning";
            if( value < 38 )
                return "below average";
            if( value < 46 )
                return "slightly below average";
            if( value < 54 )
                return "average";
            if( value < 62 )
                return "slightly above average";
            if( value < 70 )
                return "above average";
            if( value < 78 )
                return "good";
            if( value < 86 )
                return "very good";
            if( value < 94 )
                return "excellent";
            if( value < 96 )
                return "master";
            return "grand master";

            // Players should never get to grand master skill level.
        }


        /// <summary>
        /// Prints ability score as a string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AbilityScoreString( int value )
        {
            if( value > 100 )
                return "quite excellent";
            if( value > 90 )
                return "excellent";
            if( value > 80 )
                return "very good";
            if( value > 70 )
                return "good";
            if( value > 60 )
                return "above average";
            if( value > 50 )
                return "average";
            if( value > 40 )
                return "below average";
            if( value > 30 )
                return "bad";
            if( value > 20 )
                return "very bad";
            if( value > 10 )
                return "awful";
            if( value > 0 )
                return "incredibly lame";
            return "buggy";
        }

        /// <summary>
        /// Prints hitroll, damage, and other bonuses as a string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string BonusString( int value )
        {
            if( value > 50 )
                return "buggy";
            if( value > 30 )
                return "phenomenal";
            if( value > 23 )
                return "awesome";
            if( value > 16 )
                return "excellent";
            if( value > 10 )
                return "very good";
            if( value > 5 )
                return "good";
            if( value > 1 )
                return "above average";
            if( value > -2 )
                return "average";
            if( value > -6 )
                return "below average";
            if( value > -10 )
                return "bad";
            if( value > -15 )
                return "horrible";
            return "incredibly lame";
        }

        /// <summary>
        /// Gets a character's carry load as a string.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string WeightString( CharData ch )
        {
            int value = ( ch._carryWeight * 100 ) / ch.MaxCarryWeight();

            if( value > 100 )
                return "&+ROVERLOADED&n";
            if( value == 100 )
                return "&+YFully Loaded&n";
            if( value > 90 )
                return "&+YExtremely Heavy&n";
            if( value > 80 )
                return "&+rReally Heavy&n";
            if( value > 70 )
                return "&+mHeavy&n";
            if( value > 60 )
                return "&+mModerately Heavy&n";
            if( value > 50 )
                return "&+yModerate&n";
            if( value > 40 )
                return "&+bModerately Light&n";
            if( value > 30 )
                return "&+bLight&n";
            if( value > 20 )
                return "&+BReally Light&n";
            if( value > 10 )
                return "&+cExtremely Light&n";
            if( value > 00 )
                return "&+CAlmost Nothing&n";
            if( value == 0 )
                return "&+WNothing&n";
            return "&+Rbuggy - too light&n";
        }

        /// <summary>
        /// Gets progress to next experience level as a string.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string ExperienceString( CharData ch )
        {
            int exp = ( 100 * ch._experiencePoints ) / ExperienceTable.Table[ ch._level ].LevelExperience;

            if( exp < 10 )
                return "You have just begun the trek to your next level!";
            if( exp < 20 )
                return "You are still a very long way from your next level.";
            if( exp < 30 )
                return "You have gained some progress but still have a long way to your next level.";
            if( exp < 40 )
                return "You have gained some progress and are nearing the halfway point.";
            if( exp < 47 )
                return "You are close to the halfway point in the journey to your next level.";
            if( exp < 53 )
                return "You are at the halfway point.";
            if( exp < 60 )
                return "You have just passed the halfway point.";
            if( exp < 70 )
                return "You are well on your way to the next level.";
            if( exp < 80 )
                return "You are three quarters the way to your next level.";
            if( exp < 90 )
                return "You are almost ready to attain your next level.";
            return "You should level anytime now!";
        }

        /// <summary>
        /// Gets racewar side as a string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RacewarSideBWString( Race.RacewarSide value )
        {
            switch( value )
            {
                case Race.RacewarSide.enslaver:
                    return "enslaver";
                case Race.RacewarSide.good:
                    return "good";
                case Race.RacewarSide.evil:
                    return "evil";
                case Race.RacewarSide.neutral:
                    return "neutral";
                default:
                    return "buggy";
            }
        }

        /// <summary>
        /// Gets colorized text for the name of a racewar side.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RacewarSideColorString( Race.RacewarSide value )
        {
            switch( value )
            {
                case Race.RacewarSide.enslaver:
                    return "&+rEnslaver&n";
                case Race.RacewarSide.good:
                    return "&+yGood&n";
                case Race.RacewarSide.evil:
                    return "&+REvil&n";
                case Race.RacewarSide.neutral:
                    return "&+wNeutral&n";
                default:
                    return "&+WBuggy&n";
            }
        }

        /// <summary>
        /// Gets a space-separated list of mob special functions.
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static string MobSpecialString( List<MobSpecial> fun )
        {
            string str = String.Empty;
            bool found = false;

            foreach( MobSpecial function in fun )
            {
                if (found)
                {
                    str += " ";
                }
                str += function.SpecName;
                found = true;
            }

            if (!found)
            {
                return "none";
            }

            return str;
        }

        /// <summary>
        /// Gets an objects specials as a string.
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static string ObjectSpecialString( List<ObjSpecial> fun )
        {
            string names = String.Empty;
            bool first = true;

            foreach( ObjSpecial function in fun )
            {
                if( !first )
                {
                    names += " ";
                }
                names += function.Name;
                first = false;
            }
            return names;
        }

        /// <summary>
        /// Gets a bitvector's string values.
        /// </summary>
        /// <param name="bvect"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static string BitvectorString( ref Bitvector bvect, BitvectorFlagType[] flags )
        {
            int i;

            for( i = 0; flags[ i ].BitvectorData.Vector > 0; i++ )
            {
                if( flags[ i ].BitvectorData.Group == bvect.Group && flags[ i ].BitvectorData.Vector == bvect.Vector )
                    return flags[ i ].Name;
            }
            return null;
        }

        /// <summary>
        /// Gets a space-separated list of spheres for a totem.
        /// </summary>
        /// <param name="totem"></param>
        /// <returns></returns>
        public static string TotemSphereString( Object totem )
        {
            if (totem.ItemType != ObjTemplate.ObjectType.totem)
            {
                return "not a totem";
            }

            string text = String.Empty;

            if( Macros.IsSet( totem.Values[ 0 ], ObjTemplate.TOTEM_L_SPIRIT.Vector ) )
                text += "lesser spirit ";
            if (Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_G_SPIRIT.Vector))
                text += "greater spirit ";
            if (Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_L_ANIMAL.Vector))
                text += "lesser animal ";
            if (Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_G_ANIMAL.Vector))
                text += "greater animal ";
            if (Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_L_ELEMENTAL.Vector))
                text += "lesser spirit ";
            if (Macros.IsSet(totem.Values[0], ObjTemplate.TOTEM_G_ELEMENTAL.Vector))
                text += "greater spirit ";
            // If the totem has no sphere settings.
            if( text.Length < 1 )
                text += "none";

            return text;
        }

        /// <summary>
        /// Returns an ASCII item type string based on an object's type.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ItemTypeString( Object obj )
        {
            if (obj == null)
            {
                return String.Empty;
            }

            string text = obj.ItemType.ToString();

            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }

            Object inObj;

            for (inObj = obj; inObj.InObject; inObj = inObj.InObject)
            {
            }

            if (inObj.CarriedBy != null)
            {
                text = String.Format("StringConversion.ItemTypeString: Unknown type {0} from {1} owned by {2}.",
                          obj.ItemType, obj.Name, obj.CarriedBy._name);
            }
            else
            {
                text = String.Format(
                          "StringConversion.ItemTypeString: unknown type {0} from {1} owned by (unknown).",
                          obj.ItemType, obj.Name);
            }
            Log.Error( text, 0 );
            return "(unknown)";
        }

        /// <summary>
        /// Returns the text _name of an affect location.  We use this for printable strings
        /// because the ToString() for the enum has ugly things like underscores.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string AffectApplyString( Affect.Apply location )
        {
            switch( location )
            {
                case Affect.Apply.none:
                    return "none";
                case Affect.Apply.strength:
                    return "strength";
                case Affect.Apply.dexterity:
                    return "dexterity";
                case Affect.Apply.intelligence:
                    return "intelligence";
                case Affect.Apply.wisdom:
                    return "wisdom";
                case Affect.Apply.constitution:
                    return "constitution";
                case Affect.Apply.sex:
                    return "sex";
                case Affect.Apply.age:
                    return "age";
                case Affect.Apply.height:
                    return "height";
                case Affect.Apply.weight:
                    return "weight";
                case Affect.Apply.size:
                    return "size";
                case Affect.Apply.mana:
                    return "mana";
                case Affect.Apply.hitpoints:
                    return "hp";
                case Affect.Apply.move:
                    return "moves";
                case Affect.Apply.ac:
                    return "armor class";
                case Affect.Apply.hitroll:
                    return "hit roll";
                case Affect.Apply.damroll:
                    return "damage roll";
                case Affect.Apply.save_paralysis:
                    return "save vs paralysis";
                case Affect.Apply.save_poison:
                    return "save vs poison";
                case Affect.Apply.save_petrification:
                    return "save vs petrification";
                case Affect.Apply.save_breath:
                    return "save vs breath";
                case Affect.Apply.save_spell:
                    return "save vs spell";
                case Affect.Apply.fire_protection:
                    return "fire protection";
                case Affect.Apply.agility:
                    return "agility";
                case Affect.Apply.charisma:
                    return "charisma";
                case Affect.Apply.power:
                    return "power";
                case Affect.Apply.luck:
                    return "luck";
                case Affect.Apply.max_strength:
                    return "max strength";
                case Affect.Apply.max_dexterity:
                    return "max dexterity";
                case Affect.Apply.max_intelligence:
                    return "max intelligence";
                case Affect.Apply.max_wisdom:
                    return "max wisdom";
                case Affect.Apply.max_constitution:
                    return "max constitution";
                case Affect.Apply.max_agility:
                    return "max agility";
                case Affect.Apply.max_power:
                    return "max power";
                case Affect.Apply.max_charisma:
                    return "max charisma";
                case Affect.Apply.max_luck:
                    return "max luck";
                case Affect.Apply.race_strength:
                    return "racial strength";
                case Affect.Apply.race_dexterity:
                    return "racial dexterity";
                case Affect.Apply.race_intelligence:
                    return "racial intelligence";
                case Affect.Apply.race_wisdom:
                    return "racial wisdom";
                case Affect.Apply.race_constitution:
                    return "racial constitution";
                case Affect.Apply.race_agility:
                    return "racial agility";
                case Affect.Apply.race_power:
                    return "racial power";
                case Affect.Apply.race_charisma:
                    return "racial charisma";
                case Affect.Apply.race_luck:
                    return "racial luck";
                case Affect.Apply.curse:
                    return "curse";
                case Affect.Apply.resistant:
                    return "resistant";
                case Affect.Apply.immune:
                    return "immune";
                case Affect.Apply.susceptible:
                    return "susceptible";
                case Affect.Apply.vulnerable:
                    return "vulnerable";
                case Affect.Apply.race:
                    return "race";
            }

            Log.Error( "Affect_location_name: unknown location {0}.", location );
            return "(unknown)";
        }

        /// <summary>
        /// Returs the ASCII _name of a body part vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static string PartsBitString( Race.Parts vector )
        {
            string text = String.Empty;

            if( ( vector & Race.Parts.skull ) != 0 )
                text += " head";
            if( (vector & Race.Parts.arms) != 0 )
                text += " arms";
            if( (vector & Race.Parts.legs) != 0 )
                text += " legs";
            if( (vector & Race.Parts.heart) != 0 )
                text += " heart";
            if( (vector & Race.Parts.brains) != 0 )
                text += " brains";
            if( (vector & Race.Parts.guts) != 0 )
                text += " guts";
            if( (vector & Race.Parts.hands) != 0 )
                text += " hands";
            if( (vector & Race.Parts.feet) != 0 )
                text += " feet";
            if( (vector & Race.Parts.fingers) != 0 )
                text += " fingers";
            if( (vector & Race.Parts.ears) != 0 )
                text += " ears";
            if( (vector & Race.Parts.eyes) != 0 )
                text += " eyes";
            if( (vector & Race.Parts.tongue) != 0 )
                text += " tongue";
            if( (vector & Race.Parts.eyestalks) != 0 )
                text += " eyestalks";
            if( (vector & Race.Parts.tentacles) != 0 )
                text += " tentacles";
            if( (vector & Race.Parts.fins) != 0 )
                text += " fins";
            if( (vector & Race.Parts.wings) != 0 )
                text += " wings";
            if( (vector & Race.Parts.tail) != 0 )
                text += " tail";
            if( (vector & Race.Parts.claws) != 0 )
                text += " claws";
            if( (vector & Race.Parts.fangs) != 0 )
                text += " fangs";
            if( (vector & Race.Parts.horns) != 0 )
                text += " horns";
            if( (vector & Race.Parts.scales) != 0 )
                text += " scales";
            if( (vector & Race.Parts.tusks) != 0 )
                text += " tusks";
            if( (vector & Race.Parts.leaves) != 0 )
                text += " leaves";
            if( (vector & Race.Parts.branches) != 0 )
                text += " branches";
            if( (vector & Race.Parts.bark) != 0 )
                text += " bark";
            if( (vector & Race.Parts.trunk) != 0 )
                text += " trunk";

            if( text.Length < 1 )
            {
                return "none";
            }
            text.Remove( 0, 1 ); // Nuke leading space.
            return text;
        }

        /// <summary>
        /// Returns the ASCII name of a RISV damage type.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static string DamageTypeString( Race.DamageType vector )
        {
            string buf = String.Empty;

            if( (vector & Race.DamageType.fire) != 0 )
                buf += " fire";
            if( (vector & Race.DamageType.cold) != 0 )
                buf += " cold";
            if( (vector & Race.DamageType.electricity) != 0 )
                buf += " electricity";
            if( (vector & Race.DamageType.energy) != 0 )
                buf += " energy";
            if( (vector & Race.DamageType.acid) != 0 )
                buf += " acid";
            if( (vector & Race.DamageType.poison) != 0 )
                buf += " poison";
            if( (vector & Race.DamageType.charm) != 0 )
                buf += " charm";
            if( (vector & Race.DamageType.mental) != 0 )
                buf += " mental";
            if( (vector & Race.DamageType.whiteMana) != 0 )
                buf += " white_mana";
            if( (vector & Race.DamageType.blackMana) != 0 )
                buf += " black_mana";
            if( (vector & Race.DamageType.disease) != 0 )
                buf += " disease";
            if( (vector & Race.DamageType.drowning) != 0 )
                buf += " drowning";
            if( (vector & Race.DamageType.light) != 0 )
                buf += " light";
            if( (vector & Race.DamageType.sound) != 0 )
                buf += " sound";
            if( (vector & Race.DamageType.magic) != 0 )
                buf += " magic";
            if( (vector & Race.DamageType.nonmagic) != 0 )
                buf += " nonmagic";
            if( (vector & Race.DamageType.silver) != 0 )
                buf += " silver";
            if( (vector & Race.DamageType.iron) != 0 )
                buf += " iron";
            if( (vector & Race.DamageType.wood) != 0 )
                buf += " wood";
            if( (vector & Race.DamageType.weapon) != 0 )
                buf += " weapon";
            if( (vector & Race.DamageType.bash) != 0 )
                buf += " bash";
            if( (vector & Race.DamageType.pierce) != 0 )
                buf += " pierce";
            if( (vector & Race.DamageType.slash) != 0 )
                buf += " slash";

            if( buf.Length < 1 )
            {
                return "none";
            }
            buf.Remove( 0, 1 ); // Clear leading space.
            return buf;
        }

        /// <summary>
        /// Text to be printed when a magical wall disintegrates.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        public static string WallDecayString( int indexNumber )
        {
            string message;
            switch( indexNumber )
            {
                default:
                    message = "$p&n vanishes.";
                    break;
                case StaticObjects.OBJECT_NUMBER_WALL_IRON:
                    message = "$p&n melts to &+Lslag&n and vanishes.";
                    break;
                case StaticObjects.OBJECT_NUMBER_WALL_STONE:
                case StaticObjects.OBJECT_NUMBER_WALL_EARTH:
                    message = "$p&n crumbles to nothingness.";
                    break;
                case StaticObjects.OBJECT_NUMBER_WALL_FIRE:
                    message = "$p&n consumes itself with a blazing roar.";
                    break;
                case StaticObjects.OBJECT_NUMBER_WALL_ILLUSION:
                    message = "$p&n shudders into reality.";
                    break;
                case StaticObjects.OBJECT_NUMBER_WALL_FORCE:
                    message = "$p&n weakens away.";
                    break;
                case StaticObjects.OBJECT_NUMBER_LIGHTNING_CURTAIN:
                    message = "$p&n shorts itself to ground.";
                    break;
                case StaticObjects.OBJECT_NUMBER_WALL_SPARKS:
                    message = "$p&n cools and crumbles.";
                    break;
                case StaticObjects.OBJECT_NUMBER_WALL_MIST:
                    message = "$p&n disperses into a hazy cloud.";
                    break;
            }
            return message;
        }

        /// <summary>
        /// Translate area range values to ranges.
        /// </summary>
        /// <param name="pArea"></param>
        /// <returns></returns>
        public static string RangeString( Area pArea )
        {
            string text;

            if (pArea == null)
            {
                return "none";
            }

            if (pArea.MinRecommendedLevel == 0 && pArea.MaxRecommendedLevel == Limits.MAX_LEVEL)
            {
                text = " All ";
            }
            else
            {
                if (pArea.MinRecommendedLevel == 0 && pArea.MaxRecommendedLevel == 0)
                {
                    text = "None ";
                }
                else
                {
                    text = String.Format("{0} {1}", MUDString.PadInt(pArea.MinRecommendedLevel, 2),
                        MUDString.PadInt(pArea.MaxRecommendedLevel, 2));
                }
            }
            return text;
        }

        /// Gets race abilities as a string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RaceAbilityString( int[] value )
        {
            int count;
            string buf = String.Empty;

            for( count = 0; BitvectorFlagType.InnateFlags[ count ].BitvectorData; ++count )
            {
                if( Macros.IsSet( value[ ( BitvectorFlagType.InnateFlags[ count ].BitvectorData.Group ) ], BitvectorFlagType.InnateFlags[ count ].BitvectorData.Vector ) )
                {
                    buf += " ";
                    buf += BitvectorFlagType.InnateFlags[ count ].Name;
                }
            }

            if( buf.Length < 1 )
            {
                return "none";
            }
            buf.Remove( 0, 1 );

            return buf;
        }

        /// <summary>
        /// Renders coin data as a string.
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static string CoinString( int cost )
        {
            string buf = String.Empty;

            int coins = cost;
            int pla = coins / 1000;
            coins -= pla * 1000;
            int gol = coins / 100;
            coins -= gol * 100;
            int sil = coins / 10;
            coins -= sil * 10;
            int cop = coins;

            if( pla > 0 )
            {
                buf += " " + pla + "&+W platinum&n";
            }
            if( gol > 0 )
            {
                buf += " " + gol + "&+Y gold&n";
            }
            if( sil > 0 )
            {
                buf += " " + sil + "&n&+w silver&n";
            }
            if( cop > 0 )
            {
                buf += " " + cop + "&n&+y copper&n";
            }
            if( buf.Length > 0 )
            {
                return buf;
            }
            return " nothing";
        }
    }
}