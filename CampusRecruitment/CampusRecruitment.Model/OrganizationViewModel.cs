using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class OrganizationViewModel
    {
        public OrganizationViewModel() { }
        public int OrganizationId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Website { get; set; }

        public string Contact { get; set; } = null!;

        public int OrganizationTypeId { get; set; }

        public int OrganizationSubTypeId { get; set; }
    }
}
