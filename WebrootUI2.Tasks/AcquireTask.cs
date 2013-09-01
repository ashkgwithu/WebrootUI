using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Domain.Models;
using WebrootUI2.Infrastructure.Common.Log;

namespace WebrootUI2.Tasks
{
    public class  AcquireTask :IAcquireTask
    {
         private readonly IRepository<Acquire> AquireRepo;

         public AcquireTask(IRepository<Acquire> AquireRepo)
        {
            this.AquireRepo = AquireRepo;
        }

        #region Administrator
         /// <summary>
         /// Insert a Bin object
         /// </summary>
         public Acquire Create(int LogicalId, bool enabled, string name,
            DateTime createdDate, string createdBy, DateTime modifiedDate, string modifiedBy)
         {
             var acquireEntity = new Acquire();

             try
             {
                 //acquireEntity._Acquire = acquire;
                 // acquireEntity.Id = id;
                
                 acquireEntity.name = name;
                 acquireEntity.LogicalId = LogicalId;
                 acquireEntity.Enabled = enabled;
                 acquireEntity.CreatedDate = createdDate;
                 acquireEntity.CreatedBy = createdBy;
                 acquireEntity.LastModifiedDate = modifiedDate;
                 acquireEntity.LastModifiedBy = modifiedBy;
                 acquireEntity.IsDeleted = false;
                 AquireRepo.DbContext.BeginTransaction();
                 AquireRepo.SaveOrUpdate(acquireEntity);
                 AquireRepo.DbContext.CommitTransaction();
             }
             catch (Exception ex)
             {
                 LogManager.LogException(ex);
                 return new Acquire();
             }

             return acquireEntity;
         }

         public List<Acquire> GetAll()
         {
             var bins = new List<Acquire>();

             try
             {
                 AquireRepo.DbContext.BeginTransaction();
                 bins = AquireRepo.GetAll().Where(u => u.IsDeleted == false).ToList<Acquire>();

                 return bins;
             }
             catch (Exception ex)
             {
                 LogManager.LogException(ex);
                 return new List<Acquire>();
             }
         }

         public List<Acquire> Search(string name, int LogicalId)
        {
            var users = new List<Acquire>();

            try
            {
                var allUsersQuery = GetAcquire();

                users = (from u in allUsersQuery
                         where (
                             (name == string.Empty || u.name.ToLower().Contains(name.Trim().ToLower())) &&
                             (LogicalId == 0 || u.LogicalId == LogicalId))
                         select u).ToList<Acquire>();

                return users;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<Acquire>();
            }
        }

         public List<Acquire> GetAcquire()
        {
            var users = new List<Acquire>();

            //if (Setting.AdministratorId == null)
            //    return new List<Acquire>();

            try
            {
                users = AquireRepo.GetAll()
                    .Where(u => u.IsDeleted != null)
                    .ToList<Acquire>();

                return users;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<Acquire>();
            }
        }

        public List<Acquire> SearchAdminAcquire(string name, int logicalid)
        {
            var users = new List<Acquire>();

            try
            {
                users = Search(name, logicalid);

                return users;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<Acquire>();
            }
        }

        #endregion

        public Acquire GetAcquiredata(int Id)
        {
            var user = new Acquire();

            try
            {
                AquireRepo.DbContext.BeginTransaction();
                user = AquireRepo.GetAll().Single(u => u.Id == Id);
                AquireRepo.DbContext.CommitTransaction();

                return user;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new Acquire();
            }
        }

        public bool UpdateAcquire(Acquire acquire)
        {
            try
            {
                AquireRepo.DbContext.BeginTransaction();
                AquireRepo.SaveOrUpdate(acquire);
                AquireRepo.DbContext.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        public bool Delete(int Id)
        {
            var user = new Acquire();

            try
            {
                AquireRepo.DbContext.BeginTransaction();

                user = AquireRepo.GetAll().Single(u => u.Id == Id);
                user.IsDeleted = true;
                AquireRepo.SaveOrUpdate(user);
                AquireRepo.DbContext.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        public bool Remove(int Id)
        {
            var user = new Acquire();

            try
            {
                AquireRepo.DbContext.BeginTransaction();

                user = AquireRepo.GetAll().Single(u => u.Id == Id);
                AquireRepo.Delete(user);

                AquireRepo.DbContext.CommitTransaction();

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
