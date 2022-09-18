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
using Microsoft.Reporting.WinForms;
namespace Nazam_pos
{
    public partial class frmReportSold : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        frmSolditem f;
        public frmReportSold(frmSolditem frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            f = frm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmReportSold_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
        }


       
        
        public void loadReport()
        {
            try
            {
                ReportDataSource rptDS;
                    
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report2.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                if (f.cboCashier.Text == "All Cashier")


                {
                    da.SelectCommand = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc  as discount, c.total from tblcart as c inner join tblProduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between  '" + f.dt1.Value.ToShortDateString() + "' and '" + f.dt2.Value.ToShortDateString() + "'", cn);
                }
                else
                {
                    da.SelectCommand = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc as discount, c.total from tblcart as c inner join tblProduct as p on c.pcode = p.pcode where status like 'Sold' and sdate between  '" + f.dt1.Value.ToShortDateString() + "' and '" + f.dt2.Value.ToShortDateString() + "'and cashier like '" + f.cboCashier.Text + "'", cn);
                }
                
                da.Fill(ds.Tables["dtSoldReport"]);

                cn.Close();

                ReportParameter pDate = new ReportParameter("pDate", "Date From: " + f.dt1.Value.ToShortDateString() + " To :" + f.dt2.Value.ToShortDateString());
                ReportParameter pCashier = new ReportParameter("pCashier", "Cashier: " + f.cboCashier.Text);
                ReportParameter pHeader = new ReportParameter("pHeader" , "Sales Report");
                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pCashier);

                reportViewer1.LocalReport.SetParameters(pHeader);




                rptDS = new ReportDataSource("DataSet1", ds.Tables["dtSoldReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptDS);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);

            }


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
