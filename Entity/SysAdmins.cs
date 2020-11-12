using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class SysAdmins
    {
        //登录ID
        public int LoginId { get; set; }

        //登录名称
        public string LoginName { get; set; }


        //登录密码
        public string LoginPwd { get; set; }

        //登录角色
        public int Role { get; set; }
    }
}
