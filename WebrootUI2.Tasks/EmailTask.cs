using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebrootUI2.Domain.Contracts.Tasks;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using WebrootUI2.Domain;
using WebrootUI2.Infrastructure.Common.Log;
using System.Threading;
using System.Web;

namespace WebrootUI2.Tasks
{
    public class EmailTask : IEmailTask
    {
        public bool SendResetPWMail(string username,string toEmail,string token,string role)
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                string url = string.Format("<a href=\"{0}\">{0}</a>",
                request.Url.ToString().Replace(request.RawUrl, string.Empty) + string.Format("/{0}/VerifyResetPassword?token=",role) + token);

                var message = new MailMessage();
                message.Subject = "Reset password verification";
                message.To.Add(toEmail);
                message.Body = string.Format("Hi {0},<br><br>" +
                   "You have recently requested a password reset. Please visit the following link to reset your password,<br> {1}<br><br>" +
                   "Thanks,<br> MCPayment Admin"
                   , Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(username)
                   , url);
                message.IsBodyHtml = true;

                return SendEmail(message);
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }

        private bool SendEmail(MailMessage message)
        {
            try
            {
                message.From = new MailAddress(Setting.EmailAddress);

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = Setting.SmtpHost;
                    smtpClient.Port = Setting.SmtpPort;
                    smtpClient.Credentials = new NetworkCredential(Setting.EmailAddress, Setting.EmailPassword);
                    smtpClient.EnableSsl = Setting.EnabledSSL;

                    smtpClient.Send(message);
                }
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
