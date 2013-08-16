using System;

namespace MUDEngine
{
    // This random number generator class is ABSOLUTELY NOT thread safe.
    //
    // In order to use it with multithreaded code you will have to create your own locking system.
    /// <summary>
    /// Singleton class representing a random number generator.
    /// </summary>
    public class RandomNumber
    {
        public static RandomNumberEngine _engine = RandomNumberEngine.RANDOM_MERSENNE_TWISTER;
        private static R250_521 _r250521Engine = null;
        private static MT _mtEngine = null;
        private static Random _netEngine = null;

        /// <summary>
        /// Represents one of the random number engines supported by the RandomNumber class.
        /// </summary>
        public enum RandomNumberEngine
        {
            RANDOM_DOTNET_BUILTIN,
            RANDOM_R250_521,
            RANDOM_MERSENNE_TWISTER
        }

        /// <summary>
        /// Initializes a random number engine based on the selected type.
        /// 
        /// To switch engines, a new object will have to be created.  In practice
        /// this can be handled automatically just by changing the engine type enum.
        /// </summary>
        /// <param name="engine"></param>
        public RandomNumber(RandomNumberEngine engine)
        {
            switch( engine )
            {
                case RandomNumberEngine.RANDOM_DOTNET_BUILTIN:
                    _netEngine = new Random();
                    break;
                case RandomNumberEngine.RANDOM_R250_521:
                    _r250521Engine = new R250_521();
                    break;
                case RandomNumberEngine.RANDOM_MERSENNE_TWISTER:
                    _mtEngine = new MT();
                    break;
                default:
                    throw new IndexOutOfRangeException( "RandomNumber(): Bad random number generator setting");
            }
            _engine = engine;
        }

        /// <summary>
        /// Gets a random number from the active engine.  Automatically creates
        /// the engine if it has not been intialized yet.
        /// </summary>
        /// <returns></returns>
        public static int Get()
        {
            switch (_engine)
            {
                case RandomNumberEngine.RANDOM_DOTNET_BUILTIN:
                    if (_netEngine == null)
                    {
                        // Init random number generator.
                        Log.Trace("Initializing random number generator using the .NET built-in random functions.");
                        new RandomNumber(RandomNumberEngine.RANDOM_DOTNET_BUILTIN);
                    }
                    return _netEngine.Next();
                case RandomNumberEngine.RANDOM_MERSENNE_TWISTER:
                    if (_mtEngine == null)
                    {
                        // Init random number generator.
                        Log.Trace("Initializing random number generator using the Mersenne Twister.");
                        new RandomNumber(RandomNumberEngine.RANDOM_MERSENNE_TWISTER);
                    }
                    return (int)_mtEngine.Random();
                case RandomNumberEngine.RANDOM_R250_521:
                    if (_r250521Engine == null)
                    {
                        // Init random number generator.
                        Log.Trace("Initializing random number generator using the R250_521 algorithm.");
                        new RandomNumber(RandomNumberEngine.RANDOM_R250_521);
                    }
                    return (int)_r250521Engine.Random();
                default:
                    throw new IndexOutOfRangeException("Invalid number generation method specified.");
            }
        }

        /// <summary>
        /// R250/521
        ///
        /// First described in 1981, R250 is a relatively old random number generator, but it's still one of the best.
        /// It was invented/discovered by Kirkpatrick and Stoll, in the article A Very Fast Shift-Register Sequence
        /// Random Number Generator, J. Computational Physics, vol 40, pp. 517-526.
        ///
        /// R250 is what's known as a generalized feedback shift register, or GFSR. GFSRs are determined by two
        /// parameters, a length and an offset. R250 is actually GFSR(250,103), indicating a length of 250 and an
        /// offset of 103. R250 has a period of almost 2^250.
        ///
        /// See http://www.informs-cs.org/wsc97papers/0127.PDF
        /// </summary>
        public sealed class R250_521
        {
            private uint r250_index;
            private uint r521_index;
            private readonly uint[] r250_buffer = new uint[250];
            private readonly uint[] r521_buffer = new uint[521];

            public R250_521() 
            {
                Random r = new Random();
                uint i = 521;
                uint mask1 = 1;
                uint mask2 = 0xFFFFFFFF;
        	
                while (i-- > 250) 
                {
                    r521_buffer[i] = (uint)r.Next();
                }
                while (i-- > 31)
                {
                    r250_buffer[i] = (uint)r.Next();
                    r521_buffer[i] = (uint)r.Next();
                }
            
                /*
                Establish linear independence of the _bitvector columns
                by setting the diagonal bits and clearing all bits above
                */
                while (i-- > 0) 
                {
                    r250_buffer[i] = ((uint)r.Next() | mask1) & mask2;
                    r521_buffer[i] = ((uint)r.Next() | mask1) & mask2;
                    mask2 ^= mask1;
                    mask1 >>= 1;
                }
                r250_buffer[0] = mask1;
                r521_buffer[0] = mask2;
                r250_index = 0;
                r521_index = 0;
            }
        	
            /// <summary>
            /// Generates a random uint.
            /// </summary>
            /// <returns></returns>
            public uint Random() 
            {
                uint i1 = r250_index;
                uint i2 = r521_index;
            
                uint j1 = i1 - (250-103);
                if (j1 < 0)
                    j1 = i1 + 103;
                uint j2 = i2 - (521-168);
                if (j2 < 0)
                    j2 = i2 + 168;
            
                uint r = r250_buffer[j1] ^ r250_buffer[i1];
                r250_buffer[i1] = r;
                uint s = r521_buffer[j2] ^ r521_buffer[i2];
                r521_buffer[i2] = s;
            
                i1 = (i1 != 249) ? (i1 + 1) : 0;
                r250_index = i1;
                i2 = (i2 != 521) ? (i2 + 1) : 0;
                r521_index = i2;
                
                return r ^ s;
            }
        }

        /// <summary>
        /// The Mersenne Twister
        ///
        /// The Mersenne Twister is a new random number generator, invented/discovered in 1996 by 
        /// Matsumora and Nishimura.
        ///
        /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html
        /// </summary>
        public sealed class MT
        {
            private uint mt_index;
            private readonly uint[] mt_buffer = new uint[624];

            public MT() 
            {
                Random r = new Random();
                for (uint i = 0; i < 624; i++)
                    mt_buffer[i] = (uint)r.Next();
                mt_index = 0;
            }

            public uint Random()
            {
                if (mt_index == 624)
                {
                    mt_index = 0;
                    uint i = 0;
                    uint s;
                    for (; i < 624 - 397; i++) {
                        s = (mt_buffer[i] & 0x80000000) | (mt_buffer[i+1] & 0x7FFFFFFF);
                        mt_buffer[i] = mt_buffer[i + 397] ^ (s >> 1) ^ ((s & 1) * 0x9908B0DF);
                    }
                    for (; i < 623; i++) {
                        s = (mt_buffer[i] & 0x80000000) | (mt_buffer[i+1] & 0x7FFFFFFF);
                        mt_buffer[i] = mt_buffer[i - (624 - 397)] ^ (s >> 1) ^ ((s & 1) * 0x9908B0DF);
                    }
                
                    s = (mt_buffer[623] & 0x80000000) | (mt_buffer[0] & 0x7FFFFFFF);
                    mt_buffer[623] = mt_buffer[396] ^ (s >> 1) ^ ((s & 1) * 0x9908B0DF);
                }
                return mt_buffer[mt_index++];
            }
        }
    }
}
