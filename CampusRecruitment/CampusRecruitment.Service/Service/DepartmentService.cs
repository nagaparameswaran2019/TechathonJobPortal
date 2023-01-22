using CampusRecruitment.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using CampusRecruitment.ViewModel;
using CampusRecruitment.Repository.Repository;
using CampusRecruitment.Mapper;
using CampusRecruitment.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusRecruitment.Service
{
    public class DepartmentService : IDepartmentService
    {
        IUnitOfWork _unitOfWork;
        IDepartmentRepository _departmentRepository;
        IDepartmentCoreAreaMappingRepository _departmentCoreAreaMappingRepository;
        ILookUpGroupRepository _lookUpGroupRepository;

        public DepartmentService(IUnitOfWork unitOfWork
            , IDepartmentRepository departmentRepository
            , IDepartmentCoreAreaMappingRepository departmentCoreAreaMappingRepository,
            ILookUpGroupRepository lookUpGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;
            _departmentCoreAreaMappingRepository = departmentCoreAreaMappingRepository;
            _lookUpGroupRepository = lookUpGroupRepository;
        }

        public Result<List<DepartmentViewModel>> GetAll()
        {
            var data = _departmentRepository.GetAll();
            var viewData = data.CopyTo<List<DepartmentViewModel>>();
            return new Result<List<DepartmentViewModel>>("Department details get successfully", viewData, true);
        }

        public Result<List<DepartmentCoreAreaMappingViewModel>> AddCoreAreasToDepartment(DepartmentCoreAreaMappingViewModel model)
        {
            if (!string.IsNullOrEmpty(model.CoreAreaTypes))
            {
                var data = _departmentCoreAreaMappingRepository.Get(t => t.DepartmentId == model.DepartmentId).ToList();

                List<DepartmentCoreAreaMapping> departmentList = model.CoreAreaTypes.Split(',').Where(t => !data.Any(s => s.CoreAreaTypeId == Convert.ToInt32(t.Trim()))).Select(s =>
                    new DepartmentCoreAreaMapping()
                    {
                        CoreAreaTypeId = Convert.ToInt32(s.Trim()),
                        DepartmentId = model.DepartmentId,
                        CoreAreaType = null,
                        Department = null
                    }
                ).ToList();

                _departmentCoreAreaMappingRepository.Add(departmentList);
                _unitOfWork.Save();

                data = _departmentCoreAreaMappingRepository.Get(t => t.DepartmentId == model.DepartmentId).ToList();

                var viewData = data.CopyTo<List<DepartmentCoreAreaMappingViewModel>>();
                return new Result<List<DepartmentCoreAreaMappingViewModel>>("Department Core Area Mapping saved successfully", viewData, true);
            }
            else
            {
                return new Result<List<DepartmentCoreAreaMappingViewModel>>("CoreAreaTypes should not be empty", null, false);
            }
        }

        public Result<List<DepartmentViewModel>> GetAllDepartmentByOrgId(int orgId)
        {
            var jobCoreAreas = _lookUpGroupRepository.Get(predicate: t => t.Code.ToLower().Equals("coreareatype"), include: s => s.Include(i => i.LookUps)).FirstOrDefault();
            var data = _departmentRepository.Get(predicate: t => t.OrganizationId == orgId, include: s => s.Include(i => i.DepartmentType).Include(c => c.DepartmentCoreAreaMappings)).ToList();
            var viewData = data.CopyTo<List<DepartmentViewModel>>();

            if (jobCoreAreas != null && jobCoreAreas.LookUps?.Count > 0)
            {
                var lookUp = jobCoreAreas.LookUps.ToList();
                foreach (var item in viewData)
                {
                    if (item.DepartmentCoreAreaMappings == null)
                    {
                        continue;
                    }

                    var filteredData = from a in item.DepartmentCoreAreaMappings
                                       join b in lookUp
                                       on a.CoreAreaTypeId equals b.LookUpId
                                       select b.LookUpId.ToString();

                    item.DepartmentCoreAreaMapping = string.Join(",", filteredData);
                }
            }

            return new Result<List<DepartmentViewModel>>("Department details retrieved successfully.", viewData, true);
        }
    }
}
