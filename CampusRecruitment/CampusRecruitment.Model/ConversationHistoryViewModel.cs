using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.ViewModel
{
    public class ConversationHistoryViewModel : ConversationViewModel
    {
        public string? OrganizationFromName { get; set; } = null;
        public string? OrganizationToName { get; set; } = null;
    }
}
