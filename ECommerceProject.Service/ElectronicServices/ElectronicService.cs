using ECommerceProject.Core.Bases;
using ECommerceProject.Core.Models.Products;
using ECommerceProject.Service.Interfaces.Products;
using System.Collections.Generic;
using System.Linq;
namespace ECommerceProject.Service.ElectronicServices
{
    public partial class ElectronicService : IProductService, IElectronicService
    {
        /// <summary>
        /// Ekleme metodu ile kullanıcın aldığı bilgiler listelere ekler
        /// Silme metodu ile alınan ıd ile silme işlemi yapılacak 
        /// Update metodu ile alınban id ürün bulunup seçilen işleme göre bilgiler güncellenecek
        /// Listeleme metoduna göre ise ürünlerin listelenmesi istenecek
        /// Listemi ters çevir ve ilk elamanı al (1,2,3,4,4) orderby ile (5,4,3,2,1) ve ilk elaman olan 5 alır artır
        /// </summary>
        #region OrtakMetodlar
        public bool Delete(int id)
        {
            Electronic electronic = Electronics.FirstOrDefault(model => model.Id == id);
            if (electronic != null)
            {
                Electronics.Remove(electronic);
                return true;
            }
            return false;
        }
        public int GetNextId(ProductBase productBase)
        {
            var idFind = Electronics.OrderByDescending(model => model.Id).First();
            return idFind.Id + 1;
        }

        #endregion

        #region KendiInterfaceMetodları

        public bool Add(Electronic electronic)
        {
            Electronics.Add(electronic); return true;

        }
        public bool Update(int id, Electronic electronic)
        {
            Electronic result = Electronics.FirstOrDefault(model => model.Id == id);
            if ( result != null)
            {
                result.Color = electronic.Color;
                result.ElectronicBrand = electronic.ElectronicBrand;
                result.Category = electronic.Category;
                result.Price = electronic.Price;
                result.GraphicsCard = electronic.GraphicsCard;
                result.WarrantyPeriod = electronic.WarrantyPeriod;
                result.Dimension = electronic.Dimension;

                return true;
            }
            return false;
        }
        public List<Electronic> GetAll()
        {
            return Electronics;
        }

        #endregion
    }
}