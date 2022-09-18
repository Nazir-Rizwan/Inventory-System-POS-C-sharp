using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Nazam_pos
{
      public partial class frmDiscount : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        string stitle = "Inventory Management system ";
        fmPOS f;



        public frmDiscount(fmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            f = frm;
            this.KeyPreview = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmDiscount_Load(object sender, EventArgs e)
        {

        }

        private void textPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {

               
                
                double discount = Double.Parse(txtPrice.Text) * Double.Parse(textPercent.Text)/100;
                textAmount.Text = discount.ToString("#,##0.00");
            }catch(Exception ex)
            {
                textAmount.Text = "0.00";
            }

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Add discount? Click yes to confirm.", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("  update tblCart set disc =@disc , disc_percent=@disc_percent where id = @id ", cn);
                    cm.Parameters.AddWithValue("@disc", Double.Parse(textAmount.Text));
                    cm.Parameters.AddWithValue("@disc_percent", Double.Parse(textPercent.Text));

                    cm.Parameters.AddWithValue("@id", int.Parse(lblD.Text));
                    cm.ExecuteNonQuery();

                    cn.Close();
                    f.loadcart();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }

        }

        private void lblD_Click(object sender, EventArgs e)
        {

        }

        private void frmDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
