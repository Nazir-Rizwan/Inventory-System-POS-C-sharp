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
    public partial class frmVoid : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        frmCancelDetails f;

        public frmVoid(frmCancelDetails frm)
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
                if (txtPass.Text != string.Empty)
                {
                    string user;

                    cn.Open();
                    cm = new SqlCommand("select * from tblUser where username= @username and password= @password ", cn);
                    cm.Parameters.AddWithValue("@username", txtUser.Text);
                    cm.Parameters.AddWithValue("@password", txtPass.Text);
                    dr = cm.ExecuteReader();

                    dr.Read();
                    if (dr.HasRows)
                    {
                        user = dr["username"].ToString();
                        dr.Close();
                        cn.Close();

                        SaveCancel(user);
                        if (f.cboAction.Text == "Yes")
                        {
                            updatedata("update tblProduct set qty= qty + " + int.Parse(f.txtCancelQty.Text) + " where pcode ='" + f.txtPcode.Text + "'");

                        }
                        updatedata("update tblcart set qty=qty - " + int.Parse(f.txtCancelQty.Text) + " where id like '" + f.txtID.Text + "'");
                        MessageBox.Show("Order transaction successfully cancelled!", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                        f.RefreshList();
                        f.Dispose();
                    }
                    dr.Close();
                    cn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            

        }
        public void SaveCancel( string user)
        {
            cn.Open();
            cm = new SqlCommand("insert into tblCancel (transno ,pcode ,price, qty, sdate ,voidby, cancelledby,reason,action ) values (@transno ,@pcode,@price, @qty, @sdate ,@voidby, @cancelledby,@reason,@action )",cn);
            cm.Parameters.AddWithValue("@transno", f.txtTransNo.Text);
            cm.Parameters.AddWithValue("@pcode", f.txtPcode.Text);
            cm.Parameters.AddWithValue("@price", double.Parse(f.txtPrice.Text));
            cm.Parameters.AddWithValue("@qty", int.Parse(f.txtCancelQty.Text));
            cm.Parameters.AddWithValue("@sdate", DateTime.Now);
            cm.Parameters.AddWithValue("@voidby", user);
            cm.Parameters.AddWithValue("@cancelledby", f.txtCancel.Text);
            cm.Parameters.AddWithValue("@reason", f.txtReason.Text);
            cm.Parameters.AddWithValue("@action", f.cboAction.Text);
            cm.ExecuteNonQuery();
            cn.Close();
        }

        public void updatedata(string sql)
        {
            cn.Open();
            cm = new SqlCommand(sql,cn);
            cm.ExecuteNonQuery();

            cn.Close();
        }

        private void frmVoid_Load(object sender, EventArgs e)
        {

        }
    }
}
