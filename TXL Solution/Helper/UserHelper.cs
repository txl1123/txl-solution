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
            //验证登录信息
            bool loginstate;
            try
            {
                return loginstate = UserProfileData.CheckLogin(loginInfo);
            }
            catch (Exception ex)
            {
                errorMsg ="用户登录出错"+ex.ToString();
                return false;
            }
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
            int registerState;
            UserProfileData userPorfile=new UserProfileData();
            var userList=userPorfile.select(0," username ","username='"+regInfo.UserName+"'",null);
            bool  isNewName = true ;
            if (userList.Count > 0)
            {
                isNewName = false;
                errorMsg = "该用户名已注册";
                return isNewName;
                
            }
            try
            {
                registerState = userPorfile.insert(regInfo);
                if (registerState > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                errorMsg = "注册出错" + ex.ToString();
                return false;
            }
        }
    }
}