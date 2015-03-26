using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    class GenericRegionFlags: Flags
    {
        public  const String MMR = "MMR";
        public  const String GB_TEMPLATE = "GB_TEMPLATE";
        public  const String TPGDON = "TPGDON";

        public override void setFlags(int flagsAsInt) {
		    this.flagsAsInt = flagsAsInt;
		
		    /** extract MMR */
		    flags.Add(MMR, (flagsAsInt & 1));
		
		    /** extract GB_TEMPLATE */
		    flags.Add(GB_TEMPLATE, ((flagsAsInt >> 1) & 3));
		
		    /** extract TPGDON */
		    flags.Add(TPGDON, ((flagsAsInt >> 3) & 1));
		
		
		    if(JBIG2StreamDecoder.debug)    Debug.WriteLine(flags);
	    }
    }
}
