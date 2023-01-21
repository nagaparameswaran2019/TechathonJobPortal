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
    }
}
