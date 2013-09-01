using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Domain.Models;
using WebrootUI2.Infrastructure.Common.Log;

namespace WebrootUI2.Tasks
{
    public class RoleTask : IRoleTask
    {
        private readonly IRepository<Role> roleRepo;

        public RoleTask(IRepository<Role> roleRepo)
        {
            this.roleRepo = roleRepo;
        }

        public bool Update(Role role)
        {
            try
            {
                roleRepo.DbContext.BeginTransaction();
                roleRepo.SaveOrUpdate(role);
                roleRepo.DbContext.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false; 
            }
        }
    }
}
