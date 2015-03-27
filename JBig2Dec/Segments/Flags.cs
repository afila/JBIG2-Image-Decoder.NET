using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBig2Dec
{
    abstract class Flags
    {
        protected int flagsAsInt;

        protected Dictionary<string, int> flags = new Dictionary<string, int>();

        public int getFlagValue(String key)
        {
            int value = 0;
            flags.TryGetValue(key, out value);
            return value;
        }

        public abstract void setFlags(int flagsAsInt);
    }
}
