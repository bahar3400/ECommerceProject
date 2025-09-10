using ECommerceProject.Core.Models.Enums;
using ECommerceProject.Core.Models.Products;
using System;
using System.Collections.Generic;
namespace ECommerceProject.Service.ElectronicServices
{
    public sealed partial class ElectronicService
    {
        public List<Electronic> Electronics { get; set; }

        public ElectronicService()
        {
            Electronics = new List<Electronic>();

            Electronics.Add(new Electronic(DateTime.Now)
            {
                Id = 1,
                Name = "Bilgisayar",
                Color = "Beyaz",
                Dimension = 30.48,
                WarrantyPeriod = 2,
                Price = 1000,
                Model = "CN.EVG870",
                Category = ProductCategory.Electronic,
                Memory = 16,
                Processor = "i5",
                ProcessorSpeed = "65hz",
                GraphicsCard = "Var",
                ElectronicBrand = ElectronicBrand.Mac

            });
            Electronics.Add(new Electronic(DateTime.Now)
            {
                Id = 2,
                Name = "Tablet",
                Color = "Siyah",
                Dimension = 25.4,
                WarrantyPeriod = 1,
                Price = 700,
                Model = "TB.XYZ123",
                Category = ProductCategory.Electronic,
                Memory = 8,
                Processor = "Snapdragon 720G",
                ProcessorSpeed = "2.3GHz",
                GraphicsCard = "Entegre",
                ElectronicBrand = ElectronicBrand.Samsung
            });

            Electronics.Add(new Electronic(DateTime.Now)
            {
                Id = 3,
                Name = "Masaüstü Bilgisayar",
                Color = "Gri",
                Dimension = 45.7,
                WarrantyPeriod = 3,
                Price = 1500,
                Model = "DT.PQR456",
                Category = ProductCategory.Electronic,
                Memory = 32,
                Processor = "i7",
                ProcessorSpeed = "3.6GHz",
                GraphicsCard = "NVIDIA GTX 1660",
                ElectronicBrand = ElectronicBrand.Huawei
            });

            Electronics.Add(new Electronic(DateTime.Now)
            {
                Id = 4,
                Name = "Oyun Laptopu",
                Color = "Kırmızı",
                Dimension = 35.6,
                WarrantyPeriod = 2,
                Price = 2500,
                Model = "GL.789ABC",
                Category = ProductCategory.Electronic,
                Memory = 16,
                Processor = "i9",
                ProcessorSpeed = "4.0GHz",
                GraphicsCard = "NVIDIA RTX 3060",
                ElectronicBrand = ElectronicBrand.Lenovo
            });

            Electronics.Add(new Electronic(DateTime.Now)
            {
                Id = 5,
                Name = "Ultrabook",
                Color = "Gümüş",
                Dimension = 29.2,
                WarrantyPeriod = 2,
                Price = 1800,
                Model = "UB.DEF321",
                Category = ProductCategory.Electronic,
                Memory = 16,
                Processor = "i7",
                ProcessorSpeed = "3.8GHz",
                GraphicsCard = "Entegre",
                ElectronicBrand = ElectronicBrand.Lenovo
            });


        }


    }
}