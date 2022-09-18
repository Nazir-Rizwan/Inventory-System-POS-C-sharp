using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nazam_pos
{
    public partial class barcoderdlc : Form
    {
        public barcoderdlc()
        {
            InitializeComponent();
        }

        private void btngenerate_Click(object sender, EventArgs e)
        {
            //int id = 1;
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
            Image img = barcode.Encode(BarcodeLib.TYPE.UPCA, textbarcode.Text, Color.Black, Color.White, 100, 30);
            pictureBox1.Image = img;
            using (MemoryStream ms =  new MemoryStream() )
            {
                img.Save(ms, ImageFormat.Png);

               // for (int i = 0; i < number.Value; i++)

                   // this.dataSet11.Barcode.AddBarcodeRow(textbarcode.Text, ms.ToArray());

                
            }

            //using ()
            //frmbarcodereports frm = new frmbarcodereports(this.dataSet11.Barcode))
            //{
                //frm.ShowDialog();
            //}

        }
    }
}
