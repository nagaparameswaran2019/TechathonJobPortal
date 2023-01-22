using CampusRecruitment.Service.Interface;
using CampusRecruitment.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CampusRecruitment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        [Route("GetAllOrganization")]
        public Result<List<OrganizationViewModel>> GetAllOrganization()
        {
            try
            {
                return _organizationService.GetAllOrganization();
            }
            catch (Exception ex)
            {
                return new Result<List<OrganizationViewModel>>("Unable to get Organization details", null, false);
            }
        }

        /// <summary>
        /// GetOrganizationTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOrganizationTypes")]
        public Result<List<LookUpViewModel>> GetOrganizationTypes()
        {
            try
            {
                return _organizationService.GetOrganizationTypes();
            }
            catch (Exception ex)
            {
                return new Result<List<LookUpViewModel>>("Unable to get Organization Types", null, false);
            }
        }
    }
}
