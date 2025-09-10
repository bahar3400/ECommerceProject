using ECommerceProject.Core.Models.Enums;
using System;

namespace ECommerceProject.Core.Base
{
    public abstract class UserBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Year { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreateTime { get; set; }
        public Role Role { get; set; }

        public static int AttempCount = 0;

        protected UserBase(DateTime createTime)
        {
            CreateTime = createTime;
        }

    }
}
