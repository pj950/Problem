using Application;
using Application.Impl;
using Application.Model;
using Common;
using EF;
using Problem.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Problem.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        private AppdbContent appContext = new AppdbContent();
        private IAccountService aService = new AccountService();

        public ActionResult Index()
        {
            UserLoginInfo loginInfo = aService.GetUserLoginInfo();
            ViewBag.usertype = loginInfo.RoleType;

            


            return View();
        }

        public JsonResult DangerData()
        {
            //var list = appContext.Problem_A.ToList();
            RunCheckTask();
            var list = appContext.Problem_A.Where(m => m.Status == false && (m.PastDue == true || m.RiskLevel == "高")).ToList();
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        public void RunCheckTask()
        {
            var list = appContext.Problem_A.Where(m => m.Status == false).ToList();

            foreach (var item in list)
            {
                if (DateTime.Compare(Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(item.ReportTime)) >= 0)
                {
                    item.PastDue = true;
                }
                var date = Convert.ToDateTime(item.InsertTime).AddMonths(2);
                if(DateTime.Compare(Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(date)) >= 0)
                {
                    item.PastDue = true;
                }
            }
            appContext.SaveChanges();
        }

        public JsonResult Close(string id)
        {
            try
            {
                var temp = appContext.Problem_A.Where(m => m.ProId == id).FirstOrDefault();
                temp.Status = true;
                //appContext.Problem_A.Remove(temp);
                appContext.SaveChanges();
                return Json(MsgCommon.respJsonSuccessMsg());
            }
            catch (Exception ex)
            {
                return Json(MsgCommon.respJsonErrorMsg("删除区域信息出错。"));
            }
        }

        public JsonResult EditUser()
        {
            try
            {
                var ProId = Request["ProId"];
                var Measure = Request["Measure"];
                var Reason = Request["Reason"];

                var temp = appContext.Problem_A.Where(m => m.ProId == ProId).FirstOrDefault();
                temp.Measure = Measure;
                temp.Reason = Reason;

                appContext.SaveChanges();
                return Json(MsgCommon.respJsonSuccessMsg());
            }
            catch
            {
                //获取信息出错
                return Json("false");
            }
        }
    }
}