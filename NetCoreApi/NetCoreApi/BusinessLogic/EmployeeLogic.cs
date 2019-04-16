using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApi.Model;
using NetCoreApi.Repositories;
using NetCoreApi.Views;

namespace NetCoreApi.BusinessLogic
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeLogic(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetEmployees();
        }

        public async Task<Employee> AddEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var employee = new Employee
            {
                Firstname = createEmployeeDto.Firstname,
                Surname = createEmployeeDto.Surname
            };

            return await _employeeRepository.AddEmployee(employee);
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _employeeRepository.GetEmployee(id);
        }
    }
}