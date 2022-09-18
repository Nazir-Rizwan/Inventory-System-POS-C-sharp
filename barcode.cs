using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nazam_pos
{
    public partial class barcode : Form
    {
        public barcode()
        {
            InitializeComponent();
        }

        private void btngenerate_Click(object sender, EventArgs e)
        {
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
            Image img=barcode.Encode(BarcodeLib.TYPE.UPCA,textbarcode.Text,Color.Black,Color.White,100,30);
            pictureBox1.Image = img;
        }
    }
}
