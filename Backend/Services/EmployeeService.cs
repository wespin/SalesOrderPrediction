using WebApi.Models;
using WebApi.Services.Interfaces;
using WebApi.UnitOfWork.Interfaces;

namespace WebApi.Services
{
    public class EmployeeService: IEmployeeService
    {
        private IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Employee> GetAll()
        {
            using (var context = _unitOfWork.Create())
            {
                var records = context.Repositories.EmployeeRepository.GetAll();

                return records;
            }

        }

        public Employee Get(int id)
        {
            using (var context = _unitOfWork.Create())
            {
                var result = context.Repositories.EmployeeRepository.Get(id);

                return result;
            }
        }
    }
}
