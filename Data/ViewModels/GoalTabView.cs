using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class GoalFilterView
    {
        public DateTime? SelectDate { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public List<GoalTabFilterView> GoalTabFilterViews { get; set; }
        
        

    }

    public class GoalTabFilterView
    {
        public Factory Factory { get; set; }
        public DateTime GoalDate { get; set; }
        public List<Goal> Goals { get; set; }
    }

    public class GoalDetailFilterView
    {
        public int Quantity { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductId { get; set; }



    }
}
