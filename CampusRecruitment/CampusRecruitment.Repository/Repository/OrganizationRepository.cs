﻿using CampusRecruitment.Entities.Entities;
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
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        private CampusRecruitmentContext _context = null;

        public OrganizationRepository(CampusRecruitmentContext context) : base(context)
        {
            _context = context;
        }
    }
}
