using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int OrganizationId { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
