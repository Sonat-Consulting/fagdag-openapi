using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApi.Model;
using NetCoreApi.Views;

namespace NetCoreApi.BusinessLogic
{
    public interface IEmployeeLogic
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> AddEmployee(CreateEmployeeDto createEmployee);
        Task<Employee> GetEmployee(int id);
    }
}