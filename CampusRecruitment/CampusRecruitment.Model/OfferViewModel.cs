using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class OfferViewModel
    {
        public OfferViewModel() { }

        public int OfferId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime JoiningDate { get; set; }

        public string FilePath { get; set; } = null!;

        public int InterviewId { get; set; }

        public virtual InterviewViewModel Interview { get; set; } = null!;
    }
}
