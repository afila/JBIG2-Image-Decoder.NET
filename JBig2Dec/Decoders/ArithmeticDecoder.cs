using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBig2Dec
{
    class ArithmeticDecoder
    {
        private StreamReader reader;

        public ArithmeticDecoderStats genericRegionStats, refinementRegionStats;

        public ArithmeticDecoderStats iadhStats, iadwStats, iaexStats, iaaiStats, iadtStats, iaitStats, iafsStats, iadsStats, iardxStats, iardyStats, iardwStats, iardhStats, iariStats, iaidStats;

        int[] contextSize = new int[]{ 16, 13, 10, 10 }, referredToContextSize = new int[] { 13, 10 };

        int[] qeTable = { 0x56010000, 0x34010000, 0x18010000, 0x0AC10000, 0x05210000, 0x02210000, 0x56010000, 0x54010000, 0x48010000, 0x38010000, 0x30010000, 0x24010000, 0x1C010000, 0x16010000, 0x56010000, 0x54010000, 0x51010000, 0x48010000, 0x38010000, 0x34010000, 0x30010000, 0x28010000, 0x24010000, 0x22010000, 0x1C010000, 0x18010000, 0x16010000, 0x14010000, 0x12010000, 0x11010000, 0x0AC10000, 0x09C10000, 0x08A10000, 0x05210000, 0x04410000, 0x02A10000, 0x02210000, 0x01410000, 0x01110000, 0x00850000, 0x00490000, 0x00250000, 0x00150000, 0x00090000, 0x00050000, 0x00010000, 0x56010000 };
        int[] nmpsTable = { 1, 2, 3, 4, 5, 38, 7, 8, 9, 10, 11, 12, 13, 29, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 45, 46 };
        int[] switchTable = { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] nlpsTable = { 1, 6, 9, 12, 29, 33, 6, 14, 14, 14, 17, 18, 20, 21, 14, 14, 15, 16, 17, 18, 19, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 46 };

	    long buffer0, buffer1;
	    long c, a;
	    long previous;
	
	    int counter;


        public ArithmeticDecoder(StreamReader reader)
        {
            this.reader = reader;

            genericRegionStats = new ArithmeticDecoderStats(1 << 1);
            refinementRegionStats = new ArithmeticDecoderStats(1 << 1);

            iadhStats = new ArithmeticDecoderStats(1 << 9);
            iadwStats = new ArithmeticDecoderStats(1 << 9);
            iaexStats = new ArithmeticDecoderStats(1 << 9);
            iaaiStats = new ArithmeticDecoderStats(1 << 9);
            iadtStats = new ArithmeticDecoderStats(1 << 9);
            iaitStats = new ArithmeticDecoderStats(1 << 9);
            iafsStats = new ArithmeticDecoderStats(1 << 9);
            iadsStats = new ArithmeticDecoderStats(1 << 9);
            iardxStats = new ArithmeticDecoderStats(1 << 9);
            iardyStats = new ArithmeticDecoderStats(1 << 9);
            iardwStats = new ArithmeticDecoderStats(1 << 9);
            iardhStats = new ArithmeticDecoderStats(1 << 9);
            iariStats = new ArithmeticDecoderStats(1 << 9);
            iaidStats = new ArithmeticDecoderStats(1 << 1);
        }

        public void start()  
        {
		    buffer0 = reader.readByte();
		    buffer1 = reader.readByte();

		    c = BinaryOperation.bit32ShiftL((buffer0 ^ 0xff), 16);
		    readByte();
		    c = BinaryOperation.bit32ShiftL(c, 7);
		    counter -= 7;
		    a = 0x80000000L;
	    }
        
        private void readByte() 
        {
		    if (buffer0 == 0xff)
            {
			    if (buffer1 > 0x8f) 
                    counter = 8;
                else 
                {
				    buffer0 = buffer1;
				    buffer1 = reader.readByte();
				    c = c + 0xfe00 - (BinaryOperation.bit32ShiftL(buffer0, 9));
				    counter = 7;
			    }
		    } 
            else 
            {
			    buffer0 = buffer1;
			    buffer1 = reader.readByte();
			    c = c + 0xff00 - (BinaryOperation.bit32ShiftL(buffer0, 8));
			    counter = 8;
		    }
	    }
        
        public void resetGenericStats(int template, ArithmeticDecoderStats previousStats)
        {
            int size = contextSize[template];

            if (previousStats != null && previousStats.getContextSize() == size)
            {
                if (genericRegionStats.getContextSize() == size)
                    genericRegionStats.overwrite(previousStats);
                else
                    genericRegionStats = previousStats.copy();
            }
            else
            {
                if (genericRegionStats.getContextSize() == size)
                    genericRegionStats.reset();
                else
                    genericRegionStats = new ArithmeticDecoderStats(1 << size);
            }
        }


        public int decodeBit(long context, ArithmeticDecoderStats stats)
        {
            int iCX = BinaryOperation.bit8Shift(stats.getContextCodingTableValue((int)context), 1, BinaryOperation.RIGHT_SHIFT);
		    int mpsCX = stats.getContextCodingTableValue((int) context) & 1;
		    int qe = qeTable[iCX];

		    a -= qe;

		    int bit;
		    if (c < a) {
			    if ( (a & 0x80000000) != 0) bit = mpsCX;
                else {
				    if (a < qe) {
					    bit = 1 - mpsCX;
					    if (switchTable[iCX] != 0) 
						    stats.setContextCodingTableValue((int) context, (nlpsTable[iCX] << 1) | (1 - mpsCX));
                        else 
						    stats.setContextCodingTableValue((int) context, (nlpsTable[iCX] << 1) | mpsCX);
				    } 
                    else {
					    bit = mpsCX;
					    stats.setContextCodingTableValue((int) context, (nmpsTable[iCX] << 1) | mpsCX);
				    }

				    do  {
					    if (counter == 0)   readByte();
					    a = BinaryOperation.bit32ShiftL(a, 1);
					    c = BinaryOperation.bit32ShiftL(c, 1);
					    counter--;
				    } while ( (a & 0x80000000) == 0);
			    }
		    } 
            else {
			        c -= a;

			        if (a < qe) {
				        bit = mpsCX;
				        stats.setContextCodingTableValue((int) context, (nmpsTable[iCX] << 1) | mpsCX);
			        } 
                    else {
				            bit = 1 - mpsCX;
				            if (switchTable[iCX] != 0) 
					            stats.setContextCodingTableValue((int) context, (nlpsTable[iCX] << 1) | (1 - mpsCX));
                            else 
					            stats.setContextCodingTableValue((int) context, (nlpsTable[iCX] << 1) | mpsCX);
			        }
			        a = qe;

			        do {
				        if (counter == 0)   readByte();
				        a = BinaryOperation.bit32ShiftL(a, 1);
				        c = BinaryOperation.bit32ShiftL(c, 1);
				        counter--;
			        } while ( (a & 0x80000000) == 0 );
		    }

		    return bit;
        }

    }
}
