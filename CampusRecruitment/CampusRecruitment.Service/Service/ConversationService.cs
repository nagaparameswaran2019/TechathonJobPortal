using CampusRecruitment.Entities.Entities;
using CampusRecruitment.Mapper;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using CampusRecruitment.Service.Interface;
using CampusRecruitment.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Service
{
    public class ConversationService : IConversationService
    {
        IUnitOfWork _unitOfWork;
        IConversationRepository _conversationRepository;

        public ConversationService(IUnitOfWork unitOfWork, IConversationRepository conversationRepository)
        {
            _unitOfWork = unitOfWork;
            _conversationRepository = conversationRepository;
        }

        public Result<List<ConversationHistoryViewModel>> SaveConversationDetails(ConversationViewModel conversationViewModel)
        {
            var nessage = conversationViewModel.CopyTo<Conversation>();
            nessage.ConversationDate = DateTime.Now;
            _conversationRepository.Add(nessage);
            _unitOfWork.Save();

            return GetConversationDetailsByOrganizationId(conversationViewModel.OrganizationFromId);
        }

        public Result<List<ConversationHistoryViewModel>> GetConversationDetailsByOrganizationId(int organizationId)
        {
            var data = _conversationRepository.Get(
                predicate: t => t.OrganizationFrom.OrganizationId == organizationId || t.OrganizationTo.OrganizationId == organizationId,
                orderBy: o => o.OrderByDescending(s => s.ConversationDate),
                include: i => i.Include(s => s.OrganizationFrom).Include(s => s.OrganizationTo)
            ).ToList();

            var viewData = data.CopyTo<List<ConversationHistoryViewModel>>();
            return new Result<List<ConversationHistoryViewModel>>("Chat history retrived successfullly.", viewData, true);
        }

    }
}
