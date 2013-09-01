using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Domain.Contracts.Tasks
{
    public interface IUserCategoryTask
    {
        bool CreateOrUpdateMasterMerchant(MasterMerchant masterMerchant);
        bool CreateOrUpdateMerchant(Merchant merchant);
        bool CreateOrUpdateTerminal(Terminal terminal);
    }
}
