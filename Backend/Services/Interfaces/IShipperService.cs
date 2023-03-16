using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IShipperService
    {
        IEnumerable<Shipper> GetAll();
        Shipper Get(int id);
    }
}
