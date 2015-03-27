using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace JBig2Dec
{
    sealed class FastBitSet
    {
        public long[] w; //?????????????????????
        static int BITS = IntPtr.Size * 8;

        //static final int pot = (Long.SIZE == 32) ? 5 : (Long.SIZE == 16) ? 4 : (Long.SIZE == 64) ? 6 : 7; JAVA
        public static readonly int pot = (BITS == 32) ? 5 : (BITS == 16) ? 4 : (BITS == 64) ? 6 : 7;
        
        //static final int mask = (int) ((-1L) >>> (Long.SIZE-pot)); JAVA
        public static readonly int mask = (int)( ((BITS == 64)? UInt64.MaxValue: UInt32.MaxValue)   >> (BITS - pot)); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

       

		int length;

        public FastBitSet(int length)
        {
            this.length = length;
            int wcount = length / BITS;
            if (length % BITS != 0) wcount++;
            w = new long[wcount];
        }


        public void setAll(bool value)
        {
            if (value)
                for (int i = 0; i < w.Length; i++)
                    w[i] = -1L;
            else
                for (int i = 0; i < w.Length; i++)
                    w[i] = 0;
        }

        public bool get(int index) 
        {
			int windex = (int)(((uint)(index)) >> pot);
			return ((w[windex] & (1L<<index)) != 0);
		}		

        public void set(int index, bool value)
        {
            if (value) set(index);
            else clear(index);
        }

        public void set(int index) {
			int windex = (int)(((uint)index) >> pot);
			w[windex] |= (1L<<index);
		}

        public void clear(int index) {
			int windex = (int)(((uint)index) >> pot);
			w[windex] &= ~(1L<<index);
		}

        public void or(int startindex, FastBitSet set, int setStartIndex, int length) {
			int shift = startindex - setStartIndex;
			long k = set.w[setStartIndex >> pot];
			//Cyclic shift
			k = (k << shift) | (((uint)k) >> (BITS - shift));
			if ((setStartIndex & mask) + length <= BITS) {
				setStartIndex += shift;
				for (int i=0; i<length; i++) {
					w[((uint)startindex) >> pot] |= k & (1L << setStartIndex);
					setStartIndex++;
					startindex++;
				}				
			}
			else{
				for (int i=0; i<length; i++) {
					if ((setStartIndex & mask) == 0){ 
						k = set.w[(setStartIndex) >> pot];
						k = (k << shift) | (((uint)k) >> (BITS - shift));
					}
					w[((uint)startindex) >> pot] |= k & (1L << (setStartIndex+shift));
					setStartIndex++;
					startindex++;
				}
			}
		}
    }
}
