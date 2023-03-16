using System.Data.SqlClient;
using System.Net;
using WebApi.Models;
using WebApi.Repository.Interfaces;
using WebApi.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

namespace WebApi.Repository
{
    public class OrderRepository: Repository, IOrderRepository
    {
        public OrderRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public IEnumerable<OrderPrediction> GetPrediction()
        {
            var result = new List<OrderPrediction>();

            var query = "sp_Sales_Order_Prediction";
            var command = CreateCommand(query);

            command.CommandType = CommandType.StoredProcedure;



            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new OrderPrediction
                    {
                        CustId= DataHelper.SafeGet<int>(reader, "custid"),
                        CustomerName = DataHelper.SafeGet<string>(reader, "CustomerName"),
                        LastOrderDate = DataHelper.SafeGet<DateTime>(reader, "LastOrderDate"),
                        NextPredictedOrder = DataHelper.SafeGet<DateTime>(reader, "NextPredictedOrder"),
                    });
                }
            }

            return result;
        }

        public IEnumerable<Order> GetAll()
        {
            var result = new List<Order>();

            var command = CreateCommand("SELECT * from [Sales].[Orders] WITH(NOLOCK) ");

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Order
                    {
                        OrderId = Convert.ToInt32(reader["OrderId"]),
                        Requireddate = DataHelper.SafeGet<DateTime>(reader, "Requireddate"),
                        Shippeddate = DataHelper.SafeGet<DateTime>(reader, "Shippeddate"),
                        Shipaddress = DataHelper.SafeGet<string>(reader, "Shipaddress"),
                        Shipcity = DataHelper.SafeGet<string>(reader, "Shipcity")
                    });
                }
            }

            return result;
        }

        public Order Get(int id)
        {

            var order = new Order();
            var command = CreateCommand("Select * from [Sales].[Orders] WITH(NOLOCK) WHERE orderid = @id");
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    order = new Order
                    {
                        OrderId = Convert.ToInt32(reader["OrderId"]),
                        Requireddate = DataHelper.SafeGet<DateTime>(reader, "Requireddate"),
                        Shippeddate = DataHelper.SafeGet<DateTime>(reader, "Shippeddate"),
                        Shipaddress = DataHelper.SafeGet<string>(reader, "Shipaddress"),
                        Shipcity = DataHelper.SafeGet<string>(reader, "Shipcity")
                    };
                }
            }

            return order;
        }

        public IEnumerable<Order> GetByCustId(int id) {

            var result = new List<Order>();

            var command = CreateCommand("Select * from [Sales].[Orders] WITH(NOLOCK) WHERE custid = @id");
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Order
                    {
                        OrderId = Convert.ToInt32(reader["OrderId"]),
                        Requireddate = DataHelper.SafeGet<DateTime>(reader, "Requireddate"),
                        Shippeddate = DataHelper.SafeGet<DateTime>(reader, "Shippeddate"),
                        Shipaddress = DataHelper.SafeGet<string>(reader, "Shipaddress"),
                        Shipcity = DataHelper.SafeGet<string>(reader, "Shipcity")
                    });
                }
            }
            return result;
        }


        public void Create(Order model)
        {
            XElement order = new XElement("Order",
            new XElement("Empid", model.Empid),
            new XElement("Shipperid", model.Shipperid),
            new XElement("Shipname", model.Shipname),
            new XElement("Shipaddress", model.Shipaddress),
            new XElement("Shipcity", model.Shipcity),
            new XElement("Orderdate", model.Orderdate.ToString("yyyy-MM-dd")),
            new XElement("Requireddate",model.Requireddate.ToString("yyyy-MM-dd")),
            new XElement("Shippeddate", model.Shippeddate.ToString("yyyy-MM-dd")),
            new XElement("Freight", model.Freight),
            new XElement("Shipcountry", model.Shipcountry)
);

            XElement oOrderDetail = new XElement("OrderDetail");
            foreach (var item in model.Detail)
            {
                oOrderDetail.Add(new XElement("Item",
                    new XElement("Productid", item.Productid),
                    new XElement("Unitprice", item.Unitprice),
                    new XElement("Qty", item.Qty),
                    new XElement("Discount", item.Discount)
                    ));
            }

            order.Add(oOrderDetail);
            string SaleOrderXML = order.ToString();

            var query = "sp_Sales_Order_Insert";
            var command = CreateCommand(query);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("SaleOrder_xml", SqlDbType.Xml).Value = SaleOrderXML;
            command.Parameters.Add("OrderId", SqlDbType.Int).Direction = ParameterDirection.Output;

            command.ExecuteScalar();
            string respuesta = command.Parameters["orderid"].Value.ToString();

            model.OrderId = Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
