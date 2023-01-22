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
            base.CreateMap<Department, DepartmentViewModel>()
                .ForMember(t => t.Name, opt => opt.MapFrom(src => src.DepartmentType.Description))
            .ReverseMap();
            base.CreateMap<DepartmentCoreAreaMapping, DepartmentCoreAreaMappingViewModel>().ReverseMap();
            base.CreateMap<LookUpGroup, LookUpGroupViewModel>().ReverseMap();
            base.CreateMap<LookUp, LookUpViewModel>().ReverseMap();
            base.CreateMap<User, UserViewModel>().ReverseMap();
            base.CreateMap<User, LoginModel>().ReverseMap();
            base.CreateMap<JobOpening, JobOpeningViewModel>().ReverseMap();
            base.CreateMap<JobOpeningCoreAreaMapping, JobOpeningCoreAreaMappingViewModel>().ReverseMap();
            base.CreateMap<Interview, InterviewViewModel>().ReverseMap();
            base.CreateMap<InterviewHistory, InterviewHistoryViewModel>().ReverseMap();
            base.CreateMap<Invite, InviteViewModel>().ReverseMap();
            base.CreateMap<Offer, OfferViewModel>().ReverseMap();
            base.CreateMap<Student, StudentViewModel>().ReverseMap();
            base.CreateMap<Student, StudentDepartmentModel>()
                .ForMember(t => t.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentType.Description))
                .ReverseMap();
            base.CreateMap<Conversation, ConversationViewModel>().ReverseMap();
            base.CreateMap<Conversation, ConversationHistoryViewModel>()
                .ForMember(t => t.OrganizationFromName, opt => opt.MapFrom(src => src.OrganizationFrom.Name))
                .ForMember(t => t.OrganizationToName, opt => opt.MapFrom(src => src.OrganizationTo.Name))
                .ReverseMap();
        }
    }
}
