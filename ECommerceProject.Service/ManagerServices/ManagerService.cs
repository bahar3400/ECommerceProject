using ECommerceProject.Service.Interfaces.UserInterfaces;
using ECommerceProject.Core.Base;
using ECommerceProject.Core.Models.Users;
using System.Collections.Generic;
using System.Linq;
using ECommerceProject.Service.UserBaseServices;
using System.Runtime.Remoting.Messaging;
namespace ECommerceProject.Service.ManagerServices
{

    public sealed partial class ManagerService : UserBaseService, IUserInterface, IManagerService
    {
        #region KendiInterfaceMetodları
        public bool Add(Manager manager)
        {
            Managers.Add(manager); return true;
        }
        public List<Manager> GetAll()
        {
            return Managers;
        }
        public bool Update(int id, Manager manager)
        {
            var control = Managers.First(model => model.Id == id);

            if (control != null)
            {
                control.UserName = manager.UserName;
                control.Password = manager.Password;
                control.PhoneNumber = manager.PhoneNumber;
                return true;
            }
            return false;
        }
        #endregion
        #region OrtakMetodlar
        public bool Delete(int id)
        {
            Manager electronic = Managers.First(model => model.Id == id);
            if (electronic != null)
            {
                Managers.Remove(electronic);
                return true;
            }
            return false;
        }
        public int GetNextId(UserBase userBase)
        {
            if (Managers.Count == 0)
                return 1;
            var idFind = Managers.OrderByDescending(model => model.Id).First();
            return idFind.Id + 1;
        }

        public override decimal CalculateBonus(decimal salary)
        {
            return salary + salary * 0.10m;
        }

        public override decimal CalculateSalary(decimal salary)
        {
            return salary + salary * 0.10m;
        }

        #endregion

    }
}