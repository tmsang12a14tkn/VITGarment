using AutoMapper;
using Data.DataAccessLayer;
using Data.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace Garment.Web.Controllers.api
{

    public class TeamSettingModel
    {
        public int TeamId { get; set; }
        public int DisplayMode { get; set; }
        public int PrimaryScreenId { get; set; }
        public int SecondaryScreenId { get; set; }
        public int SecondaryScreenTime { get; set; }
        //public DateTime SecondaryScreenEndTime { get; set; }


        //Auto Time
        public int FirstScreenTime { get; set; }
        public int SecondScreenTime { get; set; }
        //public int ThirdScreenTime { get; set; }
        public int FourthScreenTime { get; set; }
        public int FifthScreenTime { get; set; }
        public int SixthScreenTime { get; set; }
        public int SeventhScreenTime { get; set; }
        public int EighthScreenTime { get; set; }
        public int NinthScreenTime { get; set; }

        //ScreenMessage
        //public string FirstMessage { get; set; }
        //public string SecondMessage { get; set; }
        public string ThirdMessage { get; set; }
        public string FourthMessage { get; set; }
        public string FifthMessage { get; set; }
        public string SixthMessage { get; set; }
        public string SeventhMessage { get; set; }
        public string EighthMessage { get; set; }
        //public string CustomMessage { get; set; }

        public string ThirdColor { get; set; }
        public string FourthColor { get; set; }
        public string FifthColor { get; set; }
        public string SixthColor { get; set; }
        public string SeventhColor { get; set; }
        public string EighthColor { get; set; }
    }
    public class TeamSettingView
    {
        public int ScreenId { get; set; }
        public int DisplayMode { get; set; }

        public string ThirdMessage { get; set; }
        public string FourthMessage { get; set; }
        public string FifthMessage { get; set; }
        public string SixthMessage { get; set; }
        public string SeventhMessage { get; set; }
        public string EighthMessage { get; set; }

        //Color
        public string ThirdColor { get; set; }
        public string FourthColor { get; set; }
        public string FifthColor { get; set; }
        public string SixthColor { get; set; }
        public string SeventhColor { get; set; }
        public string EighthColor { get; set; }

        //Default
        public string DefaultUpMessage { get; set; }
        public string DefaultDownMessage { get; set; }
    }
    public class TeamSettingsController : ApiController
    {
        GarmentContext db = new GarmentContext();
        private MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<TeamSetting, TeamSettingView>();
            cfg.CreateMap<TeamSetting, TeamSettingModel>();
        });
        [HttpGet]
        public TeamSettingView Get(int teamId)
        {
            var teamSetting = db.TeamSettings.Find(teamId);
            if(teamSetting == null)
            {
                teamSetting = new TeamSetting
                {
                    ThirdMessage = "Cảm ơn các bạn đã cố gắng!!!",
                    SecondaryScreenEndTime = DateTime.Now.Date,
                    PrimaryScreenId = 9,
                    DisplayMode = 0,
                    TeamId = teamId,
                    DefaultDownMessage = "",
                    DefaultUpMessage = ""
                };
                db.TeamSettings.Add(teamSetting);
                db.SaveChanges();
                return new TeamSettingView
                {
                    ScreenId = teamSetting.PrimaryScreenId,
                    ThirdMessage = teamSetting.ThirdMessage
                };
            }
            else
            {
                int screenId = teamSetting.PrimaryScreenId; //default
                if(teamSetting.DisplayMode == 1) //auto
                {
                    var screenTimes = new int[] { teamSetting.FirstScreenTime , teamSetting.SecondScreenTime , teamSetting.FourthScreenTime , teamSetting.FifthScreenTime , teamSetting.SixthScreenTime , teamSetting.SeventhScreenTime , teamSetting.EighthScreenTime, teamSetting.NinthScreenTime };
                    var screens = new int[] { 1, 2,4,5,6,7,8,9 };
                    var totalMins = screenTimes.Sum();
                    var currentMins = (DateTime.Now - teamSetting.AutoScreenStartTime.Value).TotalMinutes % totalMins;
                    var sumMins = 0;
                    for (int i = 0;i< screenTimes.Length; i++)
                    {
                        sumMins += screenTimes[i];
                        if (currentMins <= sumMins)
                        {
                            screenId = screens[i];
                            break;
                        };
                    }
                }
                else if(teamSetting.DisplayMode == 2) //manual
                {
                    screenId = teamSetting.SecondaryScreenEndTime > DateTime.Now ? teamSetting.SecondaryScreenId : teamSetting.PrimaryScreenId;
                }
                return new TeamSettingView
                {
                    ScreenId = screenId,
                    ThirdMessage = teamSetting.ThirdMessage,
                    FourthMessage = teamSetting.FourthMessage,
                    FifthMessage = teamSetting.FifthMessage,
                    SixthMessage = teamSetting.SixthMessage,
                    SeventhMessage = teamSetting.SeventhMessage,
                    EighthMessage = teamSetting.EighthMessage,

                    ThirdColor = teamSetting.ThirdColor,
                    FourthColor = teamSetting.FourthColor,
                    FifthColor = teamSetting.FifthColor,
                    SixthColor = teamSetting.SixthColor,
                    SeventhColor = teamSetting.SeventhColor,
                    EighthColor = teamSetting.EighthColor,
                    
                };
            }
        }

        [HttpGet]
        public TeamSettingModel AdminGet(int teamId)
        {
            var teamSetting = db.TeamSettings.Find(teamId);
            if (teamSetting == null)
            {
                teamSetting = new Data.Models.TeamSetting
                {
                    ThirdMessage = "Cảm ơn các bạn đã cố gắng!!!",
                    SecondaryScreenEndTime = DateTime.Now.Date,
                    PrimaryScreenId = 9,
                    DisplayMode = 0,
                    TeamId = teamId,
                    DefaultDownMessage = "",
                    DefaultUpMessage = ""
                };
                db.TeamSettings.Add(teamSetting);
                db.SaveChanges();
            }
            var mapper = config.CreateMapper();
            return mapper.Map<TeamSettingModel>(teamSetting);
        }
        public bool CreateOrUpdate(TeamSettingModel model)
        {
            var teamSetting = db.TeamSettings.Find(model.TeamId);
            if (teamSetting == null)
            {
                teamSetting = new Data.Models.TeamSetting
                {
                    ThirdMessage = model.ThirdMessage,
                    SecondaryScreenEndTime = DateTime.Now.AddMinutes(model.SecondaryScreenTime),
                    SecondaryScreenId = model.SecondaryScreenId,
                    PrimaryScreenId = 9,
                    DisplayMode = model.DisplayMode,
                    AutoScreenStartTime = DateTime.Now,
                    TeamId = model.TeamId
                };
                db.TeamSettings.Add(teamSetting);
            }
            else
            {
                teamSetting.DisplayMode = model.DisplayMode;
                teamSetting.PrimaryScreenId = model.PrimaryScreenId;
                teamSetting.SecondaryScreenEndTime = DateTime.Now.AddMinutes(model.SecondaryScreenTime);
                teamSetting.SecondaryScreenId = model.SecondaryScreenId;
                
                teamSetting.ThirdMessage = model.ThirdMessage;
                teamSetting.FourthMessage = model.FourthMessage;
                teamSetting.FifthMessage = model.FifthMessage;
                teamSetting.SixthMessage = model.SixthMessage;
                teamSetting.SeventhMessage = model.SeventhMessage;
                teamSetting.EighthMessage = model.EighthMessage;

                teamSetting.ThirdColor = model.ThirdColor;
                teamSetting.FourthColor = model.FourthColor;
                teamSetting.FifthColor = model.FifthColor;
                teamSetting.SixthColor = model.SixthColor;
                teamSetting.SeventhColor = model.SeventhColor;
                teamSetting.EighthColor = model.EighthColor;

                if (teamSetting.DisplayMode == 1) //auto mode
                {
                    teamSetting.AutoScreenStartTime = DateTime.Now;
                    teamSetting.FirstScreenTime = model.FirstScreenTime;
                    teamSetting.SecondScreenTime = model.SecondScreenTime;
                    teamSetting.FourthScreenTime = model.FourthScreenTime;
                    teamSetting.FifthScreenTime = model.FifthScreenTime;
                    teamSetting.SixthScreenTime = model.SixthScreenTime;
                    teamSetting.SeventhScreenTime = model.SeventhScreenTime;
                    teamSetting.EighthScreenTime = model.EighthScreenTime;
                    teamSetting.NinthScreenTime = model.NinthScreenTime;
                }
            }
            db.SaveChanges();
            return true;
        }
    }
}
