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
using Tulpep.NotificationWindow;
namespace Nazam_pos
{
    public partial class fmPOS : Form
    {

        string id;
        string price;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr ;

        string stitle = "Inventory Management system ";
        int qty;
        frmSecurity f;
        Form1 f1;
        public static string passingtext;
        public static double totalvalue;
        


                    // yaha pa frmsecurity frm tha (frmsecurity frm )
        public fmPOS(Form1 frm)
        {
            InitializeComponent();
            lblDate.Text = DateTime.Now.ToLongDateString();

            cn = new SqlConnection(dbcon.Myconnection());
            this.KeyPreview = true;
            f1 = frm;

            NotifyCriticalItem();
            autoProduct();
        }
        public void autoProduct()
        {
            cn.Open();
            cm = new SqlCommand("select pdesc from tblproducts where pdesc like '" + txtSearch.Text + "%'", cn);
            dr = cm.ExecuteReader();
            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                auto.Add(dr["pdesc"].ToString());
            }
            txtSearch.AutoCompleteCustomSource = auto;
            dr.Close();
            cn.Close();
        }
        public void NotifyCriticalItem()
        {
            string critical = "";
            cn.Open();
            cm = new SqlCommand("select count(*) from vwCriticalItems", cn);
            string count = cm.ExecuteScalar().ToString();
            cn.Close();


            int i = 0;
            cn.Open();
            cm = new SqlCommand("select * from vwCriticalItems", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                critical += i + " . " + dr["pdesc"].ToString() + Environment.NewLine;
            }

            dr.Close();
            cn.Close();

            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.icons8_cancel_16;
            popup.TitleText = count + " Critical Items";
            popup.ContentText = critical;
            popup.Popup();

        }


