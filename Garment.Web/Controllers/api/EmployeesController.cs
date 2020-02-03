using Data.DataAccessLayer;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
namespace Garment.Web.Controllers.api
{
    public class EmployeeView
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
    //[EnableCors(origins: "http://vitgarment.vitcorp.vn", headers: "*", methods: "*")]
    public class EmployeesController : ApiController
    {
        GarmentContext db = new GarmentContext();
        [HttpGet]
        public List<EmployeeView> FromTeam(int teamId)
        {
            List<EmployeeView> employees = db.Employees.Where(e => e.TeamId == teamId).Select(e=> new EmployeeView
            {
                Id = e.Id, Name = e.Name
            }).ToList();
            return employees;
        }
        [HttpGet]
        public List<EmployeeView> GetAll()
        {
            List<EmployeeView> employees = db.Employees.Select(e => new EmployeeView
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
            return employees;
        }
    }
}
