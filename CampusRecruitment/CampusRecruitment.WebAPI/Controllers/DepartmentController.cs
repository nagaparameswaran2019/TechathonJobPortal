using CampusRecruitment.Service.Interface;
using CampusRecruitment.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusRecruitment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [Route("GetAllDepartment")]
        public Result<List<DepartmentViewModel>> GetAll()
        {
            try
            {
                List<DepartmentViewModel> departmentViewModels = _departmentService.GetAll();
                return new Result<List<DepartmentViewModel>>("Department details get successfully", departmentViewModels);
            }
            catch (Exception ex)
            {
                return new Result<List<DepartmentViewModel>>("Unable to get department details", null, false);
            }
        }

        [HttpPost]
        [Route("AddCoreAreasToDepartment")]
        public Result<List<DepartmentCoreAreaMappingViewModel>> AddCoreAreasToDepartment([FromBody] DepartmentCoreAreaMappingViewModel model)
        {
            try
            {
                return _departmentService.AddCoreAreasToDepartment(model);
            }
            catch (Exception ex)
            {
                return new Result<List<DepartmentCoreAreaMappingViewModel>>("Unable to create department core area mapping", null, false);
            }
        }
    }
}
