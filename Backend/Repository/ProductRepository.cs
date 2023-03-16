using System.Data.SqlClient;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repository.Interfaces;

namespace WebApi.Repository
{
    public class ProductRepository: Repository, IProductRepository
    {
        public ProductRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public IEnumerable<Product> GetAll()
        {
            var result = new List<Product>();

            var command = CreateCommand("SELECT [productid], [productname] from [Production].[Products] WITH(NOLOCK)");

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Product
                    {
                        Productid = Convert.ToInt32(reader["productid"]),
                        Productname = DataHelper.SafeGet<string>(reader, "productname")
                    });
                }
            }

            return result;
        }

        public Product Get(int id)
        {

            var employee = new Product();
            var command = CreateCommand("SELECT [productid], [productname] from [Production].[Products] WITH(NOLOCK) WHERE productid = @id");
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    employee = new Product
                    {
                        Productid = Convert.ToInt32(reader["productid"]),
                        Productname = DataHelper.SafeGet<string>(reader, "productname")
                    };
                }
            }

            return employee;
        }
    }
}
