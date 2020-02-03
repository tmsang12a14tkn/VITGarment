using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class TeamSession
    {
        [Key, Column(Order = 1)]
        public int TeamId { get; set; }
        [Key, Column(Order = 2)]
        public DateTime Date { get; set; }
        [Key, Column(Order = 3)]
        public int Order { get; set; }
       
        public TimeSpan Start { get; set; }
        public int StartDay { get; set; }
        public TimeSpan End { get; set; }
        public int EndDay { get; set; }

        public bool NotWork { get; set; }

        public int NoEmployee { get; set; } //so cong nhan di lam
        public int TotalEmployee { get; set; } //tong so cong nhan
        public int AbsentWithoutReason { get; set; } //so cong nhan vang ko co ly do
        public string AbsentComment { get; set; }

        public virtual Team Team { get; set; }
    }
}
