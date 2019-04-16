using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.BusinessLogic;
using NetCoreApi.Views;

namespace NetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeLogic _employeeLogic;

        public EmployeeController(IEmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _employeeLogic.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employees = _employeeLogic.GetEmployee(id);
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var employeeDto = _employeeLogic.AddEmployee(createEmployeeDto);
            return Created($"v1/employees/{employeeDto.Id}", employeeDto);
        }
    }
}