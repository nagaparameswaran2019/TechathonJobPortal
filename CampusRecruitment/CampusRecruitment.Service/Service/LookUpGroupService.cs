using CampusRecruitment.Entities.Entities;
using CampusRecruitment.Mapper;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using CampusRecruitment.Repository.Repository;
using CampusRecruitment.Service.Interface;
using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Service
{
    public class LookUpGroupService : ILookUpGroupService
    {
        IUnitOfWork _unitOfWork;
        ILookUpGroupRepository _lookUpGroupRepository;

        public LookUpGroupService(IUnitOfWork unitOfWork, ILookUpGroupRepository lookUpGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _lookUpGroupRepository = lookUpGroupRepository;
        }
        public List<LookUpGroupViewModel> GetLookupGroupByName(string groupNames)
        {
            var data = _lookUpGroupRepository.GetLookupGroupByName(groupNames);
            var viewData = data.CopyTo<List<LookUpGroupViewModel>>();
            return viewData;
        }
    }
}
