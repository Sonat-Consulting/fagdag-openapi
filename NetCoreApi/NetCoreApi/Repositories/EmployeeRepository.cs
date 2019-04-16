using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApi.Model;

namespace NetCoreApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task<List<Employee>> GetEmployees()
        {
            using (var db = new EmployeeContext())
            {
                var employee = await db.Employee.FindAsync(1);
                return new List<Employee> {employee};
            }
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            using (var db = new EmployeeContext())
            {
                var entityEntry = await db.Employee.AddAsync(employee);
                return entityEntry.Entity;
            }
        }

        public async Task<Employee> GetEmployee(int id)
        {
            using (var db = new EmployeeContext())
            {
                return await db.Employee.FindAsync(id);
            }
        }
    }
}