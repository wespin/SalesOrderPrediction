using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int id);
    }
}
