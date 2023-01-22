using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class Department
{
    public int DepartmentId { get; set; }

    public int DepartmentTypeId { get; set; }

    public int OrganizationId { get; set; }

    public virtual ICollection<DepartmentCoreAreaMapping> DepartmentCoreAreaMappings { get; } = new List<DepartmentCoreAreaMapping>();

    public virtual LookUp DepartmentType { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
