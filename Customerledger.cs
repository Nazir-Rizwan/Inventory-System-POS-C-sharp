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
    public partial class Customerledger : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        frmledgners frm;
        public Customerledger()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());

            
            //loadcustomer();
           // loaddata();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            //Clear();
        }
        public void loaddata()
        {

           // int i = 0;
           //// double _total = 0;

           // dataGridView1.Rows.Clear();
           // cn.Open();

           //// if (comboBox1.Text == "All Customer")
           // //{
           //   //  cm = new SqlCommand("select * from tblledgers  and sdate between  '" + dt1.Value.ToShortDateString() + "' and '" + dt2.Value.ToShortDateString() + "'", cn);
           // //}
           // //else
           // //{
           //     cm = new SqlCommand("select id,customer,amount,credit,balance,mode,sdate  from  tblledgers  where sdate between  '" + dt1.Value.ToShortDateString() + "' and '" + dt2.Value.ToShortDateString() + "'and customer like'" + comboBox1.Text + "'", cn);

           // //}
           // dr = cm.ExecuteReader();
           // while (dr.Read())
           // {
           //     i++;
           //     // _total += double.Parse(dr["total"].ToString());
           //     dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["customer"].ToString(), dr["amount"].ToString(), dr["credit"].ToString(), dr["balance"].ToString(), dr["mode"].ToString(), dr["sdate"].ToString());
           // }
           // dr.Close();
           // cn.Close();
            //lblTotal.Text = _total.ToString("#,##0.00");
        }
    public void loadcustomer()
        {


           customercomboBox1.Items.Clear();
            cn.Open();
            cm = new SqlCommand("select id ,name from tblCustomer ", cn);
            SqlDataReader dr = cm.ExecuteReader();

            //if(sdr.HasRows)

           while (dr.Read())
            {
                customercomboBox1.Items.Add(dr["name"].ToString());
                //customercomboBox1.DisplayMember = "name";
                customercomboBox1.ValueMember = "customerid";
            }
            dr.Close();
            cn.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //comboBox1.Items.Clear();
            //cn.Open();
            //cm = new SqlCommand("Select * from tblCustomer where customer like '" + comboBox1.Text + "'", cn);
            //dr = cm.ExecuteReader();
            //dr.Read();
            //if (dr.HasRows)
            //{


            //    //comboBox1.Text = dr["customer"].ToString();

            //}


            //dr.Close();
            //cn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Customerledger_Load(object sender, EventArgs e)
        {
            loadcustomer();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this Customer ledger? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblDebt(TrsDate,Debit,Credit)VALUES (@TrsDate,@Debit,@Credit)", cn);
                    cm.Parameters.AddWithValue("@TrsDate", DateTime.Now);
                  
                    cm.Parameters.AddWithValue("@Debit", debittextBox1.Text);
                    cm.Parameters.AddWithValue("@credit", credittextBox2.Text);

                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show(" Brand has beeen Successfully saved .");
                    //Clear();
                   // frmlist.loadrecord();
                }
                cn.Open();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }

        }
    }

    }

