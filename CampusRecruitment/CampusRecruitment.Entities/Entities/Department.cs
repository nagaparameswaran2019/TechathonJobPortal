using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class Department
{
    public int DepartmentId { get; set; }

    public int DepartmentTypeId { get; set; }

    public int OrganizationId { get; set; }

    public virtual ICollection<DepartmentCoreAreaMapping> DepartmentCoreAreaMappings { get; }

    public virtual LookUp DepartmentType { get; } = null!;

    public virtual Organization Organization { get; } = null!;

    public virtual ICollection<Student> Students { get; }
}
