using ECommerceProject.Core.Models.Products;
using System.Collections.Generic;

namespace ECommerceProject.Service.FurnutireServices
{
    public interface IFurnitureService
    {
        bool Add(Furniture furniture);
        bool Update(int id, Furniture furniture);
        List<Furniture> GetAll();
    }
}
