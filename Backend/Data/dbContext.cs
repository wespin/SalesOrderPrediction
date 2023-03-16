using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Data
{
    public class dbContext
    {
        string conn = "Data Source=(localdb)\\mssqllocaldb; Database=StoreSample;Trusted_Connection=True;MultipleActiveResultSets=True";
        public List<Order> GetOrder()
        {
            List<Order> list = new List<Order>();

            string query = "Select * from [Sales].[Orders]";

            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {


                        list.Add(new Order
                        {
                            //OrderId = Convert.ToInt32(dr[0]),
                            //Requireddate = Convert.ToString(dr[4]),
                            //Shippeddate = dr["Shippeddate"] == DBNull.Value ? string.Empty : Convert.ToString(dr[5]),
                            //Shipaddress = Convert.ToString(dr[9]),
                            //Shipcity = Convert.ToString(dr[10])
                        });
                    }
                }
            }
            return list;
        }
    }
}
