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
    }
}
