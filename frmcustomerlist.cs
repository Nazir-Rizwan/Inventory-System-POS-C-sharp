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
    public partial class frmcustomerlist : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;

        public frmcustomerlist()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmcustomer frm = new frmcustomer(this);
            frm.BTNSave.Enabled = true;
            frm.btnupdate.Enabled = false;
            frm.ShowDialog();
            
        }
        public void loadrecords()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("Select * from tblCustomer", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());

            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                frmcustomer f = new frmcustomer(this);
                f.lblid.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.textcustomer.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.textAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.texttelephone.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.textemail.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.textcnic.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.BTNSave.Enabled = false;
                f.btnupdate.Enabled = true;
                f.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Delete this Record? Click Yes to Confirm", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from tblCustomer where id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();


                    cn.Close();
                    MessageBox.Show("Records has been Successfully Deleted", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadrecords();
                }
            }


        
    }
    }
}
