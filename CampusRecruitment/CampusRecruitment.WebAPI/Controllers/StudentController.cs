using CampusRecruitment.Service.Interface;
using CampusRecruitment.Service.Service;
using CampusRecruitment.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusRecruitment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        [Route("SaveStudentDetails")]
        public Result<StudentViewModel> SaveStudentDetails([FromBody] StudentViewModel studentViewModel)
        {
            try
            {
                return _studentService.SaveStudentDetails(studentViewModel);
            }
            catch (Exception ex)
            {
                return new Result<StudentViewModel>("Unable to get student details", null, false);
            }
        }

        [HttpGet]
        [Route("GetLookupGroupByName/{departmentId}")]
        public Result<List<StudentViewModel>> GetAllStudentsByDepartmentId([FromRoute] int departmentId)
        {
            try
            {
                return _studentService.GetAllStudentsByDepartmentId(departmentId);
            }
            catch (Exception ex)
            {
                return new Result<List<StudentViewModel>>("Unable to get student details", null, false);
            }
        }
    }
}
