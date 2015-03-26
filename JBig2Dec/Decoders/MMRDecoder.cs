using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    public class MMRDecoder
    {
        private StreamReader reader;
        private long bufferLength = 0, buffer = 0, noOfBytesRead = 0;

        public MMRDecoder(StreamReader reader)
        {
            this.reader = reader;
        }

        public void reset()
        {
            bufferLength = 0;
            noOfBytesRead = 0;
            buffer = 0;
        }

    //    public int get2DCode() {
    //        int[] tuple;

    //        if (bufferLength == 0) {
    //            buffer = (reader.readByte() & 0xff);

    //            bufferLength = 8;

    //            noOfBytesRead++;

    //            int lookup = (int)((buffer >> 1) & 0x7f);

    //            tuple = twoDimensionalTable1[lookup];
    //        } else if (bufferLength == 8) {
    //                    int lookup = (int) ((BitConverter.bit32ShiftR(buffer, 1)) & 0x7f);
    //                    tuple = twoDimensionalTable1[lookup];
    //                    } else {
    //                            int lookup = (int) ((BitConverter.bit32ShiftL(buffer, (int) (7 - bufferLength))) & 0x7f);

    //                            tuple = twoDimensionalTable1[lookup];
    //                            if (tuple[0] < 0 || tuple[0] > (int) bufferLength) {
    //                                int right = (reader.readByte() & 0xff);

    //                                long left = (BitConverter.bit32ShiftL(buffer, 8));

    //                                buffer = left | right;
    //                                bufferLength += 8;
    //                                noOfBytesRead++;

    //                                int look = (int) (BitConverter.bit32ShiftR(buffer, (int) (bufferLength - 7)) & 0x7f);

    //                                tuple = twoDimensionalTable1[look];
    //                    }
    //        }
    //        if (tuple[0] < 0) {
    //            if(JBIG2StreamDecoder.debug)    Debug.WriteLine("Bad two dim code in JBIG2 MMR stream");
			
    //            return 0;
    //        }
    //        bufferLength -= tuple[0];
		
    //        return tuple[1];
    //    }

       
    }
}
