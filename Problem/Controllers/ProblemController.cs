using Common;
using EF;
using Problem.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Problem.Controllers
{
    [Authentication]
    public class ProblemController : Controller
    {
        private AppdbContent appContext = new AppdbContent();


        // GET: Problem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Graph()
        {
            var list = appContext.Problem_A.ToList();
            //DateTime.Now.Month.ToString();
            string line = null;
            for (int i = 1; i <= 12; i++)
            {
                var all = list.Where(m => Convert.ToDateTime(m.OccurredTime).Month == i).ToList();
                var close = all.Where(m => m.Status == true).ToList();
                if (close.Count == 0 || all.Count == 0)
                {
                    if (i == 1)
                    {
                        line = "0";
                    }
                    else
                    {
                        line += ",0";
                    }

                }
                else
                {
                    if (i == 1)
                    {
                        line = (close.Count / all.Count).ToString();
                    }
                    else
                    {
                        line += "," + close.Count / all.Count;
                    }
                }

            }
            ViewBag.line = line;

            string sw = null;
            string sb = null;
            string ab = null;
            for (int i = 1; i <= 12; i++)
            {
                var all = list.Where(m => Convert.ToDateTime(m.OccurredTime).Month == i && m.Factory == "SW").ToList();
                if (i != 1)
                {
                    sw += "," + all.Count;
                }
                else
                {
                    sw = all.Count.ToString();
                }

            }

            for (int i = 1; i <= 12; i++)
            {
                var all = list.Where(m => Convert.ToDateTime(m.OccurredTime).Month == i && m.Factory == "SB").ToList();
                if (i != 1)
                {
                    sb += "," + all.Count;
                }
                else
                {
                    sb = all.Count.ToString();
                }

            }

            for (int i = 1; i <= 12; i++)
            {
                var all = list.Where(m => Convert.ToDateTime(m.OccurredTime).Month == i && m.Factory == "AB").ToList();
                if (i != 1)
                {
                    ab += "," + all.Count;
                }
                else
                {
                    ab = all.Count.ToString();
                }

            }
            ViewBag.SWdata = sw;
            ViewBag.SBdata = sb;
            ViewBag.ABdata = ab;
            return View();
        }
        
        public ActionResult Add()
        {
            string newid = DateTime.Now.ToString("yyyyMMddhhmmss");
            string id = System.Guid.NewGuid().ToString("N");

            ViewBag.NewID = newid;//单号
        
            ViewBag.token = id;
            Session["token"] = id;

            return View();
        }

        

        public JsonResult AddProblem()
        {
            try
            {
                var Session_token = Convert.ToString(Session["token"]);
                var Client_token = Request["token"];

                if (Client_token != Session_token)
                {
                    //ViewBag.Msg = "<script type='text/javascript'>alert('不可重复提交');</script>";
                    return Json(MsgCommon.respJsonErrorMsg("不可重复提交"));
                }
                else
                {
                    Problem_A pb = new Problem_A();
                    pb.ProId = Request["pId"];
                    pb.OccurredTime = Convert.ToDateTime(Request["OccurredTime"]);
                    pb.Week = DateCommon.GetWeekOfYear(Convert.ToDateTime(Request["OccurredTime"]));
                    pb.ComplaintsType = Request["ComplaintsType"];
                    pb.Customer = Request["Customer"];

                    pb.Model = Request["Model"];
                    pb.CusPartNum = Request["CusPartNum"];
                    pb.InPartNum = Request["InPartNum"];
                    pb.Factory = Request["Factory"];

                    pb.DefectNum = Convert.ToInt32(Request["DefectNum"]);
                    pb.ProductType = Request["ProductType"];
                    pb.SpecialEffects = Request["SpecialEffects"];
                    pb.RiskLevel = Request["RiskLevel"];

                    pb.RiskAssessment = Request["RiskAssessment"];
                    pb.DefectLevel = Request["DefectLevel"];
                    pb.DescribePro = Request["DescribePro"];
                    pb.Reason = Request["Reason"];

                    pb.Measure = Request["Measure"];
                    pb.Responsible_Department = Request["Responsible_Department"];
                    pb.Responsible_Person = Request["Responsible_Person"];
                    pb.ReportTime = Convert.ToDateTime(Request["ReportTime"]);

                    pb.Status = false;
                    pb.InsertTime = DateTime.Now;
                    pb.PastDue = false;
                    
                    appContext.Problem_A.Add(pb);
                    appContext.SaveChanges();

                    Session.Remove("token");

                    //ViewBag.Msg = "<script type='text/javascript'>alert('添加成功');</script>";
                    return Json(MsgCommon.respJsonSuccessMsg());
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                return Json(MsgCommon.respJsonErrorMsg(dbEx.ToString()));
            }

        }

        public JsonResult PData()
        {
            var list = appContext.Problem_A.ToList();
            #region 筛选条件

            
            if (Request["StartDate"] != null && Request["StartDate"] != "")
            {
                var Starttime = Convert.ToDateTime(Request["StartDate"]);
                list = list.Where(m => m.OccurredTime == Starttime).ToList();
            }

            string ComplaintsType = Request["ComplaintsType"];
            if (!string.IsNullOrWhiteSpace(ComplaintsType))
            {
                list = list.Where(m => m.ComplaintsType.Contains(ComplaintsType)).ToList();
            }

            string Customer = Request["Customer"];
            if (!string.IsNullOrWhiteSpace(Customer))
            {
                list = list.Where(m => m.Customer.Contains(Customer)).ToList();
            }

            string ProductType = Request["ProductType"];
            if (!string.IsNullOrWhiteSpace(ProductType))
            {
                list = list.Where(m => m.ProductType.Contains(ProductType)).ToList();
            }

            string RiskLevel = Request["RiskLevel"];
            if (!string.IsNullOrWhiteSpace(RiskLevel))
            {
                list = list.Where(m => m.RiskLevel.Contains(RiskLevel)).ToList();
            }

            string Responsible_Department = Request["Responsible_Department"];
            if (!string.IsNullOrWhiteSpace(Responsible_Department))
            {
                list = list.Where(m => m.Responsible_Department.Contains(Responsible_Department)).ToList();
            }
            #endregion
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}