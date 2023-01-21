using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class JobOpeningCoreAreaMappingViewModel
    {
        public JobOpeningCoreAreaMappingViewModel() {}

        public int JobOpeningCoreAreaMappingId { get; set; }

        public int JobOpeningId { get; set; }

        public int CoreAreaTypeId { get; set; }

        public virtual LookUpViewModel CoreAreaType { get; set; } = null!;

        public virtual JobOpeningViewModel JobOpening { get; set; } = null!;
    }
}
