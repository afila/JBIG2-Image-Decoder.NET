using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    class PageInformationFlags: Flags
    {
        public static String DEFAULT_PIXEL_VALUE = "DEFAULT_PIXEL_VALUE";
        public static String DEFAULT_COMBINATION_OPERATOR = "DEFAULT_COMBINATION_OPERATOR";

        public override void setFlags(int flagsAsInt) {
		    this.flagsAsInt = flagsAsInt;

		    /** extract DEFAULT_PIXEL_VALUE */
		    flags.Add(DEFAULT_PIXEL_VALUE, ((flagsAsInt >> 2) & 1));

		    /** extract DEFAULT_COMBINATION_OPERATOR */
		    flags.Add(DEFAULT_COMBINATION_OPERATOR, ((flagsAsInt >> 3) & 3));

            if (JBIG2StreamDecoder.debug)
            {
                flags.ToList().ForEach(x => Debug.Write(String.Format("{0}={1} ", x.Key, x.Value)));//Debug.WriteLine(flags);
                Debug.WriteLine("");
            }
	    }
    }
}
