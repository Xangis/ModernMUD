using System;

namespace MUDEngine
{
    /// <summary>
    /// Nouns. Used for randomly-generated sentences.
    /// </summary>
    public class NounType
    {
        public NounType(string classname, bool animatedsubj)
        {
            Name = classname;
            Animate = animatedsubj;
        }

        public string Name { get; set; } // The noun.
        public bool Animate { get; set; }  // Can the noun be a subject?

        public static NounType[] Table =   
        {
            new NounType( "rat",        true  ),  // 0  
            new NounType( "cat",        true  ),  // 1  
            new NounType( "elf",        true  ),  // 2  
            new NounType( "dwarf",      true  ),  // 3  
            new NounType( "centaur",    true  ),  // 4  
            new NounType( "ogre",       true  ),  // 5  
            new NounType( "orc",        true  ),  // 6  
            new NounType( "troll",      true  ),  // 7  
            new NounType( "aquatic elf",true  ),  // 8  
            new NounType( "saurial",    true  ),  // 9  
            new NounType( "thri-kreen", true  ),  // 10 
            new NounType( "dog",        true  ),  // 11 
            new NounType( "dragon",     true  ),  // 12 
            new NounType( "cat",        true  ),  // 13 
            new NounType( "pig",        true  ),  // 14 
            new NounType( "goblin",     true  ),  // 15 
            new NounType( "god",        true  ),  // 16 
            new NounType( "halfling",   true  ),  // 17 
            new NounType( "githyanki",  true  ),  // 18 
            new NounType( "drow",       true  ),  // 19 
            new NounType( "kobold",     true  ),  // 20 
            new NounType( "gnome",      true  ),  // 21 
            new NounType( "animal",     true  ),  // 22 
            new NounType( "duergar",    true  ),  // 23 
            new NounType( "githzerai",  true  ),  // 24 
            new NounType( "flind",      true  ),  // 25 
            new NounType( "rakshasa",   true  ),  // 26 
            new NounType( "minotaur",   true  ),  // 27 
            new NounType( "gnoll",      true  ),  // 28 
            new NounType( "barbarian",  true  ),  // 29 
            new NounType( "cheese",     false ),  // 30 
            new NounType( "book",       false ),  // 31 
            new NounType( "horse",      true  ),
            new NounType( "cow",        true  ),
            new NounType( "table",      false ),
            new NounType( "chair",      false ),
            new NounType( "knife",      false ),
            new NounType( "fork",       false ),
            new NounType( "truncheon",  false ),
            new NounType( "sandwich",   false ),
            new NounType( "rock",       false ),
            new NounType( "egg",        false ),
            new NounType( "pickle",     false ),
        };

        /// <summary>
        /// Generates a random sentence.
        /// </summary>
        /// <returns></returns>
        public static string RandomSentence()
        {
            string text;

            NounType noun = Table[MUDMath.NumberRange(0, Table.Length - 1)];
            VerbType verb = VerbType.Table[MUDMath.NumberRange(0, VerbType.Table.Length - 1)];

            while (noun.Animate == false)
                noun = Table[MUDMath.NumberRange(0, Table.Length - 1)];

            if (MUDMath.NumberPercent() > 50)
            {
                if (MUDString.IsVowel(noun.Name))
                {
                    text = String.Format("An ");
                }
                else
                {
                    text = String.Format("A ");
                }
            }
            else
            {
                text = String.Format("The ");
            }

            text += noun.Name;
            text += " ";
            text += verb.Name;
            if (verb.DirectObject != VerbType.DirectObjectType.none)
            {
                noun = Table[MUDMath.NumberRange(0, Table.Length - 1)];

                if (MUDString.IsVowel(noun.Name))
                {
                    text += " an ";
                }
                else
                {
                    text += " a ";
                }

                text += noun.Name;
            }

            if (MUDMath.NumberPercent() > 80)
            {
                text += "!";
            }
            else
            {
                text += ".";
            }

            return text;
        }
    }
}
