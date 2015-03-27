using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    class MMRDecoder
    {
        public const int ccittEndOfLine = -2;

	    public const int twoDimensionalPass = 0;
	    public const int twoDimensionalHorizontal = 1;
	    public const int twoDimensionalVertical0 = 2;
	    public const int twoDimensionalVerticalR1 = 3;
	    public const int twoDimensionalVerticalL1 = 4;
	    public const int twoDimensionalVerticalR2 = 5;
	    public const int twoDimensionalVerticalL2 = 6;
	    public const int twoDimensionalVerticalR3 = 7;
	    public const int twoDimensionalVerticalL3 = 8;

        private int[][] twoDimensionalTable1 = {           
             new int []{ -1, -1 },  new int []{ -1, -1 },  new int []{ 7, twoDimensionalVerticalL3 },  new int []{ 7, twoDimensionalVerticalR3 }, new int []{ 6, twoDimensionalVerticalL2 }, new int []{ 6, twoDimensionalVerticalL2 },  new int []{ 6, twoDimensionalVerticalR2 },  new int [] { 6, twoDimensionalVerticalR2 },  new int [] { 4, twoDimensionalPass },       new int [] { 4, twoDimensionalPass },       new int [] { 4, twoDimensionalPass },       new int [] { 4, twoDimensionalPass },       new int [] { 4, twoDimensionalPass },       new int [] { 4, twoDimensionalPass },       new int [] { 4, twoDimensionalPass },       new int [] { 4, twoDimensionalPass },       new int [] { 3, twoDimensionalHorizontal }, new int [] { 3, twoDimensionalHorizontal },  new int [] { 3, twoDimensionalHorizontal },  new int [] { 3, twoDimensionalHorizontal },
             new int []{ 3, twoDimensionalHorizontal },    new int []{ 3, twoDimensionalHorizontal },  new int []{ 3, twoDimensionalHorizontal }, new int []{ 3, twoDimensionalHorizontal }, new int []{ 3, twoDimensionalHorizontal }, new int [] { 3, twoDimensionalHorizontal }, new int [] { 3, twoDimensionalHorizontal },  new int [] { 3, twoDimensionalHorizontal }, new int [] { 3, twoDimensionalHorizontal }, new int [] { 3, twoDimensionalHorizontal }, new int [] { 3, twoDimensionalHorizontal }, new int [] { 3, twoDimensionalHorizontal }, new int [] { 3, twoDimensionalVerticalL1 }, new int [] { 3, twoDimensionalVerticalL1 }, new int [] { 3, twoDimensionalVerticalL1 }, new int [] { 3, twoDimensionalVerticalL1 }, new int [] { 3, twoDimensionalVerticalL1 },  new int [] { 3, twoDimensionalVerticalL1 },  new int [] { 3, twoDimensionalVerticalL1 }, new int [] { 3, twoDimensionalVerticalL1 }, 
             new int []{ 3, twoDimensionalVerticalL1 },    new int []{ 3, twoDimensionalVerticalL1 },  new int []{ 3, twoDimensionalVerticalL1 }, new int []{ 3, twoDimensionalVerticalL1 }, new int []{ 3, twoDimensionalVerticalL1 }, new int [] { 3, twoDimensionalVerticalL1 }, new int [] { 3, twoDimensionalVerticalL1 }, new int [] { 3, twoDimensionalVerticalL1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, 
             new int []{ 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 }, new int [] { 3, twoDimensionalVerticalR1 },  new int []{ 3, twoDimensionalVerticalR1 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, 
             new int []{ 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, 
             new int []{ 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, 
             new int []{ 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 }, new int [] { 1, twoDimensionalVertical0 } 
        };

        /** white run lengths */
        private int[][] whiteTable1 = { new int[] { -1, -1 }, new int [] { 12, ccittEndOfLine }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { 11, 1792 }, new int [] { 11, 1792 }, new int [] { 12, 1984 }, new int [] { 12, 2048 }, new int [] { 12, 2112 }, new int [] { 12, 2176 }, new int [] { 12, 2240 }, new int [] { 12, 2304 }, new int [] { 11, 1856 }, new int [] { 11, 1856 }, new int [] { 11, 1920 }, new int [] { 11, 1920 }, new int [] { 12, 2368 }, new int [] { 12, 2432 }, new int [] { 12, 2496 }, new int [] { 12, 2560 } };

        private int[][] whiteTable2 = { new int []{ -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { 8, 29 }, new int [] { 8, 29 }, new int [] { 8, 30 }, new int [] { 8, 30 }, new int [] { 8, 45 }, new int [] { 8, 45 }, new int [] { 8, 46 }, new int [] { 8, 46 }, new int [] { 7, 22 }, new int [] { 7, 22 }, new int [] { 7, 22 }, new int [] { 7, 22 }, new int [] { 7, 23 }, new int [] { 7, 23 }, new int [] { 7, 23 }, new int [] { 7, 23 }, new int [] { 8, 47 }, new int [] { 8, 47 }, new int [] { 8, 48 }, new int [] { 8, 48 }, new int [] { 6, 13 }, new int [] { 6, 13 }, new int [] { 6, 13 }, new int [] { 6, 13 }, new int [] { 6, 13 }, new int [] { 6, 13 }, new int [] { 6, 13 }, new int [] { 6, 13 }, new int [] { 7, 20 }, new int [] { 7, 20 }, new int [] { 7, 20 }, new int [] { 7, 20 }, new int [] { 8, 33 }, new int [] { 8, 33 }, new int [] { 8, 34 }, new int [] { 8, 34 }, new int [] { 8, 35 }, new int [] { 8, 35 }, new int [] { 8, 36 }, new int [] { 8, 36 }, new int [] { 8, 37 }, new int [] { 8, 37 }, new int [] { 8, 38 }, new int [] { 8, 38 }, new int [] { 7, 19 }, new int [] { 7, 19 }, new int []
			{ 7, 19 }, new int [] { 7, 19 }, new int [] { 8, 31 }, new int [] { 8, 31 }, new int [] { 8, 32 }, new int [] { 8, 32 }, new int [] { 6, 1 }, new int [] { 6, 1 }, new int [] { 6, 1 }, new int [] { 6, 1 }, new int [] { 6, 1 }, new int [] { 6, 1 }, new int [] { 6, 1 }, new int [] { 6, 1 }, new int [] { 6, 12 }, new int [] { 6, 12 }, new int [] { 6, 12 }, new int [] { 6, 12 }, new int [] { 6, 12 }, new int [] { 6, 12 }, new int [] { 6, 12 }, new int [] { 6, 12 }, new int [] { 8, 53 }, new int [] { 8, 53 }, new int [] { 8, 54 }, new int [] { 8, 54 }, new int [] { 7, 26 }, new int [] { 7, 26 }, new int [] { 7, 26 }, new int [] { 7, 26 }, new int [] { 8, 39 }, new int [] { 8, 39 }, new int [] { 8, 40 }, new int [] { 8, 40 }, new int [] { 8, 41 }, new int [] { 8, 41 }, new int [] { 8, 42 }, new int [] { 8, 42 }, new int [] { 8, 43 }, new int [] { 8, 43 }, new int [] { 8, 44 }, new int [] { 8, 44 }, new int [] { 7, 21 }, new int [] { 7, 21 }, new int [] { 7, 21 }, new int [] { 7, 21 }, new int [] { 7, 28 }, new int [] { 7, 28 }, new int [] { 7, 28 }, new int [] { 7, 28 }, new int [] { 8, 61 }, new int [] { 8, 61 }, new int []
			{ 8, 62 }, new int [] { 8, 62 }, new int [] { 8, 63 }, new int [] { 8, 63 }, new int [] { 8, 0 }, new int [] { 8, 0 }, new int [] { 8, 320 }, new int [] { 8, 320 }, new int [] { 8, 384 }, new int [] { 8, 384 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 10 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 5, 11 }, new int [] { 7, 27 }, new int [] { 7, 27 }, new int [] { 7, 27 }, new int [] { 7, 27 }, new int [] { 8, 59 }, new int [] { 8, 59 }, new int [] { 8, 60 }, new int [] { 8, 60 }, new int [] { 9, 1472 }, new int []
			{ 9, 1536 }, new int [] { 9, 1600 }, new int [] { 9, 1728 }, new int [] { 7, 18 }, new int [] { 7, 18 }, new int [] { 7, 18 }, new int [] { 7, 18 }, new int [] { 7, 24 }, new int [] { 7, 24 }, new int [] { 7, 24 }, new int [] { 7, 24 }, new int [] { 8, 49 }, new int [] { 8, 49 }, new int [] { 8, 50 }, new int [] { 8, 50 }, new int [] { 8, 51 }, new int [] { 8, 51 }, new int [] { 8, 52 }, new int [] { 8, 52 }, new int [] { 7, 25 }, new int [] { 7, 25 }, new int [] { 7, 25 }, new int [] { 7, 25 }, new int [] { 8, 55 }, new int [] { 8, 55 }, new int [] { 8, 56 }, new int [] { 8, 56 }, new int [] { 8, 57 }, new int [] { 8, 57 }, new int [] { 8, 58 }, new int [] { 8, 58 }, new int [] { 6, 192 }, new int [] { 6, 192 }, new int [] { 6, 192 }, new int [] { 6, 192 }, new int [] { 6, 192 }, new int [] { 6, 192 }, new int [] { 6, 192 }, new int [] { 6, 192 }, new int [] { 6, 1664 }, new int [] { 6, 1664 }, new int [] { 6, 1664 }, new int [] { 6, 1664 }, new int [] { 6, 1664 }, new int [] { 6, 1664 }, new int [] { 6, 1664 }, new int [] { 6, 1664 }, new int [] { 8, 448 }, new int []
			{ 8, 448 }, new int [] { 8, 512 }, new int [] { 8, 512 }, new int [] { 9, 704 }, new int [] { 9, 768 }, new int [] { 8, 640 }, new int [] { 8, 640 }, new int [] { 8, 576 }, new int [] { 8, 576 }, new int [] { 9, 832 }, new int [] { 9, 896 }, new int [] { 9, 960 }, new int [] { 9, 1024 }, new int [] { 9, 1088 }, new int [] { 9, 1152 }, new int [] { 9, 1216 }, new int [] { 9, 1280 }, new int [] { 9, 1344 }, new int [] { 9, 1408 }, new int [] { 7, 256 }, new int [] { 7, 256 }, new int [] { 7, 256 }, new int [] { 7, 256 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int []
			{ 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 2 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 4, 3 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 128 }, new int [] { 5, 8 }, new int []
			{ 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 8 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 5, 9 }, new int [] { 6, 16 }, new int [] { 6, 16 }, new int [] { 6, 16 }, new int [] { 6, 16 }, new int [] { 6, 16 }, new int [] { 6, 16 }, new int [] { 6, 16 }, new int [] { 6, 16 }, new int [] { 6, 17 }, new int [] { 6, 17 }, new int [] { 6, 17 }, new int [] { 6, 17 }, new int [] { 6, 17 }, new int [] { 6, 17 }, new int [] { 6, 17 }, new int [] { 6, 17 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int []
			{ 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 4 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int []
			{ 6, 14 }, new int [] { 6, 14 }, new int [] { 6, 14 }, new int [] { 6, 14 }, new int [] { 6, 14 }, new int [] { 6, 14 }, new int [] { 6, 14 }, new int [] { 6, 14 }, new int [] { 6, 15 }, new int [] { 6, 15 }, new int [] { 6, 15 }, new int [] { 6, 15 }, new int [] { 6, 15 }, new int [] { 6, 15 }, new int [] { 6, 15 }, new int [] { 6, 15 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 5, 64 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int []
			{ 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 }, new int [] { 4, 7 } };


        	/** black run lengths */
	   int[][] blackTable1 = { new int []{ -1, -1 }, new int [] { -1, -1 }, new int [] { 12, ccittEndOfLine }, new int [] { 12, ccittEndOfLine }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { 11, 1792 }, new int [] { 11, 1792 }, new int [] { 11, 1792 }, new int [] { 11, 1792 }, new int [] { 12, 1984 }, new int [] { 12, 1984 }, new int [] { 12, 2048 }, new int [] { 12, 2048 }, new int [] { 12, 2112 }, new int [] { 12, 2112 }, new int []
			{ 12, 2176 }, new int [] { 12, 2176 }, new int [] { 12, 2240 }, new int [] { 12, 2240 }, new int [] { 12, 2304 }, new int [] { 12, 2304 }, new int [] { 11, 1856 }, new int [] { 11, 1856 }, new int [] { 11, 1856 }, new int [] { 11, 1856 }, new int [] { 11, 1920 }, new int [] { 11, 1920 }, new int [] { 11, 1920 }, new int [] { 11, 1920 }, new int [] { 12, 2368 }, new int [] { 12, 2368 }, new int [] { 12, 2432 }, new int [] { 12, 2432 }, new int [] { 12, 2496 }, new int [] { 12, 2496 }, new int [] { 12, 2560 }, new int [] { 12, 2560 }, new int [] { 10, 18 }, new int [] { 10, 18 }, new int [] { 10, 18 }, new int [] { 10, 18 }, new int [] { 10, 18 }, new int [] { 10, 18 }, new int [] { 10, 18 }, new int [] { 10, 18 }, new int [] { 12, 52 }, new int [] { 12, 52 }, new int [] { 13, 640 }, new int [] { 13, 704 }, new int [] { 13, 768 }, new int [] { 13, 832 }, new int [] { 12, 55 }, new int [] { 12, 55 }, new int [] { 12, 56 }, new int [] { 12, 56 }, new int [] { 13, 1280 }, new int [] { 13, 1344 }, new int []
			{ 13, 1408 }, new int [] { 13, 1472 }, new int [] { 12, 59 }, new int [] { 12, 59 }, new int [] { 12, 60 }, new int [] { 12, 60 }, new int [] { 13, 1536 }, new int [] { 13, 1600 }, new int [] { 11, 24 }, new int [] { 11, 24 }, new int [] { 11, 24 }, new int [] { 11, 24 }, new int [] { 11, 25 }, new int [] { 11, 25 }, new int [] { 11, 25 }, new int [] { 11, 25 }, new int [] { 13, 1664 }, new int [] { 13, 1728 }, new int [] { 12, 320 }, new int [] { 12, 320 }, new int [] { 12, 384 }, new int [] { 12, 384 }, new int [] { 12, 448 }, new int [] { 12, 448 }, new int [] { 13, 512 }, new int [] { 13, 576 }, new int [] { 12, 53 }, new int [] { 12, 53 }, new int [] { 12, 54 }, new int [] { 12, 54 }, new int [] { 13, 896 }, new int [] { 13, 960 }, new int [] { 13, 1024 }, new int [] { 13, 1088 }, new int [] { 13, 1152 }, new int [] { 13, 1216 }, new int [] { 10, 64 }, new int [] { 10, 64 }, new int [] { 10, 64 }, new int [] { 10, 64 }, new int [] { 10, 64 }, new int [] { 10, 64 }, new int [] { 10, 64 }, new int [] { 10, 64 } };

       int[][] blackTable2 = { new int []{ 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 8, 13 }, new int [] { 11, 23 }, new int [] { 11, 23 }, new int [] { 12, 50 }, new int [] { 12, 51 }, new int [] { 12, 44 }, new int [] { 12, 45 }, new int [] { 12, 46 }, new int [] { 12, 47 }, new int [] { 12, 57 }, new int [] { 12, 58 }, new int [] { 12, 61 }, new int [] { 12, 256 }, new int [] { 10, 16 }, new int [] { 10, 16 }, new int [] { 10, 16 }, new int [] { 10, 16 }, new int [] { 10, 17 }, new int [] { 10, 17 }, new int [] { 10, 17 }, new int [] { 10, 17 }, new int [] { 12, 48 }, new int [] { 12, 49 }, new int [] { 12, 62 }, new int [] { 12, 63 }, new int [] { 12, 30 }, new int [] { 12, 31 }, new int [] { 12, 32 }, new int [] { 12, 33 }, new int [] { 12, 40 }, new int [] { 12, 41 }, new int [] { 11, 22 }, new int []
            { 11, 22 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 8, 14 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 10 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int []
            { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 7, 11 }, new int [] { 9, 15 }, new int [] { 9, 15 }, new int [] { 9, 15 }, new int [] { 9, 15 }, new int [] { 9, 15 }, new int [] { 9, 15 }, new int [] { 9, 15 }, new int [] { 9, 15 }, new int [] { 12, 128 }, new int [] { 12, 192 }, new int [] { 12, 26 }, new int [] { 12, 27 }, new int [] { 12, 28 }, new int [] { 12, 29 }, new int [] { 11, 19 }, new int [] { 11, 19 }, new int [] { 11, 20 }, new int [] { 11, 20 }, new int [] { 12, 34 }, new int [] { 12, 35 }, new int []
            { 12, 36 }, new int [] { 12, 37 }, new int [] { 12, 38 }, new int [] { 12, 39 }, new int [] { 11, 21 }, new int [] { 11, 21 }, new int [] { 12, 42 }, new int [] { 12, 43 }, new int [] { 10, 0 }, new int [] { 10, 0 }, new int [] { 10, 0 }, new int [] { 10, 0 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 }, new int [] { 7, 12 } };

        int[][] blackTable3 = { new int []{ -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { -1, -1 }, new int [] { 6, 9 }, new int [] { 6, 8 }, new int [] { 5, 7 }, new int [] { 5, 7 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 6 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 4, 5 }, new int [] { 3, 1 }, new int [] { 3, 1 }, new int [] { 3, 1 }, new int [] { 3, 1 }, new int [] { 3, 1 }, new int [] { 3, 1 }, new int [] { 3, 1 }, new int [] { 3, 1 }, new int [] { 3, 4 }, new int [] { 3, 4 }, new int [] { 3, 4 }, new int [] { 3, 4 }, new int [] { 3, 4 }, new int [] { 3, 4 }, new int [] { 3, 4 }, new int [] { 3, 4 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 3 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int []
            { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 }, new int [] { 2, 2 } };


        private StreamReader reader;
        private long bufferLength = 0, buffer = 0, noOfBytesRead = 0;

        public MMRDecoder(StreamReader reader)
        {
            this.reader = reader;
        }

        public void reset()
        {
            bufferLength = 0;
            noOfBytesRead = 0;
            buffer = 0;
        }

    
        public int get2DCode() 
        {
		    int[] tuple;

		    if (bufferLength == 0) {
			    buffer = (reader.readByte() & 0xff);

			    bufferLength = 8;

			    noOfBytesRead++;

			    int lookup = (int) ((BinaryOperation.bit32ShiftR(buffer, 1)) & 0x7f);

			    tuple = twoDimensionalTable1[lookup];
		    } 
            else if (bufferLength == 8) {
			        int lookup = (int) ((BinaryOperation.bit32ShiftR(buffer, 1)) & 0x7f);
			        tuple = twoDimensionalTable1[lookup];
		         } else {
			                int lookup = (int) ((BinaryOperation.bit32ShiftL(buffer, (int) (7 - bufferLength))) & 0x7f);

			                tuple = twoDimensionalTable1[lookup];
			                if (tuple[0] < 0 || tuple[0] > (int) bufferLength) {
				                int right = (reader.readByte() & 0xff);

				                long left = (BinaryOperation.bit32ShiftL(buffer, 8));

				                buffer = left | right;
				                bufferLength += 8;
				                noOfBytesRead++;

				                int look = (int) (BinaryOperation.bit32ShiftR(buffer, (int) (bufferLength - 7)) & 0x7f);

				                tuple = twoDimensionalTable1[look];
			                }
		                }
		        if (tuple[0] < 0) {
			        if(JBIG2StreamDecoder.debug)    Debug.WriteLine ("Bad two dim code in JBIG2 MMR stream");
			
			        return 0;
		        }
		        bufferLength -= tuple[0];
		
		    return tuple[1];
	    }


        public int getBlackCode()  {
		    int[] tuple;
		    long code;

		    if (bufferLength == 0) {
			    buffer = (reader.readByte() & 0xff);
			    bufferLength = 8;
			    noOfBytesRead++;
		    }
		    while (true) {
			    if (bufferLength >= 6 && ((BinaryOperation.bit32ShiftR(buffer, (int) (bufferLength - 6))) & 0x3f) == 0) {
				    if (bufferLength <= 13) {
					    code = BinaryOperation.bit32ShiftL(buffer, (int) (13 - bufferLength));
				    } else {
					        code = BinaryOperation.bit32ShiftR(buffer, (int) (bufferLength - 13));
				      }
				    tuple = blackTable1[(int) (code & 0x7f)];
			    } else if (bufferLength >= 4 && ((buffer >> (int)(bufferLength - 4)) & 0x0f) == 0) {
				            if (bufferLength <= 12) {
					            code = BinaryOperation.bit32ShiftL(buffer, (int) (12 - bufferLength));
				            } else {
					            code = BinaryOperation.bit32ShiftR(buffer, (int) (bufferLength - 12));
				              }

				            int lookup = (int) ((code & 0xff) - 64);
				            if (lookup >= 0)
					            tuple = blackTable2[lookup];
				            else
					            tuple = blackTable1[blackTable1.Length + lookup];
			            } else {
				                if (bufferLength <= 6) {
					                code = BinaryOperation.bit32ShiftL(buffer, (int) (6 - bufferLength));
				                } else {
					                code = BinaryOperation.bit32ShiftR(buffer, (int) (bufferLength - 6));
				                  }

				                int lookup = (int) (code & 0x3f);
				                if (lookup >= 0)
					                tuple = blackTable3[lookup];
				                else
					                tuple = blackTable2[blackTable2.Length + lookup];
			              }
			            if (tuple[0] > 0 && tuple[0] <= (int) bufferLength) {
				            bufferLength -= tuple[0];
				            return tuple[1];
			            }
			            if (bufferLength >= 13) {
				            break;
			            }
			    buffer = ((BinaryOperation.bit32ShiftL(buffer, 8)) | (reader.readByte() & 0xff));
			    bufferLength += 8;
			    noOfBytesRead++;
		}
		
		if(JBIG2StreamDecoder.debug)
			Debug.WriteLine("Bad black code in JBIG2 MMR stream");

		bufferLength--;
		return 1;
	}

     public int getWhiteCode() {
		int[] tuple;
		long code;

		if (bufferLength == 0) {
			buffer = (reader.readByte() & 0xff);
			bufferLength = 8;
			noOfBytesRead++;
		}
		while (true) {
			if (bufferLength >= 7 && ((BinaryOperation.bit32ShiftR(buffer, (int) (bufferLength - 7))) & 0x7f) == 0) {
				if (bufferLength <= 12) {
					code = BinaryOperation.bit32ShiftL(buffer, (int) (12 - bufferLength));
				} else {
					code = BinaryOperation.bit32ShiftR(buffer, (int) (bufferLength - 12));
				}

				tuple = whiteTable1[(int) (code & 0x1f)];
			} else {
				if (bufferLength <= 9) {
					code = BinaryOperation.bit32ShiftL(buffer, (int) (9 - bufferLength));
				} else {
					code = BinaryOperation.bit32ShiftR(buffer, (int) (bufferLength - 9));
				}

				int lookup = (int) (code & 0x1ff);
				if (lookup >= 0)
					tuple = whiteTable2[lookup];
				else
					tuple = whiteTable2[whiteTable2.Length + lookup];
			}
			if (tuple[0] > 0 && tuple[0] <= (int) bufferLength) {
				bufferLength -= tuple[0];
				return tuple[1];
			}
			if (bufferLength >= 12) {
				break;
			}
			buffer = ((BinaryOperation.bit32ShiftL(buffer, 8)) | reader.readByte() & 0xff);
			bufferLength += 8;
			noOfBytesRead++;
		}
		if(JBIG2StreamDecoder.debug)
			Debug.WriteLine("Bad white code in JBIG2 MMR stream");

		bufferLength--;
		
		return 1;
	}

     public long get24Bits() 
     {
		while (bufferLength < 24) {

			buffer = ((BinaryOperation.bit32ShiftL(buffer, 8)) | (reader.readByte() & 0xff));
			bufferLength += 8;
			noOfBytesRead++;
		}

		return (BinaryOperation.bit32ShiftR(buffer, (int) (bufferLength - 24))) & 0xffffff;
	 }


   	public void skipTo(int length) 
    {
		while (noOfBytesRead < length) {
			reader.readByte();
			noOfBytesRead++;
		}
	}

    }
}
