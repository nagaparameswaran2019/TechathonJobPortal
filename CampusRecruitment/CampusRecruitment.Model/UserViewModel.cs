using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Organization = new OrganizationViewModel();
        }
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int OrganizationId { get; set; }

        public virtual OrganizationViewModel Organization { get; set; }
    }
}
