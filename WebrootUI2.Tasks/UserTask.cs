using NHibernate;
using NHibernate.Criterion.Lambda;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebrootUI2.Domain;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Domain.Models;
using WebrootUI2.Infrastructure.Common.Log;

namespace WebrootUI2.Tasks
{
    public class UserTask : IUserTask
    {
        private readonly IRepository<User> userRepo;

        public UserTask(IRepository<User> userRepo)
        {
            this.userRepo = userRepo;
        }

        #region Administrator

        public List<User> Search(string username, string role)
        {
            var users = new List<User>();

            try
            {
                var allUsersQuery = GetAdminUsers();

                users = (from u in allUsersQuery
                         where (
                             (username == string.Empty || u.UserName.ToLower().Contains(username.Trim().ToLower())) &&
                             (role == string.Empty || u.Role.LoweredName.Contains(role.Trim().ToLower())))
                         select u).ToList<User>();

                return users;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<User>();
            }
        }

        public List<User> GetAdminUsers()
        {
            var users = new List<User>();

            if (Setting.AdministratorId == null)
                return new List<User>();

            try
            {
                users = userRepo.GetAll()
                    .Where(u => u.UserCategory != null && u.UserCategory.AdministratorId == Setting.AdministratorId)
                    .ToList<User>();

                return users;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<User>();
            }
        }

        public List<User> SearchAdminUsers(string username, string role)
        {
            var users = new List<User>();

            try
            {
                users = Search(username, role);

                return users;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<User>();
            }
        }

        #endregion

        public User GetUserById(Guid userId)
        {
            var user = new User();

            try
            {
                userRepo.DbContext.BeginTransaction();
                user = userRepo.GetAll().Single(u=>u.UserId == userId);
                userRepo.DbContext.CommitTransaction();

                return user;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new User();
            }
        }

        public bool Delete(Guid userId)
        {
            var user = new User();

            try
            {
                userRepo.DbContext.BeginTransaction();

                user = userRepo.GetAll().Single(u => u.UserId == userId);
                user.Audit.IsDeleted = true;

                userRepo.DbContext.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        public bool Remove(Guid userId)
        {
            var user = new User();

            try
            {
                userRepo.DbContext.BeginTransaction();

                user = userRepo.GetAll().Single(u => u.UserId == userId);
                userRepo.Delete(user);

                userRepo.DbContext.CommitTransaction();

                return true;

            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                userRepo.DbContext.BeginTransaction();
                userRepo.SaveOrUpdate(user);
                userRepo.DbContext.CommitTransaction();

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