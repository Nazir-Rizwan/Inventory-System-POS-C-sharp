using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;

namespace Nazam_pos
{
    public partial class frmreceipt : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        string store = "Nazam Software Solution";
        string address = "Nadir khan gali pakpattan city";
        fmPOS f;
        private static List<Stream> m_streams;
        private static int m_currentPageIndex = 0;
        public frmreceipt(fmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            f = frm;
            this.KeyPreview = true;
        }

        private void frmreceipt_Load(object sender, EventArgs e)
        {
   
            this.reportViewer1.RefreshReport();
          
            
        }
        public static void PrintToPrinter(LocalReport report)
        {
            Export(report);

        }

        public static void Export(LocalReport report, bool print = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>3.2688in</PageWidth>
                <PageHeight>35cm</PageHeight>
                <MarginTop>0</MarginTop>
                <MarginLeft>0.0in</MarginLeft>
                <MarginRight>0.0in</MarginRight>
                <MarginBottom>0.0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (print)
            {
                Print();
            }
        }


        public static void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        public void loadreport(string pcash , string pchange )
        {
            ReportDataSource rptdatasource;

            try
            {
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report1.rdlc";

                
                //this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"C:\Users\nazir\OneDrive\Desktop\.vs\Nazam pos\bin\Debug\Reports\Report1.rdlc";

                this.reportViewer1.LocalReport.DataSources.Clear();
                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();
                cn.Open();
                da.SelectCommand = new SqlCommand("select c.id, c.transno , c.pcode, c.price, c.qty, c.discount , c.total , c.sdate, c.status, p.pdesc from tblCart as c  inner join tblProducts as p on p.pcode = c.pcode  where transno like  '" + f.lblTransno.Text+ "'" , cn);
                da.Fill(ds.Tables["dtSold"]);
                cn.Close();

               // ReportParameter pVatable = new ReportParameter("pVatable", f.lblVatable.Text);
                //ReportParameter pVat = new ReportParameter("pVat", f.lblVat.Text);
                ReportParameter pDiscount = new ReportParameter("pDiscount", f.lblDiscount.Text);
                ReportParameter pTotal = new ReportParameter("pTotal", f.lblTotal.Text);
                ReportParameter pCash = new ReportParameter("pCash", pcash);
                ReportParameter pChange = new ReportParameter("pChange", pchange);

                ReportParameter pStore = new ReportParameter("pStore", store);
                ReportParameter pAddress = new ReportParameter("pAddress", address);
                ReportParameter pTransaction = new ReportParameter("pTransaction","Invoice # : " + f.lblTransno.Text);
                ReportParameter pCashier = new ReportParameter("pCashier", f.lblUser.Text);

               // reportViewer1.LocalReport.SetParameters(pVatable);
                //reportViewer1.LocalReport.SetParameters(pVat);
                reportViewer1.LocalReport.SetParameters(pDiscount);
                reportViewer1.LocalReport.SetParameters(pTotal);
                reportViewer1.LocalReport.SetParameters(pCash);
                reportViewer1.LocalReport.SetParameters(pChange);

                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);
                reportViewer1.LocalReport.SetParameters(pTransaction);
                reportViewer1.LocalReport.SetParameters(pCashier);

                rptdatasource = new ReportDataSource("DataSet1",ds.Tables["dtSold"]);
                reportViewer1.LocalReport.DataSources.Add(rptdatasource);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                PrintToPrinter(this.reportViewer1.LocalReport);
               // reportViewer1.Print();
                

            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
        public void printreceipt()
        {
            PrintDialog printdialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            
            
            //printDocument.PrintPage +=new PrintPageEventHandler
            
            //printDocument.DocumentName = "filename";
            //printdialog.Document = ;
            //printdialog.AllowSelection = true;
            //printdialog.AllowSomePages = true;

            //DialogResult result = printdialog.ShowDialog();
            //if (result == DialogResult.OK)
            //{
                printDocument.Print();
            //}

        }

        private void frmreceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                this.Dispose();
            }
        }

        private void reportViewer1_Print(object sender, ReportPrintEventArgs e)
        {
            this.reportViewer1.PrintDialog();

        }

        private void reportViewer1_Enter(object sender, EventArgs e)
        {
            
        }
    }
}
