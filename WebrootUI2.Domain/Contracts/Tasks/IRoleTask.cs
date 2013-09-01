using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Domain.Contracts.Tasks
{
    public interface IRoleTask
    {
        bool Update(Role role);
    }
}