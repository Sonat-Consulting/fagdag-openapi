using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApi.Model;

namespace NetCoreApi.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        Employee AddEmployee(Employee employee);
        Employee GetEmployee(int id);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}