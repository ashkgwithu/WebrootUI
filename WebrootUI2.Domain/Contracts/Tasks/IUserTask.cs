using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Domain.Contracts.Tasks
{
    public interface IUserTask
    {
        List<User> Search(string username, string role);
        List<User> GetAdminUsers();
        List<User> SearchAdminUsers(string username, string role);
        User GetUserById(Guid userId);
        bool UpdateUser(User user);
        bool Delete(Guid userId);
        bool Remove(Guid userId);
    }
}
