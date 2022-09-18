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
    public partial class frmCancelDetails : Form
    {
        frmSolditem f;

        public frmCancelDetails(frmSolditem frm)
        {
            InitializeComponent();
            f = frm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void frmCancelDetails_Load(object sender, EventArgs e)
        {

        }

        private void cboAction_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            try
            {
                if((cboAction.Text!=string.Empty) && (txtQty.Text!=string.Empty) && (txtReason.Text!=string.Empty))
                {
                    if(int.Parse(txtQty.Text) >= int.Parse(txtCancelQty.Text))
                    {
                        frmVoid f = new frmVoid(this);
                        f.ShowDialog();
                    }
                    
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    public void RefreshList()
        {
            f.LoadRecord();
        }
    
    }
}
