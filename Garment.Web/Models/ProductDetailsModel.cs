using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garment.Web.Models
{
    public class ProductInfo
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Lot { get; set; }

        public string Gender { get; set; } //0: Male - 1: Female
        public int Quantity { get; set; }
        public int ProduceQuantity { get; set; }
        public int Duration { get; set; }
        public string ProductType { get; set; }
    }
    public class ProductGoalView
    {
        public string TeamName { get; set; }
        public int Quantity { get; set; }
        public int ProduceQuantity { get; set; }
    }
    public class ProductDateView
    {
        public DateTime Date { get; set; }
        public List<ProductGoalView> ProductGoalViews { get; set; }
        public int Quantity { get; set; }
        public int ProduceQuantity { get; set; }
    }
    public class ProductDetailsModel
    {
        public ProductInfo ProductInfo { get; set; }
        public List<ProductDateView> ProductDateViews { get; set; }
    }
}