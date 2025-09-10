using ECommerceProject.Core.Models.Users;
using System.Collections.Generic;

namespace ECommerceProject.Service.ManagerServices
{
    public interface IManagerService
    {
        bool Add(Manager manager);
        bool Update(int id, Manager manager);
        List<Manager> GetAll();

    }
}
