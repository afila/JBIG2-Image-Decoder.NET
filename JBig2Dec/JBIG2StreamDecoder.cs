using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JBig2Dec;
using System.Diagnostics;

namespace JBig2Dec
{
    public class JBIG2StreamDecoder
    {
        private StreamReader reader;
        private bool noOfPagesKnown;
        private bool randomAccessOrganisation;

        private int noOfPages = -1;

        private List<Segment> segments = new List<Segment>();
        private List<JBIG2Bitmap> bitmaps = new List<JBIG2Bitmap>();

        private byte[] globalData;

        private ArithmeticDecoder arithmeticDecoder;

        private HuffmanDecoder huffmanDecoder;

        private MMRDecoder mmrDecoder;

        public static bool debug = true; //false

        public void movePointer(int i)
        {
            reader.movePointer(i);
        }


        private void readSegments() {

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("==== Segments ====");

		    bool finished = false;
		    while (!reader.isFinished() && !finished) {

			    SegmentHeader segmentHeader = new SegmentHeader();

			    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("==== Segment Header ====");

			    readSegmentHeader(segmentHeader);

			    // read the Segment data
			    Segment segment = null;

			    int segmentType = segmentHeader.getSegmentType();
			    int[] referredToSegments = segmentHeader.getReferredToSegments();
			    int noOfReferredToSegments = segmentHeader.getReferredToSegmentCount();

			    switch (segmentType) {
            //        case Segment.SYMBOL_DICTIONARY:
            //            if (JBIG2StreamDecoder.debug)   Debug.WriteLine("==== Segment Symbol Dictionary ====");

            //            segment = new SymbolDictionarySegment(this);
            //            segment.setSegmentHeader(segmentHeader);

            //        break;

            //case Segment.INTERMEDIATE_TEXT_REGION:
            //    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("==== Intermediate Text Region ====");

            //    segment = new TextRegionSegment(this, false);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.IMMEDIATE_TEXT_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Immediate Text Region ====");

            //    segment = new TextRegionSegment(this, true);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.IMMEDIATE_LOSSLESS_TEXT_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Immediate Lossless Text Region ====");

            //    segment = new TextRegionSegment(this, true);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.PATTERN_DICTIONARY:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Pattern Dictionary ====");

            //    segment = new PatternDictionarySegment(this);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.INTERMEDIATE_HALFTONE_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Intermediate Halftone Region ====");

            //    segment = new HalftoneRegionSegment(this, false);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.IMMEDIATE_HALFTONE_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Immediate Halftone Region ====");

            //    segment = new HalftoneRegionSegment(this, true);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.IMMEDIATE_LOSSLESS_HALFTONE_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Immediate Lossless Halftone Region ====");

            //    segment = new HalftoneRegionSegment(this, true);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.INTERMEDIATE_GENERIC_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Intermediate Generic Region ====");

            //    segment = new GenericRegionSegment(this, false);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

                    case Segment.IMMEDIATE_GENERIC_REGION:
                        if (JBIG2StreamDecoder.debug)   Debug.WriteLine("==== Immediate Generic Region ====");

                        segment = new GenericRegionSegment(this, true);
                        segment.setSegmentHeader(segmentHeader);

                    break;

            //case Segment.IMMEDIATE_LOSSLESS_GENERIC_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Immediate Lossless Generic Region ====");

            //    segment = new GenericRegionSegment(this, true);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.INTERMEDIATE_GENERIC_REFINEMENT_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Intermediate Generic Refinement Region ====");

            //    segment = new RefinementRegionSegment(this, false, referredToSegments, noOfReferredToSegments);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.IMMEDIATE_GENERIC_REFINEMENT_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Immediate Generic Refinement Region ====");

            //    segment = new RefinementRegionSegment(this, true, referredToSegments, noOfReferredToSegments);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            //case Segment.IMMEDIATE_LOSSLESS_GENERIC_REFINEMENT_REGION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Immediate lossless Generic Refinement Region ====");

            //    segment = new RefinementRegionSegment(this, true, referredToSegments, noOfReferredToSegments);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

            case Segment.PAGE_INFORMATION:
                if (JBIG2StreamDecoder.debug)   Debug.WriteLine("==== Page Information Dictionary ====");

                segment = new PageInformationSegment(this);
                segment.setSegmentHeader(segmentHeader);

            break;

            //case Segment.END_OF_PAGE:
            //    continue;

            //case Segment.END_OF_STRIPE:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== End of Stripes ====");

            //    segment = new EndOfStripeSegment(this);

            //    segment.setSegmentHeader(segmentHeader);
            //    break;

            //case Segment.END_OF_FILE:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== End of File ====");

            //    finished = true;

            //    continue;

            //case Segment.PROFILES:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("PROFILES UNIMPLEMENTED");
            //    break;

            //case Segment.TABLES:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("TABLES UNIMPLEMENTED");
            //    break;

            //case Segment.EXTENSION:
            //    if (JBIG2StreamDecoder.debug)
            //        Debug.WriteLine("==== Extensions ====");

            //    segment = new ExtensionSegment(this);

            //    segment.setSegmentHeader(segmentHeader);

            //    break;

			default:
				Debug.WriteLine("Unknown Segment type in JBIG2 stream");

				break;
			}
			
			if (!randomAccessOrganisation) {
				segment.readSegment();
			}

			segments.Add(segment);
		}

		if (randomAccessOrganisation) {
			foreach (Segment segment in segments) {
				segment.readSegment();
			}
		}
	}


