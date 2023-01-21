using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class JobOpeningViewModel
    {
        public JobOpeningViewModel() { }

        public int JobOpeningId { get; set; }

        public int NumberOfOpening { get; set; }

        public decimal MinCgpaorPercent { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<InterviewViewModel> Interviews { get; } = new List<InterviewViewModel>();

        public virtual ICollection<InviteViewModel> Invites { get; } = new List<InviteViewModel>();

        public virtual ICollection<JobOpeningCoreAreaMappingViewModel> JobOpeningCoreAreaMappings { get; } = new List<JobOpeningCoreAreaMappingViewModel>();
    }
}
