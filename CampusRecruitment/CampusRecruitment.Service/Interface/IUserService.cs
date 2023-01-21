using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Interface
{
    public interface IUserService
    {
        List<UserViewModel> GetAll();
        Result<UserViewModel> RegisterUser(UserViewModel model);
        Result<LoginModel> Login(LoginModel model);
    }
}

