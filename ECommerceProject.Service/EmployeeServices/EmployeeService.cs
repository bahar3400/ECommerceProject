using ECommerceProject.Core.Base;
using ECommerceProject.Core.Models.Users;
using ECommerceProject.Service.Interfaces.UserInterfaces;
using ECommerceProject.Service.UserBaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ECommerceProject.Service.EmployeeServices
{
    public sealed partial class EmployeeService : UserBaseService, IUserInterface, IEmployeeService
    {
        #region KeniInterfaceMetodları
        public bool Add(Employee employee)
        {
            Employees.Add(employee); return true;
        }
        public bool Update(int id, Employee employee)
        {
            var control = Employees.First(model => model.Id == id);
            if (control != null)
            {
                control.UserName = employee.UserName;
                control.Password = employee.Password;
                control.PhoneNumber = employee.PhoneNumber;
                control.Salary = employee.Salary;
                return true;
            }

            return false;
        }
        public List<Employee> GetAll()
        {
            return Employees;
        }
        #endregion


        #region OrtakMetodlar
        public override decimal CalculateBonus(decimal salary)
        {
            return salary + salary * 0.5m;
        }

        public override decimal CalculateSalary(decimal salary)
        {
            return salary + salary * 0.2m;
        }

        public bool Delete(int id)
        {
            try
            {
                Employee electronic = Employees.FirstOrDefault(model => model.Id==id);

                if (electronic != null)
                {
                    Employees.Remove(electronic);
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Delete işleminde hata alındı" + ex.Message);
            }
            return false;
        }


        public int GetNextId(UserBase userBase)
        {
            if (Employees.Count == 0)
                return 1;

            var idFind = Employees.OrderByDescending(model => model.Id).First();
            return idFind.Id + 1;

        }
        #endregion
    }
}
