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
    public partial class fmbrandlist : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr ;
        DBconnection dbcon = new DBconnection();



        public fmbrandlist()
        {
            InitializeComponent();

            cn = new SqlConnection(dbcon.Myconnection());
            loadrecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FMBRAND2 frm = new FMBRAND2(this);
            
            
            frm.btnsave.Enabled = true;
            frm.btnupdate.Enabled = false;
            frm.ShowDialog();


            

        }

        public void loadrecord()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblBrand order by brand ", cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString()); 
            }
            dr.Close();
            cn.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                FMBRAND2 frm = new FMBRAND2(this);
                frm.lblID.Text= dataGridView1[1, e.RowIndex].Value.ToString();

                frm.textBrand.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                frm.ShowDialog();
            }

            else if (colName == "Delete")
            {
                
            if(MessageBox.Show("Are you sure you wnant to delete this record ?", "Delete Record ",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from tblBrand where id like '" + dataGridView1[1, e.RowIndex].Value.ToString()+ "'",cn);

                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Brand has been successfully deleted. ", "POS", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    loadrecord();
                }
            }


        }
    }
    }

