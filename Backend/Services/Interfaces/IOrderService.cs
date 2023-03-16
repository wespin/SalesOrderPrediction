using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IOrderService
    {
        Order Get(int id);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetByCustid(int id);
        IEnumerable<OrderPrediction> GetPrediction();
        void Create(Order model);
    }
}
