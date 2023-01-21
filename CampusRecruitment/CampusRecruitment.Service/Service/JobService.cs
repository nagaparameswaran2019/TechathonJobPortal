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
using Microsoft.EntityFrameworkCore;

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

        public Result<JobOpeningViewModel> SaveJobOpening(JobOpeningViewModel model)
        {
            JobOpening? jobOpening = null;

            model.IsActive = true;
            if (model.JobOpeningId == 0)
            {
                if (!string.IsNullOrEmpty(model.JobOpeningCoreAreaMapping))
                {
                    List<JobOpeningCoreAreaMappingViewModel> jobOpeningCoreAreaMappingViewModels = model.JobOpeningCoreAreaMapping.Split(',').Select(s =>
                    new JobOpeningCoreAreaMappingViewModel()
                    {
                        CoreAreaTypeId = Convert.ToInt32(s.Trim()),
                        JobOpening = model
                    }
                    ).ToList();

                    model.JobOpeningCoreAreaMappings = jobOpeningCoreAreaMappingViewModels;
                }
                else
                {
                    model.JobOpeningCoreAreaMappings = null;
                }
                jobOpening = model.CopyTo<JobOpening>();
                _jobOpeningRepository.Add(jobOpening);
            }
            else
            {
                jobOpening = _jobOpeningRepository.Single(predicate: t => t.JobOpeningId == model.JobOpeningId, include: i => i.Include(s => s.JobOpeningCoreAreaMappings));

                if (jobOpening != null)
                {
                    jobOpening.MinCgpaorPercent = model.MinCgpaorPercent;
                    jobOpening.NumberOfOpening = model.NumberOfOpening;
                    _jobOpeningRepository.Update(jobOpening);
                }
                else
                {
                    return new Result<JobOpeningViewModel>("Unable to get Job opening details.", null, false);
                }
            }

            _unitOfWork.Save();

            var viewData = jobOpening.CopyTo<JobOpeningViewModel>();
            return new Result<JobOpeningViewModel>("Job opening details saved successfully", viewData, true);
        }

        public Result<InviteViewModel> SaveInvite(InviteViewModel inviteViewModel)
        {
            if (inviteViewModel == null)
            {
                return new Result<InviteViewModel>("Unable save Invite details.", null, false);
            }

            var inviteModel = inviteViewModel.CopyTo<Invite>();           


            if (inviteModel.InviteId > 0)
            {
                _inviteRepository.Update(inviteModel);
            }
            else
            {
                inviteModel.DatetimeOfInvite = DateTime.Now;
                _inviteRepository.Add(inviteModel);
            }

            _unitOfWork.Save();

            var viewData = inviteModel.CopyTo<InviteViewModel>();
            return new Result<InviteViewModel>("Invite details saved successfully", viewData, true);
        }
    }
}
