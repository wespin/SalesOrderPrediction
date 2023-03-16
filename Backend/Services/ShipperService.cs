using WebApi.Models;
using WebApi.Services.Interfaces;
using WebApi.UnitOfWork.Interfaces;

namespace WebApi.Services
{
    public class ShipperService : IShipperService
    {
        private IUnitOfWork _unitOfWork;
        public ShipperService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Shipper> GetAll()
        {
            using (var context = _unitOfWork.Create())
            {
                var records = context.Repositories.ShipperRepository.GetAll();

                return records;
            }

        }

        public Shipper Get(int id)
        {
            using (var context = _unitOfWork.Create())
            {
                var result = context.Repositories.ShipperRepository.Get(id);

                return result;
            }
        }
    }
}
