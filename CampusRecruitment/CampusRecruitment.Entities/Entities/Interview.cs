using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class Interview
{
    public int InterviewId { get; set; }

    public int RoundTypeId { get; set; }

    public int StatusTypeId { get; set; }

    public DateTime? DateOfInterview { get; set; }

    public int StudentId { get; set; }

    public int JobOpeningId { get; set; }

    public virtual ICollection<InterviewHistory> InterviewHistories { get; } = new List<InterviewHistory>();

    public virtual JobOpening JobOpening { get; set; } = null!;

    public virtual ICollection<Offer> Offers { get; } = new List<Offer>();

    public virtual LookUp RoundType { get; set; } = null!;

    public virtual LookUp StatusType { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
