using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GenerateGraph.Models
{
    public class GenerateGraphContext
    {
        readonly string cs = "data source=G131LTRV\\SQLEXPRESS;initial catalog=graph;persist security info=True;Integrated Security=SSPI;";
        public IList<UserGraphReport> GetAllRequests()
        {
            var userGraphReports = new List<UserGraphReport>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Select_AllUserGraphReport", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                //Open the connection and execute the query
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var userGraphReport = new UserGraphReport();
                    userGraphReport.Id = Convert.ToInt32(sdr["Id"].ToString());
                    userGraphReport.ClientName = sdr["ClientName"].ToString();
                    userGraphReport.Request = sdr["Request"].ToString();
                    userGraphReport.Status = sdr["Status"].ToString();
                    userGraphReport.CreatedBy = Convert.ToInt32(sdr["CreatedBy"].ToString());
                    userGraphReport.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]);
                    if (userGraphReport.Status != "Not Started")
                    {
                        userGraphReport.RqstPrcsStartedBy = Convert.ToInt32(sdr["RqstPrcsStartedBy"].ToString());
                        userGraphReport.RqstPrcsStartDate = Convert.ToDateTime(sdr["RqstPrcsStartDate"].ToString());
                    }
                    if (userGraphReport.Status == "Completed")
                    {
                        userGraphReport.CompletedBy = Convert.ToInt32(sdr["CompletedBy"].ToString());
                        userGraphReport.CompletionDate = Convert.ToDateTime(sdr["CompletionDate"].ToString());
                    }
                    userGraphReports.Add(userGraphReport);
                }
                con.Close();
            }
            return userGraphReports;
        }
        public IList<ReportData> GetMonthlyReqs(int yr,string status)
        {
            var reportDatas1 = new List<ReportData>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Select_MonthlyStatusReqs", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@year", yr);
                cmd.Parameters.AddWithValue("@Status", status);
                //Open the connection and execute the query
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var reportData = new ReportData();
                    reportData.Month = sdr["month_name"].ToString();
                    reportData.MonthlyRequests = Convert.ToInt32(sdr["Count"].ToString());
                    //reportData.Count = Convert.ToInt32(sdr["Count"].ToString());
                    reportData.Status = sdr["Status"].ToString();
                    reportDatas1.Add(reportData);
                }
                con.Close();
            }
            string[] months = new string[12] { "January", "February", "March", "April", "May",
            "June","July","August","September","October","November","December"};
            var reportDatas2 = new List<ReportData>();
            int index = 0;
            string currentStatus = "";
            foreach (var item in reportDatas1)
            {
                while (index < months.Length)
                {
                    if (item.Month.Equals(months[index]))
                    {
                        reportDatas2.Add(item);
                        index++; break;
                    }
                    else
                    {
                        var reportData = new ReportData();
                        reportData.Month = months[index];
                        reportData.MonthlyRequests = 0;
                        reportData.Status = item.Status;
                        reportDatas2.Add(reportData);
                        index++;                        
                    }
                }
                currentStatus = item.Status;
            }
            if (reportDatas2.Count != months.Length)
            {
                for(int i = reportDatas2.Count; i < months.Length; i++)
                {
                    var reportData = new ReportData();
                    reportData.Month = months[i];
                    reportData.MonthlyRequests = 0;
                    reportData.Status = currentStatus;
                    reportDatas2.Add(reportData);
                }
            }
            return reportDatas2;
        }     
        public UserGraphReport GetRequestById(int id)
        {
            var userGraphReport = new UserGraphReport();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Select_UserGraphReportById", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);

                //Open the connection and execute the query
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    //var userGraphReport = new UserGraphReport();
                    userGraphReport.Id = Convert.ToInt32(sdr["Id"].ToString());
                    userGraphReport.ClientName = sdr["ClientName"].ToString();
                    userGraphReport.Request = sdr["Request"].ToString();
                    userGraphReport.Status = sdr["Status"].ToString();
                    userGraphReport.CreatedBy = Convert.ToInt32(sdr["CreatedBy"].ToString());
                    userGraphReport.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]);
                    if (userGraphReport.Status != "Not Started")
                    {
                        userGraphReport.RqstPrcsStartedBy = Convert.ToInt32(sdr["RqstPrcsStartedBy"].ToString());
                        userGraphReport.RqstPrcsStartDate = Convert.ToDateTime(sdr["RqstPrcsStartDate"].ToString());
                    }
                    if (userGraphReport.Status == "Completed")
                    {
                        userGraphReport.CompletedBy = Convert.ToInt32(sdr["CompletedBy"].ToString());
                        userGraphReport.CompletionDate = Convert.ToDateTime(sdr["CompletionDate"].ToString());
                    }
                    //userGraphReports.Add(userGraphReport);
                }
                con.Close();
            }
            return userGraphReport;
        }
        public IList<UserGraphReport> GetReqsByMonth(int yr,string month)
        {
            int mon = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
            var userGraphReports = new List<UserGraphReport>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Select_RqsByMonth", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@year", yr);
                cmd.Parameters.AddWithValue("@mon", mon);
                //Open the connection and execute the query
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var userGraphReport = new UserGraphReport();
                    userGraphReport.Id = Convert.ToInt32(sdr["Id"].ToString());
                    userGraphReport.ClientName = sdr["ClientName"].ToString();
                    userGraphReport.Request = sdr["Request"].ToString();
                    userGraphReport.Status = sdr["Status"].ToString();
                    userGraphReport.CreatedBy = Convert.ToInt32(sdr["CreatedBy"].ToString());
                    userGraphReport.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]);                   
                    userGraphReports.Add(userGraphReport);
                }
                con.Close();
            }
            return userGraphReports;
        }
        public List<UserGraphReport> GetDetailsByStatus(string status)
        {
            IList<UserGraphReport> userGraphReports = GetAllRequests();
            List<UserGraphReport> result = new List<UserGraphReport>();
            try
            {
                if (status != "Not Started" && status != "In Progress" && status != "Completed")
                {
                    throw new Exception("Invalid Status");
                }
                for (int i = 0; i < userGraphReports.Count; i++)
                {
                    if (userGraphReports[i].Status == status)
                    {
                        result.Add(userGraphReports[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return result;
        }
        public List<ReportData> GetReportData()
        {
            var reportDatas = new List<ReportData>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Select_StatusCount", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                //Open the connection and execute the query
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var reportData = new ReportData();
                    reportData.Count = Convert.ToInt32(sdr["Count"].ToString()); 
                    reportData.Status = sdr["Status"].ToString();
                    reportDatas.Add(reportData);
                }
                con.Close();                
            }
            return reportDatas;            
        }             
    }
}