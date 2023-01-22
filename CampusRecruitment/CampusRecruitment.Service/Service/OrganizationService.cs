using CampusRecruitment.Entities.Entities;
using CampusRecruitment.Mapper;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using CampusRecruitment.Service.Interface;
using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Service
{
    public class OrganizationService : IOrganizationService
    {
        IUnitOfWork _unitOfWork;
        IOrganizationRepository _organizationRepository;
        ILookUpRepository _lookUpRepository;

        public OrganizationService(IUnitOfWork unitOfWork, IOrganizationRepository organizationRepository, ILookUpRepository lookUpRepository)
        {
            _unitOfWork = unitOfWork;
            _organizationRepository = organizationRepository;
            _lookUpRepository = lookUpRepository;
        }

        public Result<List<OrganizationViewModel>> GetAllOrganization()
        {
            var data = _organizationRepository.GetAll();
            var viewData = data.CopyTo<List<OrganizationViewModel>>();
            return new Result<List<OrganizationViewModel>>("Organization details get successfully", viewData);
        }

        public Result<List<LookUpViewModel>> GetOrganizationTypes()
        {
            var data = _lookUpRepository.Get(t => t.LookUpGroupId == 3);
            var viewData = data.CopyTo<List<LookUpViewModel>>();
            return new Result<List<LookUpViewModel>>("Organization Types get successfully", viewData);
        }
    }
}
