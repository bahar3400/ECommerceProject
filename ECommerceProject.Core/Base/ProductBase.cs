using ECommerceProject.Core.Models.Enums;
using System;

namespace ECommerceProject.Core.Bases
{
    public  class ProductBase
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Color { get; set; }
        public double Dimension { get; set; }
        public int WarrantyPeriod { get; set; }
        public decimal Price { get; set; }
        public string Model { get; set; }
        public ProductCategory Category { get; set; }

        public DateTime CreateTime { get; set; }

        protected  ProductBase(DateTime createTime)
        {
            CreateTime = createTime;
        }


    }
}
