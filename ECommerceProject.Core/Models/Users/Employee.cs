using ECommerceProject.Core.Base;
using ECommerceProject.Core.Interfaces;
using ECommerceProject.Core.Models.Enums;
using System;

namespace ECommerceProject.Core.Models.Users
{
    public class Employee : UserBase, IUserLogin
    {
        #region Metodlar
        public Employee(DateTime createTime) : base(createTime)
        {

        }

        //Kullanıcadan alın role göre karşına  işlemler çıkacak
        public Role GetRole()
        {
            return Role.Employee;
        }

        //İsim ve şifre kontrol edilerek sayfaya giriş yapması istenecek şuan sadece liste üzerinde kontrol yapılacak veri tabanı olmadığı için
        public bool UserLogin(string userName, string password)
        {
            return UserName == userName && Password == password;
        }
        #endregion

        public int Seniority { get; set; }

    }
}
