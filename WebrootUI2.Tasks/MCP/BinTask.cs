using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Contracts.Tasks.MCP;
using WebrootUI2.Domain.Models.MCP;
using WebrootUI2.Infrastructure.Common.Log;

namespace WebrootUI2.Tasks.MCP
{
    public class BinTask : IBinTask
    {
        private readonly IRepository<Bin> binRepo;

        /// <summary>
        /// Inject binRepo in the BinTask constructor
        /// </summary>
        public BinTask(IRepository<Bin> binRepo)
        {
            this.binRepo = binRepo;
        }

        /// <summary>
        /// Insert a Bin object
        /// </summary>
        public Bin Create(int bin, int cardNumberLength, string description, int range,
            DateTime createdDate,string createdBy,DateTime modifiedDate,string modifiedBy)
        {
            var binEntity = new Bin();

            try
            {
                binEntity._Bin = bin;
                binEntity.CardNumberLength = cardNumberLength;
                binEntity.Description = description;
                binEntity.Range = range;
                binEntity.CreatedDate = createdDate;
                binEntity.CreatedBy = createdBy;
                binEntity.ModifiedDate = modifiedDate;
                binEntity.ModifiedBy = modifiedBy;

                binRepo.DbContext.BeginTransaction();
                binRepo.SaveOrUpdate(binEntity);
                binRepo.DbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new Bin();
            }

            return binEntity;
        }

        /// <summary>
        /// Get all Bin objects
        /// </summary>
        public List<Bin> GetAll()
        {
            var bins = new List<Bin>();

            try
            {
                binRepo.DbContext.BeginTransaction();
                bins = binRepo.GetAll().ToList<Bin>();

                return bins;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<Bin>();
            }
        }
    }
}
