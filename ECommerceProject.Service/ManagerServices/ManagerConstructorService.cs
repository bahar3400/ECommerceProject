using ECommerceProject.Core.Models.Enums;
using ECommerceProject.Core.Models.Users;
using System;
using System.Collections.Generic;

namespace ECommerceProject.Service.ManagerServices
{
    public partial class ManagerService
    {
        private List<Manager> Managers { get; set; }

        public ManagerService()
        {
            Managers = new List<Manager>();
            Managers.Add(new Manager(DateTime.Now)
            {
                UserName = "Admin",
                Password = "admin.123",
                Role = Role.Manager,
                IsRoot = true
            });
            Managers.Add(new Manager(DateTime.Now)
            {
                Id = 1,
                UserName = "Bahar",
                Password = "Bahar.34",
                PhoneNumber = "05423696418",
                Year = 23,
                Salary = 10000,
                Role = Role.Manager,


            });
            Managers.Add(new Manager(DateTime.Now)
            {
                Id = 2,
                UserName = "Ahmet",
                Password = "Ahmet.45",
                PhoneNumber = "05321234567",
                Year = 35,
                Salary = 12000,
                Role = Role.Manager,
            });

            Managers.Add(new Manager(DateTime.Now)
            {
                Id = 3,
                UserName = "Elif",
                Password = "Elif.89",
                PhoneNumber = "05559876543",
                Year = 29,
                Salary = 11000,
                Role = Role.Manager,
            });
        }
    }
}

