using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DateOfBirth { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal CgpaorPercentage { get; set; }

    public string Contact { get; set; } = null!;

    public int StatusId { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Interview> Interviews { get; } = new List<Interview>();

    public virtual LookUp Status { get; set; } = null!;
}
