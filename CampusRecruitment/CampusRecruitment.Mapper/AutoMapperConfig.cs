using AutoMapper;
using CampusRecruitment.Entities.Entities;
using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            base.CreateMap<Organization, OrganizationViewModel>().ReverseMap();
            base.CreateMap<Department, DepartmentViewModel>().ReverseMap();
            base.CreateMap<LookUp, LookUpViewModel>().ReverseMap();
            base.CreateMap<User, UserViewModel>().ReverseMap();
            base.CreateMap<LookUpGroup, LookUpGroupViewModel>().ReverseMap();
        }
    }
}
