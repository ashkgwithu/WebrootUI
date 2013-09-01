using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Domain.Contracts.Tasks
{
   public interface  IAcquireTask
    {

       Acquire Create(int LogicalId, bool Enabled, string name, 
            DateTime createdDate, string createdBy, DateTime modifiedDate, string modifiedBy);

       List<Acquire> GetAll(); 
        List<Acquire> Search(string name, int LogicalId);
        List<Acquire> GetAcquire();

        Acquire GetAcquiredata(int Id);
        bool UpdateAcquire(Acquire Acuire);
        bool Delete(int Id);
        bool Remove(int Id);
    }
}
