using CampusRecruitment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Service.Interface
{
    public interface IStudentService
    {
        Result<List<StudentViewModel>> GetAllStudentsByDepartmentId(int departmentId);
        Result<StudentViewModel> SaveStudentDetails(StudentViewModel studentViewModel);
    }
}