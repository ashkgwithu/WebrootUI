using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebrootUI2.Domain.Contracts.Tasks
{
    public interface IEmailTask
    {
        bool SendResetPWMail(string username, string toEmail,string token,string role);
    }
}
