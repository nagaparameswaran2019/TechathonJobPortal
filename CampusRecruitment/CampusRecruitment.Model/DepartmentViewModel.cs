using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class DepartmentViewModel
    {
        public DepartmentViewModel() { }
        public int DepartmentId { get; set; }

        public string Name { get; set; } = null!;

        public int DepartmentTypeId { get; set; }

        public int OrganizationId { get; set; }

        public virtual OrganizationViewModel Organization { get; set; } = null!;

        public virtual ICollection<DepartmentCoreAreaMappingViewModel> DepartmentCoreAreaMappings { get; }
    }
}
