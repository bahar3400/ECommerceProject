using ECommerceProject.Core.Models.Users;
using System.Collections.Generic;

namespace ECommerceProject.Service.EmployeeServices
{
    public interface IEmployeeService
    {
        bool Add(Employee employee);
        bool Update(int id, Employee employee);
        List<Employee> GetAll();
    }
}
