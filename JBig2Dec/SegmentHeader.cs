using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    public class SegmentHeader
    {
        private int segmentNumber;

        private int segmentType;
        private bool pageAssociationSizeSet;
        private bool deferredNonRetainSet;

        private int referredToSegmentCount;
        private byte[] rententionFlags;

        private int[] referredToSegments;
        private int pageAssociation;
        private int dataLength;

        public void setSegmentNumber(int SegmentNumber)
        {
            this.segmentNumber = SegmentNumber;
        }

        public void setSegmentHeaderFlags(short SegmentHeaderFlags) {
		    segmentType = SegmentHeaderFlags & 63; // 63 = 00111111
		    pageAssociationSizeSet = (SegmentHeaderFlags & 64) == 64; // 64 = // 01000000
		    deferredNonRetainSet = (SegmentHeaderFlags & 80) == 80; // 64 = 10000000

		    if (JBIG2StreamDecoder.debug) {
			    Debug.WriteLine("SegmentType = " + segmentType);
			    Debug.WriteLine("pageAssociationSizeSet = " + pageAssociationSizeSet);
			    Debug.WriteLine("deferredNonRetainSet = " + deferredNonRetainSet);
		}
	}

        public void setReferredToSegmentCount(int referredToSegmentCount)
        {
            this.referredToSegmentCount = referredToSegmentCount;
        }

        public void setRententionFlags(byte[] rententionFlags)
        {
            this.rententionFlags = rententionFlags;
        }


        public int getReferredToSegmentCount()
        {
            return referredToSegmentCount;
        }

        public int getSegmentNumber()
        {
            return segmentNumber;
        }

        public void setReferredToSegments(int[] referredToSegments)
        {
            this.referredToSegments = referredToSegments;
        }

        public void setPageAssociation(int pageAssociation)
        {
            this.pageAssociation = pageAssociation;
        }

        public bool isPageAssociationSizeSet()
        {
            return pageAssociationSizeSet;
        }

        public int getSegmentType()
        {
            return segmentType;
        }

        public void setDataLength(int dataLength)
        {
            this.dataLength = dataLength;
        }

        public int[] getReferredToSegments()
        {
            return referredToSegments;
        }


        public int getSegmentDataLength()
        {
            return dataLength;
        }

        public int getPageAssociation()
        {
            return pageAssociation;
        }
    }
}
