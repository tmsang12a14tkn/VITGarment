using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garment.Web.Helpers
{
    public static class Extensions
    {
        public static Tuple<DateTime, DateTime> SetLastQuarterDates(int quarter, int year)
        {
            int startMonth = 0;
            int startDay = 1;
            int endMonth = 0;
            int endDay = 31;

            switch (quarter)
            {
                case 1:
                    startMonth = 1;
                    endMonth = 3;
                    endDay = 31;
                    break;

                case 2:
                    startMonth = 4;
                    endMonth = 6;
                    endDay = 30;
                    break;

                case 3:
                    startMonth = 7;
                    endMonth = 9;
                    endDay = 30;
                    break;

                case 4:
                    startMonth = 10;
                    endMonth = 12;
                    break;
            }

            DateTime startDate = new DateTime(year, startMonth, startDay);
            DateTime endDate = new DateTime(year, endMonth, endDay);
            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }
    }
}