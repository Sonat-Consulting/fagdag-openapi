using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Model;

namespace NetCoreApi.Repositories
{
    public class EmployeeRepository 
    {
        public async  Task<List<Employee>> GetEmployees()
        {
            using (var db = new EmployeeContext())
            {
                var allAsync = await db.Employee.FindAsync(1);
                return new List<Employee>{allAsync};
            } 
        }
    }
}