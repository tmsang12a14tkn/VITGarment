using AutoMapper;
using Data.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garment.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        //protected override void Configure()
        //{
        //    Mapper.CreateMap<IdentityRole, CreateRoleModel>();

        //}
    }
}
