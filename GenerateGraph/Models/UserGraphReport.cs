using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerateGraph.Models
{
    public class UserGraphReport
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Request { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> RqstPrcsStartedBy { get; set; }
        public Nullable<System.DateTime> RqstPrcsStartDate { get; set; }
        public Nullable<int> CompletedBy { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
    }
}