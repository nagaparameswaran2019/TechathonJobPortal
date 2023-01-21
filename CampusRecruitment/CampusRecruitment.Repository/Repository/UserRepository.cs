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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private CampusRecruitmentContext _context = null;

        public UserRepository(CampusRecruitmentContext context) : base(context)
        {
            _context = context;
        }

        public User Login(string userName, string password)
        {
            return _dbSet.Where(t => t.UserName.ToLower().Equals(userName.ToLower()) && t.Password.Equals(password))
                .Include(t => t.Organization)
                .Include(t=>t.Organization.OrganizationType)
                .Include(t => t.Organization.OrganizationSubType)
                .FirstOrDefault();
        }
    }
}
