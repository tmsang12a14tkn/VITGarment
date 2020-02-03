using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Factory
    {
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Mô tả tóm tắt")]
        public string ShortDescription { get; set; }
        [Display(Name = "Mô tả chi tiết")]
        public string FullDescription { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Picture { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
