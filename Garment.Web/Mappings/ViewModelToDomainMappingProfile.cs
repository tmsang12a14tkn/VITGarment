using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Garment.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        //protected override void Configure()
        //{
        //    Mapper.CreateMap<CreateRoleModel, IdentityRole>();
        //}
    }
}
