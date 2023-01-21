using CampusRecruitment.Entities.Entities;
using CampusRecruitment.Entities;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Repository.Repository
{
    public class DepartmentCoreAreaMappingRepository : GenericRepository<DepartmentCoreAreaMapping>, IDepartmentCoreAreaMappingRepository
    {
        private CampusRecruitmentContext _context = null;

        public DepartmentCoreAreaMappingRepository(CampusRecruitmentContext context) : base(context)
        {
            _context = context;
        }
    }
}