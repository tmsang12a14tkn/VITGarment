using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    class QCModel
    {
    }
    public class QCTeamModel
    {
        public int TeamId { get; set; }
        public int QCId { get; set; }
        [Display(Name = "Từ ngày")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }
        [Display(Name = "Đến ngày")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? To { get; set; }

        public bool ApplyAll { get; set; } //áp dụng cho tất cả các tổ trong xí nghiệp
    }

    public class QCReportFactoryDateView //danh sách các QCReport của xí nghiệp trong 1 ngày
    {
        public DateTime? SelectDate { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        
    }

    public class TeamQCReportView
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public List<SessionQCReportView> SessionQCReports { get; set; }
    }

    public class SessionQCReportView
    {
        public int SessionOrder { get; set; }
        public List<ProductQCReportView> ProductQCReports { get; set; }
    }

    public class ProductQCReportView
    {
        public string ProductId { get; set; }
        public List<QCReport> QCReports { get; set; }
    }
}
