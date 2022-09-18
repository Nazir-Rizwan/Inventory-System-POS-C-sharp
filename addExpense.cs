using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Nazam_pos
{
    public partial class addExpense : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;

        public addExpense()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
           
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {


                if (comboType.Text == "" || textamount.Text == "" || combostatus.Text == "")
                {
                    MessageBox.Show("Please! fill the empty fileds.", "Empty error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblexpense(sdate,type,amount,status,comments) VALUES (@sdate,@type,@amount,@status,@comments)", cn);
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@type", comboType.Text);
                    cm.Parameters.AddWithValue("@amount", double.Parse(textamount.Text));
                    cm.Parameters.AddWithValue("@status", combostatus.Text);
                    cm.Parameters.AddWithValue("@comments", textcomment.Text);

                    cm.ExecuteNonQuery();





                    MessageBox.Show("! Expense Has been successfully Saved", "New Expenditure", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clear();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cn.Close();

            }
        }

        public void clear()
        {
           // comboType.Clear();
            textamount.Clear();
            textcomment.Clear();
            comboType.Text = " ";
            combostatus.Text=" ";


        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            try
            {


                if (comboType.Text == "" || textamount.Text == "" || combostatus.Text == "")
                {
                    MessageBox.Show("Please! fill the empty fileds.", "Empty error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblexpense(sdate,type,amount,status,comments) VALUES (@sdate,@type,@amount,@status,@comments)", cn);
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@type", comboType.Text);
                    cm.Parameters.AddWithValue("@amount", double.Parse(textamount.Text));
                    cm.Parameters.AddWithValue("@status", combostatus.Text);
                    cm.Parameters.AddWithValue("@comments", textcomment.Text);

                    cm.ExecuteNonQuery();





                    MessageBox.Show("! Expense Has been successfully Saved", "New Expenditure", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cn.Close();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
