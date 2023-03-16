using System.Data.SqlClient;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repository.Interfaces;

namespace WebApi.Repository
{
    public class ShipperRepository : Repository, IShipperRepository
    {
        public ShipperRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public IEnumerable<Shipper> GetAll()
        {
            var result = new List<Shipper>();

            var command = CreateCommand("SELECT [shipperid], [companyname] from [Sales].[Shippers] WITH(NOLOCK)");

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Shipper
                    {
                        ShipperId = Convert.ToInt32(reader["shipperid"]),
                        CompanyName = DataHelper.SafeGet<string>(reader, "companyname") 
                    });
                }
            }

            return result;
        }

        public Shipper Get(int id)
        {

            var employee = new Shipper();
            var command = CreateCommand("SELECT [shipperid], [companyname] from [Sales].[Shippers] WITH(NOLOCK) WHERE shipperid = @id");
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    employee = new Shipper
                    {
                        ShipperId = Convert.ToInt32(reader["shipperid"]),
                        CompanyName = DataHelper.SafeGet<string>(reader, "companyname")
                    };
                }
            }

            return employee;
        }
    }
}
