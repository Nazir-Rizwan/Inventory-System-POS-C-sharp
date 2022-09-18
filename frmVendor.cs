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
    public partial class frmVendor : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        frmVendorList f;


        public frmVendor(frmVendorList f)
        {
            InitializeComponent();

            cn = new SqlConnection(dbcon.Myconnection());
            this.f = f;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmVendor_Load(object sender, EventArgs e)
        {

        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Save this Records? Click Yes to Confirm.","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();

                    cm = new SqlCommand("insert into tblVendor (vendor , address, contactperson,telephone,email,fax) values (@vendor ,@address,@contactperson,@telephone,@email,@fax)", cn);
                    cm.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@contactperson", txtContact.Text);
                    cm.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                    cm.Parameters.AddWithValue("@email", txtEmail.Text);
                    cm.Parameters.AddWithValue("@fax", txtFax.Text);
                    cm.ExecuteNonQuery();

                    cn.Close();
                   
                    MessageBox.Show("Records has been successfully saved", "Saved Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.LoadsRecords();
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Clear()
        {
            
            txtAddress.Clear();
            txtContact.Clear();
            txtTelephone.Clear();
            txtEmail.Clear();
            txtFax.Clear();
            txtVendor.Clear();
            txtVendor.Focus();
            BTNSave.Enabled = true;
            btnupdate.Enabled = false;

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Update this Records? Click Yes to Confirm.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();

                    cm = new SqlCommand("UPDATE tblVendor set vendor=@vendor , address=@address, contactperson=@contactperson, telephone=@telephone, email=@email, fax=@fax where id=@id" , cn);
                    cm.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@contactperson", txtContact.Text);
                    cm.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                    cm.Parameters.AddWithValue("@email", txtEmail.Text);
                    cm.Parameters.AddWithValue("@fax", txtFax.Text);
                    cm.Parameters.AddWithValue("@id",labelID.Text);
                    cm.ExecuteNonQuery();

                    cn.Close();

                    MessageBox.Show("Records has been successfully Updated", "Saved Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.LoadsRecords();
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
