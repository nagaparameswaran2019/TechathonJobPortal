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
            base.CreateMap<LookUpGroup, LookUpGroupViewModel>().ReverseMap();
            base.CreateMap<LookUp, LookUpViewModel>().ReverseMap();
            base.CreateMap<User, UserViewModel>().ReverseMap();
            base.CreateMap<User, LoginModel>().ReverseMap();
                //.ForMember(t => t.OrganizationName, opt => opt.MapFrom(src => src != null && src.Organization != null ? src.Organization.Name : string.Empty))
                //.ForMember(t => t.OrganizationType, opt => opt.MapFrom(src => src != null && src.Organization != null && src.Organization.OrganizationType != null ? src.Organization.OrganizationType.Code : string.Empty))
                //.ForMember(t => t.OrganizationSubType, opt => opt.MapFrom(src => src != null && src.Organization != null && src.Organization.OrganizationSubType != null ? src.Organization.OrganizationSubType.Code : string.Empty));
        }
    }
}
