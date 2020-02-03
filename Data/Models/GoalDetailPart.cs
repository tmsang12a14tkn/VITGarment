using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class GoalDetailPart
    {
        public int Id { get; set; }
        [ForeignKey("GoalDetail")]
        public int GoalDetailId { get; set; }
        public TimeSpan StartCountTime { get; set; }
        public TimeSpan EndCountTime { get; set; }
        public int Quantity { get; set; }
        public int ProduceQuantity { get; set; }
        public int ManualProduceQuantity { get; set; }
        public virtual GoalDetail GoalDetail { get; set; }

        public virtual ICollection<ProduceHistory> ProduceHistories { get; set; }
    }
}
