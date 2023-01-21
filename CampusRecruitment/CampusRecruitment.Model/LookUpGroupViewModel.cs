using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class LookUpGroupViewModel
    {
        public LookUpGroupViewModel() {
            LookUps = new List<LookUpViewModel>();
        }

        public int LookUpGroupId { get; set; }

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<LookUpViewModel> LookUps { get; set; }
    }
}
