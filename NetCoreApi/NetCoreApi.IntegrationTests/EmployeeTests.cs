using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreApi.BusinessLogic;
using NetCoreApi.Configuration;
using NetCoreApi.Controllers;
using NetCoreApi.Exceptions;
using NetCoreApi.Model;
using NetCoreApi.Repositories;
using NetCoreApi.Repositories.Db;
using NetCoreApi.Views;
using Newtonsoft.Json;
using NUnit.Framework;

namespace NetCoreApi.IntegrationTests
{
    [TestFixture]
    public class EmployeeTests
    {
        private EmployeeController _employeeController; 
        private ServiceProvider _serviceProvider;
        
        [OneTimeSetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json", false, true).Build();
            var databaseConfiguration = new DatabaseConfiguration();
            configuration.GetSection("Database").Bind(databaseConfiguration);
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton(databaseConfiguration);
            serviceCollection.AddSingleton<IEmployeeLogic, EmployeeLogic>();
            serviceCollection.AddSingleton<IEmployeeRepository, EmployeeRepository>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var employeeLogic = _serviceProvider.GetService<IEmployeeLogic>();
            _employeeController = new EmployeeController(employeeLogic)
            {
                ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()}
            };
        }

        [Test]
        public void AddEmployeeTest()
        {
            var addEmployee = (CreatedResult) _employeeController.AddEmployee(new NewEmployeeDto {Firstname = $"stig {DateTime.Now.Second}", Surname = $"{DateTime.Now.Minute}Hausberg"});
            var addEmployeeValue = (Employee)addEmployee.Value;
            Assert.IsTrue(addEmployeeValue.Id != 0);
            Console.Write(JsonConvert.SerializeObject(addEmployee));
        }
        
        [Test]
        public void UpdateEmployeeTest()
        {
            var employee = (OkObjectResult) _employeeController.GetEmployee(1);
            var employeeValue = (Employee)employee.Value;
            
            _employeeController.UpdateEmployee(new EmployeeDto {Id = 1, Firstname = $"stigen {DateTime.Now.Millisecond}", Surname = $"Hausberg"});
            
            var employee2 = (OkObjectResult) _employeeController.GetEmployee(1);
            var employeeValue2 = (Employee)employee2.Value;
            
            Assert.AreNotSame(employeeValue.Firstname, employeeValue2.Firstname);
            Console.Write(JsonConvert.SerializeObject(employee));
        }
        
        [Test]
        public void DeleteEmployeeTest()
        {
            var addEmployee = (CreatedResult) _employeeController.AddEmployee(new NewEmployeeDto {Firstname = $"stig {DateTime.Now.Second}", Surname = $"{DateTime.Now.Minute}Hausberg"});
            var addEmployeeValue = (Employee)addEmployee.Value;
            Assert.IsTrue(addEmployeeValue.Id != 0);
            
            var employee = (OkObjectResult) _employeeController.GetEmployee(addEmployeeValue.Id);
            Assert.IsNotNull(employee);
            var employeeValue = (Employee)employee.Value;
            
            _employeeController.DeleteEmployee(addEmployeeValue.Id);
            Assert.Throws<NotFoundException>(() => _employeeController.GetEmployee(addEmployeeValue.Id));
            
        }
        
        [Test]
        public void GetEmployeeTest()
        {
            var employee = (OkObjectResult) _employeeController.GetEmployee(1);
            var employeeValue = (Employee)employee.Value;
            Assert.IsTrue(employeeValue.Id != 0);
            Console.Write(JsonConvert.SerializeObject(employee));
        }
        
        [Test]
        public void GetEmployeesTest()
        {
            var employee = (OkObjectResult) _employeeController.GetEmployees();
            var employeeValue = (List<Employee>)employee.Value;
            Assert.IsTrue(employeeValue.Count != 0);
            Console.Write(JsonConvert.SerializeObject(employee));
        }
    }
}