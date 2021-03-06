﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    class PageInformationSegment : Segment
    {
        private int pageBitmapHeight, pageBitmapWidth;
        private int yResolution, xResolution;

        PageInformationFlags pageInformationFlags = new PageInformationFlags();
	    private int pageStriping;

	    private JBIG2Bitmap pageBitmap;

	    public PageInformationSegment(JBIG2StreamDecoder streamDecoder):base(streamDecoder) {
		  //  base(streamDecoder);
	    }

	    public PageInformationFlags getPageInformationFlags() {
		    return pageInformationFlags;
	    }

	public JBIG2Bitmap getPageBitmap() {
		return pageBitmap;
	}

	public override void readSegment() {

		if (JBIG2StreamDecoder.debug)   Debug.WriteLine("==== Reading Page Information Dictionary ====");

		byte[] buff = new byte[4];
		decoder.readByte(buff);
		pageBitmapWidth = BinaryOperation.getInt32(buff);

        buff = new byte[4];
		decoder.readByte(buff);
        pageBitmapHeight = BinaryOperation.getInt32(buff);

		if (JBIG2StreamDecoder.debug)   Debug.WriteLine("Bitmap size = " + pageBitmapWidth + 'x' + pageBitmapHeight);

        buff = new byte[4];
		decoder.readByte(buff);
        xResolution = BinaryOperation.getInt32(buff);

        buff = new byte[4];
		decoder.readByte(buff);
        yResolution = BinaryOperation.getInt32(buff);

		if (JBIG2StreamDecoder.debug)   Debug.WriteLine("Resolution = " + xResolution + 'x' + yResolution);

		/** extract page information flags */
		byte pageInformationFlagsField = decoder.readByte();

		pageInformationFlags.setFlags(pageInformationFlagsField);

		if (JBIG2StreamDecoder.debug)   Debug.WriteLine("symbolDictionaryFlags = " + pageInformationFlagsField);

        buff = new byte[2];
		decoder.readByte(buff);
        pageStriping = BinaryOperation.getInt16(buff);

		if (JBIG2StreamDecoder.debug)   Debug.WriteLine("Page Striping = " + pageStriping);

		int defPix = pageInformationFlags.getFlagValue(PageInformationFlags.DEFAULT_PIXEL_VALUE);

		int height;

		if (pageBitmapHeight == -1) {
			height = pageStriping & 0x7fff;
		} else {
			height = pageBitmapHeight;
		}

		pageBitmap = new JBIG2Bitmap(pageBitmapWidth, height, arithmeticDecoder, huffmanDecoder, mmrDecoder);
		pageBitmap.clear(defPix);
	}

	public int getPageBitmapHeight() {
		return pageBitmapHeight;
	}
    }
}
