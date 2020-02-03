using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Data.Models
{
    public class QC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Supplier { get; set; }
        public bool Visible { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<QCTeam> QCTeams { get; set; }
    }

    public class QCTeam
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("QC")]
        public int QCId { get; set; }
        
        public DateTime From { get; set; }
        public DateTime? To { get; set; }

        public virtual QC QC { get; set; }
        public virtual Team Team { get; set; }
        
    }
    public class QCReport
    {
        [Key, Column(Order = 1)]
        public int TeamId { get; set; }
        [Key, Column(Order = 2)]
        public DateTime Date { get; set; }
        [Key, Column(Order = 3)]
        public int SessionOrder { get; set; }

        [Key, Column(Order = 4)]
        public string ProductId { get; set; }

        [Key, Column(Order = 5)]
        [ForeignKey("QC")]
        public int QCId { get; set; }

        public string ManagementCode { get; set; } //Mã quản lý

        //public string ProductCode { get; set; } //Mã hàng
        public string Color { get; set; } //Màu


        /// <summary>
        /// SL Kiểm
        /// </summary>
        public int ProductCount { get; set; } //SL Kiểm
        /// <summary>
        /// SL SP Lỗi
        /// </summary>
        public int FailProductCount { get; set; }
        /// <summary>
        /// SL SP Đạt
        /// </summary>
        public int GoodProductCount { get; set; }
        /// <summary>
        /// SL Lỗi
        /// </summary>
        public int FailCheckCount { get; set; } 

        public bool AccordingDocument { get; set;}
        public bool AccordingColorTable { get; set; }
        public bool AccordingComment { get; set; }

        public virtual QC QC { get; set; }
        public virtual Team Team { get; set; }

        public virtual ICollection<QCError> Errors { get; set; }
        public virtual ICollection<QCSpecification> Specifications { get; set; }

        [NotMapped]
        public bool IsEmpty { get; set; }

        [NotMapped]
        public bool InputWrong { get {
                if (Errors != null)
                {
                    return Errors.Sum(e => e.FailCheckCount) != FailCheckCount || Errors.Sum(e => e.FailProductCount) != FailProductCount;
                }
                return false;
            } }
    }

    public class QCError
    {
        public int Id { get; set; }
        public string ErrorPosition { get; set; }
        public int FailCheckCount { get; set; } //Số lỗi
        public int FailProductCount { get; set; } //Số SP Lỗi
        public int FixedProductCount { get; set; } //Số SP sửa
        public string Reason { get; set; } //Nguyên nhân
        public string FixMethod { get; set; } //Phương pháp khắc phục

        [NotMapped]
        public bool IsDeleted { get; set; }

    }
    public class QCSpecification
    {
        public int Id { get; set; }
        public string Size { get; set; }
       
        [NotMapped]
        public bool IsDeleted { get; set; }
        public virtual ICollection<QCSpecDetail> QCSpecDetails { get; set; }
    }

    public class QCSpecDetail
    {
        public int Id { get; set; }
        public int QCSpecificationId { get; set; }
        public string Element { get; set; }
        public string Specification { get; set; }
        public string Tolerance { get; set; }
        public float Parameter1 { get; set; }
        public float Parameter2 { get; set; }
        public float Parameter3 { get; set; }
        public float Parameter4 { get; set; }
        public float Parameter5 { get; set; }
        public string Parameter6 { get; set; }
        public string Note { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; }

        [ForeignKey("QCSpecificationId")]
        public virtual QCSpecification QCSpecification { get; set; }
    }
}
