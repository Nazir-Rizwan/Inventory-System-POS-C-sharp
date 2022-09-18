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
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        public string _pass, _user;

        public Form1()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            MyDashboard();
            NotifyCriticalItem();
            

           //cn.Open();
           //MessageBox.Show("Connected ");
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
            cm = new SqlCommand("select * from vwCriticalItems",cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                critical += i +" . " + dr["pdesc"].ToString() + Environment.NewLine;
            }

            dr.Close();
            cn.Close();

            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.icons8_cancel_16;
            popup.TitleText =count + " Critical Items";
            popup.ContentText = critical;
            popup.Popup();

        }


        private void panel2_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            fmbrandlist frm = new fmbrandlist();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (DateTime.Now.ToString("MM") == "02" && DateTime.Now.ToString("dd") == "05" && DateTime.Now.ToString("yyyy") == ("2022"))
            //{
              //  Application.Exit();

            //}
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategoryList frm = new frmCategoryList();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadCategory();
            frm.Show();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            fmproductlist frm = new fmproductlist();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadRecords();
            frm.Show();
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            fmstockin frm = new fmstockin();
            //frm.TopLevel = false;
            //panel3.Controls.Clear();
            //panel3.Controls.Add(frm);
            //frm.BringToFront();
            frm.Show();



        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            //fmPOS frm = new fmPOS(this);
            //frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmUserAccount f = new frmUserAccount(this);
            f.TopLevel = false;
            
            panel3.Controls.Add(f);
            f.txtuser1.Text = _user;
            f.BringToFront();
            f.Show();
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            frmSolditem frm = new frmSolditem();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmRecords frm = new frmRecords();
            frm.TopLevel = false;
            frm.loadcriticalitems();
            frm.loadInventorylist();
            frm.CancelledOrders();
            frm.loadstockinhistory();

            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmStore f = new frmStore();
            f.loadRecords();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyDashboard();
        }

        public void MyDashboard()
        {
            frmDashboard f = new frmDashboard();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.lblDailySales.Text = dbcon.DailySales().ToString("#,##0.00");
            f.lblProduct.Text = dbcon.ProductLine().ToString("#,##0");
            f.lblStockOnhand.Text = dbcon.StockOnHand().ToString("#,##0");
            f.lblCritical.Text = dbcon.CriticalItem().ToString("#,##0");

            f.BringToFront();
            f.Show();
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            frmVendorList frm = new frmVendorList();
            frm.TopLevel = false;
            frm.LoadsRecords();
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmdatabackup f = new frmdatabackup();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();

            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            fmPOS frm = new fmPOS(this);
            //frm.lblUser.Text = _username;
            //frm.lblName.Text = _name + "|   " + _role;
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)// button customer 
        {
            frmcustomerlist frm = new frmcustomerlist();
            frm.TopLevel = false;
            frm.loadrecords();
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            //Application.Exit();
            //this.Close();
            //this.Hide();
            //frmdatabackup f = new frmdatabackup();
            //f.Show();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout Application ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frmSecurity frm = new frmSecurity();
                frm.ShowDialog();
            }
        }

        private void dailyExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addExpense frm =new  addExpense();
            frm.Show();

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int intx = Screen.PrimaryScreen.Bounds.Width;
            int inty = Screen.PrimaryScreen.Bounds.Height;
            this.Width = intx;
            this.Height = inty - 20;
            this.Top = 0;
            this.Left = 0;

        }

        private void recordExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            expenserecord f = new expenserecord();
            f.Show();

        }

        private void addEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmemployeeregister f = new frmemployeeregister();
            f.Show();

        }

        private void updateEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmemployeesalary f = new frmemployeesalary();
            f.Show();
        }

        private void BTNadjustment_Click(object sender, EventArgs e)
        {
            frmstockAdjustment f = new frmstockAdjustment(this);
            f.LoadRecords();
            f.txtuser.Text = lblUser.Text; // ya line check kerni h abi lblname kaya ha  //lbluser ko ha 
            f.Referenceno();
            f.ShowDialog();
        }
    }
}
