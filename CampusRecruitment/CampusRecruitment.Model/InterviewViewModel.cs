using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class InterviewViewModel
    {
        public InterviewViewModel() { }

        public int InterviewId { get; set; }

        public int RoundTypeId { get; set; }

        public int StatusTypeId { get; set; }

        public DateTime? DateOfInterview { get; set; }

        public int StudentId { get; set; }

        public int JobOpeningId { get; set; }

        public virtual ICollection<InterviewHistoryViewModel>? InterviewHistories { get; set; } = new List<InterviewHistoryViewModel>();

        public virtual JobOpeningViewModel? JobOpening { get; set; } = null!;

        public virtual ICollection<OfferViewModel>? Offers { get; } = new List<OfferViewModel>();

        public virtual LookUpViewModel? RoundType { get; set; } = null!;

        public virtual LookUpViewModel? StatusType { get; set; } = null!;

        public virtual LookUpViewModel? Student { get; set; } = null!;
    }
}
