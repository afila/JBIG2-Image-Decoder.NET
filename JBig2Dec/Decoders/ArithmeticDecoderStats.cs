using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBig2Dec
{
    class ArithmeticDecoderStats
    {
        private int contextSize;
        private int[] codingContextTable;

        public ArithmeticDecoderStats(int contextSize)
        {
            this.contextSize = contextSize;
            this.codingContextTable = new int[contextSize];

            //reset();
        }


        public int getContextSize()
        {
            return contextSize;
        }

        public void overwrite(ArithmeticDecoderStats stats)
        {
            //System.arraycopy(stats.codingContextTable, 0, codingContextTable, 0, contextSize);
            Buffer.BlockCopy(stats.codingContextTable, 0, codingContextTable, 0, contextSize);
        }

        public ArithmeticDecoderStats copy()
        {
            ArithmeticDecoderStats stats = new ArithmeticDecoderStats(contextSize);

            //System.arraycopy(codingContextTable, 0, stats.codingContextTable, 0, contextSize);
            Buffer.BlockCopy(codingContextTable, 0, stats.codingContextTable, 0, contextSize);

            return stats;
        }

        public void reset()
        {
            //for (int i = 0; i < contextSize; i++) {
            //    codingContextTable[i] = 0;
            //}
            Array.Clear(codingContextTable, 0, codingContextTable.Length);
        }

        public int getContextCodingTableValue(int index)
        {
            return codingContextTable[index];
        }

        public void setContextCodingTableValue(int index, int value)
        {
            codingContextTable[index] = value;
        }
    }
}
