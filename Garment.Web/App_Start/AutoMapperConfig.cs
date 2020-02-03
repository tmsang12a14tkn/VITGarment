using AutoMapper;
using Data.Models;
using Garment.Web.Controllers.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garment.Web
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            ConfigureDomainToView();
            ConfigureViewToDomain();
        }

        private static void ConfigureDomainToView()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Goal, GoalSessionModel>()
                .ForMember(x => x.TeamName, opt => opt.MapFrom(src => src.Team.Name));
                cfg.CreateMap<GoalDetail, GoalDetailModel>();
                //cfg.CreateMap<ProduceHistory, ProduceHistoryModel>()
                    //.ForMember(x => x.OldQuantity, opt => opt.MapFrom(src => src.Quantity))
                    //.ForMember(x => x.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name));
                cfg.CreateMap<Product, ProductModel>();
                cfg.CreateMap<Reason, ReasonModel>();
                cfg.CreateMap<TeamSetting, TeamSettingModel>();
            });

            //Mapper.CreateMap<Goal, GoalModel>()
            //    .ForMember(x => x.TeamName, opt => opt.MapFrom(src => src.Team.Name));
            //Mapper.CreateMap<GoalDetail, GoalDetailModel>();
            //Mapper.CreateMap<ProduceHistory, ProduceHistoryModel>()
            //    .ForMember(x=>x.OldQuantity, opt=>opt.MapFrom(src=>src.Quantity))
            //    .ForMember(x => x.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name));
            //Mapper.CreateMap<Product, ProductModel>();
            //Mapper.CreateMap<Reason, ReasonModel>();
            //Mapper.CreateMap<TeamSetting, TeamSettingModel>();
        }

        private static void ConfigureViewToDomain()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<GoalSessionModel, Goal>();
                cfg.CreateMap<GoalDetailModel, GoalDetail>();
                //cfg.CreateMap<ProduceHistoryModel, ProduceHistory>();
            });
        }
    }
}