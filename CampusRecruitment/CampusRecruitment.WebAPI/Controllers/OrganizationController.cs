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
                List<OrganizationViewModel> organizationViewModels = _organizationService.GetAllOrganization();
                return new Result<List<OrganizationViewModel>>("Organization details get successfully", organizationViewModels);
            }
            catch (Exception ex)
            {
                return new Result<List<OrganizationViewModel>>("Unable to get Organization the details", null, false);
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
                List<LookUpViewModel> organizationViewModels = _organizationService.GetOrganizationTypes();
                return new Result<List<LookUpViewModel>>("Lookup details get successfully", organizationViewModels);
            }
            catch (Exception ex)
            {
                return new Result<List<LookUpViewModel>>("Unable to get Lookup the details", null, false);
            }
        }
    }
}
