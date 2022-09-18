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
    public partial class frmChangePassword : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        fmPOS f;

        public frmChangePassword(fmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            f = frm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                string _oldpass = dbcon.GetPassword(f.lblUser.Text);
                if(_oldpass != txtOld.Text)
                {
                    MessageBox.Show("Old password did not matched! ","Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtNew.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Confirm new password did not matched! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    
                    if(MessageBox.Show("Change Password?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("update tblUser set password =@password where username =@username", cn);
                        cm.Parameters.AddWithValue("@password", txtNew.Text);
                        cm.Parameters.AddWithValue("@username", f.lblUser.Text);

                        cm.ExecuteNonQuery();
                        cn.Close();

                        MessageBox.Show("Password has beeen successfully saved!", "Save Changes", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        this.Dispose();

                    }


                }


            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERRORR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
