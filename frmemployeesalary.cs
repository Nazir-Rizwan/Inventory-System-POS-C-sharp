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
    public partial class frmemployeesalary : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;

        public frmemployeesalary()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void textemployeeid_TextChanged(object sender, EventArgs e)
        {
            cm = new SqlCommand("Select top(10) Empid from tblemployee where empid like '" + textemployeeid.Text + "%'");

            //dataGridView2.DataSource = LoadsRecords();
        }
        public void LoadsRecords()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("Select * from tblEmployee", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

                i++;
                dataGridView1.Rows.Add(i, dr[1].ToString(), dr[2].ToString());

            }
            dr.Close();
            cn.Close();
        }

    }
}
