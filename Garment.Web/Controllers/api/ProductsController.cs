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
    
    public class ProductModel
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Lot { get; set; }
    }
    
    public class ProductsController : ApiController
    {
        GarmentContext db = new GarmentContext();
        private MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<Product, ProductModel>();
        });
        [HttpGet]
        public List<ProductModel> GetAll()
        {
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<ProductModel>>(db.Products);
        }
    }
}
