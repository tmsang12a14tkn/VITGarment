using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garment.Web.Helpers
{
    public static class DateTimeHelper
    {
        public static string DayOfWeekVN(this DateTime date)
        {
            switch (date.DayOfWeek)            
            {
                case DayOfWeek.Sunday: return "Chủ Nhật";
                case DayOfWeek.Monday: return "Thứ Hai";
                case DayOfWeek.Tuesday: return "Thứ Ba";
                case DayOfWeek.Wednesday: return "Thứ Tư";
                case DayOfWeek.Thursday: return "Thứ Năm";
                case DayOfWeek.Friday: return "Thứ Sáu";
                default: return "Thứ Bảy";
            }
        }
    }
}