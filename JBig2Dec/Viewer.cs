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
using System.Drawing.Imaging;

namespace WindowsFormsApplication1
{
    public partial class Viewer : Form
    {
        public Viewer()
        {
            InitializeComponent();
        }

        //Open JBIG2 image
        private void tsbtnOpen_Click(object sender, EventArgs e)
        {
            // init OpenFileDialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Resources\\";
            dlg.Filter = "JBIG2 Files (*.jb2; *.jbig2)|*.jb2;*.jbig2|All Files (*.*)|*.*";

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                JBIG2Decoder decoder = new JBIG2Decoder();
   
                try
                {
                    //decode JBIG2
                    decoder.decodeJBIG2(dlg.FileName); //decode

                    //get image
                    picJbig2.Image = decoder.getPageAsBufferedImage(0);

                    tbarSize.Value = 10; // 10/10=1 (Actual size)
                    tbarSize_Scroll(null, null);

                }
                catch (Exception ex)
                {
                   MessageBox.Show(ex.Message, ex.Source);
                }
            }
        }
        

        private void tbarSize_Scroll(object sender, EventArgs e)
        {
            //set toolTip
            float factor = ((float)tbarSize.Value) / 10;
            tooltipSize.SetToolTip(tbarSize, String.Format("x{0:0.0}", factor ));
            
            //Scale Image
            picJbig2.Size = new Size((int)(picJbig2.Image.Width * factor), (int)(picJbig2.Image.Height * factor));
            //picJbig2.Update(); //---------
        }

        //Save JBIG2 image
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if (picJbig2.Image == null)
            {
                MessageBox.Show("Nothing to save");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
                        
            dlg.Filter = "Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png|Tiff Image|*.tiff";
            dlg.Title = "Save an JBIG2 Image as";


            // If the file name is not an empty string open it for saving.
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.FileName != "")
                {
                    ImageFormatConverter conv = new ImageFormatConverter();

                    ImageFormat fileFormat = null;
                    try
                    {
                        fileFormat = (ImageFormat)conv.ConvertFromString(Path.GetExtension(dlg.FileName).Substring(1));
                        picJbig2.Image.Save(dlg.FileName, fileFormat);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.Source);
                    }
                    
                }
            }
        }

    }
}
