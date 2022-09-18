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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace Nazam_pos
{
    public partial class fmproductlist : Form 
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        string stitle = "Inventory Management system ";
         
        DataTable dt;
        



        public fmproductlist()
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
            fmProduct frm = new fmProduct(this);
            frm.BTNSave.Enabled = true;
            frm.btnupdate.Enabled = false;
            frm.LoadBrand();
            frm.LoadCategory();
            frm.ShowDialog();
        }
        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select p.pcode,p.barcode, p.pdesc, b.brand,c.category, p.pname  ,p.purchase ,p.price , p.qty , p.reorder   from tblProducts as p inner join tblBrand as b on b.id=p.bid inner join tblCategory as c on c.id= p.cid where p.pdesc like '%" + txtSearch.Text + "%' order by p.pdesc", cn);
            dr = cm.ExecuteReader();
            while(dr.Read())

            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(),dr[7].ToString(), dr[8].ToString(), dr[9].ToString());
            }
            dr.Close();
            cn.Close();


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                fmProduct frm = new fmProduct(this);
                 frm.BTNSave.Enabled = false;
                frm.btnupdate.Enabled = true;
                frm.LoadBrand();
                frm.LoadCategory();
                frm.textpcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.textbarcode.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.textpdesc.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.textprice.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                frm.textpurchase.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.textpname.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();




                frm.cboBrand.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.cboCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.textqty.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                frm.txtReorder.Text= dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
               // frm.dateTimePicker1.Value = dataGridView1.Rows[0].Cells[10].Value.ToString();
                
                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {

                if (MessageBox.Show("Are you sure you wnant to delete this Record ?", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                   cn.Open();
                    cm = new SqlCommand("delete from tblstockin where pcode like '" + dataGridView1[1,e.RowIndex].Value.ToString() + "'", cn);
                    
                    
                    cm.ExecuteNonQuery();
                    cn.Close();
                   cn.Open();
                    cm = new SqlCommand("delete from tblProducts where pcode like  '" + dataGridView1[1,e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product Record has been successfully deleted!");
                    LoadRecords();
                }
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        private void fmproductlist_Load(object sender, EventArgs e)
        {

            
            string total="";
            cn.Open();
            cm = new SqlCommand ("SELECT count(barcode) AS barcode FROM tblProducts",cn);

            dr = cm.ExecuteReader();
            //textBox1.Clear();
            dr.Read();
            if (dr.HasRows)
            {
                textBox1.Text = dr.GetValue(0).ToString();

            }

            cn.Close();
           
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frminventoryreport f = new frminventoryreport();
            f.loadproduct();
            f.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            show();

            





        }

        public void show()
        {

            //ReportDocument cry = new ReportDocument();
            // dt.Columns.Add("Qty", typeof(int));


            //fmProduct frm = new fmProduct(this);
            // frm.textpcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //frm.textbarcode.Text = dataGridView1.CurrentCell.Value.ToString(); //2
            //frm.textqty.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            //frm.textprice.Text = dataGridView1.CurrentCell.Value.ToString(); //7
            // frm.textpurchase.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            //aaaaa fm = new aaaaa();

            

            //dt.Rows.Add(frm.textbarcode.Text, frm.textprice.Text, frm.textqty.Text);

            //string reportpath = @"Reports\CrystalReport2.rpt";
            
            //cry.Load (reportpath);
            
            //cry.SetDataSource(dt);
            //crystalReportViewer1.ReportSource = cry;


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            barrepo rp = new barrepo();
            rp.Show();

        }
    }
}
