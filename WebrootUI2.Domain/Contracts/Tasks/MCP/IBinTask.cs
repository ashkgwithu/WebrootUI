using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models.MCP;

namespace WebrootUI2.Domain.Contracts.Tasks.MCP
{
    public interface IBinTask
    {
        Bin Create(int bin,int cardNumberLength,string description,int range,
            DateTime createdDate, string createdBy, DateTime modifiedDate, string modifiedBy);
        List<Bin> GetAll();
    }
}
