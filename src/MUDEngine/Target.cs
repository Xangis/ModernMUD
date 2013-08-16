using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Target is used for spellcasting, skills, traps, etc.  It allows
    /// us to store one of object, victim, room, or string and determine
    /// what the type should be.
    /// </summary>
    public class Target
    {
        readonly Object _obj;
        readonly ObjTemplate _objTemplate;
        readonly CharData _victim;
        readonly Room _room;
        readonly Affect _affect;
        readonly string _str;
        readonly TargetType _type;
        readonly Spell _spell;
        public TargetType Type
        {
            get
            {
                return _type;
            }
        }
        
        public enum TargetType
        {
            chardata,
            object_target,
            room,
            affect,
            string_target,
            spell,
            object_template
        }

        // Constructors let us take one of many types and automatically set
        // the type to the correct value.
        public Target( Object ob )
        {
            _obj = ob;
            _type = TargetType.object_target;
        }
        public Target( CharData ch )
        {
            _victim = ch;
            _type = TargetType.chardata;
        }
        public Target( Room rm )
        {
            _room = rm;
            _type = TargetType.room;
        }
        public Target( Affect aff )
        {
            _affect = aff;
            _type = TargetType.affect;
        }
        public Target( string st )
        {
            _str = st;
            _type = TargetType.string_target;
        }
        public Target(Spell sp)
        {
            _spell = sp;
            _type = TargetType.spell;
        }
        public Target(ObjTemplate obj)
        {
            _objTemplate = obj;
            _type = TargetType.object_template;
        }
        // These conversions let us pass any of object, chardata, affect, room, or string as if it were a _targetType.
        public static implicit operator Target( Object ob )
        {
            return new Target( ob );
        }
        public static implicit operator Target(ObjTemplate obj)
        {
            return new Target( obj );
        }
        public static implicit operator Target( CharData cd )
        {
            return new Target( cd );
        }
        public static implicit operator Target( Affect af )
        {
            return new Target( af );
        }
        public static implicit operator Target( Room rm )
        {
            return new Target( rm );
        }
        public static implicit operator Target( string st )
        {
            return new Target( st );
        }
        public static implicit operator Target(Spell sp)
        {
            return new Target(sp);
        }
        // We need to add explicit cast operators that allow us to convert
        // the _targetType to another type.
        //
        // Ideally this would have some sort of built-in type safety that would
        // allow us to gracefully recover from something like Object o = (Object)Target
        // when the type is Affect or string.
        static public explicit operator CharData( Target t )
        {
            if( t == null )
                return null;
            if( t._type == TargetType.chardata )
            {
                return t._victim;
            }
            return null;
            //FormatException e = new FormatException( "Invalid type conversion to CharData for class Target.  Type is " + t._type.ToString() );
            //throw e;
        }
        static public explicit operator Room( Target t )
        {
            if( t == null )
                return null;
            if( t._type == TargetType.room )
            {
                return t._room;
            }
            return null;
            //FormatException e = new FormatException( "Invalid type conversion to RoomIndex for class Target.  Type is " + t._type.ToString() );
            //throw e;
        }
        static public explicit operator string( Target t )
        {
            if( t == null )
                return null;
            if( t._type == TargetType.string_target )
            {
                return t._str;
            }
            return null;
            //FormatException e = new FormatException( "Invalid type conversion to string for class Target.  Type is " + t._type.ToString() );
            //throw e;
        }
        static public explicit operator Affect( Target t )
        {
            if( t == null )
                return null;
            if( t._type == TargetType.affect )
            {
                return t._affect;
            }
            return null;
            //FormatException e = new FormatException( "Invalid type conversion to Affect for class Target.  Type is " + t._type.ToString() );
            //throw e;
        }
        static public explicit operator Object( Target t )
        {
            if( t == null )
                return null;
            if( t._type == TargetType.object_target )
            {
                return t._obj;
            }
            return null;
            //FormatException e = new FormatException( "Invalid type conversion to Object for class Target.  Type is " + t._type.ToString() );
            //throw e;
        }
        static public explicit operator ObjTemplate(Target t)
        {
            if (t == null)
                return null;
            if (t._type == TargetType.object_template)
            {
                return t._objTemplate;
            }
            return null;
            //FormatException e = new FormatException( "Invalid type conversion to Object for class Target.  Type is " + t._type.ToString() );
            //throw e;
        }
        static public explicit operator Spell( Target t )
        {
            if (t == null)
                return null;
            if (t._type == TargetType.spell)
            {
                return t._spell;
            }
            return null;
        }
        public bool IsObject()
        {
            if( _type == TargetType.object_target && _obj != null )
            {
                return true;
            }
            return false;
        }
        public bool IsCharData()
        {
            if( _type == TargetType.chardata && _victim != null )
            {
                return true;
            }
            return false;
        }
    }

}