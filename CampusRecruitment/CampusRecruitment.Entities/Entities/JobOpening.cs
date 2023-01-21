using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class JobOpening
{
    public int JobOpeningId { get; set; }

    public int NumberOfOpening { get; set; }

    public decimal MinCgpaorPercent { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Interview> Interviews { get; } = new List<Interview>();

    public virtual ICollection<Invite> Invites { get; } = new List<Invite>();

    public virtual ICollection<JobOpeningCoreAreaMapping> JobOpeningCoreAreaMappings { get; set; } = new List<JobOpeningCoreAreaMapping>();
}