        public void GetTransno()

        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transno;
                int count;
                cn.Open();
                cm = new SqlCommand("select top 1 transno from tblCart where transno like '" + "%' order by id desc",cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse( transno.Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1);

                }
                else
                {
                    transno = sdate + "1001";
                    lblTransno.Text = transno;
                }
                dr.Close();
                cn.Close();

            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle,MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        
        
        
        
        }






        private void BTNNew_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>0)
            {
                return;
            }
            GetTransno();
            txtSearch.Enabled = true;
            txtSearch.Focus();


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text == string.Empty)
                {
                    return;
                }
                else
                {
                      String _pcode;
                     double _price;
                      int _qty;
                     cn.Open();
                    cm = new SqlCommand("select * from tblProducts where barcode like '" + txtSearch.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        qty = int.Parse(dr["qty"].ToString());
                        //fmQty frm = new fmQty(this);
                        //frm.ProductDetail(dr["pcode"].ToString(), double.Parse(dr["price"].ToString()), lblTransno.Text,qty);

                        _pcode = dr["pcode"].ToString();
                        _price =double.Parse( dr["price"].ToString());
                        _qty = int.Parse(txtQty.Text);


                        dr.Close();
                        cn.Close();

                        //frm.ShowDialog();
                        AddtoCart(_pcode, _price, _qty);

                    }
                    else
                    {
                        dr.Close();
                        cn.Close();
                    }

                    
                }


            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void AddtoCart(String _pcode, double _price , int _qty)
        {
            string id = "";
            bool found = false;
            //bool _QTY = true;
            int cart_qty=0;
            cn.Open();
            cm = new SqlCommand("select * from tblCart where transno =@transno and pcode =@pcode ", cn);
            cm.Parameters.AddWithValue("@transno",lblTransno.Text);
            cm.Parameters.AddWithValue("@pcode", _pcode);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                found = true;
                id = dr["id"].ToString();
                cart_qty = int.Parse(dr["qty"].ToString());

            }
            else
            {
                found = false;
            }
            

                //checkBox1.Checked = bool.Parse(dr["qty"].ToString());
            

            dr.Close();
            cn.Close();

            if (found == true)
            {
                if (qty < (int.Parse(txtQty.Text) + cart_qty))
                {
                    MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    //if(checkBox1.Checked==true)
                    //{
                    //    cn.Open();
                    //    MessageBox.Show("else condition");

                    //    cm = new SqlCommand("update tblCart set qty = (qty + " + _qty + ") where id = '" + id + "'", cn);
                                    
                    //    cm.ExecuteNonQuery();

                    //    cn.Close();

                    //}

                }

                cn.Open();
                cm = new SqlCommand("update tblCart set qty = (qty + " + _qty + ") where id = '" + id + "'", cn);

                cm.ExecuteNonQuery();

                cn.Close();


                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                loadcart();
               // this.Dispose();

            }
            else
            {

                if (qty < int.Parse(txtQty.Text) && checkBox1.Checked == false)
                {

                    MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                //if (checkBox1.Checked == true)
                //{
                //    cn.Open();
                //     MessageBox.Show("Less Quantity. Please Update stock In","INFORMATION" );

                //    cm = new SqlCommand("update tblCart set qty = (qty + " + _qty + ") where id = '" + id + "'", cn);

                //    cm.ExecuteNonQuery();

                //    cn.Close();
                   

                //}

                


                //}
                cn.Open();
                cm = new SqlCommand("insert into tblCart (transno ,pcode,price,qty,sdate,cashier) values (@transno ,@pcode,@price,@qty,@sdate,@cashier)", cn);
                cm.Parameters.AddWithValue("@transno",lblTransno.Text);
                cm.Parameters.AddWithValue("@pcode", _pcode);
                cm.Parameters.AddWithValue("@price", _price);
                cm.Parameters.AddWithValue("@qty", _qty);
                cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                cm.Parameters.AddWithValue("@cashier", lblUser.Text);
                cm.ExecuteNonQuery();

                cn.Close();

                
                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                loadcart();
                //this.Dispose();

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName== "Delete")
            {
                if(MessageBox.Show(" Remove this item?" ,"Remove item", MessageBoxButtons. YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand(" delete from tblCart where id like'" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()+ "'",cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Item has been Successfully Removed",stitle,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    loadcart();

                }
            }

            else if(colName=="colAdd") 
            {
                int i = 0;
                cn.Open();

                cm = new SqlCommand("select sum(qty) as qty  from tblProducts where pcode like '" +dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()+ "'group by pcode",cn);

                i = int.Parse(cm.ExecuteScalar().ToString());

                
                cn.Close();
                if(int.Parse( dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString())< i)
                {
                    cn.Open();
                    cm = new SqlCommand("update tblcart set qty= qty + " +int.Parse(txtQty.Text)+" where transno like '"+lblTransno.Text+"'and pcode like '"+ dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()+"'",cn);
                    cm.ExecuteNonQuery();

                    cn.Close();

                    loadcart();
                }
                else
                {
                    MessageBox.Show("Remaining Qty on hand is " + i + " !","Out of stock.",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                
                }
            }


            else if (colName == "colRemove")
            {
                int i = 0;
                cn.Open();

                cm = new SqlCommand("select sum(qty) as qty  from tblCart where pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'and transno like '"+ lblTransno.Text+"'group by transno,pcode", cn);

                i = int.Parse(cm.ExecuteScalar().ToString());


                cn.Close();
                if (i>1)
                {
                    cn.Open();
                    cm = new SqlCommand("update tblcart set qty= qty - " + int.Parse(txtQty.Text) + " where transno like '" + lblTransno.Text + "'and pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();

                    cn.Close();

                    loadcart();
                }
                else
                {
                    MessageBox.Show("Remaining Qty on cart is " + i + " !", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
            }


        }
        public void loadcart()
        {


            try

            {
                Boolean hasrecord = false;
                dataGridView1.Rows.Clear();
                int i = 0;
                double total = 0;
                double discount = 0;
                cn.Open();
                cm = new SqlCommand("select c.id, c.pcode, p.pdesc, c.price, c.qty,c.discount ,c.total  from tblCart as c inner join tblProducts as p on c.pcode = p.pcode where transno like '" + lblTransno.Text + "' and status like 'Pending'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    total += Double.Parse(dr["total"].ToString());
                    discount += Double.Parse(dr["discount"].ToString());
                    dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["discount"].ToString(), Double.Parse(dr["total"].ToString()).ToString("#,##0.00"), "ADD 1 Qty", "Remove 1 Qty");
                    hasrecord = true;
                }
                dr.Close();
                cn.Close();
                lblTotal.Text = total.ToString("#,##0.00");

                lblDiscount.Text = discount.ToString("#,##0.00");
                GetCartTotal();
                if(hasrecord= true)
                {
                    btnSettle.Enabled = true;
                    btnDiscount.Enabled = true;
                    btnCancel.Enabled = true;
                }
                else
                {
                      btnSettle.Enabled = false;
                    btnDiscount.Enabled = false;
                    btnCancel.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(lblTransno.Text== "000000000000000")
            {
                return; 
            }
            fmLookup frm = new fmLookup(this);
            frm.LoadRecords();
            frm.ShowDialog();
        }

        public  void GetCartTotal()
        {



            double sales = Double.Parse(lblTotal.Text);

            double discount = Double.Parse(lblDiscount.Text);
           
            //double vat = sales * dbcon.GetVal();
            //double vatable = sales-vat;
           
           // lblVat.Text = vat.ToString("#,##0.00");
            //lblVatable.Text = vatable.ToString("#,##0.00");
            lblDisplayTotal.Text = sales.ToString("#,##0.00");
           

        }
        //public void ledger()
        //{
        //    double sales = Double.Parse(lblTotal.Text);

        //}




        private void fmPOS_Load(object sender, EventArgs e)
        {
            autoProduct();
            BTNNew_Click(sender, e);
            //this.ActiveControl = txtSearch;
            //this.Refresh();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            
            int i = dataGridView1.CurrentRow.Index;
            id = dataGridView1[1, i].Value.ToString();
            price = dataGridView1[7, i].Value.ToString();

            frmDiscount frm = new frmDiscount(this);
            frm.lblD.Text = id;
            frm.txtPrice.Text = price;

            frm.ShowDialog();



        }

        public void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //int i = dataGridView1.CurrentRow.Index;
            //id = dataGridView1[1, i].Value.ToString();
            //price = dataGridView1[7, i].Value.ToString();




        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                GetTransno();
            }
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");  // after ss than write tt for am / pm show time 
            lblDate1.Text = DateTime.Now.ToShortDateString();                     //ToLongDateString()
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }
        
        private void btnSettle_Click(object sender, EventArgs e)
        {
            frmsettle frm = new frmsettle(this);
            frm.txtSale.Text = lblDisplayTotal.Text;
            
            frm.ShowDialog();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            frmSolditem frm = new frmSolditem();
            frm.dt1.Enabled = false;
            frm.dt2.Enabled = false;
            frm.suser = lblUser.Text;
            frm.cboCashier.Enabled = false;
            frm.cboCashier.Text = lblUser.Text;
            frm.loadCashier();
            frm.LoadRecord();
            
            frm.Show();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>0)
            {
                MessageBox.Show("Unable to Logout. Please cancel the transaction.", "warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if(MessageBox.Show("Logout Application ?","Logout",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                this.Hide();
                //frmSecurity frm = new frmSecurity();
                //frm.ShowDialog();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                BTNNew_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearch_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDiscount_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnSettle_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                txtSearch_TextChanged(sender, e);
            }
            else if (e.KeyCode==Keys.F5)
            {
                btnCancel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnChangePass_Click(sender, e);
            }
            else if (e.KeyCode==Keys.F6)
            {
                btnSale_Click(sender, e);
            }

            //else if (e.KeyCode == Keys.F6)
            //{
            //    btnSale_Click(sender, e);
            //}

            //else if (e.KeyCode == Keys.F7)
            //{
            //    btnChangePass_Click(sender, e);
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Remove all item from cart?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("delete from tblcart where transno like '" + lblTransno.Text + "'", cn);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("All item has been successfully removes! ", "Remove item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadcart();
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(this);
            frm.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtSearch.Text == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        String _pcode;
                        double _price;
                        int _qty;
                        cn.Open();
                        cm = new SqlCommand("select * from tblProducts where pdesc like '" + txtSearch.Text + "'", cn);
                        dr = cm.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows)
                        {
                            qty = int.Parse(dr["qty"].ToString());
                            //fmQty frm = new fmQty(this);
                            //frm.ProductDetail(dr["pcode"].ToString(), double.Parse(dr["price"].ToString()), lblTransno.Text,qty);

                            _pcode = dr["pcode"].ToString();
                            _price = double.Parse(dr["price"].ToString());
                            _qty = int.Parse(txtQty.Text);


                            dr.Close();
                            cn.Close();

                            //frm.ShowDialog();
                            AddtoCart(_pcode, _price, _qty);

                        }
                        else
                        {
                            dr.Close();
                            cn.Close();
                        }


                    }


                }
                catch (Exception ex)
                {
                    cn.Close();
                    MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void lblVat_Click(object sender, EventArgs e)
        {

        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            //double sales = Double.Parse(lblTotal.Text);
            passingtext = lblTotal.Text;
            totalvalue = double.Parse(lblTotal.Text);
            frmledgners f = new frmledgners();
           // f.Loadcutomer();
            
           
           
            f.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fmPOS frm = new fmPOS(f1);
            //frm.lblUser.Text = _username;
            //frm.lblName.Text = _name + "|   " + _role;
            frm.Show();

        }

        private void Ledgerbtn_Click(object sender, EventArgs e)
        {
            //Customerledger f = new Customerledger();
            //f.Show();
            // frmledgerrecord f = new frmledgerrecord();
            frmledgners f = new frmledgners();
            f.Show();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           //if(checkBox2.Checked==true)
           // {
           //     updatedata("update tblProduct set qty= qty + " + int.Parse(f.txtCancelQty.Text) + " where pcode ='" + f.txtPcode.Text + "'");

           // }
           // updatedata("update tblcart set qty=qty - " + int.Parse(f.txtCancelQty.Text) + " where id like '" + f.txtID.Text + "'");
           // MessageBox.Show("Order transaction successfully cancelled!", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
           // this.Dispose();
           // dr.Close();
           // cn.Close();
        }
    }
}
