using CampusRecruitment.Service;
using CampusRecruitment.Service.Interface;
using CampusRecruitment.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusRecruitment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        [Route("SaveJobOpening")]
        public Result<JobOpeningViewModel> SaveJobOpening([FromBody] JobOpeningViewModel model)
        {
            try
            {
                return _jobService.SaveJobOpening(model);
            }
            catch (Exception ex)
            {
                return new Result<JobOpeningViewModel>("Unable to Save Job opening", null, false);
            }
        }

        [HttpGet]
        [Route("GetJobOpeningsByOrganizationId/{organizationId}")]
        public Result<List<JobOpeningViewModel>> GetJobOpeningsByOrganizationId([FromRoute] int organizationId)
        {
            try
            {
                return _jobService.GetJobOpeningsByOrganizationId(organizationId);
            }
            catch (Exception ex)
            {
                return new Result<List<JobOpeningViewModel>>("Unable to get Job opening details", null, false);
            }
        }

        [HttpPost]
        [Route("SaveInvite")]
        public Result<InviteViewModel> AddInvite([FromBody] InviteViewModel model)
        {
            try
            {
                return _jobService.SaveInvite(model);
            }
            catch (Exception ex)
            {
                return new Result<InviteViewModel>("Unable to Save Invite details.", null, false);
            }
        }

        [HttpPost]
        [Route("SaveInterviewDetails")]
        public Result<InterviewViewModel> SaveInterviewDetails([FromBody] InterviewViewModel model)
        {
            try
            {
                return _jobService.SaveInterviewDetails(model);
            }
            catch (Exception ex)
            {
                return new Result<InterviewViewModel>("Unable to Save Interview details.", null, false);
            }
        }
    }
}
