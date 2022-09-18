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
    public partial class frmSolditem : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBconnection dbcon = new DBconnection();
        //fmPOS fp;
        public string suser;
        public frmSolditem()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
           dt1.Value = DateTime.Now;
            dt2.Value = DateTime.Now;
            //LoadRecord();
            //loadCashier();
           // fp = frm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecord()
        {
            int i = 0;
            double _total = 0;

            dataGridView1.Rows.Clear();
            cn.Open();

            if (cboCashier.Text == "All Cashier")
            {
                cm = new SqlCommand("select c.id, c.transno , c.pcode , p.pdesc , c.price , c.qty , c.discount , c.total from tblcart as c inner join tblProducts as p on c.pcode = p.pcode where status like 'Sold' and sdate between  '" + dt1.Value.ToShortDateString() + "' and '" + dt2.Value.ToShortDateString() + "'", cn);
            }
            else
            {
                cm = new SqlCommand("select c.id, c.transno , c.pcode , p.pdesc , c.price , c.qty , c.discount , c.total from tblcart as c inner join tblProducts as p on c.pcode = p.pcode where status like 'Sold' and sdate between  '" + dt1.Value.ToShortDateString() + "' and '" + dt2.Value.ToShortDateString() + "'and cashier like'" + cboCashier.Text + "'", cn);

            }
                dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i += 1;
                _total  += double.Parse(dr["total"].ToString());
                dataGridView1.Rows.Add(i, dr["id"].ToString() , dr["transno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), dr["total"].ToString());
            }
            dr.Close();
            cn.Close();
            lblTotal.Text = _total.ToString("#,##0.00");
        }

        private void frmSolditem_Load(object sender, EventArgs e)
        {

        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            //if(colName=="colCancel")
            //{
            //    frmCancelDetails f = new frmCancelDetails(this);
            //    f.txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //    f.txtTransNo.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            //    f.txtPcode.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            //    f.txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            //    f.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            //    f.txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            //    f.txtDiscount.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            //    f.txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            //    f.txtCancel.Text = suser;
                
            //    f.Show();
            //}
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportSold frm = new frmReportSold(this);
            frm.loadReport();
            frm.ShowDialog();
        }

        private void cboCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public void  loadCashier()
        {
            cboCashier.Items.Clear();
            cboCashier.Items.Add("All Cashier");
            cn.Open();
            cm = new SqlCommand("select * from tblUser where role like 'Cashier'", cn);
            dr=cm.ExecuteReader();
            while(dr.Read())
            {
                cboCashier.Items.Add(dr["username"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void cboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            //string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (e.KeyCode==Keys.Enter)
            {
                frmCancelDetails f = new frmCancelDetails(this);
                f.txtID.Text = dataGridView1.Rows[row].Cells[1].Value.ToString();
                f.txtTransNo.Text = dataGridView1.Rows[row].Cells[2].Value.ToString();
                f.txtPcode.Text = dataGridView1.Rows[row].Cells[3].Value.ToString();
                f.txtDescription.Text = dataGridView1.Rows[row].Cells[4].Value.ToString();
                f.txtPrice.Text = dataGridView1.Rows[row].Cells[5].Value.ToString();
                f.txtQty.Text = dataGridView1.Rows[row].Cells[6].Value.ToString();
                f.txtDiscount.Text = dataGridView1.Rows[row].Cells[7].Value.ToString();
                f.txtTotal.Text = dataGridView1.Rows[row].Cells[8].Value.ToString();
                f.txtCancel.Text = suser;

                f.Show();
            }
        }
    }
}
