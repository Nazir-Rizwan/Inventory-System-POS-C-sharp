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
    public partial class frmdatabackup : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBconnection dbcon = new DBconnection();

        public frmdatabackup()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
           
        }

        private void btnbackup_Click(object sender, EventArgs e)
        {
            
        }

        private void btnbrowse1_Click(object sender, EventArgs e)
        {

          
        }

        private void btnrestore_Click(object sender, EventArgs e)
        {
            string database = cn.Database.ToString();
            
            try
            {
                if(cn.State!=ConnectionState.Open)
                {
                    cn.Open();
                }
                //cn.Open();
                string str1 = string.Format("Alter Database [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE ");
                SqlCommand cmd1 = new SqlCommand(str1, cn);
                cmd1.ExecuteNonQuery();

                string str2 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + textBox2.Text + "'WITH REPLACE;";
                SqlCommand cmd2 = new SqlCommand(str2, cn);
                cmd2.ExecuteNonQuery();
                string str3 = string.Format("ALTER DATEBASE [" + database + "]SET MULTI_USER ");
                SqlCommand cmd3 = new SqlCommand(str3, cn);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Database restore done successfully");
                cn.Close();
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnrestore_Click_1(object sender, EventArgs e)
        {
            string database = cn.Database.ToString();

            try
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }
                //cn.Open();
                string str1 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE ");
                SqlCommand cmd1 = new SqlCommand(str1, cn);
                cmd1.ExecuteNonQuery();

                string str2 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK = '" + textBox2.Text + "'WITH REPLACE;";
                SqlCommand cmd2 = new SqlCommand(str2, cn);
                cmd2.ExecuteNonQuery();
                string str3 = string.Format("ALTER DATABASE [" + database + "]SET MULTI_USER ");
                SqlCommand cmd3 = new SqlCommand(str3, cn);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Database restore done successfully");
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnbrowse1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Sql Server database backup files |* .bak";
            dlg.Title = "Database restore ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dlg.FileName;
                btnrestore.Enabled = true;
            }
        }

        private void btnbrowse_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.SelectedPath;
                btnbackup.Enabled = true;
            }
        }

        private void btnbackup_Click_1(object sender, EventArgs e)
        {
            string database = cn.Database.ToString();
            try
            {
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Backup File Location ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cn.Open();
                    string cmd = "Backup Database [" + database + "] TO DISK='" + textBox1.Text + "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".bak'";

                    SqlCommand command = new SqlCommand(cmd, cn);
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    command.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Database backup done successfully");

                    btnbackup.Enabled = false;

                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);

            }
        }
    }
}
