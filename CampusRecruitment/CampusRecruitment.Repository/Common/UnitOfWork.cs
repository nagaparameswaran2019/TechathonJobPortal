using CampusRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Repository.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public CampusRecruitmentContext Context { get; }

        public UnitOfWork(CampusRecruitmentContext context)
        {
            Context = context;
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
