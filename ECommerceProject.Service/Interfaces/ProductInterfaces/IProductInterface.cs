using ECommerceProject.Core.Bases;
namespace ECommerceProject.Service.Interfaces.Products
{
    
    public interface IProductService
    {
        bool Delete(int id);
        int GetNextId(ProductBase productBase);
    }
}
