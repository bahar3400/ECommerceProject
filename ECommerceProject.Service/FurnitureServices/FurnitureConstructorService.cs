using ECommerceProject.Core.Models.Enums;
using ECommerceProject.Core.Models.Products;
using System;
using System.Collections.Generic;

namespace ECommerceProject.Service.FurnutireServices
{
    public sealed partial class FurnitureService
    {
        public List<Furniture> Furnitures { get; set; }
        public FurnitureService()
        {
            Furnitures = new List<Furniture>();

            Furnitures.Add(new Furniture(DateTime.Now)
            {
                Id = 1,
                Name = "Oyun Masası",
                Color = "Siyah",
                Dimension = 140,
                FurnitureBrand = FurnitureBrand.Hepsiburada,
                Model = "GMR-XPRO",
                Price = 1000,
                Surface = "Ahşap",
                WarrantyPeriod = 2,
                Weight = 5,
                Category = ProductCategory.Furniture
            });
            Furnitures.Add(new Furniture(DateTime.Now)
            {
                Id = 2,
                Name = "Çalışma Masası",
                Color = "Beyaz",
                Dimension = 120,
                FurnitureBrand = FurnitureBrand.Ikea,
                Model = "CLSM-2023",
                Price = 900,
                Surface = "MDF",
                WarrantyPeriod = 2,
                Weight = 8,
                Category = ProductCategory.Furniture
            });

            Furnitures.Add(new Furniture(DateTime.Now)
            {
                Id = 3,
                Name = "Toplantı Masası",
                Color = "Ceviz",
                Dimension = 200,
                FurnitureBrand = FurnitureBrand.Trendyol,
                Model = "TPLNT-XL",
                Price = 2500,
                Surface = "Ahşap",
                WarrantyPeriod = 3,
                Weight = 18,
                Category = ProductCategory.Furniture
            });

            Furnitures.Add(new Furniture(DateTime.Now)
            {
                Id = 4,
                Name = "Yazı Masası",
                Color = "Siyah",
                Dimension = 100,
                FurnitureBrand = FurnitureBrand.Trendyol,
                Model = "YZMS-100",
                Price = 750,
                Surface = "Lake",
                WarrantyPeriod = 1,
                Weight = 6,
                Category = ProductCategory.Furniture
            });

            Furnitures.Add(new Furniture(DateTime.Now)
            {
                Id = 5,
                Name = "Bilgisayar Masası",
                Color = "Gri",
                Dimension = 140,
                FurnitureBrand = FurnitureBrand.Koçtaş,
                Model = "BGSRMS-KMP",
                Price = 1100,
                Surface = "Metal+Ahşap",
                WarrantyPeriod = 2,
                Weight = 9,
                Category = ProductCategory.Furniture
            });
        }
    }
}
