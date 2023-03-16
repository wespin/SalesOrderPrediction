using WebApi.Repository.Interfaces;

namespace WebApi.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRepository
    {
       IOrderRepository OrderRepository { get; }
       IEmployeeRepository EmployeeRepository { get; }
       IShipperRepository ShipperRepository { get; }
       IProductRepository ProductRepository { get; }
    }
}
