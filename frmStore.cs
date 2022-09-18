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
    public partial class frmStore : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;

        public frmStore()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadRecords()
        {
            cn.Open();
            cm = new SqlCommand("select * from tblStore",cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                txtAddress.Text = dr["address"].ToString();
                textStore.Text = dr["store"].ToString();
            }
            else
            {
                textStore.Clear();
                txtAddress.Clear();
            }
            dr.Close();
            cn.Close();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Save Store Details ?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    int count;
                    cn.Open();
                    cm = new SqlCommand(" select count(*) from tblStore", cn);
                    count = int.Parse(cm.ExecuteScalar().ToString());

                    cn.Close();
                    if( count >0)
                    {
                        cn.Open();
                        cm = new SqlCommand("update tblStore set store=@store , address =@address", cn);
                        cm.Parameters.AddWithValue("@store",textStore.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.ExecuteNonQuery();
                    }
                    else
                    {
                        cn.Open();
                        cm = new SqlCommand("insert into tblStore (store ,address  ) values (@store ,@address)", cn);
                        cm.Parameters.AddWithValue("@store", textStore.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.ExecuteNonQuery();
                    }
                    MessageBox.Show("Store Details has been successfully saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
    }
}