        private void readSegmentHeader(SegmentHeader segmentHeader) {

		    handleSegmentNumber(segmentHeader);

		    handleSegmentHeaderFlags(segmentHeader);

		    handleSegmentReferredToCountAndRententionFlags(segmentHeader);

		    handleReferedToSegmentNumbers(segmentHeader);

		    handlePageAssociation(segmentHeader);

		    if (segmentHeader.getSegmentType() != Segment.END_OF_FILE)
			    handleSegmentDataLength(segmentHeader);
	    }

        private void handleSegmentDataLength(SegmentHeader segmentHeader) {
		    byte[] buf = new byte[4];
		    reader.readByte(buf);
		
		    int dateLength = BitConverter.ToInt32(buf, 0);
		    segmentHeader.setDataLength(dateLength);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("dateLength = " + dateLength);
	}

        private void handlePageAssociation(SegmentHeader segmentHeader) {
		    int pageAssociation;

		    bool isPageAssociationSizeSet = segmentHeader.isPageAssociationSizeSet();
		    if (isPageAssociationSizeSet) { // field is 4 bytes long
			    byte[] buf = new byte[4];
			    reader.readByte(buf);
			    pageAssociation = BitConverter.ToInt32(buf, 0);
		    } else { // field is 1 byte long
			    pageAssociation = reader.readByte();
		      }

		    segmentHeader.setPageAssociation(pageAssociation);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("pageAssociation = " + pageAssociation);
	    }

        private void handleReferedToSegmentNumbers(SegmentHeader segmentHeader) {
		    int referredToSegmentCount = segmentHeader.getReferredToSegmentCount();
		    int[] referredToSegments = new int[referredToSegmentCount];

		int segmentNumber = segmentHeader.getSegmentNumber();

		if (segmentNumber <= 256) {
			for (int i = 0; i < referredToSegmentCount; i++)
				referredToSegments[i] = reader.readByte();
		} else if (segmentNumber <= 65536) {
			byte[] buf = new byte[2];
			for (int i = 0; i < referredToSegmentCount; i++) {
				reader.readByte(buf);
				referredToSegments[i] = BitConverter.ToInt16(buf, 0);
			}
		} else {
			byte[] buf = new byte[4];
			for (int i = 0; i < referredToSegmentCount; i++) {
				reader.readByte(buf);
				referredToSegments[i] = BitConverter.ToInt32(buf, 0);
			}
		}

		segmentHeader.setReferredToSegments(referredToSegments);

		if (JBIG2StreamDecoder.debug) {
			Debug.WriteLine("referredToSegments = ");
			for (int i = 0; i < referredToSegments.Length; i++)
				Debug.WriteLine(referredToSegments[i] + " ");
			Debug.WriteLine("");
		}
	}

        private void handleSegmentReferredToCountAndRententionFlags(SegmentHeader segmentHeader) {
		    byte referedToSegmentCountAndRetentionFlags = reader.readByte();

		    int referredToSegmentCount = (referedToSegmentCountAndRetentionFlags & 224) >> 5; // 224
																							  // =
																							  // 11100000
            byte[] retentionFlags = null;
		    /** take off the first three bits of the first byte */
		    byte firstByte = (byte) (referedToSegmentCountAndRetentionFlags & 31); // 31 =
																				   // 00011111

		    if (referredToSegmentCount <= 4) { // short form

			    retentionFlags = new byte[1];
			    retentionFlags[0] = firstByte;

		     } else if (referredToSegmentCount == 7) { // long form

			        byte[] longFormCountAndFlags = new byte[4];
			        /** add the first byte of the four */
			        longFormCountAndFlags[0] = firstByte;
             
                    // add the next 3 bytes to the array
			        for (int i = 1; i < 4; i++)
				        longFormCountAndFlags[i] = reader.readByte();
             
			        /** get the count of the referred to Segments */
			        referredToSegmentCount = BitConverter.ToInt32(longFormCountAndFlags,0);

			        /** calculate the number of bytes in this field */
			        int noOfBytesInField = (int) Math.Ceiling(4 + ((referredToSegmentCount + 1) / 8d));
			        Debug.WriteLine("noOfBytesInField = " + noOfBytesInField);

			        int noOfRententionFlagBytes = noOfBytesInField - 4;
			        retentionFlags = new byte[noOfRententionFlagBytes];
			        reader.readByte(retentionFlags);

		         } else  // error
			             throw new Exception("Error, 3 bit Segment count field = " + referredToSegmentCount);
		                  

		    segmentHeader.setReferredToSegmentCount(referredToSegmentCount);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("referredToSegmentCount = " + referredToSegmentCount);

		    segmentHeader.setRententionFlags(retentionFlags);

		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("retentionFlags = ");

		if (JBIG2StreamDecoder.debug) {
			for (int i = 0; i < retentionFlags.Length; i++)
				Debug.WriteLine(retentionFlags[i] + " ");

			Debug.WriteLine("");
		}
	}


