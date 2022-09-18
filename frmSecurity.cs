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
    public partial class frmSecurity : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        public string _pass, _username = "";
        public bool _isactive = false;
        public frmSecurity()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Apllication?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string _role="",_name = "";
            try
            {
                bool found = false;
                cn.Open();
                cm = new SqlCommand("Select * from tblUser where username=@username and password =@password", cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                dr= cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    found = true;
                    _username = dr["username"].ToString();
                    _role = dr["role"].ToString();
                    _name = dr["name"].ToString();
                    _pass = dr["password"].ToString();
                    _isactive = bool.Parse(dr["isactive"].ToString());

                }
                else
                {
                    found = false;
                }
                dr.Close();
                cn.Close();
                if (found == true)
                {
                    if(_isactive==false)
                    {
                        MessageBox.Show("Account is inactive. Unable to login", "Inactive account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    if (_role == "Cashier")
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPass.Clear();
                        txtUser.Clear();
                        this.Hide();
                        //fmPOS frm = new fmPOS(this);
                        //frm.lblUser.Text = _username;
                        //frm.lblName.Text = _name+ "|   "+ _role;
                        //frm.ShowDialog();
                    }
                 else   
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPass.Clear();
                        txtUser.Clear();
                        this.Hide();
                        Form1 frm = new Form1();
                        frm.lblname.Text = _name;
                        frm.lblUser.Text = _username;
                        frm._pass = _pass;
                        frm._user = _username;
                        frm.lblRole.Text = _role;
                        //frm.MyDashboard();
                        frm.ShowDialog();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "ACCESS Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmSecurity_Load(object sender, EventArgs e)
        {
            //textBox1.Text = txtPass.Text.ToString();
            //txtPass.UseSystemPasswordChar = false;
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            bool check = checkBox1.Checked;
            switch (check)
            {
                case true:
                    txtPass.UseSystemPasswordChar = false;
                    break;
                default:
                    txtPass.UseSystemPasswordChar = true;
                    break;

            }



            //if (checkBox1.Checked == true)
            //{
            //    txtPass.UseSystemPasswordChar = false;
            //}
            //else
            //{
            //    txtPass.UseSystemPasswordChar = true;
            //}
            //bool check = checkBox1.Checked;



            //switch (check)
            //{
            //    case true:
            //        txtPass.UseSystemPasswordChar = false;
            //        break;
            //    default:
            //        txtPass.UseSystemPasswordChar = true;
            //        break;

            //}
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            bool check = checkBox1.Checked;
            switch (check)
            {
                case true:
                    txtPass.UseSystemPasswordChar = false;
                    break;
                default:
                    txtPass.UseSystemPasswordChar = true;
                    break;

            }

        }

        private void txtUser_Click(object sender, EventArgs e)
        {

        }
    }
}
