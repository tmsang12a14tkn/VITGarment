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
    public class FixDataController : ApiController
    {
        GarmentContext db = new GarmentContext();

        //[HttpGet]
        //public bool FixGoalDetail()
        //{
        //    foreach (var gd in db.GoalDetails)
        //    {
        //        if (gd.ProductDetails.Count == 0)
        //            gd.ProductDetails.Add(new ProductDetail
        //            {
        //                Quantity = gd.Quantity,
        //                ReasonId = gd.ReasonId,
        //                Comment = gd.Comment,
        //                ProduceQuantity = gd.ProduceQuantity,
        //                ProductId = gd.ProductId
        //            });
        //    }
        //    db.SaveChanges();
        //    return true;
        //}
        [HttpGet]
        public bool FixProductDetailReason()
        {
            foreach (var pd in db.ProductDetails)
            {
                if(pd.ReasonId.HasValue && pd.Reasons.Count == 0)
                {
                    pd.Reasons.Add(db.Reasons.Find(pd.ReasonId));
                }
            }
            db.SaveChanges();
            return true;
        }
    }
}
