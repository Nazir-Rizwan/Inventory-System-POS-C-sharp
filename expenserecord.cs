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
    public partial class expenserecord : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        public expenserecord()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            //loadrecord();
        }

        //public void LoadRecords()
        //{
        //    int i = 0;
        //    dataGridView1.Rows.Clear();
        //    cn.Open();
        //    cm = new SqlCommand("Select * from tblexpense where sdate like '%" + txtSearch.Text + "%' order by sdate", cn);
        //    dr = cm.ExecuteReader();
        //    while (dr.Read())

        //    {
        //        i++;
        //        dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
        //    }
        //    dr.Close();
        //    cn.Close();


        //}

        public void loadrecord()
        {
            try
            {


                int i = 0;
                double _total = 0;
                dataGridView1.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("Select * from tblexpense where type  like '%" + metroTextBox1.Text.Trim() + "%'", cn);



                dr = cm.ExecuteReader();
                while (dr.Read())

                {
                    i++;
                    dataGridView1.Rows.Add(i, dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());

                    //cm = new SqlCommand("Select amount from tblexpense where status like 'Paid'", cn);
                    _total += double.Parse(dr["amount"].ToString());

                }
                dr.Close();
                cn.Close();
                lbltotal.Text = _total.ToString("#,##0.00");

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message , "Expense Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadrecord();

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            loadrecord();
        }

        private void expenserecord_Load(object sender, EventArgs e)
        {
            this.ActiveControl = metroTextBox1;
            loadrecord();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            addExpense exp = new addExpense();
            exp.Show();
        }
    }
}
