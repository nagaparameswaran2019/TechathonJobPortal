using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class LookUpViewModel
    {
        public LookUpViewModel() { }

        public int LookUpId { get; set; }

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public int LookUpGroupId { get; set; }
    }
}
