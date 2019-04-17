using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApi.Exceptions;
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
            ValidateId(id);
            
            return _employeeRepository.GetEmployee(id);
        }

        public List<Employee> GetEmployees()
        {
            return  _employeeRepository.GetEmployees();
        }

        public Employee AddEmployee(NewEmployeeDto newEmployeeDto)
        {
            ValidateNames(newEmployeeDto.Firstname, newEmployeeDto.Surname);
            
            var employee = new Employee
            {
                Firstname = newEmployeeDto.Firstname,
                Surname = newEmployeeDto.Surname
            };

            return _employeeRepository.AddEmployee(employee);
        }

        public void UpdateEmployee(EmployeeDto employeeDto)
        {
            ValidateNames(employeeDto.Firstname, employeeDto.Surname);
            ValidateId(employeeDto.Id);
            
            var employee = new Employee
            {
                Firstname = employeeDto.Firstname,
                Surname = employeeDto.Surname,
                Id = employeeDto.Id
            };

            _employeeRepository.UpdateEmployee(employee);
        }

        public void DeleteEmployee(int id)
        {
            ValidateId(id);
            
            _employeeRepository.DeleteEmployee(id);
        }

        private static void ValidateId(int id)
        {
            if (id == 0)
            {
                throw new BadRequestException($"Invalid id : {id}");
            }
        }

        private static void ValidateNames(string firstname, string lastname)
        {
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname))
            {
                throw new BadRequestException($"Firstname and Surname has to be filled out");
            }
        }
    }
}