        private void handleSegmentHeaderFlags(SegmentHeader segmentHeader) {
		    byte segmentHeaderFlags = reader.readByte();
		    // Debud.WriteLine("SegmentHeaderFlags = " + SegmentHeaderFlags);
		    segmentHeader.setSegmentHeaderFlags(segmentHeaderFlags);
	    }


        private void handleSegmentNumber(SegmentHeader segmentHeader) {
		    byte[] segmentBytes = new byte[4];
		    reader.readByte(segmentBytes);

		    int segmentNumber = BitConverter.ToInt32(segmentBytes,0);

		    if (JBIG2StreamDecoder.debug)
			    Debug.WriteLine("SegmentNumber = " + segmentNumber);
		        segmentHeader.setSegmentNumber(segmentNumber);
	        }


        public HuffmanDecoder getHuffmanDecoder()
        {
            return huffmanDecoder;
        }

        public MMRDecoder getMMRDecoder()
        {
            return mmrDecoder;
        }

        public ArithmeticDecoder getArithmeticDecoder()
        {
            return arithmeticDecoder;
        }

        public void readByte(byte[] buff)  {
		    reader.readByte(buff);
	    }
        public byte readByte() {
		    return reader.readByte();
	    }

        public PageInformationSegment findPageSegement(int page) {
		foreach (Segment segment in segments) {
			SegmentHeader segmentHeader = segment.getSegmentHeader();
			if (segmentHeader.getSegmentType() == Segment.PAGE_INFORMATION && segmentHeader.getPageAssociation() == page) {
				return (PageInformationSegment) segment;
			}
		}

		return null;
	}



        public void decodeJBIG2(byte[] data) {
		    reader = new StreamReader(data);

		    resetDecoder();

		    bool validFile = checkHeader();
		    if (JBIG2StreamDecoder.debug)   Debug.WriteLine("validFile = " + validFile);

		    if (!validFile) {
			    /**
			    * Assume this is a stream from a PDF so there is no file header,
			    * end of page segments, or end of file segments. Organisation must
			    * be sequential, and the number of pages is assumed to be 1.
			    */

			    noOfPagesKnown = true;
			    randomAccessOrganisation = false;
			    noOfPages = 1;

			    /** check to see if there is any global data to be read */
			    if (globalData != null) {
				    /** set the reader to read from the global data */
				    reader = new StreamReader(globalData);

				    huffmanDecoder = new HuffmanDecoder(reader);
				    mmrDecoder = new MMRDecoder(reader);
				    arithmeticDecoder = new ArithmeticDecoder(reader);
				
				    /** read in the global data segments */
				    readSegments();

				    /** set the reader back to the main data */
				    reader = new StreamReader(data);
			    } else {
				    /**
				    * There's no global data, so move the file pointer back to the
				    * start of the stream
				    */
				    reader.movePointer(-8);
			      }
		        } else {
			        /**
			        * We have the file header, so assume it is a valid stand-alone
			        * file.
			        */

			        if (JBIG2StreamDecoder.debug)   Debug.WriteLine("==== File Header ====");

			        setFileHeaderFlags();

			        if (JBIG2StreamDecoder.debug) {
				        Debug.WriteLine("randomAccessOrganisation = " + randomAccessOrganisation);
				        Debug.WriteLine("noOfPagesKnown = " + noOfPagesKnown);
			        }

			        if (noOfPagesKnown) {
				        noOfPages = getNoOfPages();

				        if (JBIG2StreamDecoder.debug)Debug.WriteLine("noOfPages = " + noOfPages);
			        }
		        }

		huffmanDecoder = new HuffmanDecoder(reader);
		mmrDecoder = new MMRDecoder(reader);
		arithmeticDecoder = new ArithmeticDecoder(reader);
		
		/** read in the main segment data */
		readSegments();
	    }

        private int getNoOfPages() {
		    byte[] noOfPages = new byte[4];
		    reader.readByte(noOfPages);

		    return BitConverter.ToInt32(noOfPages, 0);
	    }

        private void setFileHeaderFlags() {
		    byte headerFlags = reader.readByte();

		    if ((headerFlags & 0xfc) != 0) {
			Debug.WriteLine("Warning, reserved bits (2-7) of file header flags are not zero " + headerFlags);
		}

		int fileOrganisation = headerFlags & 1;
		randomAccessOrganisation = fileOrganisation == 0;

		int pagesKnown = headerFlags & 2;
		noOfPagesKnown = pagesKnown == 0;
	}

        private bool checkHeader()
        {
            throw new NotImplementedException();
        }

        private void resetDecoder()
        {
            noOfPagesKnown = false;
            randomAccessOrganisation = false;

            noOfPages = -1;

            segments.Clear();
            bitmaps.Clear();
        }
    }
}
