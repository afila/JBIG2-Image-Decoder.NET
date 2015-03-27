using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JBig2Dec;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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
	    public void decodeJBIG2(String file)  
        {
            using(FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
		        decodeJBIG2(fs);
            }
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


    public Image getPageAsBufferedImage(int page)
    {
        //Bitmap b = (Bitmap)Bitmap.FromFile(@"c:\java.bmp");
        //BitmapData d = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);


        //byte[] bb = new byte[d.Stride * d.Height];
        //Marshal.Copy(d.Scan0, bb, 0, bb.Length);
        
        page++;
        JBIG2Bitmap pageBitmap = streamDecoder.findPageSegement(page).getPageBitmap();

        byte[] bytes = pageBitmap.getData(true);

        if (bytes == null)  return null;

        int width = pageBitmap.getWidth();
		int height = pageBitmap.getHeight();

        Bitmap bmp = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
        BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);


        //int w = width >> 3; // /8
        int reminder;
        int w = Math.DivRem(width, 8, out reminder); //8bit
        byte[] data = new byte[bd.Stride*height];

        //copy array + stride
        int l = 0;
        for (int y = 0; y < height; y++)
        {
            //int n = y * bd.Stride;
            int x;
            for (x = y * bd.Stride; x < y * bd.Stride + w; x++)
            {
                //data[n + x] = bytes[l++];
                data[x] = bytes[l++];
            }

            if (reminder == 0)  continue;
            //data[n + w] = (byte)(bytes[l++] << (8 - reminder));
            data[x] = (byte)(bytes[l++] << (8 - reminder));

           // for (int k = 1; k < bd.Stride - w; k++ )
           //     data[x + k] = 0;
        }
        
        //Copy the data from the byte array into BitmapData.Scan0
        Marshal.Copy(data, 0, bd.Scan0, data.Length);

        //unlock the pixel
        bmp.UnlockBits(bd);
        
        return bmp;
    }

  }
}
