using CampusRecruitment.Entities.Entities;
using CampusRecruitment.Mapper;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using CampusRecruitment.Repository.Repository;
using CampusRecruitment.Service.Interface;
using CampusRecruitment.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Service
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        IUserRepository _userRepository;
        //IOrganizationRepository _organizationRepository;
        //IDepartmentRepository _departmentRepository;

        public UserService(IUnitOfWork unitOfWork
            , IUserRepository userRepository
            //,IOrganizationRepository organizationRepository
            //,IDepartmentRepository departmentRepository
            )
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            //_organizationRepository = organizationRepository;
            //_departmentRepository = departmentRepository;
        }

        public Result<List<UserViewModel>> GetAll()
        {
            var data = _userRepository.GetAll();
            var viewData = data.CopyTo<List<UserViewModel>>();
            return new Result<List<UserViewModel>>("User details get successfully", viewData);
        }

        public Result<UserViewModel> RegisterUser(UserViewModel model)
        {
            var users = _userRepository.Get(t => t.UserName.ToLower().Equals(model.UserName.ToLower()));

            if (users?.Count() > 0)
            {
                return new Result<UserViewModel>("Username already exists", null, false);
            }

            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;

            if (!string.IsNullOrEmpty(model.Organization.Department))
            {
                List<DepartmentViewModel> departmentList = model.Organization.Department.Split(',').Select(s =>
                new DepartmentViewModel()
                {
                    DepartmentTypeId = Convert.ToInt32(s.Trim()),
                    OrganizationId = model.Organization.OrganizationId,
                    Organization = model.Organization,
                }
                ).ToList();

                model.Organization.Departments = departmentList;
            }
            else
            {
                model.Organization.Departments = null;
            }

            User user = model.CopyTo<User>();

            _userRepository.Add(user);

            _unitOfWork.Save();

            var viewData = user.CopyTo<UserViewModel>();
            return new Result<UserViewModel>("User details save successfully", viewData, true);
        }

        public Result<LoginModel> Login(LoginModel model)
        {
            var user = _userRepository.Login(model.UserName, model.Password);
            if (user == null)
            {
                return new Result<LoginModel>("Unable to login user", null, false);
            }

            var viewData = user.CopyTo<LoginModel>();
            viewData.Email = user?.Organization?.Email;
            viewData.OrganizationName = user?.Organization?.Name;
            viewData.OrganizationType = user?.Organization?.OrganizationType?.Code;
            viewData.OrganizationSubType = user?.Organization?.OrganizationSubType?.Code;


            return new Result<LoginModel>("User login successfully.", viewData, true);
        }
    }
}
