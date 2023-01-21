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
            base.CreateMap<OrganizationViewModel, Organization>().ReverseMap();
            base.CreateMap<DepartmentViewModel, Department>().ReverseMap();
            base.CreateMap<LookUpViewModel, LookUp>().ReverseMap();
            base.CreateMap<UserViewModel, User>().ReverseMap();
        }
    }
}
