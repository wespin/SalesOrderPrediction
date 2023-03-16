using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
    }
}
