using ECommerceProject.Core.Base;
using ECommerceProject.Core.Interfaces;
using ECommerceProject.Core.Models.Enums;
using System;

namespace ECommerceProject.Core.Models.Users
{
    public class Manager : UserBase, IUserLogin
    {
        public bool IsRoot { get; set; }
        public Manager(DateTime createTime) : base(createTime)
        {
        }

        public Role GetRole()
        {
            return Role.Manager; 
        }

        public bool UserLogin(string userName, string password)
        {
            return UserName == userName && Password == password; 
        }
    }
}
