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
using System.IO;

namespace Nazam_pos
{
    public partial class frmemployeeregister : Form
    {
        string filename;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        public frmemployeeregister()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()    { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false }) 
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    filename= ofd.FileName;
                    lblfilename.Text = filename;
                    employeeimage.Image=Image.FromFile(filename);

                }
                
            }
        
        
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            employeeimage.Image = null;
        }

        private void frmemployeeregister_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textname;
            LoadsRecords();
            btnsave.Enabled = true;
            btnupdate.Enabled = false;
            btnDelete.Enabled = false;
        }


        private void textmobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar)&(Keys)e.KeyChar!=Keys.Back & e.KeyChar !='.')
            {
                e.Handled = true;
            }
        }
        byte[] ConvertImageToBinary(Image img)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                img.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();

            }

        }


        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Save this Records? Click Yes to Confirm.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();

                    cm = new SqlCommand("insert into tblEmployee (Name ,Mobile, Email,Cnic,address,salary,joindate,filename,imagedata) values (@name ,@mobile,@email,@cnic,@address,@salary,@joindate,@filename,@imagedata)", cn);
                    cm.Parameters.AddWithValue("@name", textname.Text);
                    cm.Parameters.AddWithValue("@mobile", textmobile.Text);
                    cm.Parameters.AddWithValue("@email", textemail.Text);
                    cm.Parameters.AddWithValue("@cnic", textcnic.Text);
                    cm.Parameters.AddWithValue("@address", textaddress.Text);
                    cm.Parameters.AddWithValue("@salary", textsalary.Text);
                    cm.Parameters.AddWithValue("@joindate", dtjoindate.Value);


                    cm.Parameters.AddWithValue("@filename", lblfilename.Text);
                    cm.Parameters.AddWithValue("@imagedata", ConvertImageToBinary(employeeimage.Image));
                    cm.ExecuteNonQuery();

                    cn.Close();

                    MessageBox.Show("Records has been successfully saved", "Saved Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    LoadsRecords();
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void clear()
        {
            textname.Clear();
            textaddress.Clear();
            textcnic.Clear();
            textemail.Clear();
            textmobile.Clear();
            lblfilename.Text = "";
            textemployeeid.Clear();
            //DateTime=DateTime.Now;
            employeeimage.Image = null;
            btnsave.Enabled = true;
            btnupdate.Enabled = false;
            btnDelete.Enabled = false;
            dtjoindate.Value = DateTime.Now;
            textsalary.Clear();
        }
        public void LoadsRecords()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("Select * from tblEmployee", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

                i++;
                dataGridView1.Rows.Add(i, dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(),dr[6].ToString(),dr[7].ToString(), dr[8].ToString(), dr[9].ToString());

            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int row= dataGridView1.CurrentCell.RowIndex;
           // int col= dataGridView1.CurrentCell.ColumnIndex;
           //textemployeeid.Text=Convert.ToString(row);

            textemployeeid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textmobile.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textemail.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textcnic.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textaddress.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            dtjoindate.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            textsalary.Text=dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
           

            lblfilename.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            employeeimage.Image=Image.FromFile(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
            btnsave.Enabled = false;
            btnupdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Product? ", "Saving Product ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                  
                    // new Statement 
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblemployee SET name=@name, mobile=@mobile, cnic=@cnic ,address=@address, salary=@salary,joindate=@joindate ,imagedata=@imagedata,filename=@filename where empid like @empid", cn);

                    cm.Parameters.AddWithValue("@empid", textemployeeid.Text);
                    cm.Parameters.AddWithValue("@name", textname.Text);
                    cm.Parameters.AddWithValue("@mobile", textmobile.Text);
                    cm.Parameters.AddWithValue("@email", textemail.Text);
                    cm.Parameters.AddWithValue("@cnic", textcnic.Text);
                    cm.Parameters.AddWithValue("@address", textaddress.Text);
                    cm.Parameters.AddWithValue("@salary", textsalary.Text);
                    cm.Parameters.AddWithValue("@joindate", dtjoindate.Value);
                    cm.Parameters.AddWithValue("@filename", lblfilename.Text);
                    cm.Parameters.AddWithValue("@imagedata", ConvertImageToBinary(employeeimage.Image));
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show(" Employee Record has beeen Successfully Updated.");
                    clear();

                    LoadsRecords();


                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to Delete this Product? ", "delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    // new Statement 
                    cn.Open();
                    cm = new SqlCommand("delete from tblEmployee where empid like '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'", cn);



                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show(" Employee Record has beeen Successfully Deleted.");
                    clear();

                    LoadsRecords();


                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void dtjoindate_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                textsalary.Focus();
            }
        }

        private void textsalary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(textsalary.Text.Length>0)
                {
                    btnsave.Focus();
                }
            }
        }
    }
}
