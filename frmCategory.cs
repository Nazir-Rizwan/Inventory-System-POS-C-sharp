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
    public partial class frmCategory : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        frmCategoryList flist;
        string title = "Inventory category";
        SqlDataReader dr;



        public frmCategory(frmCategoryList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            flist = frm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        public void Clear()
        {
            BTNSave.Enabled = true;
            btnupdate.Enabled = false;
            textCategory.Clear();
            textCategory.Focus();
        }


        private void BTNSave_Click(object sender, EventArgs e)
        {
            bool found = false;

            try
            {
                if (textCategory.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Brand Text ", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textCategory.Focus();
                    return;
                }


                cn.Open();
                cm = new SqlCommand("select  Category from tblCategory where Category ='" + textCategory.Text + "'", cn);
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
                cn.Close();
                dr.Close();

                if (found == true)
                {
                    MessageBox.Show("Already Category exist ?", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;

                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to save this Category? ", "Saving Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblCategory(category)VALUES(@category)", cn);
                        cm.Parameters.AddWithValue("@category", textCategory.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show(" Category has beeen Successfully saved .");
                        Clear();
                        flist.LoadCategory();

                    }
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void frmCategory_Load(object sender, EventArgs e)
        {

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Category?", "Update Category ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();

                    cm = new SqlCommand(" update tblCategory set category = @category where id like '" + lblID.Text + "'", cn);
                    cm.Parameters.AddWithValue("@category", textCategory.Text);
                    cm.ExecuteNonQuery();

                    cn.Close();
                    MessageBox.Show("Category has been Successfully updated. ");
                    Clear();
                    flist.LoadCategory();
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
    }
}
