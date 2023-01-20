using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class DepartmentCoreAreaMapping
{
    public int DepartmentCoreAreaMappingId { get; set; }

    public int DepartmentId { get; set; }

    public int CoreAreaTypeId { get; set; }

    public virtual LookUp CoreAreaType { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;
}
