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
    public partial class frmledgners : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        fmPOS f;
        double value = fmPOS.totalvalue;



        public frmledgners()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            Loadcutomer();

        }

        private void frmledgners_Load(object sender, EventArgs e)
        {
           // textBox1.Text = frm.customer_name;

           // textamount.Text = f.lblTotal.Text.ToString();
           //textamount.Text = fmPOS.passingtext;

            
          
           textamount.Text = value.ToString("#,##0.00");
            
            // textBox1.Text = frm.customerbox.Text;
            /// textBox1.Text=frm.customer_name.
            /// t
            /// 
            // textBox1.Text = frm.textname.Text;
            //string name = frmsettle.customer_name;
            //textBox1.Text = name.ToString();
        }
        public void Loadcutomer()
        {
            cbocustomer.Items.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tblledger where name not like 'walk in customer' ", cn);
            //cm = new SqlCommand("select sum(credit) from tblledger where name not like 'walk in customer' ", cn);



            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cbocustomer.Items.Add(dr["name"].ToString());
                textcredit.Text = dr["credit"].ToString();


            }
            dr.Close();
            cn.Close();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            try
            {
                //string cid = " ";


                //if (MessageBox.Show("Are you sure you want to save this Product? ", "Saving Product ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{


                //    cn.Open();
                //    cm = new SqlCommand("select  customer from  tblCustomer where customer like '" + cbocustomer.Text + "'", cn);
                //    dr = cm.ExecuteReader();
                //    dr.Read();
                //    if (dr.HasRows) { cid = dr[0].ToString(); }
                //    dr.Close();
                //    cn.Close();

                   

                  

                    //double sale = Double.Parse(f.lblDisplayTotal.Text);
                //}

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            cbocustomer.Text = "";
            textamount.Clear();
            textcredit.Clear();
            textremaining.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textamount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double amount = Double.Parse(textamount.Text) + Double.Parse(textcredit.Text);
                textremaining.Text = amount.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                textremaining.Text = "0.00";
            }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textbarcode_TextChanged(object sender, EventArgs e)
        {
           

            try
            {
                double amount = Double.Parse(textamount.Text) - Double.Parse(textcredit.Text);
                textremaining.Text = amount.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                textremaining.Text = "0.00";
            }
        }

        private void cbocustomer_TextChanged(object sender, EventArgs e)
        {
            //double value = Double.Parse(textamount.Text) + Double.Parse(fmPOS.passingtext);
            //textamount.Text = value.ToString("#,##0.00");
            //double add_remainingvalu;
            //cbocustomer.Items.Clear();
            //cn.Open();
            //cm = new SqlCommand("Select * from tblLedgers where name  not like 'Walk in customer' '" + cbocustomer.Text + "'", cn);
            //dr = cm.ExecuteReader();
            //dr.Read();
            //if (dr.HasRows)
            //{

            //    //textamount.Text = dr["amount"].ToString();
            //    textcredit.Text = dr["credit"].ToString();
            //    textremaining.Text =  dr["balance"].ToString();
            //}
           
          
            //dr.Close();
            //cn.Close();
            // add_remainingvalu = Double.Parse(textremaining.Text);
            //add_remainingvalu = add_remainingvalu + value;
                
            //textremaining.Text = add_remainingvalu.ToString("#,##0.00");
           // Clear();
        }

        private void texttotal_TextChanged(object sender, EventArgs e)
        {


           //texttotal.Text =Convert.ToString( frm.customerbox.SelectedValue);
            

            //try
            //{
            //    double amt = Double.Parse(textamount.Text)  + fmPOS.totalvalue;
            //    texttotal.Text = amt.ToString("#,##0.00");
            //}
            //catch (Exception ex)
            //{
            //    texttotal.Text = "0.00";
            //}

        }

        private void cbocustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
           // cbocustomer.Text= frm.customerbox.SelectedText;
            //cbocustomer.Text=
            

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //textBox1.Text = frm.customerbox.Text;
        }

        private void BTNSave_Click_1(object sender, EventArgs e)
        {
            //cn.Open();
            //cm = new SqlCommand("INSERT INTO tblLedgers (customer,amount,credit,balance,mode,sdate) VALUES(@customer,@amount,@credit,@balance,@mode,@sdate)", cn);

            //cm.Parameters.AddWithValue("@customer", textBox1.Text);
            //cm.Parameters.AddWithValue("@amount", double.Parse(textamount.Text));

            //cm.Parameters.AddWithValue("@credit", double.Parse(textcredit.Text));
            /////cm.Parameters.AddWithValue("@customer", cid);

            //cm.Parameters.AddWithValue("@balance", double.Parse(textremaining.Text));

            //cm.Parameters.AddWithValue("@mode", textmode.Text);
            //cm.Parameters.AddWithValue("@sdate", DateTime.Now);

            //cm.ExecuteNonQuery();
            //cn.Close();
            //MessageBox.Show(" Ledger Record has beeen Successfully saved .");
            //Clear();
            cn.Open();
            cm = new SqlCommand("update tblledger set credit =credit + " + double.Parse(textamount.Text.ToString()) + "where name = '" +cbocustomer.Text + "'", cn);

            cm.ExecuteNonQuery();

            cn.Close();
            MessageBox.Show("update succwesfully ");


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
