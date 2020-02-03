using AutoMapper;
using Data.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Garment.Web.Controllers.api
{
    public class FactoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class FactoriesController : ApiController
    {
        private GarmentContext db = new GarmentContext();
        private MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Data.Models.Factory, FactoryModel>();
        });

        [HttpGet]
        public List<FactoryModel> GetAll()
        {
            var factories = db.Factories.ToList();
            IMapper mapper = config.CreateMapper();
            var factoriesModel = mapper.Map<List<Data.Models.Factory>, List<FactoryModel>>(factories);
            return factoriesModel;
        }
    }
}
