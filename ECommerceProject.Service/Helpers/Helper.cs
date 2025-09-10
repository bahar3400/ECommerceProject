using ECommerceProject.Core.Models.Enums;
using System;
using System.Linq;

namespace ECommerceProject.Service.Helpers
{
    public static class Helper
    {
        #region StaticMetodlar

        //İsmin her bir karakterin a-z arasında olup olmadığı kontrol edilir ve boşluğa bakılır
        public static bool IsValidName(string name)
        {
            return name.All(n => char.IsLetter(n) || n == ' ');
        }

        //Telefon numarasın 11 karakter olup olmadığı ve sayı giriş mi o kontrol edilir
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 11 && phoneNumber.All(char.IsDigit);
        }

        //şifrenin boşluğu var ise true döner tam tersin yaparak false yaptık
        public static bool IsValidPassword(string password)
        {

            return !password.Any(char.IsWhiteSpace);
        }

        /// Time Kullanıcın seçtiği katagoriye göre min değer dönecek 
        public static TimeSpan CalculateDeliveryTime(ProductCategory category)
        {
            switch (category)
            {
                case ProductCategory.Electronic:
                    return TimeSpan.FromDays(10);
                case ProductCategory.Furniture:
                    return TimeSpan.FromDays(15);
                default:
                    return TimeSpan.FromDays(5);
            }
        }
        #endregion
    }
}
