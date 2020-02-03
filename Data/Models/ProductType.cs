using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        [Display(Name = "Tên loại hàng")]
        public string Name { get; set; }
    }
}
