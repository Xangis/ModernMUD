using System;
using System.Xml.Serialization;

namespace ModernMUD
{
    /// <summary>
    /// Represents a bitvector that may span multiple integers.
    /// </summary>
    [Serializable]
    public class Bitvector
    {
        private int _group;
        private int _vector;

        /// <summary>
        /// Value-based constructor.
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="vect"></param>
        public Bitvector(int grp, int vect)
        {
            _group = grp;
            _vector = vect;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Bitvector()
        {
            _group = 0;
            _vector = 0;
        }

        /// <summary>
        /// The group number of the bitvector.  Represents the index of the vector
        /// in an array of integers.
        /// </summary>
        public int Group
        {
            get { return _group; }
            set { _group = value; }
        }

        /// <summary>
        /// The bit value(s) for the vector.
        /// </summary>
        public int Vector
        {
            get { return _vector; }
            set { _vector = value; }
        }
        
        /// <summary>
        /// Displays bitvector information as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Group " + Group.ToString() + ", Vector " + Vector.ToString();
        }

        // Bit definitions.
        /// <summary>
        /// 1
        /// </summary>
        public const int BV00 = ( 1 << 0 );
        /// <summary>
        /// 2
        /// </summary>
        public const int BV01 = ( 1 << 1 );
        /// <summary>
        /// 4
        /// </summary>
        public const int BV02 = ( 1 << 2 );
        /// <summary>
        /// 8
        /// </summary>
        public const int BV03 = ( 1 << 3 );
        /// <summary>
        /// 16
        /// </summary>
        public const int BV04 = ( 1 << 4 );
        /// <summary>
        /// 32
        /// </summary>
        public const int BV05 = ( 1 << 5 );
        /// <summary>
        /// 64
        /// </summary>
        public const int BV06 = ( 1 << 6 );
        /// <summary>
        /// 128
        /// </summary>
        public const int BV07 = ( 1 << 7 );
        /// <summary>
        /// 256
        /// </summary>
        public const int BV08 = ( 1 << 8 );
        /// <summary>
        /// 512
        /// </summary>
        public const int BV09 = ( 1 << 9 );
        /// <summary>
        /// 1024
        /// </summary>
        public const int BV10 = ( 1 << 10 );
        /// <summary>
        /// 2048
        /// </summary>
        public const int BV11 = ( 1 << 11 );
        /// <summary>
        /// 4096
        /// </summary>
        public const int BV12 = ( 1 << 12 );
        /// <summary>
        /// 8192
        /// </summary>
        public const int BV13 = ( 1 << 13 );
        /// <summary>
        /// 16384
        /// </summary>
        public const int BV14 = ( 1 << 14 );
        /// <summary>
        /// 32768
        /// </summary>
        public const int BV15 = ( 1 << 15 );
        /// <summary>
        /// 65536
        /// </summary>
        public const int BV16 = ( 1 << 16 );
        /// <summary>
        /// 131072
        /// </summary>
        public const int BV17 = ( 1 << 17 );
        /// <summary>
        /// 262144
        /// </summary>
        public const int BV18 = ( 1 << 18 );
        /// <summary>
        /// 524288
        /// </summary>
        public const int BV19 = ( 1 << 19 );
        /// <summary>
        /// 1048576
        /// </summary>
        public const int BV20 = ( 1 << 20 );
        /// <summary>
        /// 2097152
        /// </summary>
        public const int BV21 = ( 1 << 21 );
        /// <summary>
        /// 4194304
        /// </summary>
        public const int BV22 = ( 1 << 22 );
        /// <summary>
        /// 8388608
        /// </summary>
        public const int BV23 = ( 1 << 23 );
        /// <summary>
        /// 16777216
        /// </summary>
        public const int BV24 = ( 1 << 24 );
        /// <summary>
        /// 33554432
        /// </summary>
        public const int BV25 = ( 1 << 25 );
        /// <summary>
        /// 67108864
        /// </summary>
        public const int BV26 = ( 1 << 26 );
        /// <summary>
        /// 134217728 
        /// </summary>
        public const int BV27 = ( 1 << 27 );
        /// <summary>
        /// 268435456
        /// </summary>
        public const int BV28 = ( 1 << 28 );
        /// <summary>
        /// 536870912
        /// </summary>
        public const int BV29 = ( 1 << 29 );
        /// <summary>
        /// 1073741824
        /// </summary>
        public const int BV30 = ( 1 << 30 );

        /// <summary>
        /// Allows checking a bitvector for null using the NOT (!) operator.
        /// </summary>
        /// <param name="bv"></param>
        /// <returns></returns>
        public static implicit operator bool( Bitvector bv )
        {
            if( bv == null )
                return false;
            return true;
        }
    }
}
