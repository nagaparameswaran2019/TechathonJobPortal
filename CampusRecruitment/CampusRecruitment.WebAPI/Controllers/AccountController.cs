using CampusRecruitment.Service.Interface;
using CampusRecruitment.Service.Service;
using CampusRecruitment.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CampusRecruitment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public Result<List<UserViewModel>> GetAllUsers()
        {
            try
            {
                return _userService.GetAll();
            }
            catch (Exception ex)
            {
                return new Result<List<UserViewModel>>("Unable to get User details", null, false);
            }
        }

        [HttpPost]
        [Route("RegisterUser")]
        public Result<UserViewModel> RegisterUser([FromBody] UserViewModel model)
        {
            try
            {
                return _userService.RegisterUser(model);
            }
            catch (Exception ex)
            {
                return new Result<UserViewModel>("Unable to register user details", null, false);
            }
        }

        [HttpPost]
        [Route("Login")]
        public Result<LoginModel> Login([FromBody] LoginModel model)
        {
            try
            {
                return _userService.Login(model);
            }
            catch (Exception ex)
            {
                return new Result<LoginModel>("Unable to login now, please try again after sometime", null, false);
            }
        }
    }
}
