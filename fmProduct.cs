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
using System.Drawing;
namespace Nazam_pos
{
    public partial class fmProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        fmproductlist flist;
        string title = "Inventory Products";

        public fmProduct(fmproductlist frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            flist = frm;
        }

        public void LoadCategory()
        {
            cboCategory.Items.Clear();
            cn.Open();
            cm = new SqlCommand("select category from tblCategory ", cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                cboCategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void LoadBrand()
        {
            cboBrand.Items.Clear();
            cn.Open();
            cm = new SqlCommand("select brand from tblBrand ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboBrand.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void fmProduct_Load(object sender, EventArgs e)
        {
            LinkLabelLinkClickedEventArgs ex = new LinkLabelLinkClickedEventArgs(linkLabel1.Links[0]);

            linkLabel1_LinkClicked(sender, ex);
            pcode_increment();
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        


        private void BTNSave_Click(object sender, EventArgs e)
        {
            bool b = false; 
            try
            {
                errorProvider1.Clear();
                
                
                if (textpurchase.Text == string.Empty)
                {
                    //MessageBox.Show("Please Enter Purchase Price ", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    errorProvider1.SetError(this.textpurchase, "Please Enter Purchase Price");
                    textpurchase.Focus();
                    return;
                }
                if (textqty.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Product Quantity ", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textqty.Focus();
                    return;
                }
                cn.Open();
                    cm = new SqlCommand("select barcode from tblProducts where barcode ='" + textbarcode.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();

                    if (dr.HasRows)
                    {

                        b = true;
                    }
                    else
                    {
                        b = false;
                    }
                    cn.Close();
                    dr.Close();

                    if (b == true)
                    {
                        MessageBox.Show("Already Product exist ?", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;

                    }
                    else
                    {

                    if (MessageBox.Show("Are you sure you want to save this Product? ", "Saving Product ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string bid = " ";
                        string cid = " ";
                        cn.Open();
                        cm = new SqlCommand("select  id from  tblBrand where brand like '" + cboBrand.Text + "'", cn);
                        dr = cm.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows) { bid = dr[0].ToString(); }
                        dr.Close();
                        cn.Close();

                        // category id as fetch in category table 

                        cn.Open();
                        cm = new SqlCommand("select id from  tblCategory where category like '" + cboCategory.Text + "'", cn);
                        dr = cm.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows)
                        {
                            cid = dr[0].ToString();
                        }
                        dr.Close();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblProducts (pcode,barcode,pdesc,bid, cid,pname ,purchase,price, qty ,reorder) VALUES(@pcode, @barcode, @pdesc,@bid, @cid,@pname ,@purchase,@price, @qty ,@reorder)", cn);
                        cm.Parameters.AddWithValue("@pcode", textpcode.Text);
                        cm.Parameters.AddWithValue("@barcode", textbarcode.Text);

                        cm.Parameters.AddWithValue("@pdesc", textpdesc.Text);
                        cm.Parameters.AddWithValue("@bid", bid);
                        cm.Parameters.AddWithValue("@cid", cid);
                        cm.Parameters.AddWithValue("@pname", textpname.Text);

                        cm.Parameters.AddWithValue("@purchase", double.Parse(textpurchase.Text));
                        cm.Parameters.AddWithValue("@price", double.Parse(textprice.Text));
                        cm.Parameters.AddWithValue("@qty", int.Parse(textqty.Text));

                        cm.Parameters.AddWithValue("@reorder", int.Parse(txtReorder.Text));
                        //cm.Parameters.AddWithValue("@piece", int.Parse(txtpiece.Text));
                        // cm.Parameters.AddWithValue("@issuedate", dateTimePicker1.Value);
                        // cm.Parameters.AddWithValue("@expirydate", dateTimePicker2.Value);


                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show(" Product has beeen Successfully saved .");
                        Clear();
                        flist.LoadRecords();

                    }
                }

            }
            catch (Exception ex)
            {
                cn.Close();

                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            

        }

        public void Clear()
        {
            textprice.Clear();
            textpdesc.Clear();
            textpcode.Clear();
            textbarcode.Clear();
            textpurchase.Clear();
            textqty.Clear();
            cboBrand.Text = " ";
            cboCategory.Text = " ";
            textpcode.Focus();
            BTNSave.Enabled = true;
            btnupdate.Enabled = false;


        }


        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Product? ", "Saving Product ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = " ";
                    string cid = " ";
                    cn.Open();
                    cm = new SqlCommand("select id from  tblBrand where brand like '" + cboBrand.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bid = dr[0].ToString();
                    }
                    dr.Close();
                    cn.Close();

                    // category id as fetch in category table 

                    cn.Open();
                    cm = new SqlCommand("select id from  tblCategory where category like '" + cboCategory.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();
                    }
                    dr.Close();
                    cn.Close();




                    // new Statement 
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblProducts SET barcode=@barcode, pdesc=@pdesc,bid=@bid, cid=@cid, pname=@pname,purchase=@purchase ,price=@price , qty=@qty , reorder=@reorder  where pcode like @pcode", cn);

                    cm.Parameters.AddWithValue("@pcode", textpcode.Text);
                    cm.Parameters.AddWithValue("@barcode", textbarcode.Text);
                    cm.Parameters.AddWithValue("@pdesc", textpdesc.Text);
                    cm.Parameters.AddWithValue("@bid", bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@pname", textpname.Text);
                    cm.Parameters.AddWithValue("@barcode", textbarcode.Text);
                    cm.Parameters.AddWithValue("@purchase", double.Parse(textpurchase.Text));
                    cm.Parameters.AddWithValue("@price", double.Parse(textprice.Text));
                    cm.Parameters.AddWithValue("@qty", int.Parse(textqty.Text));

                    cm.Parameters.AddWithValue("@reorder",int.Parse( txtReorder.Text));
                   // cm.Parameters.AddWithValue("@piece", int.Parse(txtpiece.Text));
                    //cm.Parameters.AddWithValue("@issuedate", dateTimePicker1.Value);
                    //cm.Parameters.AddWithValue("@expirydate", dateTimePicker2.Value);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show(" Product has beeen Successfully Updated.");
                    Clear();
                    flist.LoadRecords();
                    this.Dispose();


                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void textprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==  48)
            {
                //accept character 
            }
            else if (e.KeyChar==8)
            {
                //accept backspace  
            }

            else if((e.KeyChar<48)|| (e.KeyChar>57))   // ascii code 48-57 between 0-9
            {
                e.Handled = true;

            }
        }

        private void textprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtReorder_TextChanged(object sender, EventArgs e)
        {

        }

        private void textqty_TextChanged(object sender, EventArgs e)
        {

        }

        private void textpurchase_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = cboBrand.SelectedItem.ToString();
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = cboCategory.SelectedItem.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textpurchase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rnd = new Random();
            textpcode.Clear();

           // for (int i=100;i< 500000000;i++)
            //{
              //  textpcode.Text += i; 
                textpcode.Text += rnd.Next(1000, 500000000);
            //}

            
        }

        private void textbarcode_Leave(object sender, EventArgs e)
        {

            if(string.IsNullOrEmpty(textbarcode.Text))
            {
                textbarcode.Focus();
                errorProvider1.SetError(this.textbarcode, "Please Enter Barcode");
                //MessageBox.Show("Please Enter Barcode");
            }
            else
            {
                //errorProvider1.Icon= Properties.Resources.icons8_checked_checkbox_24x24;
                errorProvider1.Clear();
            }

           

            //if (textbarcode.Text == string.Empty)

            //{
                //MessageBox.Show("Please Enter Barcode No ", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //textbarcode.Focus();
               // textbarcode.Focus();
                //errorProvider1.SetError(this.textbarcode, "Please Enter Barcode No");
                
            //}
            //else
            //{
              //  errorProvider1.Clear();
            //}
        }

        private void textbarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void textbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
           // if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            //{
              //  e.Handled = true;
            //}
        }

        private void textpurchase_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textpurchase.Text))
            {
                textpurchase.Focus();
                errorProvider1.SetError(this.textpurchase, "Please Enter price in number format[1000] ");
                //MessageBox.Show("Please Enter Barcode");
            }
            else
            {
                //errorProvider1.Icon= Properties.Resources.icons8_checked_checkbox_24x24;
                errorProvider1.Clear();
            }
        }
        public void pcode_increment()
        {
            string sdate = DateTime.Now.ToString("yyyyMMdd");
            string transno;
            int count;

            transno = "pcoo";
            //transno = dr[0].ToString();
            //count = int.Parse(transno.Substring(8, 4));
            count = 0;    
            textpcode.Text = sdate + (count + 1);

                //transno = sdate + "1001";
                //lblTransno.Text = transno;
            }

        private void textbarcode_Enter(object sender, EventArgs e)
        {
            //if(textpcode.Text== "scan product here")
            //{
            //    textpcode.Text="";
            //}
            //else
            //{

            //}
        }
    }
}
