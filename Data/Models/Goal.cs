using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Data.Models
{
    public class Goal
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime GoalDate { get; set; }
        [NotMapped]
        public int Quantity { get { return GoalDetails.Sum(gd => gd.ProductDetails.Sum(pd=>pd.Quantity)); } }
        [NotMapped]
        public int ProduceQuantity { get { return GoalDetails.Sum(gd => gd.ProductDetails.Sum(pd=>pd.ProduceQuantity.HasValue? pd.ProduceQuantity.Value:0)); } }
        //[DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        //public TimeSpan StartHour { get; set; }
        //[DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        //public TimeSpan EndHour { get; set; }
        //[NotMapped]
        //public double TotalHour { get { return (EndHour - StartHour).TotalHours; }}
        //[NotMapped]
        //public double QuantityPerHour { get { return Quantity / TotalHour; } }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? LastestUpdateTime { get; set; }
        public string CreateUserId { get; set; }
        public int NoEmployee { get; set; } //so cong nhan di lam - deprecated
        public int TotalEmployee { get; set; } //tong so cong nhan - deprecated
        public int AbsentWithoutReason { get; set; } //so cong nhan vang ko co ly do - deprecated
        public string AbsentComment { get; set; }  //- deprecated
        //[ForeignKey("Product")]
        //public int ProductId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ApplicationUser CreateUser { get; set; }
        //public virtual Product Product { get; set; }
        public virtual ICollection<GoalDetail> GoalDetails { get; set; }
        public virtual ICollection<LatestSummary> LatestSummaries { get; set; }

    }
}
