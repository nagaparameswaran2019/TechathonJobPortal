using CampusRecruitment.Service.Interface;
using CampusRecruitment.Service.Service;
using CampusRecruitment.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusRecruitment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookUpGroupController : ControllerBase
    {
        private ILookUpGroupService _lookUpGroupService;
        public LookUpGroupController(ILookUpGroupService lookUpGroupService)
        {
            _lookUpGroupService = lookUpGroupService;
        }

        [HttpGet]
        [Route("GetLookupGroupByName/{groupNames?}")]
        public Result<List<LookUpGroupViewModel>> GetLookupGroupByName([FromRoute]string? groupNames = "")
        {
            try
            {
                List<LookUpGroupViewModel> lookUpGroupViewModels = _lookUpGroupService.GetLookupGroupByName(groupNames);
                return new Result<List<LookUpGroupViewModel>>("Lookup details get successfully", lookUpGroupViewModels);
            }
            catch (Exception ex)
            {
                return new Result<List<LookUpGroupViewModel>>("Unable to get Lookup details", null, false);
            }
        }
    }
}
