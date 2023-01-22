using CampusRecruitment.Entities.Entities;
using CampusRecruitment.Entities;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CampusRecruitment.Repository.Repository
{
    public class LookUpGroupRepository : GenericRepository<LookUpGroup>, ILookUpGroupRepository
    {
        private CampusRecruitmentContext _context = null;

        public LookUpGroupRepository(CampusRecruitmentContext context) : base(context)
        {
            _context = context;
        }

        public List<LookUpGroup> GetLookupGroupByName(string? groupNames)
        {
            List<string> groupNameList = !string.IsNullOrEmpty(groupNames) ? groupNames.Split(',').Select(s => s.ToLower().Trim()).ToList() : new List<string>();
            List<LookUpGroup> lookUpGroups = null;

            if (string.IsNullOrEmpty(groupNames) || string.Compare(groupNames, "all", true) == 0)
            {
                lookUpGroups = _dbSet.Include(t => t.LookUps).ToList();
            }
            else
            {
                lookUpGroups = _dbSet.Where(t => groupNameList.Any(s => s.Equals(t.Code.ToLower()))).Include(t => t.LookUps).ToList();
            }

            return lookUpGroups;
        }
    }
}
