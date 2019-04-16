using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApi.Model;

namespace NetCoreApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> GetEmployee(int id);
    }
}