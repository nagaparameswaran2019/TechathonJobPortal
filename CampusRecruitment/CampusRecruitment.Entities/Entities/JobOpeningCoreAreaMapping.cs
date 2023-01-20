using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class JobOpeningCoreAreaMapping
{
    public int JobOpeningCoreAreaMappingId { get; set; }

    public int JobOpeningId { get; set; }

    public int CoreAreaTypeId { get; set; }

    public virtual LookUp CoreAreaType { get; set; } = null!;

    public virtual JobOpening JobOpening { get; set; } = null!;
}
