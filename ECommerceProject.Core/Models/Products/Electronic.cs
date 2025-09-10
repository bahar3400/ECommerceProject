using ECommerceProject.Core.Bases;
using ECommerceProject.Core.Models.Enums;
using System;

namespace ECommerceProject.Core.Models.Products
{
    public class Electronic : ProductBase
    {
        #region Constructor
        public Electronic(DateTime createTime) : base(createTime)
        {
        }
        #endregion

        public int Memory { get; set; }
        public string Processor { get; set; }
        public string ProcessorSpeed { get; set; }
        public string GraphicsCard { get; set; }
        public ElectronicBrand ElectronicBrand { get; set; }

    }
}
