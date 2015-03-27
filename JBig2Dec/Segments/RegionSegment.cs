using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    abstract class RegionSegment: Segment
    {
        protected int regionBitmapWidth, regionBitmapHeight;
	    protected int regionBitmapXLocation, regionBitmapYLocation;

	    protected RegionFlags regionFlags = new RegionFlags();

	    public RegionSegment(JBIG2StreamDecoder streamDecoder) :base(streamDecoder){}

	    public override void readSegment() {
		    byte[] buff = new byte[4];
		    decoder.readByte(buff);
   		    regionBitmapWidth = BinaryOperation.getInt32(buff);

		    buff = new byte[4];
		    decoder.readByte(buff);
		    regionBitmapHeight = BinaryOperation.getInt32(buff);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("Bitmap size = " + regionBitmapWidth + 'x' + regionBitmapHeight);

		    buff = new byte[4];
		    decoder.readByte(buff);
		    regionBitmapXLocation = BinaryOperation.getInt32(buff);

		    buff = new byte[4];
		    decoder.readByte(buff);
		    regionBitmapYLocation = BinaryOperation.getInt32(buff);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("Bitmap location = " + regionBitmapXLocation + ',' + regionBitmapYLocation);

		    /** extract region Segment flags */
		    byte regionFlagsField = decoder.readByte();

		    regionFlags.setFlags(regionFlagsField);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("region Segment flags = " + regionFlagsField);
	    }
    }
}
