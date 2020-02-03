using AutoMapper;
using Data.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Garment.Web.Controllers.api
{
    public class TeamModel
    {
        public int Id { get; set; }
        public int NoEmployee { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public DateTime date { get; set; }
    }
    public class TeamStatus
    {
        public int Id;
        public int Status;
    }
    public class TeamsController : ApiController
    {
        private GarmentContext db = new GarmentContext();
        private MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Data.Models.Team, TeamModel>()
                .ForMember(x => x.Selected, opt => opt.UseValue(true));
        });

        [HttpGet]
        public List<TeamModel> GetAllByFactoryId(int factoryId)
        {
            var teams = db.Teams.Where(t => t.FactoryId == factoryId).ToList();
            IMapper mapper = config.CreateMapper();
            var teamsModel = mapper.Map<List<Data.Models.Team>, List<TeamModel>>(teams);
            return teamsModel;
        }
         [HttpGet]
        public TeamModel GetById(int teamId)
        {
            var team = db.Teams.Find(teamId);
            IMapper mapper = config.CreateMapper();
            var teamModel = mapper.Map<Data.Models.Team, TeamModel>(team);
            return teamModel;
        }
        [HttpPost]
        [Authorize]
        public int ChangeVisibleStatus([FromBody] TeamStatus teamStatus)
        {
            var team = db.Teams.Find(teamStatus.Id);
            team.VisibleStatus = teamStatus.Status;
            db.SaveChanges();
            return team.VisibleStatus;
        }
    }
}
