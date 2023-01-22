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

        public DepartmentService(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository, IDepartmentCoreAreaMappingRepository departmentCoreAreaMappingRepository)
        {
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;
            _departmentCoreAreaMappingRepository = departmentCoreAreaMappingRepository;
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

                List<DepartmentCoreAreaMapping> departmentList = model.CoreAreaTypes.Split(',').Select(s =>
                new DepartmentCoreAreaMapping()
                {
                    CoreAreaTypeId = Convert.ToInt32(s.Trim()),
                    DepartmentId = model.DepartmentId,
                    CoreAreaType = null,
                    Department = null
                }
                ).ToList();

                departmentList = departmentList.Where(d => !data.Any(a => a.CoreAreaTypeId == d.DepartmentId)).ToList();

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
            var data = _departmentRepository.Get(predicate: t => t.OrganizationId == orgId, include: s => s.Include(i => i.DepartmentType)).ToList();
            var viewData = data.CopyTo<List<DepartmentViewModel>>();
            return new Result<List<DepartmentViewModel>>("Department details retrieved successfully.", viewData, true);
        }
    }
}
