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
    public partial class frmUserAccount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        Form1 f;
        public frmUserAccount(Form1 f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            this.f = f;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmUserAccount_Resize(object sender, EventArgs e)
        {
            metroTabControl1.Left = (this.Width - metroTabControl1.Width)/2;
            metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Clear()
        {
            txtName.Clear();
            txtpass.Clear();
            txtRe.Clear();
            txtUser.Clear();
            cborole.Text = "";
            txtUser.Focus();

        }

        private void frmUserAccount_Load(object sender, EventArgs e)
        {
            //txtpass.PasswordChar = char."•";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtpass.Text != txtRe.Text)
                {
                    MessageBox.Show("Password did not match!" , "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new SqlCommand("insert into tblUser (username, password , role , name )values(@username, @password , @role , @name ) ", cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtpass.Text);
                cm.Parameters.AddWithValue("@role", cborole.Text);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.ExecuteNonQuery();

                cn.Close();
                MessageBox.Show("New account has saved!");
                Clear();
            }catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void txtUser2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("select * from tbluser where username=@username", cn);
                cm.Parameters.AddWithValue("@username", txtUser2.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    checkBox1.Checked = bool.Parse(dr["isactive"].ToString());

                }
                else
                {
                    checkBox1.Checked = false;
                }
                dr.Close();
                cn.Close();

            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                bool found = true;
                cn.Open();
                cm = new SqlCommand("select * from tbluser where username=@username", cn);
                cm.Parameters.AddWithValue("@username", txtUser2.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                }
                else
                {
                    found = false;
                }
                dr.Close();
                cn.Close();
                if (found == true)
                {


                    cn.Open();
                    cm = new SqlCommand("update tbluser set isactive=@isactive where username=@username", cn);
                    cm.Parameters.AddWithValue("@isactive", checkBox1.Checked.ToString());
                    cm.Parameters.AddWithValue("@username", txtUser2.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Accounts status has been successfully updated.", "Updated Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUser2.Clear();
                    checkBox1.Checked = false;

                }

                else
                {
                    MessageBox.Show("Accounts not exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
