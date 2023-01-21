using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class DepartmentCoreAreaMappingViewModel
    {
        public int DepartmentCoreAreaMappingId { get; set; }

        public int DepartmentId { get; set; }

        public int? CoreAreaTypeId { get; set; } = null;

        public string? CoreAreaTypes { get; set; } = null;

        public virtual LookUpViewModel? CoreAreaType { get; set; } = null!;

        public virtual DepartmentViewModel? Department { get; set; } = null!;
    }
}
