using AutoMapper;
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
   
    public class ReasonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ErrorCode { get; set; }
        public string ColorCode { get; set; }
    }

    public class ReasonsController : ApiController
    {
        GarmentContext db = new GarmentContext();
        private MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<Reason, ReasonModel>();
        });
        [HttpGet]
        public List<ReasonModel> GetAll()
        {
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<ReasonModel>>(db.Reasons);
        }
    }
    
}
