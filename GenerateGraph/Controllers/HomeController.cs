using GenerateGraph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
//System.Windows.Forms.DataVisualization;

namespace GenerateGraph.Controllers
{
    public class HomeController : Controller
    {
        readonly GenerateGraphContext generateGraphContext = new GenerateGraphContext();
        public ActionResult Chart()
        {
            List<ReportData> reportData = generateGraphContext.GetReportData();
            string[] status = new string[reportData.Count];
            int[] count = new int[reportData.Count];
            for (int i = 0; i < reportData.Count; i++)
            {
                status[i] = reportData[i].Status;
                count[i] = reportData[i].Count;
            }
            // Reversing the arrays to get status in the order-Not started,in progress,completed
            Array.Reverse(status);
            Array.Reverse(count);
            ViewBag.Status = status;
            ViewBag.Count = count;
            return View();
        }
        public ActionResult GenerateLineChart(int year, string status)
        {
            var res = generateGraphContext.GetMonthlyReqs(year, status);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRquests()
        {
            var reqs = generateGraphContext.GetAllRequests();
            return Json(new { data = reqs.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRquestById(int id)
        {
            var reqDetails = generateGraphContext.GetRequestById(id);
            return Json(reqDetails, JsonRequestBehavior.AllowGet);
        }
        public string DetailsByStatus(string status)
        {
            string res = "";
            var result = generateGraphContext.GetDetailsByStatus(status);
            foreach (var item in result)
            {
                int id = item.Id;
                string clientName = item.ClientName;
                string req = item.Request;
                string stat = item.Status;
                int createdBy = item.CreatedBy;
                var createdDate = item.CreatedDate.ToShortDateString();
                res = res +
                    "<tr><td>" + id + "</td>" +
                    "<td>" + clientName + "</td>" +
                    "<td>" + req + "</td>" +
                    "<td>" + stat + "</td>" +
                    "<td>" + createdBy + "</td>" +
                    "<td>" + createdDate + "</td></tr>";
            }
            return res;
        }
        public string DetailsByMonth(int year, string month)
        {
            string res = "";
            var result = generateGraphContext.GetReqsByMonth(year, month);
            foreach (var item in result)
            {
                int id = item.Id;
                string clientName = item.ClientName;
                string req = item.Request;
                string stat = item.Status;
                int createdBy = item.CreatedBy;
                var createdDate = item.CreatedDate.ToShortDateString();
                res = res +
                    "<tr><td>" + id + "</td>" +
                    "<td>" + clientName + "</td>" +
                    "<td>" + req + "</td>" +
                    "<td>" + stat + "</td>" +
                    "<td>" + createdBy + "</td>" +
                    "<td>" + createdDate + "</td></tr>";
            }
            return res;
        }
    }

}