using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Tasks
{
    public class PermissionTask: IPermissionTask
    {
        private readonly IRepository<Permission> permissionRepo;

        public PermissionTask(IRepository<Permission> permissionRepo)
        {
            this.permissionRepo = permissionRepo;
        }

    }
}
