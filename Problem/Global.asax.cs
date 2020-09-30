using EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Problem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private AppdbContent appContext = new AppdbContent();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //DateTime now = DateTime.Now;
            //DateTime zeroOClock = DateTime.Today.AddHours(6); //凌晨6
            //if (now > zeroOClock)
            //{
            //    zeroOClock = zeroOClock.AddDays(1.0);
            //}
            //int msUntilFour = (int)((zeroOClock - now).TotalMilliseconds);

            //var t = new System.Threading.Timer(ExcuteJob);
            //t.Change(msUntilFour, Timeout.Infinite);
            RunCheckTask();
        }

        

        public  void RunCheckTask()
        {
            var list = appContext.Problem_A.Where(m => m.Status == false).ToList();
            
            foreach (var item in list)
            {
                if (DateTime.Compare(Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(item.ReportTime)) >= 0)
                {
                    item.PastDue = true;
                }
            }
            appContext.SaveChanges();
        }

        public void ExcuteJob(object state)//Object source, ElapsedEventArgs e)
        {
            var list = appContext.Problem_A.Where(m => m.Status == false).ToList();
            foreach (var item in list)
            {
                if (DateTime.Compare(Convert.ToDateTime(item.ReportTime), Convert.ToDateTime(item.OccurredTime)) >= 0)
                {
                    item.PastDue = true;
                }
                var date = Convert.ToDateTime(item.InsertTime).AddMonths(2);
                if (DateTime.Compare(Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(date)) >= 0)
                {
                    item.PastDue = true;
                }
            }
            appContext.SaveChanges();
        }
    }
}
