using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    public abstract class RegionSegment: Segment
    {
        protected int regionBitmapWidth, regionBitmapHeight;
	    protected int regionBitmapXLocation, regionBitmapYLocation;

	    protected RegionFlags regionFlags = new RegionFlags();

	    public RegionSegment(JBIG2StreamDecoder streamDecoder) :base(streamDecoder){}

	    public void readSegment() {
		    byte[] buff = new byte[4];
		    decoder.readByte(buff);
		    regionBitmapWidth = BitConverter.ToInt32(buff, 0);

		    buff = new byte[4];
		    decoder.readByte(buff);
		    regionBitmapHeight = BitConverter.ToInt32(buff, 0);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("Bitmap size = " + regionBitmapWidth + 'x' + regionBitmapHeight);

		    buff = new byte[4];
		    decoder.readByte(buff);
		    regionBitmapXLocation = BitConverter.ToInt32(buff, 0);

		    buff = new byte[4];
		    decoder.readByte(buff);
		    regionBitmapYLocation = BitConverter.ToInt32(buff, 0);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("Bitmap location = " + regionBitmapXLocation + ',' + regionBitmapYLocation);

		    /** extract region Segment flags */
		    byte regionFlagsField = decoder.readByte();

		    regionFlags.setFlags(regionFlagsField);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("region Segment flags = " + regionFlagsField);
	    }
    }
}
