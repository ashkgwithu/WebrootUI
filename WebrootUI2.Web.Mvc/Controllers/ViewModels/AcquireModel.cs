using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Web.Mvc.Controllers.ViewModels
{
    public class AcquireModel     {
        public AcquireModel()
        {
            Acquire = new List<AcquireModel>();
        }
        public int  Id { get; set; }
       public string name { get; set; }
       public int logicalid { get; set; }
       public bool Enabled { get; set; }
       public bool IsDeleted { get; set; }
       public int TotalRecordsCount { get; set; }
       public List<AcquireModel> Acquire { get; set; }
    }
    public class LogModel
    {
        public string DisplayDate { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }
}