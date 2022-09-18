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
    public partial class fmQty : Form


    {


        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        private String pcode;
        private double price;
        private int qty;
        private String transno;

        string stitle = "Inventory Management system ";
        fmPOS fpos;


        public fmQty(fmPOS  frmpos)
        {
            InitializeComponent();

            cn = new SqlConnection(dbcon.Myconnection());
            fpos = frmpos;
        }

        public void ProductDetail(String pcode, double price , String transno , int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty;

        }


        private void fmQty_Load(object sender, EventArgs e)
        {
           // txtQty.Text = "0";
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar==13) && (txtQty.Text!=String.Empty))
                    {
                string id="";
                int cart_qty=0;
                bool found =false;
                
                cn.Open();
                cm = new SqlCommand("select * from tblCart where transno =@transno and pcode =@pcode ",cn);
                cm.Parameters.AddWithValue("@transno",fpos.lblTransno.Text);
                cm.Parameters.AddWithValue("@pcode", pcode);
                dr = cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    found = true;
                    id = dr["id"].ToString();
                    cart_qty =int.Parse( dr["qty"].ToString());
                      
                }
                else
                {
                    found = false;
                }
                dr.Close();
                cn.Close();

                if(found == true)
                {
                    if (qty < (int.Parse(txtQty.Text)+ cart_qty))
                    {
                        MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    cn.Open();
                    cm = new SqlCommand("update tblCart set qty = (qty + " + int.Parse(txtQty.Text)   +") where id = '"+ id +"'", cn);
                   
                    cm.ExecuteNonQuery();

                    cn.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.loadcart();
                    this.Dispose();

                }
                else
                {
                    if (qty < int.Parse(txtQty.Text))
                    {
                        MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cn.Open();
                    cm = new SqlCommand("insert into tblCart (transno ,pcode,price,qty,sdate,cashier) values (@transno ,@pcode,@price,@qty,@sdate,@cashier)", cn);
                    cm.Parameters.AddWithValue("@transno", transno);
                    cm.Parameters.AddWithValue("@pcode", pcode);
                    cm.Parameters.AddWithValue("@price", price);
                    cm.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@cashier", fpos.lblUser.Text);
                    cm.ExecuteNonQuery();

                    cn.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.loadcart();
                    this.Dispose();

                }


            }

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
