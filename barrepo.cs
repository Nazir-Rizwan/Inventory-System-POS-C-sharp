using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;


namespace Nazam_pos
{
    public partial class barrepo : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        DataTable dt;
       

        public barrepo()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            loadproduct();
        }

        private void barrepo_Load(object sender, EventArgs e)
        {

            dt = new DataTable();
            dt.TableName = "barcode";
            dt.Columns.Add("barcode", typeof(string));
            dt.Columns.Add("price", typeof(decimal));
            dt.Columns.Add("pname", typeof(string));


        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                for (int i = 1; i <= int.Parse(textcpy.Text); i++)
                {
                    ReportDocument rdlc = new ReportDocument();
                    dt.Rows.Add(textBox1.Text, textBox2.Text, comboBox1.Text);

                    string reportpath = @"Reports\CrystalReport1.rpt";

                    rdlc.Load(reportpath);

                    rdlc.SetDataSource(dt);
                    crystalReportViewer1.ReportSource = rdlc;

                }




            }
            catch(Exception ex)
            {
                MessageBox.Show("Barcode Form Error " + ex.Message+MessageBoxButtons.OK+ MessageBoxIcon.Warning);
            }




           


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("Select * from tblProducts where pname like '" + comboBox1.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                //lblVendorID.Text = dr["id"].ToString();
                textBox1.Text = dr["barcode"].ToString();
                textBox2.Text = dr["price"].ToString();
               // textBox3.Text = dr["qty"].ToString();
               
                // textBox4.Text = dr["pname"].ToString();
            }
            dr.Close();
            cn.Close();
        }


        public void loadproduct()
        {
            cn.Open();
            comboBox1.Items.Clear();
            
            cm = new SqlCommand("Select * from tblProducts", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["pname"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
