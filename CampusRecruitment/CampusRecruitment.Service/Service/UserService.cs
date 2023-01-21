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

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public List<UserViewModel> GetAll()
        {
            var data = _userRepository.GetAll();
            var viewData = data.CopyTo<List<UserViewModel>>();
            return viewData;
        }
    }
}
