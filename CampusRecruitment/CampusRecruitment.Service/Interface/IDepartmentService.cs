using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Interface
{
    public interface IDepartmentService
    {
        Result<List<DepartmentViewModel>> GetAll();
        Result<List<DepartmentCoreAreaMappingViewModel>> AddCoreAreasToDepartment(DepartmentCoreAreaMappingViewModel model);
        Result<List<DepartmentViewModel>> GetAllDepartmentByOrgId(int orgId);
    }
}
