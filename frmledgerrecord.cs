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
    public partial class frmledgerrecord : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        public frmledgerrecord()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            loadrecord1();
            loadrecord2();
        }

        public void loadrecord1()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select * from tblledger   where name not like 'Walk in Customer' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;

                dataGridView1.Rows.Add(i, dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());

            }
            dr.Close();
            cn.Close();
        }
        public void loadrecord2()
        {
            int i = 0;
            dataGridView2.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select * from tblledger   where name  like 'Walk in Customer' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;

                dataGridView2.Rows.Add(i, dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());

            }
            dr.Close();
            cn.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

       
        
        
        
        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
          

        }

       

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textcredit.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }
        private void textamount_TextChanged(object sender, EventArgs e)
        {
            try
            {

                //double cash = double.Parse(textamount.Text);

                //double credit = double.Parse(textcredit.Text);
                //double change = cash + credit;
                //textremaining.Text = change.ToString("#,##0.00");

            }

            catch (Exception ex)
            {
                textremaining.Text = "0.00";
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("update tblledger set credit =credit + " + double.Parse(textamount.Text.ToString()) + "where name = '" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'", cn);

            cm.ExecuteNonQuery();

            cn.Close();
            MessageBox.Show("update succwesfully ");
        }
    }

}
