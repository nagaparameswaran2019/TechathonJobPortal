using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class JobOpeningViewModel
    {
        public int JobOpeningId { get; set; }

        public int NumberOfOpening { get; set; }

        public decimal MinCgpaorPercent { get; set; }

        public bool? IsActive { get; set; }
        public string JobOpeningCoreAreaMapping { get; set; }

        public string? JobDescription { get; set; }

        public string? Qualification { get; set; }

        public string? Role { get; set; }

        public int? EmploymentTypeId { get; set; }

        public int? OrganizationId { get; set; }

        public virtual LookUpViewModel? EmploymentType { get; set; }

        public virtual ICollection<InterviewViewModel> Interviews { get; } = new List<InterviewViewModel>();

        public virtual ICollection<InviteViewModel> Invites { get; } = new List<InviteViewModel>();

        public virtual ICollection<JobOpeningCoreAreaMappingViewModel> JobOpeningCoreAreaMappings { get; set; } = new List<JobOpeningCoreAreaMappingViewModel>();

        public virtual OrganizationViewModel? Organization { get; set; }
    }
}