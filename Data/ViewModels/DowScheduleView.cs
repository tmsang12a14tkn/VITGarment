using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class DowScheduleView
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<GoalDetailTemplate> GoalDetailTemplates { get; set; }
    }
}
