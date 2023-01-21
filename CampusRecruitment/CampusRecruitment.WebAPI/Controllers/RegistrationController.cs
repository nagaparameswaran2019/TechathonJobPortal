﻿using CampusRecruitment.Service.Interface;
using CampusRecruitment.Service.Service;
using CampusRecruitment.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CampusRecruitment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IUserService _userService;
        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public Result<List<UserViewModel>> GetAllOrganization()
        {
            try
            {
                List<UserViewModel> userViewModels = _userService.GetAll();
                return new Result<List<UserViewModel>>("User details get successfully", userViewModels);
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
    }
}
