using WebApi.Models;
using WebApi.Repository.Interfaces.Actions;

namespace WebApi.Repository.Interfaces
{
    public interface IEmployeeRepository: IReadRepository<Employee, int>
    {
    }
}
