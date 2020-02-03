using Data.DataAccessLayer;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Garment.Web.Controllers.api
{
    //public class GoalDetailTemplateView
    //{
    //    public TimeSpan
    //}
    public class GoalDetailTemplatesController : ApiController
    {
        private GarmentContext db = new GarmentContext();
        [HttpGet]
        public List<GoalDetailModel> FromDayOfWeek(DayOfWeek dow = DayOfWeek.Monday)
        {
            return db.GoalDetailTemplates.Where(t => t.DayOfWeek == dow).OrderBy(t => t.StartCountTime).Select(
                    t => new GoalDetailModel { 
                        StartCountTime = t.StartCountTime,
                        EndCountTime = t.EndCountTime,
                        //SessionId = t.SessionId,
                    }
                ).ToList();
        }
    }
}
