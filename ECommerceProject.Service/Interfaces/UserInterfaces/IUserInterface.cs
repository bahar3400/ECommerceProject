using ECommerceProject.Core.Base;

namespace ECommerceProject.Service.Interfaces.UserInterfaces
{
    public interface IUserInterface
    {
        bool Delete(int id);
        int GetNextId(UserBase userBase);
    }
}
