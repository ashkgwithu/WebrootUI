using NHibernate;
using NHibernate.Criterion.Lambda;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebrootUI2.Domain;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Domain.Models;
using WebrootUI2.Web.Mvc.Controllers.ViewModels;
using WebrootUI2.Infrastructure.Common.Log;
using System.Xml;
using System.IO;

namespace WebrootUI2.Web.Mvc.Controllers.Queries
{
    public class UserQuery : NHibernateQuery
    {
        public List<SelectListItem> GetSecurityQuestions()
        {
            var securityQuestions = new List<SelectListItem>();
            var xml = new XmlDocument();

            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XmlFiles\SecurityQuestion.xml");
                xml.Load(path);

                XmlNodeList nodeList = xml.SelectNodes("questions/question");

                foreach (XmlNode node in nodeList)
                {
                    securityQuestions.Add(new SelectListItem() { Text = node.InnerText, Value = node.InnerText });
                }

                return securityQuestions;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<SelectListItem>();
            }
        }

        public Guid GetAdministratorId(Guid userId)
        {
            try
            {
                var user = Session.QueryOver<User>().Where(u => u.UserId == userId).SingleOrDefault();

                if (user.UserCategory.AdministratorId != null)
                    return (Guid)user.UserCategory.AdministratorId;
                else
                    return Guid.Empty;

            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return Guid.Empty;
            }
        }
    }
}