using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class StudentViewModel
    {
        public StudentViewModel() { }

        public int StudentId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string DateOfBirth { get; set; } = null!;

        public string Email { get; set; } = null!;

        public decimal CgpaorPercentage { get; set; }

        public string Contact { get; set; } = null!;

        public int StatusId { get; set; }

        public int DepartmentId { get; set; }

        public virtual DepartmentViewModel? Department { get; set; } = null!;

        public virtual ICollection<InterviewViewModel>? Interviews { get; } = new List<InterviewViewModel>();

        public virtual LookUpViewModel? Status { get; set; } = null!;
    }
}
