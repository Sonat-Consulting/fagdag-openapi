using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApi.Model;
using NetCoreApi.Views;

namespace NetCoreApi.BusinessLogic
{
    public interface IEmployeeLogic
    {
        Employee GetEmployee(int id);
        
        List<Employee> GetEmployees();
        Employee AddEmployee(NewEmployeeDto newEmployee);

        void UpdateEmployee(EmployeeDto employeeDto);
        void DeleteEmployee(int id);
    }
}