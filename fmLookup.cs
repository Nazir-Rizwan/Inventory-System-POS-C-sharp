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
    public partial class fmLookup : Form
    {
        fmPOS f;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        string stitle = "Inventory Management system ";



        public fmLookup(fmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            f = frm;
            this.KeyPreview = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select p.pcode,p.barcode, p.pdesc, b.brand,c.category, p.price , p.qty from tblProducts as p inner join tblBrand as b on b.id=p.bid inner join tblCategory as c on c.id= p.cid where p.pdesc like '%" + txtSearch.Text + "%' order by p.pdesc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())

            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "Select")
            {
                fmQty frm = new fmQty(f);
               
                frm.ProductDetail(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()),f.lblTransno.Text, int.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString()));
                
                frm.ShowDialog();


                

            }
        }

        private void fmLookup_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //dataGridView1.Columns.[e.col] = true;
            //dataGridView1.Columns.[e.row] = true;

            int row = dataGridView1.CurrentCell.RowIndex;
            fmQty frm = new fmQty(f);
            if (e.KeyCode == Keys.Enter)
            {
                frm.ProductDetail(dataGridView1.Rows[row].Cells[1].Value.ToString(), Double.Parse(dataGridView1.Rows[row].Cells[6].Value.ToString()), f.lblTransno.Text, int.Parse(dataGridView1.Rows[row].Cells[7].Value.ToString()));

                frm.ShowDialog();
            }
        }
    }
}
