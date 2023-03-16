using System.Xml.Linq;
using WebApi.Models;
using WebApi.Services.Interfaces;
using WebApi.UnitOfWork.Interfaces;

namespace WebApi.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Order> GetAll()
        {
            using (var context = _unitOfWork.Create())
            {
                var records = context.Repositories.OrderRepository.GetAll();

                return records;
            }

        }

        public Order Get(int id)
        {
            using (var context = _unitOfWork.Create())
            {
                var result = context.Repositories.OrderRepository.Get(id);
                //result.Client = context.Repositories.ClientRepository.Get(result.ClientId);
                //result.Detail = context.Repositories.InvoiceDetailRepository.GetAllByInvoiceId(result.Id);

                //foreach (var item in result.Detail)
                //{
                //    item.Product = context.Repositories.ProductRepository.Get(item.ProductId);
                //}

                return result;
            }
        }

        public IEnumerable<Order> GetByCustid(int custid)
        {
            using (var context = _unitOfWork.Create())
            {
                var records = context.Repositories.OrderRepository.GetByCustId(custid);

                return records;
            }

        }

        public IEnumerable<OrderPrediction> GetPrediction()
        {
            using (var context = _unitOfWork.Create())
            {
                var records = context.Repositories.OrderRepository.GetPrediction();

                return records;
            }
        }

        public void Create(Order model)
        {

            using (var context = _unitOfWork.Create())
            {
                 context.Repositories.OrderRepository.Create(model);
                 context.SaveChanges();
            }
        }
    }
}
