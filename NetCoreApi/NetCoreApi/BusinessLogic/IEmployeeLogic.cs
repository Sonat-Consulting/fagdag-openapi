using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreApi.Model;

namespace NetCoreApi.BusinessLogic
{
    public interface IEmployeeLogic
    {
        Task<List<Employee>> GetEmployees();
    }
}