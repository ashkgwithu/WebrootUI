using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Contracts.Tasks.MCP;
using WebrootUI2.Domain.Models;
using WebrootUI2.Domain.Models.MCP;
using WebrootUI2.Infrastructure.Common.Log;

namespace WebrootUI2.Tasks.MCP
{
    public class ModuleTask : IModuleTask
    {
        private readonly IRepository<Module> moduleRepo;

        /// <summary>
        /// Inject module repo in the constructor.
        /// </summary>
        public ModuleTask(IRepository<Module> moduleRepo)
        {
            this.moduleRepo = moduleRepo;
        }


        /// <summary>
        /// Insert a module object
        /// </summary>
        public Module Create(string description, string module, DateTime createdDate, Guid createdBy, DateTime modifiedDate, Guid modifiedBy)
        {
            var moduleEntity = new Module();

            try
            {
                moduleEntity.Description = description;
                moduleEntity._Module = module;
                moduleEntity.Audit = new AuditComponent()
                {
                    CreatedDate = createdDate,
                    CreatedById = createdBy,
                    LastModifiedDate = modifiedDate,
                    LastModifiedById = modifiedBy
                };

                moduleRepo.DbContext.BeginTransaction();
                moduleRepo.SaveOrUpdate(moduleEntity);
                moduleRepo.DbContext.CommitTransaction();

                return moduleEntity;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new Module();
            }
        }


        /// <summary>
        /// Get all module objects
        /// </summary>
        public List<Module> GetAll()
        {
            var modules = new List<Module>();

            try
            {
                moduleRepo.DbContext.BeginTransaction();
                modules = moduleRepo.GetAll().ToList<Module>();

                return modules;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<Module>();
            }
        }

        /// <summary>
        /// Delete a module
        /// </summary>
        public bool Delete(Module module,Guid userId)
        {
            try
            {
                moduleRepo.DbContext.BeginTransaction();

                module.Audit.IsDeleted = true;
                module.Audit.LastModifiedDate = DateTime.Now;
                module.Audit.LastModifiedById = userId;

                moduleRepo.SaveOrUpdate(module);
                moduleRepo.DbContext.CommitTransaction();
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
