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
    public partial class fmsearchproduct_stockin : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        string stitle = "Inventory Management system ";
        fmstockin slist;
        public fmsearchproduct_stockin(fmstockin flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            slist = flist;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadProduct()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select pcode, pdesc , qty ,purchase from tblProducts where pdesc like  '%" + txtSearch.Text + "%' order by pdesc ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(),dr[3].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            //if (colName == "colSelect")
            //{
            //    if (slist.textrefno.Text == string.Empty)
            //    {
            //        MessageBox.Show("Please Enter Phone No ", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        slist.textrefno.Focus();
            //        return;
            //    }
            //    if (slist.textby.Text == string.Empty)
            //    {
            //        MessageBox.Show("Please Enter Stock in by  ", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        slist.textby.Focus();
            //        return;
                
            //    }
                //if (slist.textquantity.Text == string.Empty)
                //{
                //    MessageBox.Show("Please Enter Quantity  ", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    slist.textby.Focus();
                //    return;

                //}


                //    if (MessageBox.Show("Add this item?", "select Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    cn.Open();
                //    cm = new SqlCommand(" insert into tblStock (refno,pcode,qty,sdate,stockinby,vendorid ) values (@refno,@pcode,@qty,@sdate,@stockinby,@vendorid)", cn);
                //    cm.Parameters.AddWithValue("@refno", slist.textrefno.Text);
                //    cm.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                //    cm.Parameters.AddWithValue("@qty", slist.textquantity.Text);
                //    cm.Parameters.AddWithValue("@sdate", slist.dt1.Value);
                //    cm.Parameters.AddWithValue("@stockinby", slist.textby.Text);
                //    cm.Parameters.AddWithValue("@vendorid", slist.lblVendorID.Text);
                //    cm.ExecuteNonQuery();

                //    cn.Close();

                //    MessageBox.Show("Successfully added!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    slist.loadstockin();

                //}


            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (e.KeyCode == Keys.Enter)
            {
                if (slist.textrefno.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Refrence No ", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slist.textrefno.Focus();
                    return;
                }
                if (slist.textby.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Stock in Buy  ", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slist.textby.Focus();
                    return;

                }
                if (MessageBox.Show("Add this item?", "select Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand(" insert into tblStockin (refno,pcode,qty,sdate,stockinby,vendorid,price ) values (@refno,@pcode,@qty,@sdate,@stockinby,@vendorid,@price)", cn);
                    cm.Parameters.AddWithValue("@refno", slist.textrefno.Text);
                    //yaha mene  e.rowsindex ki jagha current row likha ha  

                     cm.Parameters.AddWithValue("@pcode", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    //cm.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

                    //dataGridView1.Rows[e.RowIndex].Cells[1]

                    cm.Parameters.AddWithValue("@qty", slist.textquantity.Text);
                    
                    cm.Parameters.AddWithValue("@sdate", slist.dt1.Value);
                    cm.Parameters.AddWithValue("@stockinby", slist.textby.Text);
                    cm.Parameters.AddWithValue("@vendorid", slist.lblVendorID.Text);
                    cm.Parameters.AddWithValue("@price", dataGridView1.CurrentRow.Cells[8].Value.ToString());
                    cm.ExecuteNonQuery();

                    cn.Close();

                    MessageBox.Show("Successfully added!", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    slist.loadstockin();

                }

            }
        }
    }
}
