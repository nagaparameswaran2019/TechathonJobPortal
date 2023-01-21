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

namespace CampusRecruitment.Service.Service
{
    public class StudentService : IStudentService
    {
        IUnitOfWork _unitOfWork;
        IStudentRepository _studentRepository;

        public StudentService(IUnitOfWork unitOfWork, IStudentRepository studentRepository)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
        }

        public Result<List<StudentViewModel>> GetAllStudentsByDepartmentId(int departmentId)
        {
            var studentList = _studentRepository.Get(t => t.DepartmentId == departmentId).ToList();
            var viewData = studentList.CopyTo<List<StudentViewModel>>();
            return new Result<List<StudentViewModel>>("Student details retrieved successfully.", viewData, true);
        }

        public Result<StudentViewModel> SaveStudentDetails(StudentViewModel studentViewModel)
        {
            var model = studentViewModel.CopyTo<Student>();
            if (model == null)
            {
                return new Result<StudentViewModel>("Unable to save Student details.", null, false);
            }

            if (model.StudentId > 0)
            {
                _studentRepository.Update(model);
            }
            else
            {
                _studentRepository.Add(model);
            }

            _unitOfWork.Save();
            var viewData = model.CopyTo<StudentViewModel>();

            return new Result<StudentViewModel>("Student details saved successfully.", viewData, true);
        }
    }
}
