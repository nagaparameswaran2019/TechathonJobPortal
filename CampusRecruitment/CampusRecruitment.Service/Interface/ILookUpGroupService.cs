using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Interface
{
    public interface ILookUpGroupService
    {
        Result<List<LookUpGroupViewModel>> GetLookupGroupByName(string? groupNames);
    }
}
