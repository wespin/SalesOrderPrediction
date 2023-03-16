using WebApi.UnitOfWork.Interfaces;
using Microsoft.Extensions.Configuration;

namespace WebApi.UnitOfWork
{
    public class UnitOfWorkSqlServer : IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public UnitOfWorkSqlServer(IConfiguration configuration = null)
        {
            _configuration = configuration;
        }

        public IUnitOfWorkAdapter Create()
        {
            //var connectionString = _configuration == null
            //    ? Parameters.ConnectionString
            //    : _configuration.GetValue<string>("SqlConnectionString");

            var connectionString = _configuration.GetValue<string>("SqlConnectionString");

            return new UnitOfWorkSqlServerAdapter(connectionString);
        }
    }
}