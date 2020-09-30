using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class UserLoginInfo
    {
        public string UserId { get; set; }
        public string UserPwd { get; set; }
        public string Name { get; set; }
        public string RoleType { get; set; }
    }
}
