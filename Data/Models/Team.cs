using System.Collections.Generic;

namespace Data.Models
{
    public class Team
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int NoEmployee { get; set;}
        public int FactoryId { get; set; }
        public int Order { get; set; }
        public int VisibleStatus { get; set; } //0: default (hide if no data), 1: always show, 2: always hide
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
        public virtual Factory Factory { get; set; }

        public virtual ICollection<QCTeam> QCTeams { get; set; }
    }
}
