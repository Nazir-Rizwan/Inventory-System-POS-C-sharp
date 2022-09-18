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
    public partial class FMBRAND2 : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        fmbrandlist frmlist;
        string title = "Inventory Brand";
        SqlDataReader dr;
       // DataSet1 ds = new DataSet1();
        public FMBRAND2(fmbrandlist flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            frmlist = flist;
        }

        private void FMBRAND2_Load(object sender, EventArgs e)
        {

        }

        private void Clear()
        {
            btnsave.Enabled = true;
            btnupdate.Enabled = false;
            textBrand.Clear();
            textBrand.Focus();
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                bool found = false;

                if (textBrand.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Brand Text ", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBrand.Focus();
                    return;
                }



                cn.Open();
                cm = new SqlCommand("select  brand from tblBrand where brand ='" + textBrand.Text + "'", cn);
                dr = cm.ExecuteReader();
                dr.Read();
               
                if (dr.HasRows)
                {

                    found = true;
                }
                else
                {
                    found = false;
                }
                cn.Close();
                dr.Close();

                if (found == true)
                {
                    MessageBox.Show("Already Brand exist ?", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;

                }

                else
                {

                    if (MessageBox.Show("Are you sure you want to save this brand? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {


                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblBrand(Brand)VALUES(@brand)", cn);
                        cm.Parameters.AddWithValue("@brand", textBrand.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show(" Brand has beeen Successfully saved .");
                        Clear();
                        frmlist.loadrecord();
                    }

                }
                cn.Close();
                // cn.Open();
                //cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this brand?", "Update Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();

                    cm = new SqlCommand(" update tblBrand set brand = @brand where id like '" + lblID.Text + "'", cn);
                    cm.Parameters.AddWithValue("@brand", textBrand.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Brand has been Successfully updated. ");
                    Clear();
                    frmlist.loadrecord();
                    this.Dispose();


                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                cn.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
