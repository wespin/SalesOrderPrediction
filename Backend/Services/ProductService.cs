using WebApi.Models;
using WebApi.Services.Interfaces;
using WebApi.UnitOfWork.Interfaces;

namespace WebApi.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> GetAll()
        {
            using (var context = _unitOfWork.Create())
            {
                var records = context.Repositories.ProductRepository.GetAll();

                return records;
            }

        }

        public Product Get(int id)
        {
            using (var context = _unitOfWork.Create())
            {
                var result = context.Repositories.ProductRepository.Get(id);

                return result;
            }
        }
    }
}
