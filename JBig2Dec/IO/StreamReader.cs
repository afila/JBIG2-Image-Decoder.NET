using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBig2Dec
{
    public class StreamReader
    {
        private byte[] data;
        private int bitPointer = 7;
        private int bytePointer = 0;

        //Constructor
        public StreamReader(byte[] data) {
            this.data = data;
        }

        public byte readByte() {
            byte bite = (byte)(data[bytePointer++] & 255);

            return bite;
        }

        public void readByte(byte[] buf)
        {
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = (byte)(data[bytePointer++] & 255);
            }
        }

        public bool isFinished() {
            return bytePointer == data.Length;
        }

        public void movePointer(int ammount)
        {
            bytePointer += ammount;
        }
    }
}
