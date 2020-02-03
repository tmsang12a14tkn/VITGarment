using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class ProduceHistory
    {
        public int Id { get; set; }
        [ForeignKey("GoalDetail")]
        public int GoalDetailId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public int Quantity { get; set; }
        public virtual GoalDetail GoalDetail { get; set; }
        public virtual Employee Employee { get; set; }
    }
}