using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JBig2Dec
{
    class Program
    {
        static string fileName = @"d:\Мои документы\WorkSpace\JBIG2-Image-Decoder-master\5230A_A.jb2";

        private static JBIG2Decoder decoder;

        static void Main(string[] args)
        {



            decoder = new JBIG2Decoder();
            try
            {
                decoder.decodeJBIG2(fileName);
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                Debug.WriteLine( e.StackTrace);
            }
        }
    }
}
