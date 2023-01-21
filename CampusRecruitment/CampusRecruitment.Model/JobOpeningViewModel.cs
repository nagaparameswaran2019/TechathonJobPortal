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
        public string JobOpeningCoreAreaMapping { get; set; }

        public virtual ICollection<InterviewViewModel>? Interviews { get; } = null;

        public virtual ICollection<InviteViewModel>? Invites { get; } = null;

        public virtual ICollection<JobOpeningCoreAreaMappingViewModel>? JobOpeningCoreAreaMappings { get; set; } = null;
    }
}
