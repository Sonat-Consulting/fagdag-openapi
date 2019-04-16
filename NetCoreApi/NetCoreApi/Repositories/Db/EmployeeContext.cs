using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Model;

namespace NetCoreApi.Repositories
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=employee.db");
        }

        
    }
}