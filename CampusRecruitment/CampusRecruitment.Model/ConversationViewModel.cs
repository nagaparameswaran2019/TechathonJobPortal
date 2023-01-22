using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class ConversationViewModel
    {
        public int ConversationId { get; set; }

        public int OrganizationFromId { get; set; }

        public int OrganizationToId { get; set; }

        public string? Message { get; set; }

        public DateTime? ConversationDate { get; set; } = null;
        
    }
}
