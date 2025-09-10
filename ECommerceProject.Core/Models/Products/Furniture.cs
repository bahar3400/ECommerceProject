using ECommerceProject.Core.Bases;
using ECommerceProject.Core.Models.Enums;
using System;

namespace ECommerceProject.Core.Models.Products
{
    public class Furniture : ProductBase
    {
        #region Constructor
        public Furniture(DateTime createTime) : base(createTime)
        {
        }
        #endregion
        public string Surface { get; set; }
        public double Weight { get; set; }
        public FurnitureBrand FurnitureBrand { get; set; }

    }
}
