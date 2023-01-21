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

        public List<DepartmentViewModel> GetAll()
        {
            var data = _departmentRepository.GetAll();
            var viewData = data.CopyTo<List<DepartmentViewModel>>();
            return viewData;
        }

        public Result<List<DepartmentCoreAreaMappingViewModel>> CreateDepartmentCoreAreaMapping(DepartmentCoreAreaMappingViewModel model)
        {
            if (!string.IsNullOrEmpty(model.CoreAreaTypes))
            {
                List<DepartmentCoreAreaMapping> departmentList = model.CoreAreaTypes.Split(',').Select(s =>
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

                var data = _departmentCoreAreaMappingRepository.Get(t => t.DepartmentId == model.DepartmentId).ToList();
                var viewData = data.CopyTo<List<DepartmentCoreAreaMappingViewModel>>();
                return new Result<List<DepartmentCoreAreaMappingViewModel>>("Department Core Area Mapping saved successfully", viewData, true);
            }
            else
            {
                return new Result<List<DepartmentCoreAreaMappingViewModel>>("CoreAreaTypes should not be empty", null, false);
            }
        }
    }
}
