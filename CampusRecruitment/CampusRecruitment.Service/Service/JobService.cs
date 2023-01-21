using CampusRecruitment.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using CampusRecruitment.ViewModel;
using CampusRecruitment.Repository.Repository;
using CampusRecruitment.Mapper;
using CampusRecruitment.Entities.Entities;

namespace CampusRecruitment.Service.Service
{
    public class JobService : IJobService
    {
        IUnitOfWork _unitOfWork;
        IInviteRepository _inviteRepository;
        IJobOpeningRepository _jobOpeningRepository;
        IJobOpeningCoreAreaMappingRepository _jobOpeningCoreAreaMappingRepository;
        IInterviewRepository _interviewRepository;
        IInterviewHistoryRepository _interviewHistoryRepository;
        IOfferRepository _offerRepository;

        public JobService(IUnitOfWork unitOfWork, IInviteRepository inviteRepository, IJobOpeningRepository jobOpeningRepository, IJobOpeningCoreAreaMappingRepository jobOpeningCoreAreaMappingRepository, IInterviewRepository interviewRepository, IInterviewHistoryRepository interviewHistoryRepository, IOfferRepository offerRepository)
        {
            _unitOfWork = unitOfWork;
            _inviteRepository = inviteRepository;
            _jobOpeningRepository = jobOpeningRepository;
            _jobOpeningCoreAreaMappingRepository = jobOpeningCoreAreaMappingRepository;
            _interviewRepository = interviewRepository;
            _interviewHistoryRepository = interviewHistoryRepository;
            _offerRepository = offerRepository;
        }
    }
}
