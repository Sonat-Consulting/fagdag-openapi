using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Exceptions;
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
                if (employee == null)
                {
                    throw new NotFoundException($"Employee with id {id} does not exist");
                }
            }

            return employee;
        }

        public void UpdateEmployee(Employee employee)
        {
            using (var db = new LiteDatabase(@"c:\temp\MyData.db"))
            {
                var employees = db.GetCollection<Employee>("employees");
                var entity = employees.Find(x => x.Id == employee.Id).SingleOrDefault();
                if (entity == null)
                {
                    throw new NotFoundException($"Employee with id {employee.Id} is not found");
                }
                entity.Firstname = employee.Firstname;
                entity.Surname = employee.Surname;
                employees.Update(entity);
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var db = new LiteDatabase(@"c:\temp\MyData.db"))
            {
                db.GetCollection<Employee>("employees").Delete(id);
            }
        }
    }
}