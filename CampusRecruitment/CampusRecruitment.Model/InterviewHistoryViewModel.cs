using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class InterviewHistoryViewModel
    {
        public InterviewHistoryViewModel() { }

        public int InterviewHistoryId { get; set; }

        public int RoundTypeId { get; set; }

        public int StatusTypeId { get; set; }

        public DateTime? AttendedDate { get; set; }

        public int InterviewId { get; set; }

        public virtual InterviewViewModel Interview { get; set; } = null!;

        public virtual LookUpViewModel RoundType { get; set; } = null!;

        public virtual LookUpViewModel StatusType { get; set; } = null!;
    }
}
