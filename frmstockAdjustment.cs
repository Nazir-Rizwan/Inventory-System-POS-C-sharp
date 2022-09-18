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
    public partial class frmstockAdjustment : Form
    {
        Form1 f;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        int _qty=0;
        public frmstockAdjustment(Form1 f)
        {
            InitializeComponent();
            cn = new SqlConnection();
            cn.ConnectionString = db.Myconnection();
            this.f = f;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName=="Select")
            {
                txtpcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

               txtdesc.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + "  "  + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() ; 
                _qty= int.Parse( dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());

            }
        }
        public void Referenceno()
        {
            Random rnd = new Random();
            txtref.Text = rnd.Next().ToString();

        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select p.pcode,p.barcode, p.pdesc, b.brand,c.category, p.price , p.qty, p.reorder from tblProducts as p inner join tblBrand as b on b.id=p.bid inner join tblCategory as c on c.id= p.cid where p.pdesc like '%" + txtSearch.Text + "%' order by p.pdesc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())

            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();


        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                LoadRecords();
            }
        }

        private void dataGridView1_BorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                //validation for empty field 
                if(int.Parse(txtqty.Text)> _qty)
                {
                    MessageBox.Show("STOCK ON HAND QUANTITY SHOULD BE GREATER THAN FROM ADJUSTMENT QTY.", "WARNINGS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //update stock 
                if(cbocommand.Text== "Remove From Inventory ")
                {
                    sqlStatement ( "update  tblproduct set qty= (qty - " + int.Parse(txtqty.Text) + ") where pcode like '" + txtpcode.Text + "'");

 
                }
                else if (cbocommand.Text== "ADD To Inventory")
                {
                    sqlStatement("update  tblproduct set qty= (qty + " + int.Parse(txtqty.Text) + ") where pcode like '" + txtpcode.Text + "'");


                }
                sqlStatement("insert into tblAdjustment (referenceno ,pcode , qty, action ,remarks , sdate ,[user]) values ('" + txtref.Text + "' , '" + txtpcode.Text + "' , '" + int.Parse(txtqty.Text) + "','" + cbocommand.Text + "','" + txtRemarks.Text + "','" + DateTime.Now.ToShortDateString()+"','" + txtuser.Text + "')");
                MessageBox.Show("STOCK HAS BEEN SUCCESSFULLY ADJUSTMENT.", "PROCESS COMPLETED ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRecords();
                clear();

            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       private void clear()
        {
            txtdesc.Clear();
            txtpcode.Clear();
            txtqty.Clear();
            txtref.Clear();
            txtRemarks.Clear();
            cbocommand.Text="";
            Referenceno();
        
        }
        public  void sqlStatement (string _sql)
        {
            cn.Open();
            cm = new SqlCommand(_sql, cn);
            cm.ExecuteNonQuery();
            cn.Close();

        }
    }
}
