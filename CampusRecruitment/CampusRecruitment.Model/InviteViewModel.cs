using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class InviteViewModel
    {
        public InviteViewModel() { }

        public int InviteId { get; set; }

        public int OrganizationId { get; set; }

        public int JobOpeningId { get; set; }

        public DateTime? DatetimeOfInvite { get; set; } = null;

        public virtual JobOpeningViewModel? JobOpening { get; set; } = null!;

        public virtual OrganizationViewModel? Organization { get; set; } = null!;
    }
}
