using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class FactorySession
    {
        [Key, Column(Order = 1)]
        public int FactoryId { get; set; }
        [Key, Column(Order = 2)]
        public DateTime Date { get; set; }
        [Key, Column(Order = 3)]
        public int Order { get; set; }
        public int Type { get; set; } //All (0) or Each Team (1)
        public TimeSpan Start { get; set; }
        public int StartDay { get; set; } //0: hom nay - 1: ngay mai
        public TimeSpan End { get; set; }
        public int EndDay { get; set; } //0: hom nay - 1: ngay mai

        public virtual Factory Factory { get; set; }
    }
}
