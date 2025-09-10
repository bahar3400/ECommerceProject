using ECommerceProject.Service.Interfaces.Products;
using ECommerceProject.Core.Bases;
using ECommerceProject.Core.Models.Products;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceProject.Service.FurnutireServices
{
    public partial class FurnitureService : IProductService, IFurnitureService
    {
        /// <summary>
        /// Buradaki metodlar ekleme, silme , güncellemei, listeleme, ıd bulma işlevini görecektir.
        /// </summary>
        #region KendiInterfaceMetodları
        public bool Add(Furniture furniture)
        {
            Furnitures.Add(furniture); return true;

        }

        public List<Furniture> GetAll()
        {
            return Furnitures;
        }

        public bool Update(int id, Furniture furniture)
        {
            Furniture electronic = Furnitures.FirstOrDefault(model => model.Id == id);
            if (electronic != null)
            {
                electronic.Name = furniture.Name;
                electronic.FurnitureBrand = furniture.FurnitureBrand;
                electronic.WarrantyPeriod = furniture.WarrantyPeriod;
                electronic.Price = furniture.Price;
                electronic.Category = furniture.Category;
                electronic.Color = furniture.Color;
                electronic.Weight = furniture.Weight;
                electronic.Dimension = furniture.Dimension;

                return true;
            }
            return false;
        }
        #endregion

        #region OrtakMetodlar
        public bool Delete(int id)
        {
            Furniture electronic = Furnitures.FirstOrDefault(model => model.Id == id);
            if (electronic != null)
            {
                Furnitures.Remove(electronic);
                return true;
            }
            return false;
        }
        // Listemi ters çevir ve ilk elamanı al (1,2,3,4,4) orderby ile (5,4,3,2,1) ve ilk elaman olan 5 alır artır
        public int GetNextId(ProductBase productBase)
        {
            var idFind = Furnitures.OrderByDescending(model => model.Id).First();
            return idFind.Id + 1;
        }
        #endregion

    }
}
