using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class LookUp
{
    public int LookUpId { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public int LookUpGroupId { get; set; }

    public virtual ICollection<DepartmentCoreAreaMapping> DepartmentCoreAreaMappings { get; } = new List<DepartmentCoreAreaMapping>();

    public virtual ICollection<Department> Departments { get; } = new List<Department>();

    public virtual ICollection<InterviewHistory> InterviewHistoryRoundTypes { get; } = new List<InterviewHistory>();

    public virtual ICollection<InterviewHistory> InterviewHistoryStatusTypes { get; } = new List<InterviewHistory>();

    public virtual ICollection<Interview> InterviewRoundTypes { get; } = new List<Interview>();

    public virtual ICollection<Interview> InterviewStatusTypes { get; } = new List<Interview>();

    public virtual ICollection<JobOpeningCoreAreaMapping> JobOpeningCoreAreaMappings { get; } = new List<JobOpeningCoreAreaMapping>();

    public virtual ICollection<JobOpening> JobOpenings { get; } = new List<JobOpening>();

    public virtual LookUpGroup LookUpGroup { get; set; } = null!;

    public virtual ICollection<Organization> OrganizationOrganizationSubTypes { get; } = new List<Organization>();

    public virtual ICollection<Organization> OrganizationOrganizationTypes { get; } = new List<Organization>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
