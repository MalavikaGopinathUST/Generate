using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerateGraph.Models
{
    public class ReportData
    {        
        public string Status { get; set; }
        public int Count { get; set; }
        public string Month { get; set; }
        public int MonthlyRequests { get; set; }
    }
}