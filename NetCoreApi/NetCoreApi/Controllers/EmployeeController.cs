using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.BusinessLogic;
using NetCoreApi.Views;

namespace NetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeLogic _employeeLogic;

        public EmployeeController(IEmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] NewEmployeeDto newEmployeeDto)
        {
            var employeeDto = _employeeLogic.AddEmployee(newEmployeeDto);
            return Created($"v1/employees/{employeeDto.Id}", employeeDto);
        }
        
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            _employeeLogic.UpdateEmployee(employeeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteEmployee(int id)
        {
            _employeeLogic.DeleteEmployee(id);
            return NoContent();
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
    }
}