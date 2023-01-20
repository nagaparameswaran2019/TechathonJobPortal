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

namespace CampusRecruitment.Service
{
    public class DepartmentService : IDepartmentService
    {
        IUnitOfWork _unitOfWork;
        IDepartmentRepository _departmentRepository;

        public DepartmentService(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository)
        {
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;
        }

        public List<DepartmentViewModel> GetAll()
        {
            var data = _departmentRepository.GetAll();
            var viewData = data.CopyTo<List<DepartmentViewModel>>();
            return viewData;
        }
    }
}
