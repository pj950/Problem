using Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Problem.Extensions
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                //未登录的时候，此处加了一个判断，判断同步请求还是异步请求
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    //异步请求，返回JSON数据
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            Status = -1,
                            Message = "登录已过期，请刷新页面后操作!"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    //非异步请求，则跳转登录页
                    //FormsAuthentication.RedirectToLoginPage();//重定向会登录页
                    filterContext.HttpContext.Response.Redirect("/Account/Login");
                }
            }
            else
            {
                //1.登录状态获取用户信息（自定义保存的用户）
                var cookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                //2.使用 FormsAuthentication 解密用户凭据
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                UserLoginInfo loginUser = new UserLoginInfo();

                //3. 直接解析到用户模型里去
                loginUser = new JavaScriptSerializer().Deserialize<UserLoginInfo>(ticket.UserData);
                return;
                //4. 将要使用的数据放到ViewData 里，方便页面使用
                //filterContext.Controller.ViewData["UserName"] = loginUser.UserName;
                //filterContext.Controller.ViewData["Portrait"] = loginUser.Portrait;
                //filterContext.Controller.ViewData["UserID"] = loginUser.ID;
            }

            base.OnActionExecuting(filterContext);
        }



    }
}