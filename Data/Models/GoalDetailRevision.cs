using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class GoalDetailRevision
    {
        [Key]
        public int Id { get; set; }
        public int GoalDetailId { get; set; }
        public int TotalMinutes { get; set; }
        public int Quantity { get; set; }
        public int ProduceQuantity { get; set; }
        public int NoEmployee { get; set; }
        public int? ReasonId { get; set; }
        public string Comment {get;set;}
        [Index(IsUnique = false)]
        [StringLength(20)]
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int NoError { get; set; }

        public virtual GoalDetail GoalDetail { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Reason Reason { get; set; }
    }
}
