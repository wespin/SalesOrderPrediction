using WebApi.Models;
using WebApi.Repository.Interfaces.Actions;

namespace WebApi.Repository.Interfaces
{
    public interface IOrderRepository : IReadRepository<Order, int>, ICreateRepository<Order>
    {
        IEnumerable<Order> GetByCustId(int id);
        IEnumerable<OrderPrediction> GetPrediction();
    }
}