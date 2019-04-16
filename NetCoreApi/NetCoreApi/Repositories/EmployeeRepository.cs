using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Model;
using NetCoreApi.Repositories.Db;

namespace NetCoreApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        public List<Employee> GetEmployees()
        {
            List<Employee> findAll;
            using (var db = new LiteDatabase(@"c:\temp\MyData.db"))
            {
                findAll = db.GetCollection<Employee>("employees").FindAll().ToList();    
            }
            
            return findAll;
        }

        public Employee AddEmployee(Employee employee)
        {
            using (var db = new LiteDatabase(@"c:\temp\MyData.db"))
            {
                //Id blir auto incrementert
                db.GetCollection<Employee>("employees").Insert(employee);
            }

            return employee;
        }

        public Employee GetEmployee(int id)
        {
            Employee employee;
            using (var db = new LiteDatabase(@"c:\temp\MyData.db"))
            {
                employee = db.GetCollection<Employee>("employees").Find(x => x.Id == id).SingleOrDefault();
            }

            return employee;
        }
    }
}