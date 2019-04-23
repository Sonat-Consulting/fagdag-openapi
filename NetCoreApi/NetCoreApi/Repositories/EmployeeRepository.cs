using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Configuration;
using NetCoreApi.Exceptions;
using NetCoreApi.Model;
using NetCoreApi.Repositories.Db;

namespace NetCoreApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public EmployeeRepository(DatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        
        public List<Employee> GetEmployees()
        {
            List<Employee> findAll;
            using (var db = new LiteDatabase(_databaseConfiguration.ConnectionString))
            {
                findAll = db.GetCollection<Employee>("employees").FindAll().ToList();    
            }
            
            return findAll;
        }

        public Employee AddEmployee(Employee employee)
        {
            using (var db = new LiteDatabase(_databaseConfiguration.ConnectionString))
            {
                //Id blir auto incrementert
                db.GetCollection<Employee>("employees").Insert(employee);
            }

            return employee;
        }

        public Employee GetEmployee(int id)
        {
            Employee employee;
            
            using (var db = new LiteDatabase(_databaseConfiguration.ConnectionString))
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
            using (var db = new LiteDatabase(_databaseConfiguration.ConnectionString))
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
            using (var db = new LiteDatabase(_databaseConfiguration.ConnectionString))
            {
                db.GetCollection<Employee>("employees").Delete(id);
            }
        }
    }
}