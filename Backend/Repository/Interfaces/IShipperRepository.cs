using WebApi.Models;
using WebApi.Repository.Interfaces.Actions;

namespace WebApi.Repository.Interfaces
{
    public interface IShipperRepository : IReadRepository<Shipper, int>
    {
    }
}
