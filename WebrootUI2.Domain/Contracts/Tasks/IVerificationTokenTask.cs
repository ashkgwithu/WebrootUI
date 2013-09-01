using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Domain.Contracts.Tasks
{
    public interface IVerificationTokenTask
    {
        VerificationToken Create(string userId);
        string IsValidToken(string token);
        bool Delete(string token);
    }
}