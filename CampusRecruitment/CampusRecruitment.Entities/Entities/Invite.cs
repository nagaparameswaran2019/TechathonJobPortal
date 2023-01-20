using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class Invite
{
    public int InviteId { get; set; }

    public int OrganizationId { get; set; }

    public int JobOpeningId { get; set; }

    public DateTime? DatetimeOfInvite { get; set; }

    public virtual JobOpening JobOpening { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;
}
