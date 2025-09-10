using ECommerceProject.Core.Models.Enums;
using ECommerceProject.Core.Models.Users;
using System;
using System.Collections.Generic;

namespace ECommerceProject.Service.EmployeeServices
{
    public partial class EmployeeService
    {
        public  List<Employee>  Employees { get; set; }

        public EmployeeService()
        {
            Employees = new List<Employee>();

            Employees.Add(new Employee(DateTime.Now)
            {
                Id=1,
                UserName = "Nur",
                Password = "Nur.34",
                Year = 19,
                PhoneNumber = "0545248545",
                Salary=1000,
                Seniority=5,
                Role=Role.Employee
            });
            Employees.Add(new Employee(DateTime.Now)
            {
                Id = 2,
                UserName = "Mehmet",
                Password = "Mehmet.45",
                Year = 25,
                PhoneNumber = "05326547890",
                Salary = 1200,
                Seniority = 3,
                Role = Role.Employee
            });

            Employees.Add(new Employee(DateTime.Now)
            {
                Id = 3,
                UserName = "Ayşe",
                Password = "Ayse.22",
                Year = 22,
                PhoneNumber = "05554447788",
                Salary = 1500,
                Seniority = 4,
                Role = Role.Employee
            });

            Employees.Add(new Employee(DateTime.Now)
            {
                Id = 4,
                UserName = "Burak",
                Password = "Burak.99",
                Year = 30,
                PhoneNumber = "05412345678",
                Salary = 1800,
                Seniority = 6,
                Role = Role.Employee
            });

            Employees.Add(new Employee(DateTime.Now)
            {
                Id = 5,
                UserName = "Zeynep",
                Password = "Zeynep.77",
                Year = 28,
                PhoneNumber = "05071234567",
                Salary = 1600,
                Seniority = 5,
                Role = Role.Employee
            });

        }

    }
}
