using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace JBig2Dec
{
    public sealed class JBIG2Bitmap
    {
        private int width, height, line;
        private int bitmapNumber;
        public BitArray data;

        private ArithmeticDecoder arithmeticDecoder;
        private HuffmanDecoder huffmanDecoder;
        private MMRDecoder mmrDecoder;

        public JBIG2Bitmap(int width, int height, ArithmeticDecoder arithmeticDecoder, HuffmanDecoder huffmanDecoder, MMRDecoder mmrDecoder)
        {
            this.width = width;
            this.height = height;
            this.arithmeticDecoder = arithmeticDecoder;
            this.huffmanDecoder = huffmanDecoder;
            this.mmrDecoder = mmrDecoder;

            this.line = (width + 7) >> 3;

            this.data = new BitArray(width * height);
        }


        internal void clear(int p)
        {
            throw new NotImplementedException();
        }

        public void readBitmap(bool useMMR, int template, bool typicalPredictionGenericDecodingOn, bool useSkip, JBIG2Bitmap skipBitmap, short[] adaptiveTemplateX, short[] adaptiveTemplateY, int mmrDataLength) {
            throw new NotImplementedException();
//        if (useMMR) {

//            //MMRDecoder mmrDecoder = MMRDecoder.getInstance();
//            mmrDecoder.reset();

//            int[] referenceLine = new int[width + 2];
//            int[] codingLine = new int[width + 2];
//            codingLine[0] = codingLine[1] = width;

//            for (int row = 0; row < height; row++) {

//                int i = 0;
//                for (; codingLine[i] < width; i++) {
//                    referenceLine[i] = codingLine[i];
//                }
//                referenceLine[i] = referenceLine[i + 1] = width;

//                int referenceI = 0;
//                int codingI = 0;
//                int a0 = 0;

//                do {
//                    int code1 = mmrDecoder.get2DCode(), code2, code3;

//                    switch (code1) {
//                    case MMRDecoder.twoDimensionalPass:
//                        if (referenceLine[referenceI] < width) {
//                            a0 = referenceLine[referenceI + 1];
//                            referenceI += 2;
//                        }
//                        break;
//                    case MMRDecoder.twoDimensionalHorizontal:
//                        if ((codingI & 1) != 0) {
//                            code1 = 0;
//                            do {
//                                code1 += code3 = mmrDecoder.getBlackCode();
//                            } while (code3 >= 64);

//                            code2 = 0;
//                            do {
//                                code2 += code3 = mmrDecoder.getWhiteCode();
//                            } while (code3 >= 64);
//                        } else {
//                            code1 = 0;
//                            do {
//                                code1 += code3 = mmrDecoder.getWhiteCode();
//                            } while (code3 >= 64);

//                            code2 = 0;
//                            do {
//                                code2 += code3 = mmrDecoder.getBlackCode();
//                            } while (code3 >= 64);

//                        }
//                        if (code1 > 0 || code2 > 0) {
//                            a0 = codingLine[codingI++] = a0 + code1;
//                            a0 = codingLine[codingI++] = a0 + code2;

//                            while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
//                                referenceI += 2;
//                            }
//                        }
//                        break;
//                    case MMRDecoder.twoDimensionalVertical0:
//                        a0 = codingLine[codingI++] = referenceLine[referenceI];
//                        if (referenceLine[referenceI] < width) {
//                            referenceI++;
//                        }

//                        break;
//                    case MMRDecoder.twoDimensionalVerticalR1:
//                        a0 = codingLine[codingI++] = referenceLine[referenceI] + 1;
//                        if (referenceLine[referenceI] < width) {
//                            referenceI++;
//                            while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
//                                referenceI += 2;
//                            }
//                        }

//                        break;
//                    case MMRDecoder.twoDimensionalVerticalR2:
//                        a0 = codingLine[codingI++] = referenceLine[referenceI] + 2;
//                        if (referenceLine[referenceI] < width) {
//                            referenceI++;
//                            while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
//                                referenceI += 2;
//                            }
//                        }
						
//                        break;
//                    case MMRDecoder.twoDimensionalVerticalR3:
//                        a0 = codingLine[codingI++] = referenceLine[referenceI] + 3;
//                        if (referenceLine[referenceI] < width) {
//                            referenceI++;
//                            while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
//                                referenceI += 2;
//                            }
//                        }

//                        break;
//                    case MMRDecoder.twoDimensionalVerticalL1:
//                        a0 = codingLine[codingI++] = referenceLine[referenceI] - 1;
//                        if (referenceI > 0) {
//                            referenceI--;
//                        } else {
//                            referenceI++;
//                        }

//                        while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
//                            referenceI += 2;
//                        }

//                        break;
//                    case MMRDecoder.twoDimensionalVerticalL2:
//                        a0 = codingLine[codingI++] = referenceLine[referenceI] - 2;
//                        if (referenceI > 0) {
//                            referenceI--;
//                        } else {
//                            referenceI++;
//                        }

//                        while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
//                            referenceI += 2;
//                        }

//                        break;
//                    case MMRDecoder.twoDimensionalVerticalL3:
//                        a0 = codingLine[codingI++] = referenceLine[referenceI] - 3;
//                        if (referenceI > 0) {
//                            referenceI--;
//                        } else {
//                            referenceI++;
//                        }

//                        while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
//                            referenceI += 2;
//                        }

//                        break;
//                    default:
//                        if (JBIG2StreamDecoder.debug)
//                            System.out.println("Illegal code in JBIG2 MMR bitmap data");

//                        break;
//                    }
//                } while (a0 < width);

//                codingLine[codingI++] = width;

//                for (int j = 0; codingLine[j] < width; j += 2) {
//                    for (int col = codingLine[j]; col < codingLine[j + 1]; col++) {
//                        setPixel(col, row, 1);
//                    }
//                }
//            }

//            if (mmrDataLength >= 0) {
//                mmrDecoder.skipTo(mmrDataLength);
//            } else {
//                if (mmrDecoder.get24Bits() != 0x001001) {
//                    if (JBIG2StreamDecoder.debug)
//                        System.out.println("Missing EOFB in JBIG2 MMR bitmap data");
//                }
//            }

//        } else {

//            //ArithmeticDecoder arithmeticDecoder = ArithmeticDecoder.getInstance();

//            BitmapPointer cxPtr0 = new BitmapPointer(this), cxPtr1 = new BitmapPointer(this);
//            BitmapPointer atPtr0 = new BitmapPointer(this), atPtr1 = new BitmapPointer(this), atPtr2 = new BitmapPointer(this), atPtr3 = new BitmapPointer(this);

//            long ltpCX = 0;
//            if (typicalPredictionGenericDecodingOn) {
//                switch (template) {
//                case 0:
//                    ltpCX = 0x3953;
//                    break;
//                case 1:
//                    ltpCX = 0x079a;
//                    break;
//                case 2:
//                    ltpCX = 0x0e3;
//                    break;
//                case 3:
//                    ltpCX = 0x18a;
//                    break;
//                }
//            }

//            bool ltp = false;
//            long cx, cx0, cx1, cx2;

//            for (int row = 0; row < height; row++) {
//                if (typicalPredictionGenericDecodingOn) {
//                    int bit = arithmeticDecoder.decodeBit(ltpCX, arithmeticDecoder.genericRegionStats);
//                    if (bit != 0) {
//                        ltp = !ltp;
//                    }

//                    if (ltp) {
//                        duplicateRow(row, row - 1);
//                        continue;
//                    }
//                }

//                int pixel;

//                switch (template) {
//                case 0:

//                    cxPtr0.setPointer(0, row - 2);
//                    cx0 = (cxPtr0.nextPixel() << 1); 
//                    cx0 |= cxPtr0.nextPixel();
//                    //cx0 = (BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel();

//                    cxPtr1.setPointer(0, row - 1);
//                    cx1 = (cxPtr1.nextPixel() << 2); 
//                    cx1 |= (cxPtr1.nextPixel() << 1); 
//                    cx1 |= (cxPtr1.nextPixel());

//                    //cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();
//                    //cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();

//                    cx2 = 0;

//                    atPtr0.setPointer(adaptiveTemplateX[0], row + adaptiveTemplateY[0]);
//                    atPtr1.setPointer(adaptiveTemplateX[1], row + adaptiveTemplateY[1]);
//                    atPtr2.setPointer(adaptiveTemplateX[2], row + adaptiveTemplateY[2]);
//                    atPtr3.setPointer(adaptiveTemplateX[3], row + adaptiveTemplateY[3]);

//                    for (int col = 0; col < width; col++) {

//                        cx = (BinaryOperation.bit32ShiftL(cx0, 13)) | (BinaryOperation.bit32ShiftL(cx1, 8)) | (BinaryOperation.bit32ShiftL(cx2, 4)) | (atPtr0.nextPixel() << 3) | (atPtr1.nextPixel() << 2) | (atPtr2.nextPixel() << 1) | atPtr3.nextPixel();

//                        if (useSkip && skipBitmap.getPixel(col, row) != 0) {
//                            pixel = 0;
//                        } else {
//                            pixel = arithmeticDecoder.decodeBit(cx, arithmeticDecoder.genericRegionStats);
//                            if (pixel != 0) {
//                                data.set(row*width + col);
//                            }
//                        }

//                        cx0 = ((BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel()) & 0x07;
//                        cx1 = ((BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel()) & 0x1f;
//                        cx2 = ((BinaryOperation.bit32ShiftL(cx2, 1)) | pixel) & 0x0f;
//                    }
//                    break;

//                case 1:

//                    cxPtr0.setPointer(0, row - 2);
//                    cx0 = (cxPtr0.nextPixel() << 2);
//                    cx0 |= (cxPtr0.nextPixel() << 1);
//                    cx0 |= (cxPtr0.nextPixel());
//                    /*cx0 = cxPtr0.nextPixel();
//                    cx0 = (BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel();
//                    cx0 = (BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel();*/

//                    cxPtr1.setPointer(0, row - 1);
//                    cx1 = (cxPtr1.nextPixel() << 2);
//                    cx1 |= (cxPtr1.nextPixel() << 1);
//                    cx1 |= (cxPtr1.nextPixel());
//                    /*cx1 = cxPtr1.nextPixel();
//                    cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();
//                    cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();*/

//                    cx2 = 0;

//                    atPtr0.setPointer(adaptiveTemplateX[0], row + adaptiveTemplateY[0]);

//                    for (int col = 0; col < width; col++) {

//                        cx = (BinaryOperation.bit32ShiftL(cx0, 9)) | (BinaryOperation.bit32ShiftL(cx1, 4)) | (BinaryOperation.bit32ShiftL(cx2, 1)) | atPtr0.nextPixel();

//                        if (useSkip && skipBitmap.getPixel(col, row) != 0) {
//                            pixel = 0;
//                        } else {
//                            pixel = arithmeticDecoder.decodeBit(cx, arithmeticDecoder.genericRegionStats);
//                            if (pixel != 0) {
//                                data.set(row*width + col);
//                            }
//                        }

//                        cx0 = ((BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel()) & 0x0f;
//                        cx1 = ((BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel()) & 0x1f;
//                        cx2 = ((BinaryOperation.bit32ShiftL(cx2, 1)) | pixel) & 0x07;
//                    }
//                    break;

//                case 2:

//                    cxPtr0.setPointer(0, row - 2);
//                    cx0 = (cxPtr0.nextPixel() << 1); 
//                    cx0 |= (cxPtr0.nextPixel());
//                    /*cx0 = cxPtr0.nextPixel();
//                    cx0 = (BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel();
//                    */

//                    cxPtr1.setPointer(0, row - 1);
//                    cx1 = (cxPtr1.nextPixel() << 1); 
//                    cx1 |= (cxPtr1.nextPixel());
//                    /*cx1 = cxPtr1.nextPixel();
//                    cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();*/

//                    cx2 = 0;

//                    atPtr0.setPointer(adaptiveTemplateX[0], row + adaptiveTemplateY[0]);

//                    for (int col = 0; col < width; col++) {

//                        cx = (BinaryOperation.bit32ShiftL(cx0, 7)) | (BinaryOperation.bit32ShiftL(cx1, 3)) | (BinaryOperation.bit32ShiftL(cx2, 1)) | atPtr0.nextPixel();

//                        if (useSkip && skipBitmap.getPixel(col, row) != 0) {
//                            pixel = 0;
//                        } else {
//                            pixel = arithmeticDecoder.decodeBit(cx, arithmeticDecoder.genericRegionStats);
//                            if (pixel != 0) {
//                                data.set(row*width + col);
//                            }
//                        }

//                        cx0 = ((BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel()) & 0x07;
//                        cx1 = ((BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel()) & 0x0f;
//                        cx2 = ((BinaryOperation.bit32ShiftL(cx2, 1)) | pixel) & 0x03;
//                    }
//                    break;

//                case 3:

//                    cxPtr1.setPointer(0, row - 1);
//                    cx1 = (cxPtr1.nextPixel() << 1);
//                    cx1 |= (cxPtr1.nextPixel());
//                    /*cx1 = cxPtr1.nextPixel();
//                    cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();
//*/
//                    cx2 = 0;

//                    atPtr0.setPointer(adaptiveTemplateX[0], row + adaptiveTemplateY[0]);

//                    for (int col = 0; col < width; col++) {

//                        cx = (BinaryOperation.bit32ShiftL(cx1, 5)) | (BinaryOperation.bit32ShiftL(cx2, 1)) | atPtr0.nextPixel();

//                        if (useSkip && skipBitmap.getPixel(col, row) != 0) {
//                            pixel = 0;

//                        } else {
//                            pixel = arithmeticDecoder.decodeBit(cx, arithmeticDecoder.genericRegionStats);
//                            if (pixel != 0) {
//                                data.set(row*width + col);
//                            }
//                        }

//                        cx1 = ((BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel()) & 0x1f;
//                        cx2 = ((BinaryOperation.bit32ShiftL(cx2, 1)) | pixel) & 0x0f;
//                    }
//                    break;
//                }
//            }
//        }
//    }
    }

        public int getWidth() {
            return width;
        }

        public int getHeight()
        {
            return height;
        }
    }
}
