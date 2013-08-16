namespace MUDEngine
{
    /// <summary>
    /// Represents a verb.  Used for auto-generated sentences.
    /// </summary>
    public class VerbType
    {
        public string Name { get; set; } // The actual word.
        public DirectObjectType DirectObject { get; set; }  // Direct object type.
        public IndirectObjectType IndirectObject { get; set; } // Indirect object type.

        public enum DirectObjectType
        {
            none,
            any,
            animate
        }

        public enum IndirectObjectType
        {
            none,
            any,
            animate
        }

        public VerbType(string classname, DirectObjectType directobjtype, IndirectObjectType indirectobjtype)
        {
            Name = classname;
            DirectObject = directobjtype;
            IndirectObject = indirectobjtype;
        }

        public static VerbType[] Table =  
        {
            new VerbType( "died",        DirectObjectType.none,  IndirectObjectType.none ),  // 0
            new VerbType( "smacked",     DirectObjectType.any, IndirectObjectType.none ),  // 1
            new VerbType( "ate",         DirectObjectType.any, IndirectObjectType.none ),  // 2
            new VerbType( "left",        DirectObjectType.none,  IndirectObjectType.none ),  // 3
            new VerbType( "ran",         DirectObjectType.none,  IndirectObjectType.none ),  // 4
            new VerbType( "bit",         DirectObjectType.any, IndirectObjectType.none ),  // 5
            new VerbType( "kicked",      DirectObjectType.any, IndirectObjectType.none ),
            new VerbType( "slept",       DirectObjectType.none,  IndirectObjectType.none ),
            new VerbType( "stole",       DirectObjectType.any, IndirectObjectType.none ),
            new VerbType( "threw",       DirectObjectType.any, IndirectObjectType.none ),
            new VerbType( "licked",      DirectObjectType.any, IndirectObjectType.none ), 
            new VerbType( "smashed",     DirectObjectType.any, IndirectObjectType.none ), 
            new VerbType( "wept",        DirectObjectType.none,  IndirectObjectType.none ), 
            new VerbType( "slept",       DirectObjectType.none,  IndirectObjectType.none ),
            new VerbType( "fled",        DirectObjectType.none,  IndirectObjectType.none ),
        };
    };
}
