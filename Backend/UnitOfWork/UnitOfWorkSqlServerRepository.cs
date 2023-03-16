using System.Data.SqlClient;
using WebApi.Repository;
using WebApi.Repository.Interfaces;
using WebApi.UnitOfWork.Interfaces;

namespace WebApi.UnitOfWork
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {
        public IOrderRepository OrderRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IShipperRepository ShipperRepository { get; }
        public IProductRepository ProductRepository { get; }

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            OrderRepository = new OrderRepository(context, transaction);
            EmployeeRepository = new EmployeeRepository(context, transaction);
            ShipperRepository = new ShipperRepository(context, transaction);
            ProductRepository = new ProductRepository(context, transaction);
        }
    }
}
