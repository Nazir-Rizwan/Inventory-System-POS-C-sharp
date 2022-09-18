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
    public partial class frmcustomer : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        frmcustomerlist f;
        public frmcustomer(frmcustomerlist f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            this.f = f;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {

           // textname.Clear();
            textAddress.Clear();
            texttelephone.Clear();
            textemail.Clear();
            textemail.Clear();
            textcnic.Clear();
            textcustomer.Focus();
            BTNSave.Enabled = true;
            btnupdate.Enabled = false;

        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Save this Records? Click Yes to Confirm.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();

                    cm = new SqlCommand("insert into tblCustomer (customer , address ,telephone,email,cnic) values (@customer , @address, @telephone,@email,@cnic)", cn);
                    cm.Parameters.AddWithValue("@customer", textcustomer.Text);
                    cm.Parameters.AddWithValue("@address", textAddress.Text);
                    cm.Parameters.AddWithValue("@telephone", texttelephone.Text);
                    cm.Parameters.AddWithValue("@email", textemail.Text);
                    cm.Parameters.AddWithValue("@cnic", textcnic.Text);
                    cm.ExecuteNonQuery();

                    cn.Close();

                    MessageBox.Show("Records has been successfully saved", "Saved Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.loadrecords();
                    
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Update this Records? Click Yes to Confirm.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();

                    cm = new SqlCommand("UPDATE tblCustomer set customer=@customer , address=@address, telephone=@telephone, email=@email, cnic=@cnic where id=@id", cn);
                    cm.Parameters.AddWithValue("@customer", textcustomer.Text);
                    cm.Parameters.AddWithValue("@address", textAddress.Text);
                    cm.Parameters.AddWithValue("@telephone", texttelephone.Text);
                    cm.Parameters.AddWithValue("@email", textemail.Text);
                    cm.Parameters.AddWithValue("@cnic", textcnic.Text);
                    cm.Parameters.AddWithValue("@id", lblid.Text);
                    cm.ExecuteNonQuery();

                    cn.Close();

                    MessageBox.Show("Records has been successfully Updated", "Saved Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.loadrecords();
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
