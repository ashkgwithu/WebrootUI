using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

public static class Setting
{
    public const int Page_Size = 8;

    private static int smtpPort;
    private static bool enabledSSL;
    private static Guid administratorId;
    //private static int verificationRequestExpireTime;

    public static string SmtpHost
    {
        get
        {
            return ConfigurationManager.AppSettings["smtpHost"] == null ? string.Empty
                : ConfigurationManager.AppSettings["smtpHost"];
        }
    }

    public static string EmailAddress
    {
        get
        {
            return ConfigurationManager.AppSettings["emailAddress"] == null ? string.Empty
                : ConfigurationManager.AppSettings["emailAddress"];
        }
    }

    public static string EmailPassword
    {
        get
        {
            return ConfigurationManager.AppSettings["emailPassword"] == null ? string.Empty
                : ConfigurationManager.AppSettings["emailPassword"];
        }
    }

    public static int SmtpPort
    {
        get
        {
            return int.TryParse(ConfigurationManager.AppSettings["smtpPort"], out smtpPort) == true ? smtpPort
                : 0;
        }
    }

    public static int VerificationRequestExpireTime
    {
        get
        {
            return int.TryParse(ConfigurationManager.AppSettings["VerificationRequestExpireTime"], out smtpPort) == true ? smtpPort
                : 0;
        }
    }

    public static bool EnabledSSL
    {
        get { return bool.TryParse(ConfigurationManager.AppSettings["enabledSSL"], out enabledSSL) 
            ? enabledSSL : false; }
    }

    /// <summary>
    /// Stores the AdministratorId of the loged in user in a cookie (If Administrator)
    /// </summary>
    public static Guid AdministratorId
    {
        get
        {
            return Guid.TryParse(HttpContext.Current.Request.Cookies["AdministratorId"].Value.ToString(), out administratorId)
            ? administratorId
             : Guid.Empty;
        }
        set
        {
            if (value != Guid.Empty)
                HttpContext.Current.Response.Cookies.Add(
                    new HttpCookie("AdministratorId") { Value = value.ToString(), Expires = DateTime.Today.AddDays(2) });
            else
                HttpContext.Current.Response.Cookies.Remove("AdministratorId");
        }
    }
}