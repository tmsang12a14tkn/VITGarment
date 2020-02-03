using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class GoalDetailTemplate
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [Display(Name = "Thời gian bắt đầu")]
        public TimeSpan StartCountTime { get; set; }
        [Display(Name = "Thời gian kết thúc")]
        public TimeSpan EndCountTime { get; set; }
        [Display(Name = "Phiên")]
        public int SessionId { get; set; }
        [Display(Name = "Số phút")]
        public int Minute { get; set; }
    }
}
