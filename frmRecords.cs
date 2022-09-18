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
using System.Windows.Forms.DataVisualization.Charting;
namespace Nazam_pos
{
    public partial class frmRecords : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBconnection dbcon = new DBconnection();

        public frmRecords()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        public void loadRecord()
        {
            int i = 0;
            cn.Open();
            dataGridView1.Rows.Clear();
           
            if(cboTopelect.Text=="SORT BY QTY")
            {
                cm = new SqlCommand("select top 10 pcode ,pdesc, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + dateTimePicker1.Value.ToShortDateString() + "'and'" + dateTimePicker2.Value.ToShortDateString() + "'and status like 'Sold' group by pcode,pdesc order by qty desc", cn);


            }
            else if (cboTopelect.Text == "SORT BY TOTAL AMOUNT")
            {
                cm = new SqlCommand("select top 10 pcode ,pdesc, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + dateTimePicker1.Value.ToShortDateString() + "'and'" + dateTimePicker2.Value.ToShortDateString() + "'and status like 'Sold' group by pcode,pdesc order by total desc", cn);

            }

            
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString(),double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
            }
            cn.Close();
        }

        public void CancelledOrders()
        {
            int i = 0;
            cn.Open();
            dataGridView5.Rows.Clear();
            
             

            cm = new SqlCommand("select * from vwcancelledorder where sdate between '" + dateTimePicker5.Value.ToShortDateString() + "'and'" + dateTimePicker6.Value.ToShortDateString()+ "'",cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dataGridView5.Rows.Add(i, dr["transno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["total"].ToString(), dr["sdate"].ToString(), dr["voidby"].ToString(), dr["cancelledby"].ToString(), dr["reason"].ToString(), dr["action"].ToString());

            }
            dr.Close();
            cn.Close();

            //create view vwCancelledOrder as
            //select c.transno , c.pcode , p.pdesc ,c.price ,c.qty,c.total,c.sdate,c.voidby,c.cancelledby,c.reason,c.action
            //from tblCancel as c inner join tblProduct as p on c.pcode = p.pcode
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadRecord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("select  c.pcode, p.pdesc, c.price,  sum (c.qty) as tot_qty, sum (c.disc) as tot_disc, sum(c.total) as total  from tblCart as c inner join tblProduct as p on c.pcode = p.pcode  where status like 'Sold' and sdate between '" + dateTimePicker4.Value.ToShortDateString() + "'and'" + dateTimePicker3.Value.ToShortDateString() + "' group by c.pcode , p.pdesc, c.price", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView2.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), double.Parse( dr["price"].ToString()).ToString("#,##0.00"), dr["tot_qty"].ToString(), dr["tot_disc"].ToString(),double.Parse( dr["total"].ToString()).ToString("#,##0.00"));
                }
                dr.Close();
                cn.Close();

                string x;
                cn.Open();
                cm = new SqlCommand("select  isnull (sum(total),0)  from tblCart   where status like 'Sold' and sdate between '" + dateTimePicker4.Value.ToShortDateString() + "'and'" + dateTimePicker3.Value.ToShortDateString() + "'", cn);
                lblTotal.Text=Double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");

                cn.Close();

            }
            catch (Exception ex)

            {
                cn.Close();
                MessageBox.Show(ex. Message, "Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void loadInventorylist()
        {
            int i = 0;
            dataGridView4.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select p.pcode ,p.barcode,p.pdesc,b.brand,c.category,p.price, p.qty, p.reorder  from tblProducts as p inner join tblBrand as b on p.bid=b.id inner join tblCategory as c on p.cid=c.id ",cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dataGridView4.Rows.Add(i, dr["pcode"].ToString(), dr["barcode"].ToString(), dr["pdesc"].ToString(), dr["brand"].ToString(), dr["category"].ToString(), dr["price"].ToString(), dr["reorder"].ToString(), dr["qty"].ToString());

            }

            dr.Close();
            cn.Close();
        }
    
        
        
        public void loadcriticalitems()
        {
            try
            {
                dataGridView3.Rows.Clear();
                int i = 0;
                cn.Open();
                //create view vwCriticalItems as select p.pcode ,p.barcode ,p.pdesc,b.brand ,c.category ,p.price , p.reorder , p.qty  from tblProduct as p inner join 
                //tblBrand as b on b.id = p.bid inner join tblCategory as c on c.id = p.cid where qty <= reorder
                
                cm = new SqlCommand("select * from vwCriticalItems", cn);
                dr = cm.ExecuteReader();
                while(dr.Read())
                {
                    i++;
                    dataGridView3.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());

                }
                dr.Close();
                cn.Close();


            }catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frminventoryreport frm = new frminventoryreport();
            frm.loadreport();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CancelledOrders();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadstockinhistory();
        }
        public void loadstockinhistory()
        {
            int i = 0;
            dataGridView6.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("Select * from vwstockin   where cast(sdate as date) between '" + dateTimePicker7.Value.ToShortDateString() + "' and '" + dateTimePicker8.Value.ToShortDateString() + "' and status like 'Done' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;

                dataGridView6.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString());

            }
            dr.Close();
            cn.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frminventoryreport f = new frminventoryreport();

            if (cboTopelect.Text == "SORT BY QTY")
            {
                f.LoadTopSelling("select top 10 pcode ,pdesc, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + dateTimePicker1.Value.ToShortDateString() + "'and'" + dateTimePicker2.Value.ToShortDateString() + "'and status like 'Sold' group by pcode,pdesc order by qty desc", "From : " + dateTimePicker1.Value.ToShortDateString() + "To :" + dateTimePicker2.Value.ToShortDateString(),"Top Selling Items Sort By QTY ");


            }
            else if (cboTopelect.Text == "SORT BY TOTAL AMOUNT")
            {
                //cm = new SqlCommand("select top 10 pcode ,pdesc, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + dateTimePicker1.Value.ToShortDateString() + "'and'" + dateTimePicker2.Value.ToShortDateString() + "'and status like 'Sold' group by pcode,pdesc order by total desc", cn);
               f.LoadTopSelling("select top 10 pcode ,pdesc, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total from vwSoldItems where sdate between '" + dateTimePicker1.Value.ToShortDateString() + "'and'" + dateTimePicker2.Value.ToShortDateString() + "'and status like 'Sold' group by pcode,pdesc order by total desc", "From : " + dateTimePicker1.Value.ToShortDateString() + "To :" + dateTimePicker2.Value.ToShortDateString(),"Top Selling Items Sort By Total Amount");


            }


           
             f.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frminventoryreport f = new frminventoryreport();
            f.LoadSoldItem("select  c.pcode, p.pdesc, c.price, sum(c.qty) as tot_qty, sum(c.disc) as tot_disc, sum(c.total) as total  from tblCart as c inner join tblProduct as p on c.pcode = p.pcode  where status like 'Sold' and sdate between '" + dateTimePicker4.Value.ToShortDateString() + "'and'" + dateTimePicker3.Value.ToShortDateString() + "' group by c.pcode, p.pdesc, c.price", "From : " + dateTimePicker4.Value.ToShortDateString() + "To :" + dateTimePicker3.Value.ToShortDateString());
            f.ShowDialog();
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(cboTopelect.Text==string.Empty)
            {
                MessageBox.Show("Please select from the dropdown list.", "warnings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            loadRecord();
            loadCharttopselling();
        }
        public void loadCharttopselling()
        {
            SqlDataAdapter da=new SqlDataAdapter();
            cn.Open();

            if (cboTopelect.Text == "SORT BY QTY")
            {
                 da= new SqlDataAdapter("select top 10 pcode, isnull(sum(qty),0) as qty from vwSoldItems where sdate between '" + dateTimePicker1.Value.ToShortDateString() + "'and'" + dateTimePicker2.Value.ToShortDateString() + "'and status like 'Sold' group by pcode order by qty desc", cn);


            }
            else if (cboTopelect.Text == "SORT BY TOTAL AMOUNT")
            {
               da = new SqlDataAdapter ("select top 10 pcode,  isnull(sum(total),0) as total from vwSoldItems where sdate between '" + dateTimePicker1.Value.ToShortDateString() + "'and'" + dateTimePicker2.Value.ToShortDateString() + "'and status like 'Sold' group by pcode order by total desc", cn);

            }
            DataSet ds = new DataSet();
            da.Fill(ds, "TOPSELLING");
            chart1.DataSource = ds.Tables["TOPSELLING"];
            Series series = chart1.Series[0];
            series.ChartType = SeriesChartType.Doughnut;
            series.Name = "Top Selling";
            var chart = chart1;
            chart.Series[0].XValueMember = "pcode";
            if (cboTopelect.Text == "SORT BY QTY")
            {
                chart.Series[0].YValueMembers = "qty";
            }
             if (cboTopelect.Text == "SORT BY TOTAL AMOUNT")
            {
                chart.Series[0].YValueMembers = "total";

            }
            chart.Series[0].IsValueShownAsLabel = true;
            if (cboTopelect.Text == "SORT BY TOTAL AMOUNT")
            {

                chart.Series[0].LabelFormat = "{#,##0.00}";
            }
            if (cboTopelect.Text == "SORT BY QTY")
            {
                chart.Series[0].LabelFormat = "{#,##0}";

            }
            cn.Close();

        }

        private void cboTopelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new SqlCommand("select  c.pcode, p.pdesc, c.price,  sum (c.qty) as tot_qty, sum (c.disc) as tot_disc, sum(c.total) as total  from tblCart as c inner join tblProduct as p on c.pcode = p.pcode  where status like 'Sold' and sdate between '" + dateTimePicker4.Value.ToShortDateString() + "'and'" + dateTimePicker3.Value.ToShortDateString() + "' group by c.pcode , p.pdesc, c.price", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView2.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), double.Parse(dr["price"].ToString()).ToString("#,##0.00"), dr["tot_qty"].ToString(), dr["tot_disc"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                }
                dr.Close();
                cn.Close();

                string x;
                cn.Open();
                cm = new SqlCommand("select  isnull (sum(total),0)  from tblCart   where status like 'Sold' and sdate between '" + dateTimePicker4.Value.ToShortDateString() + "'and'" + dateTimePicker3.Value.ToShortDateString() + "'", cn);
                lblTotal.Text = Double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");

                cn.Close();

            }
            catch (Exception ex)

            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmchart f = new frmchart();
            f.lbltitle.Text = "Sold Item [" + dateTimePicker4.Value.ToShortDateString() + " - " + dateTimePicker3.Value.ToShortDateString() + "]";
            f.loadchartsold("select  p.pdesc, sum(c.total) as total  from tblCart as c inner join tblProduct as p on c.pcode = p.pcode  where status like 'Sold' and sdate between '" + dateTimePicker4.Value.ToShortDateString() + "'and'" + dateTimePicker3.Value.ToShortDateString() + "' group by  p.pdesc order by total desc");
            
            f.ShowDialog();

        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //loadstockinhistory
            frminventoryreport frm = new frminventoryreport();
            string param = "Date Covered :" + dateTimePicker7.Value.ToShortDateString() + " - " + dateTimePicker8.Value.ToShortDateString();
            frm.loadstockinreport("Select * from vwstockin   where cast(sdate as date) between '" + dateTimePicker7.Value.ToShortDateString() + "' and '" + dateTimePicker8.Value.ToShortDateString() + "' and status like 'Done'",param);
            frm.ShowDialog();

        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadstockinhistory();

        }

        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CancelledOrders();
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frminventoryreport f = new frminventoryreport();
            string param ="Date Covered : " +dateTimePicker5.Value.ToShortDateString() + " - " + dateTimePicker6.Value.ToShortDateString();
            f.loadcancelledorder("select * from vwcancelledorder where sdate between '" + dateTimePicker5.Value.ToShortDateString() + "'and'" + dateTimePicker6.Value.ToShortDateString() + "'", param);
            f.ShowDialog();
        }
    }
}
