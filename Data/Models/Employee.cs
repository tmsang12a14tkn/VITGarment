using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<ProduceHistory> ProduceHistories { get; set; }
    }
}
