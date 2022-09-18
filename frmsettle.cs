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
    public partial class frmsettle : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        public static string customer_name;

        fmPOS fpos;
            
        public frmsettle(fmPOS fp)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            fpos = fp;
            this.KeyPreview = true;
            loadcustomer();
            
        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {


            try
            {
                double sale = double.Parse(txtSale.Text);
                double cash = double.Parse(txtCash.Text);
                double change = cash - sale;
                txtChange.Text = change.ToString("#,##0.00");

            }

            catch (Exception ex)
            {
                txtChange.Text = "0.00";
            }
        }

        private void txtSale_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChange_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn9.Text;
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            txtCash.Text += button11.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn6.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn0.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn3.Text;
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn00.Text;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try

            {
                 // if (radioButton1.Checked == true  && (double.Parse(txtChange.Text) < 0) || (txtCash.Text == string.Empty))
                //{
                  //  MessageBox.Show("Insufficient amount. Please Enter the correct amount!", "warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //return;
                //}

                //if ((double.Parse(txtChange.Text)< 0)  || (txtCash.Text == string.Empty))
                //{
                  //  MessageBox.Show("Insufficient amount. Please Enter the correct amount!", "warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;
                //}
                 
               

               // else
                //{

                   
                    

                    for (int i=0; i<fpos.dataGridView1.Rows.Count; i++)
                    {

                        cn.Open();
                        cm = new SqlCommand("update tblProducts set qty =qty - " +int.Parse(fpos.dataGridView1.Rows[i].Cells[5].Value.ToString())+ "where pcode = '"+ fpos.dataGridView1.Rows[i].Cells[2].Value.ToString() + "'", cn);

                        cm.ExecuteNonQuery();
                       
                        cn.Close();

                        cn.Open();
                         cm = new SqlCommand("update tblCart set status  ='Sold' where id = '" + fpos.dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", cn);

                        //cm = new SqlCommand("update tblCart set customer_name='" + textname.Text +"' ,status ='Sold' where id = '" + fpos.dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", cn);

                        cm.ExecuteNonQuery();
                        cn.Close();

                    cn.Open();
                     cm = new SqlCommand("INSERT INTO tblledger(transno,name,total,credit,debit)VALUES(@transno,@name,@total,@credit,@debit)", cn);

                    //cm.Parameters.AddWithValue("@cid", int.Parse(lblid.Text));
                    cm.Parameters.AddWithValue("@name", textname.Text);
                     cm.Parameters.AddWithValue("@total", double.Parse(txtSale.Text));

                     cm.Parameters.AddWithValue("@transno", fpos.lblTransno.Text);

                     cm.Parameters.AddWithValue("@debit", double.Parse(txtCash.Text));
                     cm.Parameters.AddWithValue("@credit", double.Parse(txtChange.Text));



                   
                    cm.ExecuteNonQuery();
                   
                     cn.Close();



                    //}

                    frmreceipt frm = new frmreceipt(fpos);

                    frm.loadreport(txtCash.Text,txtChange.Text);
                   
                    //frm.Refresh();
                   
                    frm.Show();
                   // frm.printreceipt();
                    MessageBox.Show("Payment Successfully saved ", "Payment!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fpos.GetTransno();
                    fpos.loadcart();
                    this.Dispose();



                }
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Insufficient amount. Please Enter the correct amount!", "warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmsettle_Load(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                customerbox.Enabled = false;
                textname.Enabled = true;

            }
            else
            {


                
                   radioButton1.Checked=false;

                       customerbox.Enabled = true;
                    textname.Enabled = true;


            }



        }

        private void frmsettle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();

            }else if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(sender, e);
            }
        }

        private void customerbox_SelectedIndexChanged(object sender, EventArgs e)
        {

            cn.Open();
            cm = new SqlCommand("Select * from tblCustomer where name like '" + customerbox.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblid.Text = dr["id"].ToString();

            }

            dr.Close();
            cn.Close();

            textname.Text = customerbox.SelectedItem.ToString();
        }

        private void customerbox_TextChanged(object sender, EventArgs e)
        {

            


        }

        public void loadcustomer()
        {


            customerbox.Items.Clear();
            cn.Open();
            cm = new SqlCommand("Select * from tblcustomer", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                customerbox.Items.Add(dr["name"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //customer_name = textname.Text;
            //frmledgners f = new frmledgners();
            
           // f.Show();
        }

        private void textname_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                customerbox.Enabled = false;
                textname.Enabled = false;

            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;

                customerbox.Enabled = true;
                textname.Enabled = true;


            }
        }

        private void btnLedger_Click(object sender, EventArgs e)
        {
            try
            {
              
               // cn.Open();
               // cm = new SqlCommand("INSERT INTO tblledger(name,total,transno,debit,credit)VALUES(@name,@total,@transno,@debit ,@credit)", cn);
               
               // cm.Parameters.AddWithValue("@name", textname.Text);
               // cm.Parameters.AddWithValue("@total", double.Parse(txtSale.Text));

               // cm.Parameters.AddWithValue("@transno", fpos.lblTransno.Text);

               // cm.Parameters.AddWithValue("@debit", double.Parse(txtCash.Text));
               // cm.Parameters.AddWithValue("@credit", double.Parse(txtChange.Text));


              
               // cm.ExecuteNonQuery();
               //btnEnter_Click(sender, e);
               // cn.Close();
              
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
            

        }
    }
}
