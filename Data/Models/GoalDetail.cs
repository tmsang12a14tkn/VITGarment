using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Data.Models
{
    public class GoalDetail
    {
        public int Id { get; set; }
        [ForeignKey("Goal")]
        public int GoalId { get; set; }
        public TimeSpan StartCountTime { get; set; }
        public int StartCountDay { get; set; } //0: hom nay - 1: ngay mai
        public TimeSpan EndCountTime { get; set; }
        public int EndCountDay { get; set; } //0: hom nay - 1: ngay mai
        public int TotalMinutes { get; set; }
        //public int Quantity { get; set; } //ko su dung - deprecated
        //public int ProduceQuantity { get; set; } //ko su dung - deprecated
        //public int ManualProduceQuantity { get; set; } //ko su dung - deprecated
        public int TotalEmployee { get; set; } //tong so cong nhan 
        public int NoEmployee { get; set; } //so cong nhan co mat
        //[ForeignKey("Reason")]
        //public int? ReasonId { get; set; } //ko su dung - deprecated
        //public string Comment {get;set; } //ko su dung - deprecated
        //public string ProductId { get; set; } //ko su dung - deprecated
        public int SessionOrder { get; set; } // ca lam viec
        
        //public int NoError { get; set; } //ko su dung - deprecated

        public bool Hide { get; set; }

        //public virtual Product Product { get; set; }
        //public virtual Reason Reason { get; set; }
        public virtual Goal Goal { get; set; }
        public virtual ICollection<ProduceHistory> ProduceHistories { get; set; }
        public virtual ICollection<GoalDetailRevision> Revisions { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        //0: khong co du lieu, 1: co, 2: thieu
        public bool IsNoData()
        {
            return ProductDetails.All(pd=>pd.Quantity == default(int) && pd.ProduceQuantity == default(int?) && pd.ProductId == default(string) && pd.ReasonId == default(int?) && pd.Comment == default(string));
        }

        public int IsChangeProQuantity() 
        {
            return ProductDetails.All(gd => gd.ProduceQuantity.HasValue) ? 1 : ProductDetails.All(gd => !gd.ProduceQuantity.HasValue) ? 0 : 2;
        }
    }

    public class ProductDetail
    {
        public int Id { get; set; }
        [ForeignKey("GoalDetail")]
        public int GoalDetailId { get; set; }
        public string ProductId { get; set; }
        public int TotalMinutes { get; set; }
        public int Quantity { get; set; }
        public int? ProduceQuantity { get; set; }
        //[ForeignKey("Reason")]
        public int? ReasonId { get; set; } //deprecated
        public string Comment { get; set; }

        //public virtual Reason Reason { get; set; } //deprecated
        public virtual ICollection<Reason> Reasons { get; set; }
        public virtual GoalDetail GoalDetail { get; set; }
    }
}
