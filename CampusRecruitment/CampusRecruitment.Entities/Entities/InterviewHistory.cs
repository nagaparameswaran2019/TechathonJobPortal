using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class InterviewHistory
{
    public int InterviewHistoryId { get; set; }

    public int RoundTypeId { get; set; }

    public int StatusTypeId { get; set; }

    public DateTime? AttendedDate { get; set; }

    public int InterviewId { get; set; }

    public virtual Interview Interview { get; set; } = null!;

    public virtual LookUp RoundType { get; set; } = null!;

    public virtual LookUp StatusType { get; set; } = null!;
}
