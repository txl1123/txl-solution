using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TxlMvc.Models;
using TxlMvc.Helper;
namespace TxlMvc.Helper
{
    public class UserHelper
    {
        private UsersContext udb = new UsersContext();
        public static bool Login(LoginModel userInfo,out string errorMsg)
        {
            errorMsg = "";
            LoginModel loginInfo = new LoginModel()
            {
                UserName = userInfo.UserName,
                Password = SecurityHelper.Encrypt(userInfo.Password)
            };
            return true;
        }

        public static bool Register(UserProfile userInfo, out string errorMsg)
        {
            errorMsg = "";
            UserProfile regInfo = new UserProfile()
            {
                UserName = userInfo.UserName,
                Password = SecurityHelper.Encrypt(userInfo.Password),
                RoleId=1//权限角色
            };
            

            return true;
        }
    }
}