using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Nazam_pos
{

    
   public class DBconnection
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        
        SqlDataReader dr;
        private double dailysales;
        private string con;
        private int productline;
        private int stockonhand;
        private int criticalitem;
        

        public string Myconnection()
        {
           //string  con= @"Data Source = DESKTOP - O9L6C35; Initial Catalog = Inventory 1; Integrated Security = True";
            con= "Data Source=DESKTOP-O9L6C35;Initial Catalog=Inventory 1;Integrated Security=True";

            //con = @"Data Source=DESKTOP-T4AVUIO;Initial Catalog=Inventory 1;Integrated Security=True ";
            return con;
        }

        public double DailySales()

        {
            string sdate = DateTime.Now.ToShortDateString();
            cn = new SqlConnection();
            cn.ConnectionString = con;

            cn.Open();

            cm = new SqlCommand("select isnull(sum(total),0) as total from tblcart where sdate between '" + sdate + "'and'" + sdate + "'and status like 'Sold'", cn);
            dailysales = double.Parse(cm.ExecuteScalar().ToString());

            cn.Close();

            return dailysales;
        }


        public double ProductLine()

        {
           
            cn = new SqlConnection();
            cn.ConnectionString = con;

            cn.Open();

            cm = new SqlCommand("select count (*) from tblProducts", cn);
            productline = int.Parse(cm.ExecuteScalar().ToString());

            cn.Close();

            return productline;

            
        }

        public double StockOnHand()

        {

            cn = new SqlConnection();
            cn.ConnectionString = con;

            cn.Open();

            cm = new SqlCommand("select isnull (sum(qty),0) as qty from tblProducts", cn);
            stockonhand = int.Parse(cm.ExecuteScalar().ToString());

            cn.Close();

            return stockonhand;
        }

        public double CriticalItem()

        {

            cn = new SqlConnection();
            cn.ConnectionString = con;

            cn.Open();

            cm = new SqlCommand("select count(*)  from vwCriticalItems", cn);
            criticalitem = int.Parse(cm.ExecuteScalar().ToString());

            cn.Close();

            return criticalitem;
        }




        public double GetVal()
        {
            double vat = 0;
            cn.ConnectionString = Myconnection();
            cn.Open();
            cm = new SqlCommand(" select * from tblVat ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                vat = Double.Parse(dr["vat"].ToString());

            }
            dr.Close();
            cn.Close();
            return vat;
        }

        public string GetPassword(string user)
        {
            string password="";
            cn.ConnectionString = Myconnection();
            cn.Open();
            cm = new SqlCommand(" select * from tblUser where username =@username", cn);
            cm.Parameters.AddWithValue("@username", user);
            dr = cm.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                password = dr["password"].ToString();
            }
            
            dr.Close();
            cn.Close();

            return password;
        }

    }
}
