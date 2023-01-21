using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Interface
{
    public interface IJobService
    {
        Result<JobOpeningViewModel> SaveJobOpening(JobOpeningViewModel model);
        Result<InviteViewModel> SaveInvite(InviteViewModel inviteViewModel);
        Result<List<JobOpeningViewModel>> GetJobOpeningsByOrganizationId(int organizationId);
    }
}