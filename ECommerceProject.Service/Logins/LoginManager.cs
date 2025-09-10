using ECommerceProject.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceProject.Service.Logins
{
    public class LoginManager
    {
        //readonly, bir alanın (field) sadece tanımlandığı anda veya constructor içinde (yani sınıf ilk oluşturulurken) değer alabileceğini belirtir.
        private readonly List<IUserLogin> _userLogins;
        public LoginManager(List<IUserLogin> users)
        {
            _userLogins = users;
        }

        // Girilen bilgilerle hangi kullanıcı eşleşiyor, onu bul ve geri ver
        public IUserLogin Authenticate(string userName,  string password)
        {

            return _userLogins.FirstOrDefault(model => model.UserLogin(userName, password));
        }

    }
}