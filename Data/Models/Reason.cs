using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Reason
    {
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        public int Order { get; set; }
        public string ColorCode { get; set; }
        public string ErrorCode { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
