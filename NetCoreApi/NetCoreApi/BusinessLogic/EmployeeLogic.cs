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

        public Employee GetEmployee(int id)
        {
            return _employeeRepository.GetEmployee(id);
        }

        public List<Employee> GetEmployees()
        {
            return  _employeeRepository.GetEmployees();
        }

        public Employee AddEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var employee = new Employee
            {
                Firstname = createEmployeeDto.Firstname,
                Surname = createEmployeeDto.Surname
            };

            return _employeeRepository.AddEmployee(employee);
        }
    }
}