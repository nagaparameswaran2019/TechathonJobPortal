using CampusRecruitment.Entities.Entities;
using CampusRecruitment.Mapper;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using CampusRecruitment.Repository.Repository;
using CampusRecruitment.Service.Interface;
using CampusRecruitment.ViewModel;
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
        IOrganizationRepository _organizationRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IOrganizationRepository organizationRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
        }

        public List<UserViewModel> GetAll()
        {
            var data = _userRepository.GetAll();
            var viewData = data.CopyTo<List<UserViewModel>>();
            return viewData;
        }

        public Result<UserViewModel> RegisterUser(UserViewModel model)
        {
            var users = _userRepository.Get(t => t.UserName.ToLower().Equals(model.UserName.ToLower()));

            if (users?.Count() > 0)
            {
                return new Result<UserViewModel>("Username already exists", null, false);
            }

            User user = model.CopyTo<User>();
            _userRepository.Add(user);

            Organization organization = model.Organization.CopyTo<Organization>();
            _organizationRepository.Add(organization);



            var data = _userRepository.GetAll();
            var viewData = user.CopyTo<UserViewModel>();
            return new Result<UserViewModel>("User details save successfully", viewData, true);
        }
    }
}
