using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebrootUI2.Domain.Contracts.Tasks;
using SharpArch.Domain.PersistenceSupport;
using WebrootUI2.Domain;
using WebrootUI2.Infrastructure.Common.Log;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Tasks
{
    public class VerificationTokenTask : IVerificationTokenTask
    {
        private readonly IRepository<VerificationToken> verificationTokenRepo;

        public VerificationTokenTask(IRepository<VerificationToken> verificationTokenRepo)
        {
            this.verificationTokenRepo = verificationTokenRepo;
        }

        public VerificationToken Create(string userId)
        {
            var verToken = new VerificationToken();

            try
            {
                verToken.Token = Guid.NewGuid();
                verToken.CreatedDate = DateTime.Now;
                verToken.UserId = Guid.Parse(userId);

                verificationTokenRepo.DbContext.BeginTransaction();
                verToken = verificationTokenRepo.SaveOrUpdate(verToken);
                verificationTokenRepo.DbContext.CommitTransaction();

                return verToken;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return verToken;
            }
        }

        public string IsValidToken(string token)
        {
            VerificationToken verToken;

            try
            {
                verificationTokenRepo.DbContext.BeginTransaction();

                if (!verificationTokenRepo.GetAll().Any(l => l.Token.ToString().ToLower() == token.ToLower()))
                    return string.Empty;

                verToken = verificationTokenRepo.GetAll().OrderBy(l => l.CreatedDate).Reverse()
                    .Single(l => l.Token.ToString().ToLower() == token.ToLower());

                if (verToken.CreatedDate.Subtract(DateTime.Now).Minutes >= Setting.VerificationRequestExpireTime)
                    return string.Empty;

                return verToken.UserId.ToString();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return string.Empty;
            }
        }

        public bool Delete(string token)
        {
            try
            {
                verificationTokenRepo.DbContext.BeginTransaction();

                if (!verificationTokenRepo.GetAll().Any(l => l.Token.ToString().ToLower() == token.ToLower()))
                    return false;

                VerificationToken verToken = verificationTokenRepo.GetAll().OrderBy(l => l.CreatedDate).Reverse()
                    .Single(l => l.Token.ToString().ToLower() == token.ToLower());
                verificationTokenRepo.Delete(verToken);
                verificationTokenRepo.DbContext.CommitTransaction();

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
