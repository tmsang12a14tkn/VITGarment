using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Models
{
    class TimeSpanConverter : IsoDateTimeConverter
    {
        public TimeSpanConverter()
        {
            base.DateTimeFormat = "hh'/'mm";
        }
    }
}