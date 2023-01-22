using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Interface
{
    public interface IConversationService
    {
        Result<List<ConversationHistoryViewModel>> SaveConversationDetails(ConversationViewModel conversationViewModel);
        Result<List<ConversationHistoryViewModel>> GetConversationDetailsByOrganizationId(int organizationId);
    }
}
