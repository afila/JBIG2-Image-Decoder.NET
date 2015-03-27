using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using JBig2Dec;

namespace WindowsFormsApplication1
{
    public partial class Viewer : Form
    {
       // private static JBIG2Decoder decoder = new JBIG2Decoder();
       // private Image img = null;


        public Viewer()
        {
            InitializeComponent();
        }

        private void tsbtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Resources\\";
            dlg.Filter = "JBIG2 Files (*.jb2; *.jbig2)|*.jb2;*.jbig2|All Files (*.*)|*.*";
             

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                JBIG2Decoder decoder = new JBIG2Decoder();
   
              //  try
             //   {
                    //decode JBIG2
                    decoder.decodeJBIG2(dlg.FileName); //decode

                    //get image
                    picJbig2.Image = decoder.getPageAsBufferedImage(0);
                    //picJbig2.Scale(0.1f);
                    

               // }
               // catch (Exception ex)
               // {
               //    MessageBox.Show(ex.Message, ex.Source);
               // }

            }

        }

        private void tbarSize_Scroll(object sender, EventArgs e)
        {
            tooltipSize.SetToolTip(tbarSize, tbarSize.Value.ToString());
            //picJbig2.Scale(0.1f);
            //picJbig2.Update();

           // picJbig2.Scale(new SizeF(tbarSize.Value, tbarSize.Value));
           
          //  picJbig2.Invalidate();
           // picJbig2.Update();
            
           // tbarSize.Value
        }

    }
}
