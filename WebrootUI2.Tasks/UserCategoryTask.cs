using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Domain.Models;
using WebrootUI2.Infrastructure.Common.Log;

namespace WebrootUI2.Tasks
{
    class UserCategoryTask : IUserCategoryTask
    {
        private readonly IRepository<MasterMerchant> RepoMasterMerchant;
        private readonly IRepository<Merchant> RepoMerchant;
        private readonly IRepository<Terminal> RepoTerminal;

        public bool CreateOrUpdateMasterMerchant(MasterMerchant masterMerchant)
        {
            try
            {
                RepoMasterMerchant.DbContext.BeginTransaction();
                RepoMasterMerchant.SaveOrUpdate(masterMerchant);
                RepoMasterMerchant.DbContext.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        public bool CreateOrUpdateMerchant(Merchant merchant)
        {
            try
            {
                RepoMerchant.DbContext.BeginTransaction();
                RepoMerchant.SaveOrUpdate(merchant);
                RepoMerchant.DbContext.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        public bool CreateOrUpdateTerminal(Terminal terminal)
        {
            try
            {

                RepoTerminal.DbContext.BeginTransaction();
                RepoTerminal.SaveOrUpdate(terminal);
                RepoTerminal.DbContext.CommitTransaction();

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
