using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using EF;
using Application.Impl;

namespace Problem.Controllers
{
    public class AccountController : Controller
    {
        private AppdbContent appContext = new AppdbContent();

        public ActionResult Login() => View();


        [HttpPost]
        public ActionResult LoginIn(string returnUrl)
        {
            try
            {
                var UserID = Request["userid"];
                var Password = Request["password"];
                var checkbox = Request["remember"];

                //OperationResult result = new AccountService().CheckLogin(UserID, Password);
                OperationResult result = new AccountService().CheckLogin(UserID, Password);
                if (result.ResultType == OperationResultType.Success)
                {
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        //LogHelper.Info("登录成功！", UserID);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Msg = "<script type='text/javascript'>alert('" + result.Message + "');</script>";
                    return View();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //throw;
            }
        }

        public ActionResult OutLogin()
        {
            OperationResult result = new AccountService().LoginOut();
            if (result.ResultType == OperationResultType.Success)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}