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
using CampusRecruitment.EmailService;
using CampusRecruitment.Utils.Common;
using System.Net;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
        ILookUpGroupRepository _lookUpGroupRepository;

        public JobService(IUnitOfWork unitOfWork
            , IInviteRepository inviteRepository
            , IJobOpeningRepository jobOpeningRepository
            , IJobOpeningCoreAreaMappingRepository jobOpeningCoreAreaMappingRepository
            , IInterviewRepository interviewRepository
            , IInterviewHistoryRepository interviewHistoryRepository
            , IOfferRepository offerRepository
            , ILookUpGroupRepository lookUpGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _inviteRepository = inviteRepository;
            _jobOpeningRepository = jobOpeningRepository;
            _jobOpeningCoreAreaMappingRepository = jobOpeningCoreAreaMappingRepository;
            _interviewRepository = interviewRepository;
            _interviewHistoryRepository = interviewHistoryRepository;
            _offerRepository = offerRepository;
            _lookUpGroupRepository = lookUpGroupRepository;
        }

        public Result<JobOpeningViewModel> SaveJobOpening(JobOpeningViewModel model)
        {
            JobOpening? jobOpening = null;

            //model.IsActive = true;
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

        public Result<List<JobOpeningViewModel>> GetJobOpeningsByOrganizationId(int organizationId)
        {
            var jobCoreAreas = _lookUpGroupRepository.Get(predicate: t => t.Code.ToLower().Equals("coreareatype"), include: s => s.Include(i => i.LookUps)).FirstOrDefault();

            List<JobOpening> jobOpenings = _jobOpeningRepository.Get(
                predicate: t => t.Invites.Any(inv => inv.OrganizationId == organizationId),
            include: inc => inc.Include(c => c.JobOpeningCoreAreaMappings)).ToList();

            var viewData = jobOpenings.CopyTo<List<JobOpeningViewModel>>();

            if (jobCoreAreas != null && jobCoreAreas.LookUps?.Count > 0)
            {
                var lookUp = jobCoreAreas.LookUps.ToList();
                foreach (var item in viewData)
                {
                    var data = from a in item.JobOpeningCoreAreaMappings
                               join b in lookUp
                               on a.CoreAreaTypeId equals b.LookUpId
                               select b.Description;

                    item.JobOpeningCoreAreaMapping = string.Join(",", data);
                    item.JobOpeningCoreAreaMappings = null;
                }
            }
            return new Result<List<JobOpeningViewModel>>("Job openings retrieved successfully.", viewData, true);
        }

        public Result<InterviewViewModel> SaveInterviewDetails(InterviewViewModel interviewViewModel)
        {
            if (interviewViewModel == null)
            {
                return new Result<InterviewViewModel>("Unable to save interview details", null, false);
            }

            var data = interviewViewModel.CopyTo<Interview>();

            if (data.InterviewId > 0)
            {
                _interviewRepository.Update(data);
            }
            else
            {
                _interviewRepository.Add(data);
            }

            InterviewHistory interviewHistory = new InterviewHistory()
            {
                AttendedDate = data.DateOfInterview,
                InterviewId = data.InterviewId,
                RoundTypeId = data.RoundTypeId,
                StatusTypeId = data.StatusTypeId,
                Interview = data
            };

            _interviewHistoryRepository.Add(interviewHistory);

            _unitOfWork.Save();

            var viewData = data.CopyTo<InterviewViewModel>();
            viewData.InterviewHistories = null;

            return new Result<InterviewViewModel>("Interview details saved successfully", viewData, true);
        }

        public Result<OfferViewModel> SendOfferLetter(OfferViewModel interviewViewModel)
        {
            if (interviewViewModel == null)
            {
                return new Result<OfferViewModel>("Unable to save Offer details", null, false);
            }

            var data = interviewViewModel.CopyTo<Offer>();

            if (data.OfferId > 0)
            {
                _offerRepository.Update(data);
            }
            else
            {
                _offerRepository.Add(data);
            }

            _unitOfWork.Save();

            var viewData = data.CopyTo<OfferViewModel>();
            viewData.Interview = null;

            EmailInfo emailInfo = new EmailInfo()
            {
                Subject = "Techathon Recruitment Email",
                Body = "Techathon Recruitment Email body",
                ToEmail = new List<string> { "test.mail@gmail.com" },
                FromEmail = AppSetting.GetConfigValue("SmtpFromEmail"),
                FromDisplayName = "Company Name",
                AttachmentPath = null,
                IsBodyHtml = true,
                ResendCount = null,
                CCEmail = (AppSetting.GetConfigValue("DefaultCcEmails").Split(',')).ToList()
            };
            EmailService.EmailService.SendEmail(emailInfo);

            return new Result<OfferViewModel>("Offer details saved successfully", viewData, true);
        }
    }
}
