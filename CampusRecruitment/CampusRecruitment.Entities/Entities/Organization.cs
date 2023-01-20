using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class Organization
{
    public int OrganizationId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Website { get; set; }

    public string Contact { get; set; } = null!;

    public int OrganizationTypeId { get; set; }

    public int OrganizationSubTypeId { get; set; }

    public virtual ICollection<Invite> Invites { get; } = new List<Invite>();

    public virtual LookUp OrganizationSubType { get; set; } = null!;

    public virtual LookUp OrganizationType { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
