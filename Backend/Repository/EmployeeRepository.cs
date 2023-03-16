using System.Data.SqlClient;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repository.Interfaces;

namespace WebApi.Repository
{
    public class EmployeeRepository: Repository, IEmployeeRepository
    {

        public EmployeeRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public IEnumerable<Employee> GetAll()
        {
            var result = new List<Employee>();

            var command = CreateCommand("SELECT [empid], [firstname], [lastname] from [HR].[Employees] WITH(NOLOCK)");

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Employee
                    {
                        Empid = Convert.ToInt32(reader["empid"]),
                        FullName = DataHelper.SafeGet<string>(reader, "firstname") + ' ' + DataHelper.SafeGet<string>(reader, "lastname"),
                    });
                }
            }

            return result;
        }

        public Employee Get(int id)
        {

            var employee = new Employee();
            var command = CreateCommand("SELECT [empid], [firstname], [lastname] from [HR].[Employees] WITH(NOLOCK) WHERE empid = @id");
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    employee = new Employee
                    {
                        Empid = Convert.ToInt32(reader["empid"]),
                        FullName = DataHelper.SafeGet<string>(reader, "firstname") + ' ' + DataHelper.SafeGet<string>(reader, "lastname")
                    };
                }
            }

            return employee;
        }
    }
}
