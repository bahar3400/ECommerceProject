using ECommerceProject.Core.Models.Enums;

namespace ECommerceProject.Core.Interfaces
{
    public interface IUserLogin
    {

        bool UserLogin(string userName, string password);
        Role GetRole();
    }
}
