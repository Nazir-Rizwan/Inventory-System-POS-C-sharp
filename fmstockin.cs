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

    

    public partial class fmstockin : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        string stitle = "Inventory Management system "; // yaha koi bi likh sakta ho



        public fmstockin()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            loadVendor();
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void fmstockin_Load(object sender, EventArgs e)
        {
            LinkLabelLinkClickedEventArgs ex = new LinkLabelLinkClickedEventArgs(linkLabel1.Links[0]);

           linkLabel2_LinkClicked(sender, ex);

        }

        //private void linkLabel2_LinkClicked(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           

        }
        public void loadstockin()
        {
            int i=0;
            dataGridView2.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select * from vwstockin   where refno  like '"+ textrefno.Text+"'and status like 'Pending' ", cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;

                dataGridView2.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(),dr["vendor"].ToString(), dr[10].ToString());
                 
            }
            dr.Close();
            cn.Close();


        }

        private void textby_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colName = dataGridView2.Columns[e.ColumnIndex].Name;
           
            if(colName=="colDelete")
            {
                if(MessageBox.Show("Remove this item?",stitle,MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
              {
                    cn.Open();
                    cm = new SqlCommand("delete from tblStockin where id = '" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString()+ "'",cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Item has been Successfully removed.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadstockin();
                }
                
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fmsearchproduct_stockin frm = new fmsearchproduct_stockin(this);
            frm.loadProduct();
            frm.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void loadstockinhistory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select * from vwstockin   where cast(sdate as date) between '" +date1.Value.ToShortDateString() + "' and '" +date2.Value.ToShortDateString() +  "' and status like 'Done' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;

                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString(),dr["vendor"].ToString());

            }
            dr.Close();
            cn.Close();
        }



        public void Clear()
        {
            textby.Clear();
            textrefno.Clear();
            textquantity.Clear();
            dt1.Value = DateTime.Now;
        }


        private void BTNSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are You sure you want to save this record?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            //update tblproduct qty
                            cn.Open();
                            cm = new SqlCommand("update tblProducts set qty=qty -" + int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString()) + " where pcode like '" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            // Update tblstockin qty 

                            //lect 23
                                      //set qty=qty =+ " +.........
                            cn.Open();
                            cm = new SqlCommand("update  tblStockin  set qty= '" + int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString()) + "', status='Done' where  id like '" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();

                            cn.Close();
                        }

                        Clear();
                        loadstockin();
                    }
                }

            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);



            }
        }

        private void textrefno_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadstockinhistory();
        }

        private void date1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cboVendor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }
        public void loadVendor()
        {
            
            cboVendor.Items.Clear();
            cn.Open();
            cm = new SqlCommand("Select * from tblVendor", cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                cboVendor.Items.Add(dr["vendor"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void cboVendor_TextChanged(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("Select * from tblVendor where vendor like '"+cboVendor.Text+"'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                lblVendorID.Text = dr["id"].ToString();
                txtPerson.Text = dr["contactperson"].ToString();
                txtAddress.Text = dr["address"].ToString();
            }
            dr.Close();
            cn.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rnd = new Random();
            textrefno.Clear();
            
            
            
                textrefno.Text += rnd.Next(1000);
            
        }

        private void cboVendor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //foreach(DataGridViewRow row in dataGridView2.Rows)
            //{
            //  row.Cells[dataGridView2.Columns["Total Price"].Index].Value = (Convert.ToDouble(row.Cells[dataGridView2.Columns["QUANTITY"].Index].Value) * Convert.ToDouble(row.Cells[dataGridView2.Columns["Price"].Index].Value));
            //}

            decimal s = 0, s1 = 0, s13 = 0;
            for (int j = 0; j < dataGridView2.Rows.Count; ++j)
            {
                s = Convert.ToDecimal(dataGridView2.Rows[j].Cells["Column9"].Value);
                s1 = Convert.ToDecimal(dataGridView2.Rows[j].Cells["Column4"].Value);
                s13 = s * s1;
                dataGridView2.Rows[j].Cells["Column7"].Value = s13.ToString();

            }
        }
    }
}
