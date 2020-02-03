using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class TeamModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Xí nghiệp")]
        public int FactoryId { get; set; }

        [Required]
        [Display(Name = "Tên đội nhóm")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Số công nhân")]
        public int NoEmployee { get; set; }

        [Required]
        [Display(Name = "Số thứ tự")]
        public int Order { get; set; }
    }
}
