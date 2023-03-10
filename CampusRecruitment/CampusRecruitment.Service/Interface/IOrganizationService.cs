using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Interface
{
    public interface IOrganizationService
    {
        Result<List<OrganizationViewModel>> GetAllOrganization();
        Result<List<LookUpViewModel>> GetOrganizationTypes();
    }
}
