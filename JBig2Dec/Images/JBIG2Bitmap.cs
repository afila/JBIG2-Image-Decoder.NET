using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace JBig2Dec
{
    sealed class JBIG2Bitmap
    {
        private int width, height, line;
        private int bitmapNumber;
        public FastBitSet data; // BitArray

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

            this.data = new FastBitSet(width * height);
        }

        public int getWidth() {
            return width;
        }

        public int getHeight() {
            return height;
        }
        
        public void clear(int defPixel)
        {
            data.setAll(defPixel == 1);
            //data.set(0, data.size(), defPixel == 1);
        }

     

        public void readBitmap(bool useMMR, int template, bool typicalPredictionGenericDecodingOn, bool useSkip, JBIG2Bitmap skipBitmap, short[] adaptiveTemplateX, short[] adaptiveTemplateY, int mmrDataLength) 
        {
            if (useMMR)  
            {
                //throw new NotImplementedException();

                		//MMRDecoder mmrDecoder = MMRDecoder.getInstance();
			    mmrDecoder.reset();

			    int[] referenceLine = new int[width + 2];
			    int[] codingLine = new int[width + 2];
			    codingLine[0] = codingLine[1] = width;

			    for (int row = 0; row < height; row++) { 
				    int i = 0;
				    for (; codingLine[i] < width; i++) {
					    referenceLine[i] = codingLine[i];
				    }
				    referenceLine[i] = referenceLine[i + 1] = width;

				    int referenceI = 0;
				    int codingI = 0;
				    int a0 = 0;

				    do {
					    int code1 = mmrDecoder.get2DCode(), code2, code3;

					switch (code1) {
					case MMRDecoder.twoDimensionalPass:
						if (referenceLine[referenceI] < width) {
							a0 = referenceLine[referenceI + 1];
							referenceI += 2;
						}
						break;
					case MMRDecoder.twoDimensionalHorizontal:
						if ((codingI & 1) != 0) {
							code1 = 0;
							do {
								code1 += code3 = mmrDecoder.getBlackCode();
							} while (code3 >= 64);

							code2 = 0;
							do {
								code2 += code3 = mmrDecoder.getWhiteCode();
							} while (code3 >= 64);
						} else {
							code1 = 0;
							do {
								code1 += code3 = mmrDecoder.getWhiteCode();
							} while (code3 >= 64);

							code2 = 0;
							do {
								code2 += code3 = mmrDecoder.getBlackCode();
							} while (code3 >= 64);

						}
						if (code1 > 0 || code2 > 0) {
							a0 = codingLine[codingI++] = a0 + code1;
							a0 = codingLine[codingI++] = a0 + code2;

							while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
								referenceI += 2;
							}
						}
						break;
					case MMRDecoder.twoDimensionalVertical0:
						a0 = codingLine[codingI++] = referenceLine[referenceI];
						if (referenceLine[referenceI] < width) {
							referenceI++;
						}

						break;
					case MMRDecoder.twoDimensionalVerticalR1:
						a0 = codingLine[codingI++] = referenceLine[referenceI] + 1;
						if (referenceLine[referenceI] < width) {
							referenceI++;
							while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
								referenceI += 2;
							}
						}

						break;
					case MMRDecoder.twoDimensionalVerticalR2:
						a0 = codingLine[codingI++] = referenceLine[referenceI] + 2;
						if (referenceLine[referenceI] < width) {
							referenceI++;
							while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
								referenceI += 2;
							}
						}
						
						break;
					case MMRDecoder.twoDimensionalVerticalR3:
						a0 = codingLine[codingI++] = referenceLine[referenceI] + 3;
						if (referenceLine[referenceI] < width) {
							referenceI++;
							while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
								referenceI += 2;
							}
						}

						break;
					case MMRDecoder.twoDimensionalVerticalL1:
						a0 = codingLine[codingI++] = referenceLine[referenceI] - 1;
						if (referenceI > 0) {
							referenceI--;
						} else {
							referenceI++;
						}

						while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
							referenceI += 2;
						}

						break;
					case MMRDecoder.twoDimensionalVerticalL2:
						a0 = codingLine[codingI++] = referenceLine[referenceI] - 2;
						if (referenceI > 0) {
							referenceI--;
						} else {
							referenceI++;
						}

						while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
							referenceI += 2;
						}

						break;
					case MMRDecoder.twoDimensionalVerticalL3:
						a0 = codingLine[codingI++] = referenceLine[referenceI] - 3;
						if (referenceI > 0) {
							referenceI--;
						} else {
							referenceI++;
						}

						while (referenceLine[referenceI] <= a0 && referenceLine[referenceI] < width) {
							referenceI += 2;
						}

						break;
					default:
						if (JBIG2StreamDecoder.debug)
							Debug.WriteLine("Illegal code in JBIG2 MMR bitmap data");

						break;
					}
				} while (a0 < width);

				codingLine[codingI++] = width;

				for (int j = 0; codingLine[j] < width; j += 2) {
					for (int col = codingLine[j]; col < codingLine[j + 1]; col++) {
						setPixel(col, row, 1);
					}
				}
			}

			if (mmrDataLength >= 0) {
				mmrDecoder.skipTo(mmrDataLength);
			} else {
				if (mmrDecoder.get24Bits() != 0x001001) {
					if (JBIG2StreamDecoder.debug)
						Debug.WriteLine("Missing EOFB in JBIG2 MMR bitmap data");
				}
			}

            } 
            else {

                //ArithmeticDecoder arithmeticDecoder = ArithmeticDecoder.getInstance();

                BitmapPointer cxPtr0 = new BitmapPointer(this), cxPtr1 = new BitmapPointer(this);
                BitmapPointer atPtr0 = new BitmapPointer(this), atPtr1 = new BitmapPointer(this), atPtr2 = new BitmapPointer(this), atPtr3 = new BitmapPointer(this);

                long ltpCX = 0;
                if (typicalPredictionGenericDecodingOn) {
                    switch (template) {
                        case 0:
                            ltpCX = 0x3953;
                            break;
                        case 1:
                            ltpCX = 0x079a;
                            break;
                        case 2:
                            ltpCX = 0x0e3;
                            break;
                        case 3:
                            ltpCX = 0x18a;
                            break;
                    }
                }

                bool ltp = false;
                long cx, cx0, cx1, cx2;

                for (int row = 0; row < height; row++) 
                {
                    if (typicalPredictionGenericDecodingOn) {
                        int bit = arithmeticDecoder.decodeBit(ltpCX, arithmeticDecoder.genericRegionStats);
                        if (bit != 0)   ltp = !ltp;
                        
                        if (ltp) {
                            duplicateRow(row, row - 1);
                            continue;
                        }
                    }

                    int pixel;

                    switch (template) {
                        case 0:
                            cxPtr0.setPointer(0, row - 2);
                            cx0 = (cxPtr0.nextPixel() << 1); 
                            cx0 |= cxPtr0.nextPixel();
                            //cx0 = (BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel();

                            cxPtr1.setPointer(0, row - 1);
                            cx1 = (cxPtr1.nextPixel() << 2); 
                            cx1 |= (cxPtr1.nextPixel() << 1); 
                            cx1 |= (cxPtr1.nextPixel());

                            //cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();
                            //cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();

                            cx2 = 0;

                            atPtr0.setPointer(adaptiveTemplateX[0], row + adaptiveTemplateY[0]);
                            atPtr1.setPointer(adaptiveTemplateX[1], row + adaptiveTemplateY[1]);
                            atPtr2.setPointer(adaptiveTemplateX[2], row + adaptiveTemplateY[2]);
                            atPtr3.setPointer(adaptiveTemplateX[3], row + adaptiveTemplateY[3]);

                            for (int col = 0; col < width; col++) {
                                cx = (BinaryOperation.bit32ShiftL(cx0, 13)) | (BinaryOperation.bit32ShiftL(cx1, 8)) | (BinaryOperation.bit32ShiftL(cx2, 4)) | (atPtr0.nextPixel() << 3) | (atPtr1.nextPixel() << 2) | (atPtr2.nextPixel() << 1) | atPtr3.nextPixel();

                                if (useSkip && skipBitmap.getPixel(col, row) != 0) {
                                    pixel = 0;
                                } else {
                                        pixel = arithmeticDecoder.decodeBit(cx, arithmeticDecoder.genericRegionStats);
                                        if (pixel != 0) {
                                            data.set(row*width + col);
                                        }
                                  }

                                cx0 = ((BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel()) & 0x07;
                                cx1 = ((BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel()) & 0x1f;
                                cx2 = ((BinaryOperation.bit32ShiftL(cx2, 1)) | pixel) & 0x0f;
                            }
                        break;

                        case 1:
                            cxPtr0.setPointer(0, row - 2);
                            cx0 = (cxPtr0.nextPixel() << 2);
                            cx0 |= (cxPtr0.nextPixel() << 1);
                            cx0 |= (cxPtr0.nextPixel());
                            /*cx0 = cxPtr0.nextPixel();
                            cx0 = (BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel();
                            cx0 = (BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel();*/

                            cxPtr1.setPointer(0, row - 1);
                            cx1 = (cxPtr1.nextPixel() << 2);
                            cx1 |= (cxPtr1.nextPixel() << 1);
                            cx1 |= (cxPtr1.nextPixel());
                            /*cx1 = cxPtr1.nextPixel();
                            cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();
                            cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();*/

                            cx2 = 0;

                            atPtr0.setPointer(adaptiveTemplateX[0], row + adaptiveTemplateY[0]);

                            for (int col = 0; col < width; col++) {

                                cx = (BinaryOperation.bit32ShiftL(cx0, 9)) | (BinaryOperation.bit32ShiftL(cx1, 4)) | (BinaryOperation.bit32ShiftL(cx2, 1)) | atPtr0.nextPixel();

                                if (useSkip && skipBitmap.getPixel(col, row) != 0) {
                                    pixel = 0;
                                } else {
                                        pixel = arithmeticDecoder.decodeBit(cx, arithmeticDecoder.genericRegionStats);
                                        if (pixel != 0) {
                                            data.set(row*width + col);
                                        }
                                }

                                cx0 = ((BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel()) & 0x0f;
                                cx1 = ((BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel()) & 0x1f;
                                cx2 = ((BinaryOperation.bit32ShiftL(cx2, 1)) | pixel) & 0x07;
                            }
                        break;

                        case 2:

                            cxPtr0.setPointer(0, row - 2);
                            cx0 = (cxPtr0.nextPixel() << 1); 
                            cx0 |= (cxPtr0.nextPixel());
                            /*cx0 = cxPtr0.nextPixel();
                            cx0 = (BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel();
                            */

                            cxPtr1.setPointer(0, row - 1);
                            cx1 = (cxPtr1.nextPixel() << 1); 
                            cx1 |= (cxPtr1.nextPixel());
                            /*cx1 = cxPtr1.nextPixel();
                            cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();*/

                            cx2 = 0;

                            atPtr0.setPointer(adaptiveTemplateX[0], row + adaptiveTemplateY[0]);

                            for (int col = 0; col < width; col++) {

                                cx = (BinaryOperation.bit32ShiftL(cx0, 7)) | (BinaryOperation.bit32ShiftL(cx1, 3)) | (BinaryOperation.bit32ShiftL(cx2, 1)) | atPtr0.nextPixel();

                                if (useSkip && skipBitmap.getPixel(col, row) != 0) {
                                    pixel = 0;
                                } else {
                                        pixel = arithmeticDecoder.decodeBit(cx, arithmeticDecoder.genericRegionStats);
                                        if (pixel != 0) {
                                            data.set(row*width + col);
                                        }
                                }

                                cx0 = ((BinaryOperation.bit32ShiftL(cx0, 1)) | cxPtr0.nextPixel()) & 0x07;
                                cx1 = ((BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel()) & 0x0f;
                                cx2 = ((BinaryOperation.bit32ShiftL(cx2, 1)) | pixel) & 0x03;
                            }
                            break;

                        case 3:

                            cxPtr1.setPointer(0, row - 1);
                            cx1 = (cxPtr1.nextPixel() << 1);
                            cx1 |= (cxPtr1.nextPixel());
                            /*cx1 = cxPtr1.nextPixel();
                            cx1 = (BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel();
                            */
                            cx2 = 0;

                            atPtr0.setPointer(adaptiveTemplateX[0], row + adaptiveTemplateY[0]);

                            for (int col = 0; col < width; col++) {

                                cx = (BinaryOperation.bit32ShiftL(cx1, 5)) | (BinaryOperation.bit32ShiftL(cx2, 1)) | atPtr0.nextPixel();

                                if (useSkip && skipBitmap.getPixel(col, row) != 0) {
                                    pixel = 0;

                                } else {
                                        pixel = arithmeticDecoder.decodeBit(cx, arithmeticDecoder.genericRegionStats);
                                        if (pixel != 0) {
                                            data.set(row*width + col);
                                        }
                                  }

                                cx1 = ((BinaryOperation.bit32ShiftL(cx1, 1)) | cxPtr1.nextPixel()) & 0x1f;
                                cx2 = ((BinaryOperation.bit32ShiftL(cx2, 1)) | pixel) & 0x0f;
                            }
                        break;
                }
            }
        }
    }

        private void duplicateRow(int yDest, int ySrc) {
            for (int i = 0; i < width; i++) 
                setPixel(i, yDest, getPixel(i, ySrc));
        }

        private void setPixel(int col, int row, FastBitSet data, int value) {
            int index = (row * width) + col;

            data.set(index, value == 1);
        }

        public void setPixel(int col, int row, int value) {
            setPixel(col, row, data, value);
        }
                

        public int getPixel(int col, int row) {
            return data.get( (row * width) + col)? 1 : 0;
        }


        public void expand(int newHeight, int defaultPixel)
        {
            //		Debug.WriteLine("expand FastBitSet");
            //		FastBitSet newData = new FastBitSet(width, newHeight);
            //
            //		for (int row = 0; row < height; row++) {
            //			for (int col = 0; col < width; col += 8) {
            //				setPixelByte(col, row, newData, getPixelByte(col, row));
            //			}
            //		}
            //
            //		this.height = newHeight;
            //		this.data = newData;
            FastBitSet newData = new FastBitSet(newHeight * width);
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    setPixel(col, row, newData, getPixel(col, row));

            this.height = newHeight;
            this.data = newData;
        }

        public void combine(JBIG2Bitmap bitmap, int x, int y, long combOp)
        {
            int srcWidth = bitmap.width;
            int srcHeight = bitmap.height;

            //		int maxRow = y + srcHeight;
            //		int maxCol = x + srcWidth;
            //
            //		for (int row = y; row < maxRow; row++) {
            //			for (int col = x; col < maxCol; srcCol += 8, col += 8) {
            //
            //				byte srcPixelByte = bitmap.getPixelByte(srcCol, srcRow);
            //				byte dstPixelByte = getPixelByte(col, row);
            //				byte endPixelByte;
            //
            //				switch ((int) combOp) {
            //				case 0: // or
            //					endPixelByte = (byte) (dstPixelByte | srcPixelByte);
            //					break;
            //				case 1: // and
            //					endPixelByte = (byte) (dstPixelByte & srcPixelByte);
            //					break;
            //				case 2: // xor
            //					endPixelByte = (byte) (dstPixelByte ^ srcPixelByte);
            //					break;
            //				case 3: // xnor
            //					endPixelByte = (byte) ~(dstPixelByte ^ srcPixelByte);
            //					break;
            //				case 4: // replace
            //				default:
            //					endPixelByte = srcPixelByte;
            //					break;
            //				}
            //				int used = maxCol - col;
            //				if (used < 8) {
            //					// mask bits
            //					endPixelByte = (byte) ((endPixelByte & (0xFF >> (8 - used))) | (dstPixelByte & (0xFF << (used))));
            //				}
            //				setPixelByte(col, row, endPixelByte);
            //			}
            //
            //			srcCol = 0;
            //			srcRow++;
            int minWidth = srcWidth;
            if (x + srcWidth > width)
            {
                //Should not happen but occurs sometimes because there is something wrong with halftone pics
                minWidth = width - x;
            }
            if (y + srcHeight > height)
            {
                //Should not happen but occurs sometimes because there is something wrong with halftone pics
                srcHeight = height - y;
            }

            int srcIndx = 0;
            int indx = y * width + x;
            if (combOp == 0)
            {
                if (x == 0 && y == 0 && srcHeight == height && srcWidth == width)
                {
                    for (int i = 0; i < data.w.Length; i++)
                    {
                        data.w[i] |= bitmap.data.w[i];
                    }
                }
                for (int row = y; row < y + srcHeight; row++)
                {
                    indx = row * width + x;
                    data.or(indx, bitmap.data, srcIndx, minWidth);
                    /*for (int col = 0; col < minWidth; col++) {
                        if (bitmap.data.get(srcIndx + col)) data.set(indx);
                        //data.set(indx, bitmap.data.get(srcIndx + col) || data.get(indx));
                        indx++;
                    }*/
                    srcIndx += srcWidth;
                }
            }
            else if (combOp == 1)
            {
                if (x == 0 && y == 0 && srcHeight == height && srcWidth == width)
                {
                    for (int i = 0; i < data.w.Length; i++)
                    {
                        data.w[i] &= bitmap.data.w[i];
                    }
                }
                for (int row = y; row < y + srcHeight; row++)
                {
                    indx = row * width + x;
                    for (int col = 0; col < minWidth; col++)
                    {
                        data.set(indx, bitmap.data.get(srcIndx + col) && data.get(indx));
                        indx++;
                    }
                    srcIndx += srcWidth;
                }
            }

            else if (combOp == 2)
            {
                if (x == 0 && y == 0 && srcHeight == height && srcWidth == width)
                {
                    for (int i = 0; i < data.w.Length; i++)
                    {
                        data.w[i] ^= bitmap.data.w[i];
                    }
                }
                else
                {
                    for (int row = y; row < y + srcHeight; row++)
                    {
                        indx = row * width + x;
                        for (int col = 0; col < minWidth; col++)
                        {
                            data.set(indx, bitmap.data.get(srcIndx + col) ^ data.get(indx));
                            indx++;
                        }
                        srcIndx += srcWidth;
                    }
                }
            }

            else if (combOp == 3)
            {
                for (int row = y; row < y + srcHeight; row++)
                {
                    indx = row * width + x;
                    for (int col = 0; col < minWidth; col++)
                    {
                        bool srcPixel = bitmap.data.get(srcIndx + col);
                        bool pixel = data.get(indx);
                        data.set(indx, pixel == srcPixel);
                        indx++;
                    }
                    srcIndx += srcWidth;
                }
            }

            else if (combOp == 4)
            {
                if (x == 0 && y == 0 && srcHeight == height && srcWidth == width)
                {
                    for (int i = 0; i < data.w.Length; i++)
                    {
                        data.w[i] = bitmap.data.w[i];
                    }
                }
                else
                {
                    for (int row = y; row < y + srcHeight; row++)
                    {
                        indx = row * width + x;
                        for (int col = 0; col < minWidth; col++)
                        {
                            data.set(indx, bitmap.data.get(srcIndx + col));
                            srcIndx++;
                            indx++;
                        }
                        srcIndx += srcWidth;
                    }
                }
            }
        }

        public byte[] getData(bool switchPixelColor) {
//		byte[] bytes = new byte[height * line];
//
//		for (int i = 0; i < height; i++) {
//			System.arraycopy(data.bytes[i], 0, bytes, line * i, line);
//		}
//
//		for (int i = 0; i < bytes.length; i++) {
//			// reverse bits
//
//			int value = bytes[i];
//			value = (value & 0x0f) << 4 | (value & 0xf0) >> 4;
//			value = (value & 0x33) << 2 | (value & 0xcc) >> 2;
//			value = (value & 0x55) << 1 | (value & 0xaa) >> 1;
//
//			if (switchPixelColor) {
//				value ^= 0xff;
//			}
//
//			bytes[i] = (byte) (value & 0xFF);
//		}
//
//		return bytes;
		byte[] bytes = new byte[height * line];

		int count = 0, offset = 0;
		long k = 0;
		for (int row = 0; row < height; row++) {
			for (int col = 0; col < width; col++) {
				if ((count & FastBitSet.mask) == 0) {
					k = data.w[((uint)count) >> FastBitSet.pot];
   				}
				//if ((k & (1L << count)) != 0) {
                //if ((offset >> 3) == 908)
                //{
                //    Debug.WriteLine(String.Format("row={0},col={1},k={2}", row, col, k));
                //}

					int bit = 7 - (offset & 0x7);
					//bytes[offset >> 3] |= (byte)( ( ( ((UInt64)k) >> count) & 1) << bit);
                    bytes[offset >> 3] |= (byte)(((((ulong)k) >> count) & 1) << bit);
                //ulong
				//}
				count++;
				offset++;
			}

			offset = (line * 8 * (row + 1));
		}

		if (switchPixelColor) {
			for (int i = 0; i < bytes.Length; i++) {
				bytes[i] ^= 0xff;
			}
		}

		return bytes;
	}

        public void setBitmapNumber(int segmentNumber)
        {
            this.bitmapNumber = segmentNumber;
        }

        //public int getBitmapNumber()
        //{
        //    return bitmapNumber;
        //}
    }
    
}
