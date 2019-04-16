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
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeLogic.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployees(int id)
        {
            var employees = await _employeeLogic.GetEmployee(id);
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var employeeDto = await _employeeLogic.AddEmployee(createEmployeeDto);
            return Created($"v1/employees/{employeeDto.Id}", employeeDto);
        }
    }
}