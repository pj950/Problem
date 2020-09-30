using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Model;
using Common;

namespace Application
{
    public interface IAccountService
    {
        OperationResult CheckLogin(string userid, string password);

        OperationResult LoginOut();

        UserLoginInfo GetUserLoginInfo();
    }
}
