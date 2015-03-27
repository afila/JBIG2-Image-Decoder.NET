using System;

namespace JBig2Dec
{
   class BinaryOperation 
   {

	public const int LEFT_SHIFT = 0;
	public const int RIGHT_SHIFT = 1;
	
	public const long LONGMASK = 0xffffffffL; // 1111 1111 1111 1111 1111 1111 1111 1111
	public const int INTMASK = 0xff; // 1111 1111

	public static int getInt32( byte[] number) {
	    return (number[0] << 24) | (number[1] << 16) | (number[2] << 8) | number[3];
	}

    public static int getInt16(byte[] number) {
        return (number[0] << 8) | number[1];
    }

    public static long bit32ShiftL(long number, int shift) {
        //return (number << shift) & LONGMASK;
        return number << shift;
    }

    public static long bit32ShiftR(long number, int shift) {
        //return (number >> shift) & LONGMASK;
        return number >> shift;
    }

    /*public final static long bit32Shift(long number, int shift, int direction) {
        if (direction == LEFT_SHIFT)
            number <<= shift;
        else
            number >>= shift;

        return (number & LONGMASK);
    }*/

    public static int bit8Shift(int number, int shift, int direction) {
        if (direction == LEFT_SHIFT)
            number <<= shift;
        else
            number >>= shift;

        return (number & INTMASK);
    }
}

}
