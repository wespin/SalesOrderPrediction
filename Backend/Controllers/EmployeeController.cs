using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            try
            {                
                return Ok(_employeeService.GetAll());

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }



        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Employee>> Get(int id)
        {
            var employee = _employeeService.Get(id);

            if (employee.Empid == 0)
            {
                return NotFound();
            }

            return Ok(employee);
        }


    }
}
