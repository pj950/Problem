using System;
using System.Linq;
using Application.Model;
using Common;
using EF;
using System.Web.Security;
using System.Web;
using System.Web.Script.Serialization;

namespace Application.Impl
{
    public class AccountService : IAccountService
    {

        private AppdbContent appContext = new AppdbContent();

        public OperationResult CheckLogin(string userid, string password)
        {
            Problem_User user = appContext.Problem_User.Where(m => m.UserId == userid).FirstOrDefault();
            if (user != null)
            {
                if (user.UserPwd == password)
                {
                    UserLoginInfo userlogininfo = new UserLoginInfo
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        UserPwd = user.UserPwd,
                        RoleType = user.Type
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                       1,
                       userid,
                       DateTime.Now,
                       DateTime.Now.AddMinutes(300),
                       false,
                       serial.Serialize(userlogininfo));

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Current.Response.Cookies.Add(authCookie);

                    return new OperationResult(OperationResultType.Success, "登录成功。");
                }
                else
                {
                    return new OperationResult(OperationResultType.Warning, "登录密码不正确。");
                }
            }
            else
            {
                return new OperationResult(OperationResultType.QueryNull, "指定账号的用户不存在。");
            }
        }

        public UserLoginInfo GetUserLoginInfo()
        {
            if (HttpContext.Current.Request.IsAuthenticated)//是否通过身份验证 
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];//获取cookie 
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);//解密 
                UserLoginInfo loginUser = new UserLoginInfo();
                loginUser = new JavaScriptSerializer().Deserialize<UserLoginInfo>(ticket.UserData);
                return loginUser;
            }
            else
            {
                return null;
            }
        }

        public OperationResult LoginOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            return new OperationResult(OperationResultType.Success, "登出成功。");
        }

    }
}
