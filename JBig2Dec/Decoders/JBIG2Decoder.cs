using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JBig2Dec;

namespace JBig2Dec
{
    class JBIG2Decoder
    {
    
        private JBIG2StreamDecoder streamDecoder;

	    /**
	     * Constructor
	     */
	    public JBIG2Decoder() {
		    streamDecoder = new JBIG2StreamDecoder();
	    }

        /// <summary>
        /// Decodes a JBIG2 image from a String path
        /// </summary>
        /// <param name="file">Must be the full path to the image</param>
	    public void decodeJBIG2(String file)  {
		    decodeJBIG2(new FileStream(file, FileMode.Open));
	    }

        /**
	 * Decodes a JBIG2 image from an InputStream
	 * @param inputStream InputStream
	 * @throws IOException
	 * @throws JBIG2Exception
	 */
	public void decodeJBIG2(FileStream inputStream) {
        long availiable = inputStream.Length;// available();

        byte[] bytes = new byte[availiable];
		inputStream.Read(bytes, 0, (int)availiable);

		decodeJBIG2(bytes);
	}

    /**
	 * Decodes a JBIG2 image from a byte array 
	 * @param data the raw data stream
	 * @throws IOException
	 * @throws JBIG2Exception
	 */
	public void decodeJBIG2(byte[] data) {
		streamDecoder.decodeJBIG2(data);
	}

    }
}
