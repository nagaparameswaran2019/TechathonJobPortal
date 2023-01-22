using CampusRecruitment.Service.Interface;
using CampusRecruitment.Service.Service;
using CampusRecruitment.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusRecruitment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private IConversationService _conversationService;
        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpPost]
        [Route("SaveConversationDetails")]
        public Result<List<ConversationHistoryViewModel>> SaveConversationDetails([FromBody] ConversationViewModel conversationViewModel)
        {
            try
            {
                return _conversationService.SaveConversationDetails(conversationViewModel);
            }
            catch (Exception ex)
            {
                return new Result<List<ConversationHistoryViewModel>>("Unable to save conversation details.", null, false);
            }
        }

        [HttpGet]
        [Route("GetConversationDetailsByOrganizationId/{organizationId}")]
        public Result<List<ConversationHistoryViewModel>> GetConversationDetailsByOrganizationId([FromRoute] int organizationId)
        {
            try
            {
                return _conversationService.GetConversationDetailsByOrganizationId(organizationId);
            }
            catch (Exception ex)
            {
                return new Result<List<ConversationHistoryViewModel>>("Unable to get conversation details", null, false);
            }
        }
    }
}
