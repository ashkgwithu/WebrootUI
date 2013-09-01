using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models.MCP;

namespace WebrootUI2.Domain.Contracts.Tasks.MCP
{
    public interface IModuleTask
    {
        Module Create(string description,string module,DateTime createdDate,Guid createdBy,DateTime modifiedDate,
            Guid modifiedBy);

        List<Module> GetAll();

        bool Delete(Module module,Guid userId);
    }
}
