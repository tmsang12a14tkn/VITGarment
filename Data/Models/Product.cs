using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã hàng")]
        public string ProductId { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Lô hàng")]
        public string Lot { get; set; }
        [Display(Name = "Giới tính")]
        public int Gender { get; set; } //0: Male - 1: Female
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        public int Duration { get; set; }
        [Display(Name = "Loại hàng")]
        public int? ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }
        //public virtual ICollection<GoalDetail> Goals { get; set; }
    }
}
