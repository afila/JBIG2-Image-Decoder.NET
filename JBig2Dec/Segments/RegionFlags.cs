using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    class RegionFlags: Flags
    {
        public static String EXTERNAL_COMBINATION_OPERATOR = "EXTERNAL_COMBINATION_OPERATOR";

        public override void setFlags(int flagsAsInt) {
		    this.flagsAsInt = flagsAsInt;

		    /** extract EXTERNAL_COMBINATION_OPERATOR */
		    flags.Add(EXTERNAL_COMBINATION_OPERATOR, (flagsAsInt & 7));

            if (JBIG2StreamDecoder.debug)
            {   //Debug.WriteLine(flags);
                flags.ToList().ForEach(x => Debug.Write(String.Format("{0}={1} ", x.Key, x.Value)));//Debug.WriteLine(flags);
                Debug.WriteLine("");

            }
	    }
    }
}
