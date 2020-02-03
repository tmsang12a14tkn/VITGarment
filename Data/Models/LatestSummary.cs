using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class LatestSummary
    {
        public int Id { get; set; }
        public DateTime SummaryDate { get; set; }
        public int Quantity { get; set; }
        public int QuantityPerHour { get; set; }
        public int SummaryHour { get; set; }
        [ForeignKey("Goal")]
        public int GoalId { get; set; }
        public virtual Goal Goal { get; set; }
    }
}
