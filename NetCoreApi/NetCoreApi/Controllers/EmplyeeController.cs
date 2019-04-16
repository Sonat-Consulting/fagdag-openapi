using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreApi.BusinessLogic;

namespace NetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/employee")]
    [ApiController]
    public class EmplyeeController:ControllerBase
    {
        private readonly EmployeeLogic _employeeLogic;

        public EmplyeeController(EmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeLogic.GetEmployees();
            return Ok(employees);
        }
    }
}