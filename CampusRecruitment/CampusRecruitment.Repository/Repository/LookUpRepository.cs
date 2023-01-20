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
    public class LookUpRepository : GenericRepository<LookUp>, ILookUpRepository
    {
        private CampusRecruitmentContext _context = null;

        public LookUpRepository(CampusRecruitmentContext context) : base(context)
        {
            _context = context;
        }
    }
}
