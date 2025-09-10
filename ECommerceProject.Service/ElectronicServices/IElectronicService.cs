using ECommerceProject.Core.Models.Products;
using System.Collections.Generic;

namespace ECommerceProject.Service.ElectronicServices
{
    public interface IElectronicService
    {
        bool Add(Electronic electronic);
        bool Update(int id, Electronic electronic);
        List<Electronic> GetAll();
    }
}

