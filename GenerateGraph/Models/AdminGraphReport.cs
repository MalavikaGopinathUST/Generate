using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerateGraph.Models
{
    public class AdminGraphReport
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Request { get; set; }
        public string Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> RequestProcessStartedBy { get; set; }
        public Nullable<System.DateTime> RequestProcessStartDate { get; set; }
        public Nullable<int> CompletedBy { get; set; }
        public Nullable<System.DateTime> completionDate { get; set; }
    }
}