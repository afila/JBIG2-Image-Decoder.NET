using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBig2Dec
{
    public class ArithmeticDecoder
    {
        private StreamReader reader;

        public ArithmeticDecoderStats genericRegionStats, refinementRegionStats;

        public ArithmeticDecoderStats iadhStats, iadwStats, iaexStats, iaaiStats, iadtStats, iaitStats, iafsStats, iadsStats, iardxStats, iardyStats, iardwStats, iardhStats, iariStats, iaidStats;


        public ArithmeticDecoder(StreamReader reader)
        {
            this.reader = reader;
            throw new NotImplementedException();

            //genericRegionStats = new ArithmeticDecoderStats(1 << 1);
            //refinementRegionStats = new ArithmeticDecoderStats(1 << 1);

            //iadhStats = new ArithmeticDecoderStats(1 << 9);
            //iadwStats = new ArithmeticDecoderStats(1 << 9);
            //iaexStats = new ArithmeticDecoderStats(1 << 9);
            //iaaiStats = new ArithmeticDecoderStats(1 << 9);
            //iadtStats = new ArithmeticDecoderStats(1 << 9);
            //iaitStats = new ArithmeticDecoderStats(1 << 9);
            //iafsStats = new ArithmeticDecoderStats(1 << 9);
            //iadsStats = new ArithmeticDecoderStats(1 << 9);
            //iardxStats = new ArithmeticDecoderStats(1 << 9);
            //iardyStats = new ArithmeticDecoderStats(1 << 9);
            //iardwStats = new ArithmeticDecoderStats(1 << 9);
            //iardhStats = new ArithmeticDecoderStats(1 << 9);
            //iariStats = new ArithmeticDecoderStats(1 << 9);
            //iaidStats = new ArithmeticDecoderStats(1 << 1);
        }

        internal void start()
        {
            throw new NotImplementedException();
        }
        internal void resetGenericStats(int template, ArithmeticDecoderStats previousStats)
        {
            throw new NotImplementedException();
        }
    }
}
