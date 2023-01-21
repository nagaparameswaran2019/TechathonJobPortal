using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; } = null;

        public string? FirstName { get; set; } = null;

        public string? LastName { get; set; } = null;

        public int? OrganizationId { get; set; } = null;

        public string? OrganizationName { get; set; } = null;

        public string? OrganizationType { get; set; } = null;

        public string? OrganizationSubType { get; set; } = null;
    }
}
