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
using System.Windows.Forms.DataVisualization.Charting;
namespace Nazam_pos
{
    public partial class frmDashboard : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;

        public frmDashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            LoadChart();
            //ik or method ha iska 
            //cn= new SqlConnection();
            //cn.ConnectionString = dbcon.MyConnection();

                    }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmDashboard_Resize(object sender, EventArgs e)
        {
            //panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        public void LoadChart()
        {
            cn.Open();
            
            SqlDataAdapter da = new SqlDataAdapter("select Year(sdate) as year, isnull (sum(total),0) as total from tblcart where status like 'Sold' group by year (sdate)", cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Sales");
            chart1.DataSource = ds.Tables["Sales"];
            Series series1 = chart1.Series["Series1"];
            series1.ChartType = SeriesChartType.Doughnut;
            series1.Name = "Sales";
            var chart = chart1;
            chart.Series[series1.Name].XValueMember="Year";
            chart.Series[series1.Name].YValueMembers = "Total";
            chart.Series[0].IsValueShownAsLabel = true;
            //chart.Series[0].LegendText = "";
            //chart.Series[0].
            cn.Close();
        }
    }
}
