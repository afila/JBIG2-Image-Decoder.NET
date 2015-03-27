using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBig2Dec
{
    class HuffmanDecoder
    {
        //public static int jbig2HuffmanLOW = (int)0xfffffffd;
        //public static int jbig2HuffmanOOB = (int)0xfffffffe;
        //public static int jbig2HuffmanEOT = (int)0xffffffff;

        private StreamReader reader;

        public HuffmanDecoder(StreamReader reader)
        {
            this.reader = reader;
        }

    }
}